using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Primitives;

namespace SigcatminProAddinUI.Resources.Helpers
{
    public static class CheckboxHelper
    {
        public static CheckBox GenerateChexbox(string text, bool isCheked)
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
            checkBox.IsEnabled = false; 

            return checkBox;


        }
    }
}
