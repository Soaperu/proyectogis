<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_opcionplanos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_opcionplanos))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lbldato = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Check_dato4 = New System.Windows.Forms.CheckBox()
        Me.Check_dato3 = New System.Windows.Forms.CheckBox()
        Me.Check_dato1 = New System.Windows.Forms.CheckBox()
        Me.Check_dato2 = New System.Windows.Forms.CheckBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.lbldato)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 77)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(405, 218)
        Me.GroupBox2.TabIndex = 149
        Me.GroupBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.Location = New System.Drawing.Point(50, 167)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 28)
        Me.Button1.TabIndex = 158
        '
        'lbldato
        '
        Me.lbldato.AutoSize = True
        Me.lbldato.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lbldato.Location = New System.Drawing.Point(13, 16)
        Me.lbldato.Name = "lbldato"
        Me.lbldato.Size = New System.Drawing.Size(0, 13)
        Me.lbldato.TabIndex = 157
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Check_dato4)
        Me.GroupBox1.Controls.Add(Me.Check_dato3)
        Me.GroupBox1.Controls.Add(Me.Check_dato1)
        Me.GroupBox1.Controls.Add(Me.Check_dato2)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(377, 120)
        Me.GroupBox1.TabIndex = 156
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Seleccione la opción a consultar :"
        '
        'Check_dato4
        '
        Me.Check_dato4.AutoSize = True
        Me.Check_dato4.Location = New System.Drawing.Point(8, 88)
        Me.Check_dato4.Name = "Check_dato4"
        Me.Check_dato4.Size = New System.Drawing.Size(367, 17)
        Me.Check_dato4.TabIndex = 9
        Me.Check_dato4.Text = "D.M. Vig. y Ext.  (a Publicar,  Publicados LD aun no peticionables) + AR "
        Me.Check_dato4.UseVisualStyleBackColor = True
        '
        'Check_dato3
        '
        Me.Check_dato3.AutoSize = True
        Me.Check_dato3.Location = New System.Drawing.Point(8, 65)
        Me.Check_dato3.Name = "Check_dato3"
        Me.Check_dato3.Size = New System.Drawing.Size(327, 17)
        Me.Check_dato3.TabIndex = 8
        Me.Check_dato3.Text = "Derechos Mineros Vigentes y Extinguidos (a Publicar  LD) + AR "
        Me.Check_dato3.UseVisualStyleBackColor = True
        '
        'Check_dato1
        '
        Me.Check_dato1.AutoSize = True
        Me.Check_dato1.Location = New System.Drawing.Point(8, 19)
        Me.Check_dato1.Name = "Check_dato1"
        Me.Check_dato1.Size = New System.Drawing.Size(183, 17)
        Me.Check_dato1.TabIndex = 0
        Me.Check_dato1.Text = "Solo Derechos Mineros Vigentes "
        Me.Check_dato1.UseVisualStyleBackColor = True
        '
        'Check_dato2
        '
        Me.Check_dato2.AutoSize = True
        Me.Check_dato2.Location = New System.Drawing.Point(8, 42)
        Me.Check_dato2.Name = "Check_dato2"
        Me.Check_dato2.Size = New System.Drawing.Size(282, 17)
        Me.Check_dato2.TabIndex = 1
        Me.Check_dato2.Text = "Derechos Mineros Vigentes y Äreas Restringidas (AR) "
        Me.Check_dato2.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(245, 167)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(104, 28)
        Me.btnAceptar.TabIndex = 155
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(403, 59)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 148
        Me.PictureBox1.TabStop = False
        '
        'frm_opcionplanos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 309)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "frm_opcionplanos"
        Me.Text = "frm_opcionplanos"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbldato As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Check_dato4 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato3 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato1 As System.Windows.Forms.CheckBox
    Friend WithEvents Check_dato2 As System.Windows.Forms.CheckBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
