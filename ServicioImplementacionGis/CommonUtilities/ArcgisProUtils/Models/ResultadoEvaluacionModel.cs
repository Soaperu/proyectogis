using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.ArcgisProUtils.Models
{
    public class ResultadoEvaluacionModel
    {
        public bool isCompleted = false;
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string distanciaFrontera { get; set; }
        public string zonaReservada { get; set; }
        public string zonaUrbana { get; set; }

        public Dictionary<string, List<ResultadoEval>> ResultadosCriterio = new Dictionary<string, List<ResultadoEval>>();
    }

    public class ResultadoEval
    {
        public string Contador { get; set; }
        public string Concesion { get; set; }
        public string TipoEx { get; set; }
        public string CodigoU { get; set; }
        public string Estado { get; set; }
        public string Eval { get; set; }
    }
}
