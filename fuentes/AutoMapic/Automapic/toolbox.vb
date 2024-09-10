Imports System.Windows.Forms
Imports ESRI.ArcGIS.GeoprocessingUI
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Geodatabase
'Imports cademternet.GPHelper
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Display

Module toolbox
    '1. Variables globales
    '* _toolboxPath: Construye la ruta donde se encuentra el archivo *.tbx
    Public _toolboxPath_plano_topografico As String = _path & "\scripts\T01_plano_topografico_25k.tbx"
    Public _toolboxPath_peligros_geologicos As String = _path & "\scripts\T02_mapa_peligros_geologicos.tbx"
    Public _toolboxPath_automapic As String = _path & "\scripts\T00_automapic.tbx"
    Public _toolboxPath_mapa_geologico As String = _path & "\scripts\T03_mapa_geologico_50k.tbx"
    Public _toolboxPath_mapa_hidrogeologico As String = _path & "\scripts\T04_mapa_hidrogeologico.tbx"
    Public _toolboxPath_mapa_hidrogeoquimico As String = _path & "\scripts\T05_mapa_hidrogeoquimico.tbx"
    Public _toolboxPath_sincronizacion_geodatabase As String = _path & "\scripts\T06_sincronizacion_geodatabase.tbx"
    Public _toolboxPath_mapa_neotectonica As String = _path & "\scripts\T07_mapa_neotectonico.tbx"
    Public _toolboxPath_mapa_geopatrimonio As String = _path & "\scripts\T08_mapa_geopatrimonio.tbx"
    Public _toolboxPath_mapa_potencialminero As String = _path & "\scripts\T09_mapa_potencial.tbx"
    Public _toolboxPath_gestion_usuarios As String = _path & "\scripts\T10_gestion_usuarios.tbx"
    Public _toolboxPath_calculo_potencialminero As String = _path & "\scripts\T11_calculo_potencial_minero.tbx"
    Public _toolboxPath_evaluacion_mapa_geologico As String = _path & "\scripts\T12_evaluacion_mapa_geologico_50k.tbx"
    Public _toolboxPath_mapa_geologico_no_50k As String = _path & "\scripts\T13_mapa_geologico_no_50k.tbx"
    Public _toolboxPath_diagrama_esfuerzos As String = _path & "\scripts\T14_diagrama_esfuerzos.tbx"
    Public _toolboxPath_mapa_geomorfologico As String = _path & "\scripts\T15_mapa_geomorfologico.tbx"

    '* Nombre de herramientas del tbx _toolboxPath

    ':HERRAMIENTAS DEL MODULO DE PLANOS TOPOGRAFICOS
    Public _tool_getComponentCodeSheet As String = "getComponentCodeSheet"
    Public _tool_generateTopographicMap As String = "generateTopographicMap"

    ':HERRAMIENTAS DEL SISTEMA
    Public _tool_addFeatureToMap As String = "addFeatureToMap"
    Public _tool_addRasterToMap As String = "addRasterToMap"
    Public _tool_exportMXDToMPK As String = "exportMXDToMPK"
    'Public _tool_validateUser As String = "validateUser"
    Public _tool_updateSettings As String = "updateSettings"
    Public _tool_updatePreSettings As String = "updatePreSettings"
    'Public _tool_installPackages As String = "installPackages"
    Public _tool_treeLayers As String = "treeLayers"
    Public _tool_addLayerToDataFrame As String = "addLayerToDataFrame"
    Public _tool_removeFeatureOfTOC As String = "removeFeatureOfTOC"
    Public _tool_getListTopologyByModule As String = "getListTopologyByModule"
    Public _tool_getRegions As String = "getRegions"
    Public _tool_getListGeodatabases As String = "getListGeodatabases"
    Public _tool_getPythonPath As String = "getPythonPath"

    ':HERRAMIENTAS DEL MODULO DE MAPAS DE PELIGROS GEOLOGICOS
    Public _tool_mapGeologicalHazards As String = "mapGeologicalHazards"
    Public _tool_registerGeologicalHazards As String = "registerGeologicalHazards"
    Public _tool_reportGeologicalHazards As String = "reportGeologicalHazards"

    ':HERRAMIENTAS DEL MODULO DE MAPAS GEOLOGICOS 50K
    Public _tool_getComponentCodeSheetMg As String = "getComponentCodeSheetMg"
    Public _tool_addFeatureQuadsToMapMg As String = "addFeatureQuadsToMapMg"
    Public _tool_generateProfile As String = "generateProfile"
    Public _tool_setSrcDataframeByCodHoja As String = "setSrcDataframeByCodHoja"
    Public _tool_addFeaturesByCodHoja As String = "addFeaturesByCodHoja"
    Public _tool_applyTopology As String = "applyTopology"
    Public _tool_filterFeaturesBySheets As String = "filterFeaturesBySheets"
    Public _tool_addLeyendTableToTOC As String = "addLeyendTableToTOC"
    Public _tool_loadDtaLeyendTable As String = "loadDtaLeyendTable"
    Public _tool_getListDomainsMg As String = "getListDomainsMg"
    Public _tool_getListRockTypeMg As String = "getListRockTypeMg"
    Public _tool_generateGeologyLegendMap As String = "generateGeologyLegendMap"

    ':HERRAMIENTAS DEL MODULO DE MAPAS GEOLOGICOS <> 50K
    Public _tool_preProcessingMgNo As String = "preprosessingMgNo"
    Public _tool_addFeaturesTomap As String = "addFeaturesTomap"
    Public _tool_generategeologyMap As String = "generategeologyMap"
    Public _tool_addStyleToPOG As String = "addStyleToPOGs"
    Public _tool_generateProfileNo50K As String = "generateprofile"
    Public _tool_srcConfigbyHojas As String = "configSrcDataframe"
    Public _tool_generateLegendNo50k As String = "generateGeologyLegendMapNo50k"
    Public _tool_addTableLegendNo50k = "addTableLegendNo50k"

    ':HERRAMIENTAS DEL MODULO DE MAPAS HIDROGEOLOGICOS
    Public _tool_addFeatureWatershedsToMapMhg As String = "addFeatureWatershedsToMapMhg"
    Public _tool_getCodewatershedsMhg As String = "getCodewatershedsMhg"
    Public _tool_getAutoresMgh As String = "getAutoresMgh"
    Public _tool_generateRotuloMhg As String = "generateRotuloMhg"
    Public _tool_getListFormHidrogMgh As String = "getListFormHidrogMgh"
    Public _tool_generateLegendMhg As String = "generateLegendMhg"
    Public _tool_clipLayerSelectedByCuenca As String = "clipLayerSelectedByCuenca"
    Public _tool_generateMapLocation As String = "generateMapLocation"

    ':HERRAMIENTAS DEL MODULO DE MAPAS HIDROGEOQUIMICOS
    Public _tool_insertarDatosGdb As String = "insertarDatosGdb"
    Public _tool_calculoAnionesXls As String = "calculoAnionesXls"
    Public _tool_FeatureClasstoCSV As String = "FeatureClasstoCSV"
    Public _tool_piperDiagram As String = "piperDiagram"
    Public _tool_gibbsDiagram As String = "gibbsDiagram"
    Public _tool_getCodeCuencas As String = "getCodeCuencas"
    Public _tool_generateMapHidroquimico As String = "generateMapHidroquimico"
    Public _tool_getAutoresMhq As String = "getAutoresMhq"
    Public _tool_actualizarRotulo As String = "actualizarRotulo"

    ':HERRAMIENTAS DEL MODULO DE SINCRONIZACION DE GEODATABASE
    Public _tool_getFilterModeOptions As String = "getFilterModeOptions"
    Public _tool_getListOfLayers As String = "getListOfLayers"
    Public _tool_sendFilesToGDB As String = "sendFilesToGDB"

    ':HERRAMIENTAS DEL MODULO DE NEOTECTONICA
    Public _tool_getpropertiesRegion As String = "getpropertiesRegion"
    Public _tool_generateMapNeotectonica As String = "generateMapNeotectonica"
    Public _tool_getAutoresMn As String = "getAutoresMn"
    Public _tool_exportAttachmentsMn As String = "exportAttachmentsMn"

    ':HERRAMIENTAS DEL MODULO DE GEOPATRIMONIO
    Public _tool_generateMapGeopatrimonio As String = "generateMapGeopatrimonio"
    Public _tool_getAutoresMgp As String = "getAutoresMgp"
    Public _tool_getpropertiesRegionMgp As String = "getpropertiesRegionMgp"

    ':HERRAMIENTAS DEL MODULO DE POTENCIAL MINERO
    Public _tool_covertirExcelPoligono As String = "covertirExcelPoligono"
    Public _tool_preProcessingMp As String = "preProcessingMp"
    Public _tool_generateMapPotencial As String = "generateMapPotencial"
    Public _tool_searchMapPotencial As String = "searchMapPotencial"
    Public _tool_deleteMapPotencial As String = "deleteMapPotencial"
    Public _tool_generalReportMp As String = "generalReportMp"

    ':HERRAMIENTAS DEL MODULO DE GESTION DE USUARIOS
    Public _tool_guGetValues As String = "guGetValues"
    Public _tool_guInsertValues As String = "guInsertValues"

    ':HERRAMIENTAS DEL MODULO DE CALCULO DE POTENCIAL MINERO
    Public _tool_generateProjectCPM As String = "generateProjectCPM"
    Public _tool_loadProjectCPM As String = "loadProjectCPM"
    Public _tool_pmmLitologia As String = "pmmLitologia"
    Public _tool_pmmFallasGeologicas As String = "pmmFallasGeologicas"
    Public _tool_pmmDepositosMinerales As String = "pmmDepositosMinerales"
    Public _tool_pmmGeoquimica As String = "pmmGeoquimica"
    Public _tool_pmmSensoresRemotos As String = "pmmSensoresRemotos"
    Public _tool_rmiLitologia As String = "rmiLitologia"
    Public _tool_rmiSustancias As String = "rmiSustancias"
    Public _tool_rmiSensoresremotos As String = "rmiSensoresremotos"
    Public _tool_rmiAccesos As String = "rmiAccesos"
    Public _tool_calculatePmm As String = "calculatePmm"
    Public _tool_calculateRmi As String = "calculateRmi"
    Public _tool_PotencialMinero As String = "PotencialMinero"

    ':HERRAMIENTAS DEL MODULO DE EVALUACION DE MAPAS GEOLOGICOS 50k
    Public _tool_loadCodeSheets As String = "loadCodeSheets"
    Public _bat_evaluateSheets As String = _path & "\scripts\EMG_S02_evaluar_hojas.bat"

    ':HERRAMIENTAS DEL DIAGRAMA DE ESFUERZOS
    Public _tool_validationValues As String = "validationvalues"
    Public _tool_generateDiagram As String = "generateDiagram"
    Public _bat_generateDiagram As String = _path & "\scripts\DE_02_generate_stereonet.bat"

    ':HERRAMIENTAS DEL MODULO DE MAPAS GEOMORFOLOGICO
    Public _tool_addFeatureQuadsToMapMgeom As String = "addFeatureQuadsToMapMg"
    Public _tool_getComponentCodeSheetMgeom As String = "getComponentCodeSheetMgeom"
    Public _tool_generateMapGeom As String = "generateMapGeom"
    Public _tool_addSimpleFeature As String = "addSimpleFeature"
    Public _tool_addGridFeature As String = "addGridFeature"


    Dim pProDlg As IProgressDialog2
    Dim pTrkCan As ITrackCancel
    Dim pProDlgFact As IProgressDialogFactory

    'Public _tool_verificarpoligonosflotantes As String = "verificarpoligonosflotantes"

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
                tbxpath = ""
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
    Function ExecuteGP(ByVal tool As String, ByVal parameters As List(Of Object), Optional ByVal tbxpath As String = Nothing, Optional getresult As Boolean = True, Optional showCancel As Boolean = False)
        Try
            'Create a CancelTracker, required, even though we cannot use it..

            If showCancel = True Then
                pTrkCan = New CancelTracker
                ' Create the ProgressDialog. This automatically displays the dialog

                pProDlgFact = New ProgressDialogFactoryClass()
                'pProDlg = pProDlgFact.Create(Nothing, 0) 'Application.hWnd)
                'pProDlg = pProDlgFact.Create(pTrkCan, 0)
                Dim stepProgressor As IStepProgressor = pProDlgFact.Create(pTrkCan, 0)
                pProDlg = CType(stepProgressor, IProgressDialog2)
                stepProgressor.Hide()
                'Set the properties of the ProgressDialog
                'pProDlg.CancelEnabled = True
                pProDlg.Description = "Espere a que el proceso finalice (No intente cerrar esta ventana)"
                pProDlg.Title = "Proceso: " & tool & "..."
                pProDlg.Animation = esriProgressAnimationTypes.esriProgressGlobe
                'pProDlg.Hide()
            Else
                pTrkCan = Nothing
            End If

            ' Si no se especifico la ruta del tbx
            If tbxpath Is Nothing Then
                tbxpath = ""
            End If

            Dim response_object As IGeoProcessorResult = Nothing

            'Dim gpEventHandler As GPMessageEventHandler = New GPMessageEventHandler()

            Dim GP As GeoProcessor = New GeoProcessor()
            GP.LogHistory = False

            'Se registra el geoprocesor para capturar sus mensajes
            'GP.RegisterGeoProcessorEvents(gpEventHandler)

            ' Agregar el evento que capturara el mensaje
            'AddHandler gpEventHandler.GPMessage, AddressOf OnGPMessage

            'Agrega la ruta el tbx
            GP.AddToolbox(tbxpath)

            'Se crea el contedor de parametros
            Dim params As IVariantArray = New VarArrayClass()

            'Se agregan todos los parametros
            For Each i In parameters
                params.Add(i)
            Next
            Dim response = ""
            If getresult Then
                response_object = CType(GP.Execute(tool, params, pTrkCan), IGeoProcessorResult)

                response = response_object.ReturnValue
                'Desconectar el geoprocesor de la funcion que captura los eventos del mensaje
                'RemoveHandler gpEventHandler.GPMessage, AddressOf OnGPMessage

                'Remover el registro para escuchar eventos
                'GP.UnRegisterGeoProcessorEvents(gpEventHandler)
            Else
                GP.AddOutputsToMap = True
                GP.Execute(tool, params, pTrkCan)
                response = "Success"
            End If
            If showCancel = True Then
                pProDlg.HideDialog()
                Dim message As String = GP.GetMessages(1)
                If message.StartsWith("Cancelled script") Then
                    response = "{""status"": 99, ""message"": 'Se cancelo el proceso'}"
                End If
                pTrkCan = Nothing
                pProDlg = Nothing
            End If
            Return response
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If showCancel = True Then
                pProDlg.HideDialog()
                pTrkCan = Nothing
                pProDlg = Nothing
            End If
            Return Nothing
        End Try
    End Function

    'Public Sub OnGPMessage(ByVal sender As Object, ByVal e As GPMessageEventArgs)
    '    Trace.WriteLine(e.Message)
    'End Sub

End Module