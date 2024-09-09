<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_mapa_peligros_geologicos_atenciones
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_pga_generar_reporte = New System.Windows.Forms.Button()
        Me.gbx_pga_reporte_general = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.dtp_pga_enddate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_pga_startdate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbt_pga_general_anio = New System.Windows.Forms.RadioButton()
        Me.rbt_pga_general_date = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.cbx_pga_departamentoxmes = New System.Windows.Forms.CheckBox()
        Me.cbx_pga_departamentosxanio = New System.Windows.Forms.CheckBox()
        Me.cbx_pga_departamentosxanios = New System.Windows.Forms.CheckBox()
        Me.nud_pga_departamentoxmes_anio = New System.Windows.Forms.NumericUpDown()
        Me.nud_pga_departamentosxanio_anio = New System.Windows.Forms.NumericUpDown()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbx_pga_reporte_general.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.nud_pga_departamentoxmes_anio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_pga_departamentosxanio_anio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_pga_generar_reporte, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.gbx_pga_reporte_general, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 3)
        Me.TableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(333, 481)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btn_pga_generar_reporte
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_pga_generar_reporte, 2)
        Me.btn_pga_generar_reporte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_pga_generar_reporte.Image = Global.Automapic.My.Resources.Resources.TableExcel16
        Me.btn_pga_generar_reporte.Location = New System.Drawing.Point(2, 455)
        Me.btn_pga_generar_reporte.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btn_pga_generar_reporte.Name = "btn_pga_generar_reporte"
        Me.btn_pga_generar_reporte.Size = New System.Drawing.Size(329, 24)
        Me.btn_pga_generar_reporte.TabIndex = 0
        Me.btn_pga_generar_reporte.Text = "Generar reporte"
        Me.btn_pga_generar_reporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_pga_generar_reporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_pga_generar_reporte.UseVisualStyleBackColor = True
        '
        'gbx_pga_reporte_general
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.gbx_pga_reporte_general, 2)
        Me.gbx_pga_reporte_general.Controls.Add(Me.TableLayoutPanel2)
        Me.gbx_pga_reporte_general.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbx_pga_reporte_general.Location = New System.Drawing.Point(3, 7)
        Me.gbx_pga_reporte_general.Name = "gbx_pga_reporte_general"
        Me.gbx_pga_reporte_general.Size = New System.Drawing.Size(327, 94)
        Me.gbx_pga_reporte_general.TabIndex = 12
        Me.gbx_pga_reporte_general.TabStop = False
        Me.gbx_pga_reporte_general.Text = "General"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.dtp_pga_enddate, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.dtp_pga_startdate, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.rbt_pga_general_anio, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.rbt_pga_general_date, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(321, 75)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'dtp_pga_enddate
        '
        Me.dtp_pga_enddate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtp_pga_enddate.Enabled = False
        Me.dtp_pga_enddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_pga_enddate.Location = New System.Drawing.Point(162, 49)
        Me.dtp_pga_enddate.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtp_pga_enddate.Name = "dtp_pga_enddate"
        Me.dtp_pga_enddate.Size = New System.Drawing.Size(157, 20)
        Me.dtp_pga_enddate.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(2, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(156, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Fecha de inicio"
        '
        'dtp_pga_startdate
        '
        Me.dtp_pga_startdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtp_pga_startdate.Enabled = False
        Me.dtp_pga_startdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_pga_startdate.Location = New System.Drawing.Point(2, 49)
        Me.dtp_pga_startdate.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtp_pga_startdate.Name = "dtp_pga_startdate"
        Me.dtp_pga_startdate.Size = New System.Drawing.Size(156, 20)
        Me.dtp_pga_startdate.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(162, 31)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Fecha final"
        '
        'rbt_pga_general_anio
        '
        Me.rbt_pga_general_anio.AutoSize = True
        Me.rbt_pga_general_anio.Checked = True
        Me.rbt_pga_general_anio.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rbt_pga_general_anio.Location = New System.Drawing.Point(3, 4)
        Me.rbt_pga_general_anio.Name = "rbt_pga_general_anio"
        Me.rbt_pga_general_anio.Size = New System.Drawing.Size(154, 17)
        Me.rbt_pga_general_anio.TabIndex = 9
        Me.rbt_pga_general_anio.TabStop = True
        Me.rbt_pga_general_anio.Text = "Año actual"
        Me.rbt_pga_general_anio.UseVisualStyleBackColor = True
        '
        'rbt_pga_general_date
        '
        Me.rbt_pga_general_date.AutoSize = True
        Me.rbt_pga_general_date.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rbt_pga_general_date.Location = New System.Drawing.Point(163, 4)
        Me.rbt_pga_general_date.Name = "rbt_pga_general_date"
        Me.rbt_pga_general_date.Size = New System.Drawing.Size(155, 17)
        Me.rbt_pga_general_date.TabIndex = 10
        Me.rbt_pga_general_date.Text = "Por fecha"
        Me.rbt_pga_general_date.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 111)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(327, 94)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Departamento"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cbx_pga_departamentoxmes, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cbx_pga_departamentosxanio, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cbx_pga_departamentosxanios, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.nud_pga_departamentoxmes_anio, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.nud_pga_departamentosxanio_anio, 1, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(321, 75)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'cbx_pga_departamentoxmes
        '
        Me.cbx_pga_departamentoxmes.AutoSize = True
        Me.cbx_pga_departamentoxmes.Checked = True
        Me.cbx_pga_departamentoxmes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbx_pga_departamentoxmes.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_pga_departamentoxmes.Location = New System.Drawing.Point(3, 4)
        Me.cbx_pga_departamentoxmes.Name = "cbx_pga_departamentoxmes"
        Me.cbx_pga_departamentoxmes.Size = New System.Drawing.Size(255, 17)
        Me.cbx_pga_departamentoxmes.TabIndex = 0
        Me.cbx_pga_departamentoxmes.Text = "Atenciones por meses en el año:"
        Me.cbx_pga_departamentoxmes.UseVisualStyleBackColor = True
        '
        'cbx_pga_departamentosxanio
        '
        Me.cbx_pga_departamentosxanio.AutoSize = True
        Me.cbx_pga_departamentosxanio.Checked = True
        Me.cbx_pga_departamentosxanio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbx_pga_departamentosxanio.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_pga_departamentosxanio.Location = New System.Drawing.Point(3, 28)
        Me.cbx_pga_departamentosxanio.Name = "cbx_pga_departamentosxanio"
        Me.cbx_pga_departamentosxanio.Size = New System.Drawing.Size(255, 17)
        Me.cbx_pga_departamentosxanio.TabIndex = 1
        Me.cbx_pga_departamentosxanio.Text = "Atenciones en el año:"
        Me.cbx_pga_departamentosxanio.UseVisualStyleBackColor = True
        '
        'cbx_pga_departamentosxanios
        '
        Me.cbx_pga_departamentosxanios.AutoSize = True
        Me.cbx_pga_departamentosxanios.Checked = True
        Me.cbx_pga_departamentosxanios.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbx_pga_departamentosxanios.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_pga_departamentosxanios.Location = New System.Drawing.Point(3, 55)
        Me.cbx_pga_departamentosxanios.Name = "cbx_pga_departamentosxanios"
        Me.cbx_pga_departamentosxanios.Size = New System.Drawing.Size(255, 17)
        Me.cbx_pga_departamentosxanios.TabIndex = 2
        Me.cbx_pga_departamentosxanios.Text = "Atenciones en todos los años"
        Me.cbx_pga_departamentosxanios.UseVisualStyleBackColor = True
        '
        'nud_pga_departamentoxmes_anio
        '
        Me.nud_pga_departamentoxmes_anio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nud_pga_departamentoxmes_anio.Location = New System.Drawing.Point(264, 3)
        Me.nud_pga_departamentoxmes_anio.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.nud_pga_departamentoxmes_anio.Name = "nud_pga_departamentoxmes_anio"
        Me.nud_pga_departamentoxmes_anio.Size = New System.Drawing.Size(54, 20)
        Me.nud_pga_departamentoxmes_anio.TabIndex = 3
        Me.nud_pga_departamentoxmes_anio.Value = New Decimal(New Integer() {2022, 0, 0, 0})
        '
        'nud_pga_departamentosxanio_anio
        '
        Me.nud_pga_departamentosxanio_anio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nud_pga_departamentosxanio_anio.Location = New System.Drawing.Point(264, 27)
        Me.nud_pga_departamentosxanio_anio.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.nud_pga_departamentosxanio_anio.Name = "nud_pga_departamentosxanio_anio"
        Me.nud_pga_departamentosxanio_anio.Size = New System.Drawing.Size(54, 20)
        Me.nud_pga_departamentosxanio_anio.TabIndex = 4
        Me.nud_pga_departamentosxanio_anio.Value = New Decimal(New Integer() {2022, 0, 0, 0})
        '
        'Form_mapa_peligros_geologicos_atenciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(333, 481)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Form_mapa_peligros_geologicos_atenciones"
        Me.Text = "Form_mapa_peligros_geologicos_atenciones"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.gbx_pga_reporte_general.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.nud_pga_departamentoxmes_anio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_pga_departamentosxanio_anio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_pga_generar_reporte As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtp_pga_startdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_pga_enddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents gbx_pga_reporte_general As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rbt_pga_general_anio As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_pga_general_date As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbx_pga_departamentoxmes As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_pga_departamentosxanio As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_pga_departamentosxanios As System.Windows.Forms.CheckBox
    Friend WithEvents nud_pga_departamentoxmes_anio As System.Windows.Forms.NumericUpDown
    Friend WithEvents nud_pga_departamentosxanio_anio As System.Windows.Forms.NumericUpDown
End Class
