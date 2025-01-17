using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Dtos.Response
{
    public record struct CoordinatedResponseDto(int Vertice,
        decimal Norte,
        decimal Este,
        decimal NorteEquivalente,
        decimal EsteEquivalente, 
        string CodigoDatum);

}
