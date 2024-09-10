<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_diagrama_esfuerzos
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gbox_de_formato = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbtn_de_shp = New System.Windows.Forms.RadioButton()
        Me.rbtn_de_txt = New System.Windows.Forms.RadioButton()
        Me.rbtn_de_csv = New System.Windows.Forms.RadioButton()
        Me.tbx_de_pathfile = New System.Windows.Forms.TextBox()
        Me.btn_de_updata = New System.Windows.Forms.Button()
        Me.dgv_de_data = New System.Windows.Forms.DataGridView()
        Me.Nro = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Strike = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dip = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rake = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pbox_de_diagrama = New System.Windows.Forms.PictureBox()
        Me.btn_de_generar = New System.Windows.Forms.Button()
        Me.btn_downloadImg = New System.Windows.Forms.Button()
        Me.cbx_de_polo = New System.Windows.Forms.CheckBox()
        Me.cbx_de_rake = New System.Windows.Forms.CheckBox()
        Me.btn_de_editDatagrid = New System.Windows.Forms.Button()
        Me.SaveFileImg = New System.Windows.Forms.SaveFileDialog()
        Me.ttip_de_formato = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbox_de_formato.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.dgv_de_data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbox_de_diagrama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.gbox_de_formato, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_de_pathfile, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_de_updata, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_de_data, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.pbox_de_diagrama, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_de_generar, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_downloadImg, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_de_polo, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_de_rake, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_de_editDatagrid, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 215.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(284, 576)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'gbox_de_formato
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.gbox_de_formato, 4)
        Me.gbox_de_formato.Controls.Add(Me.TableLayoutPanel2)
        Me.gbox_de_formato.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbox_de_formato.Location = New System.Drawing.Point(5, 3)
        Me.gbox_de_formato.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.gbox_de_formato.Name = "gbox_de_formato"
        Me.gbox_de_formato.Size = New System.Drawing.Size(274, 54)
        Me.gbox_de_formato.TabIndex = 0
        Me.gbox_de_formato.TabStop = False
        Me.gbox_de_formato.Text = "Seleccionar formato"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.rbtn_de_shp, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.rbtn_de_txt, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.rbtn_de_csv, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(268, 35)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'rbtn_de_shp
        '
        Me.rbtn_de_shp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.rbtn_de_shp.AutoSize = True
        Me.rbtn_de_shp.Location = New System.Drawing.Point(200, 3)
        Me.rbtn_de_shp.Name = "rbtn_de_shp"
        Me.rbtn_de_shp.Size = New System.Drawing.Size(45, 29)
        Me.rbtn_de_shp.TabIndex = 3
        Me.rbtn_de_shp.Text = ".shp"
        Me.ttip_de_formato.SetToolTip(Me.rbtn_de_shp, "El Shapefile debe tener los campos:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[AZIMUT]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[BUZAMIENTO]")
        Me.rbtn_de_shp.UseVisualStyleBackColor = True
        '
        'rbtn_de_txt
        '
        Me.rbtn_de_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.rbtn_de_txt.AutoSize = True
        Me.rbtn_de_txt.Location = New System.Drawing.Point(114, 3)
        Me.rbtn_de_txt.Name = "rbtn_de_txt"
        Me.rbtn_de_txt.Size = New System.Drawing.Size(39, 29)
        Me.rbtn_de_txt.TabIndex = 1
        Me.rbtn_de_txt.Text = ".txt"
        Me.ttip_de_formato.SetToolTip(Me.rbtn_de_txt, "El orden de las columnas deben contener:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[AZIMUT]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[BUZAMIENTO]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[RAKE]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Y deben" &
        " estar se paradas por tabulacion ")
        Me.rbtn_de_txt.UseVisualStyleBackColor = True
        '
        'rbtn_de_csv
        '
        Me.rbtn_de_csv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.rbtn_de_csv.AutoSize = True
        Me.rbtn_de_csv.Checked = True
        Me.rbtn_de_csv.Location = New System.Drawing.Point(22, 3)
        Me.rbtn_de_csv.Name = "rbtn_de_csv"
        Me.rbtn_de_csv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rbtn_de_csv.Size = New System.Drawing.Size(45, 29)
        Me.rbtn_de_csv.TabIndex = 0
        Me.rbtn_de_csv.TabStop = True
        Me.rbtn_de_csv.Text = ".csv"
        Me.ttip_de_formato.SetToolTip(Me.rbtn_de_csv, "El orden de las columnas deben contener:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[AZIMUT]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[BUZAMIENTO]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[RAKE]" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Y deben" &
        " estar se paradas por: punto y como "";"" ")
        Me.rbtn_de_csv.UseVisualStyleBackColor = True
        '
        'tbx_de_pathfile
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_de_pathfile, 3)
        Me.tbx_de_pathfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_de_pathfile.Location = New System.Drawing.Point(3, 65)
        Me.tbx_de_pathfile.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.tbx_de_pathfile.Name = "tbx_de_pathfile"
        Me.tbx_de_pathfile.Size = New System.Drawing.Size(212, 20)
        Me.tbx_de_pathfile.TabIndex = 1
        '
        'btn_de_updata
        '
        Me.btn_de_updata.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_de_updata.FlatAppearance.BorderSize = 0
        Me.btn_de_updata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_de_updata.Location = New System.Drawing.Point(221, 65)
        Me.btn_de_updata.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.btn_de_updata.Name = "btn_de_updata"
        Me.btn_de_updata.Size = New System.Drawing.Size(58, 22)
        Me.btn_de_updata.TabIndex = 2
        Me.btn_de_updata.Text = "Cargar"
        Me.btn_de_updata.UseVisualStyleBackColor = False
        '
        'dgv_de_data
        '
        Me.dgv_de_data.AllowUserToAddRows = False
        Me.dgv_de_data.AllowUserToDeleteRows = False
        Me.dgv_de_data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.dgv_de_data.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_de_data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_de_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_de_data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nro, Me.Strike, Me.dip, Me.rake})
        Me.TableLayoutPanel1.SetColumnSpan(Me.dgv_de_data, 4)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_de_data.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_de_data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_de_data.GridColor = System.Drawing.SystemColors.Control
        Me.dgv_de_data.Location = New System.Drawing.Point(5, 95)
        Me.dgv_de_data.Margin = New System.Windows.Forms.Padding(5)
        Me.dgv_de_data.Name = "dgv_de_data"
        Me.dgv_de_data.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_de_data.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_de_data.RowHeadersVisible = False
        Me.dgv_de_data.Size = New System.Drawing.Size(274, 205)
        Me.dgv_de_data.TabIndex = 3
        '
        'Nro
        '
        Me.Nro.FalseValue = ""
        Me.Nro.Frozen = True
        Me.Nro.HeaderText = "Nro"
        Me.Nro.Name = "Nro"
        Me.Nro.ReadOnly = True
        Me.Nro.Width = 30
        '
        'Strike
        '
        Me.Strike.Frozen = True
        Me.Strike.HeaderText = "Azimut"
        Me.Strike.MaxInputLength = 3
        Me.Strike.Name = "Strike"
        Me.Strike.ReadOnly = True
        Me.Strike.Width = 63
        '
        'dip
        '
        Me.dip.Frozen = True
        Me.dip.HeaderText = "Buzamiento"
        Me.dip.MaxInputLength = 3
        Me.dip.Name = "dip"
        Me.dip.ReadOnly = True
        Me.dip.Width = 87
        '
        'rake
        '
        Me.rake.Frozen = True
        Me.rake.HeaderText = "rake"
        Me.rake.MaxInputLength = 4
        Me.rake.Name = "rake"
        Me.rake.ReadOnly = True
        Me.rake.Width = 53
        '
        'pbox_de_diagrama
        '
        Me.pbox_de_diagrama.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.pbox_de_diagrama, 4)
        Me.pbox_de_diagrama.InitialImage = Nothing
        Me.pbox_de_diagrama.Location = New System.Drawing.Point(5, 340)
        Me.pbox_de_diagrama.Margin = New System.Windows.Forms.Padding(5)
        Me.pbox_de_diagrama.Name = "pbox_de_diagrama"
        Me.pbox_de_diagrama.Size = New System.Drawing.Size(274, 201)
        Me.pbox_de_diagrama.TabIndex = 4
        Me.pbox_de_diagrama.TabStop = False
        '
        'btn_de_generar
        '
        Me.btn_de_generar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_de_generar.FlatAppearance.BorderSize = 0
        Me.btn_de_generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_de_generar.Location = New System.Drawing.Point(221, 308)
        Me.btn_de_generar.Name = "btn_de_generar"
        Me.btn_de_generar.Size = New System.Drawing.Size(58, 23)
        Me.btn_de_generar.TabIndex = 5
        Me.btn_de_generar.Text = "Generar"
        Me.btn_de_generar.UseVisualStyleBackColor = False
        '
        'btn_downloadImg
        '
        Me.btn_downloadImg.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_downloadImg.FlatAppearance.BorderSize = 0
        Me.btn_downloadImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_downloadImg.Location = New System.Drawing.Point(221, 549)
        Me.btn_downloadImg.Name = "btn_downloadImg"
        Me.btn_downloadImg.Size = New System.Drawing.Size(58, 23)
        Me.btn_downloadImg.TabIndex = 7
        Me.btn_downloadImg.Text = "Guardar"
        Me.btn_downloadImg.UseVisualStyleBackColor = False
        '
        'cbx_de_polo
        '
        Me.cbx_de_polo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbx_de_polo.AutoSize = True
        Me.cbx_de_polo.Checked = True
        Me.cbx_de_polo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbx_de_polo.Location = New System.Drawing.Point(142, 308)
        Me.cbx_de_polo.Name = "cbx_de_polo"
        Me.cbx_de_polo.Size = New System.Drawing.Size(47, 24)
        Me.cbx_de_polo.TabIndex = 8
        Me.cbx_de_polo.Text = "Polo"
        Me.cbx_de_polo.UseVisualStyleBackColor = True
        '
        'cbx_de_rake
        '
        Me.cbx_de_rake.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_de_rake.AutoSize = True
        Me.cbx_de_rake.Location = New System.Drawing.Point(84, 308)
        Me.cbx_de_rake.Name = "cbx_de_rake"
        Me.cbx_de_rake.Size = New System.Drawing.Size(52, 24)
        Me.cbx_de_rake.TabIndex = 9
        Me.cbx_de_rake.Text = "Rake"
        Me.cbx_de_rake.UseVisualStyleBackColor = True
        '
        'btn_de_editDatagrid
        '
        Me.btn_de_editDatagrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_de_editDatagrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_de_editDatagrid.FlatAppearance.BorderSize = 0
        Me.btn_de_editDatagrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_de_editDatagrid.Location = New System.Drawing.Point(3, 308)
        Me.btn_de_editDatagrid.Name = "btn_de_editDatagrid"
        Me.btn_de_editDatagrid.Size = New System.Drawing.Size(50, 24)
        Me.btn_de_editDatagrid.TabIndex = 6
        Me.btn_de_editDatagrid.Text = "Editar"
        Me.btn_de_editDatagrid.UseVisualStyleBackColor = False
        '
        'SaveFileImg
        '
        Me.SaveFileImg.InitialDirectory = "Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)"
        '
        'Form_diagrama_esfuerzos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 576)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_diagrama_esfuerzos"
        Me.Text = "Form_diagrama_esfuerzos"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbox_de_formato.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.dgv_de_data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbox_de_diagrama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbox_de_formato As System.Windows.Forms.GroupBox
    Friend WithEvents rbtn_de_csv As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_de_txt As System.Windows.Forms.RadioButton
    Friend WithEvents tbx_de_pathfile As System.Windows.Forms.TextBox
    Friend WithEvents btn_de_updata As System.Windows.Forms.Button
    Friend WithEvents dgv_de_data As System.Windows.Forms.DataGridView
    Friend WithEvents pbox_de_diagrama As System.Windows.Forms.PictureBox
    Friend WithEvents btn_de_generar As System.Windows.Forms.Button
    Friend WithEvents btn_de_editDatagrid As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rbtn_de_shp As System.Windows.Forms.RadioButton
    Friend WithEvents Nro As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Strike As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dip As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rake As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_downloadImg As System.Windows.Forms.Button
    Friend WithEvents SaveFileImg As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ttip_de_formato As System.Windows.Forms.ToolTip
    Friend WithEvents cbx_de_polo As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_de_rake As System.Windows.Forms.CheckBox
End Class
