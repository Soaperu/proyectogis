using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration
{
    public interface IDbManagerFactory
    {
        IDbManager CreateDbManager(string connectionString);
    }
}
