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

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin
{
    internal class BDGeocatminButton : Button
    {
        protected override void OnClick()
        {
        }
    }
    internal class ReporteDerechosMineros : BDGeocatminButton { }


    internal class ListarCoordenadasTool : MapTool
    {
        private static ListarCoordenadasWpf _listarCoordenadasWindow;
        public ListarCoordenadasTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;

        }

        protected override Task OnToolActivateAsync(bool active)
        {
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

            return base.OnToolActivateAsync(active);
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

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }

    internal class ConsultaDm : BDGeocatminButton { }
    internal class CalculaPorcentajeRegion : BDGeocatminButton { }
    internal class LimitesRegionales : BDGeocatminButton { }
    internal class PlanoAreasSuperpuestas : BDGeocatminButton { }
    internal class PlanoEvaluacion : BDGeocatminButton 
    {
        protected override async void OnClick() 
        {
            double x;
            double y;
            string planeEval;
            string pathLayout;
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeEval);
                planeEval = GlobalVariables.planeEval.Split('.')[0];
            }
            else 
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeEval56);
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
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeDemarca84);
                planeDemarca = GlobalVariables.planeDemarca84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeDemarca56);
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
            if (GlobalVariables.CurrentDatumDm == GlobalVariables.datumWGS)
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeCarta84);
                planeCarta = GlobalVariables.planeCarta84.Split('.')[0];
            }
            else
            {
                pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeCarta56);
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
    internal class DibujarPoligono : BDGeocatminButton { }
    internal class DmGoogleEarth : BDGeocatminButton
    {
        protected override async void OnClick()
        {
            var Params = Geoprocessing.MakeValueArray("Catastro");
            var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetKMLodDM, Params);
        }
    }
    internal class BorrarTemas : BDGeocatminButton { }
    internal class VerCapas : BDGeocatminButton { }
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
            await ArcGIS.Desktop.Framework.FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
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
            await ArcGIS.Desktop.Framework.FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");
            MapUtils.AnnotateLayerbyName("Departamento", "NM_DEPA", "Anotación Departamento", "#895a44", "Tahoma", 25, "Bold");
            MapUtils.AnnotateLayerbyName("Provincia", "NM_PROV", "Anotación Provincia", "#007800", "Tahoma", 15, "Bold");
            MapUtils.AnnotateLayerbyName("Distrito", "NM_DIST", "Antoación Distrito", "#0000ff", "Tahoma", 8, "Bold");
        }
           
    }
    internal class PlanoSimultaneidad : BDGeocatminButton { }
}
