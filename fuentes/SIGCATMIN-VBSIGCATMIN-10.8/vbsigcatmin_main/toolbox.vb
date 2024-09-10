Imports System.Windows.Forms
Imports ESRI.ArcGIS.GeoprocessingUI
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Geodatabase
'Imports SIGCATMIN.GPHelper
Imports ESRI.ArcGIS.esriSystem
Imports System.IO.Path

Module Toolbox
    '1. Variables globales
    '* _toolboxPath: Construye la ruta donde se encuentra el archivo *.tbx
    Public _toolboxPathAre As String = GetDirectoryName(_main_batch) & "\scripts\tbx_areas_restringidas.tbx"
    Public _toolboxPathGen As String = GetDirectoryName(_main_batch) & "\scripts\tbx_generales.tbx"
    Public _toolboxPathEva As String = GetDirectoryName(_main_batch) & "\scripts\tbx_evaluacion.tbx"
    Public _toolboxPathCDM As String = GetDirectoryName(_main_batch) & "\scripts\tbx_consultaDM.tbx"
    Public _toolboxPathLib As String = GetDirectoryName(_main_batch) & "\scripts\tbx_libredenu.tbx"

    '* Nombre de herramientas del tbx _toolboxPath
    Public _tool_are_agregarfeaturetocare As String = "agregarfeaturetocare"
    Public _tool_are_exportarexcelhistorico As String = "exportarexcelhistorico"
    Public _tool_are_exportarfeaturehistorico As String = "exportarfeaturehistorico"
    Public _tool_are_cargargdbtemporal As String = "cargargdbtemporal"
    '@Daguado 20/06/2019
    Public _tool_are_generarImagenes As String = "generarImagenes"
    '@End

    Public _tool_gen_agregarfeaturetoc As String = "agregarfeaturetoc"
    Public _tool_gen_removerfeaturetoc As String = "removerfeaturetoc"
    Public _tool_gen_agregartablatoc As String = "agregartablatoc"      '@jy
    Public _tool_gen_partircuadsim As String = "partircuadsim"

    Public _tool_gen_cargargdbtemporal As String = "cargargdbtemporal"
    Public _tool_gen_areassuperpuestas As String = "AreasSuperpuestas"

    Public _tool_cdm_generarPoligonoAcumRev As String = "generarPoligonoAcumRev" '@jy

    Public _tool_lib_generarReporte As String = "generarReporte"


    '5. Funciones globales de toolbox
    '   - Funciones que devuelven resultados y que puedes ser usados en cualquier parte del proceso

    '* GPToolDialog: Inicia un cuadro de dialogo que trae un scriptool en pantalla
    '* parametros:
    '   - tool: Nombre de la herramienta
    '   - modal: True: "Si requiere que la ventana invocar bloquea la ventana principal de arcamap" 
    '            False: "Si no desea bloquer la ventana principal"; por defecto es False
    '   - tbxpath: Ruta de un nuevo tbx; por defecto el valor apunta a la variable _toolboxPath
    Function GPToolDialog(ByVal tool As String, Optional ByVal modal As Boolean = False, Optional ByVal tbxpath As String = Nothing)
        Try
            ' Si no se especifico la ruta del tbx
            If tbxpath Is Nothing Then
                tbxpath = _toolboxPathGen
            End If


            Dim pToolHelper As IGPToolCommandHelper2 = New GPToolCommandHelper
            pToolHelper.SetToolByName(tbxpath, tool)

            If modal Then
                'Realiza la invocacion del ScriptTool bloqueando la funcionalidad de ArcMap
                Dim msgs As IGPMessages = New GPMessages
                pToolHelper.InvokeModal(0, Nothing, True, msgs)
            Else
                'Realiza la invocacion del ScriptTool sin bloquear la funcionalidad de ArcMap
                pToolHelper.Invoke(Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return Nothing
    End Function

    '* ExecuteGP: Ejecuta una herramienta personalizada
    '* parametros:
    '   - tool: Nombre de la herramienta
    '   - parameters: True: Lista de parametros que deben ser pasados a la herramienta
    '   - tbxpath: Ruta de un nuevo tbx; por defecto el valor apunta a la variable _toolboxPath
    Function ExecuteGP(ByVal tool As String, ByVal parameters As List(Of Object), Optional ByVal tbxpath As String = Nothing, Optional getresult As Boolean = True, Optional showresult As Boolean = True)
        Try

            'Dim i As Object
            ' Si no se especifico la ruta del tbx
            If tbxpath Is Nothing Then
                tbxpath = _toolboxPathGen
            End If

            Dim response_object As IGeoProcessorResult = Nothing

            'Dim gpEventHandler As GPMessageEventHandler = New GPMessageEventHandler()

            Dim GP As GeoProcessor = New GeoProcessor()

            'Se registra el geoprocesor para capturar sus mensajes
            'GP.RegisterGeoProcessorEvents(gpEventHandler)

            ' Agregar el evento que capturara el mensaje
            'AddHandler gpEventHandler.GPMessage, AddressOf OnGPMessage

            'Agrega la ruta el tbx
            GP.AddToolbox(tbxpath)

            'Se crea el contedor de parametros
            Dim params As IVariantArray = New VarArrayClass()


            For i As Integer = 0 To parameters.Count() - 1
                params.Add(parameters(i))
            Next

            GP.AddOutputsToMap = showresult
            Dim response = ""
            If getresult Then
                'GP.AddOutputsToMap = False
                response_object = CType(GP.Execute(tool, params, Nothing), IGeoProcessorResult)
                response = response_object.ReturnValue

                'Desconectar el geoprocesor de la funcion que captura los eventos del mensaje
                '    RemoveHandler gpEventHandler.GPMessage, AddressOf OnGPMessage

                'Remover el registro para escuchar eventos
                '    GP.UnRegisterGeoProcessorEvents(gpEventHandler)
            Else
                'GP.AddOutputsToMap = True
                GP.Execute(tool, params, Nothing)
                response = "Success"
            End If
            Return response
        Catch ex As Exception
            Return "0;" & ex.Message
        End Try
    End Function

    'Public Sub OnGPMessage(ByVal sender As Object, ByVal e As GPMessageEventArgs)
    '    Trace.WriteLine(e.Message)
    'End Sub

End Module