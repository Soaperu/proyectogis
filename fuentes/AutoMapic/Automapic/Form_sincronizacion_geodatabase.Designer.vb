<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_sincronizacion_geodatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_sincronizacion_geodatabase))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_sg_origen = New System.Windows.Forms.Label()
        Me.lbl_sg_destino = New System.Windows.Forms.Label()
        Me.tbx_origen = New System.Windows.Forms.TextBox()
        Me.tbx_destino = New System.Windows.Forms.TextBox()
        Me.lbl_sg_filtrocapas = New System.Windows.Forms.Label()
        Me.lbl_sg_estdestino = New System.Windows.Forms.Label()
        Me.cbx_sg_filtrocapas = New System.Windows.Forms.ComboBox()
        Me.rbtn_sg_estds = New System.Windows.Forms.RadioButton()
        Me.rbtn_sg_estraiz = New System.Windows.Forms.RadioButton()
        Me.dg_sg_capas = New System.Windows.Forms.DataGridView()
        Me.btn_sg_enviar = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btn_sg_origen = New System.Windows.Forms.Button()
        Me.btn_sg_destino = New System.Windows.Forms.Button()
        Me.lbl_sg_legend = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_s_nc = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_s_nr = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_s_ne = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_nc = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_nr = New System.Windows.Forms.Label()
        Me.lbl_sg_legend_ne = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dg_sg_capas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_origen, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_destino, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_origen, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_destino, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_filtrocapas, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_estdestino, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_sg_filtrocapas, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.rbtn_sg_estds, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.rbtn_sg_estraiz, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.dg_sg_capas, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_sg_enviar, 0, 18)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_sg_origen, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_sg_destino, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_s_nc, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_s_nr, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_s_ne, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_nc, 1, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_nr, 1, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_sg_legend_ne, 1, 16)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 19
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(308, 550)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lbl_sg_origen
        '
        Me.lbl_sg_origen.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_sg_origen, 2)
        Me.lbl_sg_origen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_sg_origen.Location = New System.Drawing.Point(2, 4)
        Me.lbl_sg_origen.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_origen.Name = "lbl_sg_origen"
        Me.lbl_sg_origen.Size = New System.Drawing.Size(264, 16)
        Me.lbl_sg_origen.TabIndex = 0
        Me.lbl_sg_origen.Text = "Fuente de datos :"
        '
        'lbl_sg_destino
        '
        Me.lbl_sg_destino.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_sg_destino, 2)
        Me.lbl_sg_destino.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_sg_destino.Location = New System.Drawing.Point(2, 53)
        Me.lbl_sg_destino.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_destino.Name = "lbl_sg_destino"
        Me.lbl_sg_destino.Size = New System.Drawing.Size(264, 13)
        Me.lbl_sg_destino.TabIndex = 2
        Me.lbl_sg_destino.Text = "Ruta de destino :"
        '
        'tbx_origen
        '
        Me.tbx_origen.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_origen, 2)
        Me.tbx_origen.Enabled = False
        Me.tbx_origen.Location = New System.Drawing.Point(2, 25)
        Me.tbx_origen.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_origen.Name = "tbx_origen"
        Me.tbx_origen.Size = New System.Drawing.Size(264, 20)
        Me.tbx_origen.TabIndex = 10
        Me.tbx_origen.Text = "BDGEOCAT"
        '
        'tbx_destino
        '
        Me.tbx_destino.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_destino, 2)
        Me.tbx_destino.Enabled = False
        Me.tbx_destino.Location = New System.Drawing.Point(2, 71)
        Me.tbx_destino.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_destino.Name = "tbx_destino"
        Me.tbx_destino.Size = New System.Drawing.Size(264, 20)
        Me.tbx_destino.TabIndex = 11
        '
        'lbl_sg_filtrocapas
        '
        Me.lbl_sg_filtrocapas.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_sg_filtrocapas, 2)
        Me.lbl_sg_filtrocapas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_sg_filtrocapas.Location = New System.Drawing.Point(2, 179)
        Me.lbl_sg_filtrocapas.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_filtrocapas.Name = "lbl_sg_filtrocapas"
        Me.lbl_sg_filtrocapas.Size = New System.Drawing.Size(264, 13)
        Me.lbl_sg_filtrocapas.TabIndex = 7
        Me.lbl_sg_filtrocapas.Text = "Filtrar capas por:"
        '
        'lbl_sg_estdestino
        '
        Me.lbl_sg_estdestino.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_sg_estdestino, 2)
        Me.lbl_sg_estdestino.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_sg_estdestino.Location = New System.Drawing.Point(2, 107)
        Me.lbl_sg_estdestino.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_estdestino.Name = "lbl_sg_estdestino"
        Me.lbl_sg_estdestino.Size = New System.Drawing.Size(264, 13)
        Me.lbl_sg_estdestino.TabIndex = 12
        Me.lbl_sg_estdestino.Text = "Estructura del destino:"
        '
        'cbx_sg_filtrocapas
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_sg_filtrocapas, 3)
        Me.cbx_sg_filtrocapas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_sg_filtrocapas.Enabled = False
        Me.cbx_sg_filtrocapas.FormattingEnabled = True
        Me.cbx_sg_filtrocapas.Location = New System.Drawing.Point(2, 194)
        Me.cbx_sg_filtrocapas.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_sg_filtrocapas.Name = "cbx_sg_filtrocapas"
        Me.cbx_sg_filtrocapas.Size = New System.Drawing.Size(304, 21)
        Me.cbx_sg_filtrocapas.TabIndex = 8
        '
        'rbtn_sg_estds
        '
        Me.rbtn_sg_estds.AutoSize = True
        Me.rbtn_sg_estds.Checked = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.rbtn_sg_estds, 2)
        Me.rbtn_sg_estds.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rbtn_sg_estds.Location = New System.Drawing.Point(2, 125)
        Me.rbtn_sg_estds.Margin = New System.Windows.Forms.Padding(2)
        Me.rbtn_sg_estds.Name = "rbtn_sg_estds"
        Me.rbtn_sg_estds.Size = New System.Drawing.Size(264, 17)
        Me.rbtn_sg_estds.TabIndex = 13
        Me.rbtn_sg_estds.TabStop = True
        Me.rbtn_sg_estds.Text = "Mantener Datasets"
        Me.rbtn_sg_estds.UseVisualStyleBackColor = True
        '
        'rbtn_sg_estraiz
        '
        Me.rbtn_sg_estraiz.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.rbtn_sg_estraiz, 2)
        Me.rbtn_sg_estraiz.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rbtn_sg_estraiz.Location = New System.Drawing.Point(2, 149)
        Me.rbtn_sg_estraiz.Margin = New System.Windows.Forms.Padding(2)
        Me.rbtn_sg_estraiz.Name = "rbtn_sg_estraiz"
        Me.rbtn_sg_estraiz.Size = New System.Drawing.Size(264, 17)
        Me.rbtn_sg_estraiz.TabIndex = 14
        Me.rbtn_sg_estraiz.Text = "Enviar a la raiz"
        Me.rbtn_sg_estraiz.UseVisualStyleBackColor = True
        '
        'dg_sg_capas
        '
        Me.dg_sg_capas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel1.SetColumnSpan(Me.dg_sg_capas, 3)
        Me.dg_sg_capas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dg_sg_capas.Location = New System.Drawing.Point(2, 226)
        Me.dg_sg_capas.Margin = New System.Windows.Forms.Padding(2)
        Me.dg_sg_capas.Name = "dg_sg_capas"
        Me.dg_sg_capas.RowTemplate.Height = 24
        Me.dg_sg_capas.Size = New System.Drawing.Size(304, 222)
        Me.dg_sg_capas.TabIndex = 15
        '
        'btn_sg_enviar
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_sg_enviar, 3)
        Me.btn_sg_enviar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_sg_enviar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_sg_enviar.ImageKey = "GeodatabaseDistributedChangesSynchronize16.png"
        Me.btn_sg_enviar.ImageList = Me.ImageList1
        Me.btn_sg_enviar.Location = New System.Drawing.Point(2, 524)
        Me.btn_sg_enviar.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_sg_enviar.Name = "btn_sg_enviar"
        Me.btn_sg_enviar.Size = New System.Drawing.Size(304, 24)
        Me.btn_sg_enviar.TabIndex = 16
        Me.btn_sg_enviar.Text = "Enviar"
        Me.btn_sg_enviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_sg_enviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_sg_enviar.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "GeodatabaseConnectionAdd32.png")
        Me.ImageList1.Images.SetKeyName(1, "ExcelXLSFile16.png")
        Me.ImageList1.Images.SetKeyName(2, "EditCopy32.png")
        Me.ImageList1.Images.SetKeyName(3, "EditCopy16.png")
        Me.ImageList1.Images.SetKeyName(4, "GeodatabaseTiny_B_16.png")
        Me.ImageList1.Images.SetKeyName(5, "GeodatabaseDistributedReplicaTiny_B_16.png")
        Me.ImageList1.Images.SetKeyName(6, "GeodatabaseDistributedChangesSynchronize16.png")
        Me.ImageList1.Images.SetKeyName(7, "GeodatabaseDistributedReplicaTiny32.png")
        Me.ImageList1.Images.SetKeyName(8, "GeodatabaseConnectionAdd16.png")
        '
        'btn_sg_origen
        '
        Me.btn_sg_origen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_sg_origen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_sg_origen.ImageKey = "GeodatabaseConnectionAdd16.png"
        Me.btn_sg_origen.ImageList = Me.ImageList1
        Me.btn_sg_origen.Location = New System.Drawing.Point(270, 22)
        Me.btn_sg_origen.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_sg_origen.Name = "btn_sg_origen"
        Me.btn_sg_origen.Size = New System.Drawing.Size(36, 26)
        Me.btn_sg_origen.TabIndex = 6
        Me.btn_sg_origen.UseVisualStyleBackColor = True
        '
        'btn_sg_destino
        '
        Me.btn_sg_destino.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_sg_destino.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_sg_destino.ImageIndex = 8
        Me.btn_sg_destino.ImageList = Me.ImageList1
        Me.btn_sg_destino.Location = New System.Drawing.Point(270, 68)
        Me.btn_sg_destino.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_sg_destino.Name = "btn_sg_destino"
        Me.btn_sg_destino.Size = New System.Drawing.Size(36, 26)
        Me.btn_sg_destino.TabIndex = 3
        Me.btn_sg_destino.UseVisualStyleBackColor = True
        '
        'lbl_sg_legend
        '
        Me.lbl_sg_legend.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbl_sg_legend, 2)
        Me.lbl_sg_legend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_sg_legend.Location = New System.Drawing.Point(2, 454)
        Me.lbl_sg_legend.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend.Name = "lbl_sg_legend"
        Me.lbl_sg_legend.Size = New System.Drawing.Size(264, 16)
        Me.lbl_sg_legend.TabIndex = 17
        Me.lbl_sg_legend.Text = "Leyenda"
        '
        'lbl_sg_legend_s_nc
        '
        Me.lbl_sg_legend_s_nc.AutoSize = True
        Me.lbl_sg_legend_s_nc.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_s_nc.ForeColor = System.Drawing.Color.Green
        Me.lbl_sg_legend_s_nc.Location = New System.Drawing.Point(2, 470)
        Me.lbl_sg_legend_s_nc.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_s_nc.Name = "lbl_sg_legend_s_nc"
        Me.lbl_sg_legend_s_nc.Size = New System.Drawing.Size(40, 16)
        Me.lbl_sg_legend_s_nc.TabIndex = 18
        Me.lbl_sg_legend_s_nc.Text = "AaBbCc"
        '
        'lbl_sg_legend_s_nr
        '
        Me.lbl_sg_legend_s_nr.AutoSize = True
        Me.lbl_sg_legend_s_nr.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_s_nr.ForeColor = System.Drawing.Color.DarkOrange
        Me.lbl_sg_legend_s_nr.Location = New System.Drawing.Point(2, 486)
        Me.lbl_sg_legend_s_nr.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_s_nr.Name = "lbl_sg_legend_s_nr"
        Me.lbl_sg_legend_s_nr.Size = New System.Drawing.Size(40, 16)
        Me.lbl_sg_legend_s_nr.TabIndex = 19
        Me.lbl_sg_legend_s_nr.Text = "AaBbCc"
        '
        'lbl_sg_legend_s_ne
        '
        Me.lbl_sg_legend_s_ne.AutoSize = True
        Me.lbl_sg_legend_s_ne.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_s_ne.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lbl_sg_legend_s_ne.Location = New System.Drawing.Point(2, 502)
        Me.lbl_sg_legend_s_ne.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_s_ne.Name = "lbl_sg_legend_s_ne"
        Me.lbl_sg_legend_s_ne.Size = New System.Drawing.Size(40, 16)
        Me.lbl_sg_legend_s_ne.TabIndex = 20
        Me.lbl_sg_legend_s_ne.Text = "AaBbCc"
        '
        'lbl_sg_legend_nc
        '
        Me.lbl_sg_legend_nc.AutoSize = True
        Me.lbl_sg_legend_nc.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_nc.Location = New System.Drawing.Point(47, 470)
        Me.lbl_sg_legend_nc.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_nc.Name = "lbl_sg_legend_nc"
        Me.lbl_sg_legend_nc.Size = New System.Drawing.Size(184, 13)
        Me.lbl_sg_legend_nc.TabIndex = 21
        Me.lbl_sg_legend_nc.Text = "Nuevo elemento(no existe en destino)"
        '
        'lbl_sg_legend_nr
        '
        Me.lbl_sg_legend_nr.AutoSize = True
        Me.lbl_sg_legend_nr.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_nr.Location = New System.Drawing.Point(47, 486)
        Me.lbl_sg_legend_nr.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_nr.Name = "lbl_sg_legend_nr"
        Me.lbl_sg_legend_nr.Size = New System.Drawing.Size(193, 13)
        Me.lbl_sg_legend_nr.TabIndex = 22
        Me.lbl_sg_legend_nr.Text = "Registros nuevos por agregar al destino"
        '
        'lbl_sg_legend_ne
        '
        Me.lbl_sg_legend_ne.AutoSize = True
        Me.lbl_sg_legend_ne.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.8!)
        Me.lbl_sg_legend_ne.Location = New System.Drawing.Point(47, 502)
        Me.lbl_sg_legend_ne.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_sg_legend_ne.Name = "lbl_sg_legend_ne"
        Me.lbl_sg_legend_ne.Size = New System.Drawing.Size(154, 13)
        Me.lbl_sg_legend_ne.TabIndex = 23
        Me.lbl_sg_legend_ne.Text = "Elemento no existe en el origen"
        '
        'Form_sincronizacion_geodatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 550)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_sincronizacion_geodatabase"
        Me.Text = "Form_sincronizacion_geodatabase"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dg_sg_capas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_sg_origen As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_destino As System.Windows.Forms.Label
    Friend WithEvents btn_sg_destino As System.Windows.Forms.Button
    Friend WithEvents btn_sg_origen As System.Windows.Forms.Button
    Friend WithEvents lbl_sg_filtrocapas As System.Windows.Forms.Label
    Friend WithEvents cbx_sg_filtrocapas As System.Windows.Forms.ComboBox
    Friend WithEvents tbx_origen As System.Windows.Forms.TextBox
    Friend WithEvents tbx_destino As System.Windows.Forms.TextBox
    Friend WithEvents lbl_sg_estdestino As System.Windows.Forms.Label
    Friend WithEvents rbtn_sg_estds As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_sg_estraiz As System.Windows.Forms.RadioButton
    Friend WithEvents dg_sg_capas As System.Windows.Forms.DataGridView
    Friend WithEvents btn_sg_enviar As System.Windows.Forms.Button
    Friend WithEvents lbl_sg_legend As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_s_nc As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_s_nr As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_s_ne As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_nc As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_nr As System.Windows.Forms.Label
    Friend WithEvents lbl_sg_legend_ne As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
