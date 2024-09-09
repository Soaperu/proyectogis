using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ArcGIS.Core.Geometry;
using System.Windows.Documents;
using ArcGIS.Desktop.Internal.Catalog;
using System.Text.Json;
using System.Windows;

namespace Automapic_pro
{
    internal class settings
    {
        public static string modeDev = "DEV"; //Cambiar a "PROD" cuando va produccion //"DEV" en modo desarrollo para ingresar sin ususario
    }
    public static class GlobalVariables
    {
        // Variables dinamicas de Geocatmin-Desktop
        public static string loginUser = "";

        //Coneccion SDE
        public static string SDE = @"scripts\bdgeocat_bdtecnica.sde";
        public static string SDE_bdgeocat = @"scripts\bdgeocat.sde";
        public static string SDE_huawei = @"scripts\bdgeocat_huawei.sde";

        // Ruta actual
        public static string currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        // Layers Geocatmin Desktop
        public static string layerHoja250K = "data_gis.gpo_hoj_hojas_250";
        public static string layerHoja100K = "data_gis.gpo_hoj_hojas_100";
        public static string layerHoja50K = "data_gis.gpo_hoj_hojas_50";
        public static string layerDpto = "data_gis.gpo_dep_departamento";
        public static string layerProv = "data_gis.gpo_dep_provincia";
        public static string layerDist = "data_gis.gpo_dep_distrito";

        // Ruta de la carpeta que contiene BASE, Carta-IGN y otros
        public static string fileLayers = @"\\srvfs01\bdgeocientifica$\DataIGN\Geocatmin_Pro";

        // Ruta de la carpeta que contiene las plantillas
        public static string fileTemplates = @"\\Srvfs01\bdgeocientifica$\DataIGN\Plantilla";

        // Nombres de Layers Base, Carta-ING y otros;
        public static string lyrCartaIGN = @"BASE\CartaIGN.lyrx";
        public static string lyrBase = @"BASE\Cartografía Base.lyrx";

        // Nombre de las Plantillas de Geocatmin-Desktop
        public static string template50k = @"Carta_IGN_50.mxd";
        public static string template100k = @"Carta_IGN_100.mxd";
        public static string template250k = @"Carta_IGN_250k_v1.mxd";

        // Zona UTM 
        public static string utm = "";

        // Extension para mapaFrame de Layout Geocatmin Desktop
        public static Envelope ExtentMain50k = null;
        public static Envelope ExtentMain100k = null;
        public static Envelope ExtentMain250k = null;
        public static Envelope ExtentSecond = null;

        // Query general de apoyo para Geocatmin Desktop
        public static string GeneralQuery= "";

        // Checked de apoyo
        public static string sheetChecked= "";

        // Nombre de capas Base
        public static string nameLyrCartaIGN = "Cartas IGN";
        public static string nameLyrCartografiaBase = "Cartografía Base";

        // Librerias python
        public static string scriptLibrerisPath = @"scripts\AutomapicExt_addin.py";
        public static string requirePath = @"scripts\require";

        // Ruta intérprete de Python de ArcGIS Pro
        public static string pythonExePath = @"C:\Program Files\ArcGIS\Pro\bin\Python\envs\arcgispro-py3\python3.exe";

        //Servicios de BD Geocientifica
        public static string serviceBdgeoModulos = "https://geocatminapp.ingemmet.gob.pe/bdgeocientifica/app/api/account/Listacriterio/1/AA";
        public static string serviceBdgeoFields = "https://geocatminapp.ingemmet.gob.pe/bdgeocientifica/app/api/account/Listacriterio/2/{0}";

        //Lista de elementos SQL BDGeocientifica
        public static List<string> operators = new List<string>(){"is equal", "is not equal to", "is greater than",
                                                    "is greater than or equal to", "is less than",
                                                    "is less than or equal to", "includes the values(s)",
                                                    "does not include the value(s)", "in null", "is not null",
                                                    "is above average", "is below average" };

        //Lista de nombres de capas BDGeocientifica
        public static string catastroMinero = "Catastro Minero";


        // Funciones Globales
        // - Funciones que devuelven resultados y que puedes ser usados en cualquier parte del proceso
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


    }

    // Clase para utilizar el Json de respuesta del servicio utilizado para BDGeocientifica
    public class Respuesta
    {
        //public string TipoRespuesta { get; set; }
        //public string ValorRespuesta { get; set; }
        public string Codigo { get; set; } // Retorna Modulo
        public string Descripcion { get; set; } // Retorna usuario.nombrecapa
        public string Adicional1 { get; set; } // Retorna Nombre del campo 
        public string Adicional2 { get; set; } // Retorna alias del campo
        public string Adicional3 { get; set; } // Retorna tipo de dato
        public string Adicional4 { get; set; } // Retorna orden (indice)
        public string Adicional5 { get; set; } // Retorna ruta en la gdb

    }
}
