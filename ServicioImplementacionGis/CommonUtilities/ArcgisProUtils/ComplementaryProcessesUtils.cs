using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Mapping;
using DatabaseConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.ArcgisProUtils
{
    public class ComplementaryProcessesUtils
    {
        public static async void GenerateFreeAreaDm(string layerName, string folderName)
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
                var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, currentCodeDm);
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
    }
}
