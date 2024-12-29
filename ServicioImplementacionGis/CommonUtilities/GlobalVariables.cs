using ArcGIS.Desktop.Core.Geoprocessing;
using System.Data;
using System.Globalization;

namespace CommonUtilities
{
    public class GlobalVariables
    {
        public static string currentUser = "";
        public static string pathFileContainerOut = @"C:\bdgeocatmin";
        public static string fileTemp = "Temporal";
        public static string pathFileTemp = Path.Combine(pathFileContainerOut, fileTemp);
        public static bool stateDmY = true;
        public static string idExport = "";
        public static string mapNameCatastro = "CATASTRO MINERO";
        public static string mapNameDemarcacionPo = "DEMARCACION POLITICA";
        public static string mapNameCartaIgn = "CARTA IGN";
        public static string fieldTypeString = "STRING";
        public static string fieldTypeLong = "LONG";
        public static string fieldTypeDouble = "DOUBLE";
        public static string fieldTypeDate = "DATE";
        //Datums
        public static string datumWGS = "WGS-84";
        public static string datumPSAD = "PSAD-56";

        // Ruta actual
        public static string? currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string scriptsPath = @"scripts\AutomapicExt_addin.py";
        public static string pythonExePath = @"C:\Program Files\ArcGIS\Pro\bin\Python\envs\arcgispro-py3\python.exe";
        // Rutas del Sistemas
        public static string toolBoxPathEval = currentPath + @"\Scripts\CMIN_EVAL.atbx";

        //Nombre de herramientas del atbx _toolboxPath//
        public static string toolGetEval = "evalCriterios";
        public static string toolGetKMLodDM = "exportarDmKmz";
        public static string toolGetDepas = "obtenerDepartamento";
        public static string toolGetAreasOverlay = "calculoAreaSupDisp";
        // Variables obtenidas de Evaluacion DM
        public static string CurrentNameDm { get; set; }
        public static string CurrentCodeDm { get; set; }
        public static string CurrentAreaDm { get; set; }
        public static string CurrentDatumDm { get; set; }
        public static string CurrentDepDm { get; set; }
        public static string CurrentProvDm { get; set; }
        public static string CurrentDistDm { get; set; }
        public static string CurrentShpName { get; set; }
        public static string CurrentZoneDm { get; set; }
        public static string CurrentPagesDm { get; set; } // hojas Carta IGN

        public static string CurrentTipoEx { get; set; }
        // Intersecciones con DM
        public static string listaCaramur { get; set; }
        public static string listaCaramre { get; set; }
        public static string listaCforestal { get; set; }

        public static ExtentModel currentExtentDM { get; set; }

        //Contenedor de Plantillas de Planos
        public static string ContaninerTemplatesReport = @"U:\\Geocatmin\\Plantillas";
        public static string planeEval = "Plantilla_evd_84.pagx";
        public static string planeEval56 = "Plantilla_evd_56.pagx";
        public static string planeDemarca84 = "plantilla_demarca_84.pagx";
        public static string planeDemarca56 = "plantilla_demarca_56.pagx";
        public static string planeCarta84 = "plantilla_cartaign_84.pagx";
        public static string planeCarta56 = "plantilla_cartaign_56.pagx";

        //Contenedor de Plantillas de Reportes
        public static string ContaninerTemplatesReports = @"U:\\Geocatmin\\Reporte";
        public static string reportDM = "rpt_Reporte_DM.xml";

        //Terminos Constantes de Planos
        public static string planeNameEval = "";
        public static string planeNameDemarca = "Plano Demarca";
        public static string planeNameCarta = "Plano carta";

        //Rutas Estilos .stylx
        public static string styleFolder = "Estilos";
        public static string stylePath = Path.Combine(pathFileContainerOut,styleFolder);//@"C:\bdgeocatmin\Estilos";

        public static string styleCatastro = "CATASTRO.stylx";
        public static string styleCaram = "CARAM.stylx";
        public static string styleMalla = "MALLA.stylx";
        public static string styleCForestal = "CATA_FORESTAL.stylx";

        //Lista de FeatureClass generales
        public static List<string> generalFeatureClass = new List<string>{ "Vías", "Drenaje", "Centro Poblado" };

        // Tablas Generales
        public static double DistBorder { get; set; }

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
