Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SQLite
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriUnits
Imports ESRI.ArcGIS.ArcMapUI
Public Class Form_gestion_usuarios_permisos
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim perfilesDictionary As New Dictionary(Of String, String)

    Private Sub Form_gestion_usuarios_permisos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetUsersIDs()
        'GetPerfiles()
        'cbx_usuario.SelectedValue = selectediduser
        'AddHandler cbx_usuario.SelectedIndexChanged, AddressOf cbx_usuario_SelectedIndexChanged
        'fillValuesfromselectedId()

    End Sub

    Private Sub fillValuesfromselectedId()

        dgv_permisos.Rows.Clear()
        dgv_permisos.ScrollBars = ScrollBars.None

        params.Clear()
        params.Add("get_access_information_by_id")
        params.Add(selectediduser)
        runProgressBar("90")

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

        For Each current In responseJson.Item("response")
            Dim arreglo As String() = {current(0), current(1), current(2) = "1", current(3)}
            dgv_permisos.Rows.Add(arreglo)
        Next


        For Each row As DataGridViewRow In dgv_permisos.Rows
            Dim comboBoxCell As DataGridViewComboBoxCell = CType(row.Cells(3), DataGridViewComboBoxCell)
            comboBoxCell.ReadOnly = False
            comboBoxCell.DataSource = New BindingSource(perfilesDictionary, Nothing)
            comboBoxCell.DisplayMember = "Value"
            comboBoxCell.ValueMember = "Key"
            comboBoxCell.FlatStyle = FlatStyle.Flat
        Next

        AddHandler dgv_permisos.CellContentClick, AddressOf dgv_permisos_CellClick

        Dim states = DataGridViewElementStates.None
        Dim totalheight = dgv_permisos.Rows.GetRowsHeight(states) + dgv_permisos.ColumnHeadersHeight
        'totalheight += dgv_gest_usuarios.Rows.Count * 4
        dgv_permisos.ClientSize = New Drawing.Size(dgv_permisos.Size.Width, totalheight)
        If totalheight >= 600 Then
            dgv_permisos.ScrollBars = ScrollBars.Vertical
        End If
        runProgressBar("ini")
    End Sub
    Private Sub GetUsersIDs()
        RemoveHandler cbx_usuario.SelectedIndexChanged, AddressOf cbx_usuario_SelectedIndexChanged
        cbx_usuario.DataSource = New BindingSource(usersDictionary, Nothing)
        cbx_usuario.DisplayMember = "Value"
        cbx_usuario.ValueMember = "Key"
    End Sub

    Private Sub GetPerfiles()
        params.Clear()
        params.Add("getperfiles")
        perfilesDictionary.Clear()

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
            perfilesDictionary.Add(current(0), current(1))
        Next

        'cbx_gu_oficina.DataSource = New BindingSource(perfilesDictionary, Nothing)
        'cbx_gu_oficina.DisplayMember = "Value"
        'cbx_gu_oficina.ValueMember = "Key"

    End Sub

    Private Sub dgv_permisos_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        'Check to ensure that the row CheckBox is clicked.
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 2 Then

            'Reference the GridView Row.
            Dim row As DataGridViewRow = dgv_permisos.Rows(e.RowIndex)

            'Set the CheckBox selection.
            row.Cells("ACCESO").Value = Convert.ToBoolean(row.Cells("ACCESO").EditedFormattedValue)

            'If CheckBox is checked, display Message Box.
            If Convert.ToBoolean(row.Cells("ACCESO").Value) Then
                row.Cells("PERFIL").Value = "1"
            Else
                'Dim comboBoxCell As DataGridViewComboBoxCell = CType(row.Cells("PERFIL"), DataGridViewComboBoxCell)
                row.Cells("PERFIL").Value = "0"
            End If
        End If
    End Sub

    Private Sub cbx_usuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_usuario.SelectedIndexChanged
        runProgressBar("60")
        selectediduser = cbx_usuario.SelectedValue
        fillValuesfromselectedId()
        runProgressBar("ini")
        lbl_gu_warning.Visible = False
    End Sub

    Private Sub Form_gestion_usuarios_permisos_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        GetUsersIDs()
        GetPerfiles()
        cbx_usuario.SelectedValue = selectediduser
        AddHandler cbx_usuario.SelectedIndexChanged, AddressOf cbx_usuario_SelectedIndexChanged
        fillValuesfromselectedId()
    End Sub

    Private Sub btn_gu_actualizar_Click(sender As Object, e As EventArgs) Handles btn_gu_actualizar.Click
        runProgressBar("70")
        Dim array As New List(Of String)
        Dim count = 0
        For Each row As DataGridViewRow In dgv_permisos.Rows
            Dim temparray = selectediduser & "," & row.Cells("ID").Value & "," & row.Cells("PERFIL").Value
            array.Add(temparray)
            count += 1
        Next
        Dim valores As String = String.Join(";", array)

        params.Clear()
        params.Add("insert_row_tb_osi_access")
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
        runProgressBar("ini")
        fillValuesfromselectedId()

        lbl_gu_warning.Text = "Los permisos del usuario '" & selectediduser & "' han sido actualizados"
        lbl_gu_warning.Visible = True
        lbl_gu_warning.ForeColor = Drawing.Color.Green

    End Sub


End Class