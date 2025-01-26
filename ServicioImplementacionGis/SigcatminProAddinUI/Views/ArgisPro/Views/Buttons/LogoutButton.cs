using ArcGIS.Desktop.Framework.Contracts;
using Sigcatmin.pro.Application.Interfaces;
using SigcatminProAddinUI.Resources.Constants;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.Buttons
{
    internal class LogoutButton : Button
    {
        private readonly IAuthService _authService;
        private readonly IUIStateService _IUIStateService;

        public LogoutButton()
        {
            _authService = Program.GetService<IAuthService>();
            _IUIStateService = Program.GetService<IUIStateService>();
        }
        protected override void OnClick()
        {
            _authService.EndSession();
            _IUIStateService.DeactivateState(UIStateConstants.IsLoggedIn);

        }
    }
}
