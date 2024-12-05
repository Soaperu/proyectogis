﻿using ArcGIS.Core.Data;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Core.Geoprocessing;
using System.Windows;
using ArcGIS.Desktop.Internal.Catalog.Wizards.CreateFeatureClass;
using ArcGIS.Desktop.Core;

namespace CommonUtilities.ArcgisProUtils
{
    public class FeatureClassLoader
    {
        private Geodatabase _geodatabase;
        private Map _map;
        // Variables de contexto
        public string v_zona_dm;
        private string cd_region_sele;
        // Diccionario para mapear loFeature con FeatureLayer
        private Dictionary<string, FeatureLayer> featureLayerMap;

        // Variables para almacenar los FeatureLayers específicos
        public FeatureLayer pFeatureLayer_depa { get; private set; }
        public FeatureLayer pFeatureLayer_prov { get; private set; }
        public FeatureLayer pFeatureLayer_dist { get; private set; }
        public FeatureLayer pFeatureLayer_cat { get; private set; }
        public FeatureLayer pFeatureLayer_usomin { get; private set; }
        public FeatureLayer pFeatureLayer_Actmin { get; private set; }
        public FeatureLayer pFeatureLayer_cuadriculas { get; private set; }
        public FeatureLayer pFeatureLayer_boletin { get; private set; }
        public FeatureLayer pFeatureLayer_fron { get; private set; }
        public FeatureLayer pFeatureLayer_fores { get; private set; }
        public FeatureLayer pFeatureLayer_rese { get; private set; }
        public FeatureLayer pFeatureLayer_reseg { get; private set; }
        public FeatureLayer pFeatureLayer_urba { get; private set; }
        public FeatureLayer pFeatureLayer_tras { get; private set; }
        public FeatureLayer pFeatureLayer_capdist { get; private set; }
        public FeatureLayer pFeatureLayer_hoja { get; private set; }
        public FeatureLayer pFeatureLayer_Geo_bol100 { get; private set; }
        public FeatureLayer pFeatureLayer_Geo_fran100 { get; private set; }
        public FeatureLayer pFeatureLayer_Geo_fran50 { get; private set; }
        public FeatureLayer pFeatureLayer_certi { get; private set; }
        public FeatureLayer pFeatureLayer_caram { get; private set; }
        public FeatureLayer pFeatureLayer_cuadriculasR { get; private set; }
        public FeatureLayer pFeatureLayer_temp { get; private set; }

        public FeatureClassLoader(Geodatabase geodatabase, Map map, string zonaDm, string regionSele)
        {
            _geodatabase = geodatabase;
            _map = map;
            v_zona_dm = zonaDm;
            cd_region_sele = regionSele;
            // Inicializar el diccionario
            featureLayerMap = new Dictionary<string, FeatureLayer>();
        }

        public async Task LoadFeatureClassAsync(string featureClassName, bool isVisible)
        {
            try
            {
                await QueuedTask.Run(() =>
                {
                    // Buscar la información de la Feature Class en la lista
                    var featureClassInfo = FeatureClassMappings?.FirstOrDefault(f => string.Equals(f.FeatureClassName, featureClassName, StringComparison.OrdinalIgnoreCase)
                                                                                    || (f.FeatureClassNameGenerator != null && string.Equals(f.FeatureClassNameGenerator(v_zona_dm), featureClassName, StringComparison.OrdinalIgnoreCase)));

                    if (featureClassInfo == null)
                    {
                        // Si no se encuentra, usar un nombre genérico
                        featureClassInfo = new FeatureClassInfo
                        {
                            FeatureClassName = featureClassName,
                            LayerName = featureClassName,
                            VariableName = null
                        };
                    }
                    // Obtener el nombre real de la Feature Class
                    string actualFeatureClassName = featureClassInfo.FeatureClassName ?? featureClassInfo.FeatureClassNameGenerator?.Invoke(v_zona_dm);

                    // Abrir la Feature Class
                    using (FeatureClass featureClass = _geodatabase.OpenDataset<FeatureClass>(actualFeatureClassName))
                    {
                        // Obtener el nombre real de la capa (Layer)
                        string actualLayerName = featureClassInfo.LayerName ?? featureClassInfo.LayerNameGenerator?.Invoke(cd_region_sele);

                        // Crear el FeatureLayer
                        FeatureLayerCreationParams flParams = new FeatureLayerCreationParams(featureClass)
                        {
                            Name = actualLayerName,//featureClassInfo.LayerName,
                            IsVisible = isVisible
                        };
                        FeatureLayer featureLayer = LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, _map);
                        //FeatureLayer featureLayer = LayerFactory.Instance.(featureClass);
                        
                        // Asignar el FeatureLayer a la variable correspondiente si es necesario
                        AssignFeatureLayerVariable(featureLayer, featureClassInfo.VariableName);
                    }
                });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                //MessageBox.Show($"Error al cargar la Feature Class '{featureClassName}': {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void AssignFeatureLayerVariable(FeatureLayer featureLayer, string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
                return;
            string loFeature = null;
            switch (variableName)
            {
                case "pFeatureLayer_depa":
                    pFeatureLayer_depa = featureLayer;
                    loFeature = "Departamento";
                    break;
                case "pFeatureLayer_prov":
                    pFeatureLayer_prov = featureLayer;
                    loFeature = "Provincia";
                    break;
                case "pFeatureLayer_dist":
                    pFeatureLayer_dist = featureLayer;
                    loFeature = "Distrito";
                    break;
                case "pFeatureLayer_cat":
                    pFeatureLayer_cat = featureLayer;
                    loFeature = "Catastro";
                    break;
                case "pFeatureLayer_usomin":
                    pFeatureLayer_usomin = featureLayer;
                    loFeature = "DM_Uso_Minero";
                    break;
                case "pFeatureLayer_Actmin":
                    pFeatureLayer_Actmin = featureLayer;
                    loFeature = "DM_Actividad_Minera";
                    break;
                case "pfeatureLayer_Cuadriculas":
                    pFeatureLayer_cuadriculas = featureLayer;
                    loFeature = "Cuadriculas";
                    break;
                case "pFeatureLayer_boletin":
                    pFeatureLayer_boletin = featureLayer;
                    loFeature = "Boletin";
                    break;
                case "pFeatureLayer_fron":
                    pFeatureLayer_fron = featureLayer;
                    loFeature = "Limite Frontera";
                    break;
                case "pFeatureLayer_fores":
                    pFeatureLayer_fores = featureLayer;
                    loFeature = "Catastro Forestal";
                    break;
                case "pFeatureLayer_rese":
                    pFeatureLayer_rese = featureLayer;
                    loFeature = "Zona Reservada";
                    break;
                case "pFeatureLayer_reseg":
                    pFeatureLayer_reseg = featureLayer;
                    break;
                case "pFeatureLayer_urba":
                    pFeatureLayer_urba = featureLayer;
                    loFeature = "Zona Urbana";
                    break;
                case "pFeatureLayer_hoja":
                    pFeatureLayer_hoja = featureLayer;
                    break;
                case "pFeatureLayer_tras":
                    pFeatureLayer_tras = featureLayer;
                    break;
                case "pFeatureLayer_capdist":
                    pFeatureLayer_capdist = featureLayer;
                    break;
                case "pFeatureLayer_Geo_bol100":
                    pFeatureLayer_Geo_bol100 = featureLayer;
                    break;
                case "pFeatureLayer_Geo_fran100":
                    pFeatureLayer_Geo_fran100 = featureLayer;
                    break;
                case "pFeatureLayer_Geo_fran50":
                    pFeatureLayer_Geo_fran50 = featureLayer;
                    break;
                case "pFeatureLayer_certi":
                    pFeatureLayer_certi = featureLayer;
                    break;
                case "pFeatureLayer_caram":
                    pFeatureLayer_caram = featureLayer;
                    break;
                default:
                    break;
            }
            if (loFeature != null)
            {
                // Agregar al diccionario
                featureLayerMap[loFeature] = featureLayer;
            }
        }


        public static List<FeatureClassInfo> FeatureClassMappings = new List<FeatureClassInfo>
        {
            // Departamento
            new FeatureClassInfo
            {
                FeatureClassName= FeatureClassConstants.gstrFC_Departamento,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Departamento_Z + v_zona_dm,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Departamento_WGS + v_zona_dm,
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DESA_GIS.GPO_DEP_GENERAL_WGS18",
                LayerName = "Departamento",
                VariableName = "pFeatureLayer_depa"
            },

            // Provincia
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Provincia,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Provincia_Z + v_zona_dm,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Provincia_WGS + v_zona_dm,
                LayerNameGenerator = (cd_region_sele) => cd_region_sele == "00" ? "Prov_Colindantes" : "Provincia",
                VariableName = "pFeatureLayer_prov"
            },

            // Distrito
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito_Z + v_zona_dm,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Distrito_WGS + v_zona_dm,
                LayerName = "Distrito",
                VariableName = "pFeatureLayer_dist"
            },

            // Catastro Minero PSAD56
            new FeatureClassInfo
            {
                FeatureClassName = "GPO_CMI_CATASTRO_MINERO_17",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "GPO_CMI_CATASTRO_MINERO_18",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "GPO_CMI_CATASTRO_MINERO_19",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // Catastro Minero WGS84
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_17",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_18",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_19",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            // Caram
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Caram56 + v_zona_dm,
                LayerName = "Caram",
                VariableName = "pFeatureLayer_caram"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Caram84 + v_zona_dm,
                LayerName = "Caram",
                VariableName = "pFeatureLayer_caram"
            },

            // CEGO0891 Catastro Minero H
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H17",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H18",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "CEGO0891.GPO_CATASTRO_MINERO_H19",
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // DESA_GIS Catastro Minero
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => "DESA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_" + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => "DESA_GIS.GPO_CMI_CATASTRO_MINERO_" + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },

            // Uso Minero
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_AUM_AREA_USO_MINERO_G56",
                LayerName = "DM_Uso_Minero",
                VariableName = "pFeatureLayer_usomin"
            },

            // Actividad Minera
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_AAC_AREA_ACT_MINERA_G56",
                LayerName = "DM_Actividad_Minera",
                VariableName = "pFeatureLayer_Actmin"
            },

            // Cuadriculas
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Cuadricula,
                LayerName = "Cuadricula Regional",
                VariableName = "pFeatureLayer_cuadriculasR"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_17",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_18",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_CUA_CUADRICULAS_WGS_19",
                LayerName = "Cuadriculas",
                VariableName = "pfeaturelayer_cuadriculas"
            },

            // Ríos
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Rios,
                LayerName = "Drenaje",
                VariableName = null
            },

            // Carreteras
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Carretera,
                LayerName = "Vías",
                VariableName = null
            },

            // Centros Poblados
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_CPoblado,
                LayerName = "Centro Poblado",
                VariableName = null
            },

            // Frontera
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Frontera_WGS,
                LayerName = "Limite Frontera",
                VariableName = "pFeatureLayer_fron"
            },
            new FeatureClassInfo
            {
                FeatureClassName = FeatureClassConstants.gstrFC_Frontera, // PSAD56
                LayerName = "Limite Frontera",
                VariableName = "pFeatureLayer_fron"
            },
            // Concesiones Forestales
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_forestal + v_zona_dm,
                LayerName = "Catastro Forestal",
                VariableName = "pFeatureLayer_fores"
            },

            // Zona Reservada
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_17",
                LayerName = "Zona Reservada",
                VariableName = "pFeatureLayer_rese"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_18",
                LayerName = "Zona Reservada",
                VariableName = "pFeatureLayer_rese"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_19",
                LayerName = "Zona Reservada",
                VariableName = "pFeatureLayer_rese"
            },

            // Zona Urbana
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_17",
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_18",
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_19",
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            // Hojas IGN
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_HOJ_HOJAS_18",
                LayerName = "Carta IGN",
                VariableName = "pFeatureLayer_hoja"
            }
            // Agregar todas las demás capas siguiendo el mismo patrón...
        };

        public async Task<string> IntersectFeatureClassAsync(string loFeature, double xMin, double yMin, double xMax, double yMax, string shapeFileOut = "")
        {
            try
            {
                //// Obtener el FeatureLayer desde el diccionario
                if (!featureLayerMap.TryGetValue(loFeature, out FeatureLayer pFLayer) || pFLayer == null)
                {
                    // Manejo de error si la capa no existe
                    //return "";
                    pFLayer = CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.GetFeatureLayerFromMap(MapView.Active as MapView, loFeature);
                }
                if (pFLayer == null)
                {
                    return "";
                }
                // Ajustar la cláusula WHERE si es necesario
                // ... código adicional ...

                // Ejecutar la selección y obtener los resultados
                string lostrJoinCodigos = "";

                await QueuedTask.Run(async() =>
                {
                    // Crear el envolvente
                    Envelope envelope = EnvelopeBuilder.CreateEnvelope(xMin, yMin, xMax, yMax, pFLayer.GetSpatialReference());

                    // Crear el filtro espacial
                    SpatialQueryFilter spatialFilter = new SpatialQueryFilter
                    {
                        FilterGeometry = envelope,
                        SpatialRelationship = SpatialRelationship.Intersects
                    };
                    // Ejecuta seleccion
                    pFLayer.Select(spatialFilter,SelectionCombinationMethod.New);
                    // Obtener el número de entidades seleccionadas
                    int selectionCount = pFLayer.SelectionCount;
                    if (selectionCount > 0 && !string.IsNullOrEmpty(shapeFileOut)) 
                    {
                        await ExportSpatialTemaAsync(loFeature, GlobalVariables.stateDmY, shapeFileOut);
                    }

                    using (RowCursor rowCursor = pFLayer.Search(spatialFilter))
                    {
                        while (rowCursor.MoveNext())
                        {
                            using (Row row = rowCursor.Current)
                            {
                               
                                // Variables para las salidas del método
                                string lostr_Join_Codigos_marcona;
                                string valida_urb_shp;
                                string lostr_Join_Codigos_AREA;

                                // Llamar al método para procesar la fila
                                lostrJoinCodigos += FeatureProcessorUtils.ProcessFeatureFields(
                                                                                                    loFeature,
                                                                                                    row,
                                                                                                    "",
                                                                                                    out lostr_Join_Codigos_marcona,
                                                                                                    out valida_urb_shp,
                                                                                                    out lostr_Join_Codigos_AREA
                                );
                            }
                        }
                    }
                });

                // Construir la cláusula WHERE final
                // ... código para construir la cláusula WHERE ...
                // Construir la cláusula WHERE final
                if (!string.IsNullOrEmpty(lostrJoinCodigos))
                {
                    lostrJoinCodigos = lostrJoinCodigos.TrimEnd(',');

                
                    try
                    {
                        string joinCondition = FeatureProcessorUtils.GenerateJoinCondition(loFeature, lostrJoinCodigos);
                        Console.WriteLine(joinCondition);
                        lostrJoinCodigos = joinCondition;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                return lostrJoinCodigos;
            

            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        ///  Exporta un tema a la carpeta C:\bdgeocatmin\Temporal
        /// </summary>
        /// <param name="pNombreArchivo"></param>
        /// <param name="sele_denu"></param>
        /// <param name="outputFileName"></param>
        /// <returns></returns>
        public async Task ExportSpatialTemaAsync(string pNombreArchivo, bool sele_denu, string outputFileName)
        {
            try
            {
                // Obtener el FeatureLayer correspondiente
                if (!featureLayerMap.TryGetValue(pNombreArchivo, out FeatureLayer tema) || tema == null)
                {
                    // Manejo de error si no se encuentra el FeatureLayer
                    return;
                }

                // Obtener el FeatureClass del FeatureLayer
                FeatureClass fclas_tema = await QueuedTask.Run(() =>
                {
                    return tema.GetFeatureClass();
                });

                // Verificar si hay entidades seleccionadas
                int selectedCount = await QueuedTask.Run(() =>
                {
                    return (int)tema.GetSelection().GetCount();
                });

                if (selectedCount == 0)
                {
                    // No hay entidades seleccionadas; manejar según corresponda
                    return;
                }

                // Definir la ruta de salida y el nombre del archivo
                string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut,GlobalVariables.fileTemp);
                //string outputFileName = pNombreArchivo;
                //string outputPath = Path.Combine(outputFolder, outputFileName + ".shp");

                // Crear el QueryFilter si es necesario
                QueryFilter queryFilter = new QueryFilter();

                if (sele_denu)
                {
                    if (pNombreArchivo == "Area_Natural")
                    {
                        queryFilter.WhereClause = "TP_AREA = 'AREA NATURAL'";
                    }
                    
                }
                else
                {
                    if (pNombreArchivo == "Area_Natural")
                    {
                        queryFilter.WhereClause = "TP_AREA = 'AREA NATURAL'";
                    }
                    else
                    {
                        queryFilter.WhereClause = "ESTADO <> 'Y'";
                    }
                }

                // Ejecutar la exportación dentro de un QueuedTask
                //await QueuedTask.Run(() =>
                //{
                // Exportar el FeatureClass a shapefile
                tema.Select(queryFilter, SelectionCombinationMethod.And);
                var valueArray = Geoprocessing.MakeValueArray(tema.Name, outputFolder, outputFileName);                
                //IGPResult gpResult = await Geoprocessing.ExecuteToolAsync("FeatureClassToShapefile_conversion", valueArray, null, null, null, GPExecuteToolFlags.Default);
                IGPResult gpResult = await Geoprocessing.ExecuteToolAsync("FeatureClassToFeatureClass_conversion", valueArray, null, null, null, GPExecuteToolFlags.Default);
                //});
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Ha ocurrido un error al exportar la data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task ExportAttributesTemaAsync(string layerName, bool sele_denu, string outputLayerName, string customWhereClause="")
        {
            try
            {
                // Obtener el FeatureLayer correspondiente
                if (!featureLayerMap.TryGetValue(layerName, out FeatureLayer tema) || tema == null)
                {
                    // Manejo de error si la capa no existe
                    //return "";
                    tema = CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.GetFeatureLayerFromMap(MapView.Active as MapView, layerName);
                }
                if (tema == null)
                {
                    return;
                }

                // Obtener el FeatureClass del FeatureLayer
                FeatureClass fclas_tema = await QueuedTask.Run(() =>
                {
                    return tema.GetFeatureClass();
                });
                // Definir la ruta de salida y el nombre del archivo
                string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
                string outputPath = Path.Combine(outputFolder, outputLayerName + ".shp");
                // Ejecutar la exportación dentro de un QueuedTask
                //await QueuedTask.Run(() =>
                //{
                // Exportar el FeatureClass a shapefile
                //tema.Select(queryFilter, SelectionCombinationMethod.New);
                var valueArray = Geoprocessing.MakeValueArray(tema.Name, outputFolder, outputLayerName, customWhereClause);
                //IGPResult gpResult = await Geoprocessing.ExecuteToolAsync("FeatureClassToShapefile_conversion", valueArray, null, null, null, GPExecuteToolFlags.Default);
                IGPResult gpResult = await Geoprocessing.ExecuteToolAsync("FeatureClassToFeatureClass_conversion", valueArray, null, null, null, GPExecuteToolFlags.Default);
                //});
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Ha ocurrido un error al exportar la data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
    public class FeatureClassInfo
    {
        public string? FeatureClassName { get; set; }
        public string? LayerName { get; set; }
        public string? VariableName { get; set; } // Nombre de la variable donde se almacenará el FeatureLayer
        public Func<string, string>? FeatureClassNameGenerator { get; set; } // Función para generar el nombre dinámicamente
        public Func<string, string>? LayerNameGenerator { get; set; }
    }

    public static class FeatureClassConstants
    {
        // Departamentos
        public const string gstrFC_Departamento = "DATA_GIS.GPO_DEP_DEPARTAMENTO";
        public const string gstrFC_Departamento_Z = "DATA_GIS.GPO_DEP_DEPARTAMENTO_";
        public const string gstrFC_Departamento_WGS = "DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_";

        // Provincias
        public const string gstrFC_Provincia = "DATA_GIS.GPO_PRO_PROVINCIA";
        public const string gstrFC_Provincia_Z = "DATA_GIS.GPO_PRO_PROVINCIA_";
        public const string gstrFC_Provincia_WGS = "DATA_GIS.GPO_PRO_PROVINCIA_WGS_";

        // Distritos
        public const string gstrFC_Distrito = "DATA_GIS.GPO_DIS_DISTRITO";
        public const string gstrFC_Distrito_Z = "DATA_GIS.GPO_DIS_DISTRITO_";
        public const string gstrFC_Distrito_WGS = "DATA_GIS.GPO_DIS_DISTRITO_WGS_";

        // Fronteras
        public const string gstrFC_Frontera = "DATA_GIS.GLI_FRO_FRONTERA";
        public const string gstrFC_Frontera_10 = "DATA_GIS.GLI_FRO_FRONTERA_10K";
        public const string gstrFC_Frontera_25 = "DATA_GIS.GLI_FRO_FRONTERA_25K";
        public const string gstrFC_Frontera_Z = "DATA_GIS.GLI_FRO_FRONTERA_";
        public const string gstrFC_Frontera_WGS = "DATA_GIS.GLI_FRO_FRONTERA_WGS_";

        // Cuadrículas
        public const string gstrFC_Cuadricula = "DATA_GIS.GPO_CUA_CUADRICULAS";
        public const string gstrFC_Cuadricula_Z = "DATA_GIS.GPO_CUA_CUADRICULAS_";

        // Caram
        public const string gstrFC_Caram56 = "DATA_GIS.GPO_CAR_CARAM_"; // PSAD56
        public const string gstrFC_Caram84 = "DATA_GIS.GPO_CAR_CARAM_WGS_"; // WGS84

        // Ríos
        public const string gstrFC_Rios = "DATA_GIS.GPO_RIO_RIOS";

        // Carreteras
        public const string gstrFC_Carretera = "DATA_GIS.GPO_CAR_CARRETERAS";

        // Centros Poblados
        public const string gstrFC_CPoblado = "DATA_GIS.GPO_CPO_CENTRO_POBLADO";

        // Hojas Cartográficas
        public const string gstrFC_HCarta56 = "DATA_GIS.GPO_HOJ_HOJAS"; // PSAD56 18S
        public const string gstrFC_HCarta84 = "DATA_GIS.GPO_HOJ_HOJAS_18"; // WGS84 18S

        // Limite de Hojas
        public const string gstrFC_LHojas = "DATA_GIS.GPO_HOJ_LIMITE_HOJAS";

        // Zona Traslape
        public const string gstrFC_ZTraslape = "DATA_GIS.GPO_ZTR_ZONA_TRASLAPE";

        // Capitales de Distrito
        public const string gstrFC_CDistrito = "DATA_GIS.GPO_CDI_CAPITAL_DISTRITO";

        // Área Reservada
        public const string gstrFC_AReservada56 = "DATA_GIS.GPO_ARE_AREA_RESERVADA_G56";

        // Zona Urbana
        public const string gstrFC_ZUrbanaG56 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_G56"; //GEOPSAD56
        public const string gstrFC_ZUrbanaPsad56 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_"; // PSAD56
        public const string gstrFC_ZUrbanaWgs84 = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_"; // WGS84

        // Predio Rural
        public const string gstrFC_prediorural = "DATA_GIS.GPO_PREDIO_RURAL";

        // Geo Boletín
        public const string gstrFC_GEO_BOLETIN100 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_BOLETIN";
        public const string gstrFC_GEO_BOLETIN100_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_BOLETIN_G56";

        // Geo Franja
        public const string gstrFC_GEO_FRANJA100 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_FRANJA";
        public const string gstrFC_GEO_FRANJA100_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_100K_FRANJA_G56";
        public const string gstrFC_GEO_FRANJA50 = "DATA_GIS.GPO_GEO_GEOLOGIA_50K_FRANJA";
        public const string gstrFC_GEO_FRANJA50_G56 = "DATA_GIS.GPO_GEO_GEOLOGIA_50K_FRANJA_G56";

        // Boletín
        public const string gstrFC_BOLETIN = "DATA_GIS.GPO_BOLETIN";
        public const string gstrFC_BOLETIN_G56 = "DATA_GIS.GPO_BOLETIN_G56";

        // Forestal
        public const string gstrFC_Cforestal = "DATA_GIS.GPO_CFO_CONCESION_FORESTAL_";
        public const string gstrFC_forestal = "DATA_GIS.GPO_SERFOR_CONCESIONES_W";

        // Usuario SDE
        public const string glo_User1_SDE = "DESA_GIS";
        public const string glo_User2_SDE = "DATA_GIS";

        // Otros
        public const string gstrFC_Paises = "DATA_GIS.GPO_PAI_PAISES";
        public const string gstrFC_LimiteZonas = "DATA_GIS.GPO_LZO_LIMITE_ZONAS";
        public const string gstrFC_IgnRaster84 = "DATA_GIS.DS_IGN_CARTA_NACIONAL_2016_W84";

        // Agregar más constantes según sea necesario
    }

}
