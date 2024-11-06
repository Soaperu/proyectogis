using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using CommonUtilities.LoginUtil;
using SigcatminProAddin.View.Constants;
using SigcatminProAddin.View.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SigcatminProAddin.View.Botones
{
    internal class LoginButton : Button
    {
        LoginWindow _loginWindow;
        protected override void OnClick()
        {
            if (IsActiveSession())
            {
                System.Windows.MessageBox.Show("Bienvenido");
                StatesUtil.ActivateState(UIState.IsLogged);
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

        private bool IsActiveSession()
        {
            bool activeSession = false;
            string encryptedData = SessionManager.LoadSession();
            if (encryptedData != null)
            {
                // Desencriptar las credenciales
                var (username, password, expiration) = CredentialManager.DecryptCredentials(encryptedData);

                if (DateTime.Now < expiration)
                {
                    activeSession = true;
                    // Aquí deberías pedir las credenciales al usuario nuevamente y generar un nuevo token
                }
            }
            return activeSession;

        }

    }
}
