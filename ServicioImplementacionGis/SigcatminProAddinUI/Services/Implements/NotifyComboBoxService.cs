using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Services.Implements
{
    public class NotifyComboBoxService : INotifyComboBoxService
    {
        public event Action<string> ComboBoxAChanged;

        public void NotifyComboBoxAChanged(string value)
        {
            ComboBoxAChanged?.Invoke(value);
        }
    }
}
