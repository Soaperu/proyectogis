using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.prop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IAuthService
    {
        bool IsLoggedIn { get; }
        event Action SessionChanged;
        void SaveSession(string username, string password);
        UserSessionDto GetSession();
        void EndSession();

    }
}
