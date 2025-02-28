using System.Linq;
using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Services.Interfaces;
using SigcatminProAddinUI.Views.WPF.Views.Modulos;
using SigcatminProAddinUI.Models;
using System.Windows.Documents;
using ArcGIS.Desktop.Internal.KnowledgeGraph.FFP;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class CategoryComboBox : ComboBox
    {
        private readonly IModuleFactory _moduleFactory;
        private readonly INotifyComboBoxService _notifyComboBoxService;
        public static CategoryComboBox Intance;

        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public CategoryComboBox()
        {
            _moduleFactory = Program.GetService<IModuleFactory>();
            _notifyComboBoxService = Program.GetService<INotifyComboBoxService>();
            Intance = this;

            LoadCategories();
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

                Add(new ComboBoxItem("seleccione"));
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
            base.OnSelectionChange(item);
            if (item == null)
                return;

            if (string.IsNullOrEmpty(item.Text))
                return;
           
            // TODO  Code behavior when selection changes.
            // 
            _notifyComboBoxService.NotifyComboBoxAChanged(item.Text);
        }

        private void LoadCategories()
        {
            _moduleFactory.RegisterModule("Consultas");
            _moduleFactory.RegisterModule("Planos");
            _moduleFactory.RegisterModule("Evaluación", new ModuleModel { Name = "Evalucion DM", Module = typeof(EvaluacionDMView) });
            _moduleFactory.RegisterModule("Evaluación", new ModuleModel { Name = "Carta Nacional", Module = typeof(CartaNacionalView) });
        }

    }
}
