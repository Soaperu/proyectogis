Imports SIGCATMIN
Imports SIGCATMIN.Modulo_Principal

Public Class PlanosMasivos
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Dim pForm As New frmImportar_excel
        pForm.m_Application = My.ArcMap.Application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, My.ArcMap.Application.hWnd)
    End Sub

    Protected Overrides Sub OnUpdate()
        If gloint_Acceso = 1 Then
            Me.Enabled = True
        Else
            Me.Enabled = False
        End If
    End Sub
End Class
