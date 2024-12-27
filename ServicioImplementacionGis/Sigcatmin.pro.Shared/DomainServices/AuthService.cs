using CredentialManagement;
using Sigcatmin.pro.Application.Dtos;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Shared.Constants;
using Sigcatmin.prop.Domain.Entities;
using Sigcatmin.prop.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Shared.DomainServices
{
    public class AuthService : IAuthService
    {
        //private const string CredentialKey = "MyAppCredential";
        //private const string ApplicationDataFolder = "MyApp";
        //private readonly string _appDataPath;
        //private readonly string _sessionFilePath;
        //public AuthService()
        //{
        //    _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationDataFolder);
        //    _sessionFilePath = Path.Combine(_appDataPath, "session.dat");
        //}

        //public void SaveSession(string encryptedCredentials)
        //{
        //    if (!Directory.Exists(_appDataPath))
        //    {
        //        Directory.CreateDirectory(_appDataPath);
        //    }
        //    File.WriteAllText(_sessionFilePath, encryptedCredentials); // Guardar el token cifrado
        //}

        //public string? LoadSession()
        //{
        //    if (File.Exists(_sessionFilePath))
        //    {
        //        return File.ReadAllText(_sessionFilePath); // Leer el token cifrado
        //    }
        //    return null;
        //}

        //public void EndSession()
        //{
        //    if (File.Exists(_sessionFilePath))
        //    {
        //        File.Delete(_sessionFilePath); // Eliminar el archivo de sesión
        //    }
        //}
        public void EndSession()
        {
            throw new NotImplementedException();
        }

        public UserSessionDto? GetSession()
        {
            var credential = new Credential
            {
                Target = CredentialConstants.CredentialKey
            };

            // Cargar las credenciales del Credential Manager
            if (credential.Load())
            {
                return new UserSessionDto(credential.Username, credential.Password);
            }
            return null;
        }

        public void SaveSession(string username, string password)
        {
            var credential = new Credential
            {
                Target = CredentialConstants.CredentialKey,
                Username = username,
                Password = password,
                PersistanceType = PersistanceType.LocalComputer  // Puedes usar "Session" si no quieres que persista entre reinicios
            };

            credential.Save();
        }

   
    }
}
