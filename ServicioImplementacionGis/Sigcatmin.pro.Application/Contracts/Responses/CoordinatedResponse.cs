using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Contracts.Responses
{
    public record struct CoordinatedResponse(int Vertice,
        double Norte,
        double Este,
        double NorteEquivalente,
        double EsteEquivalente,
        string CodigoDatum);

}
