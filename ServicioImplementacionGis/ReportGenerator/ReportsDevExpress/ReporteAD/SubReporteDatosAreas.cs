using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ReportGenerator.ReportsDevExpress.ReporteAD
{
    public partial class SubReporteDatosAreas : DevExpress.XtraReports.UI.XtraReport
    {
        public SubReporteDatosAreas()
        {
            InitializeComponent();
            ConfigureReport();
        }

        private void ConfigureReport()
        {

            // Modificar "Sustancia" por la variable a agrupar -- Eval ??
            GroupHeader1.GroupFields.Add(new GroupField("FID"));

        }
    }
}
