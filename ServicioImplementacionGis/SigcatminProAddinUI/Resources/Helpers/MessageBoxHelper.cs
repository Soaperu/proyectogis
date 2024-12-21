using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Resources.Helpers
{
    public static class MessageBoxHelper
    {
        public static void ShowInfo(string message, string title = "Información")
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public static void ShowWarning(string message, string title = "Advertencia")
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        public static void ShowError(string message, string title = "Error")
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static bool ShowConfirmation(string message, string title = "Confirmación")
        {
            var result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            return result == System.Windows.MessageBoxResult.Yes;
        }
    }
}
