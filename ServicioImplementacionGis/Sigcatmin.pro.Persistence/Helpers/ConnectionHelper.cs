using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Persistence.Helpers
{
    public static class ConnectionHelper
    {
        public static string BuildConnectionString(string connection, string user, string password)
        {
            return $"{connection}; User Id={user}; Password={password};";
        }
    }
}
