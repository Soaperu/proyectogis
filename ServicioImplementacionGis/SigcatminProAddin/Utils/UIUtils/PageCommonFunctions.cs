using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace SigcatminProAddin.Utils.UIUtils
{
    internal class PageCommonFunctions
    {
        public void ClearControls(DependencyObject container)
        {
            // Limpiar controles dentro de la página
            foreach (var control in FindVisualChildren<DependencyObject>(container))
            {
                switch (control)
                {
                    case TextBox textBox:
                        textBox.Text = string.Empty;
                        break;
                    case ComboBox comboBox:
                        comboBox.SelectedIndex = -1;
                        break;                
                }
            }
        }

        private static bool IsDataGridTable(GridControl gridControl)
        {
            // Validar por nombre (si tienen nombres específicos)
            if (!string.IsNullOrEmpty(gridControl.Name) && gridControl.Name.Contains("Table"))
            {
                return true; // Es un GridControl que corresponde a una tabla
            }

            // Validar por otra propiedad específica, si aplica
            // Puedes agregar validaciones adicionales si usas algún atributo personalizado o dato de contexto
            return false; // No es un DataGrid de tabla
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child is T t)
                {
                    yield return t;
                }

                foreach (var childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
