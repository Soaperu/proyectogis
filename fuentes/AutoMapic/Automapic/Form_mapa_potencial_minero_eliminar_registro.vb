Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class Form_mapa_potencial_minero_eliminar_registro

    Dim params As New List(Of Object)

    Private Sub btn_mpm_er_buscar_Click(sender As Object, e As EventArgs) Handles btn_mpm_er_buscar.Click
        Try
            lbl_mpm_er_datos.Text = "..."
            If tbx_mpm_er_codigo.Text Is Nothing Or tbx_mpm_er_codigo.Text = "" Then
                Throw New Exception("Debe ingresar un código de mapa antes de realizar la busqueda")
            End If

            runProgressBar()

            params.Clear()
            params.Add(tbx_mpm_er_codigo.Text)

            Dim response As String = ExecuteGP(_tool_searchMapPotencial, params, _toolboxPath_mapa_potencialminero, getresult:=True)

            If response Is Nothing Then
                Throw New Exception(message_runtime_error)
            End If

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

            If responseJson.Item("status") = 0 Then
                Throw New Exception(responseJson.Item("message"))
            End If

            If responseJson.Item("response") Is Nothing Then
                tbx_mpm_er_detalle.Enabled = False
                btn_mpm_er_eliminar.Enabled = False
                Throw New Exception("No se encontraron coincidencias")
            End If
            lbl_mpm_er_datos.Text = responseJson.Item("response")
            tbx_mpm_er_detalle.Enabled = True
            btn_mpm_er_eliminar.Enabled = True
            runProgressBar("ini")
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_mpm_er_eliminar_Click(sender As Object, e As EventArgs) Handles btn_mpm_er_eliminar.Click
        Try
            btn_mpm_er_eliminar.Enabled = False
            lbl_mpm_er_datos.Text = "..."
            If tbx_mpm_er_codigo.Text Is Nothing Or tbx_mpm_er_codigo.Text = "" Then
                Throw New Exception("Debe ingresar un código de mapa antes de realizar la busqueda")
            End If

            runProgressBar()

            params.Clear()
            params.Add(tbx_mpm_er_codigo.Text)

            If tbx_mpm_er_detalle.Text Is Nothing Then
                tbx_mpm_er_detalle.Text = ""
            End If

            params.Add(tbx_mpm_er_detalle.Text)

            Dim response As String = ExecuteGP(_tool_deleteMapPotencial, params, _toolboxPath_mapa_potencialminero, getresult:=True)

            If response Is Nothing Then
                Throw New Exception(message_runtime_error)
            End If

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

            If responseJson.Item("status") = 0 Then
                Throw New Exception(responseJson.Item("message"))
            End If

            MessageBox.Show(String.Format("Se elimino el mapa {0} satisfactoriamente", tbx_mpm_er_codigo.Text), __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            tbx_mpm_er_detalle.Text = ""
            tbx_mpm_er_detalle.Enabled = False
            btn_mpm_er_eliminar.Enabled = False
            runProgressBar("ini")
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            btn_mpm_er_eliminar.Enabled = True
        End Try
    End Sub
End Class