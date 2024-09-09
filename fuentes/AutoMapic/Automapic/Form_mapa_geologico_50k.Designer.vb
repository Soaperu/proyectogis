<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_mapa_geologico_50k
    'Inherits MetroFramework.Forms.MetroForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_geologico_50k))
        Me.ilist_mg_50k = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tc_mg_50k = New System.Windows.Forms.TabControl()
        Me.tab_mg_topologia = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.clb_mg_topologias = New System.Windows.Forms.CheckedListBox()
        Me.rbt_mg_seleccion = New System.Windows.Forms.RadioButton()
        Me.btn_mg_run_topology = New System.Windows.Forms.Button()
        Me.rbt_mg_actual = New System.Windows.Forms.RadioButton()
        Me.btn_mg_SelectlayerByLocation = New System.Windows.Forms.Button()
        Me.lbl_mg_topology_res = New System.Windows.Forms.Label()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.tb_mg_query = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_mg_filtro = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_mg_seleccion = New System.Windows.Forms.Button()
        Me.tb_mg_leyenda = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_mg_generar_leyenda = New System.Windows.Forms.Button()
        Me.cbx_mg_tiporoca = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbx_mg_dominio = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btn_mg_draw = New System.Windows.Forms.Button()
        Me.lbl_mg_extent_legend = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_mg_ver_tabla = New System.Windows.Forms.Button()
        Me.btn_mg_cargar_datos = New System.Windows.Forms.Button()
        Me.tb_mg_perfil = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_mg_pathdata = New System.Windows.Forms.TextBox()
        Me.btn_mg_loaddata = New System.Windows.Forms.Button()
        Me.lbl_mg_cargar_dem = New System.Windows.Forms.Label()
        Me.btn_mg_drawline = New System.Windows.Forms.Button()
        Me.btn_mp_seccion = New System.Windows.Forms.Button()
        Me.nud_mg_tolerancia = New System.Windows.Forms.NumericUpDown()
        Me.lbl_mg_tolerancia = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nud_mg_altura = New System.Windows.Forms.NumericUpDown()
        Me.btn_mg_loadShp = New System.Windows.Forms.Button()
        Me.tbx_mg_loadShp = New System.Windows.Forms.TextBox()
        Me.rbtn_mg_drawline = New System.Windows.Forms.RadioButton()
        Me.rbtn_mg_loadshp = New System.Windows.Forms.RadioButton()
        Me.cbx_mg_inifin_sec = New System.Windows.Forms.ComboBox()
        Me.lbl_mg_XX = New System.Windows.Forms.Label()
        Me.cbx_mg_fila = New System.Windows.Forms.ComboBox()
        Me.cbx_mg_col = New System.Windows.Forms.ComboBox()
        Me.cbx_mg_cuad = New System.Windows.Forms.ComboBox()
        Me.btn_load_code = New System.Windows.Forms.Button()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.lbl_dato_hoja = New MetroFramework.Controls.MetroLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UserControl_CheckBoxAddLayers2 = New Automapic.UserControl_CheckBoxAddLayers()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tc_mg_50k.SuspendLayout()
        Me.tab_mg_topologia.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tb_mg_query.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.tb_mg_leyenda.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.tb_mg_perfil.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.nud_mg_tolerancia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_mg_altura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tc_mg_50k, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mg_fila, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mg_col, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mg_cuad, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_load_code, 8, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MetroLabel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MetroLabel2, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MetroLabel3, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_dato_hoja, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(392, 697)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'tc_mg_50k
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tc_mg_50k, 9)
        Me.tc_mg_50k.Controls.Add(Me.tab_mg_topologia)
        Me.tc_mg_50k.Controls.Add(Me.tb_mg_query)
        Me.tc_mg_50k.Controls.Add(Me.tb_mg_leyenda)
        Me.tc_mg_50k.Controls.Add(Me.tb_mg_perfil)
        Me.tc_mg_50k.Cursor = System.Windows.Forms.Cursors.Default
        Me.tc_mg_50k.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_mg_50k.Enabled = False
        Me.tc_mg_50k.ImageList = Me.ilist_mg_50k
        Me.tc_mg_50k.Location = New System.Drawing.Point(2, 78)
        Me.tc_mg_50k.Margin = New System.Windows.Forms.Padding(2)
        Me.tc_mg_50k.Name = "tc_mg_50k"
        Me.tc_mg_50k.SelectedIndex = 0
        Me.tc_mg_50k.Size = New System.Drawing.Size(388, 617)
        Me.tc_mg_50k.TabIndex = 0
        '
        'tab_mg_topologia
        '
        Me.tab_mg_topologia.Controls.Add(Me.TableLayoutPanel3)
        Me.tab_mg_topologia.ImageIndex = 16
        Me.tab_mg_topologia.Location = New System.Drawing.Point(4, 23)
        Me.tab_mg_topologia.Margin = New System.Windows.Forms.Padding(2)
        Me.tab_mg_topologia.Name = "tab_mg_topologia"
        Me.tab_mg_topologia.Padding = New System.Windows.Forms.Padding(2)
        Me.tab_mg_topologia.Size = New System.Drawing.Size(380, 590)
        Me.tab_mg_topologia.TabIndex = 0
        Me.tab_mg_topologia.Text = "Topología"
        Me.tab_mg_topologia.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.clb_mg_topologias, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.rbt_mg_seleccion, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mg_run_topology, 0, 8)
        Me.TableLayoutPanel3.Controls.Add(Me.rbt_mg_actual, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mg_SelectlayerByLocation, 1, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_mg_topology_res, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.MetroLabel4, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 9
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(376, 586)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'clb_mg_topologias
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.clb_mg_topologias, 2)
        Me.clb_mg_topologias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clb_mg_topologias.FormattingEnabled = True
        Me.clb_mg_topologias.Location = New System.Drawing.Point(2, 27)
        Me.clb_mg_topologias.Margin = New System.Windows.Forms.Padding(2)
        Me.clb_mg_topologias.Name = "clb_mg_topologias"
        Me.clb_mg_topologias.Size = New System.Drawing.Size(372, 94)
        Me.clb_mg_topologias.TabIndex = 4
        '
        'rbt_mg_seleccion
        '
        Me.rbt_mg_seleccion.AutoSize = True
        Me.rbt_mg_seleccion.Location = New System.Drawing.Point(2, 282)
        Me.rbt_mg_seleccion.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mg_seleccion.Name = "rbt_mg_seleccion"
        Me.rbt_mg_seleccion.Size = New System.Drawing.Size(180, 17)
        Me.rbt_mg_seleccion.TabIndex = 5
        Me.rbt_mg_seleccion.Text = "Seleccionar hojas espacialmente"
        Me.rbt_mg_seleccion.UseVisualStyleBackColor = True
        Me.rbt_mg_seleccion.Visible = False
        '
        'btn_mg_run_topology
        '
        Me.btn_mg_run_topology.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.TableLayoutPanel3.SetColumnSpan(Me.btn_mg_run_topology, 2)
        Me.btn_mg_run_topology.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_run_topology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_run_topology.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_mg_run_topology.FlatAppearance.BorderSize = 0
        Me.btn_mg_run_topology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_run_topology.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_mg_run_topology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_mg_run_topology.ImageIndex = 16
        Me.btn_mg_run_topology.ImageList = Me.ilist_mg_50k
        Me.btn_mg_run_topology.Location = New System.Drawing.Point(2, 554)
        Me.btn_mg_run_topology.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_run_topology.Name = "btn_mg_run_topology"
        Me.btn_mg_run_topology.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btn_mg_run_topology.Size = New System.Drawing.Size(372, 30)
        Me.btn_mg_run_topology.TabIndex = 6
        Me.btn_mg_run_topology.Text = "Aplicar topología"
        Me.btn_mg_run_topology.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mg_run_topology.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mg_run_topology.UseVisualStyleBackColor = False
        '
        'rbt_mg_actual
        '
        Me.rbt_mg_actual.AutoSize = True
        Me.rbt_mg_actual.Checked = True
        Me.rbt_mg_actual.Location = New System.Drawing.Point(2, 254)
        Me.rbt_mg_actual.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mg_actual.Name = "rbt_mg_actual"
        Me.rbt_mg_actual.Size = New System.Drawing.Size(138, 17)
        Me.rbt_mg_actual.TabIndex = 7
        Me.rbt_mg_actual.TabStop = True
        Me.rbt_mg_actual.Text = "Aplicar en la hoja actual"
        Me.rbt_mg_actual.UseVisualStyleBackColor = True
        Me.rbt_mg_actual.Visible = False
        '
        'btn_mg_SelectlayerByLocation
        '
        Me.btn_mg_SelectlayerByLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_SelectlayerByLocation.Enabled = False
        Me.btn_mg_SelectlayerByLocation.ImageIndex = 15
        Me.btn_mg_SelectlayerByLocation.ImageList = Me.ilist_mg_50k
        Me.btn_mg_SelectlayerByLocation.Location = New System.Drawing.Point(326, 282)
        Me.btn_mg_SelectlayerByLocation.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_SelectlayerByLocation.Name = "btn_mg_SelectlayerByLocation"
        Me.TableLayoutPanel3.SetRowSpan(Me.btn_mg_SelectlayerByLocation, 2)
        Me.btn_mg_SelectlayerByLocation.Size = New System.Drawing.Size(48, 39)
        Me.btn_mg_SelectlayerByLocation.TabIndex = 8
        Me.btn_mg_SelectlayerByLocation.UseVisualStyleBackColor = True
        Me.btn_mg_SelectlayerByLocation.Visible = False
        '
        'lbl_mg_topology_res
        '
        Me.lbl_mg_topology_res.AutoSize = True
        Me.TableLayoutPanel3.SetColumnSpan(Me.lbl_mg_topology_res, 2)
        Me.lbl_mg_topology_res.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_mg_topology_res.Location = New System.Drawing.Point(2, 129)
        Me.lbl_mg_topology_res.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mg_topology_res.Name = "lbl_mg_topology_res"
        Me.lbl_mg_topology_res.Size = New System.Drawing.Size(372, 13)
        Me.lbl_mg_topology_res.TabIndex = 9
        Me.lbl_mg_topology_res.Text = "..."
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MetroLabel4.Location = New System.Drawing.Point(3, 6)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(318, 19)
        Me.MetroLabel4.TabIndex = 10
        Me.MetroLabel4.Text = "Seleccione el tipo de análisis"
        '
        'tb_mg_query
        '
        Me.tb_mg_query.Controls.Add(Me.TableLayoutPanel4)
        Me.tb_mg_query.ImageIndex = 17
        Me.tb_mg_query.Location = New System.Drawing.Point(4, 23)
        Me.tb_mg_query.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_mg_query.Name = "tb_mg_query"
        Me.tb_mg_query.Padding = New System.Windows.Forms.Padding(2)
        Me.tb_mg_query.Size = New System.Drawing.Size(380, 590)
        Me.tb_mg_query.TabIndex = 1
        Me.tb_mg_query.Text = "Consulta"
        Me.tb_mg_query.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mg_filtro, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mg_seleccion, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.UserControl_CheckBoxAddLayers2, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 5
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(376, 586)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'btn_mg_filtro
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.btn_mg_filtro, 2)
        Me.btn_mg_filtro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_filtro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_filtro.ImageIndex = 17
        Me.btn_mg_filtro.ImageList = Me.ilist_mg_50k
        Me.btn_mg_filtro.Location = New System.Drawing.Point(2, 554)
        Me.btn_mg_filtro.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_filtro.Name = "btn_mg_filtro"
        Me.btn_mg_filtro.Size = New System.Drawing.Size(372, 30)
        Me.btn_mg_filtro.TabIndex = 1
        Me.btn_mg_filtro.Text = "Filtrar"
        Me.btn_mg_filtro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mg_filtro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mg_filtro.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(2, 509)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(321, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Seleccione cuadrículas a consultar"
        '
        'btn_mg_seleccion
        '
        Me.btn_mg_seleccion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_seleccion.ImageIndex = 15
        Me.btn_mg_seleccion.ImageList = Me.ilist_mg_50k
        Me.btn_mg_seleccion.Location = New System.Drawing.Point(327, 504)
        Me.btn_mg_seleccion.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_seleccion.Name = "btn_mg_seleccion"
        Me.TableLayoutPanel4.SetRowSpan(Me.btn_mg_seleccion, 2)
        Me.btn_mg_seleccion.Size = New System.Drawing.Size(47, 46)
        Me.btn_mg_seleccion.TabIndex = 3
        Me.btn_mg_seleccion.UseVisualStyleBackColor = True
        '
        'tb_mg_leyenda
        '
        Me.tb_mg_leyenda.Controls.Add(Me.TableLayoutPanel5)
        Me.tb_mg_leyenda.ImageKey = "legend.png"
        Me.tb_mg_leyenda.Location = New System.Drawing.Point(4, 23)
        Me.tb_mg_leyenda.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_mg_leyenda.Name = "tb_mg_leyenda"
        Me.tb_mg_leyenda.Size = New System.Drawing.Size(380, 590)
        Me.tb_mg_leyenda.TabIndex = 4
        Me.tb_mg_leyenda.Text = "Leyenda Geológica"
        Me.tb_mg_leyenda.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.GroupBox1, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 4
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(380, 590)
        Me.TableLayoutPanel5.TabIndex = 1
        '
        'GroupBox1
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel6)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 108)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(374, 156)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Generar leyenda"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 4
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label5, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_mg_generar_leyenda, 2, 4)
        Me.TableLayoutPanel6.Controls.Add(Me.cbx_mg_tiporoca, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label6, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.cbx_mg_dominio, 2, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label7, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_mg_draw, 2, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.lbl_mg_extent_legend, 1, 3)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 6
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(368, 137)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(8, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(242, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Seleccionar tipo de roca"
        '
        'btn_mg_generar_leyenda
        '
        Me.btn_mg_generar_leyenda.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_mg_generar_leyenda.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_generar_leyenda.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_generar_leyenda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_mg_generar_leyenda.FlatAppearance.BorderSize = 0
        Me.btn_mg_generar_leyenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_generar_leyenda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_mg_generar_leyenda.ImageIndex = 7
        Me.btn_mg_generar_leyenda.ImageList = Me.ilist_mg_50k
        Me.btn_mg_generar_leyenda.Location = New System.Drawing.Point(256, 99)
        Me.btn_mg_generar_leyenda.Name = "btn_mg_generar_leyenda"
        Me.btn_mg_generar_leyenda.Size = New System.Drawing.Size(104, 22)
        Me.btn_mg_generar_leyenda.TabIndex = 5
        Me.btn_mg_generar_leyenda.Text = "Generar"
        Me.btn_mg_generar_leyenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mg_generar_leyenda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btn_mg_generar_leyenda, "Inicia el proceso de generación de leyenda geológica para la hoja seleccionada")
        Me.btn_mg_generar_leyenda.UseVisualStyleBackColor = False
        '
        'cbx_mg_tiporoca
        '
        Me.cbx_mg_tiporoca.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_mg_tiporoca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mg_tiporoca.FormattingEnabled = True
        Me.cbx_mg_tiporoca.Location = New System.Drawing.Point(256, 3)
        Me.cbx_mg_tiporoca.Name = "cbx_mg_tiporoca"
        Me.cbx_mg_tiporoca.Size = New System.Drawing.Size(104, 21)
        Me.cbx_mg_tiporoca.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(7, 35)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(244, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Seleccionar dominio"
        '
        'cbx_mg_dominio
        '
        Me.cbx_mg_dominio.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mg_dominio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mg_dominio.FormattingEnabled = True
        Me.cbx_mg_dominio.Location = New System.Drawing.Point(255, 26)
        Me.cbx_mg_dominio.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mg_dominio.Name = "cbx_mg_dominio"
        Me.cbx_mg_dominio.Size = New System.Drawing.Size(106, 21)
        Me.cbx_mg_dominio.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(7, 59)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(244, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Establecer área para graficar la leyenda"
        '
        'btn_mg_draw
        '
        Me.btn_mg_draw.BackgroundImage = CType(resources.GetObject("btn_mg_draw.BackgroundImage"), System.Drawing.Image)
        Me.btn_mg_draw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_mg_draw.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_draw.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_mg_draw.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btn_mg_draw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_draw.ImageKey = "NetworkAnalystPolygonRestrictionBarrier32.png"
        Me.btn_mg_draw.Location = New System.Drawing.Point(321, 50)
        Me.btn_mg_draw.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_draw.Name = "btn_mg_draw"
        Me.TableLayoutPanel6.SetRowSpan(Me.btn_mg_draw, 2)
        Me.btn_mg_draw.Size = New System.Drawing.Size(40, 44)
        Me.btn_mg_draw.TabIndex = 15
        Me.btn_mg_draw.UseVisualStyleBackColor = True
        '
        'lbl_mg_extent_legend
        '
        Me.lbl_mg_extent_legend.AutoSize = True
        Me.lbl_mg_extent_legend.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mg_extent_legend.Location = New System.Drawing.Point(7, 83)
        Me.lbl_mg_extent_legend.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mg_extent_legend.Name = "lbl_mg_extent_legend"
        Me.lbl_mg_extent_legend.Size = New System.Drawing.Size(244, 13)
        Me.lbl_mg_extent_legend.TabIndex = 16
        Me.lbl_mg_extent_legend.Text = "..."
        '
        'GroupBox2
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.GroupBox2, 2)
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel7)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(374, 94)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 4
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label4, 1, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label2, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btn_mg_ver_tabla, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btn_mg_cargar_datos, 2, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(368, 75)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(8, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(242, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Cargar datos a la tabla de leyenda"
        Me.ToolTip1.SetToolTip(Me.Label4, "Este proceso reemplazará los datos de la tabla de leyenda para la hoja selecciona" &
        "da")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(8, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(242, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Mostrar la tabla de leyenda"
        Me.ToolTip1.SetToolTip(Me.Label2, "Agrega la tabla de leyenda a la tabla de contenido")
        '
        'btn_mg_ver_tabla
        '
        Me.btn_mg_ver_tabla.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_mg_ver_tabla.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_ver_tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_ver_tabla.FlatAppearance.BorderSize = 0
        Me.btn_mg_ver_tabla.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_ver_tabla.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btn_mg_ver_tabla.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_mg_ver_tabla.ImageIndex = 22
        Me.btn_mg_ver_tabla.ImageList = Me.ilist_mg_50k
        Me.btn_mg_ver_tabla.Location = New System.Drawing.Point(256, 3)
        Me.btn_mg_ver_tabla.Name = "btn_mg_ver_tabla"
        Me.btn_mg_ver_tabla.Size = New System.Drawing.Size(104, 22)
        Me.btn_mg_ver_tabla.TabIndex = 6
        Me.btn_mg_ver_tabla.Text = "Ver tabla"
        Me.btn_mg_ver_tabla.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mg_ver_tabla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btn_mg_ver_tabla, "Agrega la tabla de leyenda a la tabla de contenido")
        Me.btn_mg_ver_tabla.UseVisualStyleBackColor = False
        '
        'btn_mg_cargar_datos
        '
        Me.btn_mg_cargar_datos.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.btn_mg_cargar_datos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_cargar_datos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_cargar_datos.FlatAppearance.BorderSize = 0
        Me.btn_mg_cargar_datos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_cargar_datos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btn_mg_cargar_datos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_mg_cargar_datos.ImageIndex = 23
        Me.btn_mg_cargar_datos.ImageList = Me.ilist_mg_50k
        Me.btn_mg_cargar_datos.Location = New System.Drawing.Point(256, 31)
        Me.btn_mg_cargar_datos.Name = "btn_mg_cargar_datos"
        Me.btn_mg_cargar_datos.Size = New System.Drawing.Size(104, 22)
        Me.btn_mg_cargar_datos.TabIndex = 7
        Me.btn_mg_cargar_datos.Text = "Cargar"
        Me.btn_mg_cargar_datos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mg_cargar_datos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btn_mg_cargar_datos, "Este proceso reemplazará los datos de la tabla de leyenda para la hoja selecciona" &
        "da")
        Me.btn_mg_cargar_datos.UseVisualStyleBackColor = False
        '
        'tb_mg_perfil
        '
        Me.tb_mg_perfil.Controls.Add(Me.TableLayoutPanel2)
        Me.tb_mg_perfil.ImageIndex = 8
        Me.tb_mg_perfil.Location = New System.Drawing.Point(4, 23)
        Me.tb_mg_perfil.Margin = New System.Windows.Forms.Padding(2)
        Me.tb_mg_perfil.Name = "tb_mg_perfil"
        Me.tb_mg_perfil.Size = New System.Drawing.Size(380, 590)
        Me.tb_mg_perfil.TabIndex = 3
        Me.tb_mg_perfil.Text = "Sección"
        Me.tb_mg_perfil.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mg_pathdata, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mg_loaddata, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mg_cargar_dem, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mg_drawline, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mp_seccion, 0, 9)
        Me.TableLayoutPanel2.Controls.Add(Me.nud_mg_tolerancia, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mg_tolerancia, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.nud_mg_altura, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mg_loadShp, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mg_loadShp, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.rbtn_mg_drawline, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.rbtn_mg_loadshp, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.cbx_mg_inifin_sec, 1, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mg_XX, 0, 8)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 10
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(380, 590)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'tbx_mg_pathdata
        '
        Me.tbx_mg_pathdata.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mg_pathdata.Enabled = False
        Me.tbx_mg_pathdata.Location = New System.Drawing.Point(2, 30)
        Me.tbx_mg_pathdata.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mg_pathdata.Name = "tbx_mg_pathdata"
        Me.tbx_mg_pathdata.Size = New System.Drawing.Size(296, 20)
        Me.tbx_mg_pathdata.TabIndex = 0
        '
        'btn_mg_loaddata
        '
        Me.btn_mg_loaddata.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_loaddata.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_loaddata.Location = New System.Drawing.Point(302, 27)
        Me.btn_mg_loaddata.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_loaddata.Name = "btn_mg_loaddata"
        Me.btn_mg_loaddata.Size = New System.Drawing.Size(76, 26)
        Me.btn_mg_loaddata.TabIndex = 3
        Me.btn_mg_loaddata.Text = "..."
        Me.btn_mg_loaddata.UseVisualStyleBackColor = True
        '
        'lbl_mg_cargar_dem
        '
        Me.lbl_mg_cargar_dem.AutoSize = True
        Me.lbl_mg_cargar_dem.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mg_cargar_dem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_mg_cargar_dem.ImageIndex = 13
        Me.lbl_mg_cargar_dem.ImageList = Me.ilist_mg_50k
        Me.lbl_mg_cargar_dem.Location = New System.Drawing.Point(2, 12)
        Me.lbl_mg_cargar_dem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mg_cargar_dem.Name = "lbl_mg_cargar_dem"
        Me.lbl_mg_cargar_dem.Size = New System.Drawing.Size(296, 13)
        Me.lbl_mg_cargar_dem.TabIndex = 4
        Me.lbl_mg_cargar_dem.Text = "Cargar DEM"
        '
        'btn_mg_drawline
        '
        Me.btn_mg_drawline.BackColor = System.Drawing.SystemColors.Control
        Me.btn_mg_drawline.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_drawline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_drawline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_drawline.ImageIndex = 14
        Me.btn_mg_drawline.ImageList = Me.ilist_mg_50k
        Me.btn_mg_drawline.Location = New System.Drawing.Point(302, 117)
        Me.btn_mg_drawline.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_drawline.Name = "btn_mg_drawline"
        Me.btn_mg_drawline.Size = New System.Drawing.Size(76, 35)
        Me.btn_mg_drawline.TabIndex = 5
        Me.btn_mg_drawline.UseVisualStyleBackColor = False
        '
        'btn_mp_seccion
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.btn_mp_seccion, 2)
        Me.btn_mp_seccion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mp_seccion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mp_seccion.ImageKey = "3DAnalystInterpolateProfileGraphCreate16.png"
        Me.btn_mp_seccion.ImageList = Me.ilist_mg_50k
        Me.btn_mp_seccion.Location = New System.Drawing.Point(2, 558)
        Me.btn_mp_seccion.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mp_seccion.Name = "btn_mp_seccion"
        Me.btn_mp_seccion.Size = New System.Drawing.Size(376, 30)
        Me.btn_mp_seccion.TabIndex = 6
        Me.btn_mp_seccion.Text = "Generar sección geológica"
        Me.btn_mp_seccion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mp_seccion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mp_seccion.UseVisualStyleBackColor = True
        '
        'nud_mg_tolerancia
        '
        Me.nud_mg_tolerancia.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nud_mg_tolerancia.Location = New System.Drawing.Point(302, 60)
        Me.nud_mg_tolerancia.Margin = New System.Windows.Forms.Padding(2)
        Me.nud_mg_tolerancia.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nud_mg_tolerancia.Name = "nud_mg_tolerancia"
        Me.nud_mg_tolerancia.Size = New System.Drawing.Size(76, 20)
        Me.nud_mg_tolerancia.TabIndex = 8
        Me.nud_mg_tolerancia.Value = New Decimal(New Integer() {150, 0, 0, 0})
        '
        'lbl_mg_tolerancia
        '
        Me.lbl_mg_tolerancia.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_mg_tolerancia.AutoSize = True
        Me.lbl_mg_tolerancia.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_mg_tolerancia.ImageIndex = 11
        Me.lbl_mg_tolerancia.ImageList = Me.ilist_mg_50k
        Me.lbl_mg_tolerancia.Location = New System.Drawing.Point(2, 63)
        Me.lbl_mg_tolerancia.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mg_tolerancia.Name = "lbl_mg_tolerancia"
        Me.lbl_mg_tolerancia.Size = New System.Drawing.Size(296, 13)
        Me.lbl_mg_tolerancia.TabIndex = 7
        Me.lbl_mg_tolerancia.Text = "Establecer radio de búsqueda de POG's (m)"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.ImageIndex = 12
        Me.Label1.ImageList = Me.ilist_mg_50k
        Me.Label1.Location = New System.Drawing.Point(2, 93)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(296, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Establecer altura de inicio (m)"
        '
        'nud_mg_altura
        '
        Me.nud_mg_altura.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nud_mg_altura.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nud_mg_altura.Location = New System.Drawing.Point(302, 90)
        Me.nud_mg_altura.Margin = New System.Windows.Forms.Padding(2)
        Me.nud_mg_altura.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nud_mg_altura.Minimum = New Decimal(New Integer() {5000, 0, 0, -2147483648})
        Me.nud_mg_altura.Name = "nud_mg_altura"
        Me.nud_mg_altura.Size = New System.Drawing.Size(76, 20)
        Me.nud_mg_altura.TabIndex = 10
        '
        'btn_mg_loadShp
        '
        Me.btn_mg_loadShp.BackColor = System.Drawing.SystemColors.Control
        Me.btn_mg_loadShp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mg_loadShp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mg_loadShp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_mg_loadShp.Image = Global.Automapic.My.Resources.Resources.polilinea20
        Me.btn_mg_loadShp.Location = New System.Drawing.Point(302, 156)
        Me.btn_mg_loadShp.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mg_loadShp.Name = "btn_mg_loadShp"
        Me.btn_mg_loadShp.Size = New System.Drawing.Size(76, 35)
        Me.btn_mg_loadShp.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.btn_mg_loadShp, "El Shapefile (.shp) debe tener el mismo sistema de referencia" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "que el mapa y solo" &
        " contener un registro de tipo polilinea" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.btn_mg_loadShp.UseVisualStyleBackColor = False
        '
        'tbx_mg_loadShp
        '
        Me.tbx_mg_loadShp.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbx_mg_loadShp.Location = New System.Drawing.Point(2, 195)
        Me.tbx_mg_loadShp.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mg_loadShp.Name = "tbx_mg_loadShp"
        Me.tbx_mg_loadShp.ReadOnly = True
        Me.tbx_mg_loadShp.Size = New System.Drawing.Size(296, 20)
        Me.tbx_mg_loadShp.TabIndex = 15
        '
        'rbtn_mg_drawline
        '
        Me.rbtn_mg_drawline.AutoSize = True
        Me.rbtn_mg_drawline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbtn_mg_drawline.Location = New System.Drawing.Point(3, 118)
        Me.rbtn_mg_drawline.Name = "rbtn_mg_drawline"
        Me.rbtn_mg_drawline.Size = New System.Drawing.Size(294, 33)
        Me.rbtn_mg_drawline.TabIndex = 16
        Me.rbtn_mg_drawline.TabStop = True
        Me.rbtn_mg_drawline.Text = "Dibujar linea de sección"
        Me.rbtn_mg_drawline.UseVisualStyleBackColor = True
        '
        'rbtn_mg_loadshp
        '
        Me.rbtn_mg_loadshp.AutoSize = True
        Me.rbtn_mg_loadshp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbtn_mg_loadshp.Location = New System.Drawing.Point(3, 157)
        Me.rbtn_mg_loadshp.Name = "rbtn_mg_loadshp"
        Me.rbtn_mg_loadshp.Size = New System.Drawing.Size(294, 33)
        Me.rbtn_mg_loadshp.TabIndex = 17
        Me.rbtn_mg_loadshp.TabStop = True
        Me.rbtn_mg_loadshp.Text = "Cargar un .shp como linea de sección"
        Me.rbtn_mg_loadshp.UseVisualStyleBackColor = True
        '
        'cbx_mg_inifin_sec
        '
        Me.cbx_mg_inifin_sec.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_mg_inifin_sec.FormattingEnabled = True
        Me.cbx_mg_inifin_sec.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F"})
        Me.cbx_mg_inifin_sec.Location = New System.Drawing.Point(302, 225)
        Me.cbx_mg_inifin_sec.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mg_inifin_sec.Name = "cbx_mg_inifin_sec"
        Me.cbx_mg_inifin_sec.Size = New System.Drawing.Size(76, 21)
        Me.cbx_mg_inifin_sec.TabIndex = 18
        Me.cbx_mg_inifin_sec.Text = "A"
        '
        'lbl_mg_XX
        '
        Me.lbl_mg_XX.AutoSize = True
        Me.lbl_mg_XX.Location = New System.Drawing.Point(3, 228)
        Me.lbl_mg_XX.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.lbl_mg_XX.Name = "lbl_mg_XX"
        Me.lbl_mg_XX.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lbl_mg_XX.Size = New System.Drawing.Size(246, 13)
        Me.lbl_mg_XX.TabIndex = 19
        Me.lbl_mg_XX.Text = "Seleccionar o digitar inicio/ fin de linea de seccion"
        '
        'cbx_mg_fila
        '
        Me.cbx_mg_fila.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mg_fila.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mg_fila.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_mg_fila.Enabled = False
        Me.cbx_mg_fila.FormattingEnabled = True
        Me.cbx_mg_fila.Location = New System.Drawing.Point(65, 21)
        Me.cbx_mg_fila.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mg_fila.Name = "cbx_mg_fila"
        Me.cbx_mg_fila.Size = New System.Drawing.Size(47, 21)
        Me.cbx_mg_fila.TabIndex = 1
        '
        'cbx_mg_col
        '
        Me.cbx_mg_col.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mg_col.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mg_col.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_mg_col.Enabled = False
        Me.cbx_mg_col.FormattingEnabled = True
        Me.cbx_mg_col.Location = New System.Drawing.Point(172, 21)
        Me.cbx_mg_col.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mg_col.Name = "cbx_mg_col"
        Me.cbx_mg_col.Size = New System.Drawing.Size(47, 21)
        Me.cbx_mg_col.TabIndex = 2
        '
        'cbx_mg_cuad
        '
        Me.cbx_mg_cuad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mg_cuad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mg_cuad.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_mg_cuad.Enabled = False
        Me.cbx_mg_cuad.FormattingEnabled = True
        Me.cbx_mg_cuad.Location = New System.Drawing.Point(279, 21)
        Me.cbx_mg_cuad.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mg_cuad.Name = "cbx_mg_cuad"
        Me.cbx_mg_cuad.Size = New System.Drawing.Size(47, 21)
        Me.cbx_mg_cuad.TabIndex = 3
        '
        'btn_load_code
        '
        Me.btn_load_code.BackColor = System.Drawing.Color.Transparent
        Me.btn_load_code.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_load_code.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btn_load_code.Enabled = False
        Me.btn_load_code.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_load_code.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.btn_load_code.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.btn_load_code.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_load_code.ImageIndex = 5
        Me.btn_load_code.ImageList = Me.ilist_mg_50k
        Me.btn_load_code.Location = New System.Drawing.Point(335, 21)
        Me.btn_load_code.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_load_code.Name = "btn_load_code"
        Me.btn_load_code.Size = New System.Drawing.Size(55, 21)
        Me.btn_load_code.TabIndex = 9
        Me.btn_load_code.UseVisualStyleBackColor = False
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.MetroLabel1, 2)
        Me.MetroLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroLabel1.Location = New System.Drawing.Point(3, 10)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(57, 34)
        Me.MetroLabel1.TabIndex = 10
        Me.MetroLabel1.Text = "Fila"
        Me.MetroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroLabel2.Location = New System.Drawing.Point(117, 10)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(50, 34)
        Me.MetroLabel2.TabIndex = 11
        Me.MetroLabel2.Text = "Columna"
        Me.MetroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroLabel3.Location = New System.Drawing.Point(224, 10)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(50, 34)
        Me.MetroLabel3.TabIndex = 12
        Me.MetroLabel3.Text = "Cuadrante"
        Me.MetroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_dato_hoja
        '
        Me.lbl_dato_hoja.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_dato_hoja.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_dato_hoja, 9)
        Me.lbl_dato_hoja.Location = New System.Drawing.Point(3, 60)
        Me.lbl_dato_hoja.Name = "lbl_dato_hoja"
        Me.lbl_dato_hoja.Size = New System.Drawing.Size(386, 0)
        Me.lbl_dato_hoja.TabIndex = 13
        Me.lbl_dato_hoja.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        '
        'UserControl_CheckBoxAddLayers2
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.UserControl_CheckBoxAddLayers2, 2)
        Me.UserControl_CheckBoxAddLayers2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_CheckBoxAddLayers2.Location = New System.Drawing.Point(3, 3)
        Me.UserControl_CheckBoxAddLayers2.Name = "UserControl_CheckBoxAddLayers2"
        Me.UserControl_CheckBoxAddLayers2.Size = New System.Drawing.Size(370, 491)
        Me.UserControl_CheckBoxAddLayers2.TabIndex = 4
        '
        'Form_mapa_geologico_50k
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 697)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_mapa_geologico_50k"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tc_mg_50k.ResumeLayout(False)
        Me.tab_mg_topologia.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.tb_mg_query.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.tb_mg_leyenda.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.tb_mg_perfil.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.nud_mg_tolerancia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_mg_altura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbx_mg_fila As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mg_col As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mg_cuad As System.Windows.Forms.ComboBox
    Friend WithEvents ilist_mg_50k As System.Windows.Forms.ImageList
    Friend WithEvents btn_load_code As System.Windows.Forms.Button
    Friend WithEvents tc_mg_50k As System.Windows.Forms.TabControl
    Friend WithEvents tab_mg_topologia As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents clb_mg_topologias As System.Windows.Forms.CheckedListBox
    Friend WithEvents rbt_mg_seleccion As System.Windows.Forms.RadioButton
    Friend WithEvents btn_mg_run_topology As System.Windows.Forms.Button
    Friend WithEvents rbt_mg_actual As System.Windows.Forms.RadioButton
    Friend WithEvents btn_mg_SelectlayerByLocation As System.Windows.Forms.Button
    Friend WithEvents lbl_mg_topology_res As System.Windows.Forms.Label
    Friend WithEvents tb_mg_query As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UserControl_CheckBoxAddLayers1 As UserControl_CheckBoxAddLayers
    Friend WithEvents btn_mg_filtro As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_mg_seleccion As System.Windows.Forms.Button
    Friend WithEvents tb_mg_perfil As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_mg_pathdata As System.Windows.Forms.TextBox
    Friend WithEvents btn_mg_loaddata As System.Windows.Forms.Button
    Friend WithEvents btn_mg_drawline As System.Windows.Forms.Button
    Friend WithEvents btn_mp_seccion As System.Windows.Forms.Button
    Friend WithEvents nud_mg_tolerancia As System.Windows.Forms.NumericUpDown
    Friend WithEvents nud_mg_altura As System.Windows.Forms.NumericUpDown
    Friend WithEvents tb_mg_leyenda As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents btn_mg_generar_leyenda As System.Windows.Forms.Button
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents lbl_dato_hoja As MetroFramework.Controls.MetroLabel
    Friend WithEvents lbl_mg_cargar_dem As System.Windows.Forms.Label
    Friend WithEvents lbl_mg_tolerancia As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_mg_ver_tabla As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbx_mg_tiporoca As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbx_mg_dominio As System.Windows.Forms.ComboBox
    Friend WithEvents btn_mg_cargar_datos As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btn_mg_draw As System.Windows.Forms.Button
    Friend WithEvents lbl_mg_extent_legend As System.Windows.Forms.Label
    Friend WithEvents UserControl_CheckBoxAddLayers2 As UserControl_CheckBoxAddLayers
    Friend WithEvents btn_mg_loadShp As System.Windows.Forms.Button
    Friend WithEvents tbx_mg_loadShp As System.Windows.Forms.TextBox
    Friend WithEvents rbtn_mg_drawline As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_mg_loadshp As System.Windows.Forms.RadioButton
    Friend WithEvents cbx_mg_inifin_sec As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_mg_XX As System.Windows.Forms.Label
End Class
