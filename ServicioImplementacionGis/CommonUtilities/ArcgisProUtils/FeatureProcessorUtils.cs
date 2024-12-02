using ArcGIS.Core.Data;
using ArcGIS.Core.Data.DDL;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.ArcgisProUtils
{
    public class FeatureProcessorUtils
    {
        private static FeatureLayer GetFeatureLayerFromMap(MapView mapView, string shapefileName)
        {
            foreach (var layer in mapView.Map.Layers)
            {
                if (layer is FeatureLayer featureLayer && featureLayer.Name.Equals(shapefileName, StringComparison.OrdinalIgnoreCase))
                {
                    return featureLayer;
                }
            }
            return null;
        }

        /// <summary>
        /// Elimina un campo si ya existe.
        /// </summary>
        public static async Task<bool> DeleteFieldIfExists(FeatureLayer featureLayer, string fieldName)
        {
            var fieldIndex = featureLayer.GetTable().GetDefinition().FindField(fieldName);
            if (fieldIndex >= 0)
            {
                try
                {
                    return await QueuedTask.Run(() =>
                    {
                        var inTable = featureLayer.Name;
                        var table = featureLayer.GetTable();
                        var dataStore = table.GetDatastore();
                        var workspaceNameDef = dataStore.GetConnectionString();
                        var workspaceName = workspaceNameDef.Split('=')[1];

                        var fullSpec = System.IO.Path.Combine(workspaceName, inTable);
                        System.Diagnostics.Debug.WriteLine($@"Delete {fieldName} from {fullSpec}");

                        var parameters = Geoprocessing.MakeValueArray(fullSpec, fieldName);
                        var cts = new CancellationTokenSource();
                        var results = Geoprocessing.ExecuteToolAsync("management.DeleteField", parameters, null, cts.Token,
                            (eventName, o) =>
                            {
                                System.Diagnostics.Debug.WriteLine($@"GP event: {eventName}");
                                if (eventName == "OnMessage")
                                {
                                    System.Diagnostics.Debug.WriteLine($@"Msg: {o}");
                                }
                            });
                        return true;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Añade un campo si no existe en un featureClass o Shapefile.
        /// </summary>
        /// <param name="fieldType">Puede ser: STRING, LONG, DOUBLE, DATE, GUID</param>
        public static async Task AddFieldIfNotExistsAsync(FeatureLayer featureLayer, string fieldName, string fieldType, int fieldLength, int? fieldPrecision = null, int? fieldScale = null)
        {
            if (featureLayer == null)
            {
                throw new ArgumentNullException(nameof(featureLayer), "El FeatureLayer no puede ser nulo.");
            }
            // Ejecutamos la herramienta AddField_management de Geoprocessing
            await QueuedTask.Run(() =>
            {
            // Verificamos si el campo ya existe
            var featureClass = featureLayer.GetTable() as FeatureClass;
            var fieldIndex = featureClass.GetDefinition().FindField(fieldName);

            if (fieldIndex == -1)  // El campo no existe, lo vamos a crear
            {
                // Construimos los parámetros para la herramienta AddField_management
                var parameters = new List<object>
                {
                    featureLayer,         // Feature Layer donde agregar el campo
                    fieldName,            // Nombre del nuevo campo
                    fieldType,            // Tipo de campo (e.g., "TEXT", "LONG", "DOUBLE", etc.)
                    fieldLength,          // Longitud del campo (en el caso de tipos de texto)
                    fieldPrecision ?? -1, // Precisión (si aplica, por ejemplo para campos numéricos)
                    fieldScale ?? -1      // Escala (si aplica, por ejemplo para campos numéricos)
                };
                var toolParams = Geoprocessing.MakeValueArray(parameters.ToArray());
                Geoprocessing.ExecuteToolAsync("AddField_management", toolParams);
            }
            else
            {
                // Si el campo ya existe, no es necesario agregarlo
                System.Diagnostics.Debug.WriteLine($"El campo {fieldName} ya existe en la Feature Layer.");
            }
            });
            
        }

        /// <summary>
        /// Añade un campo si no existe en un featureClass dentro de un GDB.
        /// </summary>
        public static async void AddFieldIfNotExistsGdb(FeatureLayer featureLayer, string fieldName, FieldType fieldType, int length, int scale=0, int precision=0)
        {
            var result = await QueuedTask.Run(() =>
            {
                // Verifica si existe el campo
                var fieldIndex = featureLayer.GetTable().GetDefinition().FindField(fieldName);
                if (fieldIndex == -1)
                {
                    var selectedLayerTable = featureLayer.GetTable();
                    var fieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription(fieldName, fieldType);
                    using var geoDb = selectedLayerTable.GetDatastore() as Geodatabase;
                    var fcName = selectedLayerTable.GetName();
                    try
                    {
                        FeatureClassDefinition originalFeatureClassDefinition = geoDb.GetDefinition<FeatureClassDefinition>(fcName);
                        FeatureClassDescription originalFeatureClassDescription = new FeatureClassDescription(originalFeatureClassDefinition);

                        // Recopila una lista de todas las descripciones de campos nuevos
                        if (FieldType.String == fieldType)
                        {
                            fieldDescription.Length = length;
                        }
                        else if (FieldType.Integer == fieldType) 
                        {
                            fieldDescription.Scale = scale;
                            fieldDescription.Precision = precision; 
                        }

                        var fieldDescriptions = new List<ArcGIS.Core.Data.DDL.FieldDescription>() {
                                                                                                    fieldDescription
                                                                                                    };
                        // Cree un objeto FeatureClassDescription para describir la clase de entidad que se creará
                        var fcDescription = new FeatureClassDescription(fcName, fieldDescriptions, originalFeatureClassDescription.ShapeDescription);

                        // Crear un objeto SchemaBuilder
                        SchemaBuilder schemaBuilder = new SchemaBuilder(geoDb);

                        // Agregue la modificación de la clase de entidad a nuestra lista de tareas DDL
                        schemaBuilder.Modify(fcDescription);

                        // Execute the DDL
                        bool success = schemaBuilder.Build();
                    }
                    catch (Exception ex)
                    {
                        return $@"Exception: {ex}";
                    }
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            });
        }

        public static string ProcessFeatureFields(string loFeature, Row row, string casoConsulta, out string lostrJoinCodigosMarcona, out string validaUrbShp, out string lostrJoinCodigosArea)
        {
            lostrJoinCodigosMarcona = string.Empty;
            validaUrbShp = string.Empty;
            lostrJoinCodigosArea = string.Empty;
            string lostrJoinCodigos = string.Empty;

            int fieldIndex;
            string codigo;

            switch (loFeature)
            {
                case "Catastro":
                    fieldIndex = row.FindField("OBJECTID");
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{codigo},";
                    break;

                case "Departamento":
                    fieldIndex = row.FindField("NM_DEPA");
                    string nmDepa = row[fieldIndex].ToString();
                    if (nmDepa != "MAR" && nmDepa != "FUERA DEL PERU")
                    {
                        lostrJoinCodigos += $"'{nmDepa}',";
                    }
                    break;

                case "Provincia":
                case "Prov_Colindantes":
                    fieldIndex = row.FindField("CD_PROV");
                    string cdProv = row[fieldIndex].ToString();
                    if (cdProv != "9901" && cdProv != "9903")
                    {
                        lostrJoinCodigos += $"'{cdProv}',";
                    }
                    break;

                case "Distrito":
                    fieldIndex = row.FindField("OBJECTID");
                    codigo = row[fieldIndex].ToString();
                    if (casoConsulta == "CARTA IGN" || casoConsulta == "DEMARCACION POLITICA")
                    {
                        lostrJoinCodigos += $"'{codigo}',";
                    }
                    else
                    {
                        fieldIndex = row.FindField("CD_DIST");
                        string cdDist = row[fieldIndex].ToString();
                        if (cdDist != "990101" && cdDist != "990301")
                        {
                            lostrJoinCodigos += $"'{codigo}',";
                        }
                    }
                    break;

                case "Zona Urbana":
                    fieldIndex = row.FindField("NOMBRE");
                    string nombreZona = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{nombreZona}',";
                    lostrJoinCodigosMarcona = nombreZona;
                    if (lostrJoinCodigosMarcona == "SAN JUAN DE MARCONA")
                    {
                        validaUrbShp = "1";
                    }
                    break;

                case "Zona Reservada":
                    fieldIndex = row.FindField("NM_RESE");
                    string nmRese = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{nmRese}',";
                    break;

                case "Caram":
                    fieldIndex = row.FindField("NM_AREA");
                    string nmArea = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{nmArea}',";
                    break;

                case "Cuadrangulo":
                    fieldIndex = row.FindField("CD_HOJA");
                    string cdHoja = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{cdHoja}',";
                    break;

                case "Limite Frontera":
                    fieldIndex = row.FindField("CODIGO");
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    break;

                case "Catastro Forestal":
                    fieldIndex = row.FindField("CD_CONCE");
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    break;

                case "Zona Traslape":
                    codigo = row[2].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    break;

                case "Limite de Zona":
                    fieldIndex = row.FindField("ZONA");
                    string zona = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{zona}',";
                    break;

                case "Capitales Distritales":
                    fieldIndex = row.FindField("DISTRITO");
                    string distrito = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{distrito}',";
                    break;

                case "Red_Hidrografica":
                    fieldIndex = row.FindField("CD_DEPA");
                    string cdDepa = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{cdDepa}',";
                    break;

                case "Certificacion Ambiental":
                    fieldIndex = row.FindField("ID_RECURSO");
                    string idRecurso = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{idRecurso}',";
                    break;

                case "DM_Uso_Minero":
                    fieldIndex = row.FindField("ID_AREA_USOMINERO");
                    string idAreaUsoMinero = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{idAreaUsoMinero}',";
                    break;

                case "DM_Actividad_Minera":
                    fieldIndex = row.FindField("CODIGO");
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    fieldIndex = row.FindField("ANOPRO");
                    string anoPro = row[fieldIndex].ToString();
                    lostrJoinCodigosArea = anoPro;
                    break;

                default:
                    codigo = row[1].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    break;
            }

            return lostrJoinCodigos;
        }
        public static string GenerateJoinCondition(string loFeature, string lostrJoinCodigos)
        {
            // Validar que lostr_Join_Codigos no sea nulo ni vacío
            if (string.IsNullOrEmpty(lostrJoinCodigos))
            {
                throw new ArgumentException("El parámetro 'lostr_Join_Codigos' no puede ser nulo o vacío.");
            }

            // Remover la última coma de la cadena
            lostrJoinCodigos = lostrJoinCodigos.TrimEnd(',');

            // Procesar según el valor de loFeature
            switch (loFeature)
            {
                case "Departamento":
                    return $"NM_DEPA IN ({lostrJoinCodigos})";

                case "Provincia":
                case "Distrito":
                    return $"OBJECTID IN ({lostrJoinCodigos})";

                case "Zona Urbana":
                    return $"NOMBRE IN ({lostrJoinCodigos})";

                case "Zona Reservada":
                    return $"NM_RESE IN ({lostrJoinCodigos})";

                case "Caram":
                    return $"NM_AREA IN ({lostrJoinCodigos})";

                case "Catastro Forestal":
                    return $"CD_CONCE IN ({lostrJoinCodigos})";

                case "Zona Traslape":
                    return $"DESCRIP IN ({lostrJoinCodigos})";

                case "Capitales Distritales":
                    return $"DISTRITO IN ({lostrJoinCodigos})";

                case "Certificacion Ambiental":
                    return $"ID_RECURSO IN ({lostrJoinCodigos})";

                case "DM_Uso_Minero":
                case "DM_Actividad_Minera":
                case "Prov_Colindantes":
                    return $"CODIGO IN ({lostrJoinCodigos})";

                default:
                    return $"OBJECTID IN ({lostrJoinCodigos})";
            }
        }

        public async static Task AgregarCampoTemaTpm(string pShapefile, string caso)
        {
            try
            {
                // Obtener el mapa actual y la capa de entidad
                var mapView = MapView.Active as MapView;
                if (mapView == null)
                {
                    MessageBox.Show("No se ha encontrado la vista activa.");
                    return;
                }

                var featureLayer = GetFeatureLayerFromMap(mapView, pShapefile);
                if (featureLayer == null)
                {
                    MessageBox.Show("No se encuentra la capa en el mapa.");
                    return;
                }

                //var featureClass = featureLayer.GetTable() as FeatureClass;

                // Añadir o eliminar campos según el caso
                if ( caso == "CODIGO")
                {
                    // Añadir el campo "CODIGO" si no existe
                    AddFieldIfNotExistsAsync(featureLayer, "CODIGO", "STRING", 16);
                }
                else if (caso == "ExclusionUEA")
                {
                    // Eliminar los campos existentes "TIT_CONCES" y "LEYENDA"
                    _ = DeleteFieldIfExists(featureLayer, "TIT_CONCES");
                    _ = DeleteFieldIfExists(featureLayer, "LEYENDA");

                    // Añadir nuevos campos
                    await AddFieldIfNotExistsAsync(featureLayer, "MOVIMIENTO", GlobalVariables.fieldTypeString, 1);
                    await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 1);
                    await AddFieldIfNotExistsAsync(featureLayer, "COD_UEA", GlobalVariables.fieldTypeString, 13);
                    await AddFieldIfNotExistsAsync(featureLayer, "NOM_UEA", GlobalVariables.fieldTypeString, 100);
                    await AddFieldIfNotExistsAsync(featureLayer, "LEYENDA", GlobalVariables.fieldTypeString, 2);
                }
                else if (caso == "Catastro")
                {
                    // Comprobamos si los campos existen, y si no, los agregamos.
                    await AddFieldIfNotExistsAsync(featureLayer, "EVAL", GlobalVariables.fieldTypeString, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "CALCULO", GlobalVariables.fieldTypeString, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "AREA_INT", GlobalVariables.fieldTypeString, 20, 4); // Precision 20, escala 4
                    await AddFieldIfNotExistsAsync(featureLayer, "DPTO", GlobalVariables.fieldTypeString, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "PROV", GlobalVariables.fieldTypeString, 40);
                    await AddFieldIfNotExistsAsync(featureLayer, "DIST", GlobalVariables.fieldTypeString, 50);
                    await AddFieldIfNotExistsAsync(featureLayer, "CONTADOR", GlobalVariables.fieldTypeString, 20);
                    await AddFieldIfNotExistsAsync(featureLayer, "NUM_RESOL", GlobalVariables.fieldTypeString, 20);
                    await AddFieldIfNotExistsAsync(featureLayer, "FEC_RESOL", GlobalVariables.fieldTypeDate, 20);
                    await AddFieldIfNotExistsAsync(featureLayer, "CALIF", GlobalVariables.fieldTypeDate, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "DISTS", GlobalVariables.fieldTypeDate, 256);
                    await AddFieldIfNotExistsAsync(featureLayer, "PROVS", GlobalVariables.fieldTypeString, 256);
                    await AddFieldIfNotExistsAsync(featureLayer, "DPTOS", GlobalVariables.fieldTypeString, 256);
                    await AddFieldIfNotExistsAsync(featureLayer, "TIPO", GlobalVariables.fieldTypeString, 80);
                    await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 20);
                    await AddFieldIfNotExistsAsync(featureLayer, "DATUM", GlobalVariables.fieldTypeString, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "BLOQUEO", GlobalVariables.fieldTypeString, 10);
                    await AddFieldIfNotExistsAsync(featureLayer, "CASO", GlobalVariables.fieldTypeString, 40);
                }
                // Otros casos a agregar de manera similar

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

}


