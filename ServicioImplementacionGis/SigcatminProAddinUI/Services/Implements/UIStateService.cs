using ArcGIS.Desktop.Framework;
using Sigcatmin.pro.Application.Interfaces;
using SigcatminProAddinUI.Resources.Constants;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Services.Implements
{
    public class UIStateService : IUIStateService
    {
        private readonly IAuthService _authService;
        public UIStateService(IAuthService authService)
        {
            _authService = authService;
        }
        public void ActivateState(string stateId)
        {
            FrameworkApplication.State.Activate(stateId);
        }

        public void DeactivateState(string stateId)
        {
            FrameworkApplication.State.Deactivate(stateId);
        }

        public void InitializeStates()
        {
            // Verifica si hay sesión activa al iniciar
            if (_authService.IsLoggedIn)
            {
                ActivateState(UIStateConstants.IsLoggedIn);
            }
            else
            {
                DeactivateState(UIStateConstants.IsLoggedIn);
            }
        }
    }
}
