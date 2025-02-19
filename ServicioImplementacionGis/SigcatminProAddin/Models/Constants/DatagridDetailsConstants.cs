using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models.Constants
{
    public static class DatagridDetailsConstants
    {
        public static class RawColumNames
        {
            public const string Vertice = "CD_NUMVER";
            public const string CoorEste = "CD_COREST";
            public const string CoorNorte = "CD_CORNOR";
            public const string CoorEsteE = "CD_COREST_E";
            public const string CoorNorteE = "CD_CORNOR_E";

           

        }
        public static class ColumnNames
        {
            public const string Vertice = "VERTICE";
            public const string Este = "ESTE";
            public const string Norte = "NORTE";

            

        }

        public static class Headers
        {
            public const string Vertice = "Vértice";
            public const string Este = "Este";
            public const string Norte = "Norte";
        }

        public static class Widths
        {
            public const double VerticeWidth = 40;
            public const double StarWidthRatio = 1;
        }
    }
}
