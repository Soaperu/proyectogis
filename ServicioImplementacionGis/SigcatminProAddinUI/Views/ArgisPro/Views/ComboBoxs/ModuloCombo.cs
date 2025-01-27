using System.Linq;
using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class ModuloCombo : ComboBox
    {
        private readonly IModuleFactory _moduleFactory;

        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public ModuloCombo()
        {
            _moduleFactory = Program.GetService<IModuleFactory>();
            UpdateCombo();
        }

        /// <summary>
        /// Updates the combo box with all the items.
        /// </summary>

        private void UpdateCombo()
        {
            // TODO – customize this method to populate the combobox with your desired items  
            if (_isInitialized)
                SelectedItem = ItemCollection.FirstOrDefault(); //set the default item in the comboBox


            if (!_isInitialized)
            {
                Clear();

                //Add 6 items to the combobox
                for (int i = 0; i < 6; i++)
                {
                    string name = string.Format("Item {0}", i);
                    Add(new ComboBoxItem(name));
                }
                _isInitialized = true;
            }


            Enabled = true; //enables the ComboBox
            SelectedItem = ItemCollection.FirstOrDefault(); //set the default item in the comboBox

        }

        public void UpdateModules(string typeName)
        {
            if (_isInitialized)
            {
                Clear();
                var categories = _moduleFactory.Categories.FirstOrDefault(x => x.Name == typeName);

                //Add 6 items to the combobox
                foreach (var module in categories.Modules)
                {
                    Add(new ComboBoxItem(module.Name));
                }
            }

        }

        /// <summary>
        /// The on comboBox selection change event. 
        /// </summary>
        /// <param name="item">The newly selected combo box item</param>
        protected override void OnSelectionChange(ComboBoxItem item)
        {

            if (item == null)
                return;

            if (string.IsNullOrEmpty(item.Text))
                return;

            

            // TODO  Code behavior when selection changes.    
        }

    }
}
