<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EstadisticasMineria
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_EstadisticasMineria))
        Me.btnCargar = New System.Windows.Forms.Button()
        Me.btncalcular = New System.Windows.Forms.Button()
        Me.cbotiporese = New System.Windows.Forms.ComboBox()
        Me.cbotipo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_nombre1 = New System.Windows.Forms.Label()
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCargar
        '
        Me.btnCargar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargar.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnCargar.Location = New System.Drawing.Point(196, 365)
        Me.btnCargar.Name = "btnCargar"
        Me.btnCargar.Size = New System.Drawing.Size(131, 26)
        Me.btnCargar.TabIndex = 258
        Me.btnCargar.Text = "Cargar Información"
        Me.btnCargar.UseVisualStyleBackColor = True
        '
        'btncalcular
        '
        Me.btncalcular.BackColor = System.Drawing.SystemColors.Control
        Me.btncalcular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncalcular.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btncalcular.Location = New System.Drawing.Point(549, 368)
        Me.btncalcular.Name = "btncalcular"
        Me.btncalcular.Size = New System.Drawing.Size(95, 26)
        Me.btncalcular.TabIndex = 260
        Me.btncalcular.Text = "CALCULAR"
        Me.btncalcular.UseVisualStyleBackColor = False
        '
        'cbotiporese
        '
        Me.cbotiporese.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotiporese.FormattingEnabled = True
        Me.cbotiporese.Items.AddRange(New Object() {"-- Selec. --"})
        Me.cbotiporese.Location = New System.Drawing.Point(390, 64)
        Me.cbotiporese.Name = "cbotiporese"
        Me.cbotiporese.Size = New System.Drawing.Size(263, 21)
        Me.cbotiporese.TabIndex = 262
        '
        'cbotipo
        '
        Me.cbotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotipo.FormattingEnabled = True
        Me.cbotipo.Items.AddRange(New Object() {"-- Selec. --", "SEGUN DEPARTAMENTO", "A NIVEL NACIONAL"})
        Me.cbotipo.Location = New System.Drawing.Point(471, 15)
        Me.cbotipo.Name = "cbotipo"
        Me.cbotipo.Size = New System.Drawing.Size(172, 21)
        Me.cbotipo.TabIndex = 261
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbotipo)
        Me.GroupBox1.Controls.Add(Me.cbotiporese)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lbl_nombre1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 78)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(659, 110)
        Me.GroupBox1.TabIndex = 263
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Estdiaticas Mineria SI/NO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 265
        Me.Label4.Text = "Tipos s realizarse:"
        '
        'lbl_nombre1
        '
        Me.lbl_nombre1.AutoSize = True
        Me.lbl_nombre1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nombre1.Location = New System.Drawing.Point(19, 23)
        Me.lbl_nombre1.Name = "lbl_nombre1"
        Me.lbl_nombre1.Size = New System.Drawing.Size(115, 13)
        Me.lbl_nombre1.TabIndex = 248
        Me.lbl_nombre1.Text = "Seleccione el caso::"
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(12, 1)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(659, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 264
        Me.imgMenu.TabStop = False
        '
        'btnExcel
        '
        Me.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.Location = New System.Drawing.Point(391, 368)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(104, 26)
        Me.btnExcel.TabIndex = 257
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCerrar.Location = New System.Drawing.Point(39, 365)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(99, 26)
        Me.btnCerrar.TabIndex = 256
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
        Me.dgdDetalle.Location = New System.Drawing.Point(12, 194)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(659, 219)
        Me.dgdDetalle.TabIndex = 259
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(69, 407)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(77, 17)
        Me.chkEstado.TabIndex = 265
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'Frm_EstadisticasMineria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 419)
        Me.Controls.Add(Me.chkEstado)
        Me.Controls.Add(Me.imgMenu)
        Me.Controls.Add(Me.btncalcular)
        Me.Controls.Add(Me.btnCargar)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Frm_EstadisticasMineria"
        Me.Text = "Frm_EstadisticasMineria"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCargar As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btncalcular As System.Windows.Forms.Button
    Friend WithEvents cbotiporese As System.Windows.Forms.ComboBox
    Friend WithEvents cbotipo As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbl_nombre1 As System.Windows.Forms.Label
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
End Class
