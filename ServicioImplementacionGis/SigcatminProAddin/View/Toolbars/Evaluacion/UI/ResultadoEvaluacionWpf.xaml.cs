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
using CommonUtilities;

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para ResultadoEvaluacionWpf.xaml
    /// </summary>
    public partial class ResultadoEvaluacionWpf : Window
    {
        public ResultadoEvaluacionWpf()
        {
            InitializeComponent();
        }
        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Comprueba que el botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalVariables.resultadoEvaluacion.isCompleted)
            {
                TbxCodigo.Text = GlobalVariables.resultadoEvaluacion.codigo.ToString();
                TbxNombre.Text = GlobalVariables.resultadoEvaluacion.nombre.ToString();
                string distancia = GlobalVariables.resultadoEvaluacion.distanciaFrontera.ToString();
                TbxFrontera.Text = $"Distancia de la línea de frontera de: {distancia} Km.";
                var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ResultadosCriterio["PR"];
                DataGridEvaluacion.ItemsSource = null;
                DataGridEvaluacion.ItemsSource = resultadosCriterio;

                string zonasUrbanasText;
                if (string.IsNullOrEmpty(GlobalVariables.resultadoEvaluacion.listaCaramUrbana))
                {
                    zonasUrbanasText = "No se encuentra superpuesto a un Área urbana";
                }
                else
                {
                    if (GlobalVariables.resultadoEvaluacion.listaCaramUrbana.Length > 65)
                    {
                        string posi_x = GlobalVariables.resultadoEvaluacion.listaCaramUrbana.Substring(0, 65);
                        string posi_x1 = GlobalVariables.resultadoEvaluacion.listaCaramUrbana.Substring(65);
                        zonasUrbanasText = posi_x + "\n" + posi_x1;
                    }
                    else
                    {
                        zonasUrbanasText =  GlobalVariables.resultadoEvaluacion.listaCaramUrbana;
                    }
                }

                string zonasReservadasText;
                if (string.IsNullOrEmpty(GlobalVariables.resultadoEvaluacion.listaCaramReservada))
                {
                    zonasReservadasText = "No se encuentra superpuesto a un Área de Reserva";
                }
                else
                {
                    if (GlobalVariables.resultadoEvaluacion.listaCaramReservada.Length > 65)
                    {
                        string posi_x = GlobalVariables.resultadoEvaluacion.listaCaramReservada.Substring(0, 65);
                        string posi_x1 = GlobalVariables.resultadoEvaluacion.listaCaramReservada.Substring(65);
                        zonasReservadasText =  posi_x + "\n" + posi_x1;
                    }
                    else
                    {
                        zonasReservadasText = GlobalVariables.resultadoEvaluacion.listaCaramReservada;
                    }
                }
                TbxZonaReservada.Text = zonasReservadasText;
                TbxZonaUrbana.Text = zonasUrbanasText;
            }
            else
            {
                this.Close();
            }
        }

        private void TcEvaluacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TcEvaluacion.SelectedItem is TabItem tabItem)
            {
                // Obtén el valor del Tag para determinar el filtro
                string filtro = tabItem.Tag as string;

                if (!string.IsNullOrEmpty(filtro) && GlobalVariables.resultadoEvaluacion.ResultadosCriterio.ContainsKey(filtro))
                {
                    var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ResultadosCriterio[filtro];
                    DataGridEvaluacion.ItemsSource = null;
                    DataGridEvaluacion.ItemsSource = resultadosCriterio;
                }
                else
                {
                    // Maneja el caso donde el filtro no existe o está vacío
                    DataGridEvaluacion.ItemsSource = null;
                }
            }


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
