namespace ReportGenerator.ReportsDevExpress.ReporteAD
{
    partial class SubReporteDatosAreas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tablaTipoDerecho = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.celdaCodigo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.celdaNombre = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.labelTipoDerecho = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTipoDerecho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tablaTipoDerecho});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // tablaTipoDerecho
            // 
            this.tablaTipoDerecho.BackColor = System.Drawing.Color.Transparent;
            this.tablaTipoDerecho.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tablaTipoDerecho.BorderWidth = 0.5F;
            this.tablaTipoDerecho.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.tablaTipoDerecho.ForeColor = System.Drawing.Color.Black;
            this.tablaTipoDerecho.LocationFloat = new DevExpress.Utils.PointFloat(169.25F, 0F);
            this.tablaTipoDerecho.Name = "tablaTipoDerecho";
            this.tablaTipoDerecho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.tablaTipoDerecho.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.tablaTipoDerecho.SizeF = new System.Drawing.SizeF(380F, 25F);
            this.tablaTipoDerecho.StylePriority.UseBackColor = false;
            this.tablaTipoDerecho.StylePriority.UseBorders = false;
            this.tablaTipoDerecho.StylePriority.UseBorderWidth = false;
            this.tablaTipoDerecho.StylePriority.UseFont = false;
            this.tablaTipoDerecho.StylePriority.UseForeColor = false;
            this.tablaTipoDerecho.StylePriority.UseTextAlignment = false;
            this.tablaTipoDerecho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.celdaCodigo,
            this.xrTableCell2,
            this.celdaNombre});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // celdaCodigo
            // 
            this.celdaCodigo.Multiline = true;
            this.celdaCodigo.Name = "celdaCodigo";
            this.celdaCodigo.Text = "[VERTICE]";
            this.celdaCodigo.Weight = 1.2547220580490941D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "[ESTE]";
            this.xrTableCell2.Weight = 1.7566115691001141D;
            // 
            // celdaNombre
            // 
            this.celdaNombre.Multiline = true;
            this.celdaNombre.Name = "celdaNombre";
            this.celdaNombre.Text = "[NORTE]";
            this.celdaNombre.Weight = 1.7566111391275849D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrTable2,
            this.labelTipoDerecho});
            this.GroupHeader1.HeightF = 82.99999F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'CONCESIÓN:   \' + [CONCESION]")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(269.25F, 14.54166F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(311.25F, 23.95833F);
            this.xrLabel1.StylePriority.UseFont = false;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.Silver;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 0.5F;
            this.xrTable2.Font = new DevExpress.Drawing.DXFont("Arial", 8F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTable2.ForeColor = System.Drawing.Color.White;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(169.25F, 57.99999F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(380F, 25F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell19,
            this.xrTableCell20});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "VÉRTICE";
            this.xrTableCell13.Weight = 1.474200616457066D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "ESTE";
            this.xrTableCell19.Weight = 2.0638808239069717D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Multiline = true;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "NORTE";
            this.xrTableCell20.Weight = 2.0638808763411922D;
            // 
            // labelTipoDerecho
            // 
            this.labelTipoDerecho.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "\'CÓDIGO:   \' + [CODIGO]")});
            this.labelTipoDerecho.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.labelTipoDerecho.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 14.54166F);
            this.labelTipoDerecho.Multiline = true;
            this.labelTipoDerecho.Name = "labelTipoDerecho";
            this.labelTipoDerecho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.labelTipoDerecho.SizeF = new System.Drawing.SizeF(220.8333F, 23.95833F);
            this.labelTipoDerecho.StylePriority.UseFont = false;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel11,
            this.xrLabel9});
            this.GroupFooter1.HeightF = 67.08336F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(149.25F, 10.00001F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(400F, 3F);
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(213.75F, 22.75001F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(150F, 17.70833F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "ÁREA UTM:";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AREA] + \' ha.\'")});
            this.xrLabel9.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(378.3334F, 22.75003F);
            this.xrLabel9.Multiline = true;
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(233.3333F, 17.70833F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // SubReporteDatosAreas
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.GroupHeader1,
            this.GroupFooter1});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(50F, 50F, 0F, 0F);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "23.1";
            ((System.ComponentModel.ISupportInitialize)(this.tablaTipoDerecho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRLabel labelTipoDerecho;
        private DevExpress.XtraReports.UI.XRTable tablaTipoDerecho;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell celdaCodigo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell celdaNombre;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
