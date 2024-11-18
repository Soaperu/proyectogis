using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

using CommonUtilities.LoginUtil;
using DatabaseConnector;
using SigcatminProAddin.View.Constants;
using SigcatminProAddin.View.Login;
using System;

namespace SigcatminProAddin.View.Botones
{
    internal class LoginButton : Button
    {
        LoginWindow _loginWindow;
        protected override void OnClick()
        {
            if (IsActiveSession())
            {
                //System.Windows.MessageBox.Show("Bienvenido", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                //StatesUtil.ActivateState(UIState.IsLogged);
                return;
            }

            if (_loginWindow == null)
            {
                _loginWindow = new LoginWindow();
                _loginWindow.Closed += (s, e) => _loginWindow = null;
                _loginWindow.Show();

            }
            else
            {
                _loginWindow.Focus();
            }
        }

        // Método de instancia para validar la sesión activa
        private bool IsActiveSession()
        {
            return IsActiveSessionStatic();
        }

        // Método estático para validar la sesión activa desde otros contextos
        public static bool IsActiveSessionStatic()
        {
            bool activeSession = false;
            string encryptedData = SessionManager.LoadSession();

            if (!string.IsNullOrEmpty(encryptedData))
            {
                var (username, password, expiration) = CredentialManager.DecryptCredentials(encryptedData);

                if (DateTime.Now < expiration)
                {
                    activeSession = true;
                    // Aquí puedes agregar lógica para renovar credenciales si es necesario
                    AppConfig.userName = username;
                    AppConfig.password = password;
                    //System.Windows.MessageBox.Show("Bienvenido", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                    StatesUtil.ActivateState(UIState.IsLogged);
                }
            }

            return activeSession;
        }

    }
}
