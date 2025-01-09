using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Domain.Dtos
{
    public class CoordinateDto
    {
        public int Vertice { get; set; }
        public decimal NorteWGS84 { get; set; }
        public decimal EsteWGS84 { get; set; }
        public decimal NortePSAD56 { get; set; }
        public decimal EstePSAD56 { get; set; }
    }
}
