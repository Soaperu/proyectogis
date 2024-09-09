Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_plano_renuncia_aprotegida.ClassId, btn_plano_renuncia_aprotegida.InterfaceId, btn_plano_renuncia_aprotegida.EventsId), _
 ProgId("SIGCATMIN.btn_plano_renuncia_aprotegida")> _
Public NotInheritable Class btn_plano_renuncia_aprotegida
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "af2fc127-8a5e-4b67-a190-50ffb287345f"
    Public Const InterfaceId As String = "e3982191-2565-4e45-9308-c31245875c8e"
    Public Const EventsId As String = "b1b4404b-d487-4f3a-b033-a036b8f71610"
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

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "Plano Area Renuncia_Area Restringida"  'localizable text 
        MyBase.m_caption = "Plano Area Renuncia_Area Restringida"   'localizable text 
        MyBase.m_message = "Plano Area Renuncia_Area Restringida"   'localizable text 
        MyBase.m_toolTip = "Plano Area Renuncia_Area Restringida" 'localizable text 
        MyBase.m_name = "Plano Area Renuncia_Area Restringida"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btn_plano_renuncia_aprotegida.OnClick implementation
        Dim cls_planos As New Cls_planos
        caso_consulta = "RENUNCIA ARESTRINGIDA"

        If v_existe_ca > 0 Then
            cls_planos.generaplano_ArestringidaRenuncia(m_application)
        Else
            MsgBox("No hay presencia de Arestringida_renuncia en el Área")
            Exit Sub
        End If

    End Sub
End Class



