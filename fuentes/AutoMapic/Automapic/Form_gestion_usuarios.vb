Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SQLite
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriUnits
Imports ESRI.ArcGIS.ArcMapUI

Public Class Form_gestion_usuarios
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim form_agregar = New Form_gestion_usuarios_agregar()
    Dim form_editar = New Form_gestion_usuarios_editar()
    Dim form_permisos = New Form_gestion_usuarios_permisos()
    'Dim form_editar = New Form_mapa_peligros_geologicos_atenciones()
    Private Shared _instance As Form_gestion_usuarios

    Private Sub Form_gestion_usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'runProgressBar()
        'fillDataGridUsers()
        'GetUsersInformation()
        'runProgressBar("ini")

    End Sub

    Public Shared Function GetInstance() As Form_gestion_usuarios

        If _instance Is Nothing Then
            _instance = New Form_gestion_usuarios()
        End If

        Return _instance

    End Function

    Private Sub GetUsersInformation()
        params.Clear()
        params.Add("getuserstb")
        usersValuesDictionary.Clear()


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
            usersValuesDictionary.Add(current(0), current(0) & ";" & current(1) & ";" & current(2) & ";" & current(3) & ";" & current(4) & ";" & current(5))
        Next


    End Sub

    Private Sub fillDataGridUsers()
        params.Clear()
        params.Add("getusersdatagrid")

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

        'For Each current In responseJson.Item("response")
        '    usersList.Add(current)
        'Next
        Dim responselist As JArray = CType(responseJson("response"), JArray)
        usersList.Clear()
        usersDictionary.Clear()
        dgv_gest_usuarios.Rows.Clear()
        'dgv_gest_usuarios.ScrollBars = ScrollBars.None

        For Each i In responselist
            Dim arr = i.ToObject(Of String())()
            dgv_gest_usuarios.Rows.Add(arr)
            Dim user = arr(1)
            usersList.Add(user)
            usersDictionary.Add(arr(0), arr(0) & " - " & arr(1))
        Next
        dgv_gest_usuarios.Columns("OFICINA").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        Dim states = DataGridViewElementStates.None
        Dim totalheight = dgv_gest_usuarios.Rows.GetRowsHeight(states) + dgv_gest_usuarios.ColumnHeadersHeight
        'totalheight += dgv_gest_usuarios.Rows.Count * 4
        dgv_gest_usuarios.ClientSize = New Drawing.Size(dgv_gest_usuarios.Size.Width, totalheight)
        'If totalheight >= 600 Then
        '    dgv_gest_usuarios.ScrollBars = ScrollBars.Vertical
        'End If
    End Sub


    Private Sub btn_agregar_usuario_Click(sender As Object, e As EventArgs) Handles btn_agregar_usuario.Click
        Dim navegacion_form = New navegacion()
        navegacion_form.form_current = form_agregar
        navegacion_form.form_back = GetInstance()
        openFormByName(navegacion_form, Me.Parent)
    End Sub

    Private Sub btn_editar_usuario_Click(sender As Object, e As EventArgs) Handles btn_editar_usuario.Click
        Dim idx = dgv_gest_usuarios.CurrentCell.RowIndex.ToString()
        Dim valor = dgv_gest_usuarios.Rows(idx).Cells("ID").Value.ToString()
        selectediduser = valor

        Dim navegacion_form = New navegacion()
        navegacion_form.form_current = form_editar
        navegacion_form.form_back = GetInstance()
        openFormByName(navegacion_form, Me.Parent)
    End Sub

    Private Sub Form_gestion_usuarios_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        runProgressBar("90")
        fillDataGridUsers()
        GetUsersInformation()
        runProgressBar("ini")
    End Sub

    Private Sub btn_eliminar_usuario_Click(sender As Object, e As EventArgs) Handles btn_eliminar_usuario.Click
        Dim idx = dgv_gest_usuarios.CurrentCell.RowIndex.ToString()
        Dim valor = dgv_gest_usuarios.Rows(idx).Cells("ID").Value.ToString()
        Dim usuario = dgv_gest_usuarios.Rows(idx).Cells("USER").Value.ToString()
        selectediduser = valor

        Dim valido = MsgBox("¿Está seguro de eliminar el registro " & valor & " - " & usuario & " ?", vbYesNo, "Agregar Usuario")
        If valido = vbYes Then

            Dim valores = selectediduser

            params.Clear()
            params.Add("delete_tb_usuarios")
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



            MsgBox("Se eliminó " & valor & " - " & usuario & " de manera satisfactoria", vbOK, "Eliminar Usuario")


            fillDataGridUsers()
            GetUsersInformation()
        End If
    End Sub

    Private Sub dgv_gest_usuarios_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_gest_usuarios.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            btn_gest_perfiles_Click(sender, e)
        End If
    End Sub

    Private Sub btn_gest_perfiles_Click(sender As Object, e As EventArgs) Handles btn_gest_perfiles.Click
        Dim idx = dgv_gest_usuarios.CurrentCell.RowIndex.ToString()
        Dim valor = dgv_gest_usuarios.Rows(idx).Cells("ID").Value.ToString()
        selectediduser = valor

        Dim navegacion_form = New navegacion()
        navegacion_form.form_current = form_permisos
        navegacion_form.form_back = GetInstance()
        openFormByName(navegacion_form, Me.Parent)
    End Sub

    Private Sub dgv_gest_usuarios_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs)
        If e.Column.Index <> 0 Then
            Return
        End If
        Try
            e.SortResult = If(CInt(e.CellValue1) < CInt(e.CellValue2), -1, 1)
            e.Handled = True
        Catch
        End Try
    End Sub

    Private Sub btn_reportes_Click(sender As Object, e As EventArgs) Handles btn_reportes.Click
        runProgressBar("80")
        params.Clear()
        params.Add("getreporte")

        Dim response As String = ExecuteGP(_tool_guGetValues, params, tbxpath:=_toolboxPath_gestion_usuarios)

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

        Dim path = responseJson.Item("response")
        runProgressBar("ini")
        Process.Start(path)

    End Sub

End Class