<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl_ComboBoxRegiones
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ucr_region = New System.Windows.Forms.Label()
        Me.cbx_ucr_region = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_ucr_region, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_ucr_region, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(400, 55)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lbl_ucr_region
        '
        Me.lbl_ucr_region.AutoSize = True
        Me.lbl_ucr_region.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_ucr_region.Location = New System.Drawing.Point(3, 3)
        Me.lbl_ucr_region.Name = "lbl_ucr_region"
        Me.lbl_ucr_region.Size = New System.Drawing.Size(394, 17)
        Me.lbl_ucr_region.TabIndex = 0
        Me.lbl_ucr_region.Text = "Seleccione una región"
        '
        'cbx_ucr_region
        '
        Me.cbx_ucr_region.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_ucr_region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_ucr_region.FormattingEnabled = True
        Me.cbx_ucr_region.Location = New System.Drawing.Point(3, 25)
        Me.cbx_ucr_region.Name = "cbx_ucr_region"
        Me.cbx_ucr_region.Size = New System.Drawing.Size(394, 24)
        Me.cbx_ucr_region.TabIndex = 1
        '
        'UserControl_ComboBoxRegiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "UserControl_ComboBoxRegiones"
        Me.Size = New System.Drawing.Size(400, 55)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_ucr_region As System.Windows.Forms.Label
    Friend WithEvents cbx_ucr_region As System.Windows.Forms.ComboBox
End Class
