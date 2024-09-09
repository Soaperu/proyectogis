<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_mapa_peligros_geologicos_registro
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_mapa_peligros_geologicos_registro))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbx_pgr_sector = New System.Windows.Forms.TextBox()
        Me.tbx_pgr_emisor = New System.Windows.Forms.TextBox()
        Me.tbx_pgr_asunto = New System.Windows.Forms.TextBox()
        Me.tbx_pgr_responsable = New System.Windows.Forms.TextBox()
        Me.tbx_pgr_observacion = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtp_pgr_fechasolicitud = New System.Windows.Forms.DateTimePicker()
        Me.dtp_pgr_fechaasignacion = New System.Windows.Forms.DateTimePicker()
        Me.dtp_pgr_fechaatencion = New System.Windows.Forms.DateTimePicker()
        Me.cbx_pgr_estadoatencion = New System.Windows.Forms.ComboBox()
        Me.tbx_pgr_documento = New System.Windows.Forms.TextBox()
        Me.tbx_pgr_codigosolicitud = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbx_pgr_solicitante = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbx_pgr_tipoinformacion = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dgv_pgr_tablahistorica = New System.Windows.Forms.DataGridView()
        Me.cbx_pgr_departamentos = New System.Windows.Forms.ComboBox()
        Me.btn_pgr_registrar = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgv_pgr_tablahistorica, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_pgr_registrar, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(350, 628)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.AutoScrollMinSize = New System.Drawing.Size(0, 650)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 18)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 0, 19)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_sector, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_emisor, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_asunto, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_responsable, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_observacion, 0, 20)
        Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 21)
        Me.TableLayoutPanel1.Controls.Add(Me.dtp_pgr_fechasolicitud, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.dtp_pgr_fechaasignacion, 1, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.dtp_pgr_fechaatencion, 1, 18)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_pgr_estadoatencion, 1, 17)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_documento, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.tbx_pgr_codigosolicitud, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label13, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_pgr_solicitante, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label14, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_pgr_tipoinformacion, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.Label15, 0, 23)
        Me.TableLayoutPanel1.Controls.Add(Me.dgv_pgr_tablahistorica, 0, 24)
        Me.TableLayoutPanel1.Controls.Add(Me.cbx_pgr_departamentos, 0, 22)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 25
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(346, 596)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(367, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código de solicitud (opcional)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(2, 27)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(367, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Sector"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(2, 67)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(457, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Documento"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 109)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(367, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Fecha de la solicitud"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(2, 173)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(367, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Emisor de documento"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(2, 213)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(367, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Asunto"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(2, 336)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(367, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Responsable de atención"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 378)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(367, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Fecha de asignación de solicitud"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(2, 402)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(367, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Estado de la atención"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(2, 426)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(367, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Fecha de finalización de atención de solicitud"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label11, 2)
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(2, 448)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(457, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Detalles u observaciones de la atención de la solicitud"
        '
        'tbx_pgr_sector
        '
        Me.tbx_pgr_sector.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_sector, 2)
        Me.tbx_pgr_sector.Location = New System.Drawing.Point(2, 42)
        Me.tbx_pgr_sector.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_sector.Name = "tbx_pgr_sector"
        Me.tbx_pgr_sector.Size = New System.Drawing.Size(457, 20)
        Me.tbx_pgr_sector.TabIndex = 13
        '
        'tbx_pgr_emisor
        '
        Me.tbx_pgr_emisor.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_emisor, 2)
        Me.tbx_pgr_emisor.Location = New System.Drawing.Point(2, 188)
        Me.tbx_pgr_emisor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_emisor.Name = "tbx_pgr_emisor"
        Me.tbx_pgr_emisor.Size = New System.Drawing.Size(457, 20)
        Me.tbx_pgr_emisor.TabIndex = 15
        '
        'tbx_pgr_asunto
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_asunto, 2)
        Me.tbx_pgr_asunto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_pgr_asunto.Location = New System.Drawing.Point(2, 228)
        Me.tbx_pgr_asunto.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_asunto.Multiline = True
        Me.tbx_pgr_asunto.Name = "tbx_pgr_asunto"
        Me.tbx_pgr_asunto.Size = New System.Drawing.Size(457, 61)
        Me.tbx_pgr_asunto.TabIndex = 16
        '
        'tbx_pgr_responsable
        '
        Me.tbx_pgr_responsable.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_responsable, 2)
        Me.tbx_pgr_responsable.Location = New System.Drawing.Point(2, 351)
        Me.tbx_pgr_responsable.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_responsable.Name = "tbx_pgr_responsable"
        Me.tbx_pgr_responsable.Size = New System.Drawing.Size(457, 20)
        Me.tbx_pgr_responsable.TabIndex = 17
        '
        'tbx_pgr_observacion
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_observacion, 2)
        Me.tbx_pgr_observacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbx_pgr_observacion.Location = New System.Drawing.Point(2, 463)
        Me.tbx_pgr_observacion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_observacion.Multiline = True
        Me.tbx_pgr_observacion.Name = "tbx_pgr_observacion"
        Me.tbx_pgr_observacion.Size = New System.Drawing.Size(457, 61)
        Me.tbx_pgr_observacion.TabIndex = 18
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(2, 529)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(367, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Seleccione el departamento"
        '
        'dtp_pgr_fechasolicitud
        '
        Me.dtp_pgr_fechasolicitud.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtp_pgr_fechasolicitud.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_pgr_fechasolicitud.Location = New System.Drawing.Point(373, 106)
        Me.dtp_pgr_fechasolicitud.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtp_pgr_fechasolicitud.Name = "dtp_pgr_fechasolicitud"
        Me.dtp_pgr_fechasolicitud.Size = New System.Drawing.Size(86, 20)
        Me.dtp_pgr_fechasolicitud.TabIndex = 19
        '
        'dtp_pgr_fechaasignacion
        '
        Me.dtp_pgr_fechaasignacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtp_pgr_fechaasignacion.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_pgr_fechaasignacion.Location = New System.Drawing.Point(373, 375)
        Me.dtp_pgr_fechaasignacion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtp_pgr_fechaasignacion.Name = "dtp_pgr_fechaasignacion"
        Me.dtp_pgr_fechaasignacion.Size = New System.Drawing.Size(86, 20)
        Me.dtp_pgr_fechaasignacion.TabIndex = 20
        '
        'dtp_pgr_fechaatencion
        '
        Me.dtp_pgr_fechaatencion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtp_pgr_fechaatencion.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_pgr_fechaatencion.Location = New System.Drawing.Point(373, 423)
        Me.dtp_pgr_fechaatencion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dtp_pgr_fechaatencion.Name = "dtp_pgr_fechaatencion"
        Me.dtp_pgr_fechaatencion.Size = New System.Drawing.Size(86, 20)
        Me.dtp_pgr_fechaatencion.TabIndex = 22
        '
        'cbx_pgr_estadoatencion
        '
        Me.cbx_pgr_estadoatencion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbx_pgr_estadoatencion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_pgr_estadoatencion.FormattingEnabled = True
        Me.cbx_pgr_estadoatencion.Location = New System.Drawing.Point(373, 399)
        Me.cbx_pgr_estadoatencion.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbx_pgr_estadoatencion.Name = "cbx_pgr_estadoatencion"
        Me.cbx_pgr_estadoatencion.Size = New System.Drawing.Size(86, 21)
        Me.cbx_pgr_estadoatencion.TabIndex = 21
        '
        'tbx_pgr_documento
        '
        Me.tbx_pgr_documento.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.tbx_pgr_documento, 2)
        Me.tbx_pgr_documento.Location = New System.Drawing.Point(2, 82)
        Me.tbx_pgr_documento.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_documento.Name = "tbx_pgr_documento"
        Me.tbx_pgr_documento.Size = New System.Drawing.Size(457, 20)
        Me.tbx_pgr_documento.TabIndex = 14
        '
        'tbx_pgr_codigosolicitud
        '
        Me.tbx_pgr_codigosolicitud.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_pgr_codigosolicitud.Location = New System.Drawing.Point(373, 2)
        Me.tbx_pgr_codigosolicitud.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbx_pgr_codigosolicitud.Name = "tbx_pgr_codigosolicitud"
        Me.tbx_pgr_codigosolicitud.Size = New System.Drawing.Size(86, 20)
        Me.tbx_pgr_codigosolicitud.TabIndex = 12
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(3, 131)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(365, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Solicitante"
        '
        'cbx_pgr_solicitante
        '
        Me.cbx_pgr_solicitante.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_pgr_solicitante, 2)
        Me.cbx_pgr_solicitante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_pgr_solicitante.FormattingEnabled = True
        Me.cbx_pgr_solicitante.Location = New System.Drawing.Point(3, 147)
        Me.cbx_pgr_solicitante.Name = "cbx_pgr_solicitante"
        Me.cbx_pgr_solicitante.Size = New System.Drawing.Size(455, 21)
        Me.cbx_pgr_solicitante.TabIndex = 26
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(3, 294)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(365, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Tipo de información"
        '
        'cbx_pgr_tipoinformacion
        '
        Me.cbx_pgr_tipoinformacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_pgr_tipoinformacion, 2)
        Me.cbx_pgr_tipoinformacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_pgr_tipoinformacion.FormattingEnabled = True
        Me.cbx_pgr_tipoinformacion.Location = New System.Drawing.Point(3, 310)
        Me.cbx_pgr_tipoinformacion.Name = "cbx_pgr_tipoinformacion"
        Me.cbx_pgr_tipoinformacion.Size = New System.Drawing.Size(455, 21)
        Me.cbx_pgr_tipoinformacion.TabIndex = 28
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label15, 2)
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(3, 571)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(455, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Seleccione la coordenada a registrar"
        '
        'dgv_pgr_tablahistorica
        '
        Me.dgv_pgr_tablahistorica.AllowUserToAddRows = False
        Me.dgv_pgr_tablahistorica.AllowUserToDeleteRows = False
        Me.dgv_pgr_tablahistorica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_pgr_tablahistorica.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgv_pgr_tablahistorica.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv_pgr_tablahistorica.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_pgr_tablahistorica.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_pgr_tablahistorica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel1.SetColumnSpan(Me.dgv_pgr_tablahistorica, 2)
        Me.dgv_pgr_tablahistorica.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_pgr_tablahistorica.EnableHeadersVisualStyles = False
        Me.dgv_pgr_tablahistorica.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgv_pgr_tablahistorica.Location = New System.Drawing.Point(2, 586)
        Me.dgv_pgr_tablahistorica.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dgv_pgr_tablahistorica.Name = "dgv_pgr_tablahistorica"
        Me.dgv_pgr_tablahistorica.ReadOnly = True
        Me.dgv_pgr_tablahistorica.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_pgr_tablahistorica.RowHeadersVisible = False
        Me.dgv_pgr_tablahistorica.RowTemplate.Height = 24
        Me.dgv_pgr_tablahistorica.ShowEditingIcon = False
        Me.dgv_pgr_tablahistorica.Size = New System.Drawing.Size(457, 192)
        Me.dgv_pgr_tablahistorica.TabIndex = 24
        '
        'cbx_pgr_departamentos
        '
        Me.cbx_pgr_departamentos.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.SetColumnSpan(Me.cbx_pgr_departamentos, 2)
        Me.cbx_pgr_departamentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_pgr_departamentos.FormattingEnabled = True
        Me.cbx_pgr_departamentos.Location = New System.Drawing.Point(3, 545)
        Me.cbx_pgr_departamentos.Name = "cbx_pgr_departamentos"
        Me.cbx_pgr_departamentos.Size = New System.Drawing.Size(455, 21)
        Me.cbx_pgr_departamentos.TabIndex = 30
        '
        'btn_pgr_registrar
        '
        Me.btn_pgr_registrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_pgr_registrar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_pgr_registrar.Image = CType(resources.GetObject("btn_pgr_registrar.Image"), System.Drawing.Image)
        Me.btn_pgr_registrar.Location = New System.Drawing.Point(2, 602)
        Me.btn_pgr_registrar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btn_pgr_registrar.Name = "btn_pgr_registrar"
        Me.btn_pgr_registrar.Size = New System.Drawing.Size(346, 24)
        Me.btn_pgr_registrar.TabIndex = 11
        Me.btn_pgr_registrar.Text = "Registrar atención"
        Me.btn_pgr_registrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_pgr_registrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_pgr_registrar.UseVisualStyleBackColor = True
        '
        'Form_mapa_peligros_geologicos_registro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(350, 628)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Form_mapa_peligros_geologicos_registro"
        Me.Text = "Registrar atencion"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dgv_pgr_tablahistorica, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbx_pgr_sector As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pgr_emisor As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pgr_asunto As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pgr_responsable As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pgr_observacion As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dgv_pgr_tablahistorica As System.Windows.Forms.DataGridView
    Friend WithEvents dtp_pgr_fechasolicitud As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_pgr_fechaasignacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_pgr_fechaatencion As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbx_pgr_estadoatencion As System.Windows.Forms.ComboBox
    Friend WithEvents tbx_pgr_documento As System.Windows.Forms.TextBox
    Friend WithEvents tbx_pgr_codigosolicitud As System.Windows.Forms.TextBox
    Friend WithEvents btn_pgr_registrar As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbx_pgr_solicitante As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbx_pgr_tipoinformacion As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbx_pgr_departamentos As System.Windows.Forms.ComboBox
End Class
