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
using CommonUtilities;
using DevExpress.DataAccess.Sql.DataApi;
using ArcGIS.Core.CIM;
//using ArcGIS.Core.Internal.CIM;
using DevExpress.XtraCharts.Native;
using System.Windows.Forms;
using System.Windows.Media;
using ArcGIS.Core.Geometry;
using Microsoft.VisualBasic;



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
                        // Procesamos todas las filas
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

                                    // Agregamos la fila al DataTable
                                    lodtRegistros.Rows.Add(dr);


                                }
                                catch (Exception ex)
                                {
                                    // Manejo de excepción
                                    MessageBox.Show("Error obteniendo Ubigeo: " + ex.Message);
                                }

                                
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

        //
        // --------------------------------------------------------------------------------------------------------
        //


        public async Task<DataTable> GenerarReporteSPEAsync()
        {
            DataTable lodtbReporte = new DataTable();
            lodtbReporte.Columns.Add("NUM", typeof(string));
            lodtbReporte.Columns.Add("CODIGO", typeof(string));
            lodtbReporte.Columns.Add("NOMBRE", typeof(string));
            lodtbReporte.Columns.Add("ZONA", typeof(string));
            lodtbReporte.Columns.Add("TE", typeof(string));
            lodtbReporte.Columns.Add("TP", typeof(string));
            lodtbReporte.Columns.Add("PUBL", typeof(string));
            lodtbReporte.Columns.Add("INCOR", typeof(string));
            lodtbReporte.Columns.Add("SUST", typeof(string));


            await QueuedTask.Run(() =>
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

                // Buscamos la capa que coincida con el nombre "Catastro"
                FeatureLayer featureLayer = map.Layers.OfType<FeatureLayer>()
                                             .FirstOrDefault(lyr => lyr.Name.Equals("Catastro", StringComparison.OrdinalIgnoreCase));

                if (featureLayer == null)
                {
                    MessageBox.Show("Layer Catastro No Existe.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                FeatureClass fclas_tema = featureLayer.GetFeatureClass();

                QueryFilter queryFilter = new QueryFilter { WhereClause = "1=1" };

                // Creamos un cursor de búsqueda
                using (RowCursor cursor = fclas_tema.Search(queryFilter, false))
                {
                    // Procesamos todas las filas
                    while (cursor.MoveNext())
                    {
                        using (Row row = cursor.Current)
                        {
                            DataRow dRow = lodtbReporte.NewRow();
                            dRow["NUM"] = row["CONTADOR"];
                            dRow["CODIGO"] = row["CODIGOU"];
                            dRow["NOMBRE"] = row["CONCESION"];
                            dRow["ZONA"] = row["ZONA"];
                            dRow["TE"] = row["TIPO_EX"];
                            dRow["TP"] = row["ESTADO"];
                            dRow["PUBL"] = row["DE_PUBL"].ToString();
                            if (string.IsNullOrWhiteSpace(row["DE_PUBL"].ToString()))
                            {
                                dRow["PUBL"] = "NP";
                            }

                            dRow["INCOR"] = row["DE_IDEN"].ToString();
                            if (string.IsNullOrWhiteSpace(row["DE_IDEN"].ToString()))
                            {
                                dRow["INCOR"] = "NI";
                            }

                            dRow["SUST"] = row["NATURALEZA"];
                            lodtbReporte.Rows.Add(dRow);
                        }
                    }
                }
            });


            return lodtbReporte;
        }

        //
        //
        // --------------------------------------------------------------------------------------------------------

        public async Task<DataTable> LeerResultadosEvalReporteAsync()
        {
            DataTable lodtbReporte = new DataTable();
            lodtbReporte.Columns.Add("CONTADOR", typeof(string));
            lodtbReporte.Columns.Add("CONCESION", typeof(string));
            lodtbReporte.Columns.Add("TIPO_EX", typeof(string));
            lodtbReporte.Columns.Add("CODIGOU", typeof(string));
            lodtbReporte.Columns.Add("ESTADO", typeof(string));
            lodtbReporte.Columns.Add("EVAL", typeof(string));
            lodtbReporte.Columns.Add("ORDEN", typeof(int));

            await QueuedTask.Run(() =>
            {
                var map = MapView.Active?.Map;
                if (map == null)
                {
                    MessageBox.Show("No hay un MapView activo.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // Buscamos la capa que coincida con el nombre "Catastro"
                FeatureLayer pFeatLayer = map.Layers.OfType<FeatureLayer>()
                               .FirstOrDefault(lyr => lyr.Name.Equals("Catastro", StringComparison.OrdinalIgnoreCase));

                if (pFeatLayer == null)
                {
                    MessageBox.Show("Layer Catastro No Existe.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                FeatureClass fclas_tema = pFeatLayer.GetFeatureClass();

                int Cuenta_an = 0, Cuenta_po = 0, Cuenta_si = 0, Cuenta_ex = 0, Cuenta_rd = 0;
                string[] criterios = { "PR", "PO", "SI", "EX", "AR" };

                foreach (var criterio in criterios)
                {
                    QueryFilter queryFilter = new QueryFilter
                    {
                        WhereClause = $"EVAL = '{criterio}'"
                    };

                    int cuenta = 0;
                    using (var cursor = fclas_tema.Search(queryFilter, false))
                    {
                        while (cursor.MoveNext())
                        {
                            using (Row row = cursor.Current)
                            {
                                var dRow = lodtbReporte.NewRow();
                                dRow["CONTADOR"] = row["CONTADOR"];
                                dRow["CONCESION"] = row["CONCESION"];
                                dRow["TIPO_EX"] = row["TIPO_EX"];
                                dRow["CODIGOU"] = row["CODIGOU"];
                                dRow["ESTADO"] = row["ESTADO"];
                                dRow["EVAL"] = ""; // Asignar EVAL vacío temporalmente
                                dRow["ORDEN"] = Array.IndexOf(criterios, criterio);
                                lodtbReporte.Rows.Add(dRow);
                                cuenta++;
                            }
                        }
                    }

                    switch (criterio)
                    {
                        case "PR":
                            Cuenta_an = cuenta;
                            break;
                        case "PO":
                            Cuenta_po = cuenta;
                            break;
                        case "SI":
                            Cuenta_si = cuenta;
                            break;
                        case "EX":
                            Cuenta_ex = cuenta;
                            break;
                        case "AR":
                            Cuenta_rd = cuenta;
                            break;
                    }



                    // Actualizar las filas de lodtbReporte con el valor correcto de EVAL
                    foreach (DataRow dRow in lodtbReporte.Rows)
                    {
                        if (dRow["EVAL"].ToString() == "")
                        {
                            dRow["EVAL"] = criterio switch
                            {
                                "PR" => $"DERECHOS PRIORITARIOS : ({Cuenta_an})",
                                "PO" => (GlobalVariables.CurrentTipoEx == "RD") ? $"DERECHOS QUE DEBEN RESPETAR EL AREA PETICIONADA: ({Cuenta_rd})" : $"DERECHOS POSTERIORES : ({Cuenta_po})",
                                "SI" => $"DERECHOS SIMULTANEOS : ({Cuenta_si})",
                                "EX" => $"DERECHOS EXTINGUIDOS : ({Cuenta_ex})",
                                "AR" => $"Petitorio formulado al amparo del Art.12 de la Ley 26615, sobre el área del derecho extinguido y publicado de libre denunciabilidad ({Cuenta_rd})",
                                _ => ""
                            };
                        }
                    }
                }

                // Agregar filas adicionales si no hay resultados
                if (Cuenta_an == 0) lodtbReporte.Rows.Add(CreateEmptyRow(lodtbReporte, "DERECHOS PRIORITARIOS : No Presenta DM Prioritarios", 1));
                if (Cuenta_po == 0)
                {
                    lodtbReporte.Rows.Add(CreateEmptyRow(lodtbReporte, GlobalVariables.CurrentTipoEx == "RD" ? "DERECHOS QUE DEBEN RESPETAR EL AREA PETICIONADA : No Presenta" : "DERECHOS POSTERIORES : No Presenta DM Posteriores", 2));
                }
                if (Cuenta_si == 0) lodtbReporte.Rows.Add(CreateEmptyRow(lodtbReporte, "DERECHOS SIMULTANEOS : No Presenta DM Simultaneos", 3));
                if (Cuenta_ex == 0) lodtbReporte.Rows.Add(CreateEmptyRow(lodtbReporte, "DERECHOS EXTINGUIDOS : No Presenta DM Extinguidos", 4));


            });

            return lodtbReporte;

        }

        private DataRow CreateEmptyRow(DataTable table, string evalValue, int orden)
        {
            DataRow row = table.NewRow();
            row["CONTADOR"] = "";
            row["CONCESION"] = "";
            row["TIPO_EX"] = "";
            row["CODIGOU"] = "";
            row["ESTADO"] = "";
            row["EVAL"] = evalValue;
            row["ORDEN"] = orden;
            return row;
        }

        public async Task<DataTable> LeerResultadosAreasReporteAsync()
        {
            DataTable lodtbReporte = new DataTable();
            lodtbReporte.Columns.Add("FID", typeof(int));
            lodtbReporte.Columns.Add("CODIGO", typeof(string));
            lodtbReporte.Columns.Add("CONCESION", typeof(string));
            lodtbReporte.Columns.Add("VERTICE", typeof(int));
            lodtbReporte.Columns.Add("ESTE", typeof(string));
            lodtbReporte.Columns.Add("NORTE", typeof(string));
            lodtbReporte.Columns.Add("AREA", typeof(double));
            //lodtbReporte.Columns.Add("NUM_AREA", typeof(double));

            await QueuedTask.Run(() =>
            {
                // 1. Obtenemos el mapa activo y buscamos la capa por su nombre
                var map = MapView.Active?.Map;
                FeatureLayer featureLayer = map.Layers
                                                   .OfType<FeatureLayer>()
                                                   .FirstOrDefault(lyr => lyr.Name.StartsWith("Areainter", StringComparison.OrdinalIgnoreCase));

                if (featureLayer == null)
                {
                    MessageBox.Show("Layer Catastro No Existe.",
                                    "BDGEOCATMIN",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                FeatureClass fclas_tema = featureLayer.GetFeatureClass();

                QueryFilter queryFilter = new QueryFilter { WhereClause = "1=1" };

                // Creamos un cursor de búsqueda
                using (RowCursor cursor = fclas_tema.Search(queryFilter, false))
                {
                    // Procesamos todas las filas
                    while (cursor.MoveNext())
                    {
                        using (Row row = cursor.Current)
                        {
                            Feature feature = (Feature)cursor.Current;
                            Polygon polygon = feature.GetShape() as Polygon;

                            if (polygon != null)
                            {
                                for (int i = 0; i < polygon.PointCount - 1; i++)
                                {
                                    var point = polygon.Points[i];
                                    var dRow = lodtbReporte.NewRow();
                                    dRow["FID"] = row["FID"];
                                    dRow["CODIGO"] = row["CODIGOU_1"];
                                    dRow["CONCESION"] = row["CONCESIO_1"];
                                    dRow["VERTICE"] = i+1;
                                    dRow["ESTE"] = Math.Round(point.X, 2).ToString("N2");
                                    dRow["NORTE"] = Math.Round(point.Y, 2).ToString("N2");
                                    dRow["AREA"] = Math.Round(Convert.ToDouble(row["AREA_FINAL"]), 4);
                                    lodtbReporte.Rows.Add(dRow);
                                }
                            }
                        }

                    }
                }
            });

            return lodtbReporte;
        }

    }
}

