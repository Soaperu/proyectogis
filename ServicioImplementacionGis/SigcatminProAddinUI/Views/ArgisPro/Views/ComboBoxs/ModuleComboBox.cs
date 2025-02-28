using System.Linq;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Internal.KnowledgeGraph.FFP;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class ModuleComboBox : ComboBox
    {
        private readonly IModuleFactory _moduleFactory;
        private readonly INotifyComboBoxService _notifyComboBoxService;
        public static ModuleComboBox Intance;

        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public ModuleComboBox()
        {
            _moduleFactory = Program.GetService<IModuleFactory>();
            _notifyComboBoxService = Program.GetService<INotifyComboBoxService>();
            _notifyComboBoxService.ComboBoxAChanged += UpdateModules;
            Intance = this;
            UpdateCombo();
        }

        /// <summary>
        /// Updates the combo box with all the items.
        /// </summary>

        private void UpdateCombo()
        {
            // TODO – customize this method to populate the combobox with your desired items  
            Enabled = false; //enables the ComboBo
            _isInitialized = true;
        }

        public void UpdateModules(string typeName)
        {
            if (_isInitialized)
            {
                Clear();
                var categories = _moduleFactory.Categories.FirstOrDefault(x => x.Name == typeName);
                var moduleNames = categories.Modules?.Select(x => x.Name).ToList();

                Add(new ComboBoxItem("seleccione"));
                if (moduleNames is null) return;
                //Add 6 items to the combobox
                foreach (var moduleName in moduleNames)
                 {
                     Add(new ComboBoxItem(moduleName));
                 } 
                SelectedItem = ItemCollection.FirstOrDefault();
                Enabled = true;
            }

        }
        protected override void OnUpdate()
        {
            // Habilita o deshabilita el ComboBox dependiendo de si hay elementos
            Enabled = ItemCollection.Any();
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
