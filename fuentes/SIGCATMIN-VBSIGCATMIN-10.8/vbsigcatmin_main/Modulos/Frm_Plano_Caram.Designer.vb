<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Plano_Caram
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Plano_Caram))
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.txt_codigo = New System.Windows.Forms.TextBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.lblConsultar = New System.Windows.Forms.Label()
        Me.dtpFecha1 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_lista = New System.Windows.Forms.Label()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnIndividual = New System.Windows.Forms.Button()
        Me.btndeseleccion = New System.Windows.Forms.Button()
        Me.btnseleccionar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(12, 12)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(540, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 170
        Me.imgMenu.TabStop = False
        '
        'txt_codigo
        '
        Me.txt_codigo.Location = New System.Drawing.Point(132, 114)
        Me.txt_codigo.Name = "txt_codigo"
        Me.txt_codigo.Size = New System.Drawing.Size(150, 20)
        Me.txt_codigo.TabIndex = 268
        '
        'btnBuscar
        '
        Me.btnBuscar.Image = CType(resources.GetObject("btnBuscar.Image"), System.Drawing.Image)
        Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBuscar.Location = New System.Drawing.Point(310, 113)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(92, 26)
        Me.btnBuscar.TabIndex = 267
        '
        'lblConsultar
        '
        Me.lblConsultar.AutoSize = True
        Me.lblConsultar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsultar.Location = New System.Drawing.Point(23, 117)
        Me.lblConsultar.Name = "lblConsultar"
        Me.lblConsultar.Size = New System.Drawing.Size(67, 13)
        Me.lblConsultar.TabIndex = 266
        Me.lblConsultar.Text = "Consultar :"
        '
        'dtpFecha1
        '
        Me.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha1.Location = New System.Drawing.Point(447, 114)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(96, 20)
        Me.dtpFecha1.TabIndex = 265
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_lista)
        Me.GroupBox1.Controls.Add(Me.dgdDetalle)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.GroupBox1.Location = New System.Drawing.Point(12, 164)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(540, 238)
        Me.GroupBox1.TabIndex = 269
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Listado de registros ingresados :"
        '
        'lbl_lista
        '
        Me.lbl_lista.AutoSize = True
        Me.lbl_lista.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_lista.Location = New System.Drawing.Point(160, 0)
        Me.lbl_lista.Name = "lbl_lista"
        Me.lbl_lista.Size = New System.Drawing.Size(11, 16)
        Me.lbl_lista.TabIndex = 257
        Me.lbl_lista.Text = " "
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
        Me.dgdDetalle.Location = New System.Drawing.Point(5, 19)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(524, 206)
        Me.dgdDetalle.TabIndex = 256
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnIndividual)
        Me.GroupBox2.Controls.Add(Me.btndeseleccion)
        Me.GroupBox2.Controls.Add(Me.btnseleccionar)
        Me.GroupBox2.Controls.Add(Me.btnCerrar)
        Me.GroupBox2.Controls.Add(Me.btnProcesar)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 419)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(534, 101)
        Me.GroupBox2.TabIndex = 270
        Me.GroupBox2.TabStop = False
        '
        'btnIndividual
        '
        Me.btnIndividual.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIndividual.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnIndividual.Location = New System.Drawing.Point(274, 10)
        Me.btnIndividual.Name = "btnIndividual"
        Me.btnIndividual.Size = New System.Drawing.Size(113, 32)
        Me.btnIndividual.TabIndex = 261
        Me.btnIndividual.Text = "P. Individual"
        Me.btnIndividual.UseVisualStyleBackColor = True
        '
        'btndeseleccion
        '
        Me.btndeseleccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeseleccion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btndeseleccion.Location = New System.Drawing.Point(274, 63)
        Me.btndeseleccion.Name = "btndeseleccion"
        Me.btndeseleccion.Size = New System.Drawing.Size(113, 32)
        Me.btndeseleccion.TabIndex = 261
        Me.btndeseleccion.Text = "Deseleccionar"
        Me.btndeseleccion.UseVisualStyleBackColor = True
        '
        'btnseleccionar
        '
        Me.btnseleccionar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnseleccionar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnseleccionar.Location = New System.Drawing.Point(411, 63)
        Me.btnseleccionar.Name = "btnseleccionar"
        Me.btnseleccionar.Size = New System.Drawing.Size(113, 32)
        Me.btnseleccionar.TabIndex = 260
        Me.btnseleccionar.Text = "Seleccionar todo"
        Me.btnseleccionar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(6, 29)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(113, 32)
        Me.btnCerrar.TabIndex = 258
        '
        'btnProcesar
        '
        Me.btnProcesar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcesar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnProcesar.Location = New System.Drawing.Point(411, 10)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(113, 32)
        Me.btnProcesar.TabIndex = 257
        Me.btnProcesar.Text = "P. Masivo"
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(60, 514)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(77, 17)
        Me.chkEstado.TabIndex = 271
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'Frm_Plano_Caram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 554)
        Me.Controls.Add(Me.chkEstado)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txt_codigo)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.lblConsultar)
        Me.Controls.Add(Me.dtpFecha1)
        Me.Controls.Add(Me.imgMenu)
        Me.Name = "Frm_Plano_Caram"
        Me.Text = "Frm_Plano_Caram"
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents txt_codigo As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents lblConsultar As System.Windows.Forms.Label
    Friend WithEvents dtpFecha1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_lista As System.Windows.Forms.Label
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnIndividual As System.Windows.Forms.Button
    Friend WithEvents btndeseleccion As System.Windows.Forms.Button
    Friend WithEvents btnseleccionar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
End Class
