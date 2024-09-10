<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Modulos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Modulos))
        Me.tlp_modulos = New System.Windows.Forms.TableLayoutPanel()
        Me.tlp_modulos_controles = New System.Windows.Forms.TableLayoutPanel()
        Me.cbx_modulos = New System.Windows.Forms.ComboBox()
        Me.lbl_modulos = New System.Windows.Forms.Label()
        Me.pbx_add = New System.Windows.Forms.PictureBox()
        Me.pbx_user_guide = New System.Windows.Forms.PictureBox()
        Me.pnl_modulos_form = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_cerrar_sesion = New System.Windows.Forms.Button()
        Me.pgb_modulos = New System.Windows.Forms.ProgressBar()
        Me.img_list_modulos = New System.Windows.Forms.ImageList(Me.components)
        Me.ttp_modulos = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlp_modulos.SuspendLayout()
        Me.tlp_modulos_controles.SuspendLayout()
        CType(Me.pbx_add, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbx_user_guide, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlp_modulos
        '
        Me.tlp_modulos.ColumnCount = 3
        Me.tlp_modulos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_modulos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_modulos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_modulos.Controls.Add(Me.tlp_modulos_controles, 1, 1)
        Me.tlp_modulos.Controls.Add(Me.pnl_modulos_form, 1, 2)
        Me.tlp_modulos.Controls.Add(Me.TableLayoutPanel1, 1, 3)
        Me.tlp_modulos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_modulos.Location = New System.Drawing.Point(0, 0)
        Me.tlp_modulos.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp_modulos.Name = "tlp_modulos"
        Me.tlp_modulos.RowCount = 5
        Me.tlp_modulos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_modulos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.tlp_modulos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_modulos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlp_modulos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_modulos.Size = New System.Drawing.Size(356, 502)
        Me.tlp_modulos.TabIndex = 0
        '
        'tlp_modulos_controles
        '
        Me.tlp_modulos_controles.ColumnCount = 3
        Me.tlp_modulos_controles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_modulos_controles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlp_modulos_controles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlp_modulos_controles.Controls.Add(Me.cbx_modulos, 0, 1)
        Me.tlp_modulos_controles.Controls.Add(Me.lbl_modulos, 0, 0)
        Me.tlp_modulos_controles.Controls.Add(Me.pbx_add, 2, 1)
        Me.tlp_modulos_controles.Controls.Add(Me.pbx_user_guide, 1, 1)
        Me.tlp_modulos_controles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_modulos_controles.Location = New System.Drawing.Point(10, 10)
        Me.tlp_modulos_controles.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp_modulos_controles.Name = "tlp_modulos_controles"
        Me.tlp_modulos_controles.RowCount = 2
        Me.tlp_modulos_controles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.tlp_modulos_controles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_modulos_controles.Size = New System.Drawing.Size(336, 41)
        Me.tlp_modulos_controles.TabIndex = 0
        '
        'cbx_modulos
        '
        Me.cbx_modulos.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_modulos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_modulos.FormattingEnabled = True
        Me.cbx_modulos.Location = New System.Drawing.Point(2, 18)
        Me.cbx_modulos.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_modulos.Name = "cbx_modulos"
        Me.cbx_modulos.Size = New System.Drawing.Size(280, 21)
        Me.cbx_modulos.TabIndex = 0
        '
        'lbl_modulos
        '
        Me.lbl_modulos.AutoSize = True
        Me.lbl_modulos.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_modulos.Location = New System.Drawing.Point(2, 3)
        Me.lbl_modulos.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_modulos.Name = "lbl_modulos"
        Me.lbl_modulos.Size = New System.Drawing.Size(280, 13)
        Me.lbl_modulos.TabIndex = 1
        Me.lbl_modulos.Text = "Seleccionar módulo"
        '
        'pbx_add
        '
        Me.pbx_add.BackgroundImage = Global.Automapic.My.Resources.Resources.ConfigureInfographics32
        Me.pbx_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbx_add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbx_add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbx_add.Location = New System.Drawing.Point(312, 18)
        Me.pbx_add.Margin = New System.Windows.Forms.Padding(2)
        Me.pbx_add.Name = "pbx_add"
        Me.pbx_add.Size = New System.Drawing.Size(22, 21)
        Me.pbx_add.TabIndex = 3
        Me.pbx_add.TabStop = False
        Me.ttp_modulos.SetToolTip(Me.pbx_add, "Configuración de Automapic")
        '
        'pbx_user_guide
        '
        Me.pbx_user_guide.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbx_user_guide.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbx_user_guide.Image = Global.Automapic.My.Resources.Resources.BookmarkDockWindow32
        Me.pbx_user_guide.Location = New System.Drawing.Point(286, 18)
        Me.pbx_user_guide.Margin = New System.Windows.Forms.Padding(2)
        Me.pbx_user_guide.Name = "pbx_user_guide"
        Me.pbx_user_guide.Size = New System.Drawing.Size(22, 21)
        Me.pbx_user_guide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbx_user_guide.TabIndex = 4
        Me.pbx_user_guide.TabStop = False
        Me.ttp_modulos.SetToolTip(Me.pbx_user_guide, "Manual de usuario")
        '
        'pnl_modulos_form
        '
        Me.pnl_modulos_form.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_modulos_form.Location = New System.Drawing.Point(10, 55)
        Me.pnl_modulos_form.Margin = New System.Windows.Forms.Padding(2)
        Me.pnl_modulos_form.Name = "pnl_modulos_form"
        Me.pnl_modulos_form.Size = New System.Drawing.Size(336, 405)
        Me.pnl_modulos_form.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_cerrar_sesion, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pgb_modulos, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(10, 464)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(336, 28)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'btn_cerrar_sesion
        '
        Me.btn_cerrar_sesion.BackColor = System.Drawing.SystemColors.Control
        Me.btn_cerrar_sesion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_cerrar_sesion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_cerrar_sesion.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btn_cerrar_sesion.Image = Global.Automapic.My.Resources.Resources.UserBlue16
        Me.btn_cerrar_sesion.Location = New System.Drawing.Point(263, 2)
        Me.btn_cerrar_sesion.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_cerrar_sesion.Name = "btn_cerrar_sesion"
        Me.btn_cerrar_sesion.Size = New System.Drawing.Size(71, 24)
        Me.btn_cerrar_sesion.TabIndex = 1
        Me.btn_cerrar_sesion.Text = "Salir"
        Me.btn_cerrar_sesion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cerrar_sesion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_cerrar_sesion.UseVisualStyleBackColor = False
        '
        'pgb_modulos
        '
        Me.pgb_modulos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgb_modulos.Location = New System.Drawing.Point(2, 2)
        Me.pgb_modulos.Margin = New System.Windows.Forms.Padding(2)
        Me.pgb_modulos.MarqueeAnimationSpeed = 50
        Me.pgb_modulos.Name = "pgb_modulos"
        Me.pgb_modulos.Size = New System.Drawing.Size(257, 24)
        Me.pgb_modulos.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pgb_modulos.TabIndex = 2
        '
        'img_list_modulos
        '
        Me.img_list_modulos.ImageStream = CType(resources.GetObject("img_list_modulos.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img_list_modulos.TransparentColor = System.Drawing.Color.Transparent
        Me.img_list_modulos.Images.SetKeyName(0, "ConfigureInfographics32.png")
        Me.img_list_modulos.Images.SetKeyName(1, "SearchWindowShow32.png")
        Me.img_list_modulos.Images.SetKeyName(2, "PopupCancel16.png")
        '
        'ttp_modulos
        '
        Me.ttp_modulos.IsBalloon = True
        Me.ttp_modulos.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ttp_modulos.ToolTipTitle = "Información"
        '
        'Modulos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(356, 502)
        Me.Controls.Add(Me.tlp_modulos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Modulos"
        Me.Text = "Modulos"
        Me.tlp_modulos.ResumeLayout(False)
        Me.tlp_modulos_controles.ResumeLayout(False)
        Me.tlp_modulos_controles.PerformLayout()
        CType(Me.pbx_add, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbx_user_guide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlp_modulos As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlp_modulos_controles As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbx_modulos As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_modulos As System.Windows.Forms.Label
    Friend WithEvents btn_cerrar_sesion As System.Windows.Forms.Button
    Friend WithEvents pnl_modulos_form As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pgb_modulos As System.Windows.Forms.ProgressBar
    Friend WithEvents pbx_add As System.Windows.Forms.PictureBox
    Friend WithEvents img_list_modulos As System.Windows.Forms.ImageList
    Friend WithEvents pbx_user_guide As System.Windows.Forms.PictureBox
    Friend WithEvents ttp_modulos As System.Windows.Forms.ToolTip
End Class
