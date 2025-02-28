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
        public double Norte { get; set; }
        public double Este { get; set; }
        public double NorteEquivalente { get; set; }
        public double EsteEquivalente { get; set; }
        public string CodigoDatum { get; set; }
    }
}
