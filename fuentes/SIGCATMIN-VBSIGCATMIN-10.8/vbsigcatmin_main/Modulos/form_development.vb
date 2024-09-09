Imports System.Drawing

Public Class form_development

    Private response

    Public Function show()
        Me.pb_icon_custom.Image = SystemIcons.Information.ToBitmap()
        Me.ShowDialog()
        Return response
    End Function

    Private Sub btn_ok_custom_Click(sender As Object, e As EventArgs) Handles btn_ok_custom.Click
        response = DialogResult.Yes
        Me.Close()
    End Sub
End Class