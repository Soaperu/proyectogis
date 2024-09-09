<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DMSuperpuestoDia
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgdResultado1 = New System.Windows.Forms.DataGridView()
        Me.lblreporte = New System.Windows.Forms.Label()
        Me.btnExporDM = New System.Windows.Forms.Button()
        Me.dgvDMPMA = New System.Windows.Forms.DataGridView()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.dtpfecha_inicio = New System.Windows.Forms.DateTimePicker()
        Me.dtpfecha_fin = New System.Windows.Forms.DateTimePicker()
        Me.lblfecha = New System.Windows.Forms.Label()
        Me.lblfecha_inicio = New System.Windows.Forms.Label()
        Me.lblfecha_fin = New System.Windows.Forms.Label()
        Me.lblconsulta = New System.Windows.Forms.Label()
        Me.rbfecha = New System.Windows.Forms.RadioButton()
        Me.rbfechas = New System.Windows.Forms.RadioButton()
        Me.gbconsulta = New System.Windows.Forms.GroupBox()
        Me.lblmensaje1 = New System.Windows.Forms.Label()
        Me.lblmensaje = New System.Windows.Forms.Label()
        Me.dtpfecha = New System.Windows.Forms.DateTimePicker()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgdResultado = New System.Windows.Forms.DataGridView()
        Me.lblreportedet = New System.Windows.Forms.Label()
        Me.btnExporDMdet = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.lblaviso = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbconsulta.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgdResultado1)
        Me.GroupBox2.Controls.Add(Me.lblreporte)
        Me.GroupBox2.Controls.Add(Me.btnExporDM)
        Me.GroupBox2.Controls.Add(Me.dgvDMPMA)
        Me.GroupBox2.Location = New System.Drawing.Point(28, 274)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(707, 213)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'dgdResultado1
        '
        Me.dgdResultado1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdResultado1.Location = New System.Drawing.Point(14, 25)
        Me.dgdResultado1.Name = "dgdResultado1"
        Me.dgdResultado1.Size = New System.Drawing.Size(675, 148)
        Me.dgdResultado1.TabIndex = 259
        '
        'lblreporte
        '
        Me.lblreporte.AutoSize = True
        Me.lblreporte.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreporte.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblreporte.Location = New System.Drawing.Point(19, 186)
        Me.lblreporte.Name = "lblreporte"
        Me.lblreporte.Size = New System.Drawing.Size(249, 15)
        Me.lblreporte.TabIndex = 7
        Me.lblreporte.Text = "Reporte de derechos mineros superpuestos (Si/No)"
        '
        'btnExporDM
        '
        Me.btnExporDM.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnExporDM.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnExporDM.Location = New System.Drawing.Point(581, 180)
        Me.btnExporDM.Name = "btnExporDM"
        Me.btnExporDM.Size = New System.Drawing.Size(110, 27)
        Me.btnExporDM.TabIndex = 4
        Me.btnExporDM.Text = "Exportar a Excel"
        Me.btnExporDM.UseVisualStyleBackColor = False
        '
        'dgvDMPMA
        '
        Me.dgvDMPMA.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDMPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvDMPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDMPMA.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvDMPMA.Location = New System.Drawing.Point(15, 25)
        Me.dgvDMPMA.Name = "dgvDMPMA"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDMPMA.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvDMPMA.Size = New System.Drawing.Size(674, 148)
        Me.dgvDMPMA.TabIndex = 0
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnProcesar.ForeColor = System.Drawing.Color.White
        Me.btnProcesar.Location = New System.Drawing.Point(107, 228)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(513, 40)
        Me.btnProcesar.TabIndex = 8
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'dtpfecha_inicio
        '
        Me.dtpfecha_inicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha_inicio.Location = New System.Drawing.Point(393, 50)
        Me.dtpfecha_inicio.Name = "dtpfecha_inicio"
        Me.dtpfecha_inicio.Size = New System.Drawing.Size(96, 20)
        Me.dtpfecha_inicio.TabIndex = 257
        '
        'dtpfecha_fin
        '
        Me.dtpfecha_fin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha_fin.Location = New System.Drawing.Point(393, 76)
        Me.dtpfecha_fin.Name = "dtpfecha_fin"
        Me.dtpfecha_fin.Size = New System.Drawing.Size(96, 20)
        Me.dtpfecha_fin.TabIndex = 259
        '
        'lblfecha
        '
        Me.lblfecha.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha.Location = New System.Drawing.Point(18, 52)
        Me.lblfecha.Name = "lblfecha"
        Me.lblfecha.Size = New System.Drawing.Size(64, 21)
        Me.lblfecha.TabIndex = 262
        Me.lblfecha.Text = "Fecha :"
        Me.lblfecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfecha_inicio
        '
        Me.lblfecha_inicio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha_inicio.Location = New System.Drawing.Point(294, 50)
        Me.lblfecha_inicio.Name = "lblfecha_inicio"
        Me.lblfecha_inicio.Size = New System.Drawing.Size(93, 21)
        Me.lblfecha_inicio.TabIndex = 263
        Me.lblfecha_inicio.Text = "Fecha inicio :"
        Me.lblfecha_inicio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfecha_fin
        '
        Me.lblfecha_fin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha_fin.Location = New System.Drawing.Point(294, 75)
        Me.lblfecha_fin.Name = "lblfecha_fin"
        Me.lblfecha_fin.Size = New System.Drawing.Size(80, 21)
        Me.lblfecha_fin.TabIndex = 264
        Me.lblfecha_fin.Text = "Fecha fin :"
        Me.lblfecha_fin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblconsulta
        '
        Me.lblconsulta.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblconsulta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblconsulta.Location = New System.Drawing.Point(154, 9)
        Me.lblconsulta.Name = "lblconsulta"
        Me.lblconsulta.Size = New System.Drawing.Size(412, 21)
        Me.lblconsulta.TabIndex = 265
        Me.lblconsulta.Text = "Consulta de Derechos Mineros Superpuestos  (Si/No)"
        Me.lblconsulta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rbfecha
        '
        Me.rbfecha.AutoSize = True
        Me.rbfecha.Location = New System.Drawing.Point(6, 19)
        Me.rbfecha.Name = "rbfecha"
        Me.rbfecha.Size = New System.Drawing.Size(114, 17)
        Me.rbfecha.TabIndex = 268
        Me.rbfecha.TabStop = True
        Me.rbfecha.Text = "Consulta por fecha"
        Me.rbfecha.UseVisualStyleBackColor = True
        '
        'rbfechas
        '
        Me.rbfechas.AutoSize = True
        Me.rbfechas.Location = New System.Drawing.Point(286, 19)
        Me.rbfechas.Name = "rbfechas"
        Me.rbfechas.Size = New System.Drawing.Size(164, 17)
        Me.rbfechas.TabIndex = 269
        Me.rbfechas.TabStop = True
        Me.rbfechas.Text = "Consulta por rango de fechas"
        Me.rbfechas.UseVisualStyleBackColor = True
        '
        'gbconsulta
        '
        Me.gbconsulta.BackColor = System.Drawing.SystemColors.Control
        Me.gbconsulta.Controls.Add(Me.lblaviso)
        Me.gbconsulta.Controls.Add(Me.lblmensaje1)
        Me.gbconsulta.Controls.Add(Me.lblmensaje)
        Me.gbconsulta.Controls.Add(Me.dtpfecha)
        Me.gbconsulta.Controls.Add(Me.dtpfecha_fin)
        Me.gbconsulta.Controls.Add(Me.dtpfecha_inicio)
        Me.gbconsulta.Controls.Add(Me.lblfecha)
        Me.gbconsulta.Controls.Add(Me.lblfecha_fin)
        Me.gbconsulta.Controls.Add(Me.lblfecha_inicio)
        Me.gbconsulta.Controls.Add(Me.rbfechas)
        Me.gbconsulta.Controls.Add(Me.rbfecha)
        Me.gbconsulta.Location = New System.Drawing.Point(107, 58)
        Me.gbconsulta.Name = "gbconsulta"
        Me.gbconsulta.Size = New System.Drawing.Size(513, 164)
        Me.gbconsulta.TabIndex = 270
        Me.gbconsulta.TabStop = False
        Me.gbconsulta.Text = "Tipo de consulta"
        '
        'lblmensaje1
        '
        Me.lblmensaje1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmensaje1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblmensaje1.Location = New System.Drawing.Point(297, 96)
        Me.lblmensaje1.Name = "lblmensaje1"
        Me.lblmensaje1.Size = New System.Drawing.Size(77, 42)
        Me.lblmensaje1.TabIndex = 272
        Me.lblmensaje1.Text = "Ingrese rango de fechas de consulta"
        Me.lblmensaje1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblmensaje
        '
        Me.lblmensaje.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmensaje.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblmensaje.Location = New System.Drawing.Point(18, 73)
        Me.lblmensaje.Name = "lblmensaje"
        Me.lblmensaje.Size = New System.Drawing.Size(77, 32)
        Me.lblmensaje.TabIndex = 271
        Me.lblmensaje.Text = "Ingrese fecha de consulta"
        Me.lblmensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpfecha
        '
        Me.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha.Location = New System.Drawing.Point(117, 53)
        Me.dtpfecha.Name = "dtpfecha"
        Me.dtpfecha.Size = New System.Drawing.Size(96, 20)
        Me.dtpfecha.TabIndex = 270
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnCerrar.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnCerrar.Location = New System.Drawing.Point(43, 711)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(128, 27)
        Me.btnCerrar.TabIndex = 271
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgdResultado)
        Me.GroupBox1.Controls.Add(Me.lblreportedet)
        Me.GroupBox1.Controls.Add(Me.btnExporDMdet)
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Location = New System.Drawing.Point(28, 493)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(707, 212)
        Me.GroupBox1.TabIndex = 273
        Me.GroupBox1.TabStop = False
        '
        'dgdResultado
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgdResultado.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgdResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgdResultado.DefaultCellStyle = DataGridViewCellStyle13
        Me.dgdResultado.Location = New System.Drawing.Point(14, 25)
        Me.dgdResultado.Name = "dgdResultado"
        Me.dgdResultado.Size = New System.Drawing.Size(675, 148)
        Me.dgdResultado.TabIndex = 259
        '
        'lblreportedet
        '
        Me.lblreportedet.AutoSize = True
        Me.lblreportedet.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreportedet.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblreportedet.Location = New System.Drawing.Point(19, 186)
        Me.lblreportedet.Name = "lblreportedet"
        Me.lblreportedet.Size = New System.Drawing.Size(249, 15)
        Me.lblreportedet.TabIndex = 7
        Me.lblreportedet.Text = "Reporte de derechos mineros superpuestos (Si/No)"
        '
        'btnExporDMdet
        '
        Me.btnExporDMdet.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnExporDMdet.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnExporDMdet.Location = New System.Drawing.Point(581, 180)
        Me.btnExporDMdet.Name = "btnExporDMdet"
        Me.btnExporDMdet.Size = New System.Drawing.Size(110, 27)
        Me.btnExporDMdet.TabIndex = 4
        Me.btnExporDMdet.Text = "Exportar a Excel"
        Me.btnExporDMdet.UseVisualStyleBackColor = False
        '
        'DataGridView2
        '
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridView2.Location = New System.Drawing.Point(15, 25)
        Me.DataGridView2.Name = "DataGridView2"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.DataGridView2.Size = New System.Drawing.Size(674, 148)
        Me.DataGridView2.TabIndex = 0
        '
        'lblaviso
        '
        Me.lblaviso.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaviso.ForeColor = System.Drawing.Color.Brown
        Me.lblaviso.Location = New System.Drawing.Point(38, 138)
        Me.lblaviso.Name = "lblaviso"
        Me.lblaviso.Size = New System.Drawing.Size(432, 22)
        Me.lblaviso.TabIndex = 273
        Me.lblaviso.Text = "Después de ingresar fecha o rango de fechas haga click en botón Procesar"
        Me.lblaviso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frm_DMSuperpuestoDia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(747, 762)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.gbconsulta)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.lblconsulta)
        Me.Controls.Add(Me.GroupBox2)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Name = "frm_DMSuperpuestoDia"
        Me.Text = "frm_DMSuperpuestoDia"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbconsulta.ResumeLayout(False)
        Me.gbconsulta.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgdResultado1 As System.Windows.Forms.DataGridView
    Friend WithEvents lblreporte As System.Windows.Forms.Label
    Friend WithEvents btnExporDM As System.Windows.Forms.Button
    Friend WithEvents dgvDMPMA As System.Windows.Forms.DataGridView
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents dtpfecha_inicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpfecha_fin As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblfecha As System.Windows.Forms.Label
    Friend WithEvents lblfecha_inicio As System.Windows.Forms.Label
    Friend WithEvents lblfecha_fin As System.Windows.Forms.Label
    Friend WithEvents lblconsulta As System.Windows.Forms.Label
    Friend WithEvents rbfecha As System.Windows.Forms.RadioButton
    Friend WithEvents rbfechas As System.Windows.Forms.RadioButton
    Friend WithEvents gbconsulta As System.Windows.Forms.GroupBox
    Friend WithEvents lblmensaje1 As System.Windows.Forms.Label
    Friend WithEvents lblmensaje As System.Windows.Forms.Label
    Friend WithEvents dtpfecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgdResultado As System.Windows.Forms.DataGridView
    Friend WithEvents lblreportedet As System.Windows.Forms.Label
    Friend WithEvents btnExporDMdet As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents lblaviso As System.Windows.Forms.Label
End Class
