using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.Models
{
    public class MantenimientoAreaRModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoEstado { get; set; }
        
        public bool IsProcess { get; set; }
    }
}
