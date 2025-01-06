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

            public const string CodigoAR = "CG_CODIGO";
            public const string NombreAR = "PE_NOMARE";
            public const string ZonaAR = "ZA_ZONA";
            public const string DescriAR = "PA_DESCRI";
            public const string DestipAR = "TN_DESTIP";
            public const string DescatAR = "CA_DESCAT";
            public const string SituexAR = "SE_SITUEX";


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

            public const string CodigoAR = "Código";
            public const string NombreAR = "Nombre";
            public const string ZonaAR = "Zona";
            public const string DescriAR = "Descripción";
            public const string DestipAR = "Tipo";
            public const string DescatAR = "Categoria";
            public const string SituexAR = "Situación";



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

            public const int CodigoAR = 80;
            public const int NombreAR = 120;
            public const int ZonaAR = 30;
            public const int DescriAR = 150;
            public const int DestipAR = 100;
            public const int DescatAR = 60;
            public const int SituexAR = 50;

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
            public const int CdHoja = 50;
            public const int NmHoja = 100;
            public const int Zona = 40;
            public const int EsteMin = 70;
            public const int NorteMin = 70;
            public const int EsteMax = 70;
            public const int NorteMax = 70;
            //public const int Naturaleza = 80;
            //public const int Carta = 80;
            //public const int Hectarea = 80;
        }


    }
}
