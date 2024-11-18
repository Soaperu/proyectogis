using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using SigcatminProAddin.View.Contenedor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class SelectorCategorias : ComboBox
    {
        public SelectorModulos ModulosComboBox = new SelectorModulos();
        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public SelectorCategorias()
        {
            UpdateCombo();
        }

        public SelectorCategorias(SelectorModulos modulosComboBox = null)
        {
            ModulosComboBox = modulosComboBox; // Asignar la referencia al ComboBox de módulos (opcional)
            UpdateCombo();
        }

        /// <summary>
        /// Updates the combo box with all the items.
        /// </summary>

        private void UpdateCombo()
        {
            Clear();
            // TODO – customize this method to populate the combobox with your desired items  
            foreach (var categoria in ModuleConfiguration.CategoriasModulos)
            {
                Add(new ComboBoxItem(categoria.Categoria));
            }
            SelectedItem = ItemCollection.FirstOrDefault();
            Enabled = true;

        }

        /// <summary>
        /// The on comboBox selection change event. 
        /// </summary>
        /// <param name="item">The newly selected combo box item</param>
        protected override void OnSelectionChange(ComboBoxItem item)
        {

            base.OnSelectionChange(item);

            if (item != null && !string.IsNullOrEmpty(item.Text) && ModulosComboBox != null)
            {
                // Actualiza el ComboBox de módulos
                ModulosComboBox.UpdateModules(item.Text);
            }
        }
        public void SelectionChanged(string text)
        {

        }

    }
    internal class SelectorModulos : ComboBox
    {
        public SelectorModulos()
        {
            // Inicialmente vacío, será actualizado según la categoría seleccionada.
            Clear();
        }

        public void UpdateModules(string categoria)
        {
            Clear();
            var modulos = ModuleConfiguration.CategoriasModulos.FirstOrDefault(c => c.Categoria == categoria)?.Modulos;
            if (modulos != null)
            {
                foreach (var modulo in modulos)
                {
                    Add(new ComboBoxItem(modulo.Nombre) );
                }
                SelectedItem = ItemCollection.FirstOrDefault();
            }
        }
    }

    internal class ConfirmationModule : Button
    {
        public SelectorModulos ModulosComboBox { get; set; }
        public System.Windows.Controls.Frame Contenedor { get; set; }

        protected override void OnClick()
        {
            base.OnClick();

            if (ModulosComboBox?.SelectedItem is ComboBoxItem selectedItem && !string.IsNullOrEmpty(selectedItem.Text))
            {
                // Buscar el módulo seleccionado en la configuración
                var moduloSeleccionado = ModuleConfiguration.CategoriasModulos
                    .SelectMany(c => c.Modulos)
                    .FirstOrDefault(m => m.Nombre == selectedItem.Text);

                if (moduloSeleccionado != null && Contenedor != null)
                {
                    // Cargar la página asociada al módulo en el contenedor
                    var page = PageManager.GetOrCreatePage(moduloSeleccionado.Pagina);
                    Contenedor.Navigate(page);
                }
            }
        }
    }
}
