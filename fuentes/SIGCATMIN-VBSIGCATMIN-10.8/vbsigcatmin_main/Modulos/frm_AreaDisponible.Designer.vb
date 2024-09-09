<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_AreaDisponible
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgdResultado1 = New System.Windows.Forms.DataGridView()
        Me.btnExpFC = New System.Windows.Forms.Button()
        Me.dgvPMA = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgdResultado = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExpAreaDispDM = New System.Windows.Forms.Button()
        Me.dgvDMPMA = New System.Windows.Forms.DataGridView()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnCerrar.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.btnCerrar.Location = New System.Drawing.Point(29, 450)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(170, 27)
        Me.btnCerrar.TabIndex = 12
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.dgdResultado1)
        Me.GroupBox3.Controls.Add(Me.btnExpFC)
        Me.GroupBox3.Controls.Add(Me.dgvPMA)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 233)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(752, 209)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(18, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Tipo de hectarea de los Derechos Mineros del Catastro"
        '
        'dgdResultado1
        '
        Me.dgdResultado1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdResultado1.Location = New System.Drawing.Point(15, 15)
        Me.dgdResultado1.Name = "dgdResultado1"
        Me.dgdResultado1.Size = New System.Drawing.Size(730, 148)
        Me.dgdResultado1.TabIndex = 260
        '
        'btnExpFC
        '
        Me.btnExpFC.Location = New System.Drawing.Point(581, 169)
        Me.btnExpFC.Name = "btnExpFC"
        Me.btnExpFC.Size = New System.Drawing.Size(164, 27)
        Me.btnExpFC.TabIndex = 5
        Me.btnExpFC.Text = "Exportar a Excel"
        Me.btnExpFC.UseVisualStyleBackColor = True
        '
        'dgvPMA
        '
        Me.dgvPMA.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPMA.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPMA.Location = New System.Drawing.Point(15, 15)
        Me.dgvPMA.Name = "dgvPMA"
        Me.dgvPMA.Size = New System.Drawing.Size(730, 148)
        Me.dgvPMA.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.dgdResultado)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnExpAreaDispDM)
        Me.GroupBox2.Controls.Add(Me.dgvDMPMA)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(751, 213)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        '
        'dgdResultado
        '
        Me.dgdResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdResultado.Location = New System.Drawing.Point(14, 25)
        Me.dgdResultado.Name = "dgdResultado"
        Me.dgdResultado.Size = New System.Drawing.Size(730, 148)
        Me.dgdResultado.TabIndex = 259
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(19, 186)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(260, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Área Disponible de Derechos Mineros por Calcular"
        '
        'btnExpAreaDispDM
        '
        Me.btnExpAreaDispDM.Location = New System.Drawing.Point(581, 180)
        Me.btnExpAreaDispDM.Name = "btnExpAreaDispDM"
        Me.btnExpAreaDispDM.Size = New System.Drawing.Size(165, 27)
        Me.btnExpAreaDispDM.TabIndex = 4
        Me.btnExpAreaDispDM.Text = "Exportar a Excel"
        Me.btnExpAreaDispDM.UseVisualStyleBackColor = True
        '
        'dgvDMPMA
        '
        Me.dgvDMPMA.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDMPMA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvDMPMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDMPMA.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvDMPMA.Location = New System.Drawing.Point(15, 25)
        Me.dgvDMPMA.Name = "dgvDMPMA"
        Me.dgvDMPMA.Size = New System.Drawing.Size(729, 148)
        Me.dgvDMPMA.TabIndex = 0
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.GrayText
        Me.btnProcesar.ForeColor = System.Drawing.Color.White
        Me.btnProcesar.Location = New System.Drawing.Point(589, 450)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(170, 27)
        Me.btnProcesar.TabIndex = 8
        Me.btnProcesar.Text = "Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'frm_AreaDisponible
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(781, 492)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "frm_AreaDisponible"
        Me.Text = "frm_AreaDisponible"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgdResultado1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPMA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgdResultado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDMPMA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgdResultado1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnExpFC As System.Windows.Forms.Button
    Friend WithEvents dgvPMA As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgdResultado As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExpAreaDispDM As System.Windows.Forms.Button
    Friend WithEvents dgvDMPMA As System.Windows.Forms.DataGridView
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
End Class
