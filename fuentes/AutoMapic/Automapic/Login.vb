Imports System.Drawing
Imports System.Windows.Forms
'Imports Newtonsoft.Json
'Imports System.Data.SQLite
'Imports System.Threading
Imports Automapic.SWLoginService
Imports System.ServiceModel
Imports System.Threading.Tasks
Imports System.Net.Mail
Imports Newtonsoft.Json

Public Class Login
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim waitTime As Integer = 1000
    Private Sub Login_load(sender As Object, e As EventArgs) Handles Me.Load
        pbx_login_loader.Image = Image.FromFile(_path_loader)
        pbx_login_loader.SizeMode = PictureBoxSizeMode.StretchImage

        Try
            params.Clear()
            Dim response = ExecuteGP(_tool_getPythonPath, params, _toolboxPath_automapic, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If

            python_path = responseJson.Item("response")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub LoginValidate(user As String, password As String)

    End Sub
    Private Sub text_box_consulta_uea_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        'Si se presiona la tecla enter y el boton buscar esta habilitado
        If e.KeyChar = Chr(13) Then
            'Llama a la funcion buscar
            Call btn_login_Click(sender, e)
        End If
    End Sub

    Private Async Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        tbx_user.Enabled = False
        tbx_pass.Enabled = False
        btn_login.Enabled = False
        pbx_login_loader.Visible = True
        lbl_login_log.Visible = True
        params.Clear()
        'bgw_login.RunWorkerAsync()
        Dim response = Await iniciar_sesion()
        If response.Item("status") = False Then
            tbx_user.Enabled = True
            tbx_pass.Enabled = True
            btn_login.Enabled = True
            pbx_login_loader.Visible = False
            lbl_login_log.Visible = False
            Cursor.Current = Cursors.Default
            RuntimeError.PythonError = response.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        ' Se carga el modulo
        Dim ModulosForm = New Modulos()
        Cursor.Current = Cursors.Default
        openFormByName(ModulosForm, Me.Parent)
    End Sub

    'Es posible devolver distintos tipos de objetos en una funcion Async
    Private Async Function iniciar_sesion() As Task(Of Dictionary(Of String, Object))
        Dim responseJson As New Dictionary(Of String, Object)
        Dim dt = New DataTable()
        Try


            Cursor.Current = Cursors.WaitCursor
            'Incluir proceso de validacion
            'params.Clear()

            lbl_login_log.Text = "Validando usuario..."


            Dim _message As String

            user = tbx_user.Text.ToLower()
            'pass = tbx_pass.Text

            Try
                Dim email = New MailAddress(user)
                user = email.User
            Catch ex As Exception
            End Try

            If __status__ = "Production" Then
                Dim Binding As BasicHttpsBinding = New BasicHttpsBinding()
                Dim endpoint As EndpointAddress = New EndpointAddress(_url_SWLoginService)
                Dim client As SWLoginClient = New SWLoginClient(Binding, endpoint)

                'Dim response As RespuestaSeguridadU = client.UsuarioActiveDirectory_Login1(user, tbx_pass.Text)
                Dim response As RespuestaSeguridadU = client.UsuarioLoginAD(user, tbx_pass.Text)

                responseJson.Add("status", response.Usuario_Valido)
                'responseJson.Add("status", True)
                client.Close()

                _message = "¡Credenciales incorrectas!"

                If Not response.Auditoria.AUTORIZADO Then
                    responseJson.Add("message", _message)
                    Return responseJson
                End If

                If Not response.Usuario_Valido Then
                    responseJson.Add("message", _message)
                    Return responseJson
                End If
            ElseIf __status__ = "Development" Then
                responseJson.Add("status", True)
            End If


            'Value to search as SQL Query - return first match
            'Dim SQLstr_validate As String = String.Format("SELECT COUNT(*) FROM TB_USER WHERE USER  ='{0}' AND PASSWORD = '{1}'", user, pass)
            'Dim SQLstr_modulos As String = String.Format("SELECT ID_MODULO, MODULO FROM VW_ACCESS WHERE USER = '{0}'", user)
            'Dim SQLstr_modulos_perfil As String = String.Format("SELECT ID_MODULO, ID_PERFIL FROM VW_ACCESS WHERE USER = '{0}'", user)
            'Dim SQLstr_modulos_manuales As String = String.Format("SELECT ID_MODULO, GUIDE FROM VW_ACCESS WHERE USER = '{0}'", user)
            'Dim SQLstr_login As String = String.Format("UPDATE TB_USER SET LOGIN = 1 WHERE USER = '{0}'", user)
            'Dim SQLstr_nameuser As String = String.Format("SELECT NOMBRES||' '||APEPAT FROM TB_USER WHERE LOWER(user) = LOWER('{0}')", user)
            'Dim SQLstr_logout As String = "UPDATE TB_USER SET LOGIN = 0"

            Dim ORAstr_modulos As String = String.Format("SELECT ID_MODULO, MODULO FROM ugeo1749.vw_osi_aut_access WHERE USUARIO = '{0}' AND ID_PERFIL <> '0'", user)
            Dim ORAstr_modulos_perfil As String = String.Format("SELECT ID_MODULO, ID_PERFIL FROM ugeo1749.vw_osi_aut_access WHERE USUARIO = '{0}' AND ID_PERFIL <> '0'", user)
            Dim ORAstr_modulos_manuales As String = String.Format("SELECT ID_MODULO, GUIDE FROM ugeo1749.vw_osi_aut_access WHERE USUARIO = '{0}'", user)
            Dim ORAstr_ulitos As String = String.Format("SELECT CODI, NOMBRE FROM UGEO1749.TB_OSI_AUT_ULITO_CODI")
            Dim ORAstr_nameuser As String = String.Format("SELECT NOMBRES||' '||APEPAT FROM ugeo1749.tb_osi_aut_usuarios WHERE LOWER(USUARIO) = LOWER('{0}')", user)
            Dim SQLstr_login As String = String.Format("INSERT INTO TB_LOGIN(USUARIO, LOGIN) VALUES('{0}',1)", user)
            Dim SQLstr_logout As String = "DELETE FROM TB_LOGIN WHERE LOGIN = 1"

            'Modulos asociados al usuario
            dt = SelectSqlcommand(ORAstr_modulos)
            modulosDict.Clear()
            For Each row As DataRow In dt.Rows
                modulosDict.Add(row(0).ToString(), row(1).ToString())
            Next

            'Modulos asociados a perfiles
            dt.Clear()
            modulosPerfilDict.Clear()
            dt = SelectSqlcommand(ORAstr_modulos_perfil)
            For Each row As DataRow In dt.Rows
                modulosPerfilDict.Add(row(0).ToString(), row(1).ToString())
            Next

            'Modulos asociados a manuales
            dt.Clear()
            modulosManualDict.Clear()
            dt = SelectSqlcommand(ORAstr_modulos_manuales)
            For Each row As DataRow In dt.Rows
                If row(1).ToString <> "" Then
                    modulosManualDict.Add(row(0).ToString(), row(1).ToString())
                End If
            Next

            'Nombre del usuario
            dt.Clear()
            dt = SelectSqlcommand(ORAstr_nameuser)
            For Each row As DataRow In dt.Rows
                nameUser = row(0).ToString()
            Next

            'Unidades litoestratigráficas y sus etiquetas
            dt.Clear()
            ulitoLabelsDictionary.Clear()
            dt = SelectSqlcommand(ORAstr_ulitos)
            For Each row As DataRow In dt.Rows
                ulitoLabelsDictionary.Add(row(0).ToString(), row(1))
            Next


            'Define file to open - .path passed from parent form
            'Dim connection As String = "Data Source=" & _path_sqlite
            'Dim SQLConn As New SQLiteConnection(connection)
            'Dim SQLcmd As New SQLiteCommand(SQLConn)
            'Dim SQLdr As SQLiteDataReader
            'Login con SQLite
            'SQLConn.Open()

            ''Modulos asociados al usuario
            'SQLcmd.CommandText = SQLstr_modulos
            'SQLdr = SQLcmd.ExecuteReader()
            'modulosDict.Clear()
            'While SQLdr.Read()
            '    modulosDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
            'End While
            'SQLdr.Close()

            ''Modulos asociados a perfiles
            'SQLcmd.CommandText = SQLstr_modulos_perfil
            'SQLdr = SQLcmd.ExecuteReader()
            'modulosPerfilDict.Clear()
            'While SQLdr.Read()
            '    modulosPerfilDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
            'End While
            'SQLdr.Close()

            ''Modulos asociados a manuales
            'SQLcmd.CommandText = SQLstr_modulos_manuales
            'SQLdr = SQLcmd.ExecuteReader()
            'modulosManualDict.Clear()
            'While SQLdr.Read()
            '    If SQLdr.GetValue(1).ToString <> "" Then
            '        modulosManualDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
            '    End If

            'End While
            'SQLdr.Close()

            ''Nombre del usuario
            'SQLcmd.CommandText = SQLstr_nameuser
            'SQLdr = SQLcmd.ExecuteReader()
            'While SQLdr.Read()
            '    nameUser = SQLdr.GetValue(0)
            'End While
            'SQLdr.Close()

            SQLConn.Open()
            Dim QueryString As String = String.Concat(SQLstr_logout, ";", SQLstr_login)
            SQLcmd.CommandText = QueryString
            SQLcmd.ExecuteNonQuery()

            'Cierre de conexion
            SQLConn.Close()

            _message = "success"
            responseJson.Add("message", _message)

            ' Se instalan librerias necesarias
            params.Clear()
            'lbl_login_log.Text = "Verificando dependencias..."
            'ExecuteGP(_tool_installPackages, params, _toolboxPath_automapic, False)

            lbl_login_log.Text = "Preconfiguración automática..."
            ExecuteGP(_tool_updatePreSettings, params, _toolboxPath_automapic, False)

            'Usar await en vez de Thread.Sleep, permite mantener la conexion con la GUI
            Await Task.Delay(waitTime)

            lbl_login_log.Text = "Bienvenido"
            Await Task.Delay(waitTime)

            Return responseJson
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If responseJson.ContainsKey("status") Then
                responseJson("status") = False
            Else
                responseJson.Add("status", False)
            End If
            If responseJson.ContainsKey("message") Then
                responseJson("message") = ex.Message
            Else
                responseJson.Add("message", ex.Message)
            End If
            Return responseJson
        End Try

    End Function

    Private Sub tbx_user_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbx_user.GotFocus

        If tbx_user.Text = "USERNAME" Then
            tbx_user.ForeColor = Color.Black
            tbx_user.Text = ""
        End If

    End Sub


    Private Sub tbx_user_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbx_user.LostFocus

        If tbx_user.Text = Nothing Then
            tbx_user.ForeColor = Color.Gray
            tbx_user.Text = "USERNAME"
        End If

    End Sub

    Private Sub tbx_pass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbx_pass.GotFocus

        If tbx_pass.Text = "PASSWORD" Then
            tbx_pass.UseSystemPasswordChar = True
            tbx_pass.ForeColor = Color.Black
            tbx_pass.Text = ""
        End If

    End Sub


    Private Sub tbx_pass_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbx_pass.LostFocus

        If tbx_pass.Text = Nothing Then
            tbx_pass.UseSystemPasswordChar = False
            tbx_pass.ForeColor = Color.Gray
            tbx_pass.Text = "PASSWORD"
        End If

    End Sub

    'Private Sub lbl_forgot_password_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbl_forgot_password.LinkClicked
    '    Process.Start("https://portal.ingemmet.gob.pe/web/lab/acceso?p_p_id=58&p_p_lifecycle=0&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_58_struts_action=%2Flogin%2Fforgot_password")
    'End Sub

    'IMPORTANTE :') 17/08/2021
    'UTILIZAR DOWORK SI O SOLO SI SE DEBEN EJECUTAR PROCESOS INDEPENDIENTES 
    '(QUE NO RETORNEN VALORES Y NO NECESITEN INTERACTUAR CON LA GUI AL FINALIZAR)

    'Private Sub bgw_login_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_login.DoWork
    '    Cursor.Current = Cursors.WaitCursor
    '    'Incluir proceso de validacion
    '    'params.Clear()

    '    bgw_login.ReportProgress(10, "Validando usuario...")

    '    Dim responseJson As New Dictionary(Of String, Object)
    '    Dim _message As String

    '    user = tbx_user.Text
    '    pass = tbx_pass.Text

    '    Dim Binding As BasicHttpsBinding = New BasicHttpsBinding()
    '    Dim endpoint As EndpointAddress = New EndpointAddress(_url_SWLoginService)
    '    Dim client As SWLoginClient = New SWLoginClient(Binding, endpoint)

    '    'Dim response As RespuestaSeguridadU = client.UsuarioActiveDirectory_Login1(user, pass)
    '    Dim response As RespuestaSeguridadU = client.UsuarioLoginAD(user, pass)

    '    responseJson.Add("status", response.Usuario_Valido)
    '    'responseJson.Add("status", True)
    '    client.Close()

    '    _message = "¡Credenciales incorrectas!"

    '    If Not response.Auditoria.AUTORIZADO Then
    '        responseJson.Add("message", _message)
    '        e.Result = responseJson
    '        Return
    '    End If

    '    If Not response.Usuario_Valido Then
    '        responseJson.Add("message", _message)
    '        e.Result = responseJson
    '        Return
    '    End If

    '    'Value to search as SQL Query - return first match
    '    'Dim SQLstr_validate As String = String.Format("SELECT COUNT(*) FROM TB_USER WHERE USER  ='{0}' AND PASSWORD = '{1}'", user, pass)
    '    Dim SQLstr_modulos As String = String.Format("SELECT ID_MODULO, MODULO FROM VW_ACCESS WHERE USER = '{0}'", user)
    '    Dim SQLstr_login As String = String.Format("UPDATE TB_USER SET LOGIN = 1 WHERE USER = '{0}'", user)
    '    Dim SQLstr_logout As String = "UPDATE TB_USER SET LOGIN = 0"

    '    'Define file to open - .path passed from parent form
    '    Dim connection As String = "Data Source=" & _path_sqlite
    '    Dim SQLConn As New SQLiteConnection(connection)
    '    Dim SQLcmd As New SQLiteCommand(SQLConn)
    '    Dim SQLdr As SQLiteDataReader
    '    SQLConn.Open()

    '    'Modulos asociados al usuario
    '    SQLcmd.CommandText = SQLstr_modulos
    '    SQLdr = SQLcmd.ExecuteReader()
    '    modulosDict.Clear()
    '    While SQLdr.Read()
    '        modulosDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
    '    End While
    '    SQLdr.Close()

    '    Dim QueryString As String = String.Concat(SQLstr_logout, ";", SQLstr_login)
    '    SQLcmd.CommandText = QueryString
    '    SQLcmd.ExecuteNonQuery()

    '    'Cierre de conexion
    '    SQLConn.Close()

    '    _message = "success"
    '    responseJson.Add("message", _message)
    '    e.Result = responseJson

    '    ' Se instalan librerias necesarias
    '    params.Clear()
    '    bgw_login.ReportProgress(50, "Verificando dependencias...")
    '    ExecuteGP(_tool_installPackages, params, _toolboxPath_automapic, False)

    '    bgw_login.ReportProgress(90, "Preconfiguración automática...")
    '    ExecuteGP(_tool_updatePreSettings, params, _toolboxPath_automapic, False)
    '    Thread.Sleep(waitTime)

    '    bgw_login.ReportProgress(100, "Bienvenido")
    '    Thread.Sleep(waitTime)

    'End Sub
    'Private Sub bgw_login_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw_login.ProgressChanged
    '    lbl_login_log.Text = DirectCast(e.UserState, String)
    'End Sub
    'Private Sub bgw_login_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw_login.RunWorkerCompleted
    '    If e.Result.Item("status") = False Then
    '        tbx_user.Enabled = True
    '        tbx_pass.Enabled = True
    '        btn_login.Enabled = True
    '        pbx_login_loader.Visible = False
    '        lbl_login_log.Visible = False
    '        Cursor.Current = Cursors.Default
    '        RuntimeError.PythonError = e.Result.Item("message")
    '        MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Return
    '    End If
    '    ' Se carga el modulo
    '    Dim ModulosForm = New Modulos()
    '    Cursor.Current = Cursors.Default
    '    openFormByName(ModulosForm, Me.Parent)
    'End Sub
End Class