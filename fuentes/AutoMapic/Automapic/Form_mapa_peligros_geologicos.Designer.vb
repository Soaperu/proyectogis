<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_mapa_peligros_geologicos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_peligros_geologicos))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_loadshp = New System.Windows.Forms.Label()
        Me.tbx_pathshp = New System.Windows.Forms.TextBox()
        Me.btn_loadshp = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbx_title_pg = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbx_autor_pg = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_escala_pg = New System.Windows.Forms.TextBox()
        Me.tbx_detalle_pg = New System.Windows.Forms.TextBox()
        Me.tbx_detalle = New System.Windows.Forms.Label()
        Me.rbt_pg = New System.Windows.Forms.RadioButton()
        Me.rbt_zc = New System.Windows.Forms.RadioButton()
        Me.rbt_smm = New System.Windows.Forms.RadioButton()
        Me.rbt_sief = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbx_numero_pg = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_generar_mapa_pg = New System.Windows.Forms.Button()
        Me.btn_draw = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbx_xmin = New System.Windows.Forms.TextBox()
        Me.tbx_ymin = New System.Windows.Forms.TextBox()
        Me.tbx_xmax = New System.Windows.Forms.TextBox()
        Me.tbx_ymax = New System.Windows.Forms.TextBox()
        Me.lbl_xmin = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.rbt_pg_gm = New System.Windows.Forms.RadioButton()
        Me.rbt_pg_cp = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_blank_extent = New System.Windows.Forms.Button()
        Me.btn_pg_export = New System.Windows.Forms.Button()
        Me.btn_pg_reportes = New System.Windows.Forms.Button()
        Me.btn_pg_registro = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.tbx_numero_pg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_loadshp, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pathshp, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_loadshp, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_title_pg, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_autor_pg, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_escala_pg, 0, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_detalle_pg, 0, 19)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_detalle, 0, 18)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_pg, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_zc, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_smm, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_sief, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_numero_pg, 1, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 1, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_generar_mapa_pg, 0, 23)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_draw, 1, 20)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 20)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_pg_gm, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.rbt_pg_cp, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 21)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.ForeColor = System.Drawing.Color.Black
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 25
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(390, 683)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lbl_loadshp
        '
        Me.lbl_loadshp.AutoSize = True
        Me.lbl_loadshp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_loadshp.Location = New System.Drawing.Point(2, 7)
        Me.lbl_loadshp.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_loadshp.Name = "lbl_loadshp"
        Me.lbl_loadshp.Size = New System.Drawing.Size(326, 13)
        Me.lbl_loadshp.TabIndex = 0
        Me.lbl_loadshp.Text = "Cargar shapefile"
        '
        'tbx_pathshp
        '
        Me.tbx_pathshp.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_pathshp.Enabled = False
        Me.tbx_pathshp.Location = New System.Drawing.Point(2, 22)
        Me.tbx_pathshp.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_pathshp.Name = "tbx_pathshp"
        Me.tbx_pathshp.Size = New System.Drawing.Size(326, 20)
        Me.tbx_pathshp.TabIndex = 1
        '
        'btn_loadshp
        '
        Me.btn_loadshp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_loadshp.Location = New System.Drawing.Point(332, 22)
        Me.btn_loadshp.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_loadshp.Name = "btn_loadshp"
        Me.btn_loadshp.Size = New System.Drawing.Size(56, 20)
        Me.btn_loadshp.TabIndex = 2
        Me.btn_loadshp.Text = "..."
        Me.btn_loadshp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btn_loadshp.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(2, 191)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(326, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nombre del sector o zona evaluada (opcional)"
        '
        'tbx_title_pg
        '
        Me.tbx_title_pg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_title_pg, 2)
        Me.tbx_title_pg.Location = New System.Drawing.Point(2, 206)
        Me.tbx_title_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_title_pg.Name = "tbx_title_pg"
        Me.tbx_title_pg.Size = New System.Drawing.Size(386, 20)
        Me.tbx_title_pg.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(2, 231)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(326, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Autor (opcional)"
        '
        'tbx_autor_pg
        '
        Me.tbx_autor_pg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_autor_pg, 2)
        Me.tbx_autor_pg.Location = New System.Drawing.Point(2, 246)
        Me.tbx_autor_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_autor_pg.Name = "tbx_autor_pg"
        Me.tbx_autor_pg.Size = New System.Drawing.Size(386, 20)
        Me.tbx_autor_pg.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(2, 271)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(326, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Escala (opcional)"
        '
        'tbx_escala_pg
        '
        Me.tbx_escala_pg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_escala_pg.Location = New System.Drawing.Point(2, 286)
        Me.tbx_escala_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_escala_pg.Name = "tbx_escala_pg"
        Me.tbx_escala_pg.Size = New System.Drawing.Size(326, 20)
        Me.tbx_escala_pg.TabIndex = 8
        '
        'tbx_detalle_pg
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_detalle_pg, 2)
        Me.tbx_detalle_pg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_detalle_pg.Enabled = False
        Me.tbx_detalle_pg.Location = New System.Drawing.Point(2, 326)
        Me.tbx_detalle_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_detalle_pg.Multiline = True
        Me.tbx_detalle_pg.Name = "tbx_detalle_pg"
        Me.tbx_detalle_pg.Size = New System.Drawing.Size(386, 77)
        Me.tbx_detalle_pg.TabIndex = 9
        '
        'tbx_detalle
        '
        Me.tbx_detalle.AutoSize = True
        Me.tbx_detalle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tbx_detalle.Location = New System.Drawing.Point(2, 311)
        Me.tbx_detalle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.tbx_detalle.Name = "tbx_detalle"
        Me.tbx_detalle.Size = New System.Drawing.Size(326, 13)
        Me.tbx_detalle.TabIndex = 10
        Me.tbx_detalle.Text = "Detalle (opcional)"
        '
        'rbt_pg
        '
        Me.rbt_pg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbt_pg.AutoSize = True
        Me.rbt_pg.Location = New System.Drawing.Point(2, 66)
        Me.rbt_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_pg.Name = "rbt_pg"
        Me.rbt_pg.Size = New System.Drawing.Size(326, 16)
        Me.rbt_pg.TabIndex = 11
        Me.rbt_pg.TabStop = True
        Me.rbt_pg.Text = "Peligros geológicos"
        Me.rbt_pg.UseVisualStyleBackColor = True
        '
        'rbt_zc
        '
        Me.rbt_zc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbt_zc.AutoSize = True
        Me.rbt_zc.Location = New System.Drawing.Point(2, 86)
        Me.rbt_zc.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_zc.Name = "rbt_zc"
        Me.rbt_zc.Size = New System.Drawing.Size(326, 16)
        Me.rbt_zc.TabIndex = 12
        Me.rbt_zc.TabStop = True
        Me.rbt_zc.Text = "Zonas críticas"
        Me.rbt_zc.UseVisualStyleBackColor = True
        '
        'rbt_smm
        '
        Me.rbt_smm.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbt_smm.AutoSize = True
        Me.rbt_smm.Location = New System.Drawing.Point(2, 106)
        Me.rbt_smm.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_smm.Name = "rbt_smm"
        Me.rbt_smm.Size = New System.Drawing.Size(326, 16)
        Me.rbt_smm.TabIndex = 13
        Me.rbt_smm.TabStop = True
        Me.rbt_smm.Text = "Susceptibilidad por movimientos en masa"
        Me.rbt_smm.UseVisualStyleBackColor = True
        '
        'rbt_sief
        '
        Me.rbt_sief.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbt_sief.AutoSize = True
        Me.rbt_sief.Location = New System.Drawing.Point(2, 126)
        Me.rbt_sief.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_sief.Name = "rbt_sief"
        Me.rbt_sief.Size = New System.Drawing.Size(326, 16)
        Me.rbt_sief.TabIndex = 14
        Me.rbt_sief.TabStop = True
        Me.rbt_sief.Text = "Susceptibilidad por inundación y erosión"
        Me.rbt_sief.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(2, 51)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(326, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Seleccione tipo de mapa"
        '
        'tbx_numero_pg
        '
        Me.tbx_numero_pg.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_numero_pg.Location = New System.Drawing.Point(332, 286)
        Me.tbx_numero_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_numero_pg.Name = "tbx_numero_pg"
        Me.tbx_numero_pg.Size = New System.Drawing.Size(56, 20)
        Me.tbx_numero_pg.TabIndex = 16
        Me.tbx_numero_pg.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(332, 271)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Nro (opc)"
        '
        'btn_generar_mapa_pg
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_generar_mapa_pg, 2)
        Me.btn_generar_mapa_pg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_generar_mapa_pg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_generar_mapa_pg.Enabled = False
        Me.btn_generar_mapa_pg.Image = Global.Automapic.My.Resources.Resources.MapRange16
        Me.btn_generar_mapa_pg.Location = New System.Drawing.Point(2, 653)
        Me.btn_generar_mapa_pg.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_generar_mapa_pg.Name = "btn_generar_mapa_pg"
        Me.btn_generar_mapa_pg.Size = New System.Drawing.Size(386, 24)
        Me.btn_generar_mapa_pg.TabIndex = 19
        Me.btn_generar_mapa_pg.Text = "Generar mapa"
        Me.btn_generar_mapa_pg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_generar_mapa_pg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_generar_mapa_pg.UseVisualStyleBackColor = True
        '
        'btn_draw
        '
        Me.btn_draw.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_draw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_draw.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btn_draw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_draw.Image = Global.Automapic.My.Resources.Resources.EditingCopyFeaturesTool32
        Me.btn_draw.Location = New System.Drawing.Point(332, 407)
        Me.btn_draw.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_draw.Name = "btn_draw"
        Me.btn_draw.Size = New System.Drawing.Size(56, 41)
        Me.btn_draw.TabIndex = 20
        Me.btn_draw.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_xmin, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_ymin, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_xmax, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.tbx_ymax, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_xmin, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label9, 3, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 407)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(326, 41)
        Me.TableLayoutPanel2.TabIndex = 21
        '
        'tbx_xmin
        '
        Me.tbx_xmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_xmin.Enabled = False
        Me.tbx_xmin.Location = New System.Drawing.Point(2, 18)
        Me.tbx_xmin.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_xmin.Name = "tbx_xmin"
        Me.tbx_xmin.Size = New System.Drawing.Size(77, 20)
        Me.tbx_xmin.TabIndex = 0
        Me.tbx_xmin.Text = "0"
        '
        'tbx_ymin
        '
        Me.tbx_ymin.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_ymin.Enabled = False
        Me.tbx_ymin.Location = New System.Drawing.Point(83, 18)
        Me.tbx_ymin.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_ymin.Name = "tbx_ymin"
        Me.tbx_ymin.Size = New System.Drawing.Size(77, 20)
        Me.tbx_ymin.TabIndex = 1
        Me.tbx_ymin.Text = "0"
        '
        'tbx_xmax
        '
        Me.tbx_xmax.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_xmax.Enabled = False
        Me.tbx_xmax.Location = New System.Drawing.Point(164, 18)
        Me.tbx_xmax.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_xmax.Name = "tbx_xmax"
        Me.tbx_xmax.Size = New System.Drawing.Size(77, 20)
        Me.tbx_xmax.TabIndex = 2
        Me.tbx_xmax.Text = "0"
        '
        'tbx_ymax
        '
        Me.tbx_ymax.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_ymax.Enabled = False
        Me.tbx_ymax.Location = New System.Drawing.Point(245, 18)
        Me.tbx_ymax.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_ymax.Name = "tbx_ymax"
        Me.tbx_ymax.Size = New System.Drawing.Size(79, 20)
        Me.tbx_ymax.TabIndex = 3
        Me.tbx_ymax.Text = "0"
        '
        'lbl_xmin
        '
        Me.lbl_xmin.AutoSize = True
        Me.lbl_xmin.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_xmin.Location = New System.Drawing.Point(2, 3)
        Me.lbl_xmin.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_xmin.Name = "lbl_xmin"
        Me.lbl_xmin.Size = New System.Drawing.Size(77, 13)
        Me.lbl_xmin.TabIndex = 4
        Me.lbl_xmin.Text = "xmin"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(83, 3)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "ymin"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(164, 3)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "xmax"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(245, 3)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "ymax"
        '
        'rbt_pg_gm
        '
        Me.rbt_pg_gm.AutoSize = True
        Me.rbt_pg_gm.Location = New System.Drawing.Point(2, 146)
        Me.rbt_pg_gm.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_pg_gm.Name = "rbt_pg_gm"
        Me.rbt_pg_gm.Size = New System.Drawing.Size(95, 16)
        Me.rbt_pg_gm.TabIndex = 23
        Me.rbt_pg_gm.TabStop = True
        Me.rbt_pg_gm.Text = "Geomorfología"
        Me.rbt_pg_gm.UseVisualStyleBackColor = True
        '
        'rbt_pg_cp
        '
        Me.rbt_pg_cp.AutoSize = True
        Me.rbt_pg_cp.Location = New System.Drawing.Point(2, 166)
        Me.rbt_pg_cp.Margin = New System.Windows.Forms.Padding(2)
        Me.rbt_pg_cp.Name = "rbt_pg_cp"
        Me.rbt_pg_cp.Size = New System.Drawing.Size(186, 16)
        Me.rbt_pg_cp.TabIndex = 24
        Me.rbt_pg_cp.TabStop = True
        Me.rbt_pg_cp.Text = "Cartografía de peligros geologicos"
        Me.rbt_pg_cp.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel3, 2)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btn_blank_extent, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_pg_export, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_pg_reportes, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_pg_registro, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 453)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(384, 34)
        Me.TableLayoutPanel3.TabIndex = 26
        '
        'btn_blank_extent
        '
        Me.btn_blank_extent.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_blank_extent.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_blank_extent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_blank_extent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_blank_extent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_blank_extent.Image = CType(resources.GetObject("btn_blank_extent.Image"), System.Drawing.Image)
        Me.btn_blank_extent.Location = New System.Drawing.Point(2, 3)
        Me.btn_blank_extent.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_blank_extent.Name = "btn_blank_extent"
        Me.btn_blank_extent.Size = New System.Drawing.Size(36, 28)
        Me.btn_blank_extent.TabIndex = 22
        Me.btn_blank_extent.UseVisualStyleBackColor = False
        '
        'btn_pg_export
        '
        Me.btn_pg_export.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_pg_export.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btn_pg_export.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_pg_export.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btn_pg_export.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_pg_export.Image = CType(resources.GetObject("btn_pg_export.Image"), System.Drawing.Image)
        Me.btn_pg_export.Location = New System.Drawing.Point(42, 3)
        Me.btn_pg_export.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_pg_export.Name = "btn_pg_export"
        Me.btn_pg_export.Size = New System.Drawing.Size(36, 28)
        Me.btn_pg_export.TabIndex = 18
        Me.btn_pg_export.UseVisualStyleBackColor = False
        '
        'btn_pg_reportes
        '
        Me.btn_pg_reportes.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_pg_reportes.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btn_pg_reportes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_pg_reportes.Enabled = False
        Me.btn_pg_reportes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btn_pg_reportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_pg_reportes.ForeColor = System.Drawing.Color.White
        Me.btn_pg_reportes.Image = Global.Automapic.My.Resources.Resources.TableExcel16
        Me.btn_pg_reportes.Location = New System.Drawing.Point(83, 3)
        Me.btn_pg_reportes.Name = "btn_pg_reportes"
        Me.btn_pg_reportes.Size = New System.Drawing.Size(146, 27)
        Me.btn_pg_reportes.TabIndex = 26
        Me.btn_pg_reportes.Text = "Reportes"
        Me.btn_pg_reportes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_pg_reportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_pg_reportes.UseVisualStyleBackColor = False
        '
        'btn_pg_registro
        '
        Me.btn_pg_registro.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_pg_registro.BackColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btn_pg_registro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_pg_registro.Enabled = False
        Me.btn_pg_registro.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btn_pg_registro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_pg_registro.ForeColor = System.Drawing.Color.White
        Me.btn_pg_registro.Image = Global.Automapic.My.Resources.Resources.SearchWindowShow16
        Me.btn_pg_registro.Location = New System.Drawing.Point(234, 3)
        Me.btn_pg_registro.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_pg_registro.Name = "btn_pg_registro"
        Me.btn_pg_registro.Size = New System.Drawing.Size(148, 28)
        Me.btn_pg_registro.TabIndex = 25
        Me.btn_pg_registro.Text = "Registrar atención"
        Me.btn_pg_registro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_pg_registro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_pg_registro.UseVisualStyleBackColor = False
        '
        'Form_mapa_peligros_geologicos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(0, 600)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(390, 683)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_mapa_peligros_geologicos"
        Me.Text = "Form_mapa_peligros_geologicos"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.tbx_numero_pg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_loadshp As System.Windows.Forms.Label
    Friend WithEvents tbx_pathshp As System.Windows.Forms.TextBox
    Friend WithEvents btn_loadshp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbx_title_pg As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbx_autor_pg As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_escala_pg As System.Windows.Forms.TextBox
    Friend WithEvents tbx_detalle_pg As System.Windows.Forms.TextBox
    Friend WithEvents tbx_detalle As System.Windows.Forms.Label
    Friend WithEvents rbt_pg As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_zc As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_smm As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_sief As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbx_numero_pg As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_pg_export As System.Windows.Forms.Button
    Friend WithEvents btn_draw As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_xmin As System.Windows.Forms.TextBox
    Friend WithEvents tbx_ymin As System.Windows.Forms.TextBox
    Friend WithEvents tbx_xmax As System.Windows.Forms.TextBox
    Friend WithEvents tbx_ymax As System.Windows.Forms.TextBox
    Friend WithEvents lbl_xmin As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btn_blank_extent As System.Windows.Forms.Button
    Friend WithEvents rbt_pg_gm As System.Windows.Forms.RadioButton
    Friend WithEvents rbt_pg_cp As System.Windows.Forms.RadioButton
    Friend WithEvents btn_generar_mapa_pg As System.Windows.Forms.Button
    Friend WithEvents btn_pg_registro As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_pg_reportes As System.Windows.Forms.Button
End Class
