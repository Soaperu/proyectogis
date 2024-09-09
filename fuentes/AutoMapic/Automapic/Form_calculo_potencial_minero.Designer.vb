<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_calculo_potencial_minero
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_calculo_potencial_minero))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_cpm_departamento_proy = New System.Windows.Forms.Label()
        Me.btn_cpm_open_folder = New System.Windows.Forms.Button()
        Me.btn_cpm_existente = New System.Windows.Forms.Button()
        Me.btn_cpm_nuevo = New System.Windows.Forms.Button()
        Me.btn_cpm_crear_cargar = New System.Windows.Forms.Button()
        Me.lbl_cpm_directorio = New System.Windows.Forms.Label()
        Me.tbx_cpm_directorio = New System.Windows.Forms.TextBox()
        Me.cbx_cpm_departamento_proy = New System.Windows.Forms.ComboBox()
        Me.btn_cpm_directorio = New System.Windows.Forms.Button()
        Me.lbl_cpm_titulo = New System.Windows.Forms.Label()
        Me.tct_cpm_carga_informacion = New System.Windows.Forms.TabControl()
        Me.tpg_cpm_metalico = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.iml_cpm_imagenes = New System.Windows.Forms.ImageList(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.M_01_GPO_Litologia = New System.Windows.Forms.Button()
        Me.M_02_GPL_FallaGeologica = New System.Windows.Forms.Button()
        Me.M_03_GPO_DepositoMineral = New System.Windows.Forms.Button()
        Me.M_VAR_RAS_Geoquimica = New System.Windows.Forms.Button()
        Me.M_05_GPO_SensorRemoto = New System.Windows.Forms.Button()
        Me.btn_M_01_GPO_Litologia = New System.Windows.Forms.Button()
        Me.btn_M_02_GPL_FallaGeologica = New System.Windows.Forms.Button()
        Me.btn_M_03_GPO_DepositoMineral = New System.Windows.Forms.Button()
        Me.btn_M_VAR_RAS_Geoquimica = New System.Windows.Forms.Button()
        Me.btn_M_05_GPO_SensorRemoto = New System.Windows.Forms.Button()
        Me.tpg_cpm_no_metalico = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btn_RMI_06_GPO_Litologia = New System.Windows.Forms.Button()
        Me.btn_RMI_07_GPT_Sustancias = New System.Windows.Forms.Button()
        Me.btn_RMI_09_GPO_SensorRemoto = New System.Windows.Forms.Button()
        Me.btn_RMI_10_GPL_Accesos = New System.Windows.Forms.Button()
        Me.RMI_06_GPO_Litologia = New System.Windows.Forms.Button()
        Me.RMI_07_GPT_Sustancias = New System.Windows.Forms.Button()
        Me.RMI_09_GPO_SensorRemoto = New System.Windows.Forms.Button()
        Me.RMI_10_GPL_Accesos = New System.Windows.Forms.Button()
        Me.lbl_cpm_pmm = New System.Windows.Forms.Label()
        Me.lbl_cpm_pmnm = New System.Windows.Forms.Label()
        Me.btn_cpm_calcular_potencial = New System.Windows.Forms.Button()
        Me.btn_M_RAS_PotencialMineroMetalico = New System.Windows.Forms.Button()
        Me.btn_M_RAS_PotencialMineroNoMetalico = New System.Windows.Forms.Button()
        Me.lbl_cpm_cargar_info = New System.Windows.Forms.Label()
        Me.lbl_cpm_calculo_potencial = New System.Windows.Forms.Label()
        Me.M_RAS_PotencialMineroMetalico = New System.Windows.Forms.Button()
        Me.M_RAS_PotencialMineroNoMetalico = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ttp_cpm = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.tct_cpm_carga_informacion.SuspendLayout()
        Me.tpg_cpm_metalico.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tpg_cpm_no_metalico.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 7
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_departamento_proy, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_open_folder, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_existente, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_nuevo, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_crear_cargar, 5, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_directorio, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_cpm_directorio, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_cpm_departamento_proy, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_directorio, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_titulo, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.tct_cpm_carga_informacion, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_pmm, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_pmnm, 0, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cpm_calcular_potencial, 0, 19)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_M_RAS_PotencialMineroMetalico, 6, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_M_RAS_PotencialMineroNoMetalico, 6, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_cargar_info, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_cpm_calculo_potencial, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.M_RAS_PotencialMineroMetalico, 5, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.M_RAS_PotencialMineroNoMetalico, 5, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 20
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(397, 585)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lbl_cpm_departamento_proy
        '
        Me.lbl_cpm_departamento_proy.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_departamento_proy, 7)
        Me.lbl_cpm_departamento_proy.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_departamento_proy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_departamento_proy.Location = New System.Drawing.Point(3, 108)
        Me.lbl_cpm_departamento_proy.Name = "lbl_cpm_departamento_proy"
        Me.lbl_cpm_departamento_proy.Size = New System.Drawing.Size(391, 13)
        Me.lbl_cpm_departamento_proy.TabIndex = 0
        Me.lbl_cpm_departamento_proy.Text = "Seleccionar departamento"
        '
        'btn_cpm_open_folder
        '
        Me.btn_cpm_open_folder.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(1, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_cpm_open_folder, 3)
        Me.btn_cpm_open_folder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_open_folder.Dock = System.Windows.Forms.DockStyle.Left
        Me.btn_cpm_open_folder.FlatAppearance.BorderSize = 0
        Me.btn_cpm_open_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_cpm_open_folder.ForeColor = System.Drawing.Color.White
        Me.btn_cpm_open_folder.Location = New System.Drawing.Point(3, 3)
        Me.btn_cpm_open_folder.Name = "btn_cpm_open_folder"
        Me.btn_cpm_open_folder.Size = New System.Drawing.Size(101, 24)
        Me.btn_cpm_open_folder.TabIndex = 0
        Me.btn_cpm_open_folder.Text = "Abrir directorio"
        Me.btn_cpm_open_folder.UseVisualStyleBackColor = False
        '
        'btn_cpm_existente
        '
        Me.btn_cpm_existente.BackColor = System.Drawing.Color.SteelBlue
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_cpm_existente, 2)
        Me.btn_cpm_existente.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_existente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cpm_existente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btn_cpm_existente.FlatAppearance.BorderSize = 0
        Me.btn_cpm_existente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_cpm_existente.ForeColor = System.Drawing.Color.White
        Me.btn_cpm_existente.Location = New System.Drawing.Point(320, 3)
        Me.btn_cpm_existente.Name = "btn_cpm_existente"
        Me.btn_cpm_existente.Size = New System.Drawing.Size(74, 24)
        Me.btn_cpm_existente.TabIndex = 2
        Me.btn_cpm_existente.Text = "Existente"
        Me.btn_cpm_existente.UseVisualStyleBackColor = False
        '
        'btn_cpm_nuevo
        '
        Me.btn_cpm_nuevo.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_cpm_nuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_cpm_nuevo, 2)
        Me.btn_cpm_nuevo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_nuevo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cpm_nuevo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btn_cpm_nuevo.FlatAppearance.BorderSize = 0
        Me.btn_cpm_nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_cpm_nuevo.ForeColor = System.Drawing.Color.White
        Me.btn_cpm_nuevo.Location = New System.Drawing.Point(240, 3)
        Me.btn_cpm_nuevo.Name = "btn_cpm_nuevo"
        Me.btn_cpm_nuevo.Size = New System.Drawing.Size(74, 24)
        Me.btn_cpm_nuevo.TabIndex = 1
        Me.btn_cpm_nuevo.Text = "Nuevo"
        Me.btn_cpm_nuevo.UseVisualStyleBackColor = False
        '
        'btn_cpm_crear_cargar
        '
        Me.btn_cpm_crear_cargar.BackColor = System.Drawing.Color.SteelBlue
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_cpm_crear_cargar, 2)
        Me.btn_cpm_crear_cargar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_crear_cargar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cpm_crear_cargar.FlatAppearance.BorderSize = 0
        Me.btn_cpm_crear_cargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_cpm_crear_cargar.ForeColor = System.Drawing.Color.White
        Me.btn_cpm_crear_cargar.Location = New System.Drawing.Point(320, 150)
        Me.btn_cpm_crear_cargar.Name = "btn_cpm_crear_cargar"
        Me.btn_cpm_crear_cargar.Size = New System.Drawing.Size(74, 24)
        Me.btn_cpm_crear_cargar.TabIndex = 2
        Me.btn_cpm_crear_cargar.Text = "Crear"
        Me.btn_cpm_crear_cargar.UseVisualStyleBackColor = False
        '
        'lbl_cpm_directorio
        '
        Me.lbl_cpm_directorio.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_directorio, 7)
        Me.lbl_cpm_directorio.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_directorio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_directorio.Location = New System.Drawing.Point(3, 62)
        Me.lbl_cpm_directorio.Name = "lbl_cpm_directorio"
        Me.lbl_cpm_directorio.Size = New System.Drawing.Size(391, 13)
        Me.lbl_cpm_directorio.TabIndex = 2
        Me.lbl_cpm_directorio.Text = "Seleccionar directorio de trabajo"
        '
        'tbx_cpm_directorio
        '
        Me.tbx_cpm_directorio.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_cpm_directorio, 6)
        Me.tbx_cpm_directorio.Enabled = False
        Me.tbx_cpm_directorio.Location = New System.Drawing.Point(3, 78)
        Me.tbx_cpm_directorio.Name = "tbx_cpm_directorio"
        Me.tbx_cpm_directorio.Size = New System.Drawing.Size(351, 20)
        Me.tbx_cpm_directorio.TabIndex = 1
        '
        'cbx_cpm_departamento_proy
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_cpm_departamento_proy, 7)
        Me.cbx_cpm_departamento_proy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbx_cpm_departamento_proy.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbx_cpm_departamento_proy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_cpm_departamento_proy.FormattingEnabled = True
        Me.cbx_cpm_departamento_proy.Location = New System.Drawing.Point(3, 124)
        Me.cbx_cpm_departamento_proy.Name = "cbx_cpm_departamento_proy"
        Me.cbx_cpm_departamento_proy.Size = New System.Drawing.Size(391, 21)
        Me.cbx_cpm_departamento_proy.TabIndex = 3
        '
        'btn_cpm_directorio
        '
        Me.btn_cpm_directorio.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_directorio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cpm_directorio.Location = New System.Drawing.Point(360, 78)
        Me.btn_cpm_directorio.Name = "btn_cpm_directorio"
        Me.btn_cpm_directorio.Size = New System.Drawing.Size(34, 20)
        Me.btn_cpm_directorio.TabIndex = 4
        Me.btn_cpm_directorio.Text = "..."
        Me.btn_cpm_directorio.UseVisualStyleBackColor = True
        '
        'lbl_cpm_titulo
        '
        Me.lbl_cpm_titulo.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_titulo, 7)
        Me.lbl_cpm_titulo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_titulo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_titulo.Location = New System.Drawing.Point(3, 199)
        Me.lbl_cpm_titulo.Name = "lbl_cpm_titulo"
        Me.lbl_cpm_titulo.Size = New System.Drawing.Size(391, 13)
        Me.lbl_cpm_titulo.TabIndex = 5
        Me.lbl_cpm_titulo.Text = "CÁLCULO DEL PORTENCIAL MINERO"
        Me.lbl_cpm_titulo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'tct_cpm_carga_informacion
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tct_cpm_carga_informacion, 7)
        Me.tct_cpm_carga_informacion.Controls.Add(Me.tpg_cpm_metalico)
        Me.tct_cpm_carga_informacion.Controls.Add(Me.tpg_cpm_no_metalico)
        Me.tct_cpm_carga_informacion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tct_cpm_carga_informacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tct_cpm_carga_informacion.ImageList = Me.iml_cpm_imagenes
        Me.tct_cpm_carga_informacion.Location = New System.Drawing.Point(3, 245)
        Me.tct_cpm_carga_informacion.Name = "tct_cpm_carga_informacion"
        Me.tct_cpm_carga_informacion.SelectedIndex = 0
        Me.tct_cpm_carga_informacion.Size = New System.Drawing.Size(391, 222)
        Me.tct_cpm_carga_informacion.TabIndex = 6
        '
        'tpg_cpm_metalico
        '
        Me.tpg_cpm_metalico.Controls.Add(Me.TableLayoutPanel3)
        Me.tpg_cpm_metalico.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpg_cpm_metalico.ImageIndex = 2
        Me.tpg_cpm_metalico.Location = New System.Drawing.Point(4, 23)
        Me.tpg_cpm_metalico.Name = "tpg_cpm_metalico"
        Me.tpg_cpm_metalico.Padding = New System.Windows.Forms.Padding(3)
        Me.tpg_cpm_metalico.Size = New System.Drawing.Size(383, 195)
        Me.tpg_cpm_metalico.TabIndex = 0
        Me.tpg_cpm_metalico.Text = "Metálico"
        Me.tpg_cpm_metalico.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label11, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label12, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label13, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label14, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.M_01_GPO_Litologia, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.M_02_GPL_FallaGeologica, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.M_03_GPO_DepositoMineral, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.M_VAR_RAS_Geoquimica, 2, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.M_05_GPO_SensorRemoto, 2, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_M_01_GPO_Litologia, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_M_02_GPL_FallaGeologica, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_M_03_GPO_DepositoMineral, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_M_VAR_RAS_Geoquimica, 3, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_M_05_GPO_SensorRemoto, 3, 4)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 6
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(377, 189)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.ImageIndex = 2
        Me.Label10.ImageList = Me.iml_cpm_imagenes
        Me.Label10.Location = New System.Drawing.Point(8, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(281, 25)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "     Unidades Geológicas"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'iml_cpm_imagenes
        '
        Me.iml_cpm_imagenes.ImageStream = CType(resources.GetObject("iml_cpm_imagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.iml_cpm_imagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.iml_cpm_imagenes.Images.SetKeyName(0, "CheckBoxUnChecked.ico")
        Me.iml_cpm_imagenes.Images.SetKeyName(1, "GenericCheckMark16.png")
        Me.iml_cpm_imagenes.Images.SetKeyName(2, "GeoprocessingFunction16.png")
        Me.iml_cpm_imagenes.Images.SetKeyName(3, "carga.png")
        Me.iml_cpm_imagenes.Images.SetKeyName(4, "GenericCheckMarkBlack16.png")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.ImageIndex = 2
        Me.Label11.ImageList = Me.iml_cpm_imagenes
        Me.Label11.Location = New System.Drawing.Point(8, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(281, 25)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "     Fallas Geológicas"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label12.ImageIndex = 2
        Me.Label12.ImageList = Me.iml_cpm_imagenes
        Me.Label12.Location = New System.Drawing.Point(8, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(281, 25)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "     Depositos Minerales"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label13.ImageIndex = 2
        Me.Label13.ImageList = Me.iml_cpm_imagenes
        Me.Label13.Location = New System.Drawing.Point(8, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(281, 25)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "     Geoquímica"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.ImageIndex = 2
        Me.Label14.ImageList = Me.iml_cpm_imagenes
        Me.Label14.Location = New System.Drawing.Point(8, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(281, 25)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "     Sensores Remotos"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'M_01_GPO_Litologia
        '
        Me.M_01_GPO_Litologia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.M_01_GPO_Litologia.FlatAppearance.BorderSize = 0
        Me.M_01_GPO_Litologia.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_01_GPO_Litologia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_01_GPO_Litologia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_01_GPO_Litologia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_01_GPO_Litologia.ImageKey = "(ninguno)"
        Me.M_01_GPO_Litologia.ImageList = Me.iml_cpm_imagenes
        Me.M_01_GPO_Litologia.Location = New System.Drawing.Point(295, 3)
        Me.M_01_GPO_Litologia.Name = "M_01_GPO_Litologia"
        Me.M_01_GPO_Litologia.Size = New System.Drawing.Size(34, 19)
        Me.M_01_GPO_Litologia.TabIndex = 5
        Me.M_01_GPO_Litologia.UseVisualStyleBackColor = True
        '
        'M_02_GPL_FallaGeologica
        '
        Me.M_02_GPL_FallaGeologica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.M_02_GPL_FallaGeologica.FlatAppearance.BorderSize = 0
        Me.M_02_GPL_FallaGeologica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_02_GPL_FallaGeologica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_02_GPL_FallaGeologica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_02_GPL_FallaGeologica.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_02_GPL_FallaGeologica.ImageKey = "(ninguno)"
        Me.M_02_GPL_FallaGeologica.ImageList = Me.iml_cpm_imagenes
        Me.M_02_GPL_FallaGeologica.Location = New System.Drawing.Point(295, 28)
        Me.M_02_GPL_FallaGeologica.Name = "M_02_GPL_FallaGeologica"
        Me.M_02_GPL_FallaGeologica.Size = New System.Drawing.Size(34, 19)
        Me.M_02_GPL_FallaGeologica.TabIndex = 6
        Me.M_02_GPL_FallaGeologica.UseVisualStyleBackColor = True
        '
        'M_03_GPO_DepositoMineral
        '
        Me.M_03_GPO_DepositoMineral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.M_03_GPO_DepositoMineral.FlatAppearance.BorderSize = 0
        Me.M_03_GPO_DepositoMineral.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_03_GPO_DepositoMineral.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_03_GPO_DepositoMineral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_03_GPO_DepositoMineral.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_03_GPO_DepositoMineral.ImageKey = "(ninguno)"
        Me.M_03_GPO_DepositoMineral.ImageList = Me.iml_cpm_imagenes
        Me.M_03_GPO_DepositoMineral.Location = New System.Drawing.Point(295, 53)
        Me.M_03_GPO_DepositoMineral.Name = "M_03_GPO_DepositoMineral"
        Me.M_03_GPO_DepositoMineral.Size = New System.Drawing.Size(34, 19)
        Me.M_03_GPO_DepositoMineral.TabIndex = 7
        Me.M_03_GPO_DepositoMineral.UseVisualStyleBackColor = True
        '
        'M_VAR_RAS_Geoquimica
        '
        Me.M_VAR_RAS_Geoquimica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.M_VAR_RAS_Geoquimica.FlatAppearance.BorderSize = 0
        Me.M_VAR_RAS_Geoquimica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_VAR_RAS_Geoquimica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_VAR_RAS_Geoquimica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_VAR_RAS_Geoquimica.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_VAR_RAS_Geoquimica.ImageKey = "(ninguno)"
        Me.M_VAR_RAS_Geoquimica.ImageList = Me.iml_cpm_imagenes
        Me.M_VAR_RAS_Geoquimica.Location = New System.Drawing.Point(295, 78)
        Me.M_VAR_RAS_Geoquimica.Name = "M_VAR_RAS_Geoquimica"
        Me.M_VAR_RAS_Geoquimica.Size = New System.Drawing.Size(34, 19)
        Me.M_VAR_RAS_Geoquimica.TabIndex = 8
        Me.M_VAR_RAS_Geoquimica.UseVisualStyleBackColor = True
        '
        'M_05_GPO_SensorRemoto
        '
        Me.M_05_GPO_SensorRemoto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.M_05_GPO_SensorRemoto.FlatAppearance.BorderSize = 0
        Me.M_05_GPO_SensorRemoto.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_05_GPO_SensorRemoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_05_GPO_SensorRemoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_05_GPO_SensorRemoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_05_GPO_SensorRemoto.ImageKey = "(ninguno)"
        Me.M_05_GPO_SensorRemoto.ImageList = Me.iml_cpm_imagenes
        Me.M_05_GPO_SensorRemoto.Location = New System.Drawing.Point(295, 103)
        Me.M_05_GPO_SensorRemoto.Name = "M_05_GPO_SensorRemoto"
        Me.M_05_GPO_SensorRemoto.Size = New System.Drawing.Size(34, 19)
        Me.M_05_GPO_SensorRemoto.TabIndex = 9
        Me.M_05_GPO_SensorRemoto.UseVisualStyleBackColor = True
        '
        'btn_M_01_GPO_Litologia
        '
        Me.btn_M_01_GPO_Litologia.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_01_GPO_Litologia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_01_GPO_Litologia.FlatAppearance.BorderSize = 0
        Me.btn_M_01_GPO_Litologia.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btn_M_01_GPO_Litologia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_M_01_GPO_Litologia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btn_M_01_GPO_Litologia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_01_GPO_Litologia.ImageIndex = 3
        Me.btn_M_01_GPO_Litologia.ImageList = Me.iml_cpm_imagenes
        Me.btn_M_01_GPO_Litologia.Location = New System.Drawing.Point(335, 3)
        Me.btn_M_01_GPO_Litologia.Name = "btn_M_01_GPO_Litologia"
        Me.btn_M_01_GPO_Litologia.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_01_GPO_Litologia.TabIndex = 10
        Me.ttp_cpm.SetToolTip(Me.btn_M_01_GPO_Litologia, "Cargar información de Unidades Geológicas")
        Me.btn_M_01_GPO_Litologia.UseVisualStyleBackColor = True
        '
        'btn_M_02_GPL_FallaGeologica
        '
        Me.btn_M_02_GPL_FallaGeologica.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_02_GPL_FallaGeologica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_02_GPL_FallaGeologica.FlatAppearance.BorderSize = 0
        Me.btn_M_02_GPL_FallaGeologica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btn_M_02_GPL_FallaGeologica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_M_02_GPL_FallaGeologica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btn_M_02_GPL_FallaGeologica.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_02_GPL_FallaGeologica.ImageIndex = 3
        Me.btn_M_02_GPL_FallaGeologica.ImageList = Me.iml_cpm_imagenes
        Me.btn_M_02_GPL_FallaGeologica.Location = New System.Drawing.Point(335, 28)
        Me.btn_M_02_GPL_FallaGeologica.Name = "btn_M_02_GPL_FallaGeologica"
        Me.btn_M_02_GPL_FallaGeologica.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_02_GPL_FallaGeologica.TabIndex = 11
        Me.ttp_cpm.SetToolTip(Me.btn_M_02_GPL_FallaGeologica, "Cargar información de Fallas Geológicas")
        Me.btn_M_02_GPL_FallaGeologica.UseVisualStyleBackColor = True
        '
        'btn_M_03_GPO_DepositoMineral
        '
        Me.btn_M_03_GPO_DepositoMineral.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_03_GPO_DepositoMineral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_03_GPO_DepositoMineral.FlatAppearance.BorderSize = 0
        Me.btn_M_03_GPO_DepositoMineral.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btn_M_03_GPO_DepositoMineral.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_M_03_GPO_DepositoMineral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btn_M_03_GPO_DepositoMineral.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_03_GPO_DepositoMineral.ImageIndex = 3
        Me.btn_M_03_GPO_DepositoMineral.ImageList = Me.iml_cpm_imagenes
        Me.btn_M_03_GPO_DepositoMineral.Location = New System.Drawing.Point(335, 53)
        Me.btn_M_03_GPO_DepositoMineral.Name = "btn_M_03_GPO_DepositoMineral"
        Me.btn_M_03_GPO_DepositoMineral.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_03_GPO_DepositoMineral.TabIndex = 12
        Me.ttp_cpm.SetToolTip(Me.btn_M_03_GPO_DepositoMineral, "Cargar informaciónde Depositos Minerales")
        Me.btn_M_03_GPO_DepositoMineral.UseVisualStyleBackColor = True
        '
        'btn_M_VAR_RAS_Geoquimica
        '
        Me.btn_M_VAR_RAS_Geoquimica.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_VAR_RAS_Geoquimica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_VAR_RAS_Geoquimica.FlatAppearance.BorderSize = 0
        Me.btn_M_VAR_RAS_Geoquimica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btn_M_VAR_RAS_Geoquimica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_M_VAR_RAS_Geoquimica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btn_M_VAR_RAS_Geoquimica.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_VAR_RAS_Geoquimica.ImageIndex = 3
        Me.btn_M_VAR_RAS_Geoquimica.ImageList = Me.iml_cpm_imagenes
        Me.btn_M_VAR_RAS_Geoquimica.Location = New System.Drawing.Point(335, 78)
        Me.btn_M_VAR_RAS_Geoquimica.Name = "btn_M_VAR_RAS_Geoquimica"
        Me.btn_M_VAR_RAS_Geoquimica.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_VAR_RAS_Geoquimica.TabIndex = 13
        Me.ttp_cpm.SetToolTip(Me.btn_M_VAR_RAS_Geoquimica, "Cargar información de Geoquímica")
        Me.btn_M_VAR_RAS_Geoquimica.UseVisualStyleBackColor = True
        '
        'btn_M_05_GPO_SensorRemoto
        '
        Me.btn_M_05_GPO_SensorRemoto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_05_GPO_SensorRemoto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_05_GPO_SensorRemoto.FlatAppearance.BorderSize = 0
        Me.btn_M_05_GPO_SensorRemoto.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.btn_M_05_GPO_SensorRemoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_M_05_GPO_SensorRemoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btn_M_05_GPO_SensorRemoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_05_GPO_SensorRemoto.ImageIndex = 3
        Me.btn_M_05_GPO_SensorRemoto.ImageList = Me.iml_cpm_imagenes
        Me.btn_M_05_GPO_SensorRemoto.Location = New System.Drawing.Point(335, 103)
        Me.btn_M_05_GPO_SensorRemoto.Name = "btn_M_05_GPO_SensorRemoto"
        Me.btn_M_05_GPO_SensorRemoto.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_05_GPO_SensorRemoto.TabIndex = 14
        Me.ttp_cpm.SetToolTip(Me.btn_M_05_GPO_SensorRemoto, "Cargar información de Sensores remotos")
        Me.btn_M_05_GPO_SensorRemoto.UseVisualStyleBackColor = True
        '
        'tpg_cpm_no_metalico
        '
        Me.tpg_cpm_no_metalico.Controls.Add(Me.TableLayoutPanel2)
        Me.tpg_cpm_no_metalico.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpg_cpm_no_metalico.ImageIndex = 2
        Me.tpg_cpm_no_metalico.Location = New System.Drawing.Point(4, 23)
        Me.tpg_cpm_no_metalico.Name = "tpg_cpm_no_metalico"
        Me.tpg_cpm_no_metalico.Padding = New System.Windows.Forms.Padding(3)
        Me.tpg_cpm_no_metalico.Size = New System.Drawing.Size(383, 195)
        Me.tpg_cpm_no_metalico.TabIndex = 1
        Me.tpg_cpm_no_metalico.Text = "No Metálico"
        Me.tpg_cpm_no_metalico.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label9, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_RMI_06_GPO_Litologia, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_RMI_07_GPT_Sustancias, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_RMI_09_GPO_SensorRemoto, 3, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_RMI_10_GPL_Accesos, 3, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.RMI_06_GPO_Litologia, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.RMI_07_GPT_Sustancias, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.RMI_09_GPO_SensorRemoto, 2, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.RMI_10_GPL_Accesos, 2, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(377, 189)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.ImageIndex = 2
        Me.Label6.ImageList = Me.iml_cpm_imagenes
        Me.Label6.Location = New System.Drawing.Point(8, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(281, 25)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "      Litología"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.ImageIndex = 2
        Me.Label7.ImageList = Me.iml_cpm_imagenes
        Me.Label7.Location = New System.Drawing.Point(8, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(281, 25)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "     Sustancias"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.ImageIndex = 2
        Me.Label8.ImageList = Me.iml_cpm_imagenes
        Me.Label8.Location = New System.Drawing.Point(8, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(281, 25)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "     Sensores Remotos"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.ImageIndex = 2
        Me.Label9.ImageList = Me.iml_cpm_imagenes
        Me.Label9.Location = New System.Drawing.Point(8, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(281, 25)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "     Accesos"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_RMI_06_GPO_Litologia
        '
        Me.btn_RMI_06_GPO_Litologia.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_RMI_06_GPO_Litologia.FlatAppearance.BorderSize = 0
        Me.btn_RMI_06_GPO_Litologia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_RMI_06_GPO_Litologia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RMI_06_GPO_Litologia.ImageIndex = 3
        Me.btn_RMI_06_GPO_Litologia.ImageList = Me.iml_cpm_imagenes
        Me.btn_RMI_06_GPO_Litologia.Location = New System.Drawing.Point(335, 3)
        Me.btn_RMI_06_GPO_Litologia.Name = "btn_RMI_06_GPO_Litologia"
        Me.btn_RMI_06_GPO_Litologia.Size = New System.Drawing.Size(34, 19)
        Me.btn_RMI_06_GPO_Litologia.TabIndex = 5
        Me.ttp_cpm.SetToolTip(Me.btn_RMI_06_GPO_Litologia, "Cargar información de litología")
        Me.btn_RMI_06_GPO_Litologia.UseVisualStyleBackColor = True
        '
        'btn_RMI_07_GPT_Sustancias
        '
        Me.btn_RMI_07_GPT_Sustancias.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_RMI_07_GPT_Sustancias.FlatAppearance.BorderSize = 0
        Me.btn_RMI_07_GPT_Sustancias.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_RMI_07_GPT_Sustancias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RMI_07_GPT_Sustancias.ImageIndex = 3
        Me.btn_RMI_07_GPT_Sustancias.ImageList = Me.iml_cpm_imagenes
        Me.btn_RMI_07_GPT_Sustancias.Location = New System.Drawing.Point(335, 28)
        Me.btn_RMI_07_GPT_Sustancias.Name = "btn_RMI_07_GPT_Sustancias"
        Me.btn_RMI_07_GPT_Sustancias.Size = New System.Drawing.Size(34, 19)
        Me.btn_RMI_07_GPT_Sustancias.TabIndex = 6
        Me.ttp_cpm.SetToolTip(Me.btn_RMI_07_GPT_Sustancias, "Cargar información de Sustancias")
        Me.btn_RMI_07_GPT_Sustancias.UseVisualStyleBackColor = True
        '
        'btn_RMI_09_GPO_SensorRemoto
        '
        Me.btn_RMI_09_GPO_SensorRemoto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_RMI_09_GPO_SensorRemoto.FlatAppearance.BorderSize = 0
        Me.btn_RMI_09_GPO_SensorRemoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_RMI_09_GPO_SensorRemoto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RMI_09_GPO_SensorRemoto.ImageIndex = 3
        Me.btn_RMI_09_GPO_SensorRemoto.ImageList = Me.iml_cpm_imagenes
        Me.btn_RMI_09_GPO_SensorRemoto.Location = New System.Drawing.Point(335, 53)
        Me.btn_RMI_09_GPO_SensorRemoto.Name = "btn_RMI_09_GPO_SensorRemoto"
        Me.btn_RMI_09_GPO_SensorRemoto.Size = New System.Drawing.Size(34, 19)
        Me.btn_RMI_09_GPO_SensorRemoto.TabIndex = 7
        Me.ttp_cpm.SetToolTip(Me.btn_RMI_09_GPO_SensorRemoto, "Cargar información de Sensores Remotos")
        Me.btn_RMI_09_GPO_SensorRemoto.UseVisualStyleBackColor = True
        '
        'btn_RMI_10_GPL_Accesos
        '
        Me.btn_RMI_10_GPL_Accesos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_RMI_10_GPL_Accesos.FlatAppearance.BorderSize = 0
        Me.btn_RMI_10_GPL_Accesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_RMI_10_GPL_Accesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_RMI_10_GPL_Accesos.ImageIndex = 3
        Me.btn_RMI_10_GPL_Accesos.ImageList = Me.iml_cpm_imagenes
        Me.btn_RMI_10_GPL_Accesos.Location = New System.Drawing.Point(335, 78)
        Me.btn_RMI_10_GPL_Accesos.Name = "btn_RMI_10_GPL_Accesos"
        Me.btn_RMI_10_GPL_Accesos.Size = New System.Drawing.Size(34, 19)
        Me.btn_RMI_10_GPL_Accesos.TabIndex = 8
        Me.ttp_cpm.SetToolTip(Me.btn_RMI_10_GPL_Accesos, "Cargar información de Accesos")
        Me.btn_RMI_10_GPL_Accesos.UseVisualStyleBackColor = True
        '
        'RMI_06_GPO_Litologia
        '
        Me.RMI_06_GPO_Litologia.FlatAppearance.BorderSize = 0
        Me.RMI_06_GPO_Litologia.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.RMI_06_GPO_Litologia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.RMI_06_GPO_Litologia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.RMI_06_GPO_Litologia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RMI_06_GPO_Litologia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMI_06_GPO_Litologia.ImageKey = "(ninguno)"
        Me.RMI_06_GPO_Litologia.ImageList = Me.iml_cpm_imagenes
        Me.RMI_06_GPO_Litologia.Location = New System.Drawing.Point(295, 3)
        Me.RMI_06_GPO_Litologia.Name = "RMI_06_GPO_Litologia"
        Me.RMI_06_GPO_Litologia.Size = New System.Drawing.Size(34, 19)
        Me.RMI_06_GPO_Litologia.TabIndex = 10
        Me.RMI_06_GPO_Litologia.UseVisualStyleBackColor = True
        Me.RMI_06_GPO_Litologia.Visible = False
        '
        'RMI_07_GPT_Sustancias
        '
        Me.RMI_07_GPT_Sustancias.FlatAppearance.BorderSize = 0
        Me.RMI_07_GPT_Sustancias.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.RMI_07_GPT_Sustancias.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.RMI_07_GPT_Sustancias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.RMI_07_GPT_Sustancias.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RMI_07_GPT_Sustancias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMI_07_GPT_Sustancias.ImageKey = "(ninguno)"
        Me.RMI_07_GPT_Sustancias.ImageList = Me.iml_cpm_imagenes
        Me.RMI_07_GPT_Sustancias.Location = New System.Drawing.Point(295, 28)
        Me.RMI_07_GPT_Sustancias.Name = "RMI_07_GPT_Sustancias"
        Me.RMI_07_GPT_Sustancias.Size = New System.Drawing.Size(34, 19)
        Me.RMI_07_GPT_Sustancias.TabIndex = 11
        Me.RMI_07_GPT_Sustancias.UseVisualStyleBackColor = True
        Me.RMI_07_GPT_Sustancias.Visible = False
        '
        'RMI_09_GPO_SensorRemoto
        '
        Me.RMI_09_GPO_SensorRemoto.FlatAppearance.BorderSize = 0
        Me.RMI_09_GPO_SensorRemoto.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.RMI_09_GPO_SensorRemoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.RMI_09_GPO_SensorRemoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.RMI_09_GPO_SensorRemoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RMI_09_GPO_SensorRemoto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMI_09_GPO_SensorRemoto.ImageKey = "(ninguno)"
        Me.RMI_09_GPO_SensorRemoto.ImageList = Me.iml_cpm_imagenes
        Me.RMI_09_GPO_SensorRemoto.Location = New System.Drawing.Point(295, 53)
        Me.RMI_09_GPO_SensorRemoto.Name = "RMI_09_GPO_SensorRemoto"
        Me.RMI_09_GPO_SensorRemoto.Size = New System.Drawing.Size(34, 19)
        Me.RMI_09_GPO_SensorRemoto.TabIndex = 12
        Me.RMI_09_GPO_SensorRemoto.UseVisualStyleBackColor = True
        Me.RMI_09_GPO_SensorRemoto.Visible = False
        '
        'RMI_10_GPL_Accesos
        '
        Me.RMI_10_GPL_Accesos.FlatAppearance.BorderSize = 0
        Me.RMI_10_GPL_Accesos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.RMI_10_GPL_Accesos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.RMI_10_GPL_Accesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RMI_10_GPL_Accesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMI_10_GPL_Accesos.ImageKey = "(ninguno)"
        Me.RMI_10_GPL_Accesos.ImageList = Me.iml_cpm_imagenes
        Me.RMI_10_GPL_Accesos.Location = New System.Drawing.Point(295, 78)
        Me.RMI_10_GPL_Accesos.Name = "RMI_10_GPL_Accesos"
        Me.RMI_10_GPL_Accesos.Size = New System.Drawing.Size(34, 19)
        Me.RMI_10_GPL_Accesos.TabIndex = 13
        Me.RMI_10_GPL_Accesos.UseVisualStyleBackColor = True
        Me.RMI_10_GPL_Accesos.Visible = False
        '
        'lbl_cpm_pmm
        '
        Me.lbl_cpm_pmm.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_pmm, 5)
        Me.lbl_cpm_pmm.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_pmm.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_pmm.Location = New System.Drawing.Point(3, 507)
        Me.lbl_cpm_pmm.Name = "lbl_cpm_pmm"
        Me.lbl_cpm_pmm.Size = New System.Drawing.Size(311, 13)
        Me.lbl_cpm_pmm.TabIndex = 7
        Me.lbl_cpm_pmm.Text = "Potencial Minero Metálico"
        '
        'lbl_cpm_pmnm
        '
        Me.lbl_cpm_pmnm.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_pmnm, 5)
        Me.lbl_cpm_pmnm.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_pmnm.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_pmnm.Location = New System.Drawing.Point(3, 532)
        Me.lbl_cpm_pmnm.Name = "lbl_cpm_pmnm"
        Me.lbl_cpm_pmnm.Size = New System.Drawing.Size(311, 13)
        Me.lbl_cpm_pmnm.TabIndex = 8
        Me.lbl_cpm_pmnm.Text = "Potencial Minero no metálico"
        '
        'btn_cpm_calcular_potencial
        '
        Me.btn_cpm_calcular_potencial.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_cpm_calcular_potencial, 7)
        Me.btn_cpm_calcular_potencial.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cpm_calcular_potencial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cpm_calcular_potencial.FlatAppearance.BorderSize = 0
        Me.btn_cpm_calcular_potencial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.btn_cpm_calcular_potencial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cpm_calcular_potencial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btn_cpm_calcular_potencial.ImageIndex = 2
        Me.btn_cpm_calcular_potencial.ImageList = Me.iml_cpm_imagenes
        Me.btn_cpm_calcular_potencial.Location = New System.Drawing.Point(3, 553)
        Me.btn_cpm_calcular_potencial.Name = "btn_cpm_calcular_potencial"
        Me.btn_cpm_calcular_potencial.Size = New System.Drawing.Size(391, 29)
        Me.btn_cpm_calcular_potencial.TabIndex = 9
        Me.btn_cpm_calcular_potencial.Text = "Calcular potencial minero"
        Me.btn_cpm_calcular_potencial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cpm_calcular_potencial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ttp_cpm.SetToolTip(Me.btn_cpm_calcular_potencial, "Calcular el potencial minero")
        Me.btn_cpm_calcular_potencial.UseVisualStyleBackColor = False
        '
        'btn_M_RAS_PotencialMineroMetalico
        '
        Me.btn_M_RAS_PotencialMineroMetalico.BackgroundImage = CType(resources.GetObject("btn_M_RAS_PotencialMineroMetalico.BackgroundImage"), System.Drawing.Image)
        Me.btn_M_RAS_PotencialMineroMetalico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_M_RAS_PotencialMineroMetalico.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_RAS_PotencialMineroMetalico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_RAS_PotencialMineroMetalico.FlatAppearance.BorderSize = 0
        Me.btn_M_RAS_PotencialMineroMetalico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_RAS_PotencialMineroMetalico.Location = New System.Drawing.Point(360, 498)
        Me.btn_M_RAS_PotencialMineroMetalico.Name = "btn_M_RAS_PotencialMineroMetalico"
        Me.btn_M_RAS_PotencialMineroMetalico.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_RAS_PotencialMineroMetalico.TabIndex = 12
        Me.ttp_cpm.SetToolTip(Me.btn_M_RAS_PotencialMineroMetalico, "Calcular el potencial minero metálico")
        Me.btn_M_RAS_PotencialMineroMetalico.UseVisualStyleBackColor = True
        '
        'btn_M_RAS_PotencialMineroNoMetalico
        '
        Me.btn_M_RAS_PotencialMineroNoMetalico.BackgroundImage = CType(resources.GetObject("btn_M_RAS_PotencialMineroNoMetalico.BackgroundImage"), System.Drawing.Image)
        Me.btn_M_RAS_PotencialMineroNoMetalico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_M_RAS_PotencialMineroNoMetalico.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_M_RAS_PotencialMineroNoMetalico.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_M_RAS_PotencialMineroNoMetalico.FlatAppearance.BorderSize = 0
        Me.btn_M_RAS_PotencialMineroNoMetalico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_M_RAS_PotencialMineroNoMetalico.Location = New System.Drawing.Point(360, 523)
        Me.btn_M_RAS_PotencialMineroNoMetalico.Name = "btn_M_RAS_PotencialMineroNoMetalico"
        Me.btn_M_RAS_PotencialMineroNoMetalico.Size = New System.Drawing.Size(34, 19)
        Me.btn_M_RAS_PotencialMineroNoMetalico.TabIndex = 13
        Me.ttp_cpm.SetToolTip(Me.btn_M_RAS_PotencialMineroNoMetalico, "Calcular el potencial minero no metálico")
        Me.btn_M_RAS_PotencialMineroNoMetalico.UseVisualStyleBackColor = True
        '
        'lbl_cpm_cargar_info
        '
        Me.lbl_cpm_cargar_info.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_cargar_info, 7)
        Me.lbl_cpm_cargar_info.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_cargar_info.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_cargar_info.Location = New System.Drawing.Point(3, 224)
        Me.lbl_cpm_cargar_info.Name = "lbl_cpm_cargar_info"
        Me.lbl_cpm_cargar_info.Size = New System.Drawing.Size(391, 13)
        Me.lbl_cpm_cargar_info.TabIndex = 14
        Me.lbl_cpm_cargar_info.Text = "2. Cargar información geográfica"
        '
        'lbl_cpm_calculo_potencial
        '
        Me.lbl_cpm_calculo_potencial.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_cpm_calculo_potencial, 7)
        Me.lbl_cpm_calculo_potencial.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_cpm_calculo_potencial.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_cpm_calculo_potencial.Location = New System.Drawing.Point(3, 482)
        Me.lbl_cpm_calculo_potencial.Name = "lbl_cpm_calculo_potencial"
        Me.lbl_cpm_calculo_potencial.Size = New System.Drawing.Size(391, 13)
        Me.lbl_cpm_calculo_potencial.TabIndex = 15
        Me.lbl_cpm_calculo_potencial.Text = "3. Cálculo del Potencial Minero"
        '
        'M_RAS_PotencialMineroMetalico
        '
        Me.M_RAS_PotencialMineroMetalico.FlatAppearance.BorderSize = 0
        Me.M_RAS_PotencialMineroMetalico.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroMetalico.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroMetalico.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroMetalico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_RAS_PotencialMineroMetalico.ImageKey = "(ninguno)"
        Me.M_RAS_PotencialMineroMetalico.ImageList = Me.iml_cpm_imagenes
        Me.M_RAS_PotencialMineroMetalico.Location = New System.Drawing.Point(320, 498)
        Me.M_RAS_PotencialMineroMetalico.Name = "M_RAS_PotencialMineroMetalico"
        Me.M_RAS_PotencialMineroMetalico.Size = New System.Drawing.Size(34, 19)
        Me.M_RAS_PotencialMineroMetalico.TabIndex = 16
        Me.M_RAS_PotencialMineroMetalico.UseVisualStyleBackColor = True
        Me.M_RAS_PotencialMineroMetalico.Visible = False
        '
        'M_RAS_PotencialMineroNoMetalico
        '
        Me.M_RAS_PotencialMineroNoMetalico.FlatAppearance.BorderSize = 0
        Me.M_RAS_PotencialMineroNoMetalico.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroNoMetalico.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroNoMetalico.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.M_RAS_PotencialMineroNoMetalico.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.M_RAS_PotencialMineroNoMetalico.ImageKey = "(ninguno)"
        Me.M_RAS_PotencialMineroNoMetalico.ImageList = Me.iml_cpm_imagenes
        Me.M_RAS_PotencialMineroNoMetalico.Location = New System.Drawing.Point(320, 523)
        Me.M_RAS_PotencialMineroNoMetalico.Name = "M_RAS_PotencialMineroNoMetalico"
        Me.M_RAS_PotencialMineroNoMetalico.Size = New System.Drawing.Size(34, 19)
        Me.M_RAS_PotencialMineroNoMetalico.TabIndex = 17
        Me.M_RAS_PotencialMineroNoMetalico.UseVisualStyleBackColor = True
        Me.M_RAS_PotencialMineroNoMetalico.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 7)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(391, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "1. Crear una geodatabase o referenciar una existente"
        '
        'ttp_cpm
        '
        Me.ttp_cpm.IsBalloon = True
        '
        'Form_calculo_potencial_minero
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 585)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_calculo_potencial_minero"
        Me.Text = "Form_calculo_potencial_minero"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.tct_cpm_carga_informacion.ResumeLayout(False)
        Me.tpg_cpm_metalico.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.tpg_cpm_no_metalico.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_cpm_open_folder As System.Windows.Forms.Button
    Friend WithEvents btn_cpm_nuevo As System.Windows.Forms.Button
    Friend WithEvents btn_cpm_existente As System.Windows.Forms.Button
    Friend WithEvents lbl_cpm_departamento_proy As System.Windows.Forms.Label
    Friend WithEvents tbx_cpm_directorio As System.Windows.Forms.TextBox
    Friend WithEvents lbl_cpm_directorio As System.Windows.Forms.Label
    Friend WithEvents btn_cpm_crear_cargar As System.Windows.Forms.Button
    Friend WithEvents cbx_cpm_departamento_proy As System.Windows.Forms.ComboBox
    Friend WithEvents btn_cpm_directorio As System.Windows.Forms.Button
    Friend WithEvents lbl_cpm_titulo As System.Windows.Forms.Label
    Friend WithEvents tct_cpm_carga_informacion As System.Windows.Forms.TabControl
    Friend WithEvents tpg_cpm_metalico As System.Windows.Forms.TabPage
    Friend WithEvents tpg_cpm_no_metalico As System.Windows.Forms.TabPage
    Friend WithEvents lbl_cpm_pmm As System.Windows.Forms.Label
    Friend WithEvents lbl_cpm_pmnm As System.Windows.Forms.Label
    Friend WithEvents btn_cpm_calcular_potencial As System.Windows.Forms.Button
    Friend WithEvents btn_M_RAS_PotencialMineroMetalico As System.Windows.Forms.Button
    Friend WithEvents btn_M_RAS_PotencialMineroNoMetalico As System.Windows.Forms.Button
    Friend WithEvents lbl_cpm_cargar_info As System.Windows.Forms.Label
    Friend WithEvents lbl_cpm_calculo_potencial As System.Windows.Forms.Label
    Friend WithEvents M_RAS_PotencialMineroMetalico As System.Windows.Forms.Button
    Friend WithEvents iml_cpm_imagenes As System.Windows.Forms.ImageList
    Friend WithEvents M_RAS_PotencialMineroNoMetalico As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btn_RMI_06_GPO_Litologia As System.Windows.Forms.Button
    Friend WithEvents btn_RMI_07_GPT_Sustancias As System.Windows.Forms.Button
    Friend WithEvents btn_RMI_09_GPO_SensorRemoto As System.Windows.Forms.Button
    Friend WithEvents btn_RMI_10_GPL_Accesos As System.Windows.Forms.Button
    Friend WithEvents RMI_06_GPO_Litologia As System.Windows.Forms.Button
    Friend WithEvents RMI_07_GPT_Sustancias As System.Windows.Forms.Button
    Friend WithEvents RMI_09_GPO_SensorRemoto As System.Windows.Forms.Button
    Friend WithEvents RMI_10_GPL_Accesos As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents M_01_GPO_Litologia As System.Windows.Forms.Button
    Friend WithEvents M_02_GPL_FallaGeologica As System.Windows.Forms.Button
    Friend WithEvents M_03_GPO_DepositoMineral As System.Windows.Forms.Button
    Friend WithEvents M_VAR_RAS_Geoquimica As System.Windows.Forms.Button
    Friend WithEvents M_05_GPO_SensorRemoto As System.Windows.Forms.Button
    Friend WithEvents btn_M_01_GPO_Litologia As System.Windows.Forms.Button
    Friend WithEvents btn_M_02_GPL_FallaGeologica As System.Windows.Forms.Button
    Friend WithEvents btn_M_03_GPO_DepositoMineral As System.Windows.Forms.Button
    Friend WithEvents btn_M_VAR_RAS_Geoquimica As System.Windows.Forms.Button
    Friend WithEvents btn_M_05_GPO_SensorRemoto As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ttp_cpm As System.Windows.Forms.ToolTip
End Class
