<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_mapa_geomorfologico
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_geomorfologico))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_hojas = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_mgeom_fila = New System.Windows.Forms.Label()
        Me.lbl_mgeom_col = New System.Windows.Forms.Label()
        Me.lbl_mgeom_cua = New System.Windows.Forms.Label()
        Me.cbx_mgeom_fila = New System.Windows.Forms.ComboBox()
        Me.cbx_mgeom_cua = New System.Windows.Forms.ComboBox()
        Me.cbx_mgeom_col = New System.Windows.Forms.ComboBox()
        Me.rbtn_mgeom_hojas = New System.Windows.Forms.RadioButton()
        Me.rbtn_mgeom_ = New System.Windows.Forms.RadioButton()
        Me.lbl_mgeom_tipo = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_mgeom_aepath = New System.Windows.Forms.TextBox()
        Me.btn_mgeom_ae = New System.Windows.Forms.Button()
        Me.ilist_mgno_50k = New System.Windows.Forms.ImageList(Me.components)
        Me.btn_mgeom_generate = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Configuración = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_mgeom_author = New System.Windows.Forms.Label()
        Me.lbl_mgeom_titleMap = New System.Windows.Forms.Label()
        Me.lbl_mgeom_codeMap = New System.Windows.Forms.Label()
        Me.tbx_mgeom_author = New System.Windows.Forms.TextBox()
        Me.tbx_mgeom_titleMap = New System.Windows.Forms.TextBox()
        Me.tbx_mgeom_codeMap = New System.Windows.Forms.TextBox()
        Me.Leyenda = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_mgeom_simbology = New System.Windows.Forms.Label()
        Me.btn_mgeom_AsigSimbology = New System.Windows.Forms.Button()
        Me.lbl_mgeom_fuenteGeom = New System.Windows.Forms.Label()
        Me.cbx_mgeom_fuente = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_mgeom_geomselected = New System.Windows.Forms.TextBox()
        Me.btn_mgeom_selectGeom = New System.Windows.Forms.Button()
        Me.ilist_mg_50k = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel_hojas.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Configuración.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Leyenda.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel_hojas, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rbtn_mgeom_hojas, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.rbtn_mgeom_, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mgeom_tipo, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mgeom_generate, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mgeom_fuenteGeom, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mgeom_fuente, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(350, 600)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel_hojas
        '
        Me.TableLayoutPanel_hojas.ColumnCount = 8
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel_hojas, 2)
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel_hojas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel_hojas.Controls.Add(Me.lbl_mgeom_fila, 1, 0)
        Me.TableLayoutPanel_hojas.Controls.Add(Me.lbl_mgeom_col, 3, 0)
        Me.TableLayoutPanel_hojas.Controls.Add(Me.lbl_mgeom_cua, 5, 0)
        Me.TableLayoutPanel_hojas.Controls.Add(Me.cbx_mgeom_fila, 2, 0)
        Me.TableLayoutPanel_hojas.Controls.Add(Me.cbx_mgeom_cua, 6, 0)
        Me.TableLayoutPanel_hojas.Controls.Add(Me.cbx_mgeom_col, 4, 0)
        Me.TableLayoutPanel_hojas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_hojas.Location = New System.Drawing.Point(3, 138)
        Me.TableLayoutPanel_hojas.Name = "TableLayoutPanel_hojas"
        Me.TableLayoutPanel_hojas.RowCount = 1
        Me.TableLayoutPanel_hojas.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_hojas.Size = New System.Drawing.Size(344, 44)
        Me.TableLayoutPanel_hojas.TabIndex = 0
        '
        'lbl_mgeom_fila
        '
        Me.lbl_mgeom_fila.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lbl_mgeom_fila.AutoSize = True
        Me.lbl_mgeom_fila.Location = New System.Drawing.Point(24, 15)
        Me.lbl_mgeom_fila.Name = "lbl_mgeom_fila"
        Me.lbl_mgeom_fila.Size = New System.Drawing.Size(23, 13)
        Me.lbl_mgeom_fila.TabIndex = 0
        Me.lbl_mgeom_fila.Text = "Fila"
        Me.lbl_mgeom_fila.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_mgeom_col
        '
        Me.lbl_mgeom_col.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lbl_mgeom_col.AutoSize = True
        Me.lbl_mgeom_col.Location = New System.Drawing.Point(103, 15)
        Me.lbl_mgeom_col.Name = "lbl_mgeom_col"
        Me.lbl_mgeom_col.Size = New System.Drawing.Size(48, 13)
        Me.lbl_mgeom_col.TabIndex = 1
        Me.lbl_mgeom_col.Text = "Columna"
        Me.lbl_mgeom_col.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_mgeom_cua
        '
        Me.lbl_mgeom_cua.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lbl_mgeom_cua.AutoSize = True
        Me.lbl_mgeom_cua.Location = New System.Drawing.Point(211, 15)
        Me.lbl_mgeom_cua.Name = "lbl_mgeom_cua"
        Me.lbl_mgeom_cua.Size = New System.Drawing.Size(56, 13)
        Me.lbl_mgeom_cua.TabIndex = 2
        Me.lbl_mgeom_cua.Text = "Cuadrante"
        Me.lbl_mgeom_cua.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbx_mgeom_fila
        '
        Me.cbx_mgeom_fila.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mgeom_fila.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mgeom_fila.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mgeom_fila.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_mgeom_fila.FormattingEnabled = True
        Me.cbx_mgeom_fila.Location = New System.Drawing.Point(53, 11)
        Me.cbx_mgeom_fila.Name = "cbx_mgeom_fila"
        Me.cbx_mgeom_fila.Size = New System.Drawing.Size(44, 21)
        Me.cbx_mgeom_fila.TabIndex = 3
        '
        'cbx_mgeom_cua
        '
        Me.cbx_mgeom_cua.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mgeom_cua.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_mgeom_cua.FormattingEnabled = True
        Me.cbx_mgeom_cua.Location = New System.Drawing.Point(273, 11)
        Me.cbx_mgeom_cua.Name = "cbx_mgeom_cua"
        Me.cbx_mgeom_cua.Size = New System.Drawing.Size(44, 21)
        Me.cbx_mgeom_cua.TabIndex = 5
        '
        'cbx_mgeom_col
        '
        Me.cbx_mgeom_col.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mgeom_col.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mgeom_col.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mgeom_col.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_mgeom_col.FormattingEnabled = True
        Me.cbx_mgeom_col.Location = New System.Drawing.Point(157, 11)
        Me.cbx_mgeom_col.Name = "cbx_mgeom_col"
        Me.cbx_mgeom_col.Size = New System.Drawing.Size(44, 21)
        Me.cbx_mgeom_col.TabIndex = 4
        '
        'rbtn_mgeom_hojas
        '
        Me.rbtn_mgeom_hojas.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rbtn_mgeom_hojas.AutoSize = True
        Me.rbtn_mgeom_hojas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtn_mgeom_hojas.Location = New System.Drawing.Point(30, 111)
        Me.rbtn_mgeom_hojas.Name = "rbtn_mgeom_hojas"
        Me.rbtn_mgeom_hojas.Size = New System.Drawing.Size(115, 17)
        Me.rbtn_mgeom_hojas.TabIndex = 1
        Me.rbtn_mgeom_hojas.TabStop = True
        Me.rbtn_mgeom_hojas.Text = "Hojas (100 k, 50 k)"
        Me.ToolTip1.SetToolTip(Me.rbtn_mgeom_hojas, "Agregará  la capa de hojas geologicas al 50k" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "de no encontrarse en el mapa")
        Me.rbtn_mgeom_hojas.UseVisualStyleBackColor = True
        '
        'rbtn_mgeom_
        '
        Me.rbtn_mgeom_.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rbtn_mgeom_.AutoSize = True
        Me.rbtn_mgeom_.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rbtn_mgeom_.Location = New System.Drawing.Point(198, 111)
        Me.rbtn_mgeom_.Name = "rbtn_mgeom_"
        Me.rbtn_mgeom_.Size = New System.Drawing.Size(128, 17)
        Me.rbtn_mgeom_.TabIndex = 2
        Me.rbtn_mgeom_.TabStop = True
        Me.rbtn_mgeom_.Text = "Area de estudio (.shp)"
        Me.ToolTip1.SetToolTip(Me.rbtn_mgeom_, "Habilita la carga de un Shapefile como" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "area de estudio para generar mapa")
        Me.rbtn_mgeom_.UseVisualStyleBackColor = True
        '
        'lbl_mgeom_tipo
        '
        Me.lbl_mgeom_tipo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_mgeom_tipo.AutoSize = True
        Me.lbl_mgeom_tipo.Location = New System.Drawing.Point(0, 86)
        Me.lbl_mgeom_tipo.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_mgeom_tipo.Name = "lbl_mgeom_tipo"
        Me.lbl_mgeom_tipo.Size = New System.Drawing.Size(175, 13)
        Me.lbl_mgeom_tipo.TabIndex = 3
        Me.lbl_mgeom_tipo.Text = "Seleccione modo de consulta"
        Me.lbl_mgeom_tipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel2, 2)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mgeom_aepath, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mgeom_ae, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 188)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(344, 44)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'tbx_mgeom_aepath
        '
        Me.tbx_mgeom_aepath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mgeom_aepath.Location = New System.Drawing.Point(3, 12)
        Me.tbx_mgeom_aepath.Name = "tbx_mgeom_aepath"
        Me.tbx_mgeom_aepath.ReadOnly = True
        Me.tbx_mgeom_aepath.Size = New System.Drawing.Size(278, 20)
        Me.tbx_mgeom_aepath.TabIndex = 0
        '
        'btn_mgeom_ae
        '
        Me.btn_mgeom_ae.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_mgeom_ae.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_mgeom_ae.FlatAppearance.BorderSize = 0
        Me.btn_mgeom_ae.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mgeom_ae.ImageIndex = 10
        Me.btn_mgeom_ae.ImageList = Me.ilist_mgno_50k
        Me.btn_mgeom_ae.Location = New System.Drawing.Point(287, 10)
        Me.btn_mgeom_ae.Name = "btn_mgeom_ae"
        Me.btn_mgeom_ae.Size = New System.Drawing.Size(54, 23)
        Me.btn_mgeom_ae.TabIndex = 1
        Me.btn_mgeom_ae.Text = " "
        Me.btn_mgeom_ae.UseVisualStyleBackColor = False
        '
        'ilist_mgno_50k
        '
        Me.ilist_mgno_50k.ImageStream = CType(resources.GetObject("ilist_mgno_50k.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilist_mgno_50k.TransparentColor = System.Drawing.Color.Transparent
        Me.ilist_mgno_50k.Images.SetKeyName(0, "setting_config.png")
        Me.ilist_mgno_50k.Images.SetKeyName(1, "settings.png")
        Me.ilist_mgno_50k.Images.SetKeyName(2, "ProfileGraph32.png")
        Me.ilist_mgno_50k.Images.SetKeyName(3, "lista-de-vinetas (1).png")
        Me.ilist_mgno_50k.Images.SetKeyName(4, "subirV316.png")
        Me.ilist_mgno_50k.Images.SetKeyName(5, "polilinea20.png")
        Me.ilist_mgno_50k.Images.SetKeyName(6, "ShapefileLine32.png")
        Me.ilist_mgno_50k.Images.SetKeyName(7, "patron.png")
        Me.ilist_mgno_50k.Images.SetKeyName(8, "add_layer20.png")
        Me.ilist_mgno_50k.Images.SetKeyName(9, "add_layer-24.png")
        Me.ilist_mgno_50k.Images.SetKeyName(10, "group-layouts24.png")
        Me.ilist_mgno_50k.Images.SetKeyName(11, "AddRaster16.png")
        Me.ilist_mgno_50k.Images.SetKeyName(12, "AddRaster32.png")
        Me.ilist_mgno_50k.Images.SetKeyName(13, "CADFeatureClassLine32.png")
        Me.ilist_mgno_50k.Images.SetKeyName(14, "MapWithGeoprocessingTool16.png")
        Me.ilist_mgno_50k.Images.SetKeyName(15, "GraphVerticalArea32.png")
        Me.ilist_mgno_50k.Images.SetKeyName(16, "chart_9164190.png")
        '
        'btn_mgeom_generate
        '
        Me.btn_mgeom_generate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_mgeom_generate.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_mgeom_generate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mgeom_generate.FlatAppearance.BorderSize = 0
        Me.btn_mgeom_generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mgeom_generate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_mgeom_generate.ImageKey = "MapWithGeoprocessingTool16.png"
        Me.btn_mgeom_generate.ImageList = Me.ilist_mgno_50k
        Me.btn_mgeom_generate.Location = New System.Drawing.Point(247, 368)
        Me.btn_mgeom_generate.Name = "btn_mgeom_generate"
        Me.btn_mgeom_generate.Size = New System.Drawing.Size(100, 30)
        Me.btn_mgeom_generate.TabIndex = 5
        Me.btn_mgeom_generate.Text = "     Generar Mapa"
        Me.ToolTip1.SetToolTip(Me.btn_mgeom_generate, "Genera mapa Geormorfologico si estan" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "completos los campos de la pestaña" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "configu" &
        "ración")
        Me.btn_mgeom_generate.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.TabControl1, 2)
        Me.TabControl1.Controls.Add(Me.Configuración)
        Me.TabControl1.Controls.Add(Me.Leyenda)
        Me.TabControl1.ImageList = Me.ilist_mgno_50k
        Me.TabControl1.Location = New System.Drawing.Point(3, 238)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(344, 124)
        Me.TabControl1.TabIndex = 6
        '
        'Configuración
        '
        Me.Configuración.Controls.Add(Me.TableLayoutPanel3)
        Me.Configuración.ImageIndex = 0
        Me.Configuración.Location = New System.Drawing.Point(4, 22)
        Me.Configuración.Name = "Configuración"
        Me.Configuración.Padding = New System.Windows.Forms.Padding(3)
        Me.Configuración.Size = New System.Drawing.Size(336, 98)
        Me.Configuración.TabIndex = 0
        Me.Configuración.Text = "Configuración"
        Me.Configuración.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_mgeom_author, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_mgeom_titleMap, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_mgeom_codeMap, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.tbx_mgeom_author, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.tbx_mgeom_titleMap, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.tbx_mgeom_codeMap, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(330, 92)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'lbl_mgeom_author
        '
        Me.lbl_mgeom_author.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_mgeom_author.AutoSize = True
        Me.lbl_mgeom_author.Location = New System.Drawing.Point(33, 8)
        Me.lbl_mgeom_author.Name = "lbl_mgeom_author"
        Me.lbl_mgeom_author.Size = New System.Drawing.Size(32, 13)
        Me.lbl_mgeom_author.TabIndex = 0
        Me.lbl_mgeom_author.Text = "Autor"
        '
        'lbl_mgeom_titleMap
        '
        Me.lbl_mgeom_titleMap.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_mgeom_titleMap.AutoSize = True
        Me.lbl_mgeom_titleMap.Location = New System.Drawing.Point(18, 38)
        Me.lbl_mgeom_titleMap.Name = "lbl_mgeom_titleMap"
        Me.lbl_mgeom_titleMap.Size = New System.Drawing.Size(63, 13)
        Me.lbl_mgeom_titleMap.TabIndex = 1
        Me.lbl_mgeom_titleMap.Text = "Titulo Mapa"
        '
        'lbl_mgeom_codeMap
        '
        Me.lbl_mgeom_codeMap.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_mgeom_codeMap.AutoSize = True
        Me.lbl_mgeom_codeMap.Location = New System.Drawing.Point(14, 69)
        Me.lbl_mgeom_codeMap.Name = "lbl_mgeom_codeMap"
        Me.lbl_mgeom_codeMap.Size = New System.Drawing.Size(70, 13)
        Me.lbl_mgeom_codeMap.TabIndex = 2
        Me.lbl_mgeom_codeMap.Text = "Código Mapa"
        '
        'tbx_mgeom_author
        '
        Me.tbx_mgeom_author.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mgeom_author.Location = New System.Drawing.Point(102, 5)
        Me.tbx_mgeom_author.Name = "tbx_mgeom_author"
        Me.tbx_mgeom_author.Size = New System.Drawing.Size(225, 20)
        Me.tbx_mgeom_author.TabIndex = 3
        '
        'tbx_mgeom_titleMap
        '
        Me.tbx_mgeom_titleMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mgeom_titleMap.Location = New System.Drawing.Point(102, 35)
        Me.tbx_mgeom_titleMap.Name = "tbx_mgeom_titleMap"
        Me.tbx_mgeom_titleMap.Size = New System.Drawing.Size(225, 20)
        Me.tbx_mgeom_titleMap.TabIndex = 4
        '
        'tbx_mgeom_codeMap
        '
        Me.tbx_mgeom_codeMap.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mgeom_codeMap.Location = New System.Drawing.Point(102, 66)
        Me.tbx_mgeom_codeMap.Name = "tbx_mgeom_codeMap"
        Me.tbx_mgeom_codeMap.Size = New System.Drawing.Size(225, 20)
        Me.tbx_mgeom_codeMap.TabIndex = 5
        '
        'Leyenda
        '
        Me.Leyenda.Controls.Add(Me.TableLayoutPanel4)
        Me.Leyenda.ImageIndex = 3
        Me.Leyenda.Location = New System.Drawing.Point(4, 22)
        Me.Leyenda.Name = "Leyenda"
        Me.Leyenda.Padding = New System.Windows.Forms.Padding(3)
        Me.Leyenda.Size = New System.Drawing.Size(336, 98)
        Me.Leyenda.TabIndex = 1
        Me.Leyenda.Text = "Leyenda"
        Me.Leyenda.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_mgeom_simbology, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mgeom_AsigSimbology, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(330, 92)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'lbl_mgeom_simbology
        '
        Me.lbl_mgeom_simbology.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_mgeom_simbology.AutoSize = True
        Me.lbl_mgeom_simbology.Location = New System.Drawing.Point(34, 11)
        Me.lbl_mgeom_simbology.Name = "lbl_mgeom_simbology"
        Me.lbl_mgeom_simbology.Size = New System.Drawing.Size(96, 13)
        Me.lbl_mgeom_simbology.TabIndex = 0
        Me.lbl_mgeom_simbology.Text = "Asignar simbología"
        '
        'btn_mgeom_AsigSimbology
        '
        Me.btn_mgeom_AsigSimbology.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_mgeom_AsigSimbology.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btn_mgeom_AsigSimbology.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mgeom_AsigSimbology.Enabled = False
        Me.btn_mgeom_AsigSimbology.FlatAppearance.BorderSize = 0
        Me.btn_mgeom_AsigSimbology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mgeom_AsigSimbology.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_mgeom_AsigSimbology.ImageIndex = 7
        Me.btn_mgeom_AsigSimbology.ImageList = Me.ilist_mgno_50k
        Me.btn_mgeom_AsigSimbology.Location = New System.Drawing.Point(207, 5)
        Me.btn_mgeom_AsigSimbology.Name = "btn_mgeom_AsigSimbology"
        Me.btn_mgeom_AsigSimbology.Size = New System.Drawing.Size(80, 25)
        Me.btn_mgeom_AsigSimbology.TabIndex = 1
        Me.btn_mgeom_AsigSimbology.Text = "  Aplicar"
        Me.ToolTip1.SetToolTip(Me.btn_mgeom_AsigSimbology, "Aplica las simbologias de los elementos presentes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sobre la capa ""Area_geomorfolo" &
        "gica""")
        Me.btn_mgeom_AsigSimbology.UseVisualStyleBackColor = False
        '
        'lbl_mgeom_fuenteGeom
        '
        Me.lbl_mgeom_fuenteGeom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_mgeom_fuenteGeom.AutoSize = True
        Me.lbl_mgeom_fuenteGeom.Location = New System.Drawing.Point(3, 3)
        Me.lbl_mgeom_fuenteGeom.Name = "lbl_mgeom_fuenteGeom"
        Me.lbl_mgeom_fuenteGeom.Size = New System.Drawing.Size(169, 13)
        Me.lbl_mgeom_fuenteGeom.TabIndex = 7
        Me.lbl_mgeom_fuenteGeom.Text = "Seleccione fuente"
        '
        'cbx_mgeom_fuente
        '
        Me.cbx_mgeom_fuente.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_mgeom_fuente, 2)
        Me.cbx_mgeom_fuente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mgeom_fuente.FormattingEnabled = True
        Me.cbx_mgeom_fuente.Items.AddRange(New Object() {"Geomorfología Nacional (servidor)", "Geomorfología Propia (carga)"})
        Me.cbx_mgeom_fuente.Location = New System.Drawing.Point(3, 23)
        Me.cbx_mgeom_fuente.Name = "cbx_mgeom_fuente"
        Me.cbx_mgeom_fuente.Size = New System.Drawing.Size(344, 21)
        Me.cbx_mgeom_fuente.TabIndex = 8
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel5, 2)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.tbx_mgeom_geomselected, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_mgeom_selectGeom, 0, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 48)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(344, 29)
        Me.TableLayoutPanel5.TabIndex = 9
        '
        'tbx_mgeom_geomselected
        '
        Me.tbx_mgeom_geomselected.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mgeom_geomselected.Location = New System.Drawing.Point(63, 4)
        Me.tbx_mgeom_geomselected.Name = "tbx_mgeom_geomselected"
        Me.tbx_mgeom_geomselected.ReadOnly = True
        Me.tbx_mgeom_geomselected.Size = New System.Drawing.Size(278, 20)
        Me.tbx_mgeom_geomselected.TabIndex = 0
        '
        'btn_mgeom_selectGeom
        '
        Me.btn_mgeom_selectGeom.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_mgeom_selectGeom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mgeom_selectGeom.FlatAppearance.BorderSize = 0
        Me.btn_mgeom_selectGeom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mgeom_selectGeom.ImageKey = "GeodatabaseAdd16.png"
        Me.btn_mgeom_selectGeom.ImageList = Me.ilist_mg_50k
        Me.btn_mgeom_selectGeom.Location = New System.Drawing.Point(3, 3)
        Me.btn_mgeom_selectGeom.Name = "btn_mgeom_selectGeom"
        Me.btn_mgeom_selectGeom.Size = New System.Drawing.Size(54, 23)
        Me.btn_mgeom_selectGeom.TabIndex = 1
        Me.btn_mgeom_selectGeom.UseVisualStyleBackColor = False
        '
        'ilist_mg_50k
        '
        Me.ilist_mg_50k.ImageStream = CType(resources.GetObject("ilist_mg_50k.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilist_mg_50k.TransparentColor = System.Drawing.Color.Transparent
        Me.ilist_mg_50k.Images.SetKeyName(0, "legend.png")
        Me.ilist_mg_50k.Images.SetKeyName(1, "makeTable.png")
        Me.ilist_mg_50k.Images.SetKeyName(2, "settings.png")
        Me.ilist_mg_50k.Images.SetKeyName(3, "database.png")
        Me.ilist_mg_50k.Images.SetKeyName(4, "surface.png")
        Me.ilist_mg_50k.Images.SetKeyName(5, "select.png")
        Me.ilist_mg_50k.Images.SetKeyName(6, "GenericGlobe64.png")
        Me.ilist_mg_50k.Images.SetKeyName(7, "Legend16.png")
        Me.ilist_mg_50k.Images.SetKeyName(8, "3DAnalystInterpolateProfileGraphCreate16.png")
        Me.ilist_mg_50k.Images.SetKeyName(9, "ArcGlobe16.png")
        Me.ilist_mg_50k.Images.SetKeyName(10, "GeodatabaseAdd16.png")
        Me.ilist_mg_50k.Images.SetKeyName(11, "EditingBuffer16.png")
        Me.ilist_mg_50k.Images.SetKeyName(12, "ElementMakeSameHeight.ico")
        Me.ilist_mg_50k.Images.SetKeyName(13, "GeodatabaseRasterGrid16.png")
        Me.ilist_mg_50k.Images.SetKeyName(14, "ElementLineSegmentBlack.ico")
        Me.ilist_mg_50k.Images.SetKeyName(15, "SelectionSelectTool16.png")
        Me.ilist_mg_50k.Images.SetKeyName(16, "GeodatabaseTopology16.png")
        Me.ilist_mg_50k.Images.SetKeyName(17, "GenericFilterByLayerChecked16.png")
        Me.ilist_mg_50k.Images.SetKeyName(18, "AddAllValues16.png")
        Me.ilist_mg_50k.Images.SetKeyName(19, "GenericSave16.png")
        Me.ilist_mg_50k.Images.SetKeyName(20, "EditCopy16.png")
        Me.ilist_mg_50k.Images.SetKeyName(21, "RepresentationRotateTool16.png")
        Me.ilist_mg_50k.Images.SetKeyName(22, "TableStandaloneSmall16.png")
        Me.ilist_mg_50k.Images.SetKeyName(23, "TableNotInProject16.png")
        Me.ilist_mg_50k.Images.SetKeyName(24, "DrawRectangle16.png")
        Me.ilist_mg_50k.Images.SetKeyName(25, "NetworkAnalystPolygonRestrictionBarrier16.png")
        Me.ilist_mg_50k.Images.SetKeyName(26, "NetworkAnalystPolygonRestrictionBarrier32.png")
        '
        'Form_mapa_geomorfologico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.ClientSize = New System.Drawing.Size(350, 600)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_mapa_geomorfologico"
        Me.Text = "Form_mapa_geomorfologico"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel_hojas.ResumeLayout(False)
        Me.TableLayoutPanel_hojas.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.Configuración.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.Leyenda.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_hojas As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_mgeom_fila As System.Windows.Forms.Label
    Friend WithEvents lbl_mgeom_col As System.Windows.Forms.Label
    Friend WithEvents lbl_mgeom_cua As System.Windows.Forms.Label
    Friend WithEvents cbx_mgeom_fila As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mgeom_col As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mgeom_cua As System.Windows.Forms.ComboBox
    Friend WithEvents rbtn_mgeom_hojas As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_mgeom_ As System.Windows.Forms.RadioButton
    Friend WithEvents lbl_mgeom_tipo As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_mgeom_aepath As System.Windows.Forms.TextBox
    Friend WithEvents btn_mgeom_ae As System.Windows.Forms.Button
    Friend WithEvents btn_mgeom_generate As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Configuración As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_mgeom_author As System.Windows.Forms.Label
    Friend WithEvents lbl_mgeom_titleMap As System.Windows.Forms.Label
    Friend WithEvents lbl_mgeom_codeMap As System.Windows.Forms.Label
    Friend WithEvents tbx_mgeom_author As System.Windows.Forms.TextBox
    Friend WithEvents tbx_mgeom_titleMap As System.Windows.Forms.TextBox
    Friend WithEvents tbx_mgeom_codeMap As System.Windows.Forms.TextBox
    Friend WithEvents Leyenda As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_mgeom_simbology As System.Windows.Forms.Label
    Friend WithEvents btn_mgeom_AsigSimbology As System.Windows.Forms.Button
    Friend WithEvents ilist_mg_50k As System.Windows.Forms.ImageList
    Friend WithEvents ilist_mgno_50k As System.Windows.Forms.ImageList
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lbl_mgeom_fuenteGeom As System.Windows.Forms.Label
    Friend WithEvents cbx_mgeom_fuente As System.Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_mgeom_geomselected As System.Windows.Forms.TextBox
    Friend WithEvents btn_mgeom_selectGeom As System.Windows.Forms.Button
End Class
