using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Contracts.Requests
{
    public record struct LoginRequest(string UserName, string Password);
}
