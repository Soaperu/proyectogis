﻿using ArcGIS.Core.Data;
using ArcGIS.Core.Data.DDL;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Geometry;
//using ArcGIS.Core.Internal.CIM;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using DatabaseConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = ArcGIS.Desktop.Framework.Dialogs.MessageBox;
/**/


namespace CommonUtilities.ArcgisProUtils
{
    public class FeatureProcessorUtils
    {
        public static FeatureLayer GetFeatureLayerFromMap(MapView mapView, string layerName)
        {
            foreach (var layer in mapView.Map.Layers)
            {
                if (layer is FeatureLayer featureLayer && featureLayer.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase))
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
        public static async void AddFieldIfNotExistsGdb(FeatureLayer featureLayer, string fieldName, FieldType fieldType, int length, int scale = 0, int precision = 0)
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

        public static string ProcessDataRowsFields(string loFeature, DataRow row, string casoConsulta, out string lostrJoinCodigosMarcona, out string validaUrbShp, out string lostrJoinCodigosArea)
        {
            lostrJoinCodigosMarcona = string.Empty;
            validaUrbShp = string.Empty;
            lostrJoinCodigosArea = string.Empty;
            string lostrJoinCodigos = string.Empty;

            string fieldIndex;
            string codigo;

            switch (loFeature)
            {
                case "Catastro":
                    fieldIndex = "OBJECTID";
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{codigo},";
                    break;

                case "Departamento":
                    fieldIndex = "NM_DEPA";
                    string nmDepa = row[fieldIndex].ToString();
                    if (nmDepa != "MAR" && nmDepa != "FUERA DEL PERU")
                    {
                        lostrJoinCodigos += $"{nmDepa},";
                    }
                    break;

                case "Provincia":
                case "Prov_Colindantes":
                    fieldIndex = "NM_PROV";
                    string cdProv = row[fieldIndex].ToString();
                    if (cdProv != "9901" && cdProv != "9903")
                    {
                        lostrJoinCodigos += $"{cdProv},";
                    }
                    break;

                case "Distrito":
                    fieldIndex = "NM_DIST";
                    codigo = row[fieldIndex].ToString();
                    if (casoConsulta == "CARTA IGN" || casoConsulta == "DEMARCACION POLITICA")
                    {
                        lostrJoinCodigos += $"{codigo} ,";
                    }
                    else
                    {
                        //fieldIndex = "NM_DIST";
                        string cdDist = row[fieldIndex].ToString();
                        if (cdDist != "990101" && cdDist != "990301")
                        {
                            lostrJoinCodigos += $"{codigo},";
                        }
                    }
                    break;

                case "Zona Urbana":
                    fieldIndex = "NOMBRE";
                    string nombreZona = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{nombreZona} ,";
                    lostrJoinCodigosMarcona = nombreZona;
                    if (lostrJoinCodigosMarcona == "SAN JUAN DE MARCONA")
                    {
                        validaUrbShp = "1";
                    }
                    break;

                case "Zona Reservada":
                    fieldIndex = "NM_RESE";
                    string nmRese = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{nmRese},";
                    break;

                case "Caram":
                    fieldIndex = "NM_AREA";
                    string nmArea = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{nmArea},";
                    break;

                case "Cuadrangulo":
                    fieldIndex = "CD_HOJA";
                    string cdHoja = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{cdHoja}',";
                    break;

                case "Limite Frontera":
                    fieldIndex = "CODIGO";
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    break;

                case "Catastro Forestal":
                    fieldIndex = "CD_CONCE";
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{codigo},";
                    break;

                case "Zona Traslape":
                    codigo = row[2].ToString();
                    lostrJoinCodigos += $"{codigo},";
                    break;

                case "Limite de Zona":
                    fieldIndex = "ZONA";
                    string zona = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{zona},";
                    break;

                case "Capitales Distritales":
                    fieldIndex = "DISTRITO";
                    string distrito = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"{distrito},";
                    break;

                case "Red_Hidrografica":
                    fieldIndex = "CD_DEPA";
                    string cdDepa = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{cdDepa}',";
                    break;

                case "Certificacion Ambiental":
                    fieldIndex = "ID_RECURSO";
                    string idRecurso = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{idRecurso}',";
                    break;

                case "DM_Uso_Minero":
                    fieldIndex = "ID_AREA_USOMINERO";
                    string idAreaUsoMinero = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{idAreaUsoMinero}',";
                    break;

                case "DM_Actividad_Minera":
                    fieldIndex = "CODIGO";
                    codigo = row[fieldIndex].ToString();
                    lostrJoinCodigos += $"'{codigo}',";
                    fieldIndex = "ANOPRO";
                    string anoPro = row[fieldIndex].ToString();
                    lostrJoinCodigosArea = anoPro;
                    break;

                default:
                    codigo = row[1].ToString();
                    lostrJoinCodigos += $"{codigo},";
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

                case "Carta IGN":
                    return $"CD_HOJA IN ({lostrJoinCodigos})";

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

                case "Carta IGN":
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

        //public async static Task AgregarCampoTemaTpm(string pShapefile, string caso)
        //{
        //    try
        //    {
        //        // Obtener el mapa actual y la capa de entidad
        //        var mapView = MapView.Active as MapView;
        //        if (mapView == null)
        //        {
        //            MessageBox.Show("No se ha encontrado la vista activa.");
        //            return;
        //        }

        //        var featureLayer = GetFeatureLayerFromMap(mapView, pShapefile);
        //        if (featureLayer == null)
        //        {
        //            MessageBox.Show("No se encuentra la capa en el mapa.");
        //            return;
        //        }

        //        //var featureClass = featureLayer.GetTable() as FeatureClass;

        //        // Añadir o eliminar campos según el caso
        //        if (caso == "CODIGO")
        //        {
        //            // Añadir el campo "CODIGO" si no existe
        //            AddFieldIfNotExistsAsync(featureLayer, "CODIGO", "STRING", 16);
        //        }
        //        else if (caso == "ExclusionUEA")
        //        {
        //            // Eliminar los campos existentes "TIT_CONCES" y "LEYENDA"
        //            _ = DeleteFieldIfExists(featureLayer, "TIT_CONCES");
        //            _ = DeleteFieldIfExists(featureLayer, "LEYENDA");

        //            // Añadir nuevos campos
        //            await AddFieldIfNotExistsAsync(featureLayer, "MOVIMIENTO", GlobalVariables.fieldTypeString, 1);
        //            await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 1);
        //            await AddFieldIfNotExistsAsync(featureLayer, "COD_UEA", GlobalVariables.fieldTypeString, 13);
        //            await AddFieldIfNotExistsAsync(featureLayer, "NOM_UEA", GlobalVariables.fieldTypeString, 100);
        //            await AddFieldIfNotExistsAsync(featureLayer, "LEYENDA", GlobalVariables.fieldTypeString, 2);
        //        }
        //        else if (caso == "Catastro")
        //        {
        //            // Comprobamos si los campos existen, y si no, los agregamos.
        //            await AddFieldIfNotExistsAsync(featureLayer, "EVAL", GlobalVariables.fieldTypeString, 10);
        //            await AddFieldIfNotExistsAsync(featureLayer, "CALCULO", GlobalVariables.fieldTypeString, 10);
        //            await AddFieldIfNotExistsAsync(featureLayer, "AREA_INT", GlobalVariables.fieldTypeDouble, 20, 4); // Precision 20, escala 4
        //            await AddFieldIfNotExistsAsync(featureLayer, "DPTO", GlobalVariables.fieldTypeString, 20);
        //            await AddFieldIfNotExistsAsync(featureLayer, "PROV", GlobalVariables.fieldTypeString, 40);
        //            await AddFieldIfNotExistsAsync(featureLayer, "DIST", GlobalVariables.fieldTypeString, 50);
        //            await AddFieldIfNotExistsAsync(featureLayer, "CONTADOR", GlobalVariables.fieldTypeString, 20);
        //            await AddFieldIfNotExistsAsync(featureLayer, "NUM_RESOL", GlobalVariables.fieldTypeString, 20);
        //            await AddFieldIfNotExistsAsync(featureLayer, "FEC_RESOL", GlobalVariables.fieldTypeDate, 20);
        //            await AddFieldIfNotExistsAsync(featureLayer, "CALIF", GlobalVariables.fieldTypeString, 10);
        //            await AddFieldIfNotExistsAsync(featureLayer, "DISTS", GlobalVariables.fieldTypeString, 256);
        //            await AddFieldIfNotExistsAsync(featureLayer, "PROVS", GlobalVariables.fieldTypeString, 256);
        //            await AddFieldIfNotExistsAsync(featureLayer, "DPTOS", GlobalVariables.fieldTypeString, 256);
        //            await AddFieldIfNotExistsAsync(featureLayer, "TIPO", GlobalVariables.fieldTypeString, 80);
        //            await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 20);
        //            await AddFieldIfNotExistsAsync(featureLayer, "DATUM", GlobalVariables.fieldTypeString, 10);
        //            await AddFieldIfNotExistsAsync(featureLayer, "BLOQUEO", GlobalVariables.fieldTypeString, 10);
        //            await AddFieldIfNotExistsAsync(featureLayer, "CASO", GlobalVariables.fieldTypeString, 40);
        //        }
        //        else if(caso== "Cata_sim")
        //        {
        //            await AddFieldIfNotExistsAsync(featureLayer, "LEYENDA", GlobalVariables.fieldTypeString, 2);
        //        }
        //        else if(caso == "Cuadri_sim")
        //        {
        //            await AddFieldIfNotExistsAsync(featureLayer, "CD_CUAD", GlobalVariables.fieldTypeString, 13);
        //            await AddFieldIfNotExistsAsync(featureLayer, "SUBGRUPO", GlobalVariables.fieldTypeString, 3);
        //            await AddFieldIfNotExistsAsync(featureLayer, "COD_REMATE", GlobalVariables.fieldTypeString, 20);
                    
        //        }
        //        else if (caso == "Cuadri_dsim")
        //        {
        //            await AddFieldIfNotExistsAsync(featureLayer, "CD_CUAD", GlobalVariables.fieldTypeString, 13);
        //            await AddFieldIfNotExistsAsync(featureLayer, "SUBGRUPO", GlobalVariables.fieldTypeString, 3);
        //            await AddFieldIfNotExistsAsync(featureLayer, "COD_REMATE", GlobalVariables.fieldTypeString, 20);

        //        }
        //        // Otros casos a agregar de manera similar

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}");
        //    }
        //}

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

                switch (caso)
                {
                    case "CODIGO":
                        await AddFieldIfNotExistsAsync(featureLayer, "CODIGO", GlobalVariables.fieldTypeString, 16);
                        break;

                    case "ExclusionUEA":
                    case "IntegrantesUEA":
                    case "InclusionUEA":
                        await DeleteFieldIfExists(featureLayer, "TIT_CONCES");
                        await DeleteFieldIfExists(featureLayer, "LEYENDA");

                        await AddFieldIfNotExistsAsync(featureLayer, "MOVIMIENTO", GlobalVariables.fieldTypeString, 1);
                        await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 1);
                        await AddFieldIfNotExistsAsync(featureLayer, "COD_UEA", GlobalVariables.fieldTypeString, 13);
                        await AddFieldIfNotExistsAsync(featureLayer, "NOM_UEA", GlobalVariables.fieldTypeString, 100);
                        await AddFieldIfNotExistsAsync(featureLayer, "LEYENDA", GlobalVariables.fieldTypeString, 2);
                        break;

                    case "Catastro":
                        await AddFieldIfNotExistsAsync(featureLayer, "EVAL", GlobalVariables.fieldTypeString, 10);
                        await AddFieldIfNotExistsAsync(featureLayer, "CALCULO", GlobalVariables.fieldTypeString, 10);
                        await AddFieldIfNotExistsAsync(featureLayer, "AREA_INT", GlobalVariables.fieldTypeDouble, 20, 4);
                        await AddFieldIfNotExistsAsync(featureLayer, "DPTO", GlobalVariables.fieldTypeString, 20);
                        await AddFieldIfNotExistsAsync(featureLayer, "PROV", GlobalVariables.fieldTypeString, 40);
                        await AddFieldIfNotExistsAsync(featureLayer, "DIST", GlobalVariables.fieldTypeString, 50);
                        await AddFieldIfNotExistsAsync(featureLayer, "CONTADOR", GlobalVariables.fieldTypeString, 20);
                        await AddFieldIfNotExistsAsync(featureLayer, "NUM_RESOL", GlobalVariables.fieldTypeString, 20);
                        await AddFieldIfNotExistsAsync(featureLayer, "FEC_RESOL", GlobalVariables.fieldTypeDate, 20);
                        await AddFieldIfNotExistsAsync(featureLayer, "CALIF", GlobalVariables.fieldTypeString, 10);
                        await AddFieldIfNotExistsAsync(featureLayer, "DISTS", GlobalVariables.fieldTypeString, 256);
                        await AddFieldIfNotExistsAsync(featureLayer, "PROVS", GlobalVariables.fieldTypeString, 256);
                        await AddFieldIfNotExistsAsync(featureLayer, "DPTOS", GlobalVariables.fieldTypeString, 256);
                        await AddFieldIfNotExistsAsync(featureLayer, "TIPO", GlobalVariables.fieldTypeString, 80);
                        await AddFieldIfNotExistsAsync(featureLayer, "SITUACION", GlobalVariables.fieldTypeString, 20);
                        await AddFieldIfNotExistsAsync(featureLayer, "DATUM", GlobalVariables.fieldTypeString, 10);
                        await AddFieldIfNotExistsAsync(featureLayer, "BLOQUEO", GlobalVariables.fieldTypeString, 10);
                        await AddFieldIfNotExistsAsync(featureLayer, "CASO", GlobalVariables.fieldTypeString, 40);
                        break;

                    case "Cata_sim":
                        await AddFieldIfNotExistsAsync(featureLayer, "LEYENDA", GlobalVariables.fieldTypeString, 2);
                        break;

                    case "Cuadri_sim":
                    case "Cuadri_dsim":
                        await AddFieldIfNotExistsAsync(featureLayer, "CD_CUAD", GlobalVariables.fieldTypeString, 13);
                        await AddFieldIfNotExistsAsync(featureLayer, "SUBGRUPO", GlobalVariables.fieldTypeString, 3);
                        await AddFieldIfNotExistsAsync(featureLayer, "COD_REMATE", GlobalVariables.fieldTypeString, 20);
                        break;

                    case "provincia_PMA":
                        await AddFieldIfNotExistsAsync(featureLayer, "LEYEN", GlobalVariables.fieldTypeString, 4);
                        await AddFieldIfNotExistsAsync(featureLayer, "PROVIN", GlobalVariables.fieldTypeString, 20);
                        break;

                    default:
                        MessageBox.Show($"El caso '{caso}' no está definido en la configuración.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nDetalles: {ex.InnerException?.Message}");
            }
        }

        public static void ProcessOverlapAreaDm(
                                       DataTable tableExistDm,
                                       out string listCodigoColin,
                                       out string listCodigoSup,
                                       out List<string> colectionAreaOverlap)
        {
            // Inicializar las salidas
            listCodigoColin = string.Empty;
            listCodigoSup = string.Empty;
            colectionAreaOverlap = new List<string>();

            // Usar StringBuilder para eficiencia en concatenaciones
            StringBuilder codigoColinBuilder = new StringBuilder();
            StringBuilder codigoSupBuilder = new StringBuilder();

            // Contadores para gestionar operadores OR
            int contador_areas = 0;
            int contador_areas_sup = 0;

            // Verificar si el DataTable tiene filas
            if (tableExistDm.Rows.Count > 0)
            {
                // Recorrer cada fila del DataTable
                foreach (DataRow row in tableExistDm.Rows)
                {
                    // Extraer valores de las columnas, asegurando que no sean null
                    string codigo_sup = row["CODIGO_SP"] != DBNull.Value ? row["CODIGO_SP"].ToString() : string.Empty;
                    double area_sup = 0.0;

                    if (row["AREASUP"] != DBNull.Value)
                    {
                        // Intentar convertir el valor a double
                        double.TryParse(row["AREASUP"].ToString(), out area_sup);
                    }

                    // Construir cláusulas WHERE y colecciones basadas en el valor de AREASUP
                    if (area_sup == 0.0)
                    {
                        // Añadir 'OR' si ya existen condiciones previas
                        if (contador_areas > 0)
                        {
                            codigoColinBuilder.Append(" OR ");
                        }

                        // Construir la condición para CODIGOU
                        codigoColinBuilder.Append($"CODIGOU = '{codigo_sup}'");
                        contador_areas++;
                    }
                    else if (area_sup > 0.0)
                    {
                        // Agregar a la colección AREA_SUP
                        colectionAreaOverlap.Add($"{area_sup}-{codigo_sup}");

                        // Añadir 'OR' si ya existen condiciones previas
                        if (contador_areas_sup > 0)
                        {
                            codigoSupBuilder.Append(" OR ");
                        }

                        // Construir la condición para CODIGOU
                        codigoSupBuilder.Append($"CODIGOU = '{codigo_sup}'");
                        contador_areas_sup++;
                    }
                }

                // Asignar las cadenas construidas a las salidas
                listCodigoColin = codigoColinBuilder.ToString();
                listCodigoSup = codigoSupBuilder.ToString();
            }
        }

        public async static Task UpdateRecordsDmAsync(string capa, string listCodigoColin, string listCodigoSup, List<string> colectionAreaOverlap)
        {
            try
            {
                await QueuedTask.Run(() =>
                {
                    // Obtener el mapa activo
                    Map map = MapView.Active?.Map;
                    if (map == null)
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No se encontró un mapa activo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Buscar la capa por nombre
                    FeatureLayer pFeatLayer = map.Layers.OfType<FeatureLayer>()
                        .FirstOrDefault(fl => fl.Name.Equals(capa, StringComparison.OrdinalIgnoreCase));

                    if (pFeatLayer == null)
                    {
                        MessageBox.Show($"No se encuentra la capa '{capa}' para generar evaluación DM.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    FeatureClass pFeatureClass = pFeatLayer.GetFeatureClass();

                    if (pFeatureClass == null)
                    {
                        MessageBox.Show($"La capa '{capa}' no tiene una FeatureClass válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Definir los índices de los campos
                    int indexEVAL = pFeatureClass.GetDefinition().FindField("EVAL");
                    int indexAREA_INT = pFeatureClass.GetDefinition().FindField("AREA_INT");
                    int indexCODIGOU = pFeatureClass.GetDefinition().FindField("CODIGOU");
                    int indexESTADO = pFeatureClass.GetDefinition().FindField("ESTADO");

                    if (indexEVAL == -1 || indexAREA_INT == -1 || indexCODIGOU == -1 || indexESTADO == -1)
                    {
                        MessageBox.Show("Uno o más campos necesarios no fueron encontrados en la FeatureClass.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Bucle para procesar Superpuesto, Colindantes y Vecinos
                    for (int j = 1; j <= 3; j++)
                    {
                        string seleccionTema = j switch
                        {
                            1 => "Superpuesto",
                            2 => "Colindantes",
                            3 => "Vecinos",
                            _ => string.Empty
                        };

                        if (string.IsNullOrEmpty(seleccionTema))
                            continue;

                        // Definir la cláusula WHERE basada en el tema
                        string whereClause = seleccionTema switch
                        {
                            "Superpuesto" => listCodigoSup,
                            "Colindantes" => listCodigoColin,
                            "Vecinos" => "EVAL = ''",
                            _ => string.Empty
                        };

                        if (string.IsNullOrEmpty(whereClause) && seleccionTema != "Vecinos")
                            continue;

                        using (RowCursor cursor = pFeatureClass.Search(new ArcGIS.Core.Data.QueryFilter { WhereClause = whereClause }, false))
                        {
                            while (cursor.MoveNext())
                            {
                                using (Row updateCursor = cursor.Current)
                                {
                                    if (updateCursor == null)
                                    {
                                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No se pudo obtener el Registro para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        continue;
                                    }

                                    switch (seleccionTema)
                                    {
                                        case "Superpuesto":
                                            UpdateOverlays(updateCursor, indexEVAL, indexAREA_INT, indexCODIGOU, colectionAreaOverlap);
                                            break;

                                        case "Colindantes":
                                            UpdateNeighboring(updateCursor, indexEVAL, indexAREA_INT);
                                            break;

                                        case "Vecinos":
                                            UpdateNeighbors(updateCursor, indexEVAL, indexAREA_INT, indexCODIGOU);
                                            break;
                                    }

                                    updateCursor.Store();
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void UpdateOverlays(Row row, int indexEVAL, int indexAREA_INT, int indexCODIGOU, List<string> colectionAreaOverlap)
        {
            List<string> colecciones_rd = new List<string>();
            // Asumimos que colecciones_AREA_SUP ya está poblado
            foreach (var item in colectionAreaOverlap)
            {
                var parts = item.Split('-');
                if (parts.Length != 2)
                    continue;

                if (double.TryParse(parts[0], out double areaSup))
                {
                    string codigoBus = parts[1];

                    // Verificar si el CODIGOU coincide
                    if (row[indexCODIGOU].ToString() == codigoBus)
                    {
                        string estadoRd = row["ESTADO"].ToString() ?? string.Empty;
                        if (estadoRd == "F")
                        {
                            colecciones_rd.Add(codigoBus);
                        }

                        // Actualizar campos
                        row[indexEVAL] = "IN";
                        row[indexAREA_INT] = areaSup;
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza las características que son colindantes.
        /// </summary>
        /// <param name="feature">La característica a actualizar.</param>
        /// <param name="indexEVAL">Índice del campo EVAL.</param>
        /// <param name="indexAREA_INT">Índice del campo AREA_INT.</param>
        private static void UpdateNeighboring(Row feature, int indexEVAL, int indexAREA_INT)
        {
            feature[indexEVAL] = "CO";
            feature[indexAREA_INT] = 0.0;
        }

        /// <summary>
        /// Actualiza las características que son vecinos.
        /// </summary>
        /// <param name="feature">La característica a actualizar.</param>
        /// <param name="indexEVAL">Índice del campo EVAL.</param>
        /// <param name="indexAREA_INT">Índice del campo AREA_INT.</param>
        /// <param name="indexCODIGOU">Índice del campo CODIGOU.</param>
        private static void UpdateNeighbors(Row feature, int indexEVAL, int indexAREA_INT, int indexCODIGOU)
        {
            string codigoOU = feature[indexCODIGOU].ToString() ?? string.Empty;

            if (codigoOU == GlobalVariables.CurrentCodeDm)
            {
                feature[indexEVAL] = "EV";
            }
            else
            {
                feature[indexEVAL] = "VE";
                feature[indexAREA_INT] = 0.0;
            }
        }

        public static async Task UpdateValueAsync(string capa, string codigoValue)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener la clase de entidades de la capa
                    DatabaseHandler dataBaseHandler = new DatabaseHandler();
                    // Obtener el documento del mapa y la capa
                    Map pMap = MapView.Active.Map;
                    FeatureLayer pFeatLayer1 = null;
                    foreach (var layer in pMap.Layers)
                    {
                        if (layer.Name.ToUpper() == capa.ToUpper())
                        {
                            pFeatLayer1 = layer as FeatureLayer;
                            break;
                        }
                    }

                    if (pFeatLayer1 == null)
                    {
                        System.Windows.MessageBox.Show("No se encuentra el Layer");
                        return;
                    }

                    // Obtener la clase de entidades de la capa
                    FeatureClass pFeatureClas1 = pFeatLayer1.GetTable() as FeatureClass;

                    // Preparar la fecha y hora
                    string fecha = DateTime.Now.ToString("yyyy/MM/dd");
                    string v_fec_denun = fecha + " 00:00";
                    string v_hor_denun = DateTime.Now.ToString("HH:mm:ss");

                    // Comenzar la transacción
                    using (RowCursor pUpdateFeatures = pFeatureClas1.Search(null, false))
                    {
                        int contador = 0;
                        while (pUpdateFeatures.MoveNext())
                        {
                            contador++;
                            using (Row row = pUpdateFeatures.Current)
                            {
                                string v_codigo_dm = row["CODIGOU"].ToString();

                                // Llamar al procedimiento para obtener datos de Datum y bloquear estado
                                DataTable lodtbDatos_dm = dataBaseHandler.ObtenerDatumDm(v_codigo_dm);
                                if (lodtbDatos_dm.Rows.Count > 0)
                                {
                                    row["DATUM"] = lodtbDatos_dm.Rows[0]["ESTADO"].ToString();
                                }

                                // Llamar a otros procedimientos para obtener situación y estado
                                DataTable lodtbDatos1 = dataBaseHandler.ObtenerBloqueadoDm(v_codigo_dm);
                                if (lodtbDatos1.Rows.Count > 0)
                                {
                                    if (lodtbDatos1.Rows[0]["CODIGO"].ToString() == "1")
                                    {
                                        row["BLOQUEO"] = "1";
                                        row["CASO"] = "D.M. - ANAP";
                                    }
                                }
                                else
                                {
                                    row["BLOQUEO"] = "0";
                                }

                                row["CONTADOR"] = contador;

                                DataTable lodtbDatos2 = dataBaseHandler.ObtenerDatosGeneralesDM(v_codigo_dm);
                                if (lodtbDatos2.Rows.Count > 0)
                                {
                                    row["SITUACION"] = lodtbDatos2.Rows[0]["SITUACION"].ToString();
                                }
                                else
                                {
                                    row["SITUACION"] = "X";
                                }

                                // Lógica de asignación de leyenda dependiendo del estado
                                string estado = row["ESTADO"].ToString();
                                string leyenda = string.Empty;
                                switch (estado)
                                {
                                    case " ":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "P":
                                        leyenda = "G1";  // Petitorio Tramite
                                        break;
                                    case "D":
                                        leyenda = "G2";  // Denuncio Tramite
                                        break;
                                    case "E":
                                    case "N":
                                    case "Q":
                                    case "T":
                                        leyenda = "G3";  // Denuncios Titulados
                                        break;
                                    case "F":
                                    case "J":
                                    case "L":
                                    case "H":
                                    case "Y":
                                    case "9":
                                    case "X":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "C":
                                        DataTable lodtbDatos3 = dataBaseHandler.ObtenerDatosDM(v_codigo_dm);
                                        string v_situacion_dm = "";
                                        string v_estado_dm = "";
                                        if (lodtbDatos3.Rows.Count > 0)
                                        {
                                            v_situacion_dm = lodtbDatos3.Rows[0]["SITUACION"].ToString();
                                            v_estado_dm = lodtbDatos3.Rows[0]["ESTADO"].ToString();
                                        }
                                        if (v_situacion_dm == "V" && v_estado_dm == "T")
                                        {
                                            leyenda = "G3";
                                        }
                                        else if (v_situacion_dm == "V" && v_estado_dm == "R")
                                        {
                                            leyenda = "G1";
                                        }
                                        else
                                        {
                                            leyenda = "G4";
                                        }
                                        break;
                                    case "A":
                                    case "B":
                                    case "S":
                                    case "M":
                                    case "G":
                                    case "R":
                                    case "Z":
                                    case "K":
                                    case "V":
                                        leyenda = "G5";  // Otros
                                        break;
                                    default:
                                        row["LEYENDA"] = "";
                                        row["EVAL"] = "EV";
                                        row["TIPO_EX"] = "PE";
                                        row["CONCESION"] = "Dm_Simulado";
                                        row["FEC_DENU"] = v_fec_denun;
                                        row["HOR_DENU"] = v_hor_denun;
                                        row["CARTA"] = "CARTA";

                                        break;
                                }
                                if (row["BLOQUEO"].ToString() == "1")
                                {
                                    leyenda = "G7";
                                }

                                // Actualizar los valores de departamento, provincia y distrito
                                DataTable lodtbDemarca = dataBaseHandler.ObtenerDatosUbigeo(row["DEMAGIS"].ToString().Substring(0, 6));
                                if (lodtbDemarca.Rows.Count > 0)
                                {
                                    row["DPTO"] = lodtbDemarca.Rows[0]["DPTO"].ToString();
                                    row["PROV"] = lodtbDemarca.Rows[0]["PROV"].ToString();
                                    row["DIST"] = lodtbDemarca.Rows[0]["DIST"].ToString();
                                }
                                if (codigoValue == row["CODIGOU"].ToString())
                                {
                                    leyenda = "G6";
                                }

                                row["LEYENDA"] = leyenda;

                                row.Store();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
                }
            });

        }

        public static async Task UpdateValueSimultaneoAsync(string capa, string subgrupo, string codRemate)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener la clase de entidades de la capa
                    DatabaseHandler dataBaseHandler = new DatabaseHandler();
                    // Obtener el documento del mapa y la capa
                    Map pMap = MapView.Active.Map;
                    FeatureLayer pFeatLayer1 = null;
                    foreach (var layer in pMap.Layers)
                    {
                        if (layer.Name.ToUpper() == capa.ToUpper())
                        {
                            pFeatLayer1 = layer as FeatureLayer;
                            break;
                        }
                    }

                    if (pFeatLayer1 == null)
                    {
                        System.Windows.MessageBox.Show("No se encuentra el Layer");
                        return;
                    }

                    // Obtener la clase de entidades de la capa
                    FeatureClass pFeatureClas1 = pFeatLayer1.GetTable() as FeatureClass;

                    // Comenzar la transacción
                    using (RowCursor pUpdateFeatures = pFeatureClas1.Search(null, false))
                    {
                        int contador = 0;
                        while (pUpdateFeatures.MoveNext())
                        {
                            contador++;
                            using (Row row = pUpdateFeatures.Current)
                            {

                                row["SUBGRUPO"] = subgrupo;
                                row["COD_REMATE"] = codRemate;

                                row.Store();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
                }
            });

        }
        public async static Task<string> IntersectFeatureLayerWithEnvelope(FeatureLayer featureLayer, ExtentModel extent, string fieldName)
        {
            // Cadena de texto para almacenar los resultados
            string result = string.Empty;

            await QueuedTask.Run(() =>
            {
                // Obtén la capa de características (FeatureLayer)
                var layer = featureLayer;
                // Crear el envolvente
                ArcGIS.Core.Geometry.Envelope envelope = EnvelopeBuilder.CreateEnvelope(extent.xmin, extent.ymin, extent.xmax, extent.ymax, featureLayer.GetSpatialReference());
                // Crea un filtro espacial usando la geometría Envelope
                var queryFilter = new ArcGIS.Core.Data.SpatialQueryFilter
                {
                    WhereClause = "", // Si no hay filtro de atributos, se puede dejar vacío
                    FilterGeometry = envelope, // Establece la geometría de la intersección
                    SpatialRelationship = SpatialRelationship.Intersects
                };

                // Realiza la consulta espacial en la capa
                var rowCursor = layer.Search(queryFilter);

                // Lista para almacenar los valores del campo 'ubigeo'
                var valuesField = new List<string>();

                // Itera sobre los resultados
                while (rowCursor.MoveNext())
                {
                    var row = rowCursor.Current;

                    // Obtén el valor del campo 'ubigeo'
                    if (row != null)
                    {
                        string ubigeoValue = row[fieldName] as string;
                        if (!string.IsNullOrEmpty(ubigeoValue))
                        {
                            // Añade el valor a la lista
                            valuesField.Add(ubigeoValue);
                        }
                    }
                }

                // Si hay valores en la lista, une todos los elementos con comas
                if (valuesField.Count > 0)
                {
                    result = string.Join(", ", valuesField.Distinct().ToList());
                }
            });

            // Devuelve el resultado como una cadena separada por comas
            return result;
        }
        public async static Task<string> IntersectFeatureLayerWithGeometry(FeatureLayer featureLayer, ArcGIS.Core.Geometry.Geometry geometryFilter, string fieldName)
        {
            // Cadena de texto para almacenar los resultados
            string result = string.Empty;

            await QueuedTask.Run(() =>
            {
                // Obtén la capa de características (FeatureLayer)
                var layer = featureLayer;
                // Crea un filtro espacial usando la geometría Envelope
                var queryFilter = new SpatialQueryFilter
                {
                    WhereClause = "", // Si no hay filtro de atributos, se puede dejar vacío
                    FilterGeometry = geometryFilter, // Establece la geometría de la intersección
                    SpatialRelationship = SpatialRelationship.Intersects
                };

                // Realiza la consulta espacial en la capa
                var rowCursor = layer.Search(queryFilter);

                // Lista para almacenar los valores del campo 'ubigeo'
                var valuesField = new List<string>();

                // Itera sobre los resultados
                while (rowCursor.MoveNext())
                {
                    var row = rowCursor.Current;

                    // Obtén el valor del campo 'ubigeo'
                    if (row != null)
                    {
                        string ubigeoValue = row[fieldName] as string;
                        if (!string.IsNullOrEmpty(ubigeoValue))
                        {
                            // Añade el valor a la lista
                            valuesField.Add(ubigeoValue);
                        }
                    }
                }

                // Si hay valores en la lista, une todos los elementos con comas
                if (valuesField.Count > 0)
                {
                    result = string.Join(", ", valuesField.Distinct().ToList()); // Sin elementos duplicados
                }
            });

            // Devuelve el resultado como una cadena separada por comas
            return result;
        }


        public static async Task ClipLayersAsync(string inpLayer, string clpLayer, string outLayer)
        {
            var layers = new[] { inpLayer};
            var clipLayer = MapView.Active.Map.Layers.OfType<FeatureLayer>().FirstOrDefault(l => l.Name == clpLayer);

            if (clipLayer == null || layers.Any(name => MapView.Active.Map.Layers.OfType<FeatureLayer>().FirstOrDefault(l => l.Name == name) == null))
            {
                MessageBox.Show("Faltan capas necesarias en el TOC.");
                return;
            }

            foreach (var layerName in layers)
            {
                //if (layerName == "Zona Urbana")
                //{
                    var featureLayer = MapView.Active.Map.Layers.OfType<FeatureLayer>().FirstOrDefault(l => l.Name == layerName);
                    await QueuedTask.Run(() =>
                    {
                        var result = Geoprocessing.ExecuteToolAsync("analysis.Clip", 
                            Geoprocessing.MakeValueArray(featureLayer, clipLayer, outLayer));
                        result.Wait();
                    });
                //}


                //MessageBox.Show($"{layerName} cortada.");
            }
        }

        public static async Task UpdateValueLayerAsync(string layerName, string tema, DataTable dataTable, string coduea = "")
        {
            await QueuedTask.Run(() =>
            {
                // Obtener la capa del mapa activo
                var mapView = MapView.Active;
                if (mapView == null)
                {
                    MessageBox.Show("No se ha encontrado la vista activa.");
                    return;
                }

                var featureLayer = mapView.Map.GetLayersAsFlattenedList()
                                             .OfType<FeatureLayer>()
                                             .FirstOrDefault(layer => layer.Name == layerName);

                if (featureLayer == null)
                {
                    MessageBox.Show("No se encuentra la capa en el mapa.");
                    return;
                }

                using (var table = featureLayer.GetTable())
                using (var rowCursor = table.Search(null, false))
                {
                    int i = 0, conta = 1;
                    Dictionary<string, int> provinciaLeyenda = new Dictionary<string, int>();
                    while (rowCursor.MoveNext())
                    {
                        using (var row = rowCursor.Current)
                        {
                            try
                            {
                                switch (tema)
                                {
                                    case "IntegrantesUEA":
                                    case "InclusionUEA":
                                    case "ExclusionUEA":
                                        UpdateFeatureValues(row, dataTable, i, "MOVIMIENTO", "SITUACION", "COD_UEA", "NOM_UEA");
                                        break;

                                    case "Catastro_circulo":
                                        row["CONTADOR"] = i + 1;
                                        break;

                                    case "Area_Externa":
                                        row["ID_AREA"] = "Area" + (i + 1);
                                        row["CODI_UEA"] = coduea;
                                        break;

                                    case "clase_tipoUEA":
                                        row["ORDEN"] = row["TIP_MOV"].ToString() switch
                                        {
                                            "C" => "A",
                                            "Z" => "B",
                                            "S" => "C",
                                            _ => row["ORDEN"]
                                        };
                                        break;

                                    case "orden_integranUEA":
                                        row["NRO"] = i + 1;
                                        break;

                                    case "provincia_PMA":
                                        if (dataTable.Columns.Contains("CODDEM") && dataTable.Columns.Contains("CODDEM_G"))
                                        {
                                            string v_cd_prov = row["CD_PROV"].ToString();
                                            foreach (DataRow dr in dataTable.Rows)
                                            {
                                                if (v_cd_prov == dr["CODDEM"].ToString() || v_cd_prov == dr["CODDEM_G"].ToString())
                                                {
                                                    row["PROVIN"] = $"{dr["CODDEM"]}_{dr["CODDEM_G"]}";
                                                }
                                            }
                                        }
                                        break;

                                    case "estilo":
                                        // 🔹 Extraer valores únicos del campo LEYEN y guardarlos en un DataTable
                                        DataTable dtPMA = new DataTable();
                                        dtPMA.Columns.Add("LEYEN", typeof(string));

                                        string v_id = row["LEYEN"].ToString();
                                        if (!dtPMA.AsEnumerable().Any(r => r.Field<string>("LEYEN") == v_id))
                                        {
                                            DataRow newRow = dtPMA.NewRow();
                                            newRow["LEYEN"] = v_id;
                                            dtPMA.Rows.Add(newRow);
                                        }
                                        GlobalVariables.currentTable = dtPMA;
                                        break;

                                    case "provincias_PMA":
                                        //row["LEYEN"] = $"L{conta:D3}";
                                        //conta++;
                                        string v_provin = row["PROVIN"].ToString();

                                        // Si la provincia no tiene un identificador asignado, le damos uno nuevo
                                        if (!provinciaLeyenda.ContainsKey(v_provin))
                                        {
                                            provinciaLeyenda[v_provin] = conta;
                                            conta++; // Incrementamos para la próxima provincia
                                        }

                                        // Asignamos el mismo identificador a todas las filas de la provincia
                                        row["LEYEN"] = $"L{provinciaLeyenda[v_provin]:D3}";
                                        break;

                                    case "rese_dm_int":
                                    case "urba_dm_int":
                                    case "caram_dm_int":
                                        row["CODIGOU"] = dataTable.Rows[i]["CODIGOU"].ToString();
                                        break;

                                    case "cata_uea":
                                        if (dataTable.Columns.Contains("CG_CODPET") && dataTable.Columns.Contains("CG_CODIGO"))
                                        {
                                            string codUEA = row["CODIGOU"].ToString();
                                            foreach (DataRow dr in dataTable.Rows)
                                            {
                                                if (dr["CG_CODPET"].ToString() == codUEA)
                                                {
                                                    row["CODI_UEA"] = dr["CG_CODIGO"];
                                                    break;
                                                }
                                            }
                                        }
                                        break;

                                    case "caram_acum":
                                    case "caram_renun":
                                    case "caram_sim":
                                        if (dataTable.Columns.Contains("ESTILO") && dataTable.Columns.Contains("NM_AREA"))
                                        {
                                            string vestilo = row["ESTILO"].ToString();
                                            string nm_area = row["NM_AREA"].ToString();

                                            if (vestilo == "ZONA URBANA")
                                            {
                                                dataTable.Rows.Add(nm_area);
                                            }
                                            else if (vestilo == "ZONA RESERVADA")
                                            {
                                                dataTable.Rows.Add(nm_area);
                                            }
                                        }
                                        break;

                                    default:
                                        UpdateFeatureValues(row, dataTable, i, "MOVIMIENTO", "SITUACION", "COD_UEA", "NOM_UEA");
                                        row["LEYENDA"] = row["MOVIMIENTO"].ToString() switch
                                        {
                                            "C" => "G3",
                                            "I" => "G1",
                                            "X" => "G2",
                                            _ => row["LEYENDA"]
                                        };
                                        break;
                                }

                                row.Store(); // Guardar cambios
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error en {tema}: {ex.Message}");
                            }
                            i++;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Método auxiliar para actualizar valores de atributos.
        /// </summary>
        private static void UpdateFeatureValues(Row row, DataTable dataTable, int index, params string[] fieldNames)
        {
            foreach (var field in fieldNames)
            {
                if (dataTable.Columns.Contains(field))
                    row[field] = dataTable.Rows[index][field].ToString();
            }
        }

        /// <summary>
        /// Ordena una capa usando el campo especificado y guarda el resultado en una nueva capa.
        /// </summary>
        /// <param name="layerIn">Nombre de la capa de entrada.</param>
        /// <param name="layerOut">Nombre de la capa de salida.</param>
        /// <param name="fieldName">Nombre del campo por el cual se ordenará la capa.</param>
        /// <param name="addToMap">Determina si la capa ordenada se agregará al mapa.</param>
        public static async Task LayerTableSortAsync(string layerIn, string layerOut, string fieldName, bool addToMap = false)
        {
            await QueuedTask.Run(async () =>
            {
                try
                {
                    string pathTemp = GlobalVariables.pathFileTemp;
                    // Ruta de las capas (asumiendo que glo_pathTEMP es una variable global)
                    string inputPath = $"{layerIn}.shp";
                    string outputPath = $"{layerOut}.shp";
                    string sort_fields = $"{fieldName} ASCENDING";
                    // Parámetros del geoproceso
                    var parameters = Geoprocessing.MakeValueArray(inputPath, outputPath, sort_fields);

                    // Configuración del entorno
                    var env = Geoprocessing.MakeEnvironmentArray(workspace: pathTemp);

                    // Ejecutar herramienta Sort
                    var gpResult = await Geoprocessing.ExecuteToolAsync("Sort_management", parameters, env, null, null, GPExecuteToolFlags.None);

                    // Verificar si la ejecución fue exitosa
                    if (gpResult.IsFailed)
                    {
                        MessageBox.Show("Error al ejecutar Sort_management.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Agregar la capa al mapa si se especifica
                    if (addToMap)
                    {
                        outputPath = Path.Combine(pathTemp, outputPath);
                        await LayerUtils.AddLayerAsync(await MapUtils.FindMapByNameAsync(GlobalVariables.mapNameCatastro), outputPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en LayerTableSortAsync: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }



    }
}


