<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHerramientasDesarrollador
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tlp_tc1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_estilos = New System.Windows.Forms.TextBox()
        Me.btn_load_style = New System.Windows.Forms.Button()
        Me.btn_apply_style = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.UserControl_FeatureLayerCbx1 = New Automapic.UserControl_FeatureLayerCbx()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.tlp_tc1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(324, 577)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(8, 43)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(308, 411)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.tlp_tc1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(300, 385)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Aplicar estilos"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'tlp_tc1
        '
        Me.tlp_tc1.ColumnCount = 2
        Me.tlp_tc1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_tc1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlp_tc1.Controls.Add(Me.Label2, 0, 0)
        Me.tlp_tc1.Controls.Add(Me.Label3, 0, 2)
        Me.tlp_tc1.Controls.Add(Me.tbx_estilos, 0, 3)
        Me.tlp_tc1.Controls.Add(Me.btn_load_style, 1, 3)
        Me.tlp_tc1.Controls.Add(Me.btn_apply_style, 0, 5)
        Me.tlp_tc1.Controls.Add(Me.UserControl_FeatureLayerCbx1, 0, 1)
        Me.tlp_tc1.Controls.Add(Me.GroupBox1, 0, 4)
        Me.tlp_tc1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_tc1.Location = New System.Drawing.Point(3, 3)
        Me.tlp_tc1.Name = "tlp_tc1"
        Me.tlp_tc1.RowCount = 7
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlp_tc1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_tc1.Size = New System.Drawing.Size(294, 379)
        Me.tlp_tc1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(248, 26)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Seleccionar capa a aplicar estilos"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(3, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(248, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Seleccionar archivo de estilos"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'tbx_estilos
        '
        Me.tbx_estilos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_estilos.Enabled = False
        Me.tbx_estilos.Location = New System.Drawing.Point(3, 77)
        Me.tbx_estilos.Name = "tbx_estilos"
        Me.tbx_estilos.Size = New System.Drawing.Size(248, 20)
        Me.tbx_estilos.TabIndex = 2
        '
        'btn_load_style
        '
        Me.btn_load_style.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_load_style.Location = New System.Drawing.Point(257, 77)
        Me.btn_load_style.Name = "btn_load_style"
        Me.btn_load_style.Size = New System.Drawing.Size(34, 20)
        Me.btn_load_style.TabIndex = 3
        Me.btn_load_style.Text = "..."
        Me.btn_load_style.UseVisualStyleBackColor = True
        '
        'btn_apply_style
        '
        Me.tlp_tc1.SetColumnSpan(Me.btn_apply_style, 3)
        Me.btn_apply_style.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_apply_style.Location = New System.Drawing.Point(3, 153)
        Me.btn_apply_style.Name = "btn_apply_style"
        Me.btn_apply_style.Size = New System.Drawing.Size(288, 24)
        Me.btn_apply_style.TabIndex = 4
        Me.btn_apply_style.Text = "Aplicar estilo"
        Me.btn_apply_style.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.tlp_tc1.SetColumnSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 103)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(288, 44)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Seleccionar etiqueta"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(146, 19)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(114, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "Nombre Formación"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(29, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(64, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Etiqueta"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(8, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(308, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Herramientas para desarrolladores"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Esri Style|*style|Todos los archivos|*.*"
        '
        'UserControl_FeatureLayerCbx1
        '
        Me.UserControl_FeatureLayerCbx1.BackColor = System.Drawing.Color.White
        Me.UserControl_FeatureLayerCbx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_FeatureLayerCbx1.Location = New System.Drawing.Point(3, 29)
        Me.UserControl_FeatureLayerCbx1.Name = "UserControl_FeatureLayerCbx1"
        Me.UserControl_FeatureLayerCbx1.Size = New System.Drawing.Size(248, 22)
        Me.UserControl_FeatureLayerCbx1.TabIndex = 5
        '
        'FormHerramientasDesarrollador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(324, 577)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormHerramientasDesarrollador"
        Me.Text = "FormHerramientasDesarrollador"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.tlp_tc1.ResumeLayout(False)
        Me.tlp_tc1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents tlp_tc1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_estilos As System.Windows.Forms.TextBox
    Friend WithEvents btn_load_style As System.Windows.Forms.Button
    Friend WithEvents btn_apply_style As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UserControl_FeatureLayerCbx1 As UserControl_FeatureLayerCbx
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
