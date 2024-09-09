<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl_FeatureLayerCbx
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.ComboBoxLayers = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ComboBoxLayers
        '
        Me.ComboBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxLayers.FormattingEnabled = True
        Me.ComboBoxLayers.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxLayers.Name = "ComboBoxLayers"
        Me.ComboBoxLayers.Size = New System.Drawing.Size(259, 21)
        Me.ComboBoxLayers.TabIndex = 0
        '
        'UserControl_FeatureLayerCbx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.ComboBoxLayers)
        Me.Name = "UserControl_FeatureLayerCbx"
        Me.Size = New System.Drawing.Size(259, 24)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ComboBoxLayers As System.Windows.Forms.ComboBox
End Class
