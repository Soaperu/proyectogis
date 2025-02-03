using ArcGIS.Core.Data;
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
using System.Data;
using DatabaseConnector;

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
        public FeatureLayer pFeatureLayer_dm { get; private set; }
        public FeatureLayer pFeatureLayer_depaNac { get; private set; }
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
        public FeatureLayer pFeatureLayer_drena { get; private set; }
        public FeatureLayer pFeatureLayer_vias { get; private set; }
        public FeatureLayer pFeatureLayer_ccpp { get; private set; }
        public FeatureLayer pFeatureLayer_polygon { get; private set; }

        public FeatureClassLoader(Geodatabase geodatabase, Map map, string zonaDm, string regionSele)
        {
            _geodatabase = geodatabase;
            _map = map;
            v_zona_dm = zonaDm;
            cd_region_sele = regionSele;
            // Inicializar el diccionario
            featureLayerMap = new Dictionary<string, FeatureLayer>();
        }

        public async Task<FeatureLayer> LoadFeatureClassAsync(string featureClassName, bool isVisible, string queryClause = "1=1")
        {
            try
            {
                #pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
                return await QueuedTask.Run(() =>
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
                            IsVisible = isVisible,
                            DefinitionQuery = new DefinitionQuery(whereClause: queryClause, name: "Filtro dema")

                        };
                        FeatureLayer featureLayer = LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, _map);
                        //FeatureLayer featureLayer = LayerFactory.Instance.(featureClass);

                        // Asignar el FeatureLayer a la variable correspondiente si es necesario
                        AssignFeatureLayerVariable(featureLayer, featureClassInfo.VariableName);
                        return featureLayer;
                    }
                });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            }
            catch (Exception ex)
            {
                return null;
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
                case "pFeatureLayer_dm":
                    pFeatureLayer_dm = featureLayer;
                    loFeature = "Catastro";
                    break;
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
                case "pfeatureLayer_CuadriculasR":
                    pFeatureLayer_cuadriculasR = featureLayer;
                    loFeature = "Cuadricula Regional";
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
                    loFeature = "Carta IGN";
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
                    loFeature = "Caram";
                    break;
                case "pFeatureLayer_drena":
                    pFeatureLayer_drena = featureLayer;
                    loFeature = "Drenaje";
                    break;
                case "pFeatureLayer_vias":
                    pFeatureLayer_vias = featureLayer;
                    loFeature = "Vías";
                    break;
                case "pFeatureLayer_ccpp":
                    pFeatureLayer_ccpp = featureLayer;
                    loFeature = "Centro Poblado";
                    break;

                case "pFeatureLayer_polygon":
                    pFeatureLayer_polygon = featureLayer;
                    loFeature = "Poligono";
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
            // Dep. Nacional
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_DepNacional + v_zona_dm,
                LayerName = "Departamento_Nacional",
                VariableName = "pFeatureLayer_depaNac"
            },
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

            // Catastro Minero WGS84
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_CatastroWGS84 + v_zona_dm,
                LayerName = "Catastro",
                VariableName = "pFeatureLayer_cat"
            },
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_18",
            //    LayerName = "Catastro",
            //    VariableName = "pFeatureLayer_cat"
            //},
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_19",
            //    LayerName = "Catastro",
            //    VariableName = "pFeatureLayer_cat"
            //},

            // Catastro Minero PSAD56
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_CatastroPSAD56 + v_zona_dm,
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

            // Cuadriculas Regionales
            new FeatureClassInfo
            {
                FeatureClassName = FeatureClassConstants.gstrFC_CuadriculaR_WGS84,
                LayerName = "Cuadricula Regional",
                VariableName = "pFeatureLayer_cuadriculasR"
            },
            new FeatureClassInfo
            {
                FeatureClassName = FeatureClassConstants.gstrFC_CuadriculaR_PSAD56,
                LayerName = "Cuadricula Regional",
                VariableName = "pFeatureLayer_cuadriculasR"
            },

            // Cuadriculas Individuales
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Cuadricula_Z + v_zona_dm,
                LayerName = "Cuadriculas",
                VariableName = "pFeatureLayer_cuadriculas"
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
                FeatureClassName =  FeatureClassConstants.gstrFC_Rios56,
                LayerName = "Drenaje",
                VariableName = "pFeatureLayer_drena"
            },
            new FeatureClassInfo
            {
                FeatureClassName = FeatureClassConstants.gstrFC_Rios84,
                LayerName = "Drenaje",
                VariableName = "pFeatureLayer_drena"
            },

            // Carreteras
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Carretera56,
                LayerName = "Vías",
                VariableName = "pFeatureLayer_vias"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_Carretera84,
                LayerName = "Vías",
                VariableName = "pFeatureLayer_vias"
            },

            // Centros Poblados
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_CPoblado56,
                LayerName = "Centro Poblado",
                VariableName = "pFeatureLayer_ccpp"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (_) => FeatureClassConstants.gstrFC_CPoblado84,
                LayerName = "Centro Poblado",
                VariableName = "pFeatureLayer_ccpp"
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
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_Cforestal + v_zona_dm,
                LayerName = "Catastro Forestal",
                VariableName = "pFeatureLayer_fores"
            },
            new FeatureClassInfo
            {
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_forestal + v_zona_dm,
                LayerName = "Catastro Forestal",
                VariableName = "pFeatureLayer_fores"
            },

            // Zona Reservada
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_AReservada84 + v_zona_dm,
                LayerName = "Zona Reservada",
                VariableName = "pFeatureLayer_rese"
            },


            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_18",
            //    LayerName = "Zona Reservada",
            //    VariableName = "pFeatureLayer_rese"
            //},
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_19",
            //    LayerName = "Zona Reservada",
            //    VariableName = "pFeatureLayer_rese"
            //},

            // Zona Urbana
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_17",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_ZUrbanaWgs84 + v_zona_dm,
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            new FeatureClassInfo
            {
                //FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_18",
                FeatureClassNameGenerator = (v_zona_dm) => FeatureClassConstants.gstrFC_ZUrbanaPsad56 + v_zona_dm,
                LayerName = "Zona Urbana",
                VariableName = "pFeatureLayer_urba"
            },
            //new FeatureClassInfo
            //{
            //    FeatureClassName = "DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_19",
            //    LayerName = "Zona Urbana",
            //    VariableName = "pFeatureLayer_urba"
            //},
            // Hojas IGN
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_HOJ_HOJAS_18",
                LayerName = "Carta IGN",
                VariableName = "pFeatureLayer_hoja"
            },
            new FeatureClassInfo
            {
                FeatureClassName = "DATA_GIS.GPO_HOJ_HOJAS", //PSAD56
                LayerName = "Carta IGN",
                VariableName = "pFeatureLayer_hoja"
            },
            // Agregar todas las demás capas siguiendo el mismo patrón...
            new FeatureClassInfo
            {
                FeatureClassName = "Poligono",
                LayerName = "Poligono",
                VariableName = "pFeatureLayer_polygon"
            }

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

                await QueuedTask.Run(async () =>
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
                    pFLayer.Select(spatialFilter, SelectionCombinationMethod.New);
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
                if (loFeature == "Carta IGN")
                {
                    GlobalVariables.CurrentPagesDm = lostrJoinCodigos;
                }
                // Construir la cláusula WHERE final
                // ... código para construir la cláusula WHERE ...
                if (!string.IsNullOrEmpty(lostrJoinCodigos))
                {
                    lostrJoinCodigos = lostrJoinCodigos.TrimEnd(',');

                    try
                    {
                        string joinCondition = FeatureProcessorUtils.GenerateJoinCondition(loFeature, lostrJoinCodigos);
                        //Console.WriteLine(joinCondition);
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

        public async Task<string> IntersectFeatureClassbyEnvelopeAsync(string loFeature, Envelope envelope, string shapeFileOut = "")
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

                await QueuedTask.Run(async () =>
                {


                    // Crear el filtro espacial
                    SpatialQueryFilter spatialFilter = new SpatialQueryFilter
                    {
                        FilterGeometry = envelope,
                        SpatialRelationship = SpatialRelationship.Intersects
                    };
                    // Ejecuta seleccion
                    pFLayer.Select(spatialFilter, SelectionCombinationMethod.New);
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
                if (loFeature == "Carta IGN")
                {
                    GlobalVariables.CurrentPagesDm = lostrJoinCodigos;
                }
                // Construir la cláusula WHERE final
                // ... código para construir la cláusula WHERE ...
                if (!string.IsNullOrEmpty(lostrJoinCodigos))
                {
                    lostrJoinCodigos = lostrJoinCodigos.TrimEnd(',');

                    try
                    {
                        string joinCondition = FeatureProcessorUtils.GenerateJoinCondition(loFeature, lostrJoinCodigos);
                        //Console.WriteLine(joinCondition);
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

        public async Task<string> IntersectFeatureClassbyGeometryAsync(string loFeature, ArcGIS.Core.Geometry.Geometry geometry, string shapeFileOut = "")
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

                await QueuedTask.Run(async () =>
                {


                    // Crear el filtro espacial
                    SpatialQueryFilter spatialFilter = new SpatialQueryFilter
                    {
                        FilterGeometry = geometry,
                        SpatialRelationship = SpatialRelationship.Intersects
                    };
                    // Ejecuta seleccion
                    pFLayer.Select(spatialFilter, SelectionCombinationMethod.New);
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
                if (loFeature == "Carta IGN")
                {
                    GlobalVariables.CurrentPagesDm = lostrJoinCodigos;
                }
                // Construir la cláusula WHERE final
                // ... código para construir la cláusula WHERE ...
                if (!string.IsNullOrEmpty(lostrJoinCodigos))
                {
                    lostrJoinCodigos = lostrJoinCodigos.TrimEnd(',');

                    try
                    {
                        string joinCondition = FeatureProcessorUtils.GenerateJoinCondition(loFeature, lostrJoinCodigos);
                        //Console.WriteLine(joinCondition);
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


        public async Task QueryFeatureClassAsync(string loFeature, string queryClause, string shapeFileOut = "")
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
                    return;
                }


                await QueuedTask.Run(async () =>
                {
                    QueryFilter queryFilter = new QueryFilter
                    {
                        WhereClause = queryClause
                        //WhereClause = $"EVAL = '{criterio}'"
                    };
                    // Ejecuta seleccion
                    pFLayer.Select(queryFilter, SelectionCombinationMethod.New);
                    // Obtener el número de entidades seleccionadas
                    int selectionCount = pFLayer.SelectionCount;
                    if (selectionCount > 0 && !string.IsNullOrEmpty(shapeFileOut))
                    {
                        await ExportSpatialTemaAsync(loFeature, GlobalVariables.stateDmY, shapeFileOut);
                    }


                });

            }
            catch
            {
                return;
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
                string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
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
                    else if (pNombreArchivo == "Catastro")
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

        public async Task ExportAttributesTemaAsync(string layerName, bool sele_denu, string outputLayerName, string customWhereClause = "")
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

        public async Task<FeatureLayer> LoadFeatureClassAsyncGDB(string featureClassName, bool isVisible, string queryClause = "1=1")
        {
            try
            {
                return await QueuedTask.Run(() =>
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
                    Geodatabase geodatabase = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri("C:\\bdgeocatmin\\Temporal\\GeneralGDB.gdb")));

                    using (FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(actualFeatureClassName))
                    {
                        // Obtener el nombre real de la capa (Layer)
                        string actualLayerName = featureClassInfo.LayerName ?? featureClassInfo.LayerNameGenerator?.Invoke(cd_region_sele);

                        // Crear el FeatureLayer
                        FeatureLayerCreationParams flParams = new FeatureLayerCreationParams(featureClass)
                        {
                            Name = actualLayerName,//featureClassInfo.LayerName,
                            IsVisible = isVisible,
                            DefinitionQuery = new DefinitionQuery(whereClause: queryClause, name: "Filtro dema")

                        };
                        FeatureLayer featureLayer = LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, _map);
                        //FeatureLayer featureLayer = LayerFactory.Instance.(featureClass);

                        // Asignar el FeatureLayer a la variable correspondiente si es necesario
                        if (featureClassName.Contains("Poligono"))
                        {
                            AssignFeatureLayerVariable(featureLayer, "pFeatureLayer_polygon");
                        }
                        else
                        {
                            AssignFeatureLayerVariable(featureLayer, featureClassInfo.VariableName);
                        }

                        return featureLayer;
                    }
                });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            }
            catch (Exception ex)
            {
                return null;
                // Manejo de excepciones
                //MessageBox.Show($"Error al cargar la Feature Class '{featureClassName}': {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public static DataTable GetDataForElemento(
                                                string sele_elemento,
                                                string v_sistema,
                                                string v_zona,
                                                string v_codigo
                                            )
        {
            // Este DataTable será el que devuelva cls_Oracle
            DataTable result = new DataTable();

            // Ajusta según tu lógica real
            // code = primer parámetro de FT_Int_tiporesexdepa
            // table1, table2 = segundo y tercer parámetro
            string code = "";
            string table1 = string.Empty;
            string table2 = string.Empty;
            string elementoParaConsulta = sele_elemento;

            
            // Sólo un ejemplo de switch para "sele_elemento".
            // Ajusta según tus necesidades reales:
            switch (sele_elemento)
            {
                case "ZONA URBANA":
                    // De tu VB original se ve que usabas code=34 (ejemplo),
                    // y las tablas "DESA_GIS.GPO_DEP_NACIONAL_WGS_18" y
                    // "DATA_GIS.GPO_CAR_CARAM_WGS_18" si v_sistema=="2"
                    code = "34";
                    if (v_sistema == "2") //WGS_84
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;//$"DATA_GIS.GPO_DEP_NACIONAL_WGS_{v_zona}";
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;//$"DATA_GIS.GPO_CAR_CARAM_WGS_{v_zona}";
                    }
                    else
                    {
                        // Ajusta en caso no sea WGS_84
                        table1 = $"DATA_GIS.GPO_DEP_NACIONAL_{v_zona}";
                        table2 = $"DATA_GIS.GPO_CAR_CARAM_{v_zona}";
                    }
                    break;

                case "PROYECTO ESPECIAL - HIDRAULICOS":
                    code = "36";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona; ;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        // A veces en tu código original enviabas "PROYECTO ESPECIAL" 
                        // como último parámetro, en lugar de sele_elemento:
                        elementoParaConsulta = "PROYECTO ESPECIAL";
                    }
                    else
                    {
                        table1 = $"DATA_GIS.GPO_DEP_NACIONAL_{v_zona}";
                        table2 = $"DATA_GIS.GPO_CAR_CARAM_{v_zona}";
                        elementoParaConsulta = "PROYECTO ESPECIAL";
                    }
                    break;
                case "AREA NATURAL - USO DIRECTO":
                    code = "30";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                case "AREA NATURAL - USO INDIRECTO":
                    code = "31";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                case "AREA NATURAL - AMORTIGUAMIENTO":
                    code = "32";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                case "PROYECTO ESPECIAL (no hidráulicos)":
                    code = "37";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "PROYECTO ESPECIAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;

                case "CLASIFICACION DIVERSA":
                    code = "39";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "OTRA AREA RESTRINGIDA";
                    }
                    break;
                case "PROPUESTA DE AREA NATURAL":
                    code = "38";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "PROPUESTA DE AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                case "SITIO RAMSAR":
                case "AREA DE DEFENSA NACIONAL":
                case "ANAP":
                case "ANAP INGEMMET":
                case "SITIO HISTORICO DE BATALLA":
                case "PAISAJE CULTURAL":
                case "ZONA DE RIESGO NO MITIGABLE":
                case "ECOSISTEMA FRAGIL":
                case "RESERVA INDIGENA":
                case "RESERVA TERRITORIAL":
                case "CONCESION FORESTAL":
                    code = "39";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                    }

                    break;
                case "PUERTO Y/O AEROPUERTOS":
                    code = "40";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                    }

                    break;
                case "ZONA ARQUEOLOGICA":
                case "RED VIAL NACIONAL":
                case "POSIBLE ZONA URBANA":
                    // Podrías asignar un code distinto, si tu original lo pide
                    code = "14";
                    if (v_sistema == "2")
                    {
                        table1 = " "; //FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        //elementoParaConsulta = "OTRA AREA RESTRINGIDA";
                        v_codigo = " ";
                    }
                    break;
                case "AREA DE CONSERVACION PRIVADA":
                    code = "42";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                case "AREA DE CONSERVACION MUNICIPAL Y OTROS":
                    code = "43.0";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "AREA NATURAL";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                // Y así sucesivamente para cada caso...
                case "AREA DE EXPANSION URBANA":
                    code = "31";
                    if (v_sistema == "2")
                    {
                        table1 = FeatureClassConstants.gstrFC_DepNacional + v_zona;
                        table2 = FeatureClassConstants.gstrFC_Caram84 + v_zona;
                        elementoParaConsulta = "ZONA URBANA";
                    }
                    else
                    {
                        // etc...
                    }
                    break;
                default:
                    // Si no coincide con ninguno, podrías retornar un DataTable vacío
                    // o lanzar una excepción controlada:
                    // throw new Exception($"Elemento '{sele_elemento}' no manejado en switch.");
                    break;
            }

            // Llamada real a la librería (ajusta nombres de parámetros y return):
            if (code != "" && !string.IsNullOrEmpty(table1) && !string.IsNullOrEmpty(table2))
            {
                // Suponiendo que cls_Oracle.FT_Int_tiporesexdepa tiene la firma:
                //   DataTable FT_Int_tiporesexdepa(int code, string table1, string table2, string codigo, string elemento)
                DatabaseHandler dataBaseHandler = new DatabaseHandler();
                result = dataBaseHandler.GetStatisticalIntersection(code, table1, table2, v_codigo, elementoParaConsulta);
            }

            return result;
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

        // Catastro
        public const string gstrFC_CatastroPSAD56 = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_";
        public const string gstrFC_CatastroWGS84 = "DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_";

        // Cuadrículas Regionales
        public const string gstrFC_CuadriculaR_WGS84 = "DATA_GIS.GPO_CRE_CUADRICULA_REGIONAL_18";
        public const string gstrFC_CuadriculaR_PSAD56 = "DATA_GIS.GPO_CRE_CUADRICULA_REGIONAL";

        // Cuadrículas
        public const string gstrFC_Cuadricula = "DATA_GIS.GPO_CUA_CUADRICULAS";
        public const string gstrFC_Cuadricula_Z = "DATA_GIS.GPO_CUA_CUADRICULAS_";

        // Caram
        public const string gstrFC_Caram56 = "DATA_GIS.GPO_CAR_CARAM_"; // PSAD56
        public const string gstrFC_Caram84 = "DATA_GIS.GPO_CAR_CARAM_WGS_"; // WGS84

        // Ríos
        public const string gstrFC_Rios56 = "DATA_GIS.GLI_RIO_RIOS";
        public const string gstrFC_Rios84 = "DATA_GIS.GLI_RIO_RIOS_18";

        // Red Vial - carreteras
        public const string gstrFC_Carretera56 = "DATA_GIS.GLI_VIA_VIAS";
        public const string gstrFC_Carretera84 = "DATA_GIS.GLI_VIA_VIAS_18";

        // Centros Poblados
        public const string gstrFC_CPoblado56 = "DATA_GIS.GPT_CPO_CENTRO_POBLADO";
        public const string gstrFC_CPoblado84 = "DATA_GIS.GPT_CPO_CENTRO_POBLADO_18";

        // Hojas Cartográficas
        public const string gstrFC_HCarta56 = "DATA_GIS.GPO_HOJ_HOJAS"; // PSAD56 18S
        public const string gstrFC_HCarta84 = "DATA_GIS.GPO_HOJ_HOJAS_18"; // WGS84 18S

        // Limite de Hojas
        public const string gstrFC_LHojas = "DATA_GIS.GPO_HOJ_LIMITE_HOJAS";

        // Zona Traslape
        public const string gstrFC_ZTraslape = "DATA_GIS.GPO_ZTR_ZONA_TRASLAPE";

        // Capitales de Distrito
        public const string gstrFC_CDistrito56 = "DATA_GIS.GPO_CDI_CAPITAL_DISTRITO";
        public const string gstrFC_CDistrito84 = "DATA_GIS.GPO_CDI_CAPITAL_DISTRITO_18";

        // Área Reservada
        public const string gstrFC_AReservada56 = "DATA_GIS.GPO_ARE_AREA_RESERVADA_G56";
        public const string gstrFC_AReservada84 = "DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_";

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
        public const string gstrFC_Cforestal = "DATA_GIS.GPO_CFO_CATASTRO_FORESTAL_W";
        public const string gstrFC_forestal = "DATA_GIS.GPO_SERFOR_CONCESIONES_P";

        // Usuario SDE
        public const string glo_User1_SDE = "DESA_GIS";
        public const string glo_User2_SDE = "DATA_GIS";

        // Otros
        public const string gstrFC_Paises = "DATA_GIS.GPO_PAI_PAISES";
        public const string gstrFC_LimiteZonas = "DATA_GIS.GPO_LZO_LIMITE_ZONAS";
        public const string gstrFC_IgnRaster84 = "DATA_GIS.DS_IGN_CARTA_NACIONAL_2016_W84";
        public const string gstrRT_IngMosaic56 = "DATA_GIS.MD_IGN_CARTA_56";
        public const string gstrRT_IngMosaic84 = "DATA_GIS.MD_IGN_CARTA_84";
        public const string gstrFC_DepNacional = "DATA_GIS.GPO_DEP_NACIONAL_WGS_";

        // Agregar más constantes según sea necesario
    }

}
