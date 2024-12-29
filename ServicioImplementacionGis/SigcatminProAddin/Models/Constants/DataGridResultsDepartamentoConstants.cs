using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models.Constants
{
    internal class DataGridResultsDepartamentoConstants
    {
        public static class ColumNames
        {
            public const string Index = "INDEX";
            public const string Codigo = "CODIGO";
            public const string Departamento = "DPTO";
            public const string Capital = "CAP_DIST";
            public const string Ubigeo = "UBIGEO";
            
        }

        public static class Headers
        {
            public const string Index = "N°";
            public const string Codigo = "Código";
            public const string Departamento = "Departamento";
            public const string Capital = "Capital";
            public const string Ubigeo = "Ubigeo";
        }

        public static class Widths
        {
            public const int Index = 30;
            public const int Codigo = 50;
            public const int Departamento = 120;
            public const int Capital = 120;
            public const int Ubigeo = 100;
        }
    }
}
