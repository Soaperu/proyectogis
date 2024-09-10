<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PMA
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
        Me.dgvDMPMA = New System.Windows.Forms.DataGridView()
        Me.dgvPMA = New System.Windows.Forms.DataGridView()
        Me.btnExporDMPMA = New System.Windows.Forms.Button()
        Me.btnExporPMA = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboDatum = New System.Windows.Forms.ComboBox()
        Me.txtDatum = New System.Windows.Forms.Label()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgdResultado1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dgdResultado = New System.Windows.Forms.DataGridView()
        Me.btnCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDMPMA
        '
        Me.dgvDMPMA.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDMPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDMPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDMPMA.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDMPMA.Location = New System.Drawing.Point(15, 25)
        Me.dgvDMPMA.Name = "dgvDMPMA"
        Me.dgvDMPMA.Size = New System.Drawing.Size(586, 148)
        Me.dgvDMPMA.TabIndex = 0
        '
        'dgvPMA
        '
        Me.dgvPMA.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPMA.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPMA.Location = New System.Drawing.Point(15, 15)
        Me.dgvPMA.Name = "dgvPMA"
        Me.dgvPMA.Size = New System.Drawing.Size(587, 148)
        Me.dgvPMA.TabIndex = 1
        '
        'btnExporDMPMA
        '
        Me.btnExporDMPMA.Location = New System.Drawing.Point(502, 180)
        Me.btnExporDMPMA.Name = "btnExporDMPMA"
        Me.btnExporDMPMA.Size = New System.Drawing.Size(99, 27)
        Me.btnExporDMPMA.TabIndex = 4
        Me.btnExporDMPMA.Text = "Exportar a Excel"
        Me.btnExporDMPMA.UseVisualStyleBackColor = True
        '
        'btnExporPMA
        '
        Me.btnExporPMA.Location = New System.Drawing.Point(503, 169)
        Me.btnExporPMA.Name = "btnExporPMA"
        Me.btnExporPMA.Size = New System.Drawing.Size(99, 27)
        Me.btnExporPMA.TabIndex = 5
        Me.btnExporPMA.Text = "Exportar a Excel"
        Me.btnExporPMA.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(18, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Relación de derechos mineros con calificación PMA fuera de la demarcación calific" & _
    "ada"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(19, 186)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(266, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Relación de derechos mineros con calificación PMA"
        '
        'cboDatum
        '
        Me.cboDatum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDatum.FormattingEnabled = True
        Me.cboDatum.Items.AddRange(New Object() {"WGS-84", "PSAD-56"})
        Me.cboDatum.Location = New System.Drawing.Point(121, 23)
        Me.cboDatum.Name = "cboDatum"
        Me.cboDatum.Size = New System.Drawing.Size(97, 21)
        Me.cboDatum.TabIndex = 2
        '
        'txtDatum
        '
        Me.txtDatum.AutoSize = True
        Me.txtDatum.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatum.ForeColor = System.Drawing.SystemColors.Highlight
        Me.txtDatum.Location = New System.Drawing.Point(19, 26)
        Me.txtDatum.Name = "txtDatum"
        Me.txtDatum.Size = New System.Drawing.Size(95, 15)
        Me.txtDatum.TabIndex = 3
        Me.txtDatum.Text = "Elegir Sistema :"
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnProcesar.ForeColor = System.Drawing.Color.White
        Me.btnProcesar.Location = New System.Drawing.Point(433, 20)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(170, 27)
        Me.btnProcesar.TabIndex = 8
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnProcesar)
        Me.GroupBox1.Controls.Add(Me.txtDatum)
        Me.GroupBox1.Controls.Add(Me.cboDatum)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(614, 60)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgdResultado1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnExporDMPMA)
        Me.GroupBox2.Controls.Add(Me.dgvDMPMA)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 78)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(612, 213)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'dgdResultado1
        '
        Me.dgdResultado1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdResultado1.Location = New System.Drawing.Point(14, 25)
        Me.dgdResultado1.Name = "dgdResultado1"
        Me.dgdResultado1.Size = New System.Drawing.Size(587, 148)
        Me.dgdResultado1.TabIndex = 259
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.dgdResultado)
        Me.GroupBox3.Controls.Add(Me.btnExporPMA)
        Me.GroupBox3.Controls.Add(Me.dgvPMA)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 298)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(613, 209)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        '
        'dgdResultado
        '
        Me.dgdResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdResultado.Location = New System.Drawing.Point(15, 15)
        Me.dgdResultado.Name = "dgdResultado"
        Me.dgdResultado.Size = New System.Drawing.Size(587, 148)
        Me.dgdResultado.TabIndex = 260
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnCerrar.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnCerrar.Location = New System.Drawing.Point(26, 515)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(170, 27)
        Me.btnCerrar.TabIndex = 9
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'frm_PMA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(637, 556)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_PMA"
        Me.Text = "Reporte de derechos mineros PMA"
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvDMPMA As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPMA As System.Windows.Forms.DataGridView
    Friend WithEvents btnExporDMPMA As System.Windows.Forms.Button
    Friend WithEvents btnExporPMA As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboDatum As System.Windows.Forms.ComboBox
    Friend WithEvents txtDatum As System.Windows.Forms.Label
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dgdResultado1 As System.Windows.Forms.DataGridView
    Friend WithEvents dgdResultado As System.Windows.Forms.DataGridView
End Class
