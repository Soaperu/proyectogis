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
using System.Windows;
//using System.Windows.Controls;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Represents the ComboBox
    /// </summary>
    internal class SelectorCategorias : ComboBox
    {
        public SelectorModulos ModulosComboBox { get; set; }

        private bool _isInitialized;

        /// <summary>
        /// Combo Box constructor
        /// </summary>
        public SelectorCategorias()
        {
            UpdateCombo();
        }

        public SelectorCategorias(SelectorModulos modulosComboBox)
        {
            ModulosComboBox = modulosComboBox;
            UpdateCombo();
        }

        /// <summary>
        /// Updates the combo box with all the items.
        /// </summary>

        private void UpdateCombo()
        {
            if (_isInitialized)
                return;
            Clear();
            // TODO – customize this method to populate the combobox with your desired items  
            foreach (var categoria in ModuleConfiguration.CategoriasModulos)
            {
                Add(new ComboBoxItem(categoria.Categoria));
            }
            //SelectedItem = ItemCollection.FirstOrDefault();
            Enabled = true;
            _isInitialized = true;
        }

        /// <summary>
        /// The on comboBox selection change event. 
        /// </summary>
        /// <param name="item">The newly selected combo box item</param>
        protected override void OnSelectionChange(ComboBoxItem item)
        {

            base.OnSelectionChange(item);

            if (item != null && !string.IsNullOrEmpty(item.Text) )
            {
                //ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Categoría seleccionada: {item.Text}");
                // Actualiza el ComboBox de módulos
                //ModulosComboBox?.UpdateModules(item.Text);
                //SelectorModulos();
                // Notifica al servicio que la categoría ha cambiado
                ModuleSelectionService.CategoryChanged(item.Text);
            }
        }

    }
    internal class SelectorModulos : ComboBox
    {
        private bool _isInitialized;
        public SelectorModulos()
        {
            // Inicialmente vacío, será actualizado según la categoría seleccionada.
            Clear();
            ModuleSelectionService.OnCategorySelected += UpdateModules;
        }

        /// <summary>
        /// Updates the combo box with all the items.
        /// </summary>
        public void UpdateModules(string categoria)
        {
            Clear();
            var modulos = ModuleConfiguration.CategoriasModulos.FirstOrDefault(c => c.Categoria == categoria)?.Modulos;
            if (modulos != null && modulos.Any())
            {
                foreach (var modulo in modulos)
                {
                    Add(new ComboBoxItem(modulo.Nombre) );
                }

            }
            else
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"No se encontraron módulos para la categoría: {categoria}",
                                                                  "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                SelectedItem = null; // Limpia la selección
            }
            // Reiniciar la selección del módulo en el servicio
            ModuleSelectionService.SelectedModule = null;
        }
        protected override void OnSelectionChange(ComboBoxItem item)
        {
            base.OnSelectionChange(item);

            if (item != null && !string.IsNullOrEmpty(item.Text))
            {
                // Actualizar el módulo seleccionado en el servicio
                ModuleSelectionService.SelectedModule = item.Text;
            }
            else
            {
                ModuleSelectionService.SelectedModule = null;
            }
        }
        protected override void OnUpdate()
        {
            // Habilita o deshabilita el ComboBox dependiendo de si hay elementos
            Enabled = ItemCollection.Any();
        }
    }

    internal class ConfirmationModule : Button
    {
        public SelectorModulos ModulosComboBox { get; set; }
        //public MainContainer Contenedor { get; set; } // Referencia al MainContainer

        protected override void OnClick()
        {
            base.OnClick();
            try
            {
                // Obtener la instancia del SelectorModulos
                //var modulosComboBox = FrameworkApplication.GetPlugInWrapper("SigcatminProAddin_View_Modulos_SelectorModulos") as SelectorModulos;
                var selectedModuleName = ModuleSelectionService.SelectedModule;

                if (!string.IsNullOrEmpty(selectedModuleName))
                {
                    // Buscar el módulo seleccionado en la configuración
                    var moduloSeleccionado = ModuleConfiguration.CategoriasModulos
                        .SelectMany(c => c.Modulos)
                        .FirstOrDefault(m => m.Nombre == selectedModuleName);

                    if (moduloSeleccionado != null)
                    {
                        // Cargar la página asociada al módulo en el contenedor
                        var page = PageManager.GetOrCreatePage(moduloSeleccionado.Pagina);
                        var contenedor = MainContainer.Instance;
                        contenedor.LoadModule(moduloSeleccionado.Nombre, page);
                        if (!contenedor.IsVisible)
                        {
                            contenedor.Show();
                        }
                        contenedor.Focus();
                    }
                    else
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                            "No se encontró el módulo seleccionado.",
                            "Error",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Warning
                        );
                    }
                }
                else
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                        "Seleccione un módulo antes de continuar.",
                        "Advertencia",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Warning
                    );
                    //Contenedor = new MainContainer();
                    //Contenedor.Show();
                    //_mainContainer.Show();
                }
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                    $"Ocurrió un error al intentar cargar el módulo: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
    public static class ModuleSelectionService
    {
        public static event Action<string> OnCategorySelected;

        public static void CategoryChanged(string categoria)
        {
            OnCategorySelected?.Invoke(categoria);
        }
        // Propiedad para almacenar el módulo seleccionado
        public static string SelectedModule { get; set; }
    }
}
