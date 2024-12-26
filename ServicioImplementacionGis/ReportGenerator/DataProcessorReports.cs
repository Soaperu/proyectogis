using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Data;
using DatabaseConnector;
using ArcGIS.Desktop.Editing;

namespace ReportGenerator
{
    public class DataProcessorReports
    {
        public DatabaseHandler dataBaseHandler;
        
        public DataProcessorReports()
        {
            dataBaseHandler = new DatabaseHandler();
        }
        
        private DataTable GetUbigeoData(string codigoUbigeo)
        {
            return dataBaseHandler.GetUbigeoData2(codigoUbigeo);
        }

        public static async Task<DataTable> GenerarReporteAsync(string layerName)
        {
            // DataTable que devolveremos
            DataTable lodtRegistros = new DataTable();

            // Necesitamos ejecutar en un QueuedTask para poder acceder a la capa y sus datos
            await QueuedTask.Run(async () =>
            {
                // 1. Obtenemos el mapa activo y buscamos la capa por su nombre
                var map = MapView.Active?.Map;
                if (map == null)
                {
                    MessageBox.Show("No hay un MapView activo.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // Buscamos la capa que coincida con layerName
                FeatureLayer featureLayer = map.Layers
                                               .OfType<FeatureLayer>()
                                               .FirstOrDefault(lyr => lyr.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase));
                if (featureLayer == null)
                {
                    MessageBox.Show("Layer Catastro No Existe.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // Abrimos la tabla asociada a la FeatureLayer
                Table table = featureLayer.GetTable();
                if (table == null)
                {
                    MessageBox.Show("No se pudo acceder a la tabla de la capa.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // 2. Creamos una operación de edición para actualizar los atributos DPTO, PROV, DIST
                EditOperation editOp = new EditOperation
                {
                    Name = "Actualizando campos de Ubigeo"
                };

                // Usamos el callback del EditOperation para ejecutar la lógica de edición
                editOp.Callback(context =>
                {
                    // 3. Preparamos un filtro de consulta. Aquí, por simplicidad, se consultan todas las entidades
                    QueryFilter queryFilter = new QueryFilter { WhereClause = "1=1" };

                    bool firstIteration = true; // Para crear las columnas del DataTable solo una vez

                    // Creamos un cursor de búsqueda (modo lectura y edición si así lo necesitamos)
                    using (RowCursor rowCursor = table.Search(queryFilter, false))
                    {
                        // Verificamos si hay al menos una fila
                        if (!rowCursor.MoveNext())
                        {
                            MessageBox.Show(".:: No Hay Datos para el Reporte ::.",
                                            "BDGEOCATMIN",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            return; // Nada más que hacer
                        }

                        // Regresamos al inicio del cursor para procesar todo
                        //rowCursor;//.Reset();

                        while (rowCursor.MoveNext())
                        {
                            using (Row row = rowCursor.Current)
                            {
                                // 4. Solo al entrar la primera vez definimos las columnas del DataTable
                                if (firstIteration)
                                {
                                    lodtRegistros.Columns.Add("NUM");
                                    lodtRegistros.Columns.Add("CODIGO");
                                    lodtRegistros.Columns.Add("ESTADO");
                                    lodtRegistros.Columns.Add("NOMBRE");
                                    lodtRegistros.Columns.Add("TITULAR");
                                    lodtRegistros.Columns.Add("SUSTANCIA");
                                    lodtRegistros.Columns.Add("DEPARTAMENTO");
                                    lodtRegistros.Columns.Add("PROVINCIA");
                                    lodtRegistros.Columns.Add("DISTRITO");
                                    lodtRegistros.Columns.Add("HECTAREA");
                                    firstIteration = false;
                                }

                                // Creamos un nuevo DataRow
                                DataRow dr = lodtRegistros.NewRow();

                                // Asignamos valores (similar a pRow.Value() en VB.NET)
                                dr["NUM"] = row["CONTADOR"];
                                dr["CODIGO"] = row["CODIGOU"];
                                dr["ESTADO"] = row["D_ESTADO"];
                                dr["NOMBRE"] = row["CONCESION"];
                                dr["TITULAR"] = row["TIT_CONCES"];

                                // Equivalente a SELECT CASE NATURALEZA
                                var naturaleza = row["NATURALEZA"]?.ToString();
                                if (naturaleza == "M")
                                    dr["SUSTANCIA"] = "Metálico";
                                else
                                    dr["SUSTANCIA"] = "No Metálico";

                                // 5. Lógica para obtener valores de ubigeo y actualizar
                                try
                                {
                                    var demagisValue = row["DEMAGIS"]?.ToString();
                                    if (!string.IsNullOrEmpty(demagisValue) && demagisValue.Length >= 6)
                                    {
                                        string codigoUbigeo = demagisValue.Substring(0, 6);

                                        // Ejecución de tu método que obtiene el DataTable con el Ubigeo
                                        // Se asume que cls_Oracle.F_Obtiene_Datos_UBIGEO(...) está disponible
                                        var mc = new DataProcessorReports();
                                        DataTable lodtbUbigeo = mc.GetUbigeoData(codigoUbigeo);

                                        if (lodtbUbigeo != null && lodtbUbigeo.Rows.Count > 0)
                                        {
                                            // Asignamos al DataRow
                                            dr["DEPARTAMENTO"] = lodtbUbigeo.Rows[0]["DPTO"].ToString();
                                            dr["PROVINCIA"] = lodtbUbigeo.Rows[0]["PROV"].ToString();
                                            dr["DISTRITO"] = lodtbUbigeo.Rows[0]["DIST"].ToString();
                                            dr["HECTAREA"] = row["HASDATUM"];

                                            // Actualizamos atributos en la propia entidad
                                            // Marcamos la fila para edición con "context.Invalidate(row)"
                                            row["DPTO"] = lodtbUbigeo.Rows[0]["DPTO"].ToString();
                                            row["PROV"] = lodtbUbigeo.Rows[0]["PROV"].ToString();
                                            row["DIST"] = lodtbUbigeo.Rows[0]["DIST"].ToString();

                                            // Invalidamos y guardamos cambios
                                            context.Invalidate(row);
                                            row.Store();
                                        }
                                        else
                                        {
                                            // Si no hay ubigeo
                                            // (Podrías asignar valores por defecto o dejar en blanco)
                                            dr["DEPARTAMENTO"] = "";
                                            dr["PROVINCIA"] = "";
                                            dr["DISTRITO"] = "";
                                            dr["HECTAREA"] = row["HASDATUM"];
                                        }
                                    }
                                    else
                                    {
                                        // DEMAGIS no tiene datos válidos
                                        dr["DEPARTAMENTO"] = "";
                                        dr["PROVINCIA"] = "";
                                        dr["DISTRITO"] = "";
                                        dr["HECTAREA"] = row["HASDATUM"];
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Manejo de excepción
                                    MessageBox.Show("Error obteniendo Ubigeo: " + ex.Message);
                                }

                                // Agregamos la fila al DataTable
                                lodtRegistros.Rows.Add(dr);
                            }
                        } // while
                    } // using RowCursor
                }, table); // editOp.Callback

                // 6. Ejecutamos la operación de edición
                bool result = await editOp.ExecuteAsync();
                if (!result)
                {
                    // Si falla, puedes revisar editOp.ErrorMessage
                    MessageBox.Show($"No se pudieron actualizar registros: {editOp.ErrorMessage}",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            });

            // 7. Devolvemos el DataTable
            return lodtRegistros;
        }
    }
}
