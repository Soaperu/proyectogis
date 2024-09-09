<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl_CheckListBoxAutores
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.clb_uc_autores = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'clb_uc_autores
        '
        Me.clb_uc_autores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clb_uc_autores.FormattingEnabled = True
        Me.clb_uc_autores.Location = New System.Drawing.Point(0, 0)
        Me.clb_uc_autores.Name = "clb_uc_autores"
        Me.clb_uc_autores.Size = New System.Drawing.Size(372, 349)
        Me.clb_uc_autores.TabIndex = 0
        '
        'UserControl_autores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.clb_uc_autores)
        Me.Name = "UserControl_autores"
        Me.Size = New System.Drawing.Size(372, 349)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents clb_uc_autores As System.Windows.Forms.CheckedListBox
End Class
