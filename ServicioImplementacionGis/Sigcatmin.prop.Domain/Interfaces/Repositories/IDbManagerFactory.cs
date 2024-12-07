using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.prop.Domain.Interfaces.Repositories
{
    public  interface IDbManagerFactory
    {
        IDbManager CreateDbManager(string connectionString);
    }
}
