<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPorcentajeUbicacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPorcentajeUbicacion))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lnk_userguide = New System.Windows.Forms.LinkLabel()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.btnExportarTxt = New System.Windows.Forms.Button()
        Me.btnExportarExcel = New System.Windows.Forms.Button()
        Me.dgvPorcentajeUbi = New System.Windows.Forms.DataGridView()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboAnnoConsulta = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnProcesarControl = New System.Windows.Forms.Button()
        Me.btnExportarExcelControl = New System.Windows.Forms.Button()
        Me.dgvControl = New System.Windows.Forms.DataGridView()
        Me.CerrarControl = New System.Windows.Forms.Button()
        Me.cboAnnoControl = New System.Windows.Forms.ComboBox()
        Me.lblAnno = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvPorcentajeUbi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(7, 11)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(681, 416)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabPage1.Controls.Add(Me.lnk_userguide)
        Me.TabPage1.Controls.Add(Me.btnProcesar)
        Me.TabPage1.Controls.Add(Me.btnExportarTxt)
        Me.TabPage1.Controls.Add(Me.btnExportarExcel)
        Me.TabPage1.Controls.Add(Me.dgvPorcentajeUbi)
        Me.TabPage1.Controls.Add(Me.btnCerrar)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cboAnnoConsulta)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(673, 390)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "PorcentajeUbicacion"
        '
        'lnk_userguide
        '
        Me.lnk_userguide.AutoSize = True
        Me.lnk_userguide.Location = New System.Drawing.Point(558, 53)
        Me.lnk_userguide.Name = "lnk_userguide"
        Me.lnk_userguide.Size = New System.Drawing.Size(94, 13)
        Me.lnk_userguide.TabIndex = 23
        Me.lnk_userguide.TabStop = True
        Me.lnk_userguide.Text = "Manual de usuario"
        '
        'btnProcesar
        '
        Me.btnProcesar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcesar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnProcesar.Location = New System.Drawing.Point(281, 45)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(80, 24)
        Me.btnProcesar.TabIndex = 22
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'btnExportarTxt
        '
        Me.btnExportarTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportarTxt.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnExportarTxt.Location = New System.Drawing.Point(281, 344)
        Me.btnExportarTxt.Name = "btnExportarTxt"
        Me.btnExportarTxt.Size = New System.Drawing.Size(101, 24)
        Me.btnExportarTxt.TabIndex = 21
        Me.btnExportarTxt.Text = "Exportar Txt"
        Me.btnExportarTxt.UseVisualStyleBackColor = True
        '
        'btnExportarExcel
        '
        Me.btnExportarExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportarExcel.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnExportarExcel.Location = New System.Drawing.Point(511, 344)
        Me.btnExportarExcel.Name = "btnExportarExcel"
        Me.btnExportarExcel.Size = New System.Drawing.Size(101, 24)
        Me.btnExportarExcel.TabIndex = 20
        Me.btnExportarExcel.Text = "Exportar Excel"
        Me.btnExportarExcel.UseVisualStyleBackColor = True
        '
        'dgvPorcentajeUbi
        '
        Me.dgvPorcentajeUbi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPorcentajeUbi.Location = New System.Drawing.Point(19, 91)
        Me.dgvPorcentajeUbi.Name = "dgvPorcentajeUbi"
        Me.dgvPorcentajeUbi.Size = New System.Drawing.Size(633, 232)
        Me.dgvPorcentajeUbi.TabIndex = 19
        '
        'btnCerrar
        '
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnCerrar.Location = New System.Drawing.Point(58, 344)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(80, 24)
        Me.btnCerrar.TabIndex = 18
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(179, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(286, 20)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Consultar Porcentaje de Ubicación"
        '
        'cboAnnoConsulta
        '
        Me.cboAnnoConsulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAnnoConsulta.FormattingEnabled = True
        Me.cboAnnoConsulta.Location = New System.Drawing.Point(126, 45)
        Me.cboAnnoConsulta.Name = "cboAnnoConsulta"
        Me.cboAnnoConsulta.Size = New System.Drawing.Size(95, 21)
        Me.cboAnnoConsulta.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(16, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Seleccionar Año:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(16, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Seleccionar Año:"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.dtpFechaFinal)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.dtpFechaInicio)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.btnProcesarControl)
        Me.TabPage2.Controls.Add(Me.btnExportarExcelControl)
        Me.TabPage2.Controls.Add(Me.dgvControl)
        Me.TabPage2.Controls.Add(Me.CerrarControl)
        Me.TabPage2.Controls.Add(Me.cboAnnoControl)
        Me.TabPage2.Controls.Add(Me.lblAnno)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(673, 390)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Control"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(23, 317)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(633, 33)
        Me.TextBox1.TabIndex = 261
        Me.TextBox1.Text = "Listado de derechos mineros a ser distribuidos en determinado mes, cuyas demarcac" & _
    "iones comprendidas difieren en relación a la base del padrón del año comparado"
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFinal.Location = New System.Drawing.Point(151, 41)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(96, 20)
        Me.dtpFechaFinal.TabIndex = 259
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(21, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 258
        Me.Label1.Text = "Fecha final:"
        '
        'dtpFechaInicio
        '
        Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicio.Location = New System.Drawing.Point(150, 15)
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Size = New System.Drawing.Size(96, 20)
        Me.dtpFechaInicio.TabIndex = 257
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(20, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Fecha inicio:"
        '
        'btnProcesarControl
        '
        Me.btnProcesarControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcesarControl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnProcesarControl.Location = New System.Drawing.Point(299, 15)
        Me.btnProcesarControl.Name = "btnProcesarControl"
        Me.btnProcesarControl.Size = New System.Drawing.Size(80, 75)
        Me.btnProcesarControl.TabIndex = 30
        Me.btnProcesarControl.Text = "Procesar"
        Me.btnProcesarControl.UseVisualStyleBackColor = True
        '
        'btnExportarExcelControl
        '
        Me.btnExportarExcelControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportarExcelControl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnExportarExcelControl.Location = New System.Drawing.Point(513, 356)
        Me.btnExportarExcelControl.Name = "btnExportarExcelControl"
        Me.btnExportarExcelControl.Size = New System.Drawing.Size(101, 24)
        Me.btnExportarExcelControl.TabIndex = 28
        Me.btnExportarExcelControl.Text = "Exportar Excel"
        Me.btnExportarExcelControl.UseVisualStyleBackColor = True
        '
        'dgvControl
        '
        Me.dgvControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvControl.Location = New System.Drawing.Point(23, 98)
        Me.dgvControl.Name = "dgvControl"
        Me.dgvControl.Size = New System.Drawing.Size(633, 219)
        Me.dgvControl.TabIndex = 27
        '
        'CerrarControl
        '
        Me.CerrarControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CerrarControl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.CerrarControl.Location = New System.Drawing.Point(60, 356)
        Me.CerrarControl.Name = "CerrarControl"
        Me.CerrarControl.Size = New System.Drawing.Size(80, 24)
        Me.CerrarControl.TabIndex = 26
        Me.CerrarControl.Text = "Cerrar"
        Me.CerrarControl.UseVisualStyleBackColor = True
        '
        'cboAnnoControl
        '
        Me.cboAnnoControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAnnoControl.FormattingEnabled = True
        Me.cboAnnoControl.Location = New System.Drawing.Point(151, 69)
        Me.cboAnnoControl.Name = "cboAnnoControl"
        Me.cboAnnoControl.Size = New System.Drawing.Size(95, 21)
        Me.cboAnnoControl.TabIndex = 25
        '
        'lblAnno
        '
        Me.lblAnno.AutoSize = True
        Me.lblAnno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnno.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblAnno.Location = New System.Drawing.Point(22, 75)
        Me.lblAnno.Name = "lblAnno"
        Me.lblAnno.Size = New System.Drawing.Size(127, 13)
        Me.lblAnno.TabIndex = 23
        Me.lblAnno.Text = "Año de comparación:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(22, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Seleccionar Año:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(494, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 262
        Me.Label7.Text = "Estado (valores)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(465, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 13)
        Me.Label9.TabIndex = 264
        Me.Label9.Text = "2 : Demarcación diferente"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Location = New System.Drawing.Point(465, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(146, 13)
        Me.Label10.TabIndex = 265
        Me.Label10.Text = "3 : DM no existe en año base"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(465, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 13)
        Me.Label8.TabIndex = 267
        Me.Label8.Text = "0 : Demarcación diferente"
        '
        'frmPorcentajeUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 437)
        Me.Controls.Add(Me.TabControl1)
        Me.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPorcentajeUbicacion"
        Me.Text = "Reporte de Porcentaje de Ubicacion"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvPorcentajeUbi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgvControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lnk_userguide As System.Windows.Forms.LinkLabel
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents btnExportarTxt As System.Windows.Forms.Button
    Friend WithEvents btnExportarExcel As System.Windows.Forms.Button
    Friend WithEvents dgvPorcentajeUbi As System.Windows.Forms.DataGridView
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboAnnoConsulta As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dtpFechaInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnProcesarControl As System.Windows.Forms.Button
    Friend WithEvents btnExportarExcelControl As System.Windows.Forms.Button
    Friend WithEvents dgvControl As System.Windows.Forms.DataGridView
    Friend WithEvents CerrarControl As System.Windows.Forms.Button
    Friend WithEvents cboAnnoControl As System.Windows.Forms.ComboBox
    Friend WithEvents lblAnno As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
