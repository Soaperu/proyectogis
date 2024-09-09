Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class UserControl_CheckListBoxAutores
    Dim response_uc As String
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim autores_array_uc As New Dictionary(Of String, String)
    Dim autors_selected As String
    Delegate Sub ProcessItemCheck(ByRef ListBoxObject As CheckedListBox)

    Public Sub populateChekListBox(tool As String, name_toolbox As String, params As List(Of Object))
        response_uc = ExecuteGP(tool, params, name_toolbox)
        If response_uc Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim responseJson2 = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response_uc)
        If responseJson2.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson2.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        For Each current In responseJson2.Item("response")
            autores_array_uc.Add(current(0), current(1))
        Next

        clb_uc_autores.DataSource = New BindingSource(autores_array_uc, Nothing)
        clb_uc_autores.DisplayMember = "Value"
        clb_uc_autores.ValueMember = "Key"
    End Sub
    Public Function getAutorsCheked()
        Return autors_selected
    End Function
    Private Sub ProcessItemCheckUc(ByRef ListBoxObject As CheckedListBox)
        Dim name_autors As New List(Of String)

        For i As Integer = 0 To clb_uc_autores.Items.Count - 1
            If i < 0 Then
                Continue For
            End If
            Dim st As CheckState = clb_uc_autores.GetItemCheckState(i)
            Dim val As String = clb_uc_autores.GetItemText(clb_uc_autores.Items.Item(i))
            If st = CheckState.Checked Then
                name_autors.Add(val)
            End If
        Next
        autors_selected = String.Join(", ", name_autors)
    End Sub

    Private Sub clb_uc_autores_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clb_uc_autores.ItemCheck
        Dim Objects As Object() = {clb_uc_autores}
        BeginInvoke(New ProcessItemCheck(AddressOf ProcessItemCheckUc), Objects)
    End Sub
End Class
