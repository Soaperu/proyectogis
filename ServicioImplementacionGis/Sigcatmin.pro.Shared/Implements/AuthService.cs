using CredentialManagement;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Shared.Constants;
using Sigcatmin.pro.Application.Interfaces;
using System.Net;

namespace Sigcatmin.pro.Shared.Implements
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {

            IsLoggedIn = IsSessionActive();
        }
        private bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get => _isLoggedIn; private set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    SessionChanged?.Invoke();
                }
            }
        }
        public event Action SessionChanged;

        public void EndSession()
        {
            var credential = new Credential
            {
                Target = CredentialConstants.CredentialKey
            };
            credential.Delete();
        }
        public UserSessionDto GetSession()
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
            return new UserSessionDto();
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

        private bool IsSessionActive()
        {
            var credential = new Credential
            {
                Target = CredentialConstants.CredentialKey
            };
            return credential.Exists();
        }


    }
}
