using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Services.Interfaces
{
    public interface INotifyComboBoxService
    {
        public event Action<string> ComboBoxAChanged;

        void NotifyComboBoxAChanged(string value);

    }
}
