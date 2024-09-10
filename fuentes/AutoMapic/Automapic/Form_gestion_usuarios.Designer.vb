<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_gestion_usuarios
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
        Me.tbpanel_gest_user = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_reportes = New System.Windows.Forms.Button()
        Me.dgv_gest_usuarios = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NOMBRE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.APELLIDO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OFICINA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_agregar_usuario = New System.Windows.Forms.Button()
        Me.btn_editar_usuario = New System.Windows.Forms.Button()
        Me.btn_eliminar_usuario = New System.Windows.Forms.Button()
        Me.btn_gest_perfiles = New System.Windows.Forms.Button()
        Me.tbpanel_gest_user.SuspendLayout()
        CType(Me.dgv_gest_usuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbpanel_gest_user
        '
        Me.tbpanel_gest_user.AutoScroll = True
        Me.tbpanel_gest_user.AutoSize = True
        Me.tbpanel_gest_user.ColumnCount = 5
        Me.tbpanel_gest_user.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tbpanel_gest_user.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tbpanel_gest_user.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tbpanel_gest_user.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tbpanel_gest_user.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tbpanel_gest_user.Controls.Add(Me.btn_reportes, 3, 2)
        Me.tbpanel_gest_user.Controls.Add(Me.dgv_gest_usuarios, 0, 0)
        Me.tbpanel_gest_user.Controls.Add(Me.btn_agregar_usuario, 0, 2)
        Me.tbpanel_gest_user.Controls.Add(Me.btn_editar_usuario, 1, 2)
        Me.tbpanel_gest_user.Controls.Add(Me.btn_eliminar_usuario, 2, 2)
        Me.tbpanel_gest_user.Controls.Add(Me.btn_gest_perfiles, 4, 2)
        Me.tbpanel_gest_user.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbpanel_gest_user.Location = New System.Drawing.Point(0, 0)
        Me.tbpanel_gest_user.Name = "tbpanel_gest_user"
        Me.tbpanel_gest_user.RowCount = 4
        Me.tbpanel_gest_user.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tbpanel_gest_user.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.tbpanel_gest_user.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tbpanel_gest_user.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.tbpanel_gest_user.Size = New System.Drawing.Size(307, 571)
        Me.tbpanel_gest_user.TabIndex = 0
        '
        'btn_reportes
        '
        Me.btn_reportes.BackColor = System.Drawing.Color.SteelBlue
        Me.btn_reportes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_reportes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_reportes.FlatAppearance.BorderSize = 0
        Me.btn_reportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_reportes.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reportes.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_reportes.Location = New System.Drawing.Point(183, 542)
        Me.btn_reportes.Name = "btn_reportes"
        Me.btn_reportes.Size = New System.Drawing.Size(54, 22)
        Me.btn_reportes.TabIndex = 4
        Me.btn_reportes.Text = "Reporte"
        Me.btn_reportes.UseVisualStyleBackColor = False
        '
        'dgv_gest_usuarios
        '
        Me.dgv_gest_usuarios.AllowUserToAddRows = False
        Me.dgv_gest_usuarios.AllowUserToDeleteRows = False
        Me.dgv_gest_usuarios.AllowUserToResizeRows = False
        Me.dgv_gest_usuarios.BackgroundColor = System.Drawing.Color.White
        Me.dgv_gest_usuarios.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv_gest_usuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_gest_usuarios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.USER, Me.NOMBRE, Me.APELLIDO, Me.OFICINA})
        Me.tbpanel_gest_user.SetColumnSpan(Me.dgv_gest_usuarios, 5)
        Me.dgv_gest_usuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_gest_usuarios.Location = New System.Drawing.Point(3, 3)
        Me.dgv_gest_usuarios.MultiSelect = False
        Me.dgv_gest_usuarios.Name = "dgv_gest_usuarios"
        Me.dgv_gest_usuarios.ReadOnly = True
        Me.dgv_gest_usuarios.RowHeadersVisible = False
        Me.dgv_gest_usuarios.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv_gest_usuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_gest_usuarios.Size = New System.Drawing.Size(301, 529)
        Me.dgv_gest_usuarios.TabIndex = 0
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 30
        '
        'USER
        '
        Me.USER.HeaderText = "USER"
        Me.USER.Name = "USER"
        Me.USER.ReadOnly = True
        Me.USER.Width = 50
        '
        'NOMBRE
        '
        Me.NOMBRE.HeaderText = "NOMBRE"
        Me.NOMBRE.Name = "NOMBRE"
        Me.NOMBRE.ReadOnly = True
        Me.NOMBRE.Width = 70
        '
        'APELLIDO
        '
        Me.APELLIDO.HeaderText = "APELLIDO"
        Me.APELLIDO.Name = "APELLIDO"
        Me.APELLIDO.ReadOnly = True
        Me.APELLIDO.Width = 70
        '
        'OFICINA
        '
        Me.OFICINA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.OFICINA.HeaderText = "OFICINA"
        Me.OFICINA.Name = "OFICINA"
        Me.OFICINA.ReadOnly = True
        '
        'btn_agregar_usuario
        '
        Me.btn_agregar_usuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_agregar_usuario.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_agregar_usuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_agregar_usuario.FlatAppearance.BorderSize = 0
        Me.btn_agregar_usuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_agregar_usuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_agregar_usuario.ForeColor = System.Drawing.Color.White
        Me.btn_agregar_usuario.Location = New System.Drawing.Point(3, 542)
        Me.btn_agregar_usuario.Name = "btn_agregar_usuario"
        Me.btn_agregar_usuario.Size = New System.Drawing.Size(54, 22)
        Me.btn_agregar_usuario.TabIndex = 1
        Me.btn_agregar_usuario.Text = "Agregar"
        Me.btn_agregar_usuario.UseVisualStyleBackColor = False
        '
        'btn_editar_usuario
        '
        Me.btn_editar_usuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(1, Byte), Integer))
        Me.btn_editar_usuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_editar_usuario.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_editar_usuario.FlatAppearance.BorderSize = 0
        Me.btn_editar_usuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_editar_usuario.ForeColor = System.Drawing.Color.White
        Me.btn_editar_usuario.Location = New System.Drawing.Point(63, 542)
        Me.btn_editar_usuario.Name = "btn_editar_usuario"
        Me.btn_editar_usuario.Size = New System.Drawing.Size(54, 22)
        Me.btn_editar_usuario.TabIndex = 2
        Me.btn_editar_usuario.Text = "Editar"
        Me.btn_editar_usuario.UseVisualStyleBackColor = False
        '
        'btn_eliminar_usuario
        '
        Me.btn_eliminar_usuario.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btn_eliminar_usuario.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_eliminar_usuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_eliminar_usuario.FlatAppearance.BorderSize = 0
        Me.btn_eliminar_usuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_eliminar_usuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_eliminar_usuario.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_eliminar_usuario.Location = New System.Drawing.Point(123, 542)
        Me.btn_eliminar_usuario.Name = "btn_eliminar_usuario"
        Me.btn_eliminar_usuario.Size = New System.Drawing.Size(54, 22)
        Me.btn_eliminar_usuario.TabIndex = 3
        Me.btn_eliminar_usuario.Text = "Eliminar"
        Me.btn_eliminar_usuario.UseVisualStyleBackColor = False
        '
        'btn_gest_perfiles
        '
        Me.btn_gest_perfiles.BackColor = System.Drawing.Color.White
        Me.btn_gest_perfiles.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_gest_perfiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_gest_perfiles.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_gest_perfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_gest_perfiles.Location = New System.Drawing.Point(243, 542)
        Me.btn_gest_perfiles.Name = "btn_gest_perfiles"
        Me.btn_gest_perfiles.Size = New System.Drawing.Size(61, 22)
        Me.btn_gest_perfiles.TabIndex = 5
        Me.btn_gest_perfiles.Text = "Gest. Permisos"
        Me.btn_gest_perfiles.UseVisualStyleBackColor = False
        '
        'Form_gestion_usuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(307, 571)
        Me.Controls.Add(Me.tbpanel_gest_user)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_gestion_usuarios"
        Me.Text = "Form_gestion_usuarios"
        Me.tbpanel_gest_user.ResumeLayout(False)
        CType(Me.dgv_gest_usuarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbpanel_gest_user As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgv_gest_usuarios As System.Windows.Forms.DataGridView
    Friend WithEvents btn_agregar_usuario As System.Windows.Forms.Button
    Friend WithEvents btn_editar_usuario As System.Windows.Forms.Button
    Friend WithEvents btn_eliminar_usuario As System.Windows.Forms.Button
    Friend WithEvents btn_gest_perfiles As System.Windows.Forms.Button
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NOMBRE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents APELLIDO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OFICINA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_reportes As System.Windows.Forms.Button
End Class
