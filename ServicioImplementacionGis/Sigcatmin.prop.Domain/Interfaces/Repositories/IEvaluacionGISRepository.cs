using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Domain.Dtos; 

namespace Sigcatmin.pro.Domain.Interfaces.Repositories
{
    public interface IEvaluacionGISRepository
    {
        ValueTask<int> CountRecords(string type, string search);
        ValueTask<IEnumerable<DerechoMineroDto>> GetDerechosMinerosUnique(string code, int type);
        ValueTask<IEnumerable<CoordinateDto>> GetcoordinatesByCode(string code);
    }
}
