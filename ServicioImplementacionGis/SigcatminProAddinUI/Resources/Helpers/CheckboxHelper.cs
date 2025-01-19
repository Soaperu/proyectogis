using System.Windows;
using System.Windows.Controls;

namespace SigcatminProAddinUI.Resources.Helpers
{
    public static class CheckboxHelper
    {
        public static CheckBox GenerateChexbox(string text, bool isCheked, bool isEnabled = true)
        {
            var checkBox = new CheckBox
            {
                Content = text,
                Margin = new Thickness(2),
                Style = (Style)Application.Current.FindResource("Esri_CheckboxToggleSwitch"),

                FlowDirection = FlowDirection.RightToLeft,
                IsThreeState = true 
            };
            checkBox.IsChecked = isCheked; 
            checkBox.IsEnabled = isEnabled; 

            return checkBox;
        }
    }
}
