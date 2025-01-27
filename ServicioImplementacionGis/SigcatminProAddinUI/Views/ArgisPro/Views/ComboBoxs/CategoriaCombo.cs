using System.Linq;
using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Services.Interfaces;
using SigcatminProAddinUI.Views.WPF.Views.Modulos;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class CategoriaCombo : ComboBox
    {
        private readonly IModuleFactory _moduleFactory;
        private readonly ModuloCombo _moduleComboBox;
        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public CategoriaCombo(ModuloCombo moduloCombo)
        {
            _moduleFactory = Program.GetService<IModuleFactory>();
            _moduleComboBox = moduloCombo;
            _moduleFactory.RegisterModule("Evaluacion", new Models.ModuleModel { Name = "Evalucion DM",Module = typeof(EvaluacionDMView) });
            _moduleFactory.RegisterModule("Evaluacion 2", new Models.ModuleModel { Name = "Evalucion DM", Module = typeof(EvaluacionDMView) });

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

                var categories = _moduleFactory.Categories;
                //Add 6 items to the combobox
                foreach (var category in categories)
                {        
                    Add(new ComboBoxItem(category.Name));
                }
                _isInitialized = true;
            }


            Enabled = true; //enables the ComboBox
            SelectedItem = ItemCollection.FirstOrDefault(); //set the default item in the comboBox

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
            // 
            _moduleComboBox.UpdateModules(item.Text);
        }

    }
}
