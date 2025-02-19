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

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI.PlanosDiversosFormatos.Views
{
    /// <summary>
    /// Lógica de interacción para Step1View.xaml
    /// </summary>
    public partial class Step1View : UserControl
    {
        public Step1View()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Cierra la ventana que contiene este UserControl
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
