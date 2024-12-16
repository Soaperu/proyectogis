using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models.Constants
{
    class DataGridSelectedPolygonsConstants
    {
        public static class ColumnNames
        {
            public const string Index = "INDEX";
            public const string Numero = "NUMERO";
            public const string Codigo = "CODIGO";
            public const string Nombre = "NOMBRE";
            public const string Area = "AREA";

        }

        public static class Headers
        {
            public const string Index = "N°";
            public const string Numero = "Número";
            public const string Codigo = "Código";
            public const string Nombre = "Nombre";
            public const string Area = "Área";
        }

        public static class Widths
        {
            public const int Index = 30;
            public const int Numero = 50;
            public const int Codigo = 100;
            public const int Nombre = 120;
            public const int Area = 80;
        }
    }
}
