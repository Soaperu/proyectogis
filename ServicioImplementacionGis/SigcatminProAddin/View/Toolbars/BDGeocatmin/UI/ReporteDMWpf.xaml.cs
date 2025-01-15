using CommonUtilities;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.IO;
using ArcGIS.Desktop.Reports;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI
{
    /// <summary>
    /// Lógica de interacción para ReporteDMWpf.xaml
    /// </summary>
    public partial class ReporteDMWpf : Window
    {
        public ReporteDMWpf()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            string pathReportTemplate;
            if (reportDm.IsChecked is true)
            {
                //pathReportTemplate = Path.Combine(GlobalVariables.ContaninerTemplatesReports, GlobalVariables.reportDM);
                var table = await ReportGenerator.DataProcessorReports.GenerarReporteAsync("Catastro");//new DataTable();
                var custom = new ReportGenerator.ReportCustomizations();
                custom.Carta = GlobalVariables.CurrentPagesDm;
                custom.Titulo = "Titulo de Prueba";
                custom.FechaDocumento = DateTime.Now;
                ReportGenerator.ReportDM.ShowReport(table, "Reporte_DM_01", custom);
            }
            
            if (reportEvatDm.IsChecked is true)
            {
                var dataProcessor = new ReportGenerator.DataProcessorReports();
                var table = await dataProcessor.LeerResultadosEvalReporteAsync();
                var custom = new ReportGenerator.ReportCustomizations();
                custom.Carta = GlobalVariables.CurrentPagesDm;
                custom.Titulo = "Titulo de Prueba";
                custom.FechaDocumento = DateTime.Now;

                ReportGenerator.ReportDM.ShowReport(table, "Reporte_DM_03", custom);
            }
        }

        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // El botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }

        private void reportDm_Checked(object sender, RoutedEventArgs e)
        {
            btnGenerar.IsEnabled = true;
        }
        private void reportPlaneEvaDm_Checked(object sender, RoutedEventArgs e)
        {
            btnGenerar.IsEnabled = true;
        }
        private void reportEvatDm_Checked(object sender, RoutedEventArgs e)
        {
            btnGenerar.IsEnabled = true;
        }
        private void reportAreaDisp_Checked(object sender, RoutedEventArgs e)
        {
            btnGenerar.IsEnabled = true;
        }
    }
}
