<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_mapa_neotectonica
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rdb_mn_horizontal = New System.Windows.Forms.RadioButton()
        Me.nud_mn_escala = New System.Windows.Forms.NumericUpDown()
        Me.btn_mn_mapa = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbx_mn_region = New System.Windows.Forms.ComboBox()
        Me.rdb_mn_vertical = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbx_mn_zona = New System.Windows.Forms.ComboBox()
        Me.lbl_mgp_numero_registros = New System.Windows.Forms.Label()
        Me.nud_mgp_n_registros = New System.Windows.Forms.NumericUpDown()
        Me.UserControl_CheckListBoxAutores1 = New Automapic.UserControl_CheckListBoxAutores()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.UserControl_CheckBoxAddLayers1 = New Automapic.UserControl_CheckBoxAddLayers()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.nud_mn_escala, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_mgp_n_registros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.rdb_mn_horizontal, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.nud_mn_escala, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mn_mapa, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mn_region, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rdb_mn_vertical, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mn_zona, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mgp_numero_registros, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.nud_mgp_n_registros, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.UserControl_CheckListBoxAutores1, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.UserControl_CheckBoxAddLayers1, 0, 12)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 17
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(291, 596)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(2, 55)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Seleccione orientación (opcional)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label2, 3)
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(2, 103)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(287, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Defina una escala (opcional)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label4, 3)
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(2, 151)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(287, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Autores"
        '
        'rdb_mn_horizontal
        '
        Me.rdb_mn_horizontal.AutoSize = True
        Me.rdb_mn_horizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_mn_horizontal.Location = New System.Drawing.Point(2, 70)
        Me.rdb_mn_horizontal.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rdb_mn_horizontal.Name = "rdb_mn_horizontal"
        Me.rdb_mn_horizontal.Size = New System.Drawing.Size(85, 20)
        Me.rdb_mn_horizontal.TabIndex = 5
        Me.rdb_mn_horizontal.Text = "Horizontal"
        Me.rdb_mn_horizontal.UseVisualStyleBackColor = True
        '
        'nud_mn_escala
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.nud_mn_escala, 3)
        Me.nud_mn_escala.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nud_mn_escala.Increment = New Decimal(New Integer() {25000, 0, 0, 0})
        Me.nud_mn_escala.Location = New System.Drawing.Point(2, 118)
        Me.nud_mn_escala.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.nud_mn_escala.Maximum = New Decimal(New Integer() {2000000, 0, 0, 0})
        Me.nud_mn_escala.Minimum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nud_mn_escala.Name = "nud_mn_escala"
        Me.nud_mn_escala.Size = New System.Drawing.Size(287, 20)
        Me.nud_mn_escala.TabIndex = 7
        Me.nud_mn_escala.Value = New Decimal(New Integer() {100000, 0, 0, 0})
        '
        'btn_mn_mapa
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_mn_mapa, 3)
        Me.btn_mn_mapa.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mn_mapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mn_mapa.Enabled = False
        Me.btn_mn_mapa.Image = Global.Automapic.My.Resources.Resources.MapRange16
        Me.btn_mn_mapa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mn_mapa.Location = New System.Drawing.Point(2, 562)
        Me.btn_mn_mapa.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btn_mn_mapa.Name = "btn_mn_mapa"
        Me.btn_mn_mapa.Size = New System.Drawing.Size(287, 32)
        Me.btn_mn_mapa.TabIndex = 10
        Me.btn_mn_mapa.Text = "Generar mapa"
        Me.btn_mn_mapa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mn_mapa.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label5, 3)
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(2, 7)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(287, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Seleccione una región"
        '
        'cbx_mn_region
        '
        Me.cbx_mn_region.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_mn_region, 3)
        Me.cbx_mn_region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mn_region.FormattingEnabled = True
        Me.cbx_mn_region.Location = New System.Drawing.Point(2, 22)
        Me.cbx_mn_region.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbx_mn_region.Name = "cbx_mn_region"
        Me.cbx_mn_region.Size = New System.Drawing.Size(287, 21)
        Me.cbx_mn_region.TabIndex = 12
        '
        'rdb_mn_vertical
        '
        Me.rdb_mn_vertical.AutoSize = True
        Me.rdb_mn_vertical.Location = New System.Drawing.Point(91, 70)
        Me.rdb_mn_vertical.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rdb_mn_vertical.Name = "rdb_mn_vertical"
        Me.rdb_mn_vertical.Size = New System.Drawing.Size(60, 17)
        Me.rdb_mn_vertical.TabIndex = 6
        Me.rdb_mn_vertical.Text = "Vertical"
        Me.rdb_mn_vertical.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(218, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Zona (opc)"
        '
        'cbx_mn_zona
        '
        Me.cbx_mn_zona.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mn_zona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mn_zona.FormattingEnabled = True
        Me.cbx_mn_zona.Items.AddRange(New Object() {"17", "18", "19"})
        Me.cbx_mn_zona.Location = New System.Drawing.Point(218, 70)
        Me.cbx_mn_zona.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbx_mn_zona.Name = "cbx_mn_zona"
        Me.cbx_mn_zona.Size = New System.Drawing.Size(71, 21)
        Me.cbx_mn_zona.TabIndex = 14
        '
        'lbl_mgp_numero_registros
        '
        Me.lbl_mgp_numero_registros.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_mgp_numero_registros, 3)
        Me.lbl_mgp_numero_registros.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mgp_numero_registros.Location = New System.Drawing.Point(2, 519)
        Me.lbl_mgp_numero_registros.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mgp_numero_registros.Name = "lbl_mgp_numero_registros"
        Me.lbl_mgp_numero_registros.Size = New System.Drawing.Size(287, 13)
        Me.lbl_mgp_numero_registros.TabIndex = 16
        Me.lbl_mgp_numero_registros.Text = "Registros por cuadro de interes geológico (opcional)"
        '
        'nud_mgp_n_registros
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.nud_mgp_n_registros, 3)
        Me.nud_mgp_n_registros.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nud_mgp_n_registros.Location = New System.Drawing.Point(2, 534)
        Me.nud_mgp_n_registros.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.nud_mgp_n_registros.Name = "nud_mgp_n_registros"
        Me.nud_mgp_n_registros.Size = New System.Drawing.Size(287, 20)
        Me.nud_mgp_n_registros.TabIndex = 17
        '
        'UserControl_CheckListBoxAutores1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.UserControl_CheckListBoxAutores1, 3)
        Me.UserControl_CheckListBoxAutores1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_CheckListBoxAutores1.Location = New System.Drawing.Point(2, 166)
        Me.UserControl_CheckListBoxAutores1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UserControl_CheckListBoxAutores1.Name = "UserControl_CheckListBoxAutores1"
        Me.UserControl_CheckListBoxAutores1.Size = New System.Drawing.Size(287, 226)
        Me.UserControl_CheckListBoxAutores1.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label6, 3)
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(2, 401)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(287, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Datos recolectados en campo"
        '
        'UserControl_CheckBoxAddLayers1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.UserControl_CheckBoxAddLayers1, 3)
        Me.UserControl_CheckBoxAddLayers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_CheckBoxAddLayers1.Location = New System.Drawing.Point(4, 418)
        Me.UserControl_CheckBoxAddLayers1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UserControl_CheckBoxAddLayers1.Name = "UserControl_CheckBoxAddLayers1"
        Me.UserControl_CheckBoxAddLayers1.Size = New System.Drawing.Size(283, 90)
        Me.UserControl_CheckBoxAddLayers1.TabIndex = 19
        '
        'Form_mapa_neotectonica
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(291, 596)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Form_mapa_neotectonica"
        Me.Text = "Form_mapa_neotectonica"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.nud_mn_escala, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_mgp_n_registros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nud_mn_escala As System.Windows.Forms.NumericUpDown
    Friend WithEvents btn_mn_mapa As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdb_mn_horizontal As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_mn_vertical As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbx_mn_region As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbx_mn_zona As System.Windows.Forms.ComboBox
    Friend WithEvents UserControl_CheckListBox1 As UserControl_CheckListBoxAutores
    Friend WithEvents lbl_mgp_numero_registros As System.Windows.Forms.Label
    Friend WithEvents nud_mgp_n_registros As System.Windows.Forms.NumericUpDown
    Friend WithEvents UserControl_CheckListBoxAutores1 As UserControl_CheckListBoxAutores
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents UserControl_CheckBoxAddLayers1 As UserControl_CheckBoxAddLayers
End Class
