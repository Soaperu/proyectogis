Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class Form_mapa_peligros_geologicos_atenciones
    Dim params As New List(Of Object)
    Private Sub Form_mapa_peligros_geologicos_atenciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nud_pga_departamentoxmes_anio.Value = Date.Today.Year
        nud_pga_departamentoxmes_anio.Maximum = Date.Today.Year
        nud_pga_departamentosxanio_anio.Value = Date.Today.Year
        nud_pga_departamentosxanio_anio.Maximum = Date.Today.Year
    End Sub

    Private Sub rbt_pga_general_anio_CheckedChanged(sender As Object, e As EventArgs) Handles rbt_pga_general_anio.CheckedChanged
        If rbt_pga_general_anio.Checked Then
            dtp_pga_enddate.Enabled = False
            dtp_pga_startdate.Enabled = False
        Else
            dtp_pga_enddate.Enabled = True
            dtp_pga_startdate.Enabled = True
        End If
    End Sub

    Private Sub btn_pga_generar_reporte_Click(sender As Object, e As EventArgs) Handles btn_pga_generar_reporte.Click
        Try
            params.Clear()
            If rbt_pga_general_anio.Checked Then
                params.Add(1)
            Else
                params.Add(0)
            End If
            params.Add(Date.Today.Year)

            If rbt_pga_general_date.Checked Then
                params.Add(1)
            Else
                params.Add(0)
            End If
            params.Add(dtp_pga_startdate.Text)
            params.Add(dtp_pga_enddate.Text)

            If cbx_pga_departamentoxmes.Checked Then
                params.Add(1)
            Else
                params.Add(0)
            End If
            params.Add(Int(nud_pga_departamentoxmes_anio.Value))

            If cbx_pga_departamentosxanio.Checked Then
                params.Add(1)
            Else
                params.Add(0)
            End If
            params.Add(Int(nud_pga_departamentosxanio_anio.Value))

            If cbx_pga_departamentosxanios.Checked Then
                params.Add(1)
            Else
                params.Add(0)
            End If

            Dim response As String = ExecuteGP(_tool_reportGeologicalHazards, params, _toolboxPath_peligros_geologicos)

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Throw New Exception(responseJson.Item("message"))
            End If
            Process.Start(responseJson.Item("response"))
            'MessageBox.Show("Se registro la atencion satisfactoriamente", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try

    End Sub
End Class