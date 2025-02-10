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
        ValueTask<int> CountRecordsAsync(string type, string search);
        ValueTask<IEnumerable<DerechoMineroDto>> GetDerechosMinerosUniqueAsync(string code, int type);
        ValueTask<IEnumerable<CoordinateDto>> GetcoordinatesByCodeAsync(string code);
    }
}
