using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigcatminProAddinUI.Resources.Constants;

namespace SigcatminProAddinUI.Services.Interfaces
{
    public interface IUIStateService
    {
        void ActivateState(string stateId);
        void DeactivateState(string stateId);
        void InitializeStates();

    }
}
