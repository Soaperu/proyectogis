<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RadioCC
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_RadioCC))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRadio = New System.Windows.Forms.Label()
        Me.txtnorte_sim = New System.Windows.Forms.TextBox()
        Me.txteste_sim = New System.Windows.Forms.TextBox()
        Me.lbl_CoorN = New System.Windows.Forms.Label()
        Me.cbozonasim = New System.Windows.Forms.ComboBox()
        Me.lbl_CoorE = New System.Windows.Forms.Label()
        Me.cboradiosim = New System.Windows.Forms.ComboBox()
        Me.lblzona = New System.Windows.Forms.Label()
        Me.btngrafica_sim = New System.Windows.Forms.Button()
        Me.btncerrar = New System.Windows.Forms.Button()
        Me.lblcirculo = New System.Windows.Forms.Label()
        Me.btn_int_sim = New System.Windows.Forms.Button()
        Me.DataGridsim = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridsim, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblRadio, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtnorte_sim, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txteste_sim, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_CoorN, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cbozonasim, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_CoorE, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.cboradiosim, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblzona, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(22, 55)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(271, 141)
        Me.TableLayoutPanel1.TabIndex = 16
        '
        'lblRadio
        '
        Me.lblRadio.AutoSize = True
        Me.lblRadio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRadio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblRadio.Location = New System.Drawing.Point(3, 0)
        Me.lblRadio.Name = "lblRadio"
        Me.lblRadio.Size = New System.Drawing.Size(38, 13)
        Me.lblRadio.TabIndex = 0
        Me.lblRadio.Text = "Radio:"
        '
        'txtnorte_sim
        '
        Me.txtnorte_sim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtnorte_sim.Enabled = False
        Me.txtnorte_sim.Location = New System.Drawing.Point(57, 108)
        Me.txtnorte_sim.Name = "txtnorte_sim"
        Me.txtnorte_sim.Size = New System.Drawing.Size(211, 20)
        Me.txtnorte_sim.TabIndex = 5
        '
        'txteste_sim
        '
        Me.txteste_sim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteste_sim.Enabled = False
        Me.txteste_sim.Location = New System.Drawing.Point(57, 73)
        Me.txteste_sim.Name = "txteste_sim"
        Me.txteste_sim.Size = New System.Drawing.Size(211, 20)
        Me.txteste_sim.TabIndex = 3
        '
        'lbl_CoorN
        '
        Me.lbl_CoorN.AutoSize = True
        Me.lbl_CoorN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CoorN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl_CoorN.Location = New System.Drawing.Point(3, 105)
        Me.lbl_CoorN.Name = "lbl_CoorN"
        Me.lbl_CoorN.Size = New System.Drawing.Size(36, 13)
        Me.lbl_CoorN.TabIndex = 4
        Me.lbl_CoorN.Text = "Norte:"
        '
        'cbozonasim
        '
        Me.cbozonasim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbozonasim.Enabled = False
        Me.cbozonasim.FormattingEnabled = True
        Me.cbozonasim.Items.AddRange(New Object() {"17", "18", "19"})
        Me.cbozonasim.Location = New System.Drawing.Point(57, 38)
        Me.cbozonasim.Name = "cbozonasim"
        Me.cbozonasim.Size = New System.Drawing.Size(211, 21)
        Me.cbozonasim.TabIndex = 9
        '
        'lbl_CoorE
        '
        Me.lbl_CoorE.AutoSize = True
        Me.lbl_CoorE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CoorE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbl_CoorE.Location = New System.Drawing.Point(3, 70)
        Me.lbl_CoorE.Name = "lbl_CoorE"
        Me.lbl_CoorE.Size = New System.Drawing.Size(31, 13)
        Me.lbl_CoorE.TabIndex = 2
        Me.lbl_CoorE.Text = "Este:"
        '
        'cboradiosim
        '
        Me.cboradiosim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboradiosim.FormattingEnabled = True
        Me.cboradiosim.Items.AddRange(New Object() {"5000", "10000", "20000"})
        Me.cboradiosim.Location = New System.Drawing.Point(57, 3)
        Me.cboradiosim.Name = "cboradiosim"
        Me.cboradiosim.Size = New System.Drawing.Size(211, 21)
        Me.cboradiosim.TabIndex = 8
        '
        'lblzona
        '
        Me.lblzona.AutoSize = True
        Me.lblzona.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblzona.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblzona.Location = New System.Drawing.Point(3, 35)
        Me.lblzona.Name = "lblzona"
        Me.lblzona.Size = New System.Drawing.Size(35, 13)
        Me.lblzona.TabIndex = 6
        Me.lblzona.Text = "Zona:"
        '
        'btngrafica_sim
        '
        Me.btngrafica_sim.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btngrafica_sim.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btngrafica_sim.Enabled = False
        Me.btngrafica_sim.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btngrafica_sim.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngrafica_sim.ForeColor = System.Drawing.Color.White
        Me.btngrafica_sim.Location = New System.Drawing.Point(212, 537)
        Me.btngrafica_sim.Name = "btngrafica_sim"
        Me.btngrafica_sim.Size = New System.Drawing.Size(78, 24)
        Me.btngrafica_sim.TabIndex = 17
        Me.btngrafica_sim.Text = "Graficar"
        Me.btngrafica_sim.UseVisualStyleBackColor = False
        '
        'btncerrar
        '
        Me.btncerrar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.btncerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btncerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncerrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btncerrar.Location = New System.Drawing.Point(128, 537)
        Me.btncerrar.Name = "btncerrar"
        Me.btncerrar.Size = New System.Drawing.Size(78, 24)
        Me.btncerrar.TabIndex = 18
        Me.btncerrar.Text = "Cancel"
        Me.btncerrar.UseVisualStyleBackColor = False
        '
        'lblcirculo
        '
        Me.lblcirculo.AutoSize = True
        Me.lblcirculo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcirculo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblcirculo.Location = New System.Drawing.Point(25, 25)
        Me.lblcirculo.Name = "lblcirculo"
        Me.lblcirculo.Size = New System.Drawing.Size(236, 18)
        Me.lblcirculo.TabIndex = 19
        Me.lblcirculo.Text = "Ingrese datos del circulo simulado "
        '
        'btn_int_sim
        '
        Me.btn_int_sim.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btn_int_sim.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_int_sim.Enabled = False
        Me.btn_int_sim.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btn_int_sim.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_int_sim.ForeColor = System.Drawing.Color.White
        Me.btn_int_sim.Location = New System.Drawing.Point(79, 203)
        Me.btn_int_sim.Name = "btn_int_sim"
        Me.btn_int_sim.Size = New System.Drawing.Size(213, 24)
        Me.btn_int_sim.TabIndex = 21
        Me.btn_int_sim.Text = "Obtener integrantes simulados"
        Me.btn_int_sim.UseVisualStyleBackColor = False
        '
        'DataGridsim
        '
        Me.DataGridsim.AllowUserToAddRows = False
        Me.DataGridsim.AllowUserToDeleteRows = False
        Me.DataGridsim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridsim.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridsim.Location = New System.Drawing.Point(22, 232)
        Me.DataGridsim.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridsim.Name = "DataGridsim"
        Me.DataGridsim.RowTemplate.Height = 24
        Me.DataGridsim.Size = New System.Drawing.Size(271, 300)
        Me.DataGridsim.TabIndex = 22
        '
        'frm_RadioCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 572)
        Me.Controls.Add(Me.DataGridsim)
        Me.Controls.Add(Me.btn_int_sim)
        Me.Controls.Add(Me.lblcirculo)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btngrafica_sim)
        Me.Controls.Add(Me.btncerrar)
        Me.ForeColor = System.Drawing.SystemColors.Control
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_RadioCC"
        Me.Text = "Graficar circulo simulado"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.DataGridsim, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblRadio As System.Windows.Forms.Label
    Friend WithEvents txtnorte_sim As System.Windows.Forms.TextBox
    Friend WithEvents txteste_sim As System.Windows.Forms.TextBox
    Friend WithEvents lbl_CoorN As System.Windows.Forms.Label
    Friend WithEvents cbozonasim As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_CoorE As System.Windows.Forms.Label
    Friend WithEvents cboradiosim As System.Windows.Forms.ComboBox
    Friend WithEvents lblzona As System.Windows.Forms.Label
    Friend WithEvents btngrafica_sim As System.Windows.Forms.Button
    Friend WithEvents btncerrar As System.Windows.Forms.Button
    Friend WithEvents lblcirculo As System.Windows.Forms.Label
    Friend WithEvents btn_int_sim As System.Windows.Forms.Button
    Friend WithEvents DataGridsim As System.Windows.Forms.DataGridView
End Class
