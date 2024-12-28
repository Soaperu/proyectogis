using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models.Constants
{
    public static class DatagridResultConstants
    {
        public static class ColumNames
        {
            public const string Index = "INDEX";
            public const string Codigo = "CODIGO";
            public const string Nombre = "NOMBRE";
            public const string PeVigCat = "PE_VIGCAT";
            public const string Zona = "ZONA";
            public const string Tipo = "TIPO";
            public const string Estado = "ESTADO";
            public const string Naturaleza = "NATURALEZA";
            public const string Carta = "CARTA";
            public const string Hectarea = "HECTAREA";
        }

        public static class Headers
        {
            public const string Index = "N°";
            public const string Codigo = "Código";
            public const string Nombre = "Nombre";
            public const string PeVigCat = "Estado Graf";
            public const string Zona = "Zona";
            public const string Tipo = "Tipo";
            public const string Estado = "Estado";
            public const string Naturaleza = "Naturaleza";
            public const string Carta = "Carta";
            public const string Hectarea = "Hectárea";
        }

        public static class Widths
        {
            public const int Index = 30;
            public const int Codigo = 100;
            public const int Nombre = 120;
            public const int PeVigCat = 70;
            public const int Zona = 50;
            public const int Tipo = 60;
            public const int Estado = 100;
            public const int Naturaleza = 80;
            public const int Carta = 80;
            public const int Hectarea = 80;
        }

        public static class ColumNamesCarta
        {
            public const string CdHoja = "CODIGO";
            public const string NmHoja = "NOMBRE";
            public const string Zona = "ZONA";
            public const string EsteMin = "XMIN";
            public const string NorteMin = "YMIN";
            public const string EsteMax = "XMAX";
            public const string NorteMax = "YMAX";
            //public const string Naturaleza = "NATURALEZA";
            //public const string Carta = "CARTA";
            //public const string Hectarea = "HECTAREA";
        }

        public static class HeadersCarta
        {
            public const string CdHoja = "Hoja";
            public const string NmHoja = "Nombre Carta";
            public const string Zona = "Zona";
            public const string EsteMin = "Este Min.";
            public const string NorteMin = "Norte Min.";
            public const string EsteMax = "Este Max.";
            public const string NorteMax = "Norte Max.";
            //public const string Naturaleza = "Naturaleza";
            //public const string Carta = "Carta";
            //public const string Hectarea = "Hectárea";
        }

        public static class WidthsCarta
        {
            public const int CdHoja = 30;
            public const int NmHoja = 100;
            public const int Zona = 120;
            public const int EsteMin = 70;
            public const int NorteMin = 60;
            public const int EsteMax = 50;
            public const int NorteMax = 100;
            //public const int Naturaleza = 80;
            //public const int Carta = 80;
            //public const int Hectarea = 80;
        }


    }
}
