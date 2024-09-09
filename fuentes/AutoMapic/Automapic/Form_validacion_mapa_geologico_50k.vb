
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class Form_validacion_mapa_geologico_50k
    Dim path_geodatabase As String
    Dim params As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim hojasDictSelected As New Dictionary(Of String, String)
    Dim urlDashboard As String = "https://geocatmin.ingemmet.gob.pe/portal/apps/dashboards/381f1ba299a24212b86e9a2b88a89b9a"
    Private Sub btn_emg_loadgdb_Click(sender As Object, e As EventArgs) Handles btn_emg_loadgdb.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim path_geodatabase_temp = openDialogBoxESRI(f_geodatabase)
            If path_geodatabase_temp Is Nothing Then
                Return
            End If
            path_geodatabase = path_geodatabase_temp
            runProgressBar()

            params.Clear()
            params.Add(path_geodatabase)

            Dim response = ExecuteGP(_tool_loadCodeSheets, params, _toolboxPath_evaluacion_mapa_geologico, showCancel:=True)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If

            cbx_emg_codhojas.Items.Clear()
            For Each current In responseJson.Item("response")
                cbx_emg_codhojas.Items.Add(current)
            Next

            tbx_emg_pathgdb.Text = path_geodatabase

            Cursor.Current = Cursors.Default
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try

    End Sub

    Private Sub cbx_emg_codhojas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_emg_codhojas.SelectedIndexChanged
        Try
            Dim codHojaSelected = cbx_emg_codhojas.SelectedItem
            If hojasDictSelected.ContainsKey(codHojaSelected) Then
                Throw New Exception("La hoja ya fue seleccionada")
                Return
            End If
            hojasDictSelected.Add(codHojaSelected, codHojaSelected)
            lbx_emg_codhojas.DataSource = New BindingSource(hojasDictSelected, Nothing)
            lbx_emg_codhojas.DisplayMember = "Value"
            lbx_emg_codhojas.ValueMember = "Key"
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lbx_emg_codhojas_DoubleClick(sender As Object, e As EventArgs) Handles lbx_emg_codhojas.DoubleClick
        If lbx_emg_codhojas.Items.Count = 0 Then
            Return
        End If
        If hojasDictSelected.Count = 0 Then
            Return
        End If
        Dim hoja_sel_idx = lbx_emg_codhojas.SelectedItem
        'If hoja_sel_idx.key = -1 Then
        '    Return
        'End If
        Dim codhoja_key As String = (CType(hoja_sel_idx, KeyValuePair(Of String, String))).Key
        hojasDictSelected.Remove(codhoja_key)
        lbx_emg_codhojas.DataSource = New BindingSource(hojasDictSelected, Nothing)
        lbx_emg_codhojas.DisplayMember = "Value"
        lbx_emg_codhojas.ValueMember = "Key"
    End Sub

    Private Sub btn_emg_procesar_Click(sender As Object, e As EventArgs) Handles btn_emg_procesar.Click
        Try
            If hojasDictSelected.Keys.Count = 0 Then
                Throw New Exception("Debe seleccionar por lo menos un código de hoja")
            End If

            runProgressBar()
            Dim codes = String.Join(",", hojasDictSelected.Keys)
            Dim p As New Process
            Dim response As String
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.FileName = _bat_evaluateSheets
            Dim params As String = String.Format("""{0}"" ""{1}"" ""{2}"" ""{3}""", python_path, tbx_emg_pathgdb.Text, codes, nameUser)
            p.StartInfo.Arguments = params
            p.StartInfo.CreateNoWindow = True
            p.Start()

            response = p.StandardOutput.ReadToEnd()
            p.WaitForExit()

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If

            Process.Start(responseJson.Item("response"))
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try
    End Sub

    Private Sub btn_emg_dashboard_Click(sender As Object, e As EventArgs) Handles btn_emg_dashboard.Click
        Process.Start(urlDashboard)
    End Sub
End Class