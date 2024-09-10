<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ueas_consulta_nombre
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ueas_consulta_nombre))
        Me.dg_seleccionar_uea = New System.Windows.Forms.DataGridView()
        Me.Label_seleccion_uea = New System.Windows.Forms.Label()
        CType(Me.dg_seleccionar_uea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dg_seleccionar_uea
        '
        Me.dg_seleccionar_uea.AllowUserToDeleteRows = False
        Me.dg_seleccionar_uea.AllowUserToOrderColumns = True
        Me.dg_seleccionar_uea.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dg_seleccionar_uea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dg_seleccionar_uea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg_seleccionar_uea.Location = New System.Drawing.Point(20, 60)
        Me.dg_seleccionar_uea.Name = "dg_seleccionar_uea"
        Me.dg_seleccionar_uea.ReadOnly = True
        Me.dg_seleccionar_uea.RowHeadersVisible = False
        Me.dg_seleccionar_uea.Size = New System.Drawing.Size(795, 380)
        Me.dg_seleccionar_uea.TabIndex = 0
        '
        'Label_seleccion_uea
        '
        Me.Label_seleccion_uea.AutoSize = True
        Me.Label_seleccion_uea.Location = New System.Drawing.Point(17, 28)
        Me.Label_seleccion_uea.Name = "Label_seleccion_uea"
        Me.Label_seleccion_uea.Size = New System.Drawing.Size(558, 13)
        Me.Label_seleccion_uea.TabIndex = 1
        Me.Label_seleccion_uea.Text = "Se muestra todas las coincidencias encontradas; por favor seleccione haciendo dob" & _
    "le clic sobre el registro de interes"
        '
        'form_ueas_consulta_nombre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 462)
        Me.Controls.Add(Me.Label_seleccion_uea)
        Me.Controls.Add(Me.dg_seleccionar_uea)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "form_ueas_consulta_nombre"
        Me.Text = "Coincidencias por el nombre de UEA Ingresado"
        CType(Me.dg_seleccionar_uea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dg_seleccionar_uea As System.Windows.Forms.DataGridView
    Friend WithEvents Label_seleccion_uea As System.Windows.Forms.Label
End Class
