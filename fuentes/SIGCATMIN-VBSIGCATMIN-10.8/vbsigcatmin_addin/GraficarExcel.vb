Imports SIGCATMIN
Public Class GraficarExcel
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Dim pForm As New frm_Grafica_Excel
        pForm.m_Application = My.ArcMap.Application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
