using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Core.Geoprocessing;
using static Automapic_pro.GlobalVariables;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Core;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using System.Threading;

namespace Automapic_pro
{
    public static class toolBox
    {
        // Variables Globales relacionadas a ToolBox's
        //* _toolBoxPath: Rutas donde se encuentran los archivos de tipo *.atbx
        public static string _toolBoxPath_mapa_geologico = currentPath + @"\scripts\T01_mapa_geologico_50k.atbx";
        public static string _toolBoxPath_datos_campo = currentPath + @"\scripts\T00_datos_campo.atbx";

        //Nombre de herramientas del atbx _toolboxPath//

        //_:Herramientas del modulo de Mapa Geologico
        public static string _tool_getComponentCodeSheetMg = "getComponentCodeSheetMg";
        public static string _tool_addFeatureQuadsToMapMg = "addFeatureQuadsToMapMg";
        
        //_:Herramientas del modulo de Datos de Campo
        public static string _tool_gettreeLayers = "getTreeLayers";
        public static string _tool_addFeatureToMap = "addFeatureToMap";
        public static string _tool_removeFeatureOfTOC = "removeFeatureOfTOC";


        //'* ExecuteGPAsync: Ejecuta una herramienta personalizada
        //'* parametros:
        //'   - toolName: Nombre de la herramienta
        //'   - parameters: True: Lista de parametros que deben ser pasados a la herramienta
        //'   - toolboxPath: Ruta de un nuevo atbx; por defecto el valor apunta a la variable _toolboxPath
        //public static Task<IGPResult> ExecuteGPAsync(string toolboxPath, string toolName, IReadOnlyList<string> parameters, bool showProgressDialog = false)
        //{

        //    try
        //    {
        //        //if (parameters.Count == 0)
        //        //{
        //        //    parameters = null;
        //        //}
        //        // Preparar la ruta completa de la herramienta incluyendo el toolboxPath si es necesario
        //        string toolPath = !string.IsNullOrEmpty(toolboxPath) ? $"{toolboxPath}\\{toolName}" : toolName;

        //        // Ejecutar la herramienta de geoprocesamiento en un hilo de UI
        //        gpResult = (IGPResult)QueuedTask.Run(async () =>
        //        {
        //            // Si se desea mostrar un diálogo de progreso (opcional)
        //            if (showProgressDialog)
        //            {
        //                // Mostrar progreso aquí (ArcGIS Pro SDK maneja el progreso de forma diferente a ArcObjects)
        //                // Por ejemplo, puedes usar ProWindow para mostrar el progreso o un diálogo modal personalizado.
        //            }
        //            GPExecuteToolFlags executeFlags = GPExecuteToolFlags.AddToHistory;
        //            // Ejecutar la herramienta de geoprocesamiento
        //            var result = await Geoprocessing.ExecuteToolAsync(toolPath, parameters, null, null, null, executeFlags);

        //            return result;
        //        });

        //        if (gpResult.IsFailed)
        //        {
        //            // Manejar el caso de fallo
        //            MessageBox.Show("Error al ejecutar geoproceso: " + gpResult.ReturnValue);
        //            return (Task<IGPResult>)gpResult;
        //        }

        //        MessageBox.Show("al ejecutar geoproceso: " + gpResult.ReturnValue);
        //        //MessageBox.Show("Error al ejecutar geoproceso: " + gpResult.);
        //        return (Task<IGPResult>)gpResult;
        //    }
        //    catch
        //    {
        //        // Manejar excepciones aquí
        //        return (Task<IGPResult>)gpResult;
        //    }
        //}

        public static async Task<IGPResult> ExecuteGPAsync(string toolboxPath, string toolName, IReadOnlyList<string> parameters, bool showProgressDialog = false)
        {
            if (parameters.Count == 0)
            {
                parameters = null;
            }
            try
            {
                //Preparar la ruta completa de la herramienta incluyendo el toolboxPath si es necesario
                string toolPath = !string.IsNullOrEmpty(toolboxPath) ? $"{toolboxPath}\\{toolName}" : toolName;
                // Asegúrate de usar "await" con cualquier llamada que devuelva Task dentro de este método
                var result = await Geoprocessing.ExecuteToolAsync(toolPath, parameters, null, CancellationToken.None, null, GPExecuteToolFlags.AddToHistory);
                return result;
            }
            catch (Exception ex)
            {
                return (IGPResult)ex;
            }

            
        }


    }
}
