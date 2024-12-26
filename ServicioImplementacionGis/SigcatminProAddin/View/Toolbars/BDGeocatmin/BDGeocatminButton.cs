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

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin
{
    internal class BDGeocatminButton : Button
    {
        public const string ExploreToolName = "esri_mapping_exploreTool";
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
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureLayer);
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

            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
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
            var layoutItem = await CommonUtilities.ArcgisProUtils.LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeEval);
            ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();
            (x, y) =await elementsLayoutUtils.TextElementsEvalAsync(layoutItem);
            y = await elementsLayoutUtils.AgregarTextosLayoutAsync("Evaluacion",layoutItem, y);
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
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
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

            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
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
    internal class GenerarNumeroResolucionDm : BDGeocatminButton { }
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
                CommonUtilities.ArcgisProUtils.MapUtils.AnnotateVertices(mapPoint);
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
            var response = System.Windows.MessageBox.Show("¿Desea Retirar los vértice?","caption", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
            if(response == MessageBoxResult.OK)
            {
                //await ArcGIS.Desktop.Framework.FrameworkApplication.SetCurrentToolAsync(null);
                string graphicLayerName = "Vertices";
                CommonUtilities.ArcgisProUtils.MapUtils.DeleteGraphicLayerByName(graphicLayerName);
            }
            
            
            
        }
    }
    internal class GenerarDemarcacionMultiple : BDGeocatminButton { }
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
}
