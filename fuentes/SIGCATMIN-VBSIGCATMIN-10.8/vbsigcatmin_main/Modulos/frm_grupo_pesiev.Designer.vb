<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_grupo_pesiev
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_grupo_pesiev))
        Me.dgdDetalle = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.btn_cerrar = New System.Windows.Forms.Button()
        Me.btn_Graficar = New System.Windows.Forms.Button()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.txtExiste = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstDM = New System.Windows.Forms.ListBox()
        Me.lblDMSimul = New System.Windows.Forms.Label()
        Me.lblCodRemate = New System.Windows.Forms.Label()
        Me.lblArea = New System.Windows.Forms.Label()
        Me.txtArea = New System.Windows.Forms.TextBox()
        Me.txtCodRemate = New System.Windows.Forms.TextBox()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.btnModificar = New System.Windows.Forms.Button()
        Me.dgvCodRemate = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvCodRemate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.dgdDetalle.Font = New System.Drawing.Font("Tahoma", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdDetalle.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.dgdDetalle.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgdDetalle.Images.Add(CType(resources.GetObject("dgdDetalle.Images"), System.Drawing.Image))
        Me.dgdDetalle.Location = New System.Drawing.Point(12, 12)
        Me.dgdDetalle.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgdDetalle.Name = "dgdDetalle"
        Me.dgdDetalle.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgdDetalle.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgdDetalle.PreviewInfo.ZoomFactor = 75.0R
        Me.dgdDetalle.PrintInfo.PageSettings = CType(resources.GetObject("dgdDetalle.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgdDetalle.RowHeight = 15
        Me.dgdDetalle.Size = New System.Drawing.Size(624, 139)
        Me.dgdDetalle.TabIndex = 137
        Me.dgdDetalle.Text = "C1TrueDBGrid1"
        Me.dgdDetalle.PropBag = resources.GetString("dgdDetalle.PropBag")
        '
        'btn_cerrar
        '
        Me.btn_cerrar.Location = New System.Drawing.Point(12, 159)
        Me.btn_cerrar.Name = "btn_cerrar"
        Me.btn_cerrar.Size = New System.Drawing.Size(140, 23)
        Me.btn_cerrar.TabIndex = 138
        Me.btn_cerrar.Text = "Cerrar"
        Me.btn_cerrar.UseVisualStyleBackColor = True
        '
        'btn_Graficar
        '
        Me.btn_Graficar.Location = New System.Drawing.Point(495, 159)
        Me.btn_Graficar.Name = "btn_Graficar"
        Me.btn_Graficar.Size = New System.Drawing.Size(141, 23)
        Me.btn_Graficar.TabIndex = 139
        Me.btn_Graficar.Text = "Graficar"
        Me.btn_Graficar.UseVisualStyleBackColor = True
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Location = New System.Drawing.Point(215, 159)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(59, 17)
        Me.chkEstado.TabIndex = 140
        Me.chkEstado.Text = "Estado"
        Me.chkEstado.UseVisualStyleBackColor = True
        Me.chkEstado.Visible = False
        '
        'txtExiste
        '
        Me.txtExiste.Location = New System.Drawing.Point(173, 157)
        Me.txtExiste.Name = "txtExiste"
        Me.txtExiste.Size = New System.Drawing.Size(36, 20)
        Me.txtExiste.TabIndex = 161
        Me.txtExiste.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstDM)
        Me.GroupBox1.Controls.Add(Me.lblDMSimul)
        Me.GroupBox1.Controls.Add(Me.lblCodRemate)
        Me.GroupBox1.Controls.Add(Me.lblArea)
        Me.GroupBox1.Controls.Add(Me.txtArea)
        Me.GroupBox1.Controls.Add(Me.txtCodRemate)
        Me.GroupBox1.Location = New System.Drawing.Point(446, 201)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(203, 139)
        Me.GroupBox1.TabIndex = 163
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Agregar Código de Remate"
        '
        'lstDM
        '
        Me.lstDM.FormattingEnabled = True
        Me.lstDM.Location = New System.Drawing.Point(99, 16)
        Me.lstDM.Name = "lstDM"
        Me.lstDM.Size = New System.Drawing.Size(91, 56)
        Me.lstDM.TabIndex = 5
        '
        'lblDMSimul
        '
        Me.lblDMSimul.AutoSize = True
        Me.lblDMSimul.Location = New System.Drawing.Point(6, 37)
        Me.lblDMSimul.Name = "lblDMSimul"
        Me.lblDMSimul.Size = New System.Drawing.Size(90, 13)
        Me.lblDMSimul.TabIndex = 4
        Me.lblDMSimul.Text = "DM Simultaneos :"
        '
        'lblCodRemate
        '
        Me.lblCodRemate.AutoSize = True
        Me.lblCodRemate.Location = New System.Drawing.Point(6, 108)
        Me.lblCodRemate.Name = "lblCodRemate"
        Me.lblCodRemate.Size = New System.Drawing.Size(81, 13)
        Me.lblCodRemate.TabIndex = 3
        Me.lblCodRemate.Text = "Código remate :"
        '
        'lblArea
        '
        Me.lblArea.AutoSize = True
        Me.lblArea.Location = New System.Drawing.Point(6, 82)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(56, 13)
        Me.lblArea.TabIndex = 2
        Me.lblArea.Text = "Área (ha) :"
        '
        'txtArea
        '
        Me.txtArea.Location = New System.Drawing.Point(99, 79)
        Me.txtArea.Name = "txtArea"
        Me.txtArea.Size = New System.Drawing.Size(92, 20)
        Me.txtArea.TabIndex = 1
        '
        'txtCodRemate
        '
        Me.txtCodRemate.Location = New System.Drawing.Point(99, 105)
        Me.txtCodRemate.Name = "txtCodRemate"
        Me.txtCodRemate.Size = New System.Drawing.Size(92, 20)
        Me.txtCodRemate.TabIndex = 0
        '
        'btnGrabar
        '
        Me.btnGrabar.Location = New System.Drawing.Point(495, 346)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(142, 24)
        Me.btnGrabar.TabIndex = 164
        Me.btnGrabar.Text = "Grabar Código Remate"
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnModificar.BackgroundImage = Global.SIGCATMIN.My.Resources.Resources.EditingSketchTool16
        Me.btnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.FlatAppearance.BorderSize = 0
        Me.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModificar.Location = New System.Drawing.Point(455, 346)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(21, 22)
        Me.btnModificar.TabIndex = 165
        Me.btnModificar.UseVisualStyleBackColor = False
        '
        'dgvCodRemate
        '
        Me.dgvCodRemate.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvCodRemate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCodRemate.Location = New System.Drawing.Point(12, 201)
        Me.dgvCodRemate.Name = "dgvCodRemate"
        Me.dgvCodRemate.Size = New System.Drawing.Size(428, 139)
        Me.dgvCodRemate.TabIndex = 166
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(333, 346)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 12)
        Me.Label1.TabIndex = 168
        Me.Label1.Text = "Click en botón rojo (lapiz)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(333, 358)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 12)
        Me.Label2.TabIndex = 169
        Me.Label2.Text = "para crear código de remate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 351)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(207, 13)
        Me.Label3.TabIndex = 170
        Me.Label3.Text = "Relación de códigos de remate generados"
        '
        'frm_grupo_pesiev
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 379)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvCodRemate)
        Me.Controls.Add(Me.btnModificar)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtExiste)
        Me.Controls.Add(Me.chkEstado)
        Me.Controls.Add(Me.btn_Graficar)
        Me.Controls.Add(Me.btn_cerrar)
        Me.Controls.Add(Me.dgdDetalle)
        Me.Name = "frm_grupo_pesiev"
        Me.Text = "Relacion de DM Simultaneos por grupo"
        CType(Me.dgdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvCodRemate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgdDetalle As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents btn_cerrar As System.Windows.Forms.Button
    Friend WithEvents btn_Graficar As System.Windows.Forms.Button
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents txtExiste As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtArea As System.Windows.Forms.TextBox
    Friend WithEvents txtCodRemate As System.Windows.Forms.TextBox
    Friend WithEvents lblCodRemate As System.Windows.Forms.Label
    Friend WithEvents lblArea As System.Windows.Forms.Label
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents lstDM As System.Windows.Forms.ListBox
    Friend WithEvents lblDMSimul As System.Windows.Forms.Label
    Friend WithEvents btnModificar As System.Windows.Forms.Button
    Friend WithEvents dgvCodRemate As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
