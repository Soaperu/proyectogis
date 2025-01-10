using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using SigcatminProAddin.View.Toolbars.InformeTecnico;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilities.ArcgisProUtils;
using System.Windows;
using ArcGIS.Core.SystemCore;
using ArcGIS.Desktop.Core.Geoprocessing;
using DevExpress.Drawing.Internal.Fonts;
using SigcatminProAddin.View.Toolbars.BDGeocatmin.UI;
using DevExpress.Data.Helpers;
using ArcGIS.Core.Data.Analyst3D;
using DatabaseConnector;

using ArcGIS.Desktop.Internal.Mapping.Ribbon;
using ArcGIS.Desktop.Internal.Editing;
using System.Data;
using static ArcGIS.Desktop.Internal.GeoProcessing.Controls.rtbEditor;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Desktop.Internal.KnowledgeGraph;
using DevExpress.Xpo;
using static System.Net.WebRequestMethods;
using DevExpress.XtraPrinting.Native;
using System.Security.Policy;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping.Events;
using DevExpress.CodeParser;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin
{
    internal class BDGeocatminButton : Button
    {
        public const string ExploreToolName = "esri_mapping_exploreTool";
        public DatabaseHandler _dataBaseHandler = new DatabaseHandler();
        protected override void OnClick()
        {
        }
    }
    internal class ReporteDerechosMineros : BDGeocatminButton 
    {
        ReporteDMWpf reporteDMWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);

            reporteDMWpf = new ReporteDMWpf();
            reporteDMWpf.Closed += (s, e) => reporteDMWpf = null;
            reporteDMWpf.Show();
        }
    }


    internal class ListarCoordenadasTool : MapTool
    {
        private static ListarCoordenadasWpf _listarCoordenadasWindow;
        public ListarCoordenadasTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;

        }

        protected override async Task OnToolActivateAsync(bool active)
        {
            await base.OnToolActivateAsync(active);
            if (_listarCoordenadasWindow != null && _listarCoordenadasWindow.IsVisible)
            {
                _listarCoordenadasWindow.Show();
            }
            else
            {
                // Crea la ventana si no existe
                _listarCoordenadasWindow = new ListarCoordenadasWpf();             
                _listarCoordenadasWindow.Show();
            }

            //return base.OnToolActivateAsync(active);
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs args)
        {
            base.OnToolMouseDown(args);
            QueuedTask.Run(async () =>
            {
                var mapPoint = MapView.Active.ClientToMap(args.ClientPoint);
                if (_listarCoordenadasWindow != null && _listarCoordenadasWindow.IsVisible)
                {
                    // Actualiza el contenido según los datos del punto
                    if (_listarCoordenadasWindow is ListarCoordenadasWpf listarCoordenadasWpf)
                    {
                        await _listarCoordenadasWindow.UpdateContent(mapPoint); // Método para actualizar contenido
                    }
                }
            });
        }

        private async Task DeactivateTool(bool deactivateTool = false)
        {
            if (deactivateTool)
                await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
        }
        protected override void OnToolKeyDown(MapViewKeyEventArgs k)
        {
            base.OnToolKeyDown(k);
            if (k.Key == System.Windows.Input.Key.Escape)
            {
                k.Handled = true;

                FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
            }
        }
        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }

    internal class ConsultaDmTool : MapTool 
    {
        private static ConsultaDMWpf _ConsultaDMWpfWindow;
        public ConsultaDmTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;

        }

        protected override async Task OnToolActivateAsync(bool active)
        {
            await base.OnToolActivateAsync(active);
            if (_ConsultaDMWpfWindow != null && _ConsultaDMWpfWindow.IsVisible)
            {
                _ConsultaDMWpfWindow.Show();
            }
            else
            {
                // Crea la ventana si no existe
                _ConsultaDMWpfWindow = new ConsultaDMWpf();
                _ConsultaDMWpfWindow.Show();
            }

            //return base.OnToolActivateAsync(active);
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs args)
        {
            base.OnToolMouseDown(args);
            QueuedTask.Run(async () =>
            {
                var mapPoint = MapView.Active.ClientToMap(args.ClientPoint);
                if (_ConsultaDMWpfWindow != null && _ConsultaDMWpfWindow.IsVisible)
                {
                    // Actualiza el contenido según los datos del punto
                    if (_ConsultaDMWpfWindow is ConsultaDMWpf consultaDMWpfWindow)
                    {
                        await _ConsultaDMWpfWindow.UpdateContent(mapPoint); // Método para actualizar contenido
                    }
                }
            });
        }

        private async Task DeactivateTool(bool deactivateTool = false)
        {
            if (deactivateTool)
                await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
        }
    }
    internal class CalculaPorcentajeRegionTool : MapTool
    {
        private static CalcularPorcentajeRegionWpf _calcularPorcentajeRegionWindow;
        public CalculaPorcentajeRegionTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;

        }

        protected override async Task OnToolActivateAsync(bool active)
        {
            await base.OnToolActivateAsync(active);
            if (_calcularPorcentajeRegionWindow != null && _calcularPorcentajeRegionWindow.IsVisible)
            {
                _calcularPorcentajeRegionWindow.Show();
            }
            else
            {
                // Crea la ventana si no existe
                _calcularPorcentajeRegionWindow = new CalcularPorcentajeRegionWpf();
                _calcularPorcentajeRegionWindow.Show();
            }

            //return base.OnToolActivateAsync(active);
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs args)
        {
            base.OnToolMouseDown(args);
            QueuedTask.Run(async () =>
            {
                var mapPoint = MapView.Active.ClientToMap(args.ClientPoint);
                if (_calcularPorcentajeRegionWindow != null && _calcularPorcentajeRegionWindow.IsVisible)
                {
                    // Actualiza el contenido según los datos del punto
                    if (_calcularPorcentajeRegionWindow is CalcularPorcentajeRegionWpf calcularPorcentajeRegionWpf)
                    {
                        await _calcularPorcentajeRegionWindow.UpdateContent(mapPoint); // Método para actualizar contenido
                    }
                }
            });
        }

        private async Task DeactivateTool(bool deactivateTool = false)
        {
            if (deactivateTool)
                await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
        }
        protected override void OnToolKeyDown(MapViewKeyEventArgs k)
        {
            base.OnToolKeyDown(k);
            if (k.Key == System.Windows.Input.Key.Escape)
            {
                k.Handled = true;

                FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
            }
        }
        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }

    internal class LimitesRegionales : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            try
            {
                FeatureLayer featureLayer = null;
                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);
                var zoneDm = GlobalVariables.CurrentZoneDm;
                if (GlobalVariables.CurrentDatumDm == "1")
                {
                    featureLayer = await LayerUtils.AddFeatureClassToMapFromGdbAsync(geodatabase,FeatureClassConstants.gstrFC_CuadriculaR_WGS84, "Cuadricula Regional", false);
                }
                else
                {
                    featureLayer = await LayerUtils.AddFeatureClassToMapFromGdbAsync(geodatabase, FeatureClassConstants.gstrFC_CuadriculaR_PSAD56, "Cuadricula Regional", false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureLayer);
            }
            catch (Exception ex) { }
        }
    }
    internal class PlanoAreasSuperpuestas : BDGeocatminButton { }
    internal class PlanoEvaluacion : BDGeocatminButton 
    {
        protected override async void OnClick() 
        {
            double x;
            double y;
            string planeEval;
            string pathLayout;
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeEval);
                planeEval = GlobalVariables.planeEval.Split('.')[0];
            }
            else 
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeEval56);
                planeEval = GlobalVariables.planeEval56.Split('.')[0];
            }
            string mapName = GlobalVariables.mapNameCatastro;
            string nameLayer = GlobalVariables.CurrentShpName;
            var layoutItem = await LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeEval);
            ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();
            (x, y) =await elementsLayoutUtils.TextElementsEvalAsync(layoutItem);
            y = await elementsLayoutUtils.AgregarTextosLayoutAsync("Evaluacion", layoutItem, 15.2);
            await elementsLayoutUtils.GeneralistaDmPlanoEvaAsync(y);
        }
    }
    internal class PlanoDemarcacion : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            double x;
            double y;
            string planeDemarca;
            string pathLayout;
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeDemarca84);
                planeDemarca = GlobalVariables.planeDemarca84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeDemarca56);
                planeDemarca = GlobalVariables.planeDemarca56.Split('.')[0];
            }
            string mapName = GlobalVariables.mapNameDemarcacionPo;
            string nameLayer = GlobalVariables.CurrentShpName;
            var layoutItem = await CommonUtilities.ArcgisProUtils.LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeDemarca);
            DemarcaElementsLayoutUtils demarcaElementsLayoutUtils = new DemarcaElementsLayoutUtils();
            await demarcaElementsLayoutUtils.AddDemarcaTextAsync("", GlobalVariables.CurrentDistDm, "", "", GlobalVariables.CurrentProvDm, "", GlobalVariables.CurrentDepDm, layoutItem);
        }
    }
    internal class PlanoCarta : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            double x;
            double y;
            string planeCarta;
            string pathLayout;
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeCarta84);
                planeCarta = GlobalVariables.planeCarta84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeCarta56);
                planeCarta = GlobalVariables.planeCarta56.Split('.')[0];
            }
            string mapName = GlobalVariables.mapNameCartaIgn;
            string nameLayer = GlobalVariables.CurrentShpName;
            var layoutItem = await CommonUtilities.ArcgisProUtils.LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeCarta);
            CartaIgnElementsLayoutUtils cartaIgnElementsLayoutUtils = new CartaIgnElementsLayoutUtils();
            string listDist = GlobalVariables.CurrentDistDm;
            string listProv = GlobalVariables.CurrentProvDm;
            string listDep = GlobalVariables.CurrentDepDm;
            string listHojas = StringProcessorUtils.FormatStringCartaIgnForTitle(GlobalVariables.CurrentPagesDm);
            await cartaIgnElementsLayoutUtils.AddCartaIgnTextAsync(layoutItem, listHojas, "", listDist, "", listProv, "", listDep, "", "");
        }
    }
    internal class DibujarPoligono : BDGeocatminButton 
    {
        DibujarPoligonoWpf DrawPolygonWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            DrawPolygonWpf = new DibujarPoligonoWpf();
            DrawPolygonWpf.Closed += (s, e) => DrawPolygonWpf = null;
            DrawPolygonWpf.Show();
        }
    }
    internal class DmGoogleEarth : BDGeocatminButton
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            var Params = Geoprocessing.MakeValueArray("Catastro");
            var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetKMLodDM, Params);
        }
    }
    internal class BorrarTemas : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            // Mostrar el cuadro de diálogo con opciones "Sí" y "No"
            var result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("¿Deseas eliminar todas las capas del Mapa?",
                                                                            "Confirmación",
                                                                            System.Windows.MessageBoxButton.YesNo,
                                                                            System.Windows.MessageBoxImage.Question);
            
            // Evaluar la respuesta del usuario
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                // Acción para "Sí"
                await LayerUtils.RemoveLayersFromActiveMapAsync();
            }
            else
            {
                // Acción para "No"
                return;
            }
        }
    }
    internal class VerCapas : BDGeocatminButton 
    {
        protected override void OnClick()
        {
            LayerUtils.SelectSetAndZoomByNameAsync("Catastro", false);
        }
    }
    internal class GenerarNumeroResolucionDm : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            //LayerUtils.SelectSetAndZoomByNameAsync("Catastro", false);
            await UpdateResoluDMFields("Catastro");
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Proceso finalizado, Valide la tabla de contenido");
        }

        public async Task UpdateResoluDMFields(string capa)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
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
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No se encuentra el Layer");
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
                                DataTable lodtbDatos_dm = _dataBaseHandler.GetDatosResolucion(v_codigo_dm);
                                if (lodtbDatos_dm.Rows.Count > 0)
                                {
                                    string resol_tit = lodtbDatos_dm.Rows[0]["RESOL_TIT"].ToString();
                                    var fec_titu_obj = lodtbDatos_dm.Rows[0]["FEC_TITU"];
                                    string fec_titu = "";
                                    string resol_ext = lodtbDatos_dm.Rows[0]["RESOL_EXT"].ToString();
                                    var fec_ext_obj = lodtbDatos_dm.Rows[0]["FEC_EXT"];
                                    string fec_ext = "";
                                    string cali = lodtbDatos_dm.Rows[0]["CALI"].ToString();

                                    if (fec_titu_obj == null || fec_titu_obj == DBNull.Value || !DateTime.TryParse(fec_titu_obj.ToString(), out DateTime fecTitu))
                                    {
                                        // No es fecha válida
                                        fec_titu = "18000101";
                                    }
                                    else
                                    {
                                        // Sí es fecha válida
                                        fec_titu = fecTitu.ToString("yyyyMMdd");
                                    }

                                    if (fec_ext_obj == null || fec_ext_obj == DBNull.Value || !DateTime.TryParse(fec_ext_obj.ToString(), out DateTime fecExt))
                                    {
                                        // No es fecha válida
                                        fec_ext = "18000101";
                                    }
                                    else
                                    {
                                        // Sí es fecha válida
                                        fec_ext = fecExt.ToString("yyyyMMdd");
                                    }
                                    if (DateTime.TryParse(fec_titu, out DateTime dt_titu) && DateTime.TryParse(fec_ext, out DateTime dt_fec_ext))
                                    {
                                        if (dt_titu > dt_fec_ext)
                                        {
                                            row["NUM_RESOL"] = resol_tit;
                                            row["FEC_RESOL"] = fec_titu_obj.ToString();
                                            row["CALIF"] = cali;
                                        }
                                        else if (dt_titu < dt_fec_ext)
                                        {
                                            row["NUM_RESOL"] = resol_ext;
                                            row["FEC_RESOL"] = fec_ext_obj.ToString();
                                            row["CALIF"] = cali;
                                        }                                       
                                    }                                    
                  
                                }                               

                                row.Store();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Error en UpdateValue: " + ex.Message);
                }
            });

        }


    }
    internal class RotularVerticesTool: MapTool
    {
        public RotularVerticesTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;

        }

        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs args)
        {
            base.OnToolMouseDown(args);
            QueuedTask.Run(() =>
            {
                var mapPoint = MapView.Active.ClientToMap(args.ClientPoint);
                MapUtils.AnnotateVertices(mapPoint);
            });
                
        }

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }
    internal class RetirarVertices : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
            var response = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("¿Desea Retirar los vértice?", "caption", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            if (response == MessageBoxResult.OK)
            {
                //await ArcGIS.Desktop.Framework.FrameworkApplication.SetCurrentToolAsync(null);
                string graphicLayerName = "Vertices";
                MapUtils.DeleteGraphicLayerByName(graphicLayerName);
            }
            
        }
    }
    internal class GenerarDemarcacionMultiple : BDGeocatminButton
    {
        protected override async void OnClick()
        {
            await UpdateDemarcacionFields("Catastro");
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Proceso finalizado, Valide la tabla de contenido");
        }

        public static int CalculateParameter(int length)
        {
            return length switch
            {
                6 => 1,
                13 => 2,
                20 => 3,
                27 => 4,
                34 => 5,
                41 => 6,
                48 => 7,
                55 => 8,
                62 => 9,
                69 => 10,
                _ => 0
            };
        }

        public static string BuildJoinedCodes(string demagisValue, int parametro)
        {
            var codes = new List<string>();
            for (int i = 0; i < parametro; i++)
            {
                int startIndex = i * 7;
                codes.Add(demagisValue.Substring(startIndex, 6));
            }
            return string.Join(",", codes.Select(code => $"'{code}'"));
        }

        public static (string dptos, string provs, string dists) ProcessDemarcaData(DataTable demarcaData)
        {
            var listaDptos = new HashSet<string>();
            var listaProvs = new HashSet<string>();
            var listaDists = new HashSet<string>();

            foreach (DataRow row in demarcaData.Rows)
            {
                listaDptos.Add(row["DPTO"].ToString());
                listaProvs.Add(row["PROV"].ToString());
                listaDists.Add(row["DIST"].ToString());
            }

            return (
                string.Join(",", listaDptos),
                string.Join(",", listaProvs),
                string.Join(",", listaDists)
            );
        }
        public async Task UpdateDemarcacionFields(string capa, string filter = "1=1")
        {
            try
            {
                // Encuentre la capa "Catastro" en el mapa activo usando el método de utilidad
                var catastroLayer = LayerUtils.GetFeatureLayerByNameAsync(capa);

                if (catastroLayer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"No se encuentra el Layer {capa}");
                    return;
                }

                // Accede al feature class
                await QueuedTask.Run(() =>
                {
                    using (FeatureClass featureClass = catastroLayer.Result.GetTable() as FeatureClass)
                    {
                        if (featureClass == null)
                        {
                            throw new InvalidOperationException("Feature class is not accessible.");
                        }

                        // Definir un filtro de consulta (actualmente recupera todas las funciones, ajustar según sea necesario)
                        QueryFilter queryFilter = new QueryFilter { WhereClause = filter };

                        using (RowCursor cursor = featureClass.Search(queryFilter, false))
                        {
                            int counter = 0;

                            while (cursor.MoveNext())
                            {
                                using (Feature feature = cursor.Current as Feature)
                                {
                                    counter++;

                                    // Actualizar campo "CONTADOR"
                                    feature["CONTADOR"] = counter;

                                    string demagisValue = feature["DEMAGIS"].ToString();
                                    int length = demagisValue.Length;

                                    int parametro = CalculateParameter(length);
                                    if (parametro > 0)
                                    {
                                        string joinedCodes = BuildJoinedCodes(demagisValue, parametro);

                                        // Recuperar datos adicionales 
                                        DataTable demarcaData = _dataBaseHandler.GetUbigeoDataMultiple(joinedCodes);

                                        if (demarcaData.Rows.Count > 0)
                                        {
                                            var (listaDptos, listaProvs, listaDists) = ProcessDemarcaData(demarcaData);

                                            feature["DPTOS"] = listaDptos;
                                            feature["PROVS"] = listaProvs;
                                            feature["DISTS"] = listaDists;
                                        }
                                    }
                                    feature.Store();
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
    internal class RotulaTextoDemarcacion : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            MapUtils.AnnotateLayerbyName("Departamento", "NM_DEPA", "Anotación Departamento", "#895a44", "Tahoma", 25, "Bold");
            MapUtils.AnnotateLayerbyName("Provincia", "NM_PROV", "Anotación Provincia", "#007800", "Tahoma", 15, "Bold");
            MapUtils.AnnotateLayerbyName("Distrito", "NM_DIST", "Antoación Distrito", "#0000ff", "Tahoma", 8, "Bold");
        }
           
    }
    internal class PlanoSimultaneidad : BDGeocatminButton { }
    internal class PlanosEvaluacionReduccion : BDGeocatminButton 
    {
        protected override async void OnClick()
        {
            double x;
            double y;
            string planeEval;
            string planeDemarca;
            string planeCarta;
            string pathLayout;
            string mapName;
            string nameLayer;
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            // Plano Evaluación
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeEval);
                planeEval = GlobalVariables.planeEval.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeEval56);
                planeEval = GlobalVariables.planeEval56.Split('.')[0];
            }
            
            mapName = GlobalVariables.mapNameCatastro;
            nameLayer = GlobalVariables.CurrentShpName;
            var layoutItem = await LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeEval);
            ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();
            (x, y) = await elementsLayoutUtils.TextElementsEvalAsync(layoutItem);
            y = await elementsLayoutUtils.AgregarTextosLayoutAsync("Evaluacion", layoutItem, y);
            await elementsLayoutUtils.GeneralistaDmPlanoEvaAsync(y);


            // Plano Demarcación
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeDemarca84);
                planeDemarca = GlobalVariables.planeDemarca84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeDemarca56);
                planeDemarca = GlobalVariables.planeDemarca56.Split('.')[0];
            }
            mapName = GlobalVariables.mapNameDemarcacionPo;
            mapName = GlobalVariables.CurrentShpName;
            layoutItem = await LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeDemarca);
            DemarcaElementsLayoutUtils demarcaElementsLayoutUtils = new DemarcaElementsLayoutUtils();
            await demarcaElementsLayoutUtils.AddDemarcaTextAsync("", GlobalVariables.CurrentDistDm, "", "", GlobalVariables.CurrentProvDm, "", GlobalVariables.CurrentDepDm, layoutItem);

            // Plano Carta IGN
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.valueDatumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeCarta84);
                planeCarta = GlobalVariables.planeCarta84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeCarta56);
                planeCarta = GlobalVariables.planeCarta56.Split('.')[0];
            }
            mapName = GlobalVariables.mapNameCartaIgn;
            nameLayer = GlobalVariables.CurrentShpName;
            layoutItem = await LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeCarta);
            CartaIgnElementsLayoutUtils cartaIgnElementsLayoutUtils = new CartaIgnElementsLayoutUtils();
            string listDist = GlobalVariables.CurrentDistDm;
            string listProv = GlobalVariables.CurrentProvDm;
            string listDep = GlobalVariables.CurrentDepDm;
            string listHojas = StringProcessorUtils.FormatStringCartaIgnForTitle(GlobalVariables.CurrentPagesDm);
            await cartaIgnElementsLayoutUtils.AddCartaIgnTextAsync(layoutItem, listHojas, "", listDist, "", listProv, "", listDep, "", "");
        }
    }
    internal class PlanosDiversosFormatos : BDGeocatminButton
    {
        protected override void OnClick()
        {
            UI.PlanosDiversosFormatos.Views.MainWindow mainWindow = new UI.PlanosDiversosFormatos.Views.MainWindow();
            mainWindow.Show();
        }
    }

    internal class SituacionDM : BDGeocatminButton
    {
        private async Task<Map> EnsureMapViewIsActiveAsync(string mapName)
        {
            if (MapView.Active != null)
            {
                return MapView.Active.Map;
            }

            // Esperar hasta que MapView.Active esté disponible
            TaskCompletionSource<Map> tcs = new TaskCompletionSource<Map>();

            SubscriptionToken eventToken = null;
            eventToken = DrawCompleteEvent.Subscribe(async args =>
            {
                // Desuscribirse del evento
                // Desuscribirse del evento
                if (eventToken != null)
                {
                    DrawCompleteEvent.Unsubscribe(eventToken);
                }
                // Activar el mapa "CATASTRO MINERO"
                Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync(mapName);
                await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        public async Task UpdateSituacionAsync(string capa)
        {
            var dataBaseHandler = new DatabaseHandler();
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener el documento del mapa y la capa
                    Map pMap = MapView.Active.Map;
                    FeatureLayer pFeatLayer1 = null;
                    string tempTipo = "";
                    string calculatedTipo = "";
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
                                string v_codigo_dm = row["CODIGOU"].ToString();

                                // Llamar al procedimiento para obtener datos de Datum y bloquear estado
                                DataTable lodtbDatos_dm = dataBaseHandler.GetDMEstaMin(v_codigo_dm);
                                if (lodtbDatos_dm.Rows.Count > 0)
                                {
                                    tempTipo = lodtbDatos_dm.Rows[0]["SITUACIONUP"].ToString();
                                }
                                else
                                {
                                    DataTable lodtbDatos1 = dataBaseHandler.GetDMIntegranteUEA(v_codigo_dm);
                                    if (lodtbDatos1.Rows.Count > 0)
                                    {
                                        string codigointegrante = lodtbDatos1.Rows[0]["CG_CODIGO"].ToString();
                                        lodtbDatos_dm = dataBaseHandler.GetDMEstaMin(codigointegrante);
                                        if (lodtbDatos_dm.Rows.Count > 0)
                                        {
                                            tempTipo = lodtbDatos_dm.Rows[0]["SITUACIONUP"].ToString();
                                        }
                                    }
                                }

                                // Actualizamos el campo tipo segun el tipo temporal y otras condiciones
                                if (tempTipo == "EXPLORACIÓN")
                                {
                                    calculatedTipo = "CONCESIÓN MINERA EN EXPLORACIÓN (1)";
                                }

                                else if(tempTipo == "EXPLOTACIÓN")
                                {
                                    calculatedTipo = "CONCESIÓN MINERA EN EXPLOTACIÓN (1)";
                                }
                                else
                                {
                                    calculatedTipo = tempTipo;
                                }
                                if (calculatedTipo == "")
                                {
                                    string leyenda = row["LEYENDA"].ToString();

                                    switch (leyenda)
                                    {
                                        case "G2":
                                            calculatedTipo = "SOLICITUD DE DERECHO MINERO";
                                            break;
                                        case "G4":
                                            calculatedTipo = "CONCESIÓN MINERA EXTINGUIDA";
                                            break;
                                        case "G5":
                                            calculatedTipo = "PLANTAS DE BENEFICIO, CANTERAS (ESTADO)";
                                            break;
                                        default:
                                            calculatedTipo = "CONCESIÓN SIN ACTIVIDAD MINERA";
                                            break;


                                    }
                                }
                                row["TIPO"] = calculatedTipo;
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

        protected override async void OnClick()
        {
            string fechaArchi = DateTime.Now.Ticks.ToString();
            

            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
            string zoneDm = GlobalVariables.CurrentZoneDm;
            var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
            await featureClassLoader.ExportAttributesTemaAsync(GlobalVariables.CurrentShpName, GlobalVariables.stateDmY, "Situacion_DM");
            await UpdateSituacionAsync("Situacion_DM");
            MapUtils.AnnotateLayerbyName("Situacion_DM", "CONTADOR", "DM_Situacion");


        }
    }
}
