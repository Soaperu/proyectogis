Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports System.Linq
Imports Newtonsoft.Json   'Importa la libreria para trabajar con JSON
Imports SIGCATMIN.form_ueas

<ComClass(btn_reporte_dm_excluidosUEA.ClassId, btn_reporte_dm_excluidosUEA.InterfaceId, btn_reporte_dm_excluidosUEA.EventsId), _
 ProgId("SIGCATMIN.btn_reporte_dm_excluidosUEA")> _
Public NotInheritable Class btn_reporte_dm_excluidosUEA
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "459b93f3-6eaa-434d-8891-4d1f9716b6b7"
    Public Const InterfaceId As String = "0875c648-57b3-4348-b26a-57176576b892"
    Public Const EventsId As String = "5940cbd7-fac3-4a87-8381-da828d446ecb"
#End Region

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication

    'Define el nombre de la funcion python que genera el reporte
    Private _fn_reporte_int_inc_exc_uea As String = "reporte_int_inc_exc_uea"

    'Define la estructura de parametros que se enviara al proceso python
    Private _parametros As String = "--uea {0} --zona {1} --user {2} --password {3} {4} s"

    'Define el controlador de excepciones para el sigcatmin
    Dim RuntimeError As SigcatminException = New SigcatminException()

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "Reporte DM Excluidos UEA"  'localizable text 
        MyBase.m_caption = "Reporte DM Excluidos UEA"   'localizable text 
        MyBase.m_message = "Reporte DM Excluidos UEA"   'localizable text 
        MyBase.m_toolTip = "Reporte DM Excluidos UEA" 'localizable text 
        MyBase.m_name = "Reporte DM Excluidos UEA"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try


    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If

        ' TODO:  Add other initialization code
    End Sub

    Public Overrides Sub OnClick()
        Dim cls_eval As New Cls_evaluacion
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)
            Dim frm As New form_ueas
            Dim params As String

            params = String.Format(_parametros, coduea, zona, gstrUsuarioAcceso, gstrUsuarioClave, _fn_reporte_int_inc_exc_uea)

            Dim response As Linq.JObject = frm.ejecutar_procesos(_bat_ueas, params)

            Dim estado As Integer = response.SelectToken("estado")

            If estado = 0 Then
                RuntimeError.PythonError = response.SelectToken("msg")
                Throw RuntimeError
            End If
        Catch meEx As SigcatminException
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show(meEx.SigcatminError, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            Dim msg As String = "VisualError: " & vbCrLf & ex.Message
            MessageBox.Show(msg, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub

        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub
End Class
