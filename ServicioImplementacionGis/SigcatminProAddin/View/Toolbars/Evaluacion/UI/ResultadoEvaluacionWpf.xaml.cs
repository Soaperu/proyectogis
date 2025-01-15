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
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para ResultadoEvaluacionWpf.xaml
    /// </summary>
    public partial class ResultadoEvaluacionWpf : Window
    {
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        public List<OpcionEval> EvalOptions { get; set; }
        public ResultadoEvaluacionWpf()
        {
            InitializeComponent();
            EvalOptions = new List<OpcionEval>
            {
                new OpcionEval { Nombre = "PR", Valor = "PR" },
                new OpcionEval { Nombre = "PO", Valor = "PO" },
                new OpcionEval { Nombre = "SI", Valor = "SI" },
                new OpcionEval { Nombre = "EX", Valor = "EX" },
                new OpcionEval { Nombre = "RD", Valor = "RD" }
            };
            DataContext = this;
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
                DataGridEvaluacion.ItemsSource = null;
                string currentTag = TcEvaluacion.SelectedItem.ToString();
                var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio
                                        .Where(r => r.Eval != null && r.Eval.Equals(currentTag, StringComparison.OrdinalIgnoreCase))
                                        .ToList(); ;
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
                    //var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ResultadosCriterio[filtro];
                    var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio
                                            .Where(r => r.Eval != null && r.Eval.Equals(filtro, StringComparison.OrdinalIgnoreCase))
                                            .ToList(); ;
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

        private void DataGridEvaluacionTableView_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Header.ToString() == "Evaluación") // Solo actualiza si se modifica la columna Eval
            {
                if (e.Row is ResultadoEval resultadoModificado)
                {
                    // Llama al método para actualizar la base de datos
                    databaseHandler.ActualizarRegistroEvaluacionTecnica(GlobalVariables.resultadoEvaluacion.codigo ,resultadoModificado.CodigoU, resultadoModificado.Eval);
                }
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la fila seleccionada
            if (sender is Button button )
            {
                if(button.Tag is DevExpress.Xpf.Grid.EditGridCellData cell && cell.Row is ResultadoEval resultadoSeleccionado) 
                {
                    MessageBoxResult confirmacion = MessageBox.Show(
                    $"¿Estás seguro de eliminar el registro:{resultadoSeleccionado.CodigoU} ?",
                    "Confirmar Eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                    if (confirmacion == MessageBoxResult.Yes)
                    {
                        // 1. Eliminar de la base de datos
                        databaseHandler.EliminarRegistroEvaluacionTecnica(GlobalVariables.resultadoEvaluacion.codigo, resultadoSeleccionado.CodigoU);


                        // 2. Eliminar del objeto en memoria
                        GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio.Remove(resultadoSeleccionado);

                        // 3. Refrescar el DataGrid
                        DataGridEvaluacion.ItemsSource = null;
                        string currentTag = TcEvaluacion.SelectedItem.ToString();
                        var resultadosCriterio = GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio
                                                .Where(r => r.Eval != null && r.Eval.Equals(currentTag, StringComparison.OrdinalIgnoreCase))
                                                .ToList(); ;
                        DataGridEvaluacion.ItemsSource = resultadosCriterio;

                        MessageBox.Show("Registro eliminado correctamente.", "Eliminación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);


                    }               
                    
                }
            }
        }

    }


    public class OpcionEval
    {
        public string Nombre { get; set; }  // Texto visible
        public string Valor { get; set; }   // Valor real
    }

}
