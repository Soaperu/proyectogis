Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SQLite
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriUnits
Imports ESRI.ArcGIS.ArcMapUI
Public Class Form_gestion_usuarios_agregar
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim oficinasDictionary As New Dictionary(Of String, String)
    Dim namesDictionary As New Dictionary(Of String, String)
    Private Sub Form_gestion_usuarios_agregar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetMaxId()
        'GetOficinaNames()
    End Sub

    Private Sub GetOficinaNames()
        oficinasDictionary.Clear()
        params.Clear()
        params.Add("getoficinanames")

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

    Private Sub GetActivedirectoryNames()
        namesDictionary.Clear()
        params.Clear()
        params.Add("getusersfromactivedirectory")

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
            namesDictionary.Add(current(0), current(1))
        Next

        RemoveHandler cbx_gu_usuario.SelectedIndexChanged, AddressOf cbx_gu_usuario_SelectedIndexChanged

        cbx_gu_usuario.DataSource = New BindingSource(namesDictionary, Nothing)
        cbx_gu_usuario.DisplayMember = "Key"
        cbx_gu_usuario.ValueMember = "Key"

        AddHandler cbx_gu_usuario.SelectedIndexChanged, AddressOf cbx_gu_usuario_SelectedIndexChanged

    End Sub

    Private Sub GetMaxId()
        params.Clear()
        params.Add("getmaxid")

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

        Dim valor = responseJson.Item("response")
        tb_gu_id.Text = valor
    End Sub

    Private Function ValidarCampos() As Integer
        If cbx_gu_usuario.SelectedValue = " " Then
            lbl_gu_warning.Text = "El campo usuario no puede estar vacío"
            lbl_gu_warning.Visible = True
            Return 0
        End If
        If usersList.Contains(cbx_gu_usuario.SelectedValue) Then
            lbl_gu_warning.Text = "El usuario ya fue incluido con anterioridad"
            lbl_gu_warning.Visible = True
            cbx_gu_usuario.ForeColor = Drawing.Color.Red
            Return 0
        End If
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
        Dim valido = MsgBox("¿Está seguro de agregar al usuario " & cbx_gu_usuario.SelectedValue & " ?", vbYesNo, "Agregar Usuario")
        If valido = vbYes Then
            Return 1
        End If
        MsgBox(valido)
        Return 0
    End Function

    Private Sub btn_gu_agregar_Click(sender As Object, e As EventArgs) Handles btn_gu_agregar.Click
        runProgressBar()
        Dim valido = ValidarCampos()
        If valido = 0 Then
            runProgressBar("ini")
            Return
        End If

        Dim valores = tb_gu_id.Text & ";" & cbx_gu_usuario.SelectedValue & ";" & cbx_gu_oficina.SelectedValue & ";" & tb_gu_nombre.Text & ";" & tb_gu_apepat.Text & ";" & tb_gu_apemat.Text

        params.Clear()
        params.Add("insert_tb_usuarios")
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

        usersList.Add(cbx_gu_usuario.SelectedValue)
        lbl_gu_warning.Text = "El usuario '" & cbx_gu_usuario.SelectedValue & "' fue agregado correctamente"
        lbl_gu_warning.Visible = True
        lbl_gu_warning.ForeColor = Drawing.Color.Green
        runProgressBar("ini")
        GetMaxId()
        cleanTextboxes()
        Return
    End Sub

    Private Sub tb_gu_usuario_TextChanged(sender As Object, e As EventArgs)
        cbx_gu_usuario.ForeColor = Drawing.Color.Black
        lbl_gu_warning.Visible = False
    End Sub

    Private Sub cleanTextboxes()
        RemoveHandler cbx_gu_usuario.SelectedIndexChanged, AddressOf cbx_gu_usuario_SelectedIndexChanged
        cbx_gu_usuario.SelectedValue = " "
        tb_gu_nombre.Text = ""
        tb_gu_apepat.Text = ""
        tb_gu_apemat.Text = ""
        AddHandler cbx_gu_usuario.SelectedIndexChanged, AddressOf cbx_gu_usuario_SelectedIndexChanged
    End Sub

    Private Sub Form_gestion_usuarios_agregar_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        GetMaxId()
        GetOficinaNames()
        GetActivedirectoryNames()
        tb_gu_id.Enabled = True
        cbx_gu_usuario.Enabled = True
        cbx_gu_oficina.Enabled = True
        tb_gu_nombre.Enabled = True
        tb_gu_apepat.Enabled = True
        tb_gu_apemat.Enabled = True
    End Sub

    Private Sub cbx_gu_usuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_gu_usuario.SelectedIndexChanged
        Dim fullname = namesDictionary(cbx_gu_usuario.SelectedValue)
        Dim lista() = fullname.Split(";")
        Dim lon = lista.Length()

        Dim nombre = lista(0)
        Dim apepat = ""
        Dim apemat = ""

        If lon = 1 Then
            Return
        ElseIf lon = 2 Then
            apepat = lista(1)
        ElseIf lon >= 3 Then
            apepat = lista(lon - 2)
            apemat = lista(lon - 1)
        End If

        tb_gu_nombre.Text = nombre
        tb_gu_apepat.Text = apepat
        tb_gu_apemat.Text = apemat

    End Sub

    Private Sub tb_gu_id_TextChanged(sender As Object, e As EventArgs) Handles tb_gu_id.TextChanged

    End Sub
End Class