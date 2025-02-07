using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models.Constants
{
    public static class DatagridResultConstantsDM
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
            public const int Zona = 40;
            public const int EsteMin = 70;
            public const int NorteMin = 70;
            public const int EsteMax = 70;
            public const int NorteMax = 70;
        }

        //
        public static class ColumNamesRenuncia
        {
            public const string Nombre = "NOMBRE";
            public const string Codigo = "CODIGO";
            public const string Hectarea = "HECTAREA";
            public const string TipoArea = "TIPO_AREA";
            public const string FechaReg = "FECHA_REG";
            public const int Naturaleza = 80;
            //public const int Carta = 80;
            //public const int Hectarea = 80;
        }

        public static class ColumNamesEstadisticasAR
        {
            public const string Select = "SELEC";
            public const string TpReset = "TP_RESE";
            public const string NmTprese = "NM_TPRESE";
            public const string Area = "AREA";
            public const string Canti = "CANTI";
            public const string AreaNeta = "AREA_NETA";
            public const string Porcen = "PORCEN";
        }

        public static class HeadersEstadisticasAR
        {
            public const string Select = "Select";
            public const string TpReset = "TP_RESE";
            public const string NmTprese = "NM_TPRESE";
            public const string Area = "AREA";
            public const string Canti = "CANTI";
            public const string AreaNeta = "AREA_NETA";
            public const string Porcen = "PORCEN (%)";
        }

        public static class WidthsEstadisticasAR
        {
            public const int Select = 30;
            public const int TpReset = 150;
            public const int NmTprese = 50;
            public const int Area = 50;
            public const int Canti = 80;
            public const int AreaNeta = 80;
            public const int Porcen = 80;

        }

        public static class HeadersRenuncia
        {
            public const string Nombre = "Nombre";
            public const string Codigo = "Código";
            public const string Hectarea = "Hectárea";
            public const string TipoArea = "Tipo Area";
            public const string FechaReg = "Fecha Reg.";
        }

        public static class WidthsRenuncia
        {
            public const int Nombre = 200;
            public const int Codigo = 100;
            public const int Hectarea = 100;
            public const int TipoArea = 100;
            public const int FechaReg = 150;
        }

        //Mantenimiento de Areas Restringidas
        public static class ColumNamesMantoAR
        {
            public const string Id = "ID";
            public const string Codigo = "CODIGO";
            public const string Zona = "ZONA";
            public const string Clase = "CLASE";
            public const string Archivo = "ARCHIVO";
            public const string ModReg = "MODREG";
            public const string Usuario = "USUARIO";
            public const string FecReg = "FECREG";
            public const string CodEst = "CODEST";
            public const string Mineria = "MINERIA";
            public const string Proc = "PROC";
        }
        public static class HeadersMantoAR
        {
            public const string Id = "ID";
            public const string Codigo = "CODIGO";
            public const string Zona = "ZONA";
            public const string Clase = "CLASE";
            public const string Archivo = "ARCHIVO";
            public const string ModReg = "MODREG";
            public const string Usuario = "USUARIO";
            public const string FecReg = "FECREG";
            public const string CodEst = "CODEST";
            public const string Mineria = "MINERIA";
            public const string Proc = "PROC";
        }

        public static class WidthsMantoAR
        {
            public const int Id = 40;
            public const int Codigo = 70;
            public const int Zona = 50;
            public const int Clase = 50;
            public const int Archivo = 100;
            public const int ModReg = 100;
            public const int Usuario = 100;
            public const int FecReg = 80;
            public const int CodEst = 80;
            public const int Mineria = 100;
            public const int Proc = 50;
        }

        public static class ColumNamesClassTemporales
        {
            public const string Total = "TOTAL";
            public const string PSad17 = "PSAD_17";
            public const string PSad18 = "PSAD_18";
            public const string PSad19 = "PSAD_19";
            public const string Wgs17= "WGS_17";
            public const string Wgs18 = "WGS_18";
            public const string Wgs19 = "WGS_19";
            public const string G56 = "G56";
            public const string G84 = "G84";
        }

        public static class HeadersClassTemporales
        {
            public const string Total = "TOTAL";
            public const string PSad17 = "PSAD_17";
            public const string PSad18 = "PSAD_18";
            public const string PSad19 = "PSAD_19";
            public const string Wgs17 = "WGS_17";
            public const string Wgs18 = "WGS_18";
            public const string Wgs19 = "WGS_19";
            public const string G56 = "G56";
            public const string G84 = "G84";
        }

        public static class WidthsClassTemporales
        {
            public const int Total = 50;
            public const int PSad17 = 60;
            public const int PSad18 = 60;
            public const int PSad19 = 60;
            public const int Wgs17 = 60;
            public const int Wgs18 = 60;
            public const int Wgs19 = 60;
            public const int G56 = 60;
            public const int G84 = 60;
        }

        public static class ColumNamesReseDifReg
        {
            public const string Codigo = "CODIGO";
            public const string Clase = "CLASE";
            public const string RegDB = "REGISTROS EN DB";
            public const string RegGDB = "REGISTROS EN GDB";
            public const string Observacion = "OBSERVACION";
        }

        public static class HeadersReseDifReg
        {
            public const string Codigo = "CODIGO";
            public const string Clase = "CLASE";
            public const string RegDB = "REGISTROS EN DB";
            public const string RegGDB = "REGISTROS EN GDB";
            public const string Observacion = "OBSERVACION";
        }

        public static class WidthsReseDifReg
        {
            public const int Codigo = 100;
            public const int Clase = 80;
            public const int RegDB = 80;
            public const int RegGDB = 80;
            public const int Observacion = 300;
            
        }





    }
}
