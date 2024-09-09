Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SQLite
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriUnits
Imports ESRI.ArcGIS.ArcMapUI
Public Class Form_gestion_usuarios_editar
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim oficinasDictionary As New Dictionary(Of String, String)
    Private Sub Form_gestion_usuarios_agregar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetUsersIDs()
        'GetOficinaNames()
        'cbx_gu_idusuario.SelectedValue = selectediduser
        'AddHandler cbx_gu_idusuario.SelectedIndexChanged, AddressOf cbx_gu_idusuario_SelectedIndexChanged
        'fillValuesfromselectedId()

    End Sub

    Private Sub fillValuesfromselectedId()
        Dim texto_valores = usersValuesDictionary(selectediduser)
        Dim arr = texto_valores.Split(";")
        tb_gu_usuario.Text = arr(1)
        cbx_gu_oficina.SelectedValue = arr(2)
        tb_gu_nombre.Text = arr(3)
        tb_gu_apepat.Text = arr(4)
        tb_gu_apemat.Text = arr(5)

    End Sub

    Private Sub GetUsersIDs()
        RemoveHandler cbx_gu_idusuario.SelectedIndexChanged, AddressOf cbx_gu_idusuario_SelectedIndexChanged
        cbx_gu_idusuario.DataSource = New BindingSource(usersDictionary, Nothing)
        cbx_gu_idusuario.DisplayMember = "Value"
        cbx_gu_idusuario.ValueMember = "Key"
    End Sub

    Private Sub GetOficinaNames()
        params.Clear()
        params.Add("getoficinanames")
        oficinasDictionary.Clear()

        Dim response As String = ExecuteGP(_tool_guGetValues, params, tbxpath:=_toolboxPath_gestion_usuarios)

        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        For Each current In responseJson.Item("response")
            oficinasDictionary.Add(current(0), current(1))
        Next

        cbx_gu_oficina.DataSource = New BindingSource(oficinasDictionary, Nothing)
        cbx_gu_oficina.DisplayMember = "Value"
        cbx_gu_oficina.ValueMember = "Key"

    End Sub


    Private Function ValidarCampos() As Integer
        If tb_gu_usuario.Text = "" Then
            lbl_gu_warning.Text = "El campo usuario no puede estar vacío"
            lbl_gu_warning.Visible = True
            Return 0
        End If
        'If usersList.Contains(tb_gu_usuario.Text) Then
        '    lbl_gu_warning.Text = "El usuario ya fue incluido con anterioridad"
        '    lbl_gu_warning.Visible = True
        '    tb_gu_usuario.ForeColor = Drawing.Color.Red
        '    Return 0
        'End If
        'If tb_gu_nombre.Text = "" Then
        '    lbl_gu_warning.Text = "El campo nombre no puede estar vacío"
        '    Return
        'End If
        'If tb_gu_apepat.Text = "" Then
        '    lbl_gu_warning.Text = "El campo apellido paterno no puede estar vacío"
        '    Return
        'End If
        'If tb_gu_apemat.Text = "" Then
        '    lbl_gu_warning.Text = "El campo apellido materno no puede estar vacío"
        '    Return
        'End If
        lbl_gu_warning.Visible = False
        Dim valido = MsgBox("¿Está seguro de aplicar los cambios", vbYesNo, "Editar Usuario")
        If valido = vbYes Then
            Return 1
        End If
        MsgBox(valido)
        Return 0
    End Function

    Private Sub btn_gu_editar_Click(sender As Object, e As EventArgs) Handles btn_gu_editar.Click
        runProgressBar()
        Dim valido = ValidarCampos()
        If valido = 0 Then
            runProgressBar("ini")
            Return
        End If

        Dim valores = cbx_gu_idusuario.SelectedValue & ";" & tb_gu_usuario.Text & ";" & cbx_gu_oficina.SelectedValue & ";" & tb_gu_nombre.Text & ";" & tb_gu_apepat.Text & ";" & tb_gu_apemat.Text

        params.Clear()
        params.Add("edit_tb_usuarios")
        params.Add(valores)

        Dim response As String = ExecuteGP(_tool_guInsertValues, params, tbxpath:=_toolboxPath_gestion_usuarios)

        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            runProgressBar("ini")
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        usersDictionary(selectediduser) = selectediduser & " - " & tb_gu_usuario.Text
        usersValuesDictionary(selectediduser) = valores
        lbl_gu_warning.Text = "El usuario '" & tb_gu_usuario.Text & "' fue editado correctamente"
        lbl_gu_warning.Visible = True
        lbl_gu_warning.ForeColor = Drawing.Color.Green
        runProgressBar("ini")
        Return
    End Sub

    Private Sub tb_gu_usuario_TextChanged(sender As Object, e As EventArgs) Handles tb_gu_usuario.TextChanged
        tb_gu_usuario.ForeColor = Drawing.Color.Black
        lbl_gu_warning.Visible = False
    End Sub

    Private Sub cbx_gu_idusuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_gu_idusuario.SelectedIndexChanged
        If cbx_gu_idusuario.SelectedValue Is Nothing Then
            fillValuesfromselectedId()
            Return
        End If
        selectediduser = cbx_gu_idusuario.SelectedValue
        fillValuesfromselectedId()
    End Sub

    Private Sub Form_gestion_usuarios_editar_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        GetUsersIDs()
        GetOficinaNames()
        cbx_gu_idusuario.SelectedValue = selectediduser
        AddHandler cbx_gu_idusuario.SelectedIndexChanged, AddressOf cbx_gu_idusuario_SelectedIndexChanged
        fillValuesfromselectedId()
    End Sub
End Class