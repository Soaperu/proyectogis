using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Dtos.Request
{
    public record struct LoginRequestDto(string UserName, string Password);
}
