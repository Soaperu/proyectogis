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
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para ObservacionesCartaIgnWpf.xaml
    /// </summary>
    public partial class ObservacionesCartaIgnWpf : Window
    {
        public ObservacionesCartaIgnWpf()
        {
            InitializeComponent();
        }

        // Se asume que v_checkbox1..v_checkbox20 provienen de algún UI o propiedades de clase
        //public string checked1 { get; set; }
        //public string checked2 { get; set; }
        // ...
        //public string checked20 { get; set; }
        public string v_codigo { get; set; }
        public string gstrCodigo_Usuario { get; set; }
        public string v_observa_carta { get; set; }

        // Colecciones que se usaban en el VB6 / ArcObjects
        private List<string> colecciones_obs = new List<string>();
        private List<string> colecciones_obs_update = new List<string>();
        private List<string> colecciones_obs_upd_borrar = new List<string>();

        // Valores de coordenadas que usabas en tu layout
        private double posi_y_m = 18.0;
        private double posi_y1_m = 17.4;

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checked11_Checked(object sender, RoutedEventArgs e)
        {
            txtRio.Visibility = Visibility.Visible;
        }

        private void checked11_Unchecked(object sender, RoutedEventArgs e)
        {
            txtRio.Visibility = Visibility.Collapsed;
        }

        private void checked13_Checked(object sender, RoutedEventArgs e)
        {
            txtLaguna.Visibility = Visibility.Visible;
        }

        private void checked13_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLaguna.Visibility = Visibility.Collapsed;
        }

        private void checked20_Checked(object sender, RoutedEventArgs e)
        {
            txtAreaUrbana.Visibility = Visibility.Visible;
        }

        private void checked20_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAreaUrbana.Visibility = Visibility.Collapsed;
        }
    }
}
