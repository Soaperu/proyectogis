using ArcGIS.Core.Data;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Mapping;
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommonUtilities.ArcgisProUtils
{
    public class ComplementaryProcessesUtils
    {
        public static async void GenerateAvailableAreaDm(string layerName, string folderName)
        {
            try
            {
                string currentCodeDm = GlobalVariables.CurrentCodeDm;
                string currentDatum = GlobalVariables.CurrentDatumDm;
                string datumWgs84 = GlobalVariables.valueDatumWGS;
                string datumPsad56 = GlobalVariables.valueDatumPSAD;
                string datumToMap;
                string zoneDm = GlobalVariables.CurrentZoneDm;
                string newDatumDm;

                // Obtener el mapa Catastro//
                if (currentDatum == datumWgs84)
                {
                    datumToMap = GlobalVariables.datumPSAD;
                    newDatumDm = datumPsad56;
                }
                else
                {
                    datumToMap = GlobalVariables.datumWGS;
                    newDatumDm = datumWgs84;
                }
                await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro + " - " + datumToMap);
                var sdeHelper = new SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);

                Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro + " - " + datumToMap); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (currentDatum == datumWgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                var extentDm = MapUtils.ObtenerExtent(currentCodeDm, int.Parse(newDatumDm));
                var extentDmRadio = MapUtils.ObtenerExtent(currentCodeDm, int.Parse(newDatumDm), GlobalVariables.CurrentRadioDm);
                string fechaArchi = currentCodeDm + "_" + datumToMap;
                string catastroShpName = layerName + fechaArchi;
                string catastroShpNamePath = layerName + fechaArchi + ".shp";
                string dmShpName = "DM" + fechaArchi;
                string dmShpNamePath = "DM" + fechaArchi + ".shp";
                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{currentCodeDm}'");
                await FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                await FeatureProcessorUtils.UpdateValueAsync(catastroShpName, currentCodeDm);
                string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                await SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, currentCodeDm);
                //var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, currentCodeDm);
                var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, currentCodeDm, currentDatum, zoneDm);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
                List<string> layersToRemove = new List<string>() { layerName, dmShpName };
                await LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                await LayerUtils.ChangeLayerNameAsync(catastroShpName, layerName);
                var Params2 = Geoprocessing.MakeValueArray(layerName, folderName, fechaArchi, 1);
                var response2 = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetAreasOverlay, Params2);
                var response2Json = JsonConvert.DeserializeObject<Dictionary<string, string>>(response2.ReturnValue);
                string layerNameDisponible = response2Json["nombreDisponible"];
                string layerNameSuperpuesta = response2Json["nombreSuperpuesta"];
                await LayerUtils.ChangeLayerNameAsync(layerNameDisponible, "Areadispo");
                await LayerUtils.ChangeLayerNameAsync(layerNameSuperpuesta, "Areainter");
                var pFeatureLayer_dispo = await LayerUtils.GetFeatureLayerByNameAsync("Areadispo");
                await SymbologyUtils.ColorPolygonSimple(pFeatureLayer_dispo);
                var pFeatureLayer_inter = await LayerUtils.GetFeatureLayerByNameAsync("Areainter");
                await SymbologyUtils.ColorPolygonSimple(pFeatureLayer_inter);
                string layerNameDispoDm = "Areadispo_" + fechaArchi;
                string layerNameInterDm = "Areainter_" + fechaArchi;
                await LayerUtils.ChangeLayerNameAsync("Areainter", layerNameInterDm);
                await LayerUtils.ChangeLayerNameAsync("Areadispo", layerNameDispoDm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable SearchCodeValue(string valueCodeDm)
        {
            DatabaseHandler dataBaseHandler = new DatabaseHandler();

            var countRecords = dataBaseHandler.CountRecords("1", valueCodeDm);
            int records = int.Parse(countRecords);
            if (records == 0)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No se encontraron resultados",
                                                                 "Advertencia",
                                                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            var dmrRecords = dataBaseHandler.GetUniqueDM(valueCodeDm, 1);
            return dmrRecords;
        }
        public async static Task<ResultadoEvaluacionModel> EvaluationDmByCode(string valueCodeDm, System.Data.DataRow dmRow, int radio = 0, int datum=2, List<string>? LayersListBox = null)
        {
            if(LayersListBox is null)
            {
                LayersListBox = new List<string>() { "Caram", "Catastro Forestal" };
            }
            string stateGraphic = dmRow["PE_VIGCAT"].ToString();
            if (stateGraphic != "G") { return null; }
            string zoneDm = dmRow["ZONA"].ToString();
            GlobalVariables.CurrentZoneDm = zoneDm;
            string areaValue = dmRow["HECTAREA"].ToString();
            GlobalVariables.CurrentAreaDm = areaValue;
            string nameDm = dmRow["NOMBRE"].ToString();
            GlobalVariables.CurrentNameDm = nameDm;
            var sdeHelper = new SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro,
                GlobalVariables.mapNameCartaIgn,
                GlobalVariables.mapNameDemarcacionPo
            };
            GlobalVariables.CurrentCodeDm = valueCodeDm;
            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            //int datum=2;
            int datumwgs84 = 2;
            string datumStr = GlobalVariables.datumWGS;
            DatabaseHandler dataBaseHandler = new DatabaseHandler();
            var v_zona_dm = dataBaseHandler.VerifyDatumDM(valueCodeDm);
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;
            GlobalVariables.dmShpNamePath = "DM" + fechaArchi + ".shp";
            await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);
            try
            {
                // Obtener el mapa Catastro//
                Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaPsad56 + zoneDm, false);
                }

                var extentDmRadio = MapUtils.ObtenerExtent(valueCodeDm, datum, radio);
                var extentDm = MapUtils.ObtenerExtent(valueCodeDm, datum);
                GlobalVariables.currentExtentDM = extentDm;
                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                // Encontrando Distritos superpuestos a DM con
                DataTable intersectDist;
                if (datum == datumwgs84)
                {
                    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, valueCodeDm);
                }
                else
                {
                    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, valueCodeDm);
                }
                DataProcessorUtils.ProcessorDataAreaAdminstrative(intersectDist);
                DataTable orderUbigeosDM;
                orderUbigeosDM = dataBaseHandler.GetUbigeoData(valueCodeDm);

                //Carga capa Hojas IGN
                string listHojas;
                ExtentModel newExtent;
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }
                if (zoneDm == "18")
                {
                    listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", extentDm.xmin, extentDm.ymin, extentDm.xmax, extentDm.ymax);
                }
                else
                {
                    newExtent = TransformBoundingBox(extentDm, zoneDm);
                    listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", newExtent.xmin, newExtent.ymin, newExtent.xmax, newExtent.ymax);
                }
                
                //GlobalVariables.CurrentPagesDm = listHojas;
                // Encontrando Caram superpuestos a DM con
                DataTable intersectCaram;
                if (datum == datumwgs84)
                {
                    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Caram84 + zoneDm, valueCodeDm);
                }
                else
                {
                    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_Caram56 + zoneDm, valueCodeDm);
                }
                DataProcessorUtils.ProcessorDataCaramIntersect(intersectCaram);

                DataTable intersectCForestal;
                if (datum == datumwgs84)
                {
                    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Cforestal + zoneDm, valueCodeDm);
                }
                else
                {
                    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_forestal + zoneDm, valueCodeDm);
                }
                DataProcessorUtils.ProcessorDataCforestalIntersect(intersectCForestal);

                DataTable intersectDm;
                if (datum == datumwgs84)
                {
                    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, valueCodeDm);
                }
                else
                {
                    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, valueCodeDm);
                }
                //DataTable distBorder;
                var distBorder = dataBaseHandler.CalculateDistanceToBorder(valueCodeDm, zoneDm, datumStr);
                GlobalVariables.DistBorder = Math.Round(Convert.ToDouble(distBorder.Rows[0][0]) / 1000.0, 3);
                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listCodigoColin, out string listCodigoSup, out List<string> colectionsAreaSup);
                //await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(map,Path.Combine(outputFolder, catastroShpNamePath));
                await FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                await FeatureProcessorUtils.UpdateValueAsync(catastroShpName, valueCodeDm);
                FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listaCodigoColin, out string listaCodigoSup, out List<string> coleccionesAareaSup);
                await FeatureProcessorUtils.UpdateRecordsDmAsync(catastroShpName, listaCodigoColin, listaCodigoSup, coleccionesAareaSup);
                await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{valueCodeDm}'");
                string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                await SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, valueCodeDm);
                var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, valueCodeDm, GlobalVariables.CurrentDatumDm, GlobalVariables.CurrentZoneDm);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
                List<ResultadoEval> responseJson = JsonConvert.DeserializeObject<List<ResultadoEval>>(response.ReturnValue);
                var areaDisponible = responseJson.FirstOrDefault(r => r.CodigoU.Equals(valueCodeDm, StringComparison.OrdinalIgnoreCase)).Hectarea.ToString();
                //var areaDisponible = JsonConvert.DeserializeObject<string>(response.ReturnValue);
                LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
                List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", dmShpName, "Zona Urbana" };
                await LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                await LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                GlobalVariables.CurrentShpName = "Catastro";
                MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, "Malla", zoneDm);
                await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
                await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
                string styleGrid = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
                await SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);

                ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();

                ResultadoEvaluacionModel resultadoEvaluacionModel = new ResultadoEvaluacionModel();
                //GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio = responseJson;
                //GlobalVariables.resultadoEvaluacion.areaDisponible = areaDisponible;
                //GlobalVariables.resultadoEvaluacion.codigo = valueCodeDm;
                //GlobalVariables.resultadoEvaluacion.nombre = GlobalVariables.CurrentNameDm;
                //GlobalVariables.resultadoEvaluacion.distanciaFrontera = GlobalVariables.DistBorder.ToString();
                //GlobalVariables.resultadoEvaluacion.isCompleted = true;

                resultadoEvaluacionModel.ListaResultadosCriterio = responseJson;
                resultadoEvaluacionModel.areaDisponible = areaDisponible;
                resultadoEvaluacionModel.codigo = valueCodeDm;
                resultadoEvaluacionModel.nombre = GlobalVariables.CurrentNameDm;
                resultadoEvaluacionModel.distanciaFrontera = GlobalVariables.DistBorder.ToString();
                resultadoEvaluacionModel.isCompleted = true;


                //var criterios = new string[] { "PR", "RD", "PO", "SI", "EX" };
                ////int contador = 0;
                //foreach (var criterio in criterios)
                //{
                //    GlobalVariables.resultadoEvaluacion.ResultadosCriterio[criterio] = await elementsLayoutUtils.ObtenerResultadosEval(criterio);
                //}
                //GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio = await elementsLayoutUtils.ObtenerResultadosEval1();
                //GlobalVariables.resultadoEvaluacion.isCompleted = true;

                try
                {
                    // Itera todos items seleccionados en el ListBox de WPF
                    foreach (var item in LayersListBox)
                    {
                            await LayerUtils.AddLayerCheckedListBox(item, zoneDm, featureClassLoader, datum, extentDmRadio);
                    }
                    MapUtils.AnnotateLayerbyName("Caram", "NOMBRE", "Caram_Anotaciones", "#ff0000", "Arial",7.5);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error en capa de listado", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return resultadoEvaluacionModel;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                geodatabase.Dispose();
            }
        }

        public static ExtentModel TransformBoundingBox(ExtentModel extentModel, string zoneDm)
        {
            ExtentModel extent = null;
            int epsgOrigen;
            if (zoneDm == "17") { epsgOrigen = 32717; }
            else if (zoneDm == "19") { epsgOrigen = 32719; }
            else { epsgOrigen = 0; }
            // 1. Construir la referencia espacial de origen y destino
            SpatialReference srOrigen = SpatialReferenceBuilder.CreateSpatialReference(epsgOrigen);
            SpatialReference srDestino = SpatialReferenceBuilder.CreateSpatialReference(32718);

            // 2. Crear un Envelope en la SR de origen
            Envelope envOrigen = EnvelopeBuilder.CreateEnvelope(
            new Coordinate2D(extentModel.xmin, extentModel.ymin),
                new Coordinate2D(extentModel.xmax, extentModel.ymax),
                srOrigen);

            // 3. Proyectar el Envelope a la SR destino
            var envDestino = GeometryEngine.Instance.Project(envOrigen, srDestino) as Envelope;
            if (envDestino == null)
                return extent; // Manejo simple de error

            extent = new ExtentModel
            {
                xmin = envDestino.XMin,
                xmax = envDestino.XMax,
                ymin = envDestino.YMin,
                ymax = envDestino.YMax, // Asignar el valor adecuado para ymax
            };
            // 4. Retornar las nuevas coordenadas
            return extent;
        }
        public async static Task EvaluationDmByCodeHistorico(string valueCodeDm, System.Data.DataRow dmRow, int radio = 0, int datum = 2)
        {
            string stateGraphic = dmRow["PE_VIGCAT"].ToString();
            //if (stateGraphic != "G") { return; }
            string zoneDm = dmRow["ZONA"].ToString();
            GlobalVariables.CurrentZoneDm = zoneDm;
            string areaValue = dmRow["HECTAREA"].ToString();
            GlobalVariables.CurrentAreaDm = areaValue;
            string nameDm = dmRow["NOMBRE"].ToString();
            GlobalVariables.CurrentNameDm = nameDm;
            var sdeHelper = new SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro,
            };
            GlobalVariables.CurrentCodeDm = valueCodeDm;
            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            //int datum=2;
            int datumwgs84 = 2;
            string datumStr = GlobalVariables.datumWGS;
            DatabaseHandler dataBaseHandler = new DatabaseHandler();
            var v_zona_dm = dataBaseHandler.VerifyDatumDM(valueCodeDm);
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;
            GlobalVariables.dmShpNamePath = "DM" + fechaArchi + ".shp";
            await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);
            try
            {
                // Obtener el mapa Catastro//
                Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroHistoricoWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaPsad56 + zoneDm, false);
                }

                var extentDmRadio = MapUtils.ObtenerExtent(valueCodeDm, datum, radio);
                var extentDm = MapUtils.ObtenerExtent(valueCodeDm, datum);
                GlobalVariables.currentExtentDM = extentDm;
                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                // Encontrando Distritos superpuestos a DM con
                //DataTable intersectDist;
                //if (datum == datumwgs84)
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroHistoricoWGS84 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, valueCodeDm);
                //}
                //else
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, valueCodeDm);
                //}
                //DataProcessorUtils.ProcessorDataAreaAdminstrative(intersectDist);
                DataTable orderUbigeosDM;
                orderUbigeosDM = dataBaseHandler.GetUbigeoData(valueCodeDm);

                //Carga capa Hojas IGN
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }
                string listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", extentDm.xmin, extentDm.ymin, extentDm.xmax, extentDm.ymax);
                //GlobalVariables.CurrentPagesDm = listHojas;
                // Encontrando Caram superpuestos a DM con
                //DataTable intersectCaram;
                //if (datum == datumwgs84)
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroHistoricoWGS84 + zoneDm, FeatureClassConstants.gstrFC_Caram84 + zoneDm, valueCodeDm);
                //}
                //else
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_Caram56 + zoneDm, valueCodeDm);
                //}
                //DataProcessorUtils.ProcessorDataCaramIntersect(intersectCaram);

                //DataTable intersectCForestal;
                //if (datum == datumwgs84)
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroHistoricoWGS84 + zoneDm, FeatureClassConstants.gstrFC_Cforestal + zoneDm, valueCodeDm);
                //}
                //else
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_forestal + zoneDm, valueCodeDm);
                //}
                //DataProcessorUtils.ProcessorDataCforestalIntersect(intersectCForestal);

                //DataTable intersectDm;
                //if (datum == datumwgs84)
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroHistoricoWGS84, FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, valueCodeDm);
                //}
                //else
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, valueCodeDm);
                //}
                
                //DataTable distBorder;
                //var distBorder = dataBaseHandler.CalculateDistanceToBorder(valueCodeDm, zoneDm, datumStr);
                //if (distBorder != null)
                //{
                //    GlobalVariables.DistBorder = Math.Round(Convert.ToDouble(distBorder.Rows[0][0]) / 1000.0, 3);
                //}

                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listCodigoColin, out string listCodigoSup, out List<string> colectionsAreaSup);
                //await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(map,Path.Combine(outputFolder, catastroShpNamePath));
                await FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                await FeatureProcessorUtils.UpdateValueAsync(catastroShpName, valueCodeDm);
                //FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listaCodigoColin, out string listaCodigoSup, out List<string> coleccionesAareaSup);
                //await FeatureProcessorUtils.UpdateRecordsDmAsync(catastroShpName, listaCodigoColin, listaCodigoSup, coleccionesAareaSup);
                await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{valueCodeDm}'");
                string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                await SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, valueCodeDm);
                var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, valueCodeDm);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
                //var areaDisponible = JsonConvert.DeserializeObject<string>(response.ReturnValue);
                //GlobalVariables.resultadoEvaluacion.areaDisponible = areaDisponible;
                LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
                List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", dmShpName, "Zona Urbana" };
                await LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                await LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                GlobalVariables.CurrentShpName = "Catastro";
                MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
                
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, "Malla", zoneDm);
                await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
                await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
                string styleGrid = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
                await SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);

                //ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();

                //GlobalVariables.resultadoEvaluacion.codigo = valueCodeDm;
                //GlobalVariables.resultadoEvaluacion.nombre = GlobalVariables.CurrentNameDm;
                //GlobalVariables.resultadoEvaluacion.distanciaFrontera = GlobalVariables.DistBorder.ToString();


                //var criterios = new string[] { "PR", "RD", "PO", "SI", "EX" };
                ////int contador = 0;
                //foreach (var criterio in criterios)
                //{
                //    GlobalVariables.resultadoEvaluacion.ResultadosCriterio[criterio] = await elementsLayoutUtils.ObtenerResultadosEval(criterio);
                //}
                //GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio = await elementsLayoutUtils.ObtenerResultadosEval1();
                //GlobalVariables.resultadoEvaluacion.isCompleted = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                geodatabase.Dispose();
            }
        }

    }
}
