<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PPM_PMA
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvPMA = New System.Windows.Forms.DataGridView()
        Me.gboxPMA = New System.Windows.Forms.GroupBox()
        Me.btnExportPMA = New System.Windows.Forms.Button()
        Me.lblPMA = New System.Windows.Forms.Label()
        Me.gboxPPM = New System.Windows.Forms.GroupBox()
        Me.btnExportPPM = New System.Windows.Forms.Button()
        Me.lblPPM = New System.Windows.Forms.Label()
        Me.dgvPPM = New System.Windows.Forms.DataGridView()
        Me.gboxPPM_0 = New System.Windows.Forms.GroupBox()
        Me.btnExportPPM0 = New System.Windows.Forms.Button()
        Me.lblPPM_PMA = New System.Windows.Forms.Label()
        Me.dgvPPM0 = New System.Windows.Forms.DataGridView()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxPMA.SuspendLayout()
        Me.gboxPPM.SuspendLayout()
        CType(Me.dgvPPM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxPPM_0.SuspendLayout()
        CType(Me.dgvPPM0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPMA
        '
        Me.dgvPMA.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPMA.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPMA.Location = New System.Drawing.Point(8, 19)
        Me.dgvPMA.Name = "dgvPMA"
        Me.dgvPMA.Size = New System.Drawing.Size(641, 129)
        Me.dgvPMA.TabIndex = 174
        '
        'gboxPMA
        '
        Me.gboxPMA.Controls.Add(Me.btnExportPMA)
        Me.gboxPMA.Controls.Add(Me.lblPMA)
        Me.gboxPMA.Controls.Add(Me.dgvPMA)
        Me.gboxPMA.Location = New System.Drawing.Point(45, 27)
        Me.gboxPMA.Name = "gboxPMA"
        Me.gboxPMA.Size = New System.Drawing.Size(658, 176)
        Me.gboxPMA.TabIndex = 179
        Me.gboxPMA.TabStop = False
        Me.gboxPMA.Text = "Relación PMA"
        '
        'btnExportPMA
        '
        Me.btnExportPMA.BackColor = System.Drawing.Color.SkyBlue
        Me.btnExportPMA.ForeColor = System.Drawing.Color.Black
        Me.btnExportPMA.Location = New System.Drawing.Point(533, 149)
        Me.btnExportPMA.Name = "btnExportPMA"
        Me.btnExportPMA.Size = New System.Drawing.Size(116, 21)
        Me.btnExportPMA.TabIndex = 186
        Me.btnExportPMA.Text = "Exportar a Excel"
        Me.btnExportPMA.UseVisualStyleBackColor = False
        '
        'lblPMA
        '
        Me.lblPMA.AutoSize = True
        Me.lblPMA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMA.Location = New System.Drawing.Point(6, 153)
        Me.lblPMA.Name = "lblPMA"
        Me.lblPMA.Size = New System.Drawing.Size(365, 13)
        Me.lblPMA.TabIndex = 182
        Me.lblPMA.Text = "Relación de Titular-Conyuge con Calificación PMA que excede las 1000 ha."
        '
        'gboxPPM
        '
        Me.gboxPPM.Controls.Add(Me.btnExportPPM)
        Me.gboxPPM.Controls.Add(Me.lblPPM)
        Me.gboxPPM.Controls.Add(Me.dgvPPM)
        Me.gboxPPM.Location = New System.Drawing.Point(45, 209)
        Me.gboxPPM.Name = "gboxPPM"
        Me.gboxPPM.Size = New System.Drawing.Size(658, 176)
        Me.gboxPPM.TabIndex = 180
        Me.gboxPPM.TabStop = False
        Me.gboxPPM.Text = "Relación PPM"
        '
        'btnExportPPM
        '
        Me.btnExportPPM.BackColor = System.Drawing.Color.SkyBlue
        Me.btnExportPPM.ForeColor = System.Drawing.Color.Black
        Me.btnExportPPM.Location = New System.Drawing.Point(533, 149)
        Me.btnExportPPM.Name = "btnExportPPM"
        Me.btnExportPPM.Size = New System.Drawing.Size(116, 21)
        Me.btnExportPPM.TabIndex = 185
        Me.btnExportPPM.Text = "Exportar a Excel"
        Me.btnExportPPM.UseVisualStyleBackColor = False
        '
        'lblPPM
        '
        Me.lblPPM.AutoSize = True
        Me.lblPPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPPM.Location = New System.Drawing.Point(6, 151)
        Me.lblPPM.Name = "lblPPM"
        Me.lblPPM.Size = New System.Drawing.Size(365, 13)
        Me.lblPPM.TabIndex = 183
        Me.lblPPM.Text = "Relación de Titular-Conyuge con Calificación PPM que excede las 2000 ha."
        '
        'dgvPPM
        '
        Me.dgvPPM.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPPM.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPPM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPPM.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPPM.Location = New System.Drawing.Point(8, 19)
        Me.dgvPPM.Name = "dgvPPM"
        Me.dgvPPM.Size = New System.Drawing.Size(641, 129)
        Me.dgvPPM.TabIndex = 174
        '
        'gboxPPM_0
        '
        Me.gboxPPM_0.Controls.Add(Me.btnExportPPM0)
        Me.gboxPPM_0.Controls.Add(Me.lblPPM_PMA)
        Me.gboxPPM_0.Controls.Add(Me.dgvPPM0)
        Me.gboxPPM_0.Location = New System.Drawing.Point(45, 401)
        Me.gboxPPM_0.Name = "gboxPPM_0"
        Me.gboxPPM_0.Size = New System.Drawing.Size(658, 176)
        Me.gboxPPM_0.TabIndex = 181
        Me.gboxPPM_0.TabStop = False
        Me.gboxPPM_0.Text = "Relación PMA/PPM = 0"
        '
        'btnExportPPM0
        '
        Me.btnExportPPM0.BackColor = System.Drawing.Color.SkyBlue
        Me.btnExportPPM0.ForeColor = System.Drawing.Color.Black
        Me.btnExportPPM0.Location = New System.Drawing.Point(533, 151)
        Me.btnExportPPM0.Name = "btnExportPPM0"
        Me.btnExportPPM0.Size = New System.Drawing.Size(116, 21)
        Me.btnExportPPM0.TabIndex = 187
        Me.btnExportPPM0.Text = "Exportar a Excel"
        Me.btnExportPPM0.UseVisualStyleBackColor = False
        '
        'lblPPM_PMA
        '
        Me.lblPPM_PMA.AutoSize = True
        Me.lblPPM_PMA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPPM_PMA.Location = New System.Drawing.Point(6, 151)
        Me.lblPPM_PMA.Name = "lblPPM_PMA"
        Me.lblPPM_PMA.Size = New System.Drawing.Size(287, 13)
        Me.lblPPM_PMA.TabIndex = 183
        Me.lblPPM_PMA.Text = "Relación de Titulares con Calificación PMA/PPM con 0 ha."
        '
        'dgvPPM0
        '
        Me.dgvPPM0.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPPM0.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPPM0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPPM0.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPPM0.Location = New System.Drawing.Point(8, 19)
        Me.dgvPPM0.Name = "dgvPPM0"
        Me.dgvPPM0.Size = New System.Drawing.Size(641, 129)
        Me.dgvPPM0.TabIndex = 174
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnProcesar.ForeColor = System.Drawing.Color.White
        Me.btnProcesar.Location = New System.Drawing.Point(533, 592)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(170, 27)
        Me.btnProcesar.TabIndex = 183
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnCerrar.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnCerrar.Location = New System.Drawing.Point(45, 592)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(170, 27)
        Me.btnCerrar.TabIndex = 184
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'frm_PPM_PMA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 657)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.gboxPPM_0)
        Me.Controls.Add(Me.gboxPPM)
        Me.Controls.Add(Me.gboxPMA)
        Me.Name = "frm_PPM_PMA"
        Me.Text = "Formulario Titulares con Calificación PMA/PPM"
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxPMA.ResumeLayout(False)
        Me.gboxPMA.PerformLayout()
        Me.gboxPPM.ResumeLayout(False)
        Me.gboxPPM.PerformLayout()
        CType(Me.dgvPPM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxPPM_0.ResumeLayout(False)
        Me.gboxPPM_0.PerformLayout()
        CType(Me.dgvPPM0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvPMA As System.Windows.Forms.DataGridView
    Friend WithEvents gboxPMA As System.Windows.Forms.GroupBox
    Friend WithEvents lblPMA As System.Windows.Forms.Label
    Friend WithEvents gboxPPM As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPPM As System.Windows.Forms.DataGridView
    Friend WithEvents gboxPPM_0 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPPM0 As System.Windows.Forms.DataGridView
    Friend WithEvents lblPPM As System.Windows.Forms.Label
    Friend WithEvents lblPPM_PMA As System.Windows.Forms.Label
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnExportPMA As System.Windows.Forms.Button
    Friend WithEvents btnExportPPM As System.Windows.Forms.Button
    Friend WithEvents btnExportPPM0 As System.Windows.Forms.Button
End Class
