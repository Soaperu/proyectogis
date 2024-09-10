Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(btn_FA_CooSupAreaRestringida.ClassId, btn_FA_CooSupAreaRestringida.InterfaceId, btn_FA_CooSupAreaRestringida.EventsId), _
 ProgId("GEOCATMIN.btn_FA_CooSupAreaRestringida")> _
Public NotInheritable Class btn_FA_CooSupAreaRestringida
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "0a8c705a-4be3-43b3-b13c-85f4fae50472"
    Public Const InterfaceId As String = "89f1fef2-e678-453a-8c6e-9d114b16cc2f"
    Public Const EventsId As String = "1b4744ab-0ee3-48c7-b76a-a8509f34f864"
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
        MyBase.m_category = "Superposición a Areas Restringidas"  'localizable text 
        MyBase.m_caption = "Superposición a Areas Restringidas"   'localizable text 
        MyBase.m_message = "Superposición a Areas Restringidas"   'localizable text 
        MyBase.m_toolTip = "Superposición a Areas Restringidas" 'localizable text 
        MyBase.m_name = "Superposición a Areas Restringidas"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
        'TODO: Add btn_FA_CooSupAreaRestringida.OnClick implementation
        Dim loLayer_1 As Boolean = False
        Dim loLayer_2 As Boolean = False
        Dim cls_eval As New Cls_evaluacion
        Dim cls_catastro As New cls_DM_1

        'Buscando la capa de reservas modificada o de la vista

        pMap = pMxDoc.FocusMap
        If pMap.Name <> "CATASTRO MINERO" Then
            MsgBox("Active Vista CATASTRO MINERO para calcular Zona Reservada Superpuesta a DM Evaluado", MsgBoxStyle.Information, "BDGEOCATMIN")
            Exit Sub
        End If
        
        pMap = pMxDoc.FocusMap

        Dim aFound1 As Boolean = False
        For A As Integer = 0 To pMap.LayerCount - 1
            If pMap.Layer(A).Name = "AreaRese_super" Then
                pFeatureLayer = pMap.Layer(A)
                aFound1 = True
                Exit For
            End If
        Next A
       
        If Not aFound1 Then 'EJECUTA PROCEDIMIENTO DE CALCULO AREA SUP RESERVA VS DM
            'cls_catastro.Add_ShapeFile1("DM_" & v_codigo, m_application, "codigo", loLayer_1)

            cls_catastro.Add_ShapeFile1(loStrShapefile_dm, m_application, "codigo", loLayer_1)
            ' cls_catastro.Add_ShapeFile1("Zona Reservada" & fecha_archi, m_application, "RESERVA", loLayer_2)
            cls_catastro.Add_ShapeFile1("Caram" & fecha_archi, m_application, "RESERVA", loLayer_2)
            If loLayer_1 And loLayer_2 Then
                cls_eval.Geoprocesamiento_temas("intersección", m_application, "RESERVA")
                Dim pForm As New Frm_DMxAreaRestringida
                pForm.m_application = m_application
                pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
                pForm.ShowDialog()
                SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
            Else
                cls_catastro.Quitar_Layer("DM_" & v_codigo, m_application)
                'cls_catastro.Quitar_Layer("RESERVA" & fecha_archi, m_application)
                cls_catastro.Quitar_Layer("Caram" & fecha_archi, m_application)
                MsgBox("El D.M. Evaluado no se Superpone con Areas Restringidas¡¡¡, por lo tanto no se realizará el Cálculo de área superpuesta para este caso.....", MsgBoxStyle.Information, "GEOCATMIN")
            End If
        Else  'CUANDO EXISTE CAPA DE RESERVA MODIFICADO
            Dim pForm As New Frm_DMxAreaRestringida
            pForm.m_application = m_application
            pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            pForm.ShowDialog()
            SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)

        End If


    End Sub
End Class



