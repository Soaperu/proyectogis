<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl_ComboBoxDataframes
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
        Me.tlp_uc_dataframe = New System.Windows.Forms.TableLayoutPanel()
        Me.cbx_uc_dataframes = New System.Windows.Forms.ComboBox()
        Me.lbl_uc_dataframes = New System.Windows.Forms.Label()
        Me.tlp_uc_dataframe.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlp_uc_dataframe
        '
        Me.tlp_uc_dataframe.ColumnCount = 1
        Me.tlp_uc_dataframe.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_uc_dataframe.Controls.Add(Me.cbx_uc_dataframes, 0, 1)
        Me.tlp_uc_dataframe.Controls.Add(Me.lbl_uc_dataframes, 0, 0)
        Me.tlp_uc_dataframe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_uc_dataframe.Location = New System.Drawing.Point(0, 0)
        Me.tlp_uc_dataframe.Name = "tlp_uc_dataframe"
        Me.tlp_uc_dataframe.RowCount = 2
        Me.tlp_uc_dataframe.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlp_uc_dataframe.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp_uc_dataframe.Size = New System.Drawing.Size(400, 50)
        Me.tlp_uc_dataframe.TabIndex = 0
        '
        'cbx_uc_dataframes
        '
        Me.cbx_uc_dataframes.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_uc_dataframes.BackColor = System.Drawing.SystemColors.Control
        Me.cbx_uc_dataframes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_uc_dataframes.FormattingEnabled = True
        Me.cbx_uc_dataframes.Location = New System.Drawing.Point(3, 23)
        Me.cbx_uc_dataframes.Name = "cbx_uc_dataframes"
        Me.cbx_uc_dataframes.Size = New System.Drawing.Size(394, 24)
        Me.cbx_uc_dataframes.TabIndex = 0
        '
        'lbl_uc_dataframes
        '
        Me.lbl_uc_dataframes.AutoSize = True
        Me.lbl_uc_dataframes.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_uc_dataframes.Location = New System.Drawing.Point(3, 3)
        Me.lbl_uc_dataframes.Name = "lbl_uc_dataframes"
        Me.lbl_uc_dataframes.Size = New System.Drawing.Size(394, 17)
        Me.lbl_uc_dataframes.TabIndex = 1
        Me.lbl_uc_dataframes.Text = "Seleccione dataframe"
        '
        'UserControl_ComboBoxDataframes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlp_uc_dataframe)
        Me.Name = "UserControl_ComboBoxDataframes"
        Me.Size = New System.Drawing.Size(400, 50)
        Me.tlp_uc_dataframe.ResumeLayout(False)
        Me.tlp_uc_dataframe.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlp_uc_dataframe As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbx_uc_dataframes As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_uc_dataframes As System.Windows.Forms.Label
End Class
