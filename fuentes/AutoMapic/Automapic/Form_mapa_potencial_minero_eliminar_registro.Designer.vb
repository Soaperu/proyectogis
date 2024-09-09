<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_mapa_potencial_minero_eliminar_registro
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_potencial_minero_eliminar_registro))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbx_mpm_er_codigo = New System.Windows.Forms.TextBox()
        Me.btn_mpm_er_buscar = New System.Windows.Forms.Button()
        Me.btn_mpm_er_eliminar = New System.Windows.Forms.Button()
        Me.lbl_mpm_er_datos = New System.Windows.Forms.Label()
        Me.tbx_mpm_er_detalle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_er_codigo, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mpm_er_buscar, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_mpm_er_eliminar, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_mpm_er_datos, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_mpm_er_detalle, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(284, 260)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(10, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(226, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ingrese código de mapa a eliminar"
        '
        'tbx_mpm_er_codigo
        '
        Me.tbx_mpm_er_codigo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_mpm_er_codigo.Location = New System.Drawing.Point(10, 28)
        Me.tbx_mpm_er_codigo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_mpm_er_codigo.Name = "tbx_mpm_er_codigo"
        Me.tbx_mpm_er_codigo.Size = New System.Drawing.Size(226, 20)
        Me.tbx_mpm_er_codigo.TabIndex = 1
        '
        'btn_mpm_er_buscar
        '
        Me.btn_mpm_er_buscar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_er_buscar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_mpm_er_buscar.Image = Global.Automapic.My.Resources.Resources.SearchWindowShow16
        Me.btn_mpm_er_buscar.Location = New System.Drawing.Point(240, 26)
        Me.btn_mpm_er_buscar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btn_mpm_er_buscar.Name = "btn_mpm_er_buscar"
        Me.btn_mpm_er_buscar.Size = New System.Drawing.Size(34, 24)
        Me.btn_mpm_er_buscar.TabIndex = 2
        Me.btn_mpm_er_buscar.UseVisualStyleBackColor = True
        '
        'btn_mpm_er_eliminar
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_mpm_er_eliminar, 2)
        Me.btn_mpm_er_eliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_mpm_er_eliminar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_mpm_er_eliminar.Enabled = False
        Me.btn_mpm_er_eliminar.Image = Global.Automapic.My.Resources.Resources.EditingErrorInspector16
        Me.btn_mpm_er_eliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_mpm_er_eliminar.Location = New System.Drawing.Point(143, 226)
        Me.btn_mpm_er_eliminar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btn_mpm_er_eliminar.Name = "btn_mpm_er_eliminar"
        Me.btn_mpm_er_eliminar.Size = New System.Drawing.Size(131, 24)
        Me.btn_mpm_er_eliminar.TabIndex = 3
        Me.btn_mpm_er_eliminar.Text = "Eliminar registro"
        Me.btn_mpm_er_eliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_mpm_er_eliminar.UseVisualStyleBackColor = True
        '
        'lbl_mpm_er_datos
        '
        Me.lbl_mpm_er_datos.AutoSize = True
        Me.lbl_mpm_er_datos.Location = New System.Drawing.Point(10, 52)
        Me.lbl_mpm_er_datos.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_mpm_er_datos.Name = "lbl_mpm_er_datos"
        Me.lbl_mpm_er_datos.Size = New System.Drawing.Size(16, 13)
        Me.lbl_mpm_er_datos.TabIndex = 4
        Me.lbl_mpm_er_datos.Text = "..."
        '
        'tbx_mpm_er_detalle
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_mpm_er_detalle, 2)
        Me.tbx_mpm_er_detalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_mpm_er_detalle.Enabled = False
        Me.tbx_mpm_er_detalle.Location = New System.Drawing.Point(10, 145)
        Me.tbx_mpm_er_detalle.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_mpm_er_detalle.Multiline = True
        Me.tbx_mpm_er_detalle.Name = "tbx_mpm_er_detalle"
        Me.tbx_mpm_er_detalle.Size = New System.Drawing.Size(264, 77)
        Me.tbx_mpm_er_detalle.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(10, 130)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(226, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Detalle (opcional)"
        '
        'Form_mapa_potencial_minero_eliminar_registro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(284, 260)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Form_mapa_potencial_minero_eliminar_registro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Eliminar registro"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbx_mpm_er_codigo As System.Windows.Forms.TextBox
    Friend WithEvents btn_mpm_er_buscar As System.Windows.Forms.Button
    Friend WithEvents btn_mpm_er_eliminar As System.Windows.Forms.Button
    Friend WithEvents lbl_mpm_er_datos As System.Windows.Forms.Label
    Friend WithEvents tbx_mpm_er_detalle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
