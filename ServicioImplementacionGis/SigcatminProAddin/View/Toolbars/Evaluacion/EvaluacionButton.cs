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
using System.Data;
using System.Security.Policy;
using SigcatminProAddin.View.Toolbars.BDGeocatmin.UI;
using SigcatminProAddin.View.Toolbars.Evaluacion.UI;
using ArcGIS.Desktop.Core.Geoprocessing;
using Newtonsoft.Json;
using System.Windows;

namespace SigcatminProAddin.View.Toolbars.Evaluacion
{
    internal class EvaluacionButton : Button
    {
        public const string ExploreToolName = "esri_mapping_exploreTool";
        protected override void OnClick()
        {
        }
    }
    internal class VerTodos: EvaluacionButton
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
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
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
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
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PR','PO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerAnteriores : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PR')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerPosteriores : EvaluacionButton
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','PO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class CargarCapasIntegrales : EvaluacionButton { }
    internal class VerSimultaneos : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','SI')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerExtinguidos : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','EX')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerAntecesorRD : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV','AR')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerColindantes : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV', 'CO')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class VerEvaluado : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string mapName = GlobalVariables.mapNameCatastro;
            string definitionQuery = "EVAL IN ('EV')";
            await LayerUtils.AplicarFiltroYZoomAsync(mapName, layerName, definitionQuery);
        }
    }
    internal class GenerarResultadosEvaluacion : EvaluacionButton 
    {
        ResultadoEvaluacionWpf resultadoEvaluacionWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);

            resultadoEvaluacionWpf = new ResultadoEvaluacionWpf();
            resultadoEvaluacionWpf.Closed += (s, e) => resultadoEvaluacionWpf = null;
            resultadoEvaluacionWpf.Show();
        }
    }
    internal class CalculoAreaDisponible : EvaluacionButton 
    {
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);
            string layerName = "Catastro";
            string folderName = GlobalVariables.pathFileTemp;
            string id = GlobalVariables.idExport;
            try
            {
                var Params = Geoprocessing.MakeValueArray(layerName, folderName, id, 1);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetAreasOverlay, Params);
                var responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.ReturnValue);
                var areaDisponible = responseJson["area_disponible"];
                var areaSuperpuesta = responseJson["area_superpuesta"];
                string layerNameDisponible = responseJson["nombreDisponible"];
                string layerNameSuperpuesta = responseJson["nombreSuperpuesta"];
                await LayerUtils.ChangeLayerNameAsync(layerNameDisponible, "Areadispo");
                await LayerUtils.ChangeLayerNameAsync(layerNameSuperpuesta, "Areainter");
                var pFeatureLayer_dispo = await LayerUtils.GetFeatureLayerByNameAsync("Areadispo");
                await SymbologyUtils.ColorPolygonSimple(pFeatureLayer_dispo);
                var pFeatureLayer_inter = await LayerUtils.GetFeatureLayerByNameAsync("Areainter");
                await SymbologyUtils.ColorPolygonSimple(pFeatureLayer_inter);
                string layerNameDispoDm = "Areadispo_" + GlobalVariables.CurrentCodeDm;
                string layerNameInterDm = "Areainter_" + GlobalVariables.CurrentCodeDm;
                await LayerUtils.ChangeLayerNameAsync("Areainter", layerNameInterDm);
                await LayerUtils.ChangeLayerNameAsync("Areadispo", layerNameDispoDm);
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(ex.Message, "Error en Calculo Area Disponible", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                // Paso 1: Preguntamos al usuario si desea continuar
                MessageBoxResult result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                    "Calculo de area disponible exitoso.\n \n¿Desea generar Área Disponible en Distinto Datum?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                // Paso 2: Verificamos la respuesta del usuario
                if (result == MessageBoxResult.Yes)
                {
                    ComplementaryProcessesUtils.GenerateAvailableAreaDm(layerName, folderName);
                }
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(ex.Message, "Error en Calculo Area Disponible en distinto datum", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
    internal class ObservacionesCartaIGN : EvaluacionButton 
    {
        ObservacionesCartaIgnWpf observacionesCartaIgnWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            observacionesCartaIgnWpf = new ObservacionesCartaIgnWpf();
            observacionesCartaIgnWpf.Closed += (s, e) => observacionesCartaIgnWpf = null;
            observacionesCartaIgnWpf.Show();
        }
    }
    internal class VerCapas : EvaluacionButton 
    {
        VerCapasWpf verCapasWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync("esri_mapping_exploreTool");

            verCapasWpf = new VerCapasWpf();
            verCapasWpf.Closed += (s, e) => verCapasWpf = null;
            verCapasWpf.Show();
        }
    }
    internal class GenerarMallaCuadriculas : EvaluacionButton {
        GenerarMallaWpf generarMallaWpf;
        protected override async void OnClick()
        {
            await FrameworkApplication.SetCurrentToolAsync(ExploreToolName);

            generarMallaWpf = new GenerarMallaWpf();
            generarMallaWpf.Closed += (s, e) => generarMallaWpf = null;
            generarMallaWpf.Show();
        }

        private void Graficarcuadriculas10(ExtentModel extent)
        {
            DataTable lodtbDatos = new DataTable();
            int k = 0;

            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"));

        }
    }
    internal class PlanoCuadriculas : EvaluacionButton { }
    internal class GraficaDesdeExcel : EvaluacionButton { }
    internal class ConsultaAreaDisponible : EvaluacionButton { }
}
