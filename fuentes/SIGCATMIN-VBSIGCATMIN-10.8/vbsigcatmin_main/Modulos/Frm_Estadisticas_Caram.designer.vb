<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Estadisticas_Caram
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Estadisticas_Caram))
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.lbl_nombre1 = New System.Windows.Forms.Label()
        Me.cbotipo = New System.Windows.Forms.ComboBox()
        Me.cbodetalle = New System.Windows.Forms.ComboBox()
        Me.lblregistro = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.lblZona = New System.Windows.Forms.Label()
        Me.btncalcular = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbotiporesemin = New System.Windows.Forms.ComboBox()
        Me.cbodatum = New System.Windows.Forms.ComboBox()
        Me.lblregion = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbotiporese = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnbaachero = New System.Windows.Forms.Button()
        Me.btnreporte = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btn_mineriasi = New System.Windows.Forms.Button()
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.txtPorcentaje = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtArea2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.txtArea1 = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btncapas = New System.Windows.Forms.Button()
        Me.btnneto = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtmar = New System.Windows.Forms.TextBox()
        Me.txttierra = New System.Windows.Forms.TextBox()
        Me.btnexceltotal = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(2, 62)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(77, 17)
        Me.chkEstado.TabIndex = 246
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'lbl_nombre1
        '
        Me.lbl_nombre1.AutoSize = True
        Me.lbl_nombre1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nombre1.Location = New System.Drawing.Point(7, 27)
        Me.lbl_nombre1.Name = "lbl_nombre1"
        Me.lbl_nombre1.Size = New System.Drawing.Size(108, 13)
        Me.lbl_nombre1.TabIndex = 248
        Me.lbl_nombre1.Text = "Seleccione el tipo:"
        '
        'cbotipo
        '
        Me.cbotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotipo.FormattingEnabled = True
        Me.cbotipo.Items.AddRange(New Object() {"-- Selec. --", "MINERIA A NIVEL NACIONAL", "PERU CONTINENTAL"})
        Me.cbotipo.Location = New System.Drawing.Point(124, 25)
        Me.cbotipo.Name = "cbotipo"
        Me.cbotipo.Size = New System.Drawing.Size(183, 21)
        Me.cbotipo.TabIndex = 247
        '
        'cbodetalle
        '
        Me.cbodetalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodetalle.FormattingEnabled = True
        Me.cbodetalle.Items.AddRange(New Object() {"-- Selec. --"})
        Me.cbodetalle.Location = New System.Drawing.Point(127, 127)
        Me.cbodetalle.Name = "cbodetalle"
        Me.cbodetalle.Size = New System.Drawing.Size(180, 21)
        Me.cbodetalle.TabIndex = 249
        '
        'lblregistro
        '
        Me.lblregistro.AutoSize = True
        Me.lblregistro.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblregistro.ForeColor = System.Drawing.Color.Red
        Me.lblregistro.Location = New System.Drawing.Point(34, 148)
        Me.lblregistro.Name = "lblregistro"
        Me.lblregistro.Size = New System.Drawing.Size(0, 13)
        Me.lblregistro.TabIndex = 250
        Me.lblregistro.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboZona
        '
        Me.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(556, 129)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(103, 21)
        Me.cboZona.TabIndex = 251
        '
        'lblZona
        '
        Me.lblZona.AutoSize = True
        Me.lblZona.Location = New System.Drawing.Point(420, 135)
        Me.lblZona.Name = "lblZona"
        Me.lblZona.Size = New System.Drawing.Size(85, 13)
        Me.lblZona.TabIndex = 252
        Me.lblZona.Text = "Seleccion Zona:"
        '
        'btncalcular
        '
        Me.btncalcular.BackColor = System.Drawing.SystemColors.Control
        Me.btncalcular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncalcular.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btncalcular.Location = New System.Drawing.Point(413, 15)
        Me.btncalcular.Name = "btncalcular"
        Me.btncalcular.Size = New System.Drawing.Size(104, 23)
        Me.btncalcular.TabIndex = 253
        Me.btncalcular.Text = "MINERIA NO"
        Me.btncalcular.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbotiporesemin)
        Me.GroupBox1.Controls.Add(Me.cbodatum)
        Me.GroupBox1.Controls.Add(Me.lblregion)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbotiporese)
        Me.GroupBox1.Controls.Add(Me.lbl_nombre1)
        Me.GroupBox1.Controls.Add(Me.cbotipo)
        Me.GroupBox1.Controls.Add(Me.cbodetalle)
        Me.GroupBox1.Controls.Add(Me.cboZona)
        Me.GroupBox1.Controls.Add(Me.lblZona)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 91)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(681, 170)
        Me.GroupBox1.TabIndex = 254
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Busqueda Por Pais/Dpto/Tipo Reserva"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(491, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(126, 13)
        Me.Label7.TabIndex = 269
        Me.Label7.Text = "Tipos para Mineria SI"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(89, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 13)
        Me.Label6.TabIndex = 268
        Me.Label6.Text = "Tipos para Mineria NO"
        '
        'cbotiporesemin
        '
        Me.cbotiporesemin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotiporesemin.FormattingEnabled = True
        Me.cbotiporesemin.Location = New System.Drawing.Point(354, 85)
        Me.cbotiporesemin.Name = "cbotiporesemin"
        Me.cbotiporesemin.Size = New System.Drawing.Size(305, 21)
        Me.cbotiporesemin.TabIndex = 267
        '
        'cbodatum
        '
        Me.cbodatum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodatum.FormattingEnabled = True
        Me.cbodatum.Items.AddRange(New Object() {"WGS_84", "PASAD_56"})
        Me.cbodatum.Location = New System.Drawing.Point(543, 27)
        Me.cbodatum.Name = "cbodatum"
        Me.cbodatum.Size = New System.Drawing.Size(116, 21)
        Me.cbodatum.TabIndex = 266
        '
        'lblregion
        '
        Me.lblregion.AutoSize = True
        Me.lblregion.BackColor = System.Drawing.Color.White
        Me.lblregion.Location = New System.Drawing.Point(13, 130)
        Me.lblregion.Name = "lblregion"
        Me.lblregion.Size = New System.Drawing.Size(103, 13)
        Me.lblregion.TabIndex = 262
        Me.lblregion.Text = "Seleccione Region :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(437, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 265
        Me.Label4.Text = "Seleccion Datum:"
        '
        'cbotiporese
        '
        Me.cbotiporese.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotiporese.FormattingEnabled = True
        Me.cbotiporese.Items.AddRange(New Object() {"-- Selec. --"})
        Me.cbotiporese.Location = New System.Drawing.Point(10, 85)
        Me.cbotiporese.Name = "cbotiporese"
        Me.cbotiporese.Size = New System.Drawing.Size(307, 21)
        Me.cbotiporese.TabIndex = 250
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnbaachero)
        Me.GroupBox2.Controls.Add(Me.btnreporte)
        Me.GroupBox2.Controls.Add(Me.btnExcel)
        Me.GroupBox2.Controls.Add(Me.chkEstado)
        Me.GroupBox2.Controls.Add(Me.btn_mineriasi)
        Me.GroupBox2.Controls.Add(Me.btnCargar)
        Me.GroupBox2.Controls.Add(Me.btncalcular)
        Me.GroupBox2.Controls.Add(Me.btnCerrar)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox2.Location = New System.Drawing.Point(21, 482)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(655, 85)
        Me.GroupBox2.TabIndex = 255
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Cálculo por tipo de áreas"
        '
        'btnbaachero
        '
        Me.btnbaachero.BackColor = System.Drawing.SystemColors.Control
        Me.btnbaachero.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbaachero.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnbaachero.Location = New System.Drawing.Point(413, 50)
        Me.btnbaachero.Name = "btnbaachero"
        Me.btnbaachero.Size = New System.Drawing.Size(95, 23)
        Me.btnbaachero.TabIndex = 263
        Me.btnbaachero.Text = "BACH TOTAL"
        Me.btnbaachero.UseVisualStyleBackColor = False
        '
        'btnreporte
        '
        Me.btnreporte.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnreporte.Image = CType(resources.GetObject("btnreporte.Image"), System.Drawing.Image)
        Me.btnreporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnreporte.Location = New System.Drawing.Point(244, 46)
        Me.btnreporte.Name = "btnreporte"
        Me.btnreporte.Size = New System.Drawing.Size(104, 26)
        Me.btnreporte.TabIndex = 262
        Me.btnreporte.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.BackColor = System.Drawing.SystemColors.Control
        Me.btnExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnExcel.Location = New System.Drawing.Point(546, 49)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(95, 23)
        Me.btnExcel.TabIndex = 257
        Me.btnExcel.Text = "TOTAL"
        Me.btnExcel.UseVisualStyleBackColor = False
        '
        'btn_mineriasi
        '
        Me.btn_mineriasi.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_mineriasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_mineriasi.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btn_mineriasi.Location = New System.Drawing.Point(546, 15)
        Me.btn_mineriasi.Name = "btn_mineriasi"
        Me.btn_mineriasi.Size = New System.Drawing.Size(95, 23)
        Me.btn_mineriasi.TabIndex = 256
        Me.btn_mineriasi.Text = "MINERIA SI"
        Me.btn_mineriasi.UseVisualStyleBackColor = False
        '
        'btnCargar
        '
        Me.btnCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargar.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnCargar.Location = New System.Drawing.Point(244, 17)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(131, 26)
        Me.btnCargar.TabIndex = 255
        Me.btnCargar.Text = "Cargar Información"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(93, 30)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(99, 26)
        Me.btnCerrar.TabIndex = 177
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.Location = New System.Drawing.Point(592, 19)
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.Size = New System.Drawing.Size(69, 20)
        Me.txtPorcentaje.TabIndex = 261
        Me.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(490, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 260
        Me.Label5.Text = "% ARES:"
        '
        'txtArea2
        '
        Me.txtArea2.Location = New System.Drawing.Point(375, 16)
        Me.txtArea2.Name = "txtArea2"
        Me.txtArea2.Size = New System.Drawing.Size(87, 20)
        Me.txtArea2.TabIndex = 259
        Me.txtArea2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(11, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 256
        Me.Label2.Text = "Area Total (Ha):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(240, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 257
        Me.Label3.Text = "Area Total ARES (Ha):"
        '
        'dgdDetalle
        '
        Me.dgdDetalle.AllowArrows = False
        Me.dgdDetalle.AllowColMove = False
        Me.dgdDetalle.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgdDetalle.AllowSort = False
        Me.dgdDetalle.AllowUpdate = False
        Me.dgdDetalle.AlternatingRows = True
        Me.dgdDetalle.CaptionHeight = 17
        Me.dgdDetalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdDetalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdDetalle.Images.Add(CType(resources.GetObject("dgdDetalle.Images"), System.Drawing.Image))
        Me.dgdDetalle.Location = New System.Drawing.Point(11, 251)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(675, 324)
        Me.dgdDetalle.TabIndex = 174
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(5, 12)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(681, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 173
        Me.imgMenu.TabStop = False
        '
        'txtArea1
        '
        Me.txtArea1.Location = New System.Drawing.Point(99, 16)
        Me.txtArea1.Name = "txtArea1"
        Me.txtArea1.Size = New System.Drawing.Size(96, 20)
        Me.txtArea1.TabIndex = 258
        Me.txtArea1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btncapas)
        Me.GroupBox3.Controls.Add(Me.btnneto)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtmar)
        Me.GroupBox3.Controls.Add(Me.txttierra)
        Me.GroupBox3.Controls.Add(Me.btnexceltotal)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox3.Location = New System.Drawing.Point(15, 631)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(674, 70)
        Me.GroupBox3.TabIndex = 264
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cálculo en Área Continental y Mar"
        '
        'btncapas
        '
        Me.btncapas.BackColor = System.Drawing.SystemColors.Control
        Me.btncapas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncapas.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btncapas.Location = New System.Drawing.Point(566, 29)
        Me.btncapas.Name = "btncapas"
        Me.btncapas.Size = New System.Drawing.Size(95, 25)
        Me.btncapas.TabIndex = 264
        Me.btncapas.Text = "VER CAPAS"
        Me.btncapas.UseVisualStyleBackColor = False
        '
        'btnneto
        '
        Me.btnneto.BackColor = System.Drawing.SystemColors.Control
        Me.btnneto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnneto.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnneto.Location = New System.Drawing.Point(11, 32)
        Me.btnneto.Name = "btnneto"
        Me.btnneto.Size = New System.Drawing.Size(97, 23)
        Me.btnneto.TabIndex = 270
        Me.btnneto.Text = "MAR"
        Me.btnneto.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(263, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 269
        Me.Label8.Text = "Area Continental:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(114, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 268
        Me.Label1.Text = "Ärea Mar:"
        '
        'txtmar
        '
        Me.txtmar.Location = New System.Drawing.Point(171, 34)
        Me.txtmar.Name = "txtmar"
        Me.txtmar.Size = New System.Drawing.Size(86, 20)
        Me.txtmar.TabIndex = 267
        Me.txtmar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txttierra
        '
        Me.txttierra.Location = New System.Drawing.Point(357, 32)
        Me.txttierra.Name = "txttierra"
        Me.txttierra.Size = New System.Drawing.Size(87, 20)
        Me.txttierra.TabIndex = 265
        Me.txttierra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnexceltotal
        '
        Me.btnexceltotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnexceltotal.Image = CType(resources.GetObject("btnexceltotal.Image"), System.Drawing.Image)
        Me.btnexceltotal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnexceltotal.Location = New System.Drawing.Point(450, 29)
        Me.btnexceltotal.Name = "btnexceltotal"
        Me.btnexceltotal.Size = New System.Drawing.Size(104, 26)
        Me.btnexceltotal.TabIndex = 262
        Me.btnexceltotal.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtPorcentaje)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtArea1)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtArea2)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox4.Location = New System.Drawing.Point(15, 581)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(671, 50)
        Me.GroupBox4.TabIndex = 264
        Me.GroupBox4.TabStop = False
        '
        'Frm_Estadisticas_Caram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 722)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblregistro)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Controls.Add(Me.imgMenu)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Name = "Frm_Estadisticas_Caram"
        Me.Text = "ESTADISTICAS DE AREAS RESTRINGIDAS A LA ACTIVIDAD MINERA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_nombre1 As System.Windows.Forms.Label
    Friend WithEvents cbotipo As System.Windows.Forms.ComboBox
    Friend WithEvents cbodetalle As System.Windows.Forms.ComboBox
    Friend WithEvents lblregistro As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents lblZona As System.Windows.Forms.Label
    Friend WithEvents btncalcular As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPorcentaje As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtArea2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents cbotiporese As System.Windows.Forms.ComboBox
    Friend WithEvents txtArea1 As System.Windows.Forms.TextBox
    Friend WithEvents lblregion As System.Windows.Forms.Label
    Friend WithEvents cbodatum As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbotiporesemin As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_mineriasi As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnreporte As System.Windows.Forms.Button
    Friend WithEvents btnbaachero As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtmar As System.Windows.Forms.TextBox
    Friend WithEvents txttierra As System.Windows.Forms.TextBox
    Friend WithEvents btnexceltotal As System.Windows.Forms.Button
    Friend WithEvents btnneto As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btncapas As System.Windows.Forms.Button
End Class
