Imports SIGCATMIN
Public Class UserGuide
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Dim pForm As New form_development
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
