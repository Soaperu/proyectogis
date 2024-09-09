<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_validacion_mapa_geologico_50k
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_validacion_mapa_geologico_50k))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbx_emg_pathgdb = New System.Windows.Forms.TextBox()
        Me.btn_emg_loadgdb = New System.Windows.Forms.Button()
        Me.cbx_emg_codhojas = New System.Windows.Forms.ComboBox()
        Me.btn_emg_procesar = New System.Windows.Forms.Button()
        Me.lbx_emg_codhojas = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_emg_dashboard = New System.Windows.Forms.Button()
        Me.ttp_emg = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_emg_pathgdb, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_emg_loadgdb, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_emg_codhojas, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_emg_procesar, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.lbx_emg_codhojas, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_emg_dashboard, 1, 11)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 14
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(284, 565)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(218, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccionar Geodatabase estandarizada"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(218, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Selecciona Hoja"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(3, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(218, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Hojas seleccionadas"
        '
        'tbx_emg_pathgdb
        '
        Me.tbx_emg_pathgdb.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_emg_pathgdb.Enabled = False
        Me.tbx_emg_pathgdb.Location = New System.Drawing.Point(3, 23)
        Me.tbx_emg_pathgdb.Name = "tbx_emg_pathgdb"
        Me.tbx_emg_pathgdb.Size = New System.Drawing.Size(218, 20)
        Me.tbx_emg_pathgdb.TabIndex = 3
        '
        'btn_emg_loadgdb
        '
        Me.btn_emg_loadgdb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_emg_loadgdb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_emg_loadgdb.Location = New System.Drawing.Point(227, 23)
        Me.btn_emg_loadgdb.Name = "btn_emg_loadgdb"
        Me.btn_emg_loadgdb.Size = New System.Drawing.Size(54, 19)
        Me.btn_emg_loadgdb.TabIndex = 4
        Me.btn_emg_loadgdb.Text = "..."
        Me.ttp_emg.SetToolTip(Me.btn_emg_loadgdb, "Seleccionar Geodatabase estandarizada")
        Me.btn_emg_loadgdb.UseVisualStyleBackColor = True
        '
        'cbx_emg_codhojas
        '
        Me.cbx_emg_codhojas.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_emg_codhojas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbx_emg_codhojas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_emg_codhojas, 2)
        Me.cbx_emg_codhojas.FormattingEnabled = True
        Me.cbx_emg_codhojas.Location = New System.Drawing.Point(3, 69)
        Me.cbx_emg_codhojas.Name = "cbx_emg_codhojas"
        Me.cbx_emg_codhojas.Size = New System.Drawing.Size(278, 21)
        Me.cbx_emg_codhojas.TabIndex = 5
        '
        'btn_emg_procesar
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_emg_procesar, 2)
        Me.btn_emg_procesar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_emg_procesar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_emg_procesar.Image = Global.Automapic.My.Resources.Resources.TableFieldsTurnAllOff16
        Me.btn_emg_procesar.Location = New System.Drawing.Point(3, 533)
        Me.btn_emg_procesar.Name = "btn_emg_procesar"
        Me.btn_emg_procesar.Size = New System.Drawing.Size(278, 24)
        Me.btn_emg_procesar.TabIndex = 6
        Me.btn_emg_procesar.Text = "Evaluar"
        Me.btn_emg_procesar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_emg_procesar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ttp_emg.SetToolTip(Me.btn_emg_procesar, "Iniciar evaluación")
        Me.btn_emg_procesar.UseVisualStyleBackColor = True
        '
        'lbx_emg_codhojas
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.lbx_emg_codhojas, 2)
        Me.lbx_emg_codhojas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbx_emg_codhojas.FormattingEnabled = True
        Me.lbx_emg_codhojas.Location = New System.Drawing.Point(3, 115)
        Me.lbx_emg_codhojas.Name = "lbx_emg_codhojas"
        Me.lbx_emg_codhojas.Size = New System.Drawing.Size(278, 102)
        Me.lbx_emg_codhojas.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label4, 2)
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 220)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(278, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "(*) Doble clic para eliminar hoja"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_emg_dashboard
        '
        Me.btn_emg_dashboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_emg_dashboard.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_emg_dashboard.Image = CType(resources.GetObject("btn_emg_dashboard.Image"), System.Drawing.Image)
        Me.btn_emg_dashboard.Location = New System.Drawing.Point(251, 497)
        Me.btn_emg_dashboard.Name = "btn_emg_dashboard"
        Me.btn_emg_dashboard.Size = New System.Drawing.Size(30, 30)
        Me.btn_emg_dashboard.TabIndex = 9
        Me.ttp_emg.SetToolTip(Me.btn_emg_dashboard, "Ver tablero de control")
        Me.btn_emg_dashboard.UseVisualStyleBackColor = True
        '
        'ttp_emg
        '
        Me.ttp_emg.IsBalloon = True
        '
        'Form_validacion_mapa_geologico_50k
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(284, 565)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form_validacion_mapa_geologico_50k"
        Me.Text = "Form_validacion_mapa_geologico_50k"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbx_emg_pathgdb As System.Windows.Forms.TextBox
    Friend WithEvents btn_emg_loadgdb As System.Windows.Forms.Button
    Friend WithEvents cbx_emg_codhojas As System.Windows.Forms.ComboBox
    Friend WithEvents btn_emg_procesar As System.Windows.Forms.Button
    Friend WithEvents lbx_emg_codhojas As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_emg_dashboard As System.Windows.Forms.Button
    Friend WithEvents ttp_emg As System.Windows.Forms.ToolTip
End Class
