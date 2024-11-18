using SigcatminProAddin.View.Modulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SigcatminProAddin.View.Contenedor
{
    internal class MainViewModel
    {
        public SelectorCategorias CategoriasComboBox { get; private set; }
        public SelectorModulos ModulosComboBox { get; private set; }
        public Frame Contenedor { get; set; } // Frame para cargar páginas

        public MainViewModel(Frame contenedor)
        {
            ModulosComboBox = new SelectorModulos();
            CategoriasComboBox = new SelectorCategorias(ModulosComboBox);

            Contenedor = contenedor;

            // Evento de cambio en el ComboBox de categorías
            CategoriasComboBox.ModulosComboBox = ModulosComboBox;
        }

        private void OnCategoriaChanged(object sender, string categoriaSeleccionada)
        {
            if (!string.IsNullOrEmpty(categoriaSeleccionada))
            {
                // Actualiza los módulos del segundo ComboBox según la categoría seleccionada
                ModulosComboBox.UpdateModules(categoriaSeleccionada);
            }
        }

        public void OnButtonClicked()
        {
            // Obtén el módulo seleccionado
            var moduloSeleccionado = ModulosComboBox.SelectedItem as ModuleConfiguration.Modulo;
            if (moduloSeleccionado != null)
            {
                // Carga la página asociada en el contenedor
                var page = PageManager.GetOrCreatePage(moduloSeleccionado.Pagina);
                Contenedor.Navigate(page);
            }
        }
    }

}
