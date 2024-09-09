Imports SIGCATMIN
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem

Public Class Logout
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Dim msg As String
        msg = "Desea finalizar la sesión en SIGCATMIN?"
        Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = DialogResult.Yes Then
            gloint_Acceso = 0
        End If
    End Sub

    Protected Overrides Sub OnUpdate()
        If gloint_Acceso = 1 Then
            Me.Enabled = True
        Else
            Me.Enabled = False
        End If
    End Sub
End Class
