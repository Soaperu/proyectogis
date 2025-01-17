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
        public decimal Norte { get; set; }
        public decimal Este { get; set; }
        public decimal NorteEquivalente { get; set; }
        public decimal EsteEquivalente { get; set; }
        public string CodigoDatum { get; set; }
    }
}
