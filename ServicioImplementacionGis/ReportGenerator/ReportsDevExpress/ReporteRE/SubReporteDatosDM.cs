using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ReportGenerator.ReportsDevExpress.ReporteRE
{
    public partial class SubReporteDatosDM : DevExpress.XtraReports.UI.XtraReport
    {
        public SubReporteDatosDM()
        {
            InitializeComponent();
            ConfigureReport();
        }

        private void ConfigureReport()
        {

            // Modificar "Sustancia" por la variable a agrupar
            GroupHeader1.GroupFields.Add(new GroupField("ORDEN"));

        }
    }
}
