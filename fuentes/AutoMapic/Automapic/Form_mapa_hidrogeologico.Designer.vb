<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_mapa_hidrogeologico
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_hidrogeologico))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cbx_mh_cuencas = New System.Windows.Forms.ComboBox()
        Me.lbx_mh_cuencas = New System.Windows.Forms.ListBox()
        Me.lbl_mh_nota1 = New System.Windows.Forms.Label()
        Me.lbl_mh_cuenca = New System.Windows.Forms.Label()
        Me.tc_mh_tools = New System.Windows.Forms.TabControl()
        Me.tp_mh_capas = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.UserControl_CheckBoxAddLayers1 = New Automapic.UserControl_CheckBoxAddLayers()
        Me.btn_mgh_extrerdatos = New System.Windows.Forms.Button()
        Me.tp_mh_rotulo = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.clb_mh_autores = New System.Windows.Forms.CheckedListBox()
        Me.lbl_mh_autor = New System.Windows.Forms.Label()
        Me.img_list_mh = New System.Windows.Forms.ImageList(Me.components)
        Me.rbt_mh_pequenio = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbx_mh_autores = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_mh_title1 = New System.Windows.Forms.TextBox()
        Me.tbx_mh_title2 = New System.Windows.Forms.TextBox()
        Me.btn_mh_grotulo = New System.Windows.Forms.Button()
        Me.nud_mh_numero = New System.Windows.Forms.NumericUpDown()
        Me.lbl_mh_numero = New System.Windows.Forms.Label()
        Me.rbt_mh_grande = New System.Windows.Forms.RadioButton()
        Me.tp_mh_leyenda = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_mh_leyenda = New System.Windows.Forms.Button()
        Me.tc_mh_leyenda_tools = New System.Windows.Forms.TabControl()
        Me.tp_mh_classif = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_mh_leyenda = New System.Windows.Forms.DataGridView()
        Me.lbl_mh_uhcount = New System.Windows.Forms.Label()
        Me.tp_mh_dhidrog = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_mh_descriph = New System.Windows.Forms.TextBox()
        Me.tvw_mh_descriph = New System.Windows.Forms.TreeView()
        Me.UserControl_ComboBoxDataframes1 = New Automapic.UserControl_ComboBoxDataframes()
        Me.tp_mh_mapa_ubicacion = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.UserControl_ComboBoxDataframes2 = New Automapic.UserControl_ComboBoxDataframes()
        Me.rbt_mhg_nacional = New System.Windows.Forms.RadioButton()
        Me.rbt_mhg_cuenca = New System.Windows.Forms.RadioButton()
        Me.btn_mgh_generar_mu = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tc_mh_tools.SuspendLayout()
        Me.tp_mh_capas.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.tp_mh_rotulo.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.nud_mh_numero, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_mh_leyenda.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tc_mh_leyenda_tools.SuspendLayout()
        Me.tp_mh_classif.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.dgv_mh_leyenda, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_mh_dhidrog.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.tp_mh_mapa_ubicacion.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mh_cuencas, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbx_mh_cuencas, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mh_nota1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mh_cuenca, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tc_mh_tools, 0, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(347, 570)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'cbx_mh_cuencas
        '
        Me.cbx_mh_cuencas.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_mh_cuencas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_mh_cuencas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_mh_cuencas.FormattingEnabled = True
        Me.cbx_mh_cuencas.Location = New System.Drawing.Point(2, 22)
        Me.cbx_mh_cuencas.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mh_cuencas.Name = "cbx_mh_cuencas"
        Me.cbx_mh_cuencas.Size = New System.Drawing.Size(343, 21)
        Me.cbx_mh_cuencas.TabIndex = 0
        '
        'lbx_mh_cuencas
        '
        Me.lbx_mh_cuencas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbx_mh_cuencas.FormattingEnabled = True
        Me.lbx_mh_cuencas.Location = New System.Drawing.Point(2, 46)
        Me.lbx_mh_cuencas.Margin = New System.Windows.Forms.Padding(2)
        Me.lbx_mh_cuencas.Name = "lbx_mh_cuencas"
        Me.lbx_mh_cuencas.Size = New System.Drawing.Size(343, 61)
        Me.lbx_mh_cuencas.TabIndex = 2
        '
        'lbl_mh_nota1
        '
        Me.lbl_mh_nota1.AutoSize = True
        Me.lbl_mh_nota1.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_mh_nota1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_mh_nota1.Location = New System.Drawing.Point(2, 109)
        Me.lbl_mh_nota1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mh_nota1.Name = "lbl_mh_nota1"
        Me.lbl_mh_nota1.Size = New System.Drawing.Size(343, 13)
        Me.lbl_mh_nota1.TabIndex = 3
        Me.lbl_mh_nota1.Text = "(*) Doble clic para eliminar item"
        Me.lbl_mh_nota1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbl_mh_cuenca
        '
        Me.lbl_mh_cuenca.AutoSize = True
        Me.lbl_mh_cuenca.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mh_cuenca.Location = New System.Drawing.Point(2, 7)
        Me.lbl_mh_cuenca.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mh_cuenca.Name = "lbl_mh_cuenca"
        Me.lbl_mh_cuenca.Size = New System.Drawing.Size(343, 13)
        Me.lbl_mh_cuenca.TabIndex = 4
        Me.lbl_mh_cuenca.Text = "Seleccionar cuenca"
        '
        'tc_mh_tools
        '
        Me.tc_mh_tools.Controls.Add(Me.tp_mh_capas)
        Me.tc_mh_tools.Controls.Add(Me.tp_mh_rotulo)
        Me.tc_mh_tools.Controls.Add(Me.tp_mh_leyenda)
        Me.tc_mh_tools.Controls.Add(Me.tp_mh_mapa_ubicacion)
        Me.tc_mh_tools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_mh_tools.ImageList = Me.img_list_mh
        Me.tc_mh_tools.Location = New System.Drawing.Point(2, 127)
        Me.tc_mh_tools.Margin = New System.Windows.Forms.Padding(2)
        Me.tc_mh_tools.Name = "tc_mh_tools"
        Me.tc_mh_tools.SelectedIndex = 0
        Me.tc_mh_tools.Size = New System.Drawing.Size(343, 441)
        Me.tc_mh_tools.TabIndex = 5
        '
        'tp_mh_capas
        '
        Me.tp_mh_capas.Controls.Add(Me.TableLayoutPanel6)
        Me.tp_mh_capas.ImageIndex = 8
        Me.tp_mh_capas.Location = New System.Drawing.Point(4, 23)
        Me.tp_mh_capas.Name = "tp_mh_capas"
        Me.tp_mh_capas.Size = New System.Drawing.Size(335, 414)
        Me.tp_mh_capas.TabIndex = 2
        Me.tp_mh_capas.Text = "Capas"
        Me.tp_mh_capas.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 414.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 414.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(335, 414)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.UserControl_CheckBoxAddLayers1, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btn_mgh_extrerdatos, 1, 1)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(331, 410)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'UserControl_CheckBoxAddLayers1
        '
        Me.TableLayoutPanel7.SetColumnSpan(Me.UserControl_CheckBoxAddLayers1, 2)
        Me.UserControl_CheckBoxAddLayers1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_CheckBoxAddLayers1.Location = New System.Drawing.Point(4, 4)
        Me.UserControl_CheckBoxAddLayers1.Margin = New System.Windows.Forms.Padding(4)
        Me.UserControl_CheckBoxAddLayers1.Name = "UserControl_CheckBoxAddLayers1"
        Me.UserControl_CheckBoxAddLayers1.Size = New System.Drawing.Size(323, 374)
        Me.UserControl_CheckBoxAddLayers1.TabIndex = 0
        '
        'btn_mgh_extrerdatos
        '
        Me.btn_mgh_extrerdatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mgh_extrerdatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mgh_extrerdatos.Location = New System.Drawing.Point(221, 384)
        Me.btn_mgh_extrerdatos.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mgh_extrerdatos.Name = "btn_mgh_extrerdatos"
        Me.btn_mgh_extrerdatos.Size = New System.Drawing.Size(108, 24)
        Me.btn_mgh_extrerdatos.TabIndex = 1
        Me.btn_mgh_extrerdatos.Text = "Extraer datos"
        Me.btn_mgh_extrerdatos.UseVisualStyleBackColor = True
        '
        'tp_mh_rotulo
        '
        Me.tp_mh_rotulo.Controls.Add(Me.TableLayoutPanel2)
        Me.tp_mh_rotulo.ImageIndex = 3
        Me.tp_mh_rotulo.Location = New System.Drawing.Point(4, 23)
        Me.tp_mh_rotulo.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mh_rotulo.Name = "tp_mh_rotulo"
        Me.tp_mh_rotulo.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mh_rotulo.Size = New System.Drawing.Size(335, 414)
        Me.tp_mh_rotulo.TabIndex = 0
        Me.tp_mh_rotulo.Text = "Rótulo"
        Me.tp_mh_rotulo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.clb_mh_autores, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mh_autor, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.rbt_mh_pequenio, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mh_autores, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 10)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mh_title1, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_mh_title2, 0, 11)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_mh_grotulo, 0, 14)
        Me.TableLayoutPanel2.Controls.Add(Me.nud_mh_numero, 2, 12)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_mh_numero, 0, 12)
        Me.TableLayoutPanel2.Controls.Add(Me.rbt_mh_grande, 1, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 15
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(331, 410)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'clb_mh_autores
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.clb_mh_autores, 3)
        Me.clb_mh_autores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clb_mh_autores.FormattingEnabled = True
        Me.clb_mh_autores.Location = New System.Drawing.Point(2, 58)
        Me.clb_mh_autores.Margin = New System.Windows.Forms.Padding(2)
        Me.clb_mh_autores.MultiColumn = True
        Me.clb_mh_autores.Name = "clb_mh_autores"
        Me.clb_mh_autores.Size = New System.Drawing.Size(327, 118)
        Me.clb_mh_autores.TabIndex = 1
        '
        'lbl_mh_autor
        '
        Me.lbl_mh_autor.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.lbl_mh_autor, 3)
        Me.lbl_mh_autor.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mh_autor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_mh_autor.ImageIndex = 7
        Me.lbl_mh_autor.ImageList = Me.img_list_mh
        Me.lbl_mh_autor.Location = New System.Drawing.Point(2, 43)
        Me.lbl_mh_autor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mh_autor.Name = "lbl_mh_autor"
        Me.lbl_mh_autor.Size = New System.Drawing.Size(327, 13)
        Me.lbl_mh_autor.TabIndex = 0
        Me.lbl_mh_autor.Text = "Seleccione los autores"
        '
        'img_list_mh
        '
        Me.img_list_mh.ImageStream = CType(resources.GetObject("img_list_mh.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img_list_mh.TransparentColor = System.Drawing.Color.Transparent
        Me.img_list_mh.Images.SetKeyName(0, "GeoprocessingFunction32.png")
        Me.img_list_mh.Images.SetKeyName(1, "ServiceProcessing16.png")
        Me.img_list_mh.Images.SetKeyName(2, "TabletPC32.png")
        Me.img_list_mh.Images.SetKeyName(3, "Apply_to_View16.png")
        Me.img_list_mh.Images.SetKeyName(4, "Apply_to_View24.png")
        Me.img_list_mh.Images.SetKeyName(5, "Apply_to_View32.png")
        Me.img_list_mh.Images.SetKeyName(6, "Legend16.png")
        Me.img_list_mh.Images.SetKeyName(7, "GenericCheckMarkSmall16.png")
        Me.img_list_mh.Images.SetKeyName(8, "LayerTree16.png")
        Me.img_list_mh.Images.SetKeyName(9, "MapFrame16.png")
        '
        'rbt_mh_pequenio
        '
        Me.rbt_mh_pequenio.AutoSize = True
        Me.rbt_mh_pequenio.Checked = True
        Me.rbt_mh_pequenio.Location = New System.Drawing.Point(2, 18)
        Me.rbt_mh_pequenio.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mh_pequenio.Name = "rbt_mh_pequenio"
        Me.rbt_mh_pequenio.Size = New System.Drawing.Size(144, 16)
        Me.rbt_mh_pequenio.TabIndex = 2
        Me.rbt_mh_pequenio.TabStop = True
        Me.rbt_mh_pequenio.Text = "Pequeño (escala grande)"
        Me.rbt_mh_pequenio.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label1, 3)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(2, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(327, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Seleccione tamaño de rotulo"
        '
        'tbx_mh_autores
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.tbx_mh_autores, 3)
        Me.tbx_mh_autores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_mh_autores.Location = New System.Drawing.Point(2, 180)
        Me.tbx_mh_autores.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mh_autores.Name = "tbx_mh_autores"
        Me.tbx_mh_autores.Size = New System.Drawing.Size(327, 20)
        Me.tbx_mh_autores.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label2, 3)
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(2, 205)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(327, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Título 1 (cuencas)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label3, 3)
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(2, 249)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(327, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Título 2 (tipo de mapa)"
        '
        'tbx_mh_title1
        '
        Me.tbx_mh_title1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.tbx_mh_title1, 3)
        Me.tbx_mh_title1.Location = New System.Drawing.Point(2, 220)
        Me.tbx_mh_title1.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mh_title1.Name = "tbx_mh_title1"
        Me.tbx_mh_title1.Size = New System.Drawing.Size(327, 20)
        Me.tbx_mh_title1.TabIndex = 8
        '
        'tbx_mh_title2
        '
        Me.tbx_mh_title2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.tbx_mh_title2, 3)
        Me.tbx_mh_title2.Location = New System.Drawing.Point(2, 264)
        Me.tbx_mh_title2.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mh_title2.Name = "tbx_mh_title2"
        Me.tbx_mh_title2.Size = New System.Drawing.Size(327, 20)
        Me.tbx_mh_title2.TabIndex = 9
        Me.tbx_mh_title2.Text = "Mapa Hidrogeológico"
        '
        'btn_mh_grotulo
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.btn_mh_grotulo, 3)
        Me.btn_mh_grotulo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mh_grotulo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btn_mh_grotulo.ImageIndex = 3
        Me.btn_mh_grotulo.ImageList = Me.img_list_mh
        Me.btn_mh_grotulo.Location = New System.Drawing.Point(2, 384)
        Me.btn_mh_grotulo.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mh_grotulo.Name = "btn_mh_grotulo"
        Me.btn_mh_grotulo.Size = New System.Drawing.Size(327, 24)
        Me.btn_mh_grotulo.TabIndex = 10
        Me.btn_mh_grotulo.Text = "Generar rótulo"
        Me.btn_mh_grotulo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mh_grotulo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mh_grotulo.UseVisualStyleBackColor = True
        '
        'nud_mh_numero
        '
        Me.nud_mh_numero.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nud_mh_numero.DecimalPlaces = 1
        Me.nud_mh_numero.Location = New System.Drawing.Point(288, 288)
        Me.nud_mh_numero.Margin = New System.Windows.Forms.Padding(2)
        Me.nud_mh_numero.Name = "nud_mh_numero"
        Me.nud_mh_numero.Size = New System.Drawing.Size(41, 20)
        Me.nud_mh_numero.TabIndex = 11
        '
        'lbl_mh_numero
        '
        Me.lbl_mh_numero.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_mh_numero.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.lbl_mh_numero, 2)
        Me.lbl_mh_numero.Location = New System.Drawing.Point(2, 289)
        Me.lbl_mh_numero.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mh_numero.Name = "lbl_mh_numero"
        Me.lbl_mh_numero.Size = New System.Drawing.Size(282, 13)
        Me.lbl_mh_numero.TabIndex = 12
        Me.lbl_mh_numero.Text = "Especifique el número de mapa"
        '
        'rbt_mh_grande
        '
        Me.rbt_mh_grande.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.rbt_mh_grande, 2)
        Me.rbt_mh_grande.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbt_mh_grande.Location = New System.Drawing.Point(152, 18)
        Me.rbt_mh_grande.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mh_grande.Name = "rbt_mh_grande"
        Me.rbt_mh_grande.Size = New System.Drawing.Size(145, 16)
        Me.rbt_mh_grande.TabIndex = 3
        Me.rbt_mh_grande.Text = "Grande (escala pequeña)"
        Me.rbt_mh_grande.UseVisualStyleBackColor = True
        '
        'tp_mh_leyenda
        '
        Me.tp_mh_leyenda.AutoScroll = True
        Me.tp_mh_leyenda.Controls.Add(Me.TableLayoutPanel3)
        Me.tp_mh_leyenda.ImageIndex = 6
        Me.tp_mh_leyenda.Location = New System.Drawing.Point(4, 23)
        Me.tp_mh_leyenda.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mh_leyenda.Name = "tp_mh_leyenda"
        Me.tp_mh_leyenda.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mh_leyenda.Size = New System.Drawing.Size(335, 414)
        Me.tp_mh_leyenda.TabIndex = 1
        Me.tp_mh_leyenda.Text = "Leyenda"
        Me.tp_mh_leyenda.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mh_leyenda, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.tc_mh_leyenda_tools, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.UserControl_ComboBoxDataframes1, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(331, 410)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'btn_mh_leyenda
        '
        Me.btn_mh_leyenda.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mh_leyenda.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mh_leyenda.ImageIndex = 6
        Me.btn_mh_leyenda.ImageList = Me.img_list_mh
        Me.btn_mh_leyenda.Location = New System.Drawing.Point(2, 384)
        Me.btn_mh_leyenda.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mh_leyenda.Name = "btn_mh_leyenda"
        Me.btn_mh_leyenda.Size = New System.Drawing.Size(327, 24)
        Me.btn_mh_leyenda.TabIndex = 0
        Me.btn_mh_leyenda.Text = "Generar Leyenda"
        Me.btn_mh_leyenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mh_leyenda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mh_leyenda.UseVisualStyleBackColor = True
        '
        'tc_mh_leyenda_tools
        '
        Me.tc_mh_leyenda_tools.Controls.Add(Me.tp_mh_classif)
        Me.tc_mh_leyenda_tools.Controls.Add(Me.tp_mh_dhidrog)
        Me.tc_mh_leyenda_tools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_mh_leyenda_tools.Location = New System.Drawing.Point(2, 2)
        Me.tc_mh_leyenda_tools.Margin = New System.Windows.Forms.Padding(2)
        Me.tc_mh_leyenda_tools.Name = "tc_mh_leyenda_tools"
        Me.tc_mh_leyenda_tools.SelectedIndex = 0
        Me.tc_mh_leyenda_tools.Size = New System.Drawing.Size(327, 333)
        Me.tc_mh_leyenda_tools.TabIndex = 0
        '
        'tp_mh_classif
        '
        Me.tp_mh_classif.Controls.Add(Me.TableLayoutPanel4)
        Me.tp_mh_classif.Location = New System.Drawing.Point(4, 22)
        Me.tp_mh_classif.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mh_classif.Name = "tp_mh_classif"
        Me.tp_mh_classif.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mh_classif.Size = New System.Drawing.Size(319, 307)
        Me.tp_mh_classif.TabIndex = 0
        Me.tp_mh_classif.Text = "Clasificación"
        Me.tp_mh_classif.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.dgv_mh_leyenda, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_mh_uhcount, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(315, 303)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'dgv_mh_leyenda
        '
        Me.dgv_mh_leyenda.AllowUserToAddRows = False
        Me.dgv_mh_leyenda.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgv_mh_leyenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_mh_leyenda.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_mh_leyenda.Location = New System.Drawing.Point(2, 2)
        Me.dgv_mh_leyenda.Margin = New System.Windows.Forms.Padding(2)
        Me.dgv_mh_leyenda.Name = "dgv_mh_leyenda"
        Me.dgv_mh_leyenda.RowTemplate.Height = 24
        Me.dgv_mh_leyenda.Size = New System.Drawing.Size(311, 279)
        Me.dgv_mh_leyenda.TabIndex = 0
        '
        'lbl_mh_uhcount
        '
        Me.lbl_mh_uhcount.AutoSize = True
        Me.lbl_mh_uhcount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_mh_uhcount.Location = New System.Drawing.Point(2, 290)
        Me.lbl_mh_uhcount.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mh_uhcount.Name = "lbl_mh_uhcount"
        Me.lbl_mh_uhcount.Size = New System.Drawing.Size(311, 13)
        Me.lbl_mh_uhcount.TabIndex = 1
        Me.lbl_mh_uhcount.Text = "..."
        Me.lbl_mh_uhcount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tp_mh_dhidrog
        '
        Me.tp_mh_dhidrog.Controls.Add(Me.TableLayoutPanel5)
        Me.tp_mh_dhidrog.Location = New System.Drawing.Point(4, 22)
        Me.tp_mh_dhidrog.Margin = New System.Windows.Forms.Padding(2)
        Me.tp_mh_dhidrog.Name = "tp_mh_dhidrog"
        Me.tp_mh_dhidrog.Padding = New System.Windows.Forms.Padding(2)
        Me.tp_mh_dhidrog.Size = New System.Drawing.Size(319, 307)
        Me.tp_mh_dhidrog.TabIndex = 1
        Me.tp_mh_dhidrog.Text = "Descripción Hidrogeológica"
        Me.tp_mh_dhidrog.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.tbx_mh_descriph, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.tvw_mh_descriph, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(315, 303)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'tbx_mh_descriph
        '
        Me.tbx_mh_descriph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_mh_descriph.Location = New System.Drawing.Point(2, 207)
        Me.tbx_mh_descriph.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mh_descriph.Multiline = True
        Me.tbx_mh_descriph.Name = "tbx_mh_descriph"
        Me.tbx_mh_descriph.Size = New System.Drawing.Size(311, 94)
        Me.tbx_mh_descriph.TabIndex = 0
        '
        'tvw_mh_descriph
        '
        Me.tvw_mh_descriph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvw_mh_descriph.Location = New System.Drawing.Point(2, 2)
        Me.tvw_mh_descriph.Margin = New System.Windows.Forms.Padding(2)
        Me.tvw_mh_descriph.Name = "tvw_mh_descriph"
        Me.tvw_mh_descriph.Size = New System.Drawing.Size(311, 201)
        Me.tvw_mh_descriph.TabIndex = 1
        '
        'UserControl_ComboBoxDataframes1
        '
        Me.UserControl_ComboBoxDataframes1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_ComboBoxDataframes1.Location = New System.Drawing.Point(2, 339)
        Me.UserControl_ComboBoxDataframes1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UserControl_ComboBoxDataframes1.Name = "UserControl_ComboBoxDataframes1"
        Me.UserControl_ComboBoxDataframes1.Size = New System.Drawing.Size(327, 41)
        Me.UserControl_ComboBoxDataframes1.TabIndex = 1
        '
        'tp_mh_mapa_ubicacion
        '
        Me.tp_mh_mapa_ubicacion.Controls.Add(Me.TableLayoutPanel8)
        Me.tp_mh_mapa_ubicacion.ImageIndex = 9
        Me.tp_mh_mapa_ubicacion.Location = New System.Drawing.Point(4, 23)
        Me.tp_mh_mapa_ubicacion.Name = "tp_mh_mapa_ubicacion"
        Me.tp_mh_mapa_ubicacion.Size = New System.Drawing.Size(335, 414)
        Me.tp_mh_mapa_ubicacion.TabIndex = 3
        Me.tp_mh_mapa_ubicacion.Text = "Mapa de ubicación"
        Me.tp_mh_mapa_ubicacion.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.UserControl_ComboBoxDataframes2, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.rbt_mhg_nacional, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.rbt_mhg_cuenca, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.btn_mgh_generar_mu, 0, 2)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 3
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(335, 414)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'UserControl_ComboBoxDataframes2
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.UserControl_ComboBoxDataframes2, 2)
        Me.UserControl_ComboBoxDataframes2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UserControl_ComboBoxDataframes2.Location = New System.Drawing.Point(2, 2)
        Me.UserControl_ComboBoxDataframes2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UserControl_ComboBoxDataframes2.Name = "UserControl_ComboBoxDataframes2"
        Me.UserControl_ComboBoxDataframes2.Size = New System.Drawing.Size(331, 41)
        Me.UserControl_ComboBoxDataframes2.TabIndex = 0
        '
        'rbt_mhg_nacional
        '
        Me.rbt_mhg_nacional.AutoSize = True
        Me.rbt_mhg_nacional.Checked = True
        Me.rbt_mhg_nacional.Location = New System.Drawing.Point(3, 48)
        Me.rbt_mhg_nacional.Name = "rbt_mhg_nacional"
        Me.rbt_mhg_nacional.Size = New System.Drawing.Size(67, 17)
        Me.rbt_mhg_nacional.TabIndex = 1
        Me.rbt_mhg_nacional.TabStop = True
        Me.rbt_mhg_nacional.Text = "Nacional"
        Me.rbt_mhg_nacional.UseVisualStyleBackColor = True
        '
        'rbt_mhg_cuenca
        '
        Me.rbt_mhg_cuenca.AutoSize = True
        Me.rbt_mhg_cuenca.Location = New System.Drawing.Point(115, 48)
        Me.rbt_mhg_cuenca.Name = "rbt_mhg_cuenca"
        Me.rbt_mhg_cuenca.Size = New System.Drawing.Size(73, 17)
        Me.rbt_mhg_cuenca.TabIndex = 2
        Me.rbt_mhg_cuenca.Text = "Cuenca(s)"
        Me.rbt_mhg_cuenca.UseVisualStyleBackColor = True
        '
        'btn_mgh_generar_mu
        '
        Me.TableLayoutPanel8.SetColumnSpan(Me.btn_mgh_generar_mu, 2)
        Me.btn_mgh_generar_mu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mgh_generar_mu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mgh_generar_mu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mgh_generar_mu.ImageIndex = 9
        Me.btn_mgh_generar_mu.ImageList = Me.img_list_mh
        Me.btn_mgh_generar_mu.Location = New System.Drawing.Point(3, 389)
        Me.btn_mgh_generar_mu.Name = "btn_mgh_generar_mu"
        Me.btn_mgh_generar_mu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btn_mgh_generar_mu.Size = New System.Drawing.Size(329, 22)
        Me.btn_mgh_generar_mu.TabIndex = 3
        Me.btn_mgh_generar_mu.Text = "Generar mapa de ubicación"
        Me.btn_mgh_generar_mu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mgh_generar_mu.UseVisualStyleBackColor = True
        '
        'Form_mapa_hidrogeologico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(347, 570)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_mapa_hidrogeologico"
        Me.Text = "Form_mapa_hidrogeologico"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tc_mh_tools.ResumeLayout(False)
        Me.tp_mh_capas.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.tp_mh_rotulo.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.nud_mh_numero, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_mh_leyenda.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.tc_mh_leyenda_tools.ResumeLayout(False)
        Me.tp_mh_classif.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.dgv_mh_leyenda, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_mh_dhidrog.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.tp_mh_mapa_ubicacion.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbx_mh_cuencas As System.Windows.Forms.ComboBox
    Friend WithEvents lbx_mh_cuencas As System.Windows.Forms.ListBox
    Friend WithEvents lbl_mh_nota1 As System.Windows.Forms.Label
    Friend WithEvents lbl_mh_cuenca As System.Windows.Forms.Label
    Friend WithEvents tc_mh_tools As System.Windows.Forms.TabControl
    Friend WithEvents tp_mh_rotulo As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_mh_autor As System.Windows.Forms.Label
    Friend WithEvents tp_mh_leyenda As System.Windows.Forms.TabPage
    Friend WithEvents clb_mh_autores As System.Windows.Forms.CheckedListBox
    Friend WithEvents rbt_mh_pequenio As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_mh_grande As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbx_mh_autores As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_mh_title1 As System.Windows.Forms.TextBox
    Friend WithEvents tbx_mh_title2 As System.Windows.Forms.TextBox
    Friend WithEvents btn_mh_grotulo As System.Windows.Forms.Button
    Friend WithEvents img_list_mh As System.Windows.Forms.ImageList
    Friend WithEvents nud_mh_numero As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbl_mh_numero As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_mh_leyenda As System.Windows.Forms.Button
    Friend WithEvents tc_mh_leyenda_tools As System.Windows.Forms.TabControl
    Friend WithEvents tp_mh_classif As System.Windows.Forms.TabPage
    Friend WithEvents tp_mh_dhidrog As System.Windows.Forms.TabPage
    'Friend WithEvents UserControl_ComboBoxDataframes As UserControl_ComboBoxDataframes
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_mh_leyenda As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_mh_uhcount As System.Windows.Forms.Label
    Friend WithEvents UserControl_ComboBoxDataframes1 As UserControl_ComboBoxDataframes
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_mh_descriph As System.Windows.Forms.TextBox
    Friend WithEvents tvw_mh_descriph As System.Windows.Forms.TreeView
    Friend WithEvents tp_mh_capas As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UserControl_CheckBoxAddLayers1 As UserControl_CheckBoxAddLayers
    Friend WithEvents btn_mgh_extrerdatos As System.Windows.Forms.Button
    Friend WithEvents tp_mh_mapa_ubicacion As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UserControl_ComboBoxDataframes2 As UserControl_ComboBoxDataframes
    Friend WithEvents rbt_mhg_nacional As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_mhg_cuenca As System.Windows.Forms.RadioButton
    Friend WithEvents btn_mgh_generar_mu As System.Windows.Forms.Button
End Class
