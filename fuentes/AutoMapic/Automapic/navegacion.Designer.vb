<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class navegacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(navegacion))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_back = New System.Windows.Forms.Button()
        Me.btn_next = New System.Windows.Forms.Button()
        Me.pnl_container = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_back, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_next, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.pnl_container, 0, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(491, 580)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 3)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 2.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(483, 2)
        Me.Label1.TabIndex = 0
        '
        'btn_back
        '
        Me.btn_back.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_back.Dock = System.Windows.Forms.DockStyle.Left
        Me.btn_back.FlatAppearance.BorderSize = 0
        Me.btn_back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_back.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_back.Image = CType(resources.GetObject("btn_back.Image"), System.Drawing.Image)
        Me.btn_back.Location = New System.Drawing.Point(4, 9)
        Me.btn_back.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_back.Name = "btn_back"
        Me.btn_back.Size = New System.Drawing.Size(112, 25)
        Me.btn_back.TabIndex = 1
        Me.btn_back.Text = "atrás"
        Me.btn_back.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_back.UseVisualStyleBackColor = True
        '
        'btn_next
        '
        Me.btn_next.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_next.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_next.FlatAppearance.BorderSize = 0
        Me.btn_next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_next.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_next.Image = CType(resources.GetObject("btn_next.Image"), System.Drawing.Image)
        Me.btn_next.Location = New System.Drawing.Point(387, 9)
        Me.btn_next.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_next.Name = "btn_next"
        Me.btn_next.Size = New System.Drawing.Size(100, 25)
        Me.btn_next.TabIndex = 2
        Me.btn_next.Text = "adelante"
        Me.btn_next.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btn_next.UseVisualStyleBackColor = True
        '
        'pnl_container
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.pnl_container, 3)
        Me.pnl_container.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_container.Location = New System.Drawing.Point(4, 56)
        Me.pnl_container.Margin = New System.Windows.Forms.Padding(4)
        Me.pnl_container.Name = "pnl_container"
        Me.pnl_container.Size = New System.Drawing.Size(483, 520)
        Me.pnl_container.TabIndex = 3
        '
        'navegacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(491, 580)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "navegacion"
        Me.Text = "navegacion"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_back As System.Windows.Forms.Button
    Friend WithEvents btn_next As System.Windows.Forms.Button
    Friend WithEvents pnl_container As System.Windows.Forms.Panel
End Class
