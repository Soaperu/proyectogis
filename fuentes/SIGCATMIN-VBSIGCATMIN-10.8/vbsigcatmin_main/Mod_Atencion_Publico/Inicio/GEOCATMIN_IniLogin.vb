Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports stdole

'Option Explicit On
'Imports Oracle.DataAccess.Client
Imports PORTAL_Clases
Imports PORTAL_Configuracion

Public Class GEOCATMIN_IniLogin
    Inherits System.Windows.Forms.Form
    Public pPropset As IPropertySet
    Public pWorkspace As IWorkspace
    Public clsData As New cls_Utilidades
    Friend WithEvents img_principal As System.Windows.Forms.PictureBox
    Friend WithEvents img_seguridad As System.Windows.Forms.PictureBox
    Friend WithEvents nombre_sistema As System.Windows.Forms.Label
    Friend WithEvents descripcion_nombre_sistema_b As System.Windows.Forms.Label
    Friend WithEvents descripcion_nombre_sistema_a As System.Windows.Forms.Label
    Friend WithEvents indicacion As System.Windows.Forms.Label
    Friend WithEvents img_user As System.Windows.Forms.PictureBox
    Friend WithEvents img_key As System.Windows.Forms.PictureBox
    Private cls_Oracle As New cls_Oracle


#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents TXTUSUARIO As System.Windows.Forms.TextBox
    Friend WithEvents txtContrasena As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GEOCATMIN_IniLogin))
        Me.TXTUSUARIO = New System.Windows.Forms.TextBox()
        Me.txtContrasena = New System.Windows.Forms.TextBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.img_principal = New System.Windows.Forms.PictureBox()
        Me.img_seguridad = New System.Windows.Forms.PictureBox()
        Me.nombre_sistema = New System.Windows.Forms.Label()
        Me.descripcion_nombre_sistema_b = New System.Windows.Forms.Label()
        Me.descripcion_nombre_sistema_a = New System.Windows.Forms.Label()
        Me.indicacion = New System.Windows.Forms.Label()
        Me.img_user = New System.Windows.Forms.PictureBox()
        Me.img_key = New System.Windows.Forms.PictureBox()
        CType(Me.img_principal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_seguridad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_user, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_key, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TXTUSUARIO
        '
        Me.TXTUSUARIO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUSUARIO.ForeColor = System.Drawing.Color.DimGray
        Me.TXTUSUARIO.Location = New System.Drawing.Point(196, 118)
        Me.TXTUSUARIO.Name = "TXTUSUARIO"
        Me.TXTUSUARIO.Size = New System.Drawing.Size(204, 22)
        Me.TXTUSUARIO.TabIndex = 2
        Me.TXTUSUARIO.Text = "usuario"
        '
        'txtContrasena
        '
        Me.txtContrasena.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContrasena.ForeColor = System.Drawing.Color.DimGray
        Me.txtContrasena.Location = New System.Drawing.Point(196, 145)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtContrasena.Size = New System.Drawing.Size(204, 22)
        Me.txtContrasena.TabIndex = 3
        Me.txtContrasena.Text = "password"
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAceptar.Location = New System.Drawing.Point(290, 178)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(110, 25)
        Me.btnAceptar.TabIndex = 4
        Me.btnAceptar.Text = "Ingresar"
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancelar.Location = New System.Drawing.Point(174, 178)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(110, 25)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Cancelar"
        '
        'img_principal
        '
        Me.img_principal.Image = Global.SIGCATMIN.My.Resources.Resources.login
        Me.img_principal.Location = New System.Drawing.Point(28, 79)
        Me.img_principal.Name = "img_principal"
        Me.img_principal.Size = New System.Drawing.Size(129, 132)
        Me.img_principal.TabIndex = 6
        Me.img_principal.TabStop = False
        '
        'img_seguridad
        '
        Me.img_seguridad.Image = Global.SIGCATMIN.My.Resources.Resources.security
        Me.img_seguridad.Location = New System.Drawing.Point(232, 68)
        Me.img_seguridad.Name = "img_seguridad"
        Me.img_seguridad.Size = New System.Drawing.Size(200, 172)
        Me.img_seguridad.TabIndex = 13
        Me.img_seguridad.TabStop = False
        '
        'nombre_sistema
        '
        Me.nombre_sistema.AutoSize = True
        Me.nombre_sistema.Font = New System.Drawing.Font("Segoe UI Semilight", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nombre_sistema.ForeColor = System.Drawing.Color.DimGray
        Me.nombre_sistema.Location = New System.Drawing.Point(20, 21)
        Me.nombre_sistema.Name = "nombre_sistema"
        Me.nombre_sistema.Size = New System.Drawing.Size(198, 46)
        Me.nombre_sistema.TabIndex = 10
        Me.nombre_sistema.Text = "SIGCATMIN"
        '
        'descripcion_nombre_sistema_b
        '
        Me.descripcion_nombre_sistema_b.AutoSize = True
        Me.descripcion_nombre_sistema_b.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.descripcion_nombre_sistema_b.ForeColor = System.Drawing.Color.Gray
        Me.descripcion_nombre_sistema_b.Location = New System.Drawing.Point(216, 46)
        Me.descripcion_nombre_sistema_b.Name = "descripcion_nombre_sistema_b"
        Me.descripcion_nombre_sistema_b.Size = New System.Drawing.Size(186, 13)
        Me.descripcion_nombre_sistema_b.TabIndex = 11
        Me.descripcion_nombre_sistema_b.Text = "Y DE CATASTRO MINERO NACIONAL"
        Me.descripcion_nombre_sistema_b.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'descripcion_nombre_sistema_a
        '
        Me.descripcion_nombre_sistema_a.AutoSize = True
        Me.descripcion_nombre_sistema_a.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.descripcion_nombre_sistema_a.ForeColor = System.Drawing.Color.Gray
        Me.descripcion_nombre_sistema_a.Location = New System.Drawing.Point(212, 31)
        Me.descripcion_nombre_sistema_a.Name = "descripcion_nombre_sistema_a"
        Me.descripcion_nombre_sistema_a.Size = New System.Drawing.Size(197, 13)
        Me.descripcion_nombre_sistema_a.TabIndex = 12
        Me.descripcion_nombre_sistema_a.Text = "SISTEMA INFORMACIÓN GEOLÓGICO"
        Me.descripcion_nombre_sistema_a.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'indicacion
        '
        Me.indicacion.AutoSize = True
        Me.indicacion.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.indicacion.ForeColor = System.Drawing.Color.DimGray
        Me.indicacion.Location = New System.Drawing.Point(171, 88)
        Me.indicacion.Name = "indicacion"
        Me.indicacion.Size = New System.Drawing.Size(91, 19)
        Me.indicacion.TabIndex = 7
        Me.indicacion.Text = "Iniciar sesión"
        '
        'img_user
        '
        Me.img_user.Image = CType(resources.GetObject("img_user.Image"), System.Drawing.Image)
        Me.img_user.Location = New System.Drawing.Point(174, 118)
        Me.img_user.Name = "img_user"
        Me.img_user.Padding = New System.Windows.Forms.Padding(2)
        Me.img_user.Size = New System.Drawing.Size(20, 21)
        Me.img_user.TabIndex = 8
        Me.img_user.TabStop = False
        '
        'img_key
        '
        Me.img_key.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.img_key.Image = CType(resources.GetObject("img_key.Image"), System.Drawing.Image)
        Me.img_key.Location = New System.Drawing.Point(174, 145)
        Me.img_key.Name = "img_key"
        Me.img_key.Padding = New System.Windows.Forms.Padding(2)
        Me.img_key.Size = New System.Drawing.Size(20, 21)
        Me.img_key.TabIndex = 9
        Me.img_key.TabStop = False
        '
        'GEOCATMIN_IniLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(430, 240)
        Me.Controls.Add(Me.img_key)
        Me.Controls.Add(Me.img_user)
        Me.Controls.Add(Me.indicacion)
        Me.Controls.Add(Me.descripcion_nombre_sistema_b)
        Me.Controls.Add(Me.descripcion_nombre_sistema_a)
        Me.Controls.Add(Me.nombre_sistema)
        Me.Controls.Add(Me.img_principal)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtContrasena)
        Me.Controls.Add(Me.TXTUSUARIO)
        Me.Controls.Add(Me.img_seguridad)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GEOCATMIN_IniLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingreso de Usuario"
        CType(Me.img_principal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_seguridad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_user, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_key, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'DialogResult = Windows.Forms.DialogResult.Cancel ' DialogResult.Cancel
        control_cancel = 1
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        PT_Validar_Acceso()
        'PT_Validar_Acceso2()
    End Sub
    Public Sub Pintar_Formulario()
        Me.BackColor = Drawing.Color.FromArgb(242, 242, 240)
    End Sub
    Private Sub PT_Validar_Acceso_PROD()
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "SIGCATMIN")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "SIGCATMIN")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text

            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)
            Select Case glo_Server_SDE
                Case "10.102.0.12"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = "BDGINGE"
                    glo_Password_SDE = "BDGINGE"
                Case "SRVDESA03", "10.102.11.35"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case Else '"10.102.0.30"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Servidor: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            Select Case gstrDatabase
                'Case "DESA"
                Case "GAMMAD"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Producción"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case "ORACLE"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Srv BD: Producción / SrvGIS: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            DialogResult = Windows.Forms.DialogResult.OK ' DialogResult.OK
        End If
    End Sub

    Private Sub PT_Validar_Acceso()  'CAMBIAR PT_Validar_Acceso_PROD POR PT_Validar_Acceso PARA EJECUTAR
        val_acceso = ""
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[SIGCATMIN]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text
            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)
            val_acceso = "1"
            Select Case glo_Server_SDE
                Case "10.102.0.12"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = "BDGINGE"
                    glo_Password_SDE = "BDGINGE"
                Case "SRVDESA03", "10.102.11.35"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case Else '"10.102.0.30"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Servidor: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            Select Case gstrDatabase
                ' Case "DESA"
                Case "GAMMA02"
                    'Case "GAMMAD"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Produccion"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case "ORACLE"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Srv BD: Producción / SrvGIS: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            DialogResult = Windows.Forms.DialogResult.OK ' DialogResult.OK
        End If

        ' @alfa
        Try
            Kill(glo_pathTEMP & "*.*")
        Catch ex As Exception

        End Try


    End Sub


    Private Sub PT_Validar_Acceso2()  'CAMBIAR PT_Validar_Acceso_PROD POR PT_Validar_Acceso PARA EJECUTAR
        Dim lostrContrasenaInvalida As String
        pgloUsuConexionOracle = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabase)
        lostrContrasenaInvalida = ContrasenaInvalida(Trim(TXTUSUARIO.Text), Trim(txtContrasena.Text))
        'Asegurarse de que el usuario ingrese el Nombre de Usuario y Contraseña
        If TXTUSUARIO.Text.Length = 0 Then
            MsgBox("Debe ingresar un Usuario", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar una Contraseña", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            txtContrasena.Focus()
        ElseIf lostrContrasenaInvalida = "1" Then
            'El Nombre de Usuario/Contraseña es inválido
            MsgBox("Nombre de Usuario/Contraseña inválida", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "2" Then
            MsgBox("No tiene permiso para acceder al Sistema", MsgBoxStyle.Exclamation, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        ElseIf lostrContrasenaInvalida = "3" Then
            MsgBox("Error de Conexión a la Base de Datos Oracle, ingrese Usuario y Clave válidos " & vbNewLine & "Intente de Nuevo", MsgBoxStyle.Information, "[BDGeocatmin]")
            TXTUSUARIO.Focus()
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text
            gstrUsuarioClave = Me.txtContrasena.Text
            pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)

            Select Case glo_Server_SDE
                Case "10.102.0.12"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = "BDGINGE"
                    glo_Password_SDE = "BDGINGE"
                Case "SRVDESA03", "10.102.11.35"
                    glo_Desarrollo_BD = "Servidor: Desarrollo"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case Else '"10.102.0.30"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Servidor: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            Select Case gstrDatabase
                ' Case "DESA"
                Case "GAMMAD"
                    glo_Desarrollo_BD = "Srv BD: Desarrollo / SrvGIS: Produccion"
                    glo_User_SDE = gstrUsuarioAcceso
                    glo_Password_SDE = gstrUsuarioClave
                Case "ORACLE"
                    If glo_Version_SDE = "SDE.DEFAULT" Then
                        glo_Desarrollo_BD = "Srv BD: Producción / SrvGIS: Producción"
                        glo_User_SDE = gstrUsuarioAcceso
                        glo_Password_SDE = gstrUsuarioClave
                    End If
            End Select
            DialogResult = Windows.Forms.DialogResult.OK ' DialogResult.OK
        End If
    End Sub


    Public Sub PT_Validar_Acceso1()

        'ESTO PARA CONECTARSE DIRECTAMENTE FECHA 03/11/2014
        Dim lostrValidando As String = ""
        Dim m_Codigo_NumReg As Integer
        Dim clave_valida As Boolean
        If Me.TXTUSUARIO.Text.Length = 0 Then



            MsgBox("Debe ingresar Usuario", MsgBoxStyle.Exclamation, "SIGCATMIN")
            Me.TXTUSUARIO.Focus()
            Exit Sub
        ElseIf Me.txtContrasena.Text.Length = 0 Then
            MsgBox("Debe ingresar Contraseña", MsgBoxStyle.Exclamation, "SIGCATMIN")
            txtContrasena.Focus()
            Exit Sub
        Else
            gstrUsuarioAcceso = Me.TXTUSUARIO.Text.ToUpper
            gstrUsuarioClave = Me.txtContrasena.Text.ToUpper
            glo_User_SDE = Me.TXTUSUARIO.Text.ToUpper
            glo_Password_SDE = Me.txtContrasena.Text.ToUpper

        End If
        Try
            pPropset = New PropertySet


            '  pgloUsuConexionGIS = clsData.ConxGis(TXTUSUARIO.Text, txtContrasena.Text, gstrDatabaseGIS)

            Dim c_usuario As String
            glo_User_SDE = Me.TXTUSUARIO.Text.ToUpper
            glo_Password_SDE = Me.txtContrasena.Text.ToUpper

            pPropset.SetProperty("CONNECTSTRING", "Provider = MSDAORA.1; Data source = ORACLE; User ID = " & _
                                  glo_User_SDE & ";Password = " & glo_Password_SDE)
            'Creando sentencias del OLEDB
            Dim pWorkspaceFact As IWorkspaceFactory
            pWorkspaceFact = New OLEDBWorkspaceFactory
            pWorkspace = pWorkspaceFact.Open(pPropset, 0)
            Dim pFeatureWorkspace As IFeatureWorkspace
            pFeatureWorkspace = pWorkspace
            Dim pQueryDef As IQueryDef
            pQueryDef = pFeatureWorkspace.CreateQueryDef
            'consulta
            pQueryDef.Tables = " SG_M_USUARIOS "
            pQueryDef.SubFields = " US_LOGUSE, US_NOMUSE "
            pQueryDef.WhereClause = " US_LOGUSE = '" & gstrUsuarioAcceso & "'"
            Dim cursor_filas As ICursor
            Dim filas_dm As IRow
            cursor_filas = pQueryDef.Evaluate
            filas_dm = cursor_filas.NextRow
            Dim i As Long
            i = 0
            m_Codigo_NumReg = i
            Do Until filas_dm Is Nothing
                c_usuario = filas_dm.Value(0) & ""
                c_user_name = filas_dm.Value(1) & ""
                i = i + 1
                filas_dm = cursor_filas.NextRow
                m_Codigo_NumReg = i
            Loop
            If m_Codigo_NumReg = 0 Then
                MsgBox("Usuario es Incorrecto para realizar la consulta", vbCritical, "OBSERVACION...")
                Exit Sub
            End If
            clave_valida = True


            If clave_valida = True Then

                DialogResult = Windows.Forms.DialogResult.OK ''
            End If

        Catch ex As Exception
            clave_valida = False
            MsgBox("El Usuario y/o Password es incorrecto para acceder a la Aplicación," & _
                   "Verificar si sus Datos son Correctos", vbInformation, "[ SIGCATMIN ]")
            glo_Validado = 0
            Exit Sub
        End Try
        glo_Validado = 1

        '    Me.Close()
    End Sub


    Private Function ContrasenaInvalida(ByVal pastrUsuario As String, ByVal pastrContrasena As String) As String
        Dim cls_Conexion As New cls_Oracle ' clsBD_Seguridad
        Dim lodtbUsuario As New DataTable
        Dim lodtbAcceso As New DataTable
        Try
            lodtbUsuario = cls_Conexion.F_Verifica_Usuario(TXTUSUARIO.Text.ToUpper, pgloUsuConexionOracle)
            If lodtbUsuario.Rows.Count = 1 Then
                gstrCodigo_Usuario = lodtbUsuario.Rows(0).Item("USUARIO")
                gstrNombre_Usuario = Texto_Alta_Baja(lodtbUsuario.Rows(0).Item("USERNAME"))
                gstr_Codigo_Oficina = lodtbUsuario.Rows(0).Item("OFICINA")


                'gstrCodigo_Usuario = "CQUI0543"
                'gstrNombre_Usuario = "CQUI0545"
                'gstr_Codigo_Oficina = "01"

                '   gstr_Codigo_Oficina = "65"
                'VALIDAD USUARIO DE REGIONES 'AUMENTADO ULTIMO
                ' Lima = 15     Tumbes = 24       Huancavelica = 09    Apurimac = 53
                '   gstr_Codigo_Oficina = "59"  'probando con oficina de prueba,luego comentar la linea
                Select Case gstr_Codigo_Oficina
                    Case "51"
                        cd_region_sele = "01"
                    Case "52"
                        cd_region_sele = "02"
                    Case "53"
                        cd_region_sele = "03"
                    Case "54"
                        cd_region_sele = "04"
                    Case "55"
                        cd_region_sele = "05"
                    Case "56"
                        cd_region_sele = "06"
                    Case "57"
                        cd_region_sele = "07"
                    Case "58"
                        cd_region_sele = "08"
                    Case "59"
                        cd_region_sele = "09"
                    Case "60"
                        cd_region_sele = "10"
                    Case "61"
                        cd_region_sele = "11"
                    Case "62"
                        cd_region_sele = "12"
                    Case "63"
                        cd_region_sele = "13"
                    Case "64"
                        cd_region_sele = "14"
                    Case "65"
                        cd_region_sele = "15"
                    Case "66"
                        cd_region_sele = "16"
                    Case "67"
                        cd_region_sele = "17"
                    Case "68"
                        cd_region_sele = "18"
                    Case "69"
                        cd_region_sele = "19"
                    Case "70"
                        cd_region_sele = "20"
                    Case "71"
                        cd_region_sele = "21"
                    Case "72"
                        cd_region_sele = "22"
                    Case "73"
                        cd_region_sele = "23"
                    Case "74"
                        cd_region_sele = "24"
                    Case "75"
                        cd_region_sele = "25"
                    Case "76"
                        cd_region_sele = "26"
                    Case Else
                        cd_region_sele = "00"  'PARA LIMA
                        cd_region_encontrado = "00"
                End Select
            End If

        Catch Ex As Exception
            Return "3"
        End Try
    End Function

    Private Sub txtContrasena_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContrasena.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            PT_Validar_Acceso()
            'PT_Validar_Acceso_ANT()
        End If
    End Sub

    Private Sub GEOCATMIN_IniLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Pintar_Formulario()
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class