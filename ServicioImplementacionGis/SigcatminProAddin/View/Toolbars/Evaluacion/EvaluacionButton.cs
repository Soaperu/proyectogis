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
using DevExpress.Utils;
using SigcatminProAddin.View.Toolbars.BDGeocatmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CommonUtilities.ArcgisProUtils;
using CommonUtilities;
using DevExpress.Pdf.Xmp;

namespace SigcatminProAddin.View.Toolbars.Evaluacion
{
    internal class EvaluacionButton : Button
    {
        protected override void OnClick()
        {
        }
    }
    internal class VerTodos: EvaluacionButton
    {
        protected override async void OnClick()
        {
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PR','PO','SI','CO','EX','VE','AR')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }       
    internal class VerAntPostColin : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PR','PO','CO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerAntPost : EvaluacionButton
    {
        protected override async void OnClick()
        {
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PR','PO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerAnteriores : EvaluacionButton { }
    internal class VerPosteriores : EvaluacionButton
    {
        protected override async void OnClick()
        {
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class CargarCapasIntegrales : EvaluacionButton { }
    internal class VerSimultaneos : EvaluacionButton { }
    internal class VerExtinguidos : EvaluacionButton { }
    internal class VerAntecesorRD : EvaluacionButton { }
    internal class VerColindantes : EvaluacionButton { }
    internal class VerEvaluado : EvaluacionButton { }
    internal class GenerarResultadosEvaluacion : EvaluacionButton { }
    internal class CalculoAreaDisponible : EvaluacionButton { }
    internal class ObservacionesCartaIGN : EvaluacionButton { }
    internal class VerCapas : EvaluacionButton { }
    internal class GenerarMallaCuadriculas : EvaluacionButton { }
    internal class PlanoCuadriculas : EvaluacionButton { }
    internal class GraficaDesdeExcel : EvaluacionButton { }
    internal class ConsultaAreaDisponible : EvaluacionButton { }
}
