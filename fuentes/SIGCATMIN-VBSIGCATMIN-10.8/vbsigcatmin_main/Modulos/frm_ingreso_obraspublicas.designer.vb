<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ingreso_obraspublicas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ingreso_obraspublicas))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.imgMenu = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btn_ingresar = New System.Windows.Forms.Button()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.btn_exporta = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgdDetalle)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.GroupBox1.Location = New System.Drawing.Point(12, 87)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(552, 289)
        Me.GroupBox1.TabIndex = 264
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Ingreso de Obras Públicas :"
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
        Me.dgdDetalle.Location = New System.Drawing.Point(10, 19)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(536, 264)
        Me.dgdDetalle.TabIndex = 256
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'imgMenu
        '
        Me.imgMenu.Image = CType(resources.GetObject("imgMenu.Image"), System.Drawing.Image)
        Me.imgMenu.Location = New System.Drawing.Point(24, 10)
        Me.imgMenu.Name = "imgMenu"
        Me.imgMenu.Size = New System.Drawing.Size(540, 71)
        Me.imgMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgMenu.TabIndex = 265
        Me.imgMenu.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_exporta)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.btn_ingresar)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 382)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(552, 59)
        Me.GroupBox2.TabIndex = 258
        Me.GroupBox2.TabStop = False
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.Location = New System.Drawing.Point(28, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(104, 32)
        Me.Button2.TabIndex = 258
        '
        'btn_ingresar
        '
        Me.btn_ingresar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ingresar.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btn_ingresar.Location = New System.Drawing.Point(411, 19)
        Me.btn_ingresar.Name = "btn_ingresar"
        Me.btn_ingresar.Size = New System.Drawing.Size(119, 32)
        Me.btn_ingresar.TabIndex = 257
        Me.btn_ingresar.Text = "Ingresar"
        Me.btn_ingresar.UseVisualStyleBackColor = True
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(248, 456)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(77, 17)
        Me.chkEstado.TabIndex = 266
        Me.chkEstado.Text = "chkEstado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'btn_exporta
        '
        Me.btn_exporta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_exporta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btn_exporta.Location = New System.Drawing.Point(208, 19)
        Me.btn_exporta.Name = "btn_exporta"
        Me.btn_exporta.Size = New System.Drawing.Size(119, 32)
        Me.btn_exporta.TabIndex = 267
        Me.btn_exporta.Text = "Exportar Capas"
        Me.btn_exporta.UseVisualStyleBackColor = True
        '
        'frm_ingreso_obraspublicas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 461)
        Me.Controls.Add(Me.chkEstado)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.imgMenu)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_ingreso_obraspublicas"
        Me.Text = "frm_ingreso_obraspublicas"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents imgMenu As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btn_ingresar As System.Windows.Forms.Button
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents btn_exporta As System.Windows.Forms.Button
End Class
