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
using SigcatminProAddin.Models.Constants;
using SigcatminProAddin.View.Login;
using System;
using System.Data;

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

                // DateTime.Now < expiration se usa para considerar tiempo de sesión, abajo se está usando dia de sesión solamente
                if (DateTime.Now.Date <= expiration.Date)
                {
                    try
                    {
                        activeSession = true;
                        // Aquí puedes agregar lógica para renovar credenciales si es necesario
                        AppConfig.userName = username;
                        AppConfig.password = password;
                        DatabaseHandler _dataBaseHandler = new DatabaseHandler();
                        var infoUser = _dataBaseHandler.VerifyUser(username, password);
                        DataRow firstRow = infoUser.Rows[0];
                        AppConfig.fullUserName = firstRow["USERNAME"].ToString();
                        //System.Windows.MessageBox.Show("Bienvenido", "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                        StatesUtil.ActivateState(UIState.IsLogged);
                    }
                    catch{
                        SessionManager.EndSession();
                    }
                    
                }
            }

            return activeSession;
        }

    }
}
