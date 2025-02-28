using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Resourecs.Constants
{
    public static class ErrorMessage
    {
        public const string EmptySearchValue = "Por favor ingrese un valor para iniciar la búsqueda.";
        public const string NoRecordsFound = "No existe ningún registro con esta consulta: {0}";
        public const string TooManyMatches = "Sea Ud. más específico en la consulta, hay {0} registro(s).";
        public const string UnexpectedError = "Se produjo un error inesperado, vuelva a intentarlo.";
    }
}
