using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Views.WPF.Views.Login;

namespace SigcatminProAddinUI.Views.ArgisPro.Views
{
    internal class LoginButton : Button
    {
        LoginView _loginWindow;
        protected override void OnClick()
        {
            if (_loginWindow == null)
            {
                _loginWindow = new LoginView();
                _loginWindow.Closed += (s, e) => _loginWindow = null;
                _loginWindow.Show();

            }
            else
            {
                _loginWindow.Focus();
            }
        }
    }
}
