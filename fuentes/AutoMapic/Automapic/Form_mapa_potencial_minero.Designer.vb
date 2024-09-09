<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_mapa_potencial_minero
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_potencial_minero))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_mpm_nombre_mapa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_mpm_load = New System.Windows.Forms.Button()
        Me.btn_mpm_generar_mapa = New System.Windows.Forms.Button()
        Me.llbl_mpm_otras_opc = New System.Windows.Forms.LinkLabel()
        Me.pnl_mpm_otras_opc = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nud_mpm_escala = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbt_mpm_vertical = New System.Windows.Forms.RadioButton()
        Me.rbt_mpm_horizontal = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbt_mpm_a3 = New System.Windows.Forms.RadioButton()
        Me.rbt_mpm_a4 = New System.Windows.Forms.RadioButton()
        Me.gbx_mpm_informacion = New System.Windows.Forms.GroupBox()
        Me.rbt_mpm_coninfo = New System.Windows.Forms.RadioButton()
        Me.rbt_mpm_sininfo = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbx_mpm_autor = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbx_mpm_revisores = New System.Windows.Forms.ComboBox()
        Me.tbx_mpm_archivo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_mpm_reportes = New System.Windows.Forms.Button()
        Me.btn_mpm_generar_mapa_vp = New System.Windows.Forms.Button()
        Me.btn_mpm_dashboard = New System.Windows.Forms.Button()
        Me.btn_mpm_delete_map = New System.Windows.Forms.Button()
        Me.btn_mpm_reporte_general = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbx_mpm_documento = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbx_mpm_formato = New System.Windows.Forms.ComboBox()
        Me.ttp_mpm = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnl_mpm_otras_opc.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.nud_mpm_escala, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbx_mpm_informacion.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_nombre_mapa, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mpm_load, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mpm_generar_mapa, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.llbl_mpm_otras_opc, 1, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.pnl_mpm_otras_opc, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_autor, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mpm_revisores, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_archivo, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_documento, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_mpm_formato, 2, 11)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 17
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(350, 625)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'tbx_mpm_nombre_mapa
        '
        Me.tbx_mpm_nombre_mapa.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_mpm_nombre_mapa, 2)
        Me.tbx_mpm_nombre_mapa.Location = New System.Drawing.Point(122, 106)
        Me.tbx_mpm_nombre_mapa.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mpm_nombre_mapa.Name = "tbx_mpm_nombre_mapa"
        Me.tbx_mpm_nombre_mapa.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mpm_nombre_mapa.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label2, 2)
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(2, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(294, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "      Agregar área en consulta (*.shp)"
        '
        'btn_mpm_load
        '
        Me.btn_mpm_load.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_load.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_load.Location = New System.Drawing.Point(300, 22)
        Me.btn_mpm_load.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_load.Name = "btn_mpm_load"
        Me.btn_mpm_load.Size = New System.Drawing.Size(48, 20)
        Me.btn_mpm_load.TabIndex = 4
        Me.btn_mpm_load.Text = "..."
        Me.btn_mpm_load.UseVisualStyleBackColor = True
        '
        'btn_mpm_generar_mapa
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_mpm_generar_mapa, 3)
        Me.btn_mpm_generar_mapa.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_generar_mapa.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_generar_mapa.Image = Global.Automapic.My.Resources.Resources.MapRange16
        Me.btn_mpm_generar_mapa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mpm_generar_mapa.Location = New System.Drawing.Point(2, 595)
        Me.btn_mpm_generar_mapa.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_generar_mapa.Name = "btn_mpm_generar_mapa"
        Me.btn_mpm_generar_mapa.Size = New System.Drawing.Size(346, 28)
        Me.btn_mpm_generar_mapa.TabIndex = 11
        Me.btn_mpm_generar_mapa.Text = "Generar mapa"
        Me.btn_mpm_generar_mapa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mpm_generar_mapa.UseVisualStyleBackColor = True
        '
        'llbl_mpm_otras_opc
        '
        Me.llbl_mpm_otras_opc.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.llbl_mpm_otras_opc, 2)
        Me.llbl_mpm_otras_opc.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.llbl_mpm_otras_opc.Location = New System.Drawing.Point(122, 195)
        Me.llbl_mpm_otras_opc.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.llbl_mpm_otras_opc.Name = "llbl_mpm_otras_opc"
        Me.llbl_mpm_otras_opc.Size = New System.Drawing.Size(226, 13)
        Me.llbl_mpm_otras_opc.TabIndex = 12
        Me.llbl_mpm_otras_opc.TabStop = True
        Me.llbl_mpm_otras_opc.Text = "Más opciones..."
        Me.llbl_mpm_otras_opc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_mpm_otras_opc
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.pnl_mpm_otras_opc, 3)
        Me.pnl_mpm_otras_opc.Controls.Add(Me.TableLayoutPanel2)
        Me.pnl_mpm_otras_opc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_mpm_otras_opc.Location = New System.Drawing.Point(2, 210)
        Me.pnl_mpm_otras_opc.Margin = New System.Windows.Forms.Padding(2)
        Me.pnl_mpm_otras_opc.Name = "pnl_mpm_otras_opc"
        Me.pnl_mpm_otras_opc.Size = New System.Drawing.Size(346, 349)
        Me.pnl_mpm_otras_opc.TabIndex = 13
        Me.pnl_mpm_otras_opc.Visible = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.nud_mpm_escala, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 1, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.gbx_mpm_informacion, 1, 10)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 13
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(346, 349)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label5, 2)
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(6, 7)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(334, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Definir escala de representación (opcional)"
        '
        'nud_mpm_escala
        '
        Me.nud_mpm_escala.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.SetColumnSpan(Me.nud_mpm_escala, 2)
        Me.nud_mpm_escala.Increment = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nud_mpm_escala.Location = New System.Drawing.Point(6, 22)
        Me.nud_mpm_escala.Margin = New System.Windows.Forms.Padding(2)
        Me.nud_mpm_escala.Maximum = New Decimal(New Integer() {20000000, 0, 0, 0})
        Me.nud_mpm_escala.Name = "nud_mpm_escala"
        Me.nud_mpm_escala.Size = New System.Drawing.Size(334, 20)
        Me.nud_mpm_escala.TabIndex = 3
        '
        'GroupBox1
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Controls.Add(Me.rbt_mpm_vertical)
        Me.GroupBox1.Controls.Add(Me.rbt_mpm_horizontal)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(6, 50)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.SetRowSpan(Me.GroupBox1, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(334, 44)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Definir orientación del mapa (opcional)"
        '
        'rbt_mpm_vertical
        '
        Me.rbt_mpm_vertical.AutoSize = True
        Me.rbt_mpm_vertical.Location = New System.Drawing.Point(98, 22)
        Me.rbt_mpm_vertical.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_vertical.Name = "rbt_mpm_vertical"
        Me.rbt_mpm_vertical.Size = New System.Drawing.Size(60, 17)
        Me.rbt_mpm_vertical.TabIndex = 1
        Me.rbt_mpm_vertical.TabStop = True
        Me.rbt_mpm_vertical.Text = "Vertical"
        Me.rbt_mpm_vertical.UseVisualStyleBackColor = True
        '
        'rbt_mpm_horizontal
        '
        Me.rbt_mpm_horizontal.AutoSize = True
        Me.rbt_mpm_horizontal.Location = New System.Drawing.Point(4, 22)
        Me.rbt_mpm_horizontal.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_horizontal.Name = "rbt_mpm_horizontal"
        Me.rbt_mpm_horizontal.Size = New System.Drawing.Size(72, 17)
        Me.rbt_mpm_horizontal.TabIndex = 0
        Me.rbt_mpm_horizontal.TabStop = True
        Me.rbt_mpm_horizontal.Text = "Horizontal"
        Me.rbt_mpm_horizontal.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.GroupBox2, 2)
        Me.GroupBox2.Controls.Add(Me.rbt_mpm_a3)
        Me.GroupBox2.Controls.Add(Me.rbt_mpm_a4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(6, 102)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.SetRowSpan(Me.GroupBox2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(334, 44)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Definir tamaño de hoja (opcional)"
        '
        'rbt_mpm_a3
        '
        Me.rbt_mpm_a3.AutoSize = True
        Me.rbt_mpm_a3.Location = New System.Drawing.Point(98, 22)
        Me.rbt_mpm_a3.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_a3.Name = "rbt_mpm_a3"
        Me.rbt_mpm_a3.Size = New System.Drawing.Size(38, 17)
        Me.rbt_mpm_a3.TabIndex = 1
        Me.rbt_mpm_a3.TabStop = True
        Me.rbt_mpm_a3.Text = "A3"
        Me.rbt_mpm_a3.UseVisualStyleBackColor = True
        '
        'rbt_mpm_a4
        '
        Me.rbt_mpm_a4.AutoSize = True
        Me.rbt_mpm_a4.Location = New System.Drawing.Point(4, 22)
        Me.rbt_mpm_a4.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_a4.Name = "rbt_mpm_a4"
        Me.rbt_mpm_a4.Size = New System.Drawing.Size(38, 17)
        Me.rbt_mpm_a4.TabIndex = 0
        Me.rbt_mpm_a4.TabStop = True
        Me.rbt_mpm_a4.Text = "A4"
        Me.rbt_mpm_a4.UseVisualStyleBackColor = True
        '
        'gbx_mpm_informacion
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.gbx_mpm_informacion, 2)
        Me.gbx_mpm_informacion.Controls.Add(Me.rbt_mpm_coninfo)
        Me.gbx_mpm_informacion.Controls.Add(Me.rbt_mpm_sininfo)
        Me.gbx_mpm_informacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbx_mpm_informacion.Location = New System.Drawing.Point(6, 154)
        Me.gbx_mpm_informacion.Margin = New System.Windows.Forms.Padding(2)
        Me.gbx_mpm_informacion.Name = "gbx_mpm_informacion"
        Me.gbx_mpm_informacion.Padding = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.SetRowSpan(Me.gbx_mpm_informacion, 2)
        Me.gbx_mpm_informacion.Size = New System.Drawing.Size(334, 44)
        Me.gbx_mpm_informacion.TabIndex = 10
        Me.gbx_mpm_informacion.TabStop = False
        Me.gbx_mpm_informacion.Text = "Mapa con información"
        '
        'rbt_mpm_coninfo
        '
        Me.rbt_mpm_coninfo.AutoSize = True
        Me.rbt_mpm_coninfo.Checked = True
        Me.rbt_mpm_coninfo.Location = New System.Drawing.Point(4, 17)
        Me.rbt_mpm_coninfo.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_coninfo.Name = "rbt_mpm_coninfo"
        Me.rbt_mpm_coninfo.Size = New System.Drawing.Size(34, 17)
        Me.rbt_mpm_coninfo.TabIndex = 1
        Me.rbt_mpm_coninfo.TabStop = True
        Me.rbt_mpm_coninfo.Text = "Si"
        Me.rbt_mpm_coninfo.UseVisualStyleBackColor = True
        '
        'rbt_mpm_sininfo
        '
        Me.rbt_mpm_sininfo.AutoSize = True
        Me.rbt_mpm_sininfo.Location = New System.Drawing.Point(98, 17)
        Me.rbt_mpm_sininfo.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_mpm_sininfo.Name = "rbt_mpm_sininfo"
        Me.rbt_mpm_sininfo.Size = New System.Drawing.Size(39, 17)
        Me.rbt_mpm_sininfo.TabIndex = 0
        Me.rbt_mpm_sininfo.Text = "No"
        Me.rbt_mpm_sininfo.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Image = CType(resources.GetObject("Label8.Image"), System.Drawing.Image)
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.Location = New System.Drawing.Point(2, 53)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(116, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "      Autor"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Image = CType(resources.GetObject("Label9.Image"), System.Drawing.Image)
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.Location = New System.Drawing.Point(2, 109)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(116, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "      Nombre de mapa"
        '
        'tbx_mpm_autor
        '
        Me.tbx_mpm_autor.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_mpm_autor, 2)
        Me.tbx_mpm_autor.Location = New System.Drawing.Point(122, 50)
        Me.tbx_mpm_autor.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mpm_autor.Name = "tbx_mpm_autor"
        Me.tbx_mpm_autor.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mpm_autor.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Image = Global.Automapic.My.Resources.Resources.GenericAsteriskRed16
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Location = New System.Drawing.Point(2, 81)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "      Revisado por"
        '
        'cbx_mpm_revisores
        '
        Me.cbx_mpm_revisores.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_mpm_revisores, 2)
        Me.cbx_mpm_revisores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mpm_revisores.FormattingEnabled = True
        Me.cbx_mpm_revisores.Items.AddRange(New Object() {"Omar Becerra", "Kadi Flores", "Bruno Barreto"})
        Me.cbx_mpm_revisores.Location = New System.Drawing.Point(122, 78)
        Me.cbx_mpm_revisores.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mpm_revisores.Name = "cbx_mpm_revisores"
        Me.cbx_mpm_revisores.Size = New System.Drawing.Size(226, 21)
        Me.cbx_mpm_revisores.TabIndex = 19
        '
        'tbx_mpm_archivo
        '
        Me.tbx_mpm_archivo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_mpm_archivo, 2)
        Me.tbx_mpm_archivo.Enabled = False
        Me.tbx_mpm_archivo.Location = New System.Drawing.Point(2, 22)
        Me.tbx_mpm_archivo.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mpm_archivo.Name = "tbx_mpm_archivo"
        Me.tbx_mpm_archivo.Size = New System.Drawing.Size(294, 20)
        Me.tbx_mpm_archivo.TabIndex = 20
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel3, 3)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mpm_reportes, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mpm_generar_mapa_vp, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mpm_dashboard, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mpm_delete_map, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_mpm_reporte_general, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 563)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(346, 28)
        Me.TableLayoutPanel3.TabIndex = 21
        '
        'btn_mpm_reportes
        '
        Me.btn_mpm_reportes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_reportes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_reportes.Image = Global.Automapic.My.Resources.Resources.TableExcel16
        Me.btn_mpm_reportes.Location = New System.Drawing.Point(2, 2)
        Me.btn_mpm_reportes.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_reportes.Name = "btn_mpm_reportes"
        Me.btn_mpm_reportes.Size = New System.Drawing.Size(26, 24)
        Me.btn_mpm_reportes.TabIndex = 0
        Me.ttp_mpm.SetToolTip(Me.btn_mpm_reportes, "Ver reportes")
        Me.btn_mpm_reportes.UseVisualStyleBackColor = True
        '
        'btn_mpm_generar_mapa_vp
        '
        Me.btn_mpm_generar_mapa_vp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_generar_mapa_vp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_mpm_generar_mapa_vp.Image = Global.Automapic.My.Resources.Resources.ArcMap_MXT_File16
        Me.btn_mpm_generar_mapa_vp.Location = New System.Drawing.Point(240, 2)
        Me.btn_mpm_generar_mapa_vp.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_generar_mapa_vp.Name = "btn_mpm_generar_mapa_vp"
        Me.btn_mpm_generar_mapa_vp.Size = New System.Drawing.Size(104, 24)
        Me.btn_mpm_generar_mapa_vp.TabIndex = 1
        Me.btn_mpm_generar_mapa_vp.Text = "Vista previa"
        Me.btn_mpm_generar_mapa_vp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mpm_generar_mapa_vp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mpm_generar_mapa_vp.UseVisualStyleBackColor = True
        '
        'btn_mpm_dashboard
        '
        Me.btn_mpm_dashboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_dashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_dashboard.Image = CType(resources.GetObject("btn_mpm_dashboard.Image"), System.Drawing.Image)
        Me.btn_mpm_dashboard.Location = New System.Drawing.Point(92, 2)
        Me.btn_mpm_dashboard.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_dashboard.Name = "btn_mpm_dashboard"
        Me.btn_mpm_dashboard.Size = New System.Drawing.Size(26, 24)
        Me.btn_mpm_dashboard.TabIndex = 2
        Me.ttp_mpm.SetToolTip(Me.btn_mpm_dashboard, "Ver tablero de control")
        Me.btn_mpm_dashboard.UseVisualStyleBackColor = True
        '
        'btn_mpm_delete_map
        '
        Me.btn_mpm_delete_map.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_delete_map.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_delete_map.Image = Global.Automapic.My.Resources.Resources.GenericGlobeLargeWithDeleteRed_B_16
        Me.btn_mpm_delete_map.Location = New System.Drawing.Point(62, 2)
        Me.btn_mpm_delete_map.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_delete_map.Name = "btn_mpm_delete_map"
        Me.btn_mpm_delete_map.Size = New System.Drawing.Size(26, 24)
        Me.btn_mpm_delete_map.TabIndex = 1
        Me.ttp_mpm.SetToolTip(Me.btn_mpm_delete_map, "Eliminar mapa por código")
        Me.btn_mpm_delete_map.UseVisualStyleBackColor = True
        '
        'btn_mpm_reporte_general
        '
        Me.btn_mpm_reporte_general.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_reporte_general.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_reporte_general.Image = CType(resources.GetObject("btn_mpm_reporte_general.Image"), System.Drawing.Image)
        Me.btn_mpm_reporte_general.Location = New System.Drawing.Point(32, 2)
        Me.btn_mpm_reporte_general.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_mpm_reporte_general.Name = "btn_mpm_reporte_general"
        Me.btn_mpm_reporte_general.Size = New System.Drawing.Size(26, 24)
        Me.btn_mpm_reporte_general.TabIndex = 3
        Me.ttp_mpm.SetToolTip(Me.btn_mpm_reporte_general, "Reporte General")
        Me.btn_mpm_reporte_general.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(2, 137)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "      Documento"
        '
        'tbx_mpm_documento
        '
        Me.tbx_mpm_documento.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_mpm_documento, 2)
        Me.tbx_mpm_documento.Location = New System.Drawing.Point(122, 134)
        Me.tbx_mpm_documento.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_mpm_documento.Name = "tbx_mpm_documento"
        Me.tbx_mpm_documento.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mpm_documento.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(2, 165)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(294, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "      Especifique el formato original de entrada"
        '
        'cbx_mpm_formato
        '
        Me.cbx_mpm_formato.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_mpm_formato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_mpm_formato.FormattingEnabled = True
        Me.cbx_mpm_formato.Items.AddRange(New Object() {"shp", "xls", "dxf", "dwg", "kml", "kmz"})
        Me.cbx_mpm_formato.Location = New System.Drawing.Point(300, 162)
        Me.cbx_mpm_formato.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_mpm_formato.Name = "cbx_mpm_formato"
        Me.cbx_mpm_formato.Size = New System.Drawing.Size(48, 21)
        Me.cbx_mpm_formato.TabIndex = 8
        '
        'ttp_mpm
        '
        Me.ttp_mpm.BackColor = System.Drawing.Color.White
        Me.ttp_mpm.IsBalloon = True
        Me.ttp_mpm.ShowAlways = True
        Me.ttp_mpm.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ttp_mpm.ToolTipTitle = "Información"
        '
        'Form_mapa_potencial_minero
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(350, 625)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_mapa_potencial_minero"
        Me.Text = "Form_mapa_potencial_minero"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pnl_mpm_otras_opc.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.nud_mpm_escala, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gbx_mpm_informacion.ResumeLayout(False)
        Me.gbx_mpm_informacion.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btn_mpm_load As System.Windows.Forms.Button
    Friend WithEvents cbx_mpm_formato As System.Windows.Forms.ComboBox
    Friend WithEvents btn_mpm_generar_mapa As System.Windows.Forms.Button
    Friend WithEvents llbl_mpm_otras_opc As System.Windows.Forms.LinkLabel
    Friend WithEvents pnl_mpm_otras_opc As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nud_mpm_escala As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbx_mpm_autor As System.Windows.Forms.TextBox
    Friend WithEvents tbx_mpm_nombre_mapa As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbx_mpm_revisores As System.Windows.Forms.ComboBox
    Friend WithEvents tbx_mpm_archivo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_mpm_vertical As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_mpm_horizontal As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_mpm_a3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_mpm_a4 As System.Windows.Forms.RadioButton
    Friend WithEvents btn_mpm_generar_mapa_vp As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_mpm_reportes As System.Windows.Forms.Button
    Friend WithEvents btn_mpm_delete_map As System.Windows.Forms.Button
    Friend WithEvents ttp_mpm As System.Windows.Forms.ToolTip
    Friend WithEvents btn_mpm_dashboard As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbx_mpm_documento As System.Windows.Forms.TextBox
    Friend WithEvents gbx_mpm_informacion As System.Windows.Forms.GroupBox
    Friend WithEvents rbt_mpm_coninfo As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_mpm_sininfo As System.Windows.Forms.RadioButton
    Friend WithEvents btn_mpm_reporte_general As System.Windows.Forms.Button
End Class
