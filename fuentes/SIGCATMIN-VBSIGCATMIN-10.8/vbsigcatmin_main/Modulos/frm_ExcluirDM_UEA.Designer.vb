<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExcluirDM_UEA
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtUEA = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvExcluidosUEA = New System.Windows.Forms.DataGridView()
        Me.btnGraficarEx = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvExcluidosUEA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtUEA)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(386, 79)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'txtUEA
        '
        Me.txtUEA.Enabled = False
        Me.txtUEA.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUEA.ForeColor = System.Drawing.SystemColors.Highlight
        Me.txtUEA.Location = New System.Drawing.Point(103, 20)
        Me.txtUEA.Name = "txtUEA"
        Me.txtUEA.Size = New System.Drawing.Size(106, 21)
        Me.txtUEA.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "UEA Consultada:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvExcluidosUEA)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 91)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(383, 264)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'dgvExcluidosUEA
        '
        Me.dgvExcluidosUEA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExcluidosUEA.Location = New System.Drawing.Point(10, 29)
        Me.dgvExcluidosUEA.Name = "dgvExcluidosUEA"
        Me.dgvExcluidosUEA.Size = New System.Drawing.Size(367, 218)
        Me.dgvExcluidosUEA.TabIndex = 0
        '
        'btnGraficarEx
        '
        Me.btnGraficarEx.Location = New System.Drawing.Point(22, 365)
        Me.btnGraficarEx.Name = "btnGraficarEx"
        Me.btnGraficarEx.Size = New System.Drawing.Size(87, 30)
        Me.btnGraficarEx.TabIndex = 2
        Me.btnGraficarEx.Text = "Graficar"
        Me.btnGraficarEx.UseVisualStyleBackColor = True
        '
        'frm_ExcluirDM_UEA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 412)
        Me.Controls.Add(Me.btnGraficarEx)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_ExcluirDM_UEA"
        Me.Text = "Excluir DM de la UEA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvExcluidosUEA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUEA As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvExcluidosUEA As System.Windows.Forms.DataGridView
    Friend WithEvents btnGraficarEx As System.Windows.Forms.Button
End Class
