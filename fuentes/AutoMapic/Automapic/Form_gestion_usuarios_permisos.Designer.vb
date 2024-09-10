<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_gestion_usuarios_permisos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_gestion_usuarios_permisos))
        Me.tlp_permisos = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_gu_actualizar = New System.Windows.Forms.Button()
        Me.lbl_seleecione = New System.Windows.Forms.Label()
        Me.lbl_usuario = New System.Windows.Forms.Label()
        Me.cbx_usuario = New System.Windows.Forms.ComboBox()
        Me.tlp2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_gu_warning = New System.Windows.Forms.Label()
        Me.dgv_permisos = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MODULO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACCESO = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PERFIL = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.tlp_permisos.SuspendLayout()
        Me.tlp2.SuspendLayout()
        CType(Me.dgv_permisos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlp_permisos
        '
        Me.tlp_permisos.BackColor = System.Drawing.Color.White
        Me.tlp_permisos.ColumnCount = 3
        Me.tlp_permisos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_permisos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.tlp_permisos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.tlp_permisos.Controls.Add(Me.btn_gu_actualizar, 0, 4)
        Me.tlp_permisos.Controls.Add(Me.lbl_seleecione, 0, 0)
        Me.tlp_permisos.Controls.Add(Me.lbl_usuario, 0, 1)
        Me.tlp_permisos.Controls.Add(Me.cbx_usuario, 2, 1)
        Me.tlp_permisos.Controls.Add(Me.tlp2, 0, 3)
        Me.tlp_permisos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_permisos.Location = New System.Drawing.Point(0, 0)
        Me.tlp_permisos.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp_permisos.Name = "tlp_permisos"
        Me.tlp_permisos.RowCount = 5
        Me.tlp_permisos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.tlp_permisos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlp_permisos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tlp_permisos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_permisos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tlp_permisos.Size = New System.Drawing.Size(284, 488)
        Me.tlp_permisos.TabIndex = 0
        '
        'btn_gu_actualizar
        '
        Me.tlp_permisos.SetColumnSpan(Me.btn_gu_actualizar, 3)
        Me.btn_gu_actualizar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_gu_actualizar.Image = CType(resources.GetObject("btn_gu_actualizar.Image"), System.Drawing.Image)
        Me.btn_gu_actualizar.Location = New System.Drawing.Point(2, 462)
        Me.btn_gu_actualizar.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_gu_actualizar.Name = "btn_gu_actualizar"
        Me.btn_gu_actualizar.Size = New System.Drawing.Size(280, 24)
        Me.btn_gu_actualizar.TabIndex = 14
        Me.btn_gu_actualizar.Text = "Actualizar Permisos"
        Me.btn_gu_actualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_gu_actualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_gu_actualizar.UseVisualStyleBackColor = True
        '
        'lbl_seleecione
        '
        Me.lbl_seleecione.AutoSize = True
        Me.lbl_seleecione.Location = New System.Drawing.Point(2, 0)
        Me.lbl_seleecione.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_seleecione.Name = "lbl_seleecione"
        Me.lbl_seleecione.Size = New System.Drawing.Size(97, 13)
        Me.lbl_seleecione.TabIndex = 0
        Me.lbl_seleecione.Text = "Seleccione usuario"
        '
        'lbl_usuario
        '
        Me.lbl_usuario.AutoSize = True
        Me.lbl_usuario.Location = New System.Drawing.Point(2, 24)
        Me.lbl_usuario.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_usuario.Name = "lbl_usuario"
        Me.lbl_usuario.Size = New System.Drawing.Size(46, 13)
        Me.lbl_usuario.TabIndex = 1
        Me.lbl_usuario.Text = "Usuario:"
        '
        'cbx_usuario
        '
        Me.cbx_usuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbx_usuario.FormattingEnabled = True
        Me.cbx_usuario.Location = New System.Drawing.Point(151, 26)
        Me.cbx_usuario.Margin = New System.Windows.Forms.Padding(2)
        Me.cbx_usuario.Name = "cbx_usuario"
        Me.cbx_usuario.Size = New System.Drawing.Size(131, 21)
        Me.cbx_usuario.TabIndex = 5
        '
        'tlp2
        '
        Me.tlp2.AutoSize = True
        Me.tlp2.ColumnCount = 1
        Me.tlp_permisos.SetColumnSpan(Me.tlp2, 3)
        Me.tlp2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp2.Controls.Add(Me.lbl_gu_warning, 0, 1)
        Me.tlp2.Controls.Add(Me.dgv_permisos, 0, 0)
        Me.tlp2.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlp2.Location = New System.Drawing.Point(2, 86)
        Me.tlp2.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp2.Name = "tlp2"
        Me.tlp2.RowCount = 2
        Me.tlp2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.tlp2.Size = New System.Drawing.Size(280, 195)
        Me.tlp2.TabIndex = 16
        '
        'lbl_gu_warning
        '
        Me.lbl_gu_warning.AutoSize = True
        Me.lbl_gu_warning.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_gu_warning.ForeColor = System.Drawing.Color.Green
        Me.lbl_gu_warning.Location = New System.Drawing.Point(2, 171)
        Me.lbl_gu_warning.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_gu_warning.Name = "lbl_gu_warning"
        Me.lbl_gu_warning.Size = New System.Drawing.Size(276, 13)
        Me.lbl_gu_warning.TabIndex = 15
        Me.lbl_gu_warning.Text = "Advertencia"
        Me.lbl_gu_warning.Visible = False
        '
        'dgv_permisos
        '
        Me.dgv_permisos.AllowUserToAddRows = False
        Me.dgv_permisos.AllowUserToDeleteRows = False
        Me.dgv_permisos.AllowUserToResizeRows = False
        Me.dgv_permisos.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgv_permisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_permisos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.MODULO, Me.ACCESO, Me.PERFIL})
        Me.dgv_permisos.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgv_permisos.Location = New System.Drawing.Point(2, 2)
        Me.dgv_permisos.Margin = New System.Windows.Forms.Padding(2)
        Me.dgv_permisos.MaximumSize = New System.Drawing.Size(0, 406)
        Me.dgv_permisos.Name = "dgv_permisos"
        Me.dgv_permisos.RowHeadersVisible = False
        Me.dgv_permisos.RowTemplate.Height = 24
        Me.dgv_permisos.Size = New System.Drawing.Size(276, 167)
        Me.dgv_permisos.TabIndex = 6
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 30
        '
        'MODULO
        '
        Me.MODULO.HeaderText = "MODULO"
        Me.MODULO.Name = "MODULO"
        Me.MODULO.ReadOnly = True
        '
        'ACCESO
        '
        Me.ACCESO.HeaderText = "ACCESO"
        Me.ACCESO.Name = "ACCESO"
        Me.ACCESO.Width = 65
        '
        'PERFIL
        '
        Me.PERFIL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PERFIL.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.PERFIL.HeaderText = "PERFIL"
        Me.PERFIL.Name = "PERFIL"
        '
        'Form_gestion_usuarios_permisos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 488)
        Me.Controls.Add(Me.tlp_permisos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form_gestion_usuarios_permisos"
        Me.Text = "Form_gestion_usuarios_permisos"
        Me.tlp_permisos.ResumeLayout(False)
        Me.tlp_permisos.PerformLayout()
        Me.tlp2.ResumeLayout(False)
        Me.tlp2.PerformLayout()
        CType(Me.dgv_permisos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlp_permisos As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_seleecione As System.Windows.Forms.Label
    Friend WithEvents lbl_usuario As System.Windows.Forms.Label
    Friend WithEvents cbx_usuario As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_permisos As System.Windows.Forms.DataGridView
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MODULO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ACCESO As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PERFIL As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents btn_gu_actualizar As System.Windows.Forms.Button
    Friend WithEvents lbl_gu_warning As System.Windows.Forms.Label
    Friend WithEvents tlp2 As System.Windows.Forms.TableLayoutPanel
End Class
