Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_plano_integrantesUEA.ClassId, btn_plano_integrantesUEA.InterfaceId, btn_plano_integrantesUEA.EventsId), _
 ProgId("SIGCATMIN.btn_plano_integrantesUEA")> _
Public NotInheritable Class btn_plano_integrantesUEA
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "55d3222f-df47-4cfe-8b32-bb0d8f0366e1"
    Public Const InterfaceId As String = "74169698-9cc1-475c-a608-0ffb289bb7e4"
    Public Const EventsId As String = "b36b1fb4-bb42-4426-9c15-645e8b51713c"
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
        MyBase.m_category = "Plano Integrantes de UEA"  'localizable text 
        MyBase.m_caption = "Plano Integrantes de UEA"   'localizable text 
        MyBase.m_message = "Plano Integrantes de UEA"   'localizable text 
        MyBase.m_toolTip = "Plano Integrantes de UEA" 'localizable text 
        MyBase.m_name = "Plano Integrantes de UEA"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        _activar_btn_reporte_circulo_uea = 0

        'If in_DataUEA.Rows.Count > 0 And ex_DataUEA.Rows.Count > 0 Then
        '    tipo_integranteUEA = "C_S"
        'ElseIf in_DataUEA.Rows.Count > 0 Then
        '    tipo_integranteUEA = "C_Z"
        'Else
        '    tipo_integranteUEA = "C"
        'End If

        If lista_integraUEA <> "" Then
            tipo_integranteUEA = "C"
        ElseIf in_DataUEA.Rows.Count > 0 And ex_DataUEA.Rows.Count > 0 Then
            tipo_integranteUEA = "C_Z"
        ElseIf in_DataUEA.Rows.Count > 0 Or ex_DataUEA.Rows.Count > 0 Then
            tipo_integranteUEA = "C_S"
        End If

        Dim cls_planos As New Cls_planos
        cls_planos.generaplano_UEA(m_application, "CATASTRO MINERO", "Plano Integrantes_UEA")

    End Sub
End Class



