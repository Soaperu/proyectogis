using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Domain.Dtos
{
    public class  DerechoMineroDto 
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string PeVigCat { get; set; }
        public string Zona { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Naturaleza { get; set; }
        public string Carta { get; set; }
        public string Hectarea { get; set; }
    }
}
