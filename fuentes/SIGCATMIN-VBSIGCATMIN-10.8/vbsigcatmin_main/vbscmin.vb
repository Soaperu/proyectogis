Imports PORTAL_Clases

Module Vbscmin
    Dim conn As New cls_Oracle

    Function get_userguide()
        Dim url = conn.P_SEL_URLMANUAL(loglo_Titulo)
        If url <> "0" Then
            Process.Start(url)
            Exit Function
        End If
        Dim pForm As New form_development
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.show()
    End Function
End Module