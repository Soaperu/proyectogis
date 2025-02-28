using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using CommonUtilities;
using DatabaseConnector;
using ReportGenerator;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para SuperpuestoPorDia.xaml
    /// </summary>
    public partial class SuperpuestoPorDia : Page
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        private ReporteSuperpuestoPorDia _reporteSuperpuestoPorDia = new ReporteSuperpuestoPorDia();
        private DataTable dtSuperpuestos;
        private DataTable dtDetalleSuperpuestos;

        public SuperpuestoPorDia()
        {
            InitializeComponent();
            CurrentUser();
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePickerInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerFin.SelectedDate = DatePickerInicio.SelectedDate;
        }

        private void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaInicio = DatePickerInicio.SelectedDate.Value;
            DateTime fechaFin = DatePickerFin.SelectedDate.Value;

            string fechaInicioAsString = fechaInicio.ToString("dd/MM/yyyy");
            string fechaFinAsString = fechaFin.ToString("dd/MM/yyyy");

            dtSuperpuestos = databaseHandler.GetDMOverlapByDay(fechaInicioAsString, fechaFinAsString);
            dtDetalleSuperpuestos = databaseHandler.GetDetailsofDMOverlapByDay(fechaInicioAsString, fechaFinAsString);

            DataGridSuperpuestos.ItemsSource = dtSuperpuestos;
            DataGridSuperpuestosDetalle.ItemsSource = dtDetalleSuperpuestos;

            LblNumRegistros.Content = dtSuperpuestos.Rows.Count.ToString();
            LblNumRegistrosDetalle.Content = dtDetalleSuperpuestos.Rows.Count.ToString();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void BtnExportarSuperpuesto_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls",
                Title = "Guardar Archivo de Excel",
                DefaultExt = "xlsx",
                FileName = "Superpuesto por dia",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                
                _reporteSuperpuestoPorDia.ExportXLSReporte(dtSuperpuestos, "REPORTE DE DERECHOS MINEROS SUPERPUESTOS POR DÍA", filePath);

                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true // Usar el shell del sistema para abrir el archivo
                    });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"No se pudo abrir el archivo Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnExportarSuperpuestDetalle_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls",
                Title = "Guardar Archivo de Excel",
                DefaultExt = "xlsx",
                FileName = "Detalle Superpuesto por dia",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                _reporteSuperpuestoPorDia.ExportXLSReporte(dtDetalleSuperpuestos, "DETALLE - REPORTE DE DERECHOS MINEROS SUPERPUESTOS POR DÍA", filePath);

                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true // Usar el shell del sistema para abrir el archivo
                    });
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"No se pudo abrir el archivo Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
        }

        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }
    }
}
