using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ReportGenerator.ReportsDevExpress.ReporteEvInformacion
{
    public partial class SR_01_datos_derechos : DevExpress.XtraReports.UI.XtraReport
    {
        public SR_01_datos_derechos()
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
