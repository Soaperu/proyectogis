using CredentialManagement;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Shared.Constants;
using Sigcatmin.prop.Domain.Entities;
using Sigcatmin.prop.Domain.Interfaces.Services;

namespace Sigcatmin.pro.Shared.Services
{
    public class UserSessionService : IUserSessionService
    {
        //private const string ApplicationDataFolder = "MyApp";
        //private readonly string _appDataPath;
        //private readonly string _sessionFilePath;
        //public UserSessionService()
        //{
        //    _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationDataFolder);
        //    _sessionFilePath = Path.Combine(_appDataPath, "session.dat");
        //}
        public UserSessionDto GetUserSession()
        {
            var credential = new Credential
            {
                Target = CredentialConstants.CredentialKey
            };

            // Cargar las credenciales del Credential Manager
            if (credential.Load())
            {
                return new UserSessionDto { Password = credential.Password, UserName = credential.Username };
            }
            return new UserSessionDto() ;
        }
    }
}
