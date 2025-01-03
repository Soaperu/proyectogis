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
        public static void ShowReport(DataTable dataSource, string reportName, ReportCustomizations customizations)
        {
            XtraReport report = new ReportsDevExpress.ReportEvalDM();
            report.DataSource = dataSource;

            // Personalizar campos si es necesario
            //var fieldFechaDocumento = report.FindControl("FECHA_DOCUMENTO", true) as XRLabel;
            //if (fieldFechaDocumento != null)
            //    fieldFechaDocumento.Text = customizations.FechaDocumento.ToString("dd/MM/yyyy");

            //var fieldTitulo = report.FindControl("lblTitulo_1", true) as XRLabel;
            //if (fieldTitulo != null)
            //    fieldTitulo.Text = customizations.Titulo;

            var fieldCarta = report.FindControl("lblHoja", true) as XRLabel;
            if (fieldCarta != null)
                fieldCarta.Text = "Carta: " + customizations.Carta.Replace("'", "");
            report.CreateDocument();
            var w = new Window
            {
                Title = "Vista Previa del Reporte",
                Content = new DocumentPreviewControl
                {
                    DocumentSource = report
                },
                Width = 900,
                Height = 700
            };
            w.ShowDialog();
        }
    }

    public class ReportCustomizations
    {
        public DateTime FechaDocumento { get; set; }
        public string Titulo { get; set; }
        public string Carta { get; set; }
    }
}
