<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_datos_campo_dgar
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
        Me.UserControl_CheckBoxAddLayers1 = New Automapic.UserControl_CheckBoxAddLayers()
        Me.SuspendLayout()
        '
        'UserControl_CheckBoxAddLayers1
        '
        Me.UserControl_CheckBoxAddLayers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_CheckBoxAddLayers1.Location = New System.Drawing.Point(0, 0)
        Me.UserControl_CheckBoxAddLayers1.Margin = New System.Windows.Forms.Padding(4)
        Me.UserControl_CheckBoxAddLayers1.Name = "UserControl_CheckBoxAddLayers1"
        Me.UserControl_CheckBoxAddLayers1.Size = New System.Drawing.Size(431, 550)
        Me.UserControl_CheckBoxAddLayers1.TabIndex = 0
        '
        'Form_datos_campo_dgar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(431, 550)
        Me.Controls.Add(Me.UserControl_CheckBoxAddLayers1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_datos_campo_dgar"
        Me.Text = "Form_datos_campo_dgar"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UserControl_CheckBoxAddLayers1 As UserControl_CheckBoxAddLayers
End Class
