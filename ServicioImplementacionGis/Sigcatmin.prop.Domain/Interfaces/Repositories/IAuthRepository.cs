using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.prop.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        bool Authenticate(string user, string password);
    }
}
