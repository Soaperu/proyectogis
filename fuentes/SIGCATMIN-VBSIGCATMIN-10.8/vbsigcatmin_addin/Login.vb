Imports SIGCATMIN
Imports SIGCATMIN.Modulo_Principal

Public Class Login
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Try

            'SE AGREGA PARA CAPTURAR EL USUARIO (cegocheaga)

            'Dim win As System.Security.Principal.WindowsIdentity
            'win = System.Security.Principal.WindowsIdentity.GetCurrent()
            'Dim parts() As String = Split(win.Name, "\")
            'Dim username As String = parts(1)
            ''Return username.ToUpper
            '''''''''''''''''''''''''''''''''''''''''''''''



            Dim m_form As New Frm_Eval_segun_codigo
            If Not m_form.Visible Then
                m_form.m_Application = My.ArcMap.Application
                m_form.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                m_form.Show()
            End If
        Catch ex As Exception

        End Try
        'My.ArcMap.Application.CurrentTool = Nothing
    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
