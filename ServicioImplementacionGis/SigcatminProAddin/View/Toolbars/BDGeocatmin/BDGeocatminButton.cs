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

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin
{
    internal class BDGeocatminButton : Button
    {
        protected override void OnClick()
        {
        }
    }
    internal class ReporteDerechosMineros : BDGeocatminButton { }
    internal class ListarCoordenadas : BDGeocatminButton { }
    internal class ConsultaDm : BDGeocatminButton { }
    internal class CalculaPorcentajeRegion : BDGeocatminButton { }
    internal class LimitesRegionales : BDGeocatminButton { }
    internal class PlanoAreasSuperpuestas : BDGeocatminButton { }
    internal class PlanoEvaluacion : BDGeocatminButton 
    {
        protected override async void OnClick() 
        {
            string pathLayout = Path.Combine(GlobalVariables.ContaninerTemplates, GlobalVariables.planeEval);
            string nameLayer = GlobalVariables.CurrentShpName;
            await CommonUtilities.ArcgisProUtils.LayoutUtils.AddLayoutPath(pathLayout, nameLayer);
        }
    }
    internal class PlanoDemarcacion : BDGeocatminButton { }
    internal class PlanoCarta : BDGeocatminButton { }
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
    internal class RotularVertices : BDGeocatminButton { }
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
                CommonUtilities.ArcgisProUtils.MapUtils.LabelVertices(mapPoint);
            });
                
        }

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }
    internal class RetirarVertices : BDGeocatminButton { }
    internal class GenerarDemarcacionMultiple : BDGeocatminButton { }
    internal class RotulaTextoDemarcacion : BDGeocatminButton { }
    internal class PlanoSimultaneidad : BDGeocatminButton { }
}
