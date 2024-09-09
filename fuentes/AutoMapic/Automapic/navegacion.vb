Public Class navegacion
    Public form_back
    Public form_next
    Public form_current

    Private Sub navegacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        openFormByName(form_current, pnl_container)
        If form_back Is Nothing Then
            btn_back.Enabled = False
        End If
        If form_next Is Nothing Then
            btn_next.Enabled = False
            btn_next.Visible = False
        End If
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        openFormByName(form_back, Me.Parent)
    End Sub

    Private Sub btn_next_Click(sender As Object, e As EventArgs) Handles btn_next.Click
        openFormByName(form_next, Me.Parent)
    End Sub
End Class