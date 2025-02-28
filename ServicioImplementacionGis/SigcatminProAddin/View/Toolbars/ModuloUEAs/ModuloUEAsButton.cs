using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Utilities;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.View.Toolbars.ModuloUEAs
{
    internal class ModuloUEAsButton : Button
    {
        protected override void OnClick()
        {
        }
    }
    internal class ReporteIntegranteUEA : ModuloUEAsButton { }
    internal class ReporteDmIncluidosUEA : ModuloUEAsButton { }
    internal class ReporteDmExcluidosUEA : ModuloUEAsButton { }
    internal class ReporteDmCirculo : ModuloUEAsButton { }
    internal class GraficasDmsSegunRadio : ModuloUEAsButton { }
    internal class PlanoIntegrantesUEA : ModuloUEAsButton { }
    internal class PlanoDmInclusionExclusionUEA : ModuloUEAsButton { }
    internal class PlanoDmExcluirseUEA : ModuloUEAsButton { }
    internal class PlanoAnalisisUEA : ModuloUEAsButton { }
    internal class PlanoIntegradoUEA : ModuloUEAsButton { }
    internal class ReporteIntegradoUEA : ModuloUEAsButton { }

}
