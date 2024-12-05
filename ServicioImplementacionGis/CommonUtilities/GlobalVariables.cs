using ArcGIS.Desktop.Core.Geoprocessing;
using System.Globalization;

namespace CommonUtilities
{
    public class GlobalVariables
    {
        public static string currentUser = "";
        public static string pathFileContainerOut = @"C:\bdgeocatmin";
        public static string fileTemp = "Temporal";
        public static bool stateDmY = true;
        public static string mapNameCastrato = "CATASTRO";
        public static string mapNameDemarcacionPo = "DEMARCACION POLITICA";
        public static string mapNameCartaIgn = "CARTA IGN";
        public static string fieldTypeString = "STRING";
        public static string fieldTypeLong = "LONG";
        public static string fieldTypeDouble = "DOUBLE";
        public static string fieldTypeDate = "DATE";
        // Ruta actual
        public static string? currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string scriptsPath = @"scripts\AutomapicExt_addin.py";
        public static string pythonExePath = @"C:\Program Files\ArcGIS\Pro\bin\Python\envs\arcgispro-py3\python.exe";

        public static string toolBoxPathEval = currentPath + @"\Scripts\CMIN_EVAL.atbx";
        public static string stylePath = @"C:\bdgeocatmin\Estilos";
        //Nombre de herramientas del atbx _toolboxPath//
        public static string toolGetEval = "evalCriterios";
        public static string CurrentCodeDm { get; set; }

        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input; // Si la entrada está vacía, se retorna como está
            }

            // Usar TextInfo para convertir a Title Case
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

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
