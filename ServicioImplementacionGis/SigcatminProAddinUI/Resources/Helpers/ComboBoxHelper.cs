using System.Collections.Generic;
using System.Windows.Controls;
using SigcatminProAddinUI.Models;

namespace SigcatminProAddinUI.Resources.Helpers
{
    internal class ComboBoxHelper
    {
        public static void LoadComboBox<TId>(ComboBox comboBox, List<ComboBoxItemGeneric<TId>> items, int selectedIndex = 0)
        {
            comboBox.ItemsSource = items;
            comboBox.DisplayMemberPath = "DisplayName";
            comboBox.SelectedValuePath = "Id";

            comboBox.SelectedIndex = selectedIndex;
        }
    }
}
