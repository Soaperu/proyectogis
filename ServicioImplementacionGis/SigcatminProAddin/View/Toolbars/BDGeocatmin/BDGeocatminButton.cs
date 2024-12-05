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
using SigcatminProAddin.View.Toolbars.InformeTecnico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await CommonUtilities.ArcgisProUtils.LayoutUtils.AddLayoutPath(@"U:\\Geocatmin\\Plantillas\\eva1Layuot.pagx");
        }
    }
    internal class PlanoDemarcacion : BDGeocatminButton { }
    internal class PlanoCarta : BDGeocatminButton { }
    internal class DibujarPoligono : BDGeocatminButton { }
    internal class DmGoogleEarth : BDGeocatminButton { }
    internal class BorrarTemas : BDGeocatminButton { }
    internal class VerCapas : BDGeocatminButton { }
    internal class GenerarNumeroResolucionDm : BDGeocatminButton { }
    internal class RotularVertices : BDGeocatminButton { }
    internal class RotularVerticesTool: MapTool
    {
        public RotularVerticesTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Rectangle;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
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
