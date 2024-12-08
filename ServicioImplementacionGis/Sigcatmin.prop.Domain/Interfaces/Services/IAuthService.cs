using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.prop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.prop.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        void SaveSession(string username, string password);
        UserSessionDto? GetSession();
        void EndSession();

    }
}
