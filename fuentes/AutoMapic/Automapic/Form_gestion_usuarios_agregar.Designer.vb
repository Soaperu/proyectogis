<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_gestion_usuarios_agregar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_gestion_usuarios_agregar))
        Me.tbl_ges_agre = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_gu_agregar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tb_gu_id = New System.Windows.Forms.TextBox()
        Me.tb_gu_nombre = New System.Windows.Forms.TextBox()
        Me.tb_gu_apepat = New System.Windows.Forms.TextBox()
        Me.tb_gu_apemat = New System.Windows.Forms.TextBox()
        Me.cbx_gu_oficina = New System.Windows.Forms.ComboBox()
        Me.lbl_gu_warning = New System.Windows.Forms.Label()
        Me.cbx_gu_usuario = New System.Windows.Forms.ComboBox()
        Me.tbl_ges_agre.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbl_ges_agre
        '
        Me.tbl_ges_agre.BackColor = System.Drawing.Color.White
        Me.tbl_ges_agre.ColumnCount = 3
        Me.tbl_ges_agre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tbl_ges_agre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.tbl_ges_agre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.tbl_ges_agre.Controls.Add(Me.btn_gu_agregar, 0, 7)
        Me.tbl_ges_agre.Controls.Add(Me.Label1, 0, 0)
        Me.tbl_ges_agre.Controls.Add(Me.Label2, 0, 1)
        Me.tbl_ges_agre.Controls.Add(Me.Label3, 0, 2)
        Me.tbl_ges_agre.Controls.Add(Me.Label4, 0, 3)
        Me.tbl_ges_agre.Controls.Add(Me.Label5, 0, 4)
        Me.tbl_ges_agre.Controls.Add(Me.Label6, 0, 5)
        Me.tbl_ges_agre.Controls.Add(Me.tb_gu_id, 2, 0)
        Me.tbl_ges_agre.Controls.Add(Me.tb_gu_nombre, 2, 3)
        Me.tbl_ges_agre.Controls.Add(Me.tb_gu_apepat, 2, 4)
        Me.tbl_ges_agre.Controls.Add(Me.tb_gu_apemat, 2, 5)
        Me.tbl_ges_agre.Controls.Add(Me.cbx_gu_oficina, 2, 2)
        Me.tbl_ges_agre.Controls.Add(Me.lbl_gu_warning, 0, 6)
        Me.tbl_ges_agre.Controls.Add(Me.cbx_gu_usuario, 2, 1)
        Me.tbl_ges_agre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbl_ges_agre.Location = New System.Drawing.Point(0, 0)
        Me.tbl_ges_agre.Name = "tbl_ges_agre"
        Me.tbl_ges_agre.RowCount = 8
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tbl_ges_agre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbl_ges_agre.Size = New System.Drawing.Size(284, 261)
        Me.tbl_ges_agre.TabIndex = 0
        '
        'btn_gu_agregar
        '
        Me.tbl_ges_agre.SetColumnSpan(Me.btn_gu_agregar, 3)
        Me.btn_gu_agregar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_gu_agregar.Image = CType(resources.GetObject("btn_gu_agregar.Image"), System.Drawing.Image)
        Me.btn_gu_agregar.Location = New System.Drawing.Point(2, 235)
        Me.btn_gu_agregar.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_gu_agregar.Name = "btn_gu_agregar"
        Me.btn_gu_agregar.Size = New System.Drawing.Size(280, 24)
        Me.btn_gu_agregar.TabIndex = 13
        Me.btn_gu_agregar.Text = "Agregar Usuario"
        Me.btn_gu_agregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_gu_agregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_gu_agregar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Id Usuario:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 28)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Usuario:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 28)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Oficina:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 28)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Nombre:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 28)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Apellido Paterno:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(3, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 28)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Apellido Materno:"
        '
        'tb_gu_id
        '
        Me.tb_gu_id.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_gu_id.Enabled = False
        Me.tb_gu_id.Location = New System.Drawing.Point(152, 3)
        Me.tb_gu_id.Name = "tb_gu_id"
        Me.tb_gu_id.Size = New System.Drawing.Size(129, 20)
        Me.tb_gu_id.TabIndex = 6
        '
        'tb_gu_nombre
        '
        Me.tb_gu_nombre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_gu_nombre.Enabled = False
        Me.tb_gu_nombre.Location = New System.Drawing.Point(152, 87)
        Me.tb_gu_nombre.Name = "tb_gu_nombre"
        Me.tb_gu_nombre.Size = New System.Drawing.Size(129, 20)
        Me.tb_gu_nombre.TabIndex = 9
        '
        'tb_gu_apepat
        '
        Me.tb_gu_apepat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_gu_apepat.Enabled = False
        Me.tb_gu_apepat.Location = New System.Drawing.Point(152, 115)
        Me.tb_gu_apepat.Name = "tb_gu_apepat"
        Me.tb_gu_apepat.Size = New System.Drawing.Size(129, 20)
        Me.tb_gu_apepat.TabIndex = 10
        '
        'tb_gu_apemat
        '
        Me.tb_gu_apemat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_gu_apemat.Enabled = False
        Me.tb_gu_apemat.Location = New System.Drawing.Point(152, 143)
        Me.tb_gu_apemat.Name = "tb_gu_apemat"
        Me.tb_gu_apemat.Size = New System.Drawing.Size(129, 20)
        Me.tb_gu_apemat.TabIndex = 11
        '
        'cbx_gu_oficina
        '
        Me.cbx_gu_oficina.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_gu_oficina.Enabled = False
        Me.cbx_gu_oficina.FormattingEnabled = True
        Me.cbx_gu_oficina.Location = New System.Drawing.Point(152, 59)
        Me.cbx_gu_oficina.Name = "cbx_gu_oficina"
        Me.cbx_gu_oficina.Size = New System.Drawing.Size(129, 21)
        Me.cbx_gu_oficina.TabIndex = 12
        '
        'lbl_gu_warning
        '
        Me.lbl_gu_warning.AutoSize = True
        Me.tbl_ges_agre.SetColumnSpan(Me.lbl_gu_warning, 3)
        Me.lbl_gu_warning.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_gu_warning.ForeColor = System.Drawing.Color.Red
        Me.lbl_gu_warning.Location = New System.Drawing.Point(2, 168)
        Me.lbl_gu_warning.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_gu_warning.Name = "lbl_gu_warning"
        Me.lbl_gu_warning.Size = New System.Drawing.Size(280, 13)
        Me.lbl_gu_warning.TabIndex = 14
        Me.lbl_gu_warning.Text = "Advertencia"
        Me.lbl_gu_warning.Visible = False
        '
        'cbx_gu_usuario
        '
        Me.cbx_gu_usuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_gu_usuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbx_gu_usuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_gu_usuario.Enabled = False
        Me.cbx_gu_usuario.FormattingEnabled = True
        Me.cbx_gu_usuario.Location = New System.Drawing.Point(151, 30)
        Me.cbx_gu_usuario.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_gu_usuario.Name = "cbx_gu_usuario"
        Me.cbx_gu_usuario.Size = New System.Drawing.Size(131, 21)
        Me.cbx_gu_usuario.TabIndex = 15
        '
        'Form_gestion_usuarios_agregar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.tbl_ges_agre)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_gestion_usuarios_agregar"
        Me.Text = "Form_gestion_usuarios_agregar"
        Me.tbl_ges_agre.ResumeLayout(False)
        Me.tbl_ges_agre.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbl_ges_agre As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tb_gu_id As System.Windows.Forms.TextBox
    Friend WithEvents tb_gu_nombre As System.Windows.Forms.TextBox
    Friend WithEvents tb_gu_apepat As System.Windows.Forms.TextBox
    Friend WithEvents tb_gu_apemat As System.Windows.Forms.TextBox
    Friend WithEvents cbx_gu_oficina As System.Windows.Forms.ComboBox
    Friend WithEvents btn_gu_agregar As System.Windows.Forms.Button
    Friend WithEvents lbl_gu_warning As System.Windows.Forms.Label
    Friend WithEvents cbx_gu_usuario As System.Windows.Forms.ComboBox
End Class
