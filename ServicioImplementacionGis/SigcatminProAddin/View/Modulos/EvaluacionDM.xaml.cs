using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para EvaluacionDM.xaml
    /// </summary>
    public partial class EvaluacionDM : Page
    {
        public EvaluacionDM()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
        }

        private void AddCheckBoxesToListBox()
        {
            // Lista de elementos para agregar al ListBox
            string[] items = { "Capa 1", "Capa 2", "Capa 3", "Capa 4" };

            // Agrega cada elemento como un CheckBox al ListBox
            foreach (var item in items)
            {
                CheckBox checkBox = new CheckBox
                {
                    Content = item,
                    Margin = new Thickness(5),
                    Style = (Style)FindResource("Esri_CheckboxToggleSwitch")
                };
                listLayers.Items.Add(checkBox);
            }
        }

        private void cbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
