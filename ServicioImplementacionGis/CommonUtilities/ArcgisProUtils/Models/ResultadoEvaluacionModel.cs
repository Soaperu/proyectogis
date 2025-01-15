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
        public string codigo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string distanciaFrontera { get; set; } = string.Empty;
        public string listaCaramUrbana { get; set; } = string.Empty;
        public string listaCaramReservada { get; set; } = string.Empty;
        public string listaCatastroforestal { get; set; } = string.Empty;
        public string areaDisponible { get;set; } = string.Empty;
        public List<ResultadoEval> ListaResultadosCriterio { get; set; } = new List<ResultadoEval>();

        public Dictionary<string, List<ResultadoEval>> ResultadosCriterio = new Dictionary<string, List<ResultadoEval>>();

        public List<ResultadoEval> FiltrarPorEval(string eval)
        {
            return ListaResultadosCriterio
                   .Where(r => r.Eval.Equals(eval, StringComparison.OrdinalIgnoreCase))
                   .ToList();
        }

        public bool EliminarporCodigoU(string codigoU)
        {
            var resultado = ListaResultadosCriterio.FirstOrDefault(r => r.CodigoU.Equals(codigoU, StringComparison.OrdinalIgnoreCase));
            if (resultado != null)
            {
                ListaResultadosCriterio.Remove(resultado);
                return true;
            }
            return false;
        }

        public bool ModificarEvaluacion(string codigoU, string eval)
        {
            var resultado = ListaResultadosCriterio.FirstOrDefault(r => r.CodigoU.Equals(codigoU, StringComparison.OrdinalIgnoreCase));
            if (resultado != null)
            {
                resultado.Eval = eval;
                return true;
            }
            return false;
        }
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
