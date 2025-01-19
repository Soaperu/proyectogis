using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Dtos.Response
{
    public record struct CoordinatedResponseDto(int Vertice,
        double Norte,
        double Este,
        double NorteEquivalente,
        double EsteEquivalente, 
        string CodigoDatum);

}
