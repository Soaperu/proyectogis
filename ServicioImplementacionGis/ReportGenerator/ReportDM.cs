using System.Data;
using System.Reflection.Emit;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.CodeParser;


namespace ReportGenerator
{
    public class ReportDM
    {
        public static void ShowReport(DataTable dataSource, string reportName, string definitionFilePath, ReportCustomizations customizations)
        {
            XtraReport report = new XtraReport();
            report.LoadLayout(definitionFilePath); // Cargar la definición del XML
            report.XmlDataPath = definitionFilePath;
            report.DataSource = dataSource;

            // Personalizar campos si es necesario
            var fieldFechaDocumento = report.FindControl("FECHA_DOCUMENTO", true) as XRLabel;
            if (fieldFechaDocumento != null)
                fieldFechaDocumento.Text = customizations.FechaDocumento.ToString("dd/MM/yyyy");

            var fieldTitulo = report.FindControl("lblTitulo_1", true) as XRLabel;
            if (fieldTitulo != null)
                fieldTitulo.Text = customizations.Titulo;

            var fieldCarta = report.FindControl("lblCarta", true) as XRLabel;
            if (fieldCarta != null)
                fieldCarta.Text = customizations.Carta;
            report.CreateDocument(true);
            var w = new Window
            {
                Title = "Vista Previa del Reporte",
                Content = new DocumentPreviewControl
                {
                    DocumentSource = report
                },
                Width = 800,
                Height = 600
            };
            //var previewControl = new DevExpress.Xpf.Printing.DocumentPreviewControl();
            //previewControl.DocumentSource = report;
            //// Mostrar el reporte
            //w.Content = previewControl;
            PrintHelper.ShowPrintPreviewDialog(w,report);
            //w.ShowDialog();
        }
    }

    public class ReportCustomizations
    {
        public DateTime FechaDocumento { get; set; }
        public string Titulo { get; set; }
        public string Carta { get; set; }
    }
}
