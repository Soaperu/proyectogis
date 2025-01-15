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
using ReportGenerator.ReportsDevExpress.ReporteRE;
using DevExpress.XtraReports;
using CommonUtilities;


namespace ReportGenerator
{
    public class ReportDM
    {
        public static void ShowReport(DataTable dataSource, string reportName, ReportCustomizations customizations)
        {
            if(reportName == "Reporte_DM_01")
            {
                XtraReport report = new ReportsDevExpress.ReportEvalDM();
                report.DataSource = dataSource;

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

            if (reportName == "Reporte_DM_02")
            {
                XtraReport report = new ReportsDevExpress.ReporteSPE.ReportePrincipal();
                report.DataSource = dataSource;

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

            if (reportName == "Reporte_DM_03")
            {
                // Crear instancia del reporte y asignar la fuente de datos
                XtraReport report = new ReportsDevExpress.ReporteRE.ReportePrincipal();

                // Crear instancia del subreporte y asignar la fuente de datos
                var subreporte = new SubReporteDatosDM();
                subreporte.DataSource = dataSource;

                // Asignar las fuentes de datos a los subreportes
                var dmDatosSubreporte = (XRSubreport)report.FindControl("xrSubreport1", true);
                dmDatosSubreporte.ReportSource = subreporte;


                // Asignar los valores de las propiedades personalizadas
                var fieldCodigoDM = report.FindControl("lblCodigo", true) as XRLabel;
                if (fieldCodigoDM != null)
                    fieldCodigoDM.Text = GlobalVariables.CurrentCodeDm;

                var fieldNombreDM = report.FindControl("lblConcesion", true) as XRLabel;
                if (fieldNombreDM != null)
                    fieldNombreDM.Text = GlobalVariables.CurrentNameDm;

                var fieldAreaDM = report.FindControl("lblHectarea", true) as XRLabel;
                if (fieldAreaDM != null)
                    fieldAreaDM.Text = GlobalVariables.CurrentAreaDm;

                string distancia = GlobalVariables.resultadoEvaluacion.distanciaFrontera.ToString();
                var fieldLimitesDM = report.FindControl("lblLimitesF", true) as XRLabel;
                if (fieldLimitesDM != null)
                    fieldLimitesDM.Text = $"Distancia de la línea de frontera de: {distancia} Km."; ;

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
                        zonasUrbanasText = GlobalVariables.resultadoEvaluacion.listaCaramUrbana;
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
                        zonasReservadasText = posi_x + "\n" + posi_x1;
                    }
                    else
                    {
                        zonasReservadasText = GlobalVariables.resultadoEvaluacion.listaCaramReservada;
                    }
                }

                var fieldZonasReservadasDM = report.FindControl("lblZonasReservadas", true) as XRLabel;
                if (fieldZonasReservadasDM != null)
                    fieldZonasReservadasDM.Text = zonasReservadasText;

                var fieldZonasUrbanasDM = report.FindControl("lblZonasUrbanas", true) as XRLabel;
                if (fieldZonasUrbanasDM != null)
                    fieldZonasUrbanasDM.Text = zonasUrbanasText;


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
    }

    public class ReportCustomizations
    {
        public DateTime FechaDocumento { get; set; }
        public string Titulo { get; set; }
        public string Carta { get; set; }
    }
}
