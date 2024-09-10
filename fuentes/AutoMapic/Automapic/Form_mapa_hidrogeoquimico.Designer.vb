<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_mapa_hidrogeoquimico
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_hidrogeoquimico))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tc_mhq_procesos = New System.Windows.Forms.TabControl()
        Me.tp_mhq_procesos_ingreso = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lml_mhq_gdb = New System.Windows.Forms.Label()
        Me.btn_mhq_gdb = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbx_mhq_gdb = New System.Windows.Forms.TextBox()
        Me.lbl_mhq_excel = New System.Windows.Forms.Label()
        Me.tbx_mhq_excel = New System.Windows.Forms.TextBox()
        Me.btn_mhq_excel = New System.Windows.Forms.Button()
        Me.btn_mhq_cargar = New System.Windows.Forms.Button()
        Me.btn_mhq_calc_xls = New System.Windows.Forms.Button()
        Me.lbl_mhq_id_xls = New System.Windows.Forms.Label()
        Me.tp_mhq_mapa = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbx_mhq_shp_micro = New System.Windows.Forms.TextBox()
        Me.btn_mhq_shape_micro = New System.Windows.Forms.Button()
        Me.btn_mhq_generar_mapa = New System.Windows.Forms.Button()
        Me.cbx_mhq_zona = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tbx_mhq_shp_sub = New System.Windows.Forms.TextBox()
        Me.btn_mhq_shape_sub = New System.Windows.Forms.Button()
        Me.tp_mhq_rotulo = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbx_mhq_title = New System.Windows.Forms.TextBox()
        Me.tbx_mhq_subtitle = New System.Windows.Forms.TextBox()
        Me.ucl_mhq_autores = New Automapic.UserControl_CheckListBoxAutores()
        Me.btn_mhq_rotulo = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbx_mhq_rotulo_subcuenca = New System.Windows.Forms.TextBox()
        Me.tp_mhq_graficos = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_mhq_gdb_tab2 = New System.Windows.Forms.Label()
        Me.btn_mhq_ggraf = New System.Windows.Forms.Button()
        Me.chx_mhq_piper = New System.Windows.Forms.CheckBox()
        Me.chx_mhq_gibbs = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbx_mhq_cuenca = New System.Windows.Forms.ComboBox()
        Me.cbx_mhq_subcuenca = New System.Windows.Forms.ComboBox()
        Me.cbx_mhq_microcuenca = New System.Windows.Forms.ComboBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tc_mhq_procesos.SuspendLayout()
        Me.tp_mhq_procesos_ingreso.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.tp_mhq_mapa.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.tp_mhq_rotulo.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.tp_mhq_graficos.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tc_mhq_procesos, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mhq_cuenca, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mhq_subcuenca, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mhq_microcuenca, 0, 6)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(308, 550)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'tc_mhq_procesos
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tc_mhq_procesos, 2)
        Me.tc_mhq_procesos.Controls.Add(Me.tp_mhq_procesos_ingreso)
        Me.tc_mhq_procesos.Controls.Add(Me.tp_mhq_mapa)
        Me.tc_mhq_procesos.Controls.Add(Me.tp_mhq_rotulo)
        Me.tc_mhq_procesos.Controls.Add(Me.tp_mhq_graficos)
        Me.tc_mhq_procesos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_mhq_procesos.ImageList = Me.ImageList1
        Me.tc_mhq_procesos.Location = New System.Drawing.Point(3, 147)
        Me.tc_mhq_procesos.Name = "tc_mhq_procesos"
        Me.tc_mhq_procesos.SelectedIndex = 0
        Me.tc_mhq_procesos.Size = New System.Drawing.Size(302, 395)
        Me.tc_mhq_procesos.TabIndex = 0
        '
        'tp_mhq_procesos_ingreso
        '
        Me.tp_mhq_procesos_ingreso.Controls.Add(Me.TableLayoutPanel2)
        Me.tp_mhq_procesos_ingreso.Location = New System.Drawing.Point(4, 23)
        Me.tp_mhq_procesos_ingreso.Name = "tp_mhq_procesos_ingreso"
        Me.tp_mhq_procesos_ingreso.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_mhq_procesos_ingreso.Size = New System.Drawing.Size(294, 368)
        Me.tp_mhq_procesos_ingreso.TabIndex = 0
        Me.tp_mhq_procesos_ingreso.Text = "Ingreso de Datos"
        Me.tp_mhq_procesos_ingreso.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lml_mhq_gdb, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mhq_gdb, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mhq_gdb, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mhq_excel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mhq_excel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mhq_excel, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mhq_cargar, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mhq_calc_xls, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mhq_id_xls, 0, 6)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 10
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(288, 362)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'lml_mhq_gdb
        '
        Me.lml_mhq_gdb.AutoSize = True
        Me.lml_mhq_gdb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lml_mhq_gdb.Location = New System.Drawing.Point(3, 4)
        Me.lml_mhq_gdb.Name = "lml_mhq_gdb"
        Me.lml_mhq_gdb.Size = New System.Drawing.Size(222, 16)
        Me.lml_mhq_gdb.TabIndex = 0
        Me.lml_mhq_gdb.Text = "Seleccione GDB"
        '
        'btn_mhq_gdb
        '
        Me.btn_mhq_gdb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_mhq_gdb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_gdb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_gdb.ImageKey = "GeodatabaseConnectionAdd32.png"
        Me.btn_mhq_gdb.ImageList = Me.ImageList1
        Me.btn_mhq_gdb.Location = New System.Drawing.Point(231, 23)
        Me.btn_mhq_gdb.Name = "btn_mhq_gdb"
        Me.btn_mhq_gdb.Size = New System.Drawing.Size(54, 24)
        Me.btn_mhq_gdb.TabIndex = 1
        Me.btn_mhq_gdb.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "GeodatabaseConnectionAdd32.png")
        Me.ImageList1.Images.SetKeyName(1, "ExcelXLSFile16.png")
        Me.ImageList1.Images.SetKeyName(2, "EditCopy32.png")
        Me.ImageList1.Images.SetKeyName(3, "EditCopy16.png")
        Me.ImageList1.Images.SetKeyName(4, "GeodatabaseCreateFromFile16.png")
        Me.ImageList1.Images.SetKeyName(5, "LayerGraphics16.png")
        Me.ImageList1.Images.SetKeyName(6, "GeodatabaseConnectionAdd16.png")
        Me.ImageList1.Images.SetKeyName(7, "LayerGeneric16.png")
        Me.ImageList1.Images.SetKeyName(8, "LayerPolygon_C_16.png")
        Me.ImageList1.Images.SetKeyName(9, "RasterImageAnalysisHillshade16.png")
        Me.ImageList1.Images.SetKeyName(10, "LayerElevationNotVisible16.png")
        Me.ImageList1.Images.SetKeyName(11, "ContentsWindowElevationLayers16.png")
        Me.ImageList1.Images.SetKeyName(12, "Map16.png")
        Me.ImageList1.Images.SetKeyName(13, "Apply_to_View16.png")
        '
        'tbx_mhq_gdb
        '
        Me.tbx_mhq_gdb.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_gdb.Enabled = False
        Me.tbx_mhq_gdb.Location = New System.Drawing.Point(3, 25)
        Me.tbx_mhq_gdb.Name = "tbx_mhq_gdb"
        Me.tbx_mhq_gdb.Size = New System.Drawing.Size(222, 20)
        Me.tbx_mhq_gdb.TabIndex = 2
        '
        'lbl_mhq_excel
        '
        Me.lbl_mhq_excel.AutoSize = True
        Me.lbl_mhq_excel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_mhq_excel.Location = New System.Drawing.Point(3, 50)
        Me.lbl_mhq_excel.Name = "lbl_mhq_excel"
        Me.lbl_mhq_excel.Size = New System.Drawing.Size(222, 16)
        Me.lbl_mhq_excel.TabIndex = 3
        Me.lbl_mhq_excel.Text = "Seleccione archivo excel"
        '
        'tbx_mhq_excel
        '
        Me.tbx_mhq_excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_excel.Enabled = False
        Me.tbx_mhq_excel.Location = New System.Drawing.Point(3, 71)
        Me.tbx_mhq_excel.Name = "tbx_mhq_excel"
        Me.tbx_mhq_excel.Size = New System.Drawing.Size(222, 20)
        Me.tbx_mhq_excel.TabIndex = 4
        '
        'btn_mhq_excel
        '
        Me.btn_mhq_excel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_excel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_excel.ImageKey = "ExcelXLSFile16.png"
        Me.btn_mhq_excel.ImageList = Me.ImageList1
        Me.btn_mhq_excel.Location = New System.Drawing.Point(231, 69)
        Me.btn_mhq_excel.Name = "btn_mhq_excel"
        Me.btn_mhq_excel.Size = New System.Drawing.Size(54, 24)
        Me.btn_mhq_excel.TabIndex = 5
        Me.btn_mhq_excel.Text = "+"
        Me.btn_mhq_excel.UseVisualStyleBackColor = True
        '
        'btn_mhq_cargar
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.btn_mhq_cargar, 2)
        Me.btn_mhq_cargar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_cargar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_cargar.Enabled = False
        Me.btn_mhq_cargar.ImageKey = "GeodatabaseCreateFromFile16.png"
        Me.btn_mhq_cargar.ImageList = Me.ImageList1
        Me.btn_mhq_cargar.Location = New System.Drawing.Point(3, 145)
        Me.btn_mhq_cargar.Name = "btn_mhq_cargar"
        Me.btn_mhq_cargar.Size = New System.Drawing.Size(282, 24)
        Me.btn_mhq_cargar.TabIndex = 6
        Me.btn_mhq_cargar.Text = "Cargar Datos a GDB"
        Me.btn_mhq_cargar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mhq_cargar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mhq_cargar.UseVisualStyleBackColor = True
        '
        'btn_mhq_calc_xls
        '
        Me.btn_mhq_calc_xls.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_calc_xls.Enabled = False
        Me.btn_mhq_calc_xls.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_mhq_calc_xls.Location = New System.Drawing.Point(231, 107)
        Me.btn_mhq_calc_xls.Name = "btn_mhq_calc_xls"
        Me.btn_mhq_calc_xls.Size = New System.Drawing.Size(54, 21)
        Me.btn_mhq_calc_xls.TabIndex = 7
        Me.btn_mhq_calc_xls.Text = "Guardar"
        Me.btn_mhq_calc_xls.UseVisualStyleBackColor = True
        '
        'lbl_mhq_id_xls
        '
        Me.lbl_mhq_id_xls.AutoSize = True
        Me.lbl_mhq_id_xls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_mhq_id_xls.Location = New System.Drawing.Point(3, 104)
        Me.lbl_mhq_id_xls.Name = "lbl_mhq_id_xls"
        Me.lbl_mhq_id_xls.Size = New System.Drawing.Size(222, 30)
        Me.lbl_mhq_id_xls.TabIndex = 8
        Me.lbl_mhq_id_xls.Text = "Guardar xls con cálculos de iones (opcional)"
        Me.lbl_mhq_id_xls.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tp_mhq_mapa
        '
        Me.tp_mhq_mapa.Controls.Add(Me.TableLayoutPanel4)
        Me.tp_mhq_mapa.ImageIndex = 12
        Me.tp_mhq_mapa.Location = New System.Drawing.Point(4, 23)
        Me.tp_mhq_mapa.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mhq_mapa.Name = "tp_mhq_mapa"
        Me.tp_mhq_mapa.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mhq_mapa.Size = New System.Drawing.Size(294, 368)
        Me.tp_mhq_mapa.TabIndex = 2
        Me.tp_mhq_mapa.Text = "Mapa"
        Me.tp_mhq_mapa.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label4, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.tbx_mhq_shp_micro, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mhq_shape_micro, 1, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mhq_generar_mapa, 0, 8)
        Me.TableLayoutPanel4.Controls.Add(Me.cbx_mhq_zona, 1, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 0, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.Label13, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.tbx_mhq_shp_sub, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.btn_mhq_shape_sub, 1, 2)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 10
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(290, 364)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(2, 50)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(226, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Ingrese shape de Microcuenca (opcional)"
        '
        'tbx_mhq_shp_micro
        '
        Me.tbx_mhq_shp_micro.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_shp_micro.Enabled = False
        Me.tbx_mhq_shp_micro.Location = New System.Drawing.Point(2, 71)
        Me.tbx_mhq_shp_micro.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mhq_shp_micro.Name = "tbx_mhq_shp_micro"
        Me.tbx_mhq_shp_micro.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mhq_shp_micro.TabIndex = 2
        '
        'btn_mhq_shape_micro
        '
        Me.btn_mhq_shape_micro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_shape_micro.ImageIndex = 7
        Me.btn_mhq_shape_micro.ImageList = Me.ImageList1
        Me.btn_mhq_shape_micro.Location = New System.Drawing.Point(232, 68)
        Me.btn_mhq_shape_micro.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mhq_shape_micro.Name = "btn_mhq_shape_micro"
        Me.btn_mhq_shape_micro.Size = New System.Drawing.Size(56, 26)
        Me.btn_mhq_shape_micro.TabIndex = 4
        Me.btn_mhq_shape_micro.UseVisualStyleBackColor = True
        '
        'btn_mhq_generar_mapa
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.btn_mhq_generar_mapa, 2)
        Me.btn_mhq_generar_mapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_generar_mapa.Enabled = False
        Me.btn_mhq_generar_mapa.ImageKey = "Map16.png"
        Me.btn_mhq_generar_mapa.ImageList = Me.ImageList1
        Me.btn_mhq_generar_mapa.Location = New System.Drawing.Point(2, 140)
        Me.btn_mhq_generar_mapa.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mhq_generar_mapa.Name = "btn_mhq_generar_mapa"
        Me.btn_mhq_generar_mapa.Size = New System.Drawing.Size(286, 26)
        Me.btn_mhq_generar_mapa.TabIndex = 6
        Me.btn_mhq_generar_mapa.Text = "Generar mapa"
        Me.btn_mhq_generar_mapa.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mhq_generar_mapa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mhq_generar_mapa.UseVisualStyleBackColor = True
        '
        'cbx_mhq_zona
        '
        Me.cbx_mhq_zona.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mhq_zona.FormattingEnabled = True
        Me.cbx_mhq_zona.Location = New System.Drawing.Point(233, 108)
        Me.cbx_mhq_zona.Name = "cbx_mhq_zona"
        Me.cbx_mhq_zona.Size = New System.Drawing.Size(54, 21)
        Me.cbx_mhq_zona.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(224, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Zona Geográfica"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(2, 4)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(196, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Ingrese shape de Subcuenca (opcional)"
        '
        'tbx_mhq_shp_sub
        '
        Me.tbx_mhq_shp_sub.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_shp_sub.Enabled = False
        Me.tbx_mhq_shp_sub.Location = New System.Drawing.Point(2, 25)
        Me.tbx_mhq_shp_sub.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mhq_shp_sub.Name = "tbx_mhq_shp_sub"
        Me.tbx_mhq_shp_sub.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mhq_shp_sub.TabIndex = 11
        '
        'btn_mhq_shape_sub
        '
        Me.btn_mhq_shape_sub.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_mhq_shape_sub.ImageIndex = 7
        Me.btn_mhq_shape_sub.ImageList = Me.ImageList1
        Me.btn_mhq_shape_sub.Location = New System.Drawing.Point(232, 22)
        Me.btn_mhq_shape_sub.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mhq_shape_sub.Name = "btn_mhq_shape_sub"
        Me.btn_mhq_shape_sub.Size = New System.Drawing.Size(56, 26)
        Me.btn_mhq_shape_sub.TabIndex = 12
        Me.btn_mhq_shape_sub.UseVisualStyleBackColor = True
        '
        'tp_mhq_rotulo
        '
        Me.tp_mhq_rotulo.Controls.Add(Me.TableLayoutPanel5)
        Me.tp_mhq_rotulo.ImageIndex = 13
        Me.tp_mhq_rotulo.Location = New System.Drawing.Point(4, 23)
        Me.tp_mhq_rotulo.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mhq_rotulo.Name = "tp_mhq_rotulo"
        Me.tp_mhq_rotulo.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mhq_rotulo.Size = New System.Drawing.Size(294, 368)
        Me.tp_mhq_rotulo.TabIndex = 3
        Me.tp_mhq_rotulo.Text = "Rótulo"
        Me.tp_mhq_rotulo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label9, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Label10, 0, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.Label11, 0, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.tbx_mhq_title, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.tbx_mhq_subtitle, 0, 6)
        Me.TableLayoutPanel5.Controls.Add(Me.ucl_mhq_autores, 0, 8)
        Me.TableLayoutPanel5.Controls.Add(Me.btn_mhq_rotulo, 0, 9)
        Me.TableLayoutPanel5.Controls.Add(Me.Label12, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.tbx_mhq_rotulo_subcuenca, 0, 4)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 11
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(290, 364)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Título del Mapa"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Subtítulo del Mapa"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 142)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(98, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Seleccione autores"
        '
        'tbx_mhq_title
        '
        Me.tbx_mhq_title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_mhq_title.Location = New System.Drawing.Point(3, 23)
        Me.tbx_mhq_title.Name = "tbx_mhq_title"
        Me.tbx_mhq_title.Size = New System.Drawing.Size(284, 20)
        Me.tbx_mhq_title.TabIndex = 3
        Me.tbx_mhq_title.Text = "PROYECTO GA47C ESTUDIO GEOAMBIENTAL"
        '
        'tbx_mhq_subtitle
        '
        Me.tbx_mhq_subtitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_subtitle.Location = New System.Drawing.Point(3, 117)
        Me.tbx_mhq_subtitle.Name = "tbx_mhq_subtitle"
        Me.tbx_mhq_subtitle.Size = New System.Drawing.Size(284, 20)
        Me.tbx_mhq_subtitle.TabIndex = 4
        Me.tbx_mhq_subtitle.Text = "MAPA HIDROGEOQUÍMICO"
        '
        'ucl_mhq_autores
        '
        Me.ucl_mhq_autores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ucl_mhq_autores.Location = New System.Drawing.Point(2, 160)
        Me.ucl_mhq_autores.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ucl_mhq_autores.Name = "ucl_mhq_autores"
        Me.ucl_mhq_autores.Size = New System.Drawing.Size(286, 116)
        Me.ucl_mhq_autores.TabIndex = 5
        '
        'btn_mhq_rotulo
        '
        Me.btn_mhq_rotulo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_mhq_rotulo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_rotulo.ImageIndex = 13
        Me.btn_mhq_rotulo.ImageList = Me.ImageList1
        Me.btn_mhq_rotulo.Location = New System.Drawing.Point(3, 281)
        Me.btn_mhq_rotulo.Name = "btn_mhq_rotulo"
        Me.btn_mhq_rotulo.Size = New System.Drawing.Size(284, 24)
        Me.btn_mhq_rotulo.TabIndex = 6
        Me.btn_mhq_rotulo.Text = "Actualizar rótulo"
        Me.btn_mhq_rotulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mhq_rotulo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mhq_rotulo.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Location = New System.Drawing.Point(3, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(284, 16)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Subcuenca"
        '
        'tbx_mhq_rotulo_subcuenca
        '
        Me.tbx_mhq_rotulo_subcuenca.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mhq_rotulo_subcuenca.Location = New System.Drawing.Point(3, 71)
        Me.tbx_mhq_rotulo_subcuenca.Name = "tbx_mhq_rotulo_subcuenca"
        Me.tbx_mhq_rotulo_subcuenca.Size = New System.Drawing.Size(284, 20)
        Me.tbx_mhq_rotulo_subcuenca.TabIndex = 8
        '
        'tp_mhq_graficos
        '
        Me.tp_mhq_graficos.Controls.Add(Me.TableLayoutPanel3)
        Me.tp_mhq_graficos.ImageIndex = 5
        Me.tp_mhq_graficos.Location = New System.Drawing.Point(4, 23)
        Me.tp_mhq_graficos.Name = "tp_mhq_graficos"
        Me.tp_mhq_graficos.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_mhq_graficos.Size = New System.Drawing.Size(294, 368)
        Me.tp_mhq_graficos.TabIndex = 1
        Me.tp_mhq_graficos.Text = "Gráficos"
        Me.tp_mhq_graficos.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_mhq_gdb_tab2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mhq_ggraf, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.chx_mhq_piper, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.chx_mhq_gibbs, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label8, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label7, 0, 3)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 7
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(288, 362)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'lbl_mhq_gdb_tab2
        '
        Me.lbl_mhq_gdb_tab2.AutoSize = True
        Me.lbl_mhq_gdb_tab2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_mhq_gdb_tab2.Location = New System.Drawing.Point(2, 4)
        Me.lbl_mhq_gdb_tab2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mhq_gdb_tab2.Name = "lbl_mhq_gdb_tab2"
        Me.lbl_mhq_gdb_tab2.Size = New System.Drawing.Size(224, 16)
        Me.lbl_mhq_gdb_tab2.TabIndex = 0
        Me.lbl_mhq_gdb_tab2.Text = "Volver a generar gráficos"
        '
        'btn_mhq_ggraf
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.btn_mhq_ggraf, 2)
        Me.btn_mhq_ggraf.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mhq_ggraf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mhq_ggraf.ImageKey = "LayerGraphics16.png"
        Me.btn_mhq_ggraf.ImageList = Me.ImageList1
        Me.btn_mhq_ggraf.Location = New System.Drawing.Point(2, 82)
        Me.btn_mhq_ggraf.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mhq_ggraf.Name = "btn_mhq_ggraf"
        Me.btn_mhq_ggraf.Size = New System.Drawing.Size(284, 24)
        Me.btn_mhq_ggraf.TabIndex = 3
        Me.btn_mhq_ggraf.Text = "Generar gráficos"
        Me.btn_mhq_ggraf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mhq_ggraf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mhq_ggraf.UseVisualStyleBackColor = True
        '
        'chx_mhq_piper
        '
        Me.chx_mhq_piper.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.chx_mhq_piper.AutoSize = True
        Me.chx_mhq_piper.Checked = True
        Me.chx_mhq_piper.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chx_mhq_piper.Location = New System.Drawing.Point(250, 23)
        Me.chx_mhq_piper.Name = "chx_mhq_piper"
        Me.chx_mhq_piper.Size = New System.Drawing.Size(15, 24)
        Me.chx_mhq_piper.TabIndex = 6
        Me.chx_mhq_piper.UseVisualStyleBackColor = True
        '
        'chx_mhq_gibbs
        '
        Me.chx_mhq_gibbs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.chx_mhq_gibbs.AutoSize = True
        Me.chx_mhq_gibbs.Checked = True
        Me.chx_mhq_gibbs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chx_mhq_gibbs.Location = New System.Drawing.Point(250, 53)
        Me.chx_mhq_gibbs.Name = "chx_mhq_gibbs"
        Me.chx_mhq_gibbs.Size = New System.Drawing.Size(15, 24)
        Me.chx_mhq_gibbs.TabIndex = 7
        Me.chx_mhq_gibbs.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(222, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Diagramas de Piper"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(222, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Diagramas de Gibbs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(2, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(236, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Seleccione Cuenca"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(2, 50)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(236, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Seleccione Subcuenca"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(2, 94)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(236, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Seleccione Microcuenca(opcional)"
        '
        'cbx_mhq_cuenca
        '
        Me.cbx_mhq_cuenca.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_mhq_cuenca.FormattingEnabled = True
        Me.cbx_mhq_cuenca.Location = New System.Drawing.Point(2, 22)
        Me.cbx_mhq_cuenca.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mhq_cuenca.Name = "cbx_mhq_cuenca"
        Me.cbx_mhq_cuenca.Size = New System.Drawing.Size(236, 21)
        Me.cbx_mhq_cuenca.TabIndex = 4
        '
        'cbx_mhq_subcuenca
        '
        Me.cbx_mhq_subcuenca.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_mhq_subcuenca.FormattingEnabled = True
        Me.cbx_mhq_subcuenca.Location = New System.Drawing.Point(2, 68)
        Me.cbx_mhq_subcuenca.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mhq_subcuenca.Name = "cbx_mhq_subcuenca"
        Me.cbx_mhq_subcuenca.Size = New System.Drawing.Size(236, 21)
        Me.cbx_mhq_subcuenca.TabIndex = 5
        '
        'cbx_mhq_microcuenca
        '
        Me.cbx_mhq_microcuenca.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_mhq_microcuenca.FormattingEnabled = True
        Me.cbx_mhq_microcuenca.Location = New System.Drawing.Point(2, 112)
        Me.cbx_mhq_microcuenca.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mhq_microcuenca.Name = "cbx_mhq_microcuenca"
        Me.cbx_mhq_microcuenca.Size = New System.Drawing.Size(236, 21)
        Me.cbx_mhq_microcuenca.TabIndex = 6
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "xls"
        Me.OpenFileDialog1.Filter = "Excel 97-2003|*.xls|Excel |*.xlsx|Todos los archivos|*.*"
        Me.OpenFileDialog1.FilterIndex = 2
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.Title = "Abrir"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.CheckPathExists = False
        Me.SaveFileDialog1.DefaultExt = "xlsx"
        Me.SaveFileDialog1.Filter = "Excel 97-2003|*.xls|Excel |*.xlsx|Todos los archivos|*.*"
        Me.SaveFileDialog1.FilterIndex = 2
        Me.SaveFileDialog1.Title = "Guardar xlsx"
        '
        'Form_mapa_hidrogeoquimico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(308, 550)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_mapa_hidrogeoquimico"
        Me.Text = "Form_mapa_hidrogeoquimico"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tc_mhq_procesos.ResumeLayout(False)
        Me.tp_mhq_procesos_ingreso.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.tp_mhq_mapa.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.tp_mhq_rotulo.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.tp_mhq_graficos.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents tc_mhq_procesos As System.Windows.Forms.TabControl
    Friend WithEvents tp_mhq_procesos_ingreso As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lml_mhq_gdb As System.Windows.Forms.Label
    Friend WithEvents btn_mhq_gdb As System.Windows.Forms.Button
    Friend WithEvents tbx_mhq_gdb As System.Windows.Forms.TextBox
    Friend WithEvents lbl_mhq_excel As System.Windows.Forms.Label
    Friend WithEvents tbx_mhq_excel As System.Windows.Forms.TextBox
    Friend WithEvents btn_mhq_excel As System.Windows.Forms.Button
    Friend WithEvents btn_mhq_cargar As System.Windows.Forms.Button
    Friend WithEvents btn_mhq_calc_xls As System.Windows.Forms.Button
    Friend WithEvents lbl_mhq_id_xls As System.Windows.Forms.Label
    Friend WithEvents tp_mhq_graficos As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_mhq_gdb_tab2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbx_mhq_cuenca As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mhq_subcuenca As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_mhq_microcuenca As System.Windows.Forms.ComboBox
    Friend WithEvents btn_mhq_ggraf As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tp_mhq_mapa As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tp_mhq_rotulo As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbx_mhq_shp_micro As System.Windows.Forms.TextBox
    Friend WithEvents btn_mhq_shape_micro As System.Windows.Forms.Button
    Friend WithEvents btn_mhq_generar_mapa As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbx_mhq_zona As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chx_mhq_piper As System.Windows.Forms.CheckBox
    Friend WithEvents chx_mhq_gibbs As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbx_mhq_title As System.Windows.Forms.TextBox
    Friend WithEvents tbx_mhq_subtitle As System.Windows.Forms.TextBox
    Friend WithEvents ucl_mhq_autores As UserControl_CheckListBoxAutores
    Friend WithEvents btn_mhq_rotulo As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbx_mhq_rotulo_subcuenca As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tbx_mhq_shp_sub As System.Windows.Forms.TextBox
    Friend WithEvents btn_mhq_shape_sub As System.Windows.Forms.Button
End Class
