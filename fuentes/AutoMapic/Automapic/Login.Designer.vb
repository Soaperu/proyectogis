<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.tlp_login = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_login = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pbx_login_loader = New System.Windows.Forms.PictureBox()
        Me.lbl_login_log = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_pass = New System.Windows.Forms.TextBox()
        Me.tbx_user = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.img_list_login = New System.Windows.Forms.ImageList(Me.components)
        Me.tlp_login.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbx_login_loader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlp_login
        '
        Me.tlp_login.ColumnCount = 6
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlp_login.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.tlp_login.Controls.Add(Me.btn_login, 2, 11)
        Me.tlp_login.Controls.Add(Me.PictureBox1, 2, 1)
        Me.tlp_login.Controls.Add(Me.pbx_login_loader, 2, 13)
        Me.tlp_login.Controls.Add(Me.lbl_login_log, 2, 14)
        Me.tlp_login.Controls.Add(Me.Label1, 1, 15)
        Me.tlp_login.Controls.Add(Me.Label2, 2, 6)
        Me.tlp_login.Controls.Add(Me.Label3, 2, 9)
        Me.tlp_login.Controls.Add(Me.tbx_pass, 3, 8)
        Me.tlp_login.Controls.Add(Me.tbx_user, 3, 5)
        Me.tlp_login.Controls.Add(Me.PictureBox2, 2, 5)
        Me.tlp_login.Controls.Add(Me.PictureBox3, 2, 8)
        Me.tlp_login.Controls.Add(Me.PictureBox4, 2, 3)
        Me.tlp_login.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_login.Location = New System.Drawing.Point(0, 0)
        Me.tlp_login.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp_login.Name = "tlp_login"
        Me.tlp_login.RowCount = 17
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.tlp_login.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.tlp_login.Size = New System.Drawing.Size(298, 547)
        Me.tlp_login.TabIndex = 0
        '
        'btn_login
        '
        Me.btn_login.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(161, Byte), Integer))
        Me.tlp_login.SetColumnSpan(Me.btn_login, 2)
        Me.btn_login.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_login.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_login.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_login.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_login.ForeColor = System.Drawing.Color.White
        Me.btn_login.Location = New System.Drawing.Point(66, 293)
        Me.btn_login.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(166, 24)
        Me.btn_login.TabIndex = 5
        Me.btn_login.Text = "LOGIN"
        Me.btn_login.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.tlp_login.SetColumnSpan(Me.PictureBox1, 2)
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(66, 52)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(166, 94)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'pbx_login_loader
        '
        Me.pbx_login_loader.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tlp_login.SetColumnSpan(Me.pbx_login_loader, 2)
        Me.pbx_login_loader.Location = New System.Drawing.Point(136, 333)
        Me.pbx_login_loader.Margin = New System.Windows.Forms.Padding(2)
        Me.pbx_login_loader.Name = "pbx_login_loader"
        Me.pbx_login_loader.Size = New System.Drawing.Size(26, 31)
        Me.pbx_login_loader.TabIndex = 7
        Me.pbx_login_loader.TabStop = False
        Me.pbx_login_loader.Visible = False
        '
        'lbl_login_log
        '
        Me.lbl_login_log.AutoSize = True
        Me.tlp_login.SetColumnSpan(Me.lbl_login_log, 2)
        Me.lbl_login_log.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_login_log.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_login_log.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_login_log.Location = New System.Drawing.Point(66, 368)
        Me.lbl_login_log.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_login_log.Name = "lbl_login_log"
        Me.lbl_login_log.Size = New System.Drawing.Size(166, 13)
        Me.lbl_login_log.TabIndex = 8
        Me.lbl_login_log.Text = "..."
        Me.lbl_login_log.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbl_login_log.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.tlp_login.SetColumnSpan(Me.Label1, 4)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 524)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Ingemmet"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(161, Byte), Integer))
        Me.tlp_login.SetColumnSpan(Me.Label2, 2)
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 1.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(97, Byte), Integer), CType(CType(176, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(67, 243)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(164, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(161, Byte), Integer))
        Me.tlp_login.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 1.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(67, 284)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(164, 1)
        Me.Label3.TabIndex = 11
        '
        'tbx_pass
        '
        Me.tbx_pass.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_pass.BackColor = System.Drawing.Color.White
        Me.tbx_pass.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbx_pass.ForeColor = System.Drawing.Color.Gray
        Me.tbx_pass.Location = New System.Drawing.Point(86, 265)
        Me.tbx_pass.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_pass.Name = "tbx_pass"
        Me.tbx_pass.Size = New System.Drawing.Size(146, 13)
        Me.tbx_pass.TabIndex = 1
        Me.tbx_pass.Text = "PASSWORD"
        '
        'tbx_user
        '
        Me.tbx_user.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_user.BackColor = System.Drawing.Color.White
        Me.tbx_user.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbx_user.ForeColor = System.Drawing.Color.Gray
        Me.tbx_user.Location = New System.Drawing.Point(86, 224)
        Me.tbx_user.Margin = New System.Windows.Forms.Padding(2)
        Me.tbx_user.Name = "tbx_user"
        Me.tbx_user.Size = New System.Drawing.Size(146, 13)
        Me.tbx_user.TabIndex = 0
        Me.tbx_user.Text = "USERNAME"
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(67, 221)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(14, 19)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 12
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(67, 262)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(14, 19)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 13
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.tlp_login.SetColumnSpan(Me.PictureBox4, 2)
        Me.PictureBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(67, 166)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(164, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox4.TabIndex = 14
        Me.PictureBox4.TabStop = False
        '
        'img_list_login
        '
        Me.img_list_login.ImageStream = CType(resources.GetObject("img_list_login.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img_list_login.TransparentColor = System.Drawing.Color.Transparent
        Me.img_list_login.Images.SetKeyName(0, "UserBlue16.png")
        Me.img_list_login.Images.SetKeyName(1, "CarKey_B_16.png")
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(298, 547)
        Me.Controls.Add(Me.tlp_login)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Login"
        Me.Text = "Login"
        Me.tlp_login.ResumeLayout(False)
        Me.tlp_login.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbx_login_loader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlp_login As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tbx_user As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pass As System.Windows.Forms.TextBox
    Friend WithEvents btn_login As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents img_list_login As System.Windows.Forms.ImageList
    Friend WithEvents pbx_login_loader As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_login_log As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
End Class
