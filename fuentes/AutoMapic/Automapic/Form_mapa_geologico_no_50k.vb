Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports Newtonsoft.Json
Imports ESRI.ArcGIS.DataSourcesFile
Imports Gdb = ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Maplex

Public Class Form_mapa_geologico_no_50k

    Dim sheet_orientation As String
    Dim sheet_scale As String
    Dim sheet_size As String
    Dim codHojas As String
    Dim params As New List(Of Object)
    Dim path_shapefile As String
    Dim path_raster As String = Nothing
    Dim path_shp As String = Nothing ' donde se almacena la ruta del shape tipo linea para perfil
    Dim query_simbology As String
    Dim zona As String = Nothing
    Dim toggleToolPolygon As Boolean = False
    Dim typeCoverage As String
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()


    Private Sub btn_mgno_loadshp_Click(sender As Object, e As EventArgs) Handles btn_mgno_loadshp.Click

        'Abre el explorador de archivos de arcmap y permite seleccionar un shapefile
        path_shapefile = openDialogBoxESRI(f_shapefile)
        If path_shapefile Is Nothing Then
            Return
        End If
        tbx_mgno_pathshp.Text = path_shapefile

        params.Clear()
        params.Add(path_shapefile)
        ''ExecuteGP(_tool_addFeatureToMap, params, _toolboxPath_automapic, getresult:=False)
        ExecuteGP(_tool_addFeaturesTomap, params, _toolboxPath_mapa_geologico_no_50k, getresult:=False)
        'Realiza una preconfiguracion de la capa (define src, , etc)
        'params.Add(path_shapefile)
        Dim response As String = ExecuteGP(_tool_preProcessingMgNo, params, _toolboxPath_mapa_geologico_no_50k, getresult:=True, showCancel:=False)

        'Si el proceso falla entonces muestra un mensaje 
        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.VisualError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Si el proceso no falla se obtiene el resultado de la operacion
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        'Si el proceso tiene como respuesta el valor 0, quiere decir que se ha generado un problema en el script de python
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'runProgressBar("ini")
            Return
        End If

        'Si no se generaron errores, se rescatan todos los resultados obtenidos
        'Ubicacion del archivo procesado
        'path_shapefile = responseJson.Item("response").item("feature")
        'Orientacion del mapa (vertical (v), horizontal(h))
        sheet_orientation = responseJson.Item("response").item("orientation")
        'Escala del mapa
        sheet_scale = responseJson.Item("response").item("scale")
        'Tamanio de la hoja (a3, a4)
        sheet_size = responseJson.Item("response").item("size")

        'Se configura el formulario segun los resultados
        'Orientacion
        If sheet_orientation = "v" Then
            rbtn_mgno_vert.Checked = True
        Else
            rbtn_mgno_horiz.Checked = True
        End If

        'Escala
        If sheet_scale > 250000 Then
            MessageBox.Show("Escala supera 1:250000", "Error de escala", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        ElseIf sheet_scale <= 250000 Then
            nup_mgno_scale.Value = sheet_scale
        End If

        'Tamanio de la hoja
        If sheet_size = "a2" Then
            rbtn_mgno_a2.Checked = True
        Else
            rbtn_mgno_a1.Checked = True
        End If


    End Sub

    Private Sub btn_mgno_generar_mapa_Click(sender As Object, e As EventArgs) Handles btn_mgno_generar_mapa.Click
        Try
            query_simbology = generate_map(0)
            codHoja_mgno50k = query_simbology
            params.Clear()
            params.Add(codHoja_mgno50k)
            'params.Add(path_geodatabase)
            Dim response = ExecuteGP(_tool_srcConfigbyHojas, params, _toolboxPath_mapa_geologico_no_50k)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 1 Then
                zona = responseJson.Item("response")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function generate_map(state As Integer)
        Try
            If tbx_mgno_pathshp.Text = "" Then
                MessageBox.Show("Debe agregrar un archivo shapefile", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgno_autor.Text = "" Then
                MessageBox.Show("Debe ingresar el nombre del autor", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgno_title.Text = "" Then
                MessageBox.Show("Debe ingresar un nombre de mapa", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgno_cod_mapa.Text = "" Then
                MessageBox.Show("Debe ingresar el documento", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If state = 1 Then
                Dim r = MessageBox.Show("¿Desea generar el mapa de potencial minero?. Recuerde que los datos serán registrados en la Base de Datos", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If r = DialogResult.No Then
                    Return 0
                End If
            End If

            If state = 2 Then
                Dim r = MessageBox.Show("¿Desea registrar un mapa sin informacón?. Recuerde que los datos serán registrados en la Base de Datos", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If r = DialogResult.No Then
                    Return 0
                End If
            End If

            'Cursor.Current = Cursors.WaitCursor
            runProgressBar()
            params.Clear()

            params.Add(path_shapefile)

            params.Add(tbx_mgno_title.Text)
            params.Add(tbx_mgno_autor.Text)
            params.Add(tbx_mgno_cod_mapa.Text)
            params.Add(nup_mgno_scale.Value)
            'params.Add(.Text)
            'params.Add(.Value)

            If rbtn_mgno_vert.Checked = True Then
                params.Add("v")
            Else
                params.Add("h")
            End If

            If rbtn_mgno_a1.Checked = True Then
                params.Add("a1")
            Else
                params.Add("a2")
            End If

            'params.Add(state)

            Dim response As String = ExecuteGP(_tool_generategeologyMap, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                runProgressBar("ini")
                Return 0
            End If
            If responseJson.Item("status") = 99 Then
                MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                runProgressBar("ini")
                Return 0
            End If

            'If state = 0 Or state = 1 Then
            SetMxdScale(
                    responseJson.Item("mxd"),
                    name_scale:=responseJson.Item("scale_params").Item("name_scale"),
                    unit_label:=responseJson.Item("scale_params").Item("UnitLabel"),
                    division:=responseJson.Item("scale_params").Item("Division"),
                    units:=responseJson.Item("scale_params").Item("Units")
                )

            Dim mxd_path As String = responseJson.Item("mxd")
            Dim query As String = responseJson.Item("query")
            My.ArcMap.Application.OpenDocument(mxd_path)
            'Else
            '    MessageBox.Show("Se registro la atención satisfactoriamente", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return query
        Catch ex As Exception
            'controller_document = 98

            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return 0
        End Try
    End Function


    Private Sub SetMxdScale(mxd As String, name_scale As String, Optional ByVal unit_label As String = Nothing, Optional ByVal division As Integer = Nothing, Optional ByVal units As String = Nothing)
        Dim mapDoc As IMapDocument = New MapDocument()
        mapDoc.Open(mxd)

        Dim IMap = mapDoc.ActiveView.FocusMap

        For index As Integer = 0 To IMap.MapSurroundCount - 1
            Dim element = IMap.MapSurround(index)
            If element.Name = name_scale Then
                Dim ScaleBar As IScaleBar = element
                If unit_label IsNot Nothing Then
                    ScaleBar.UnitLabel = unit_label
                End If
                If unit_label IsNot Nothing Then
                    ScaleBar.Division = division
                End If
                If unit_label IsNot Nothing Then
                    ScaleBar.Units = units
                End If
                Exit For
            End If
        Next

        Dim activeView = mapDoc.ActiveView
        Dim pageLayout As IPageLayout = activeView
        Dim graphicContainer As IGraphicsContainer = pageLayout
        Dim frameElement = graphicContainer.FindFrame(IMap)
        Dim mapFrame As IMapFrame = frameElement
        Dim mapGrids As IMapGrids = mapFrame
        Dim mapGridsList As New List(Of Integer)
        For index As Integer = 0 To mapGrids.MapGridCount - 1
            mapGridsList.Add(Convert.ToInt32(mapGrids.MapGrid(index).Name))
        Next
        mapGridsList.Sort()
        Dim mapGridsListFilter = mapGridsList.FindAll(Function(p) p < nup_mgno_scale.Value)
        Dim lastElement = mapGridsList(mapGridsListFilter.Count - 1)

        For index As Integer = 0 To mapGrids.MapGridCount - 1
            If mapGrids.MapGrid(index).Name = lastElement.ToString() Then
                mapGrids.MapGrid(index).Visible = True
            Else
                mapGrids.MapGrid(index).Visible = False
            End If
        Next
        mapDoc.Save()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_mgno_simbology.Click
        Try
            'Obtener la referencia al documento actual
            'Dim _lyr_name As String = "GPO_DGR_ULITO" 'String.Format("GPO_DGR_ULITO_{0}S", responseJson.Item("response"))
            '''''''Dim query As String = response_query
            ''''''For Each _lyr_name As String In _lyrs_name
            'MatchPersonalizedSymbolsUlito(_lyr_name, "CODI", _style_path)
            'Next
            'params.Clear()
            'Dim _lyr_name_pog As String = "GPT_DGR_POG"
            'params.Add(_lyr_name_pog)
            'Dim response As String = ExecuteGP(_tool_addStyleToPOG, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)

            Dim mApp As IMxApplication = CType(My.ArcMap.Application, IMxApplication)
            Dim mDoc As IMxDocument = CType(mApp.Document, IMxDocument)
            ' Obtén todas las DataFrames
            Dim maps As IMaps = mDoc.Maps
            Dim map As IMap
            Dim pActiveView As IActiveView
            'Recorremos todos los dataframes (mapas)
            For i As Integer = 0 To maps.Count - 1
                map = maps.Item(i)
                Dim mapName As String = map.Name
                If mapName = "DFMAPAPRINCIPAL" Then
                    pActiveView = CType(map, IActiveView)
                    mDoc.ActiveView = pActiveView
                    mDoc.UpdateContents()
                    Dim _lyr_name As String = "GPO_DGR_ULITO"
                    MatchPersonalizedSymbolsUlito(_lyr_name, "CODI", _style_path)
                    params.Clear()
                    Dim _lyr_name_pog As String = "GPT_DGR_POG"
                    params.Add(_lyr_name_pog)
                    Dim response As String = ExecuteGP(_tool_addStyleToPOG, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)
                    If typeCoverage = "1" Then
                        Dim _lyr_name_ae As String = "GPO_DGR_ULITO_area_estudio"
                        MatchPersonalizedSymbolsUlito(_lyr_name_ae, "CODI", _style_path)
                    End If
                ElseIf map.Name = "LEYENDA GEOLOGICA" Then
                    ' DataFrame encontrado, actívalo
                    pActiveView = CType(map, IActiveView)
                    mDoc.ActiveView = pActiveView
                    mDoc.UpdateContents()
                    Dim _lyr_name2 As String = "GPO_MG_FORM" 'String.Format("GPO_DGR_ULITO_{0}S", responseJson.Item("response"))
                    MatchPersonalizedSymbolsUlito(_lyr_name2, "CODI", _style_path)
                    'Exit Sub ' Sal del bucle al encuentrar el DataFrame
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_mgno_updata_Click(sender As Object, e As EventArgs) Handles btn_mgno_updata.Click
        Cursor.Current = Cursors.WaitCursor
        path_raster = openDialogBoxESRI(f_raster)
        If path_raster Is Nothing Then
            Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar()
        tbx_mgno_pathdata.Text = path_raster
        params.Clear()
        params.Add(path_raster)
        ExecuteGP(_tool_addRasterToMap, params, _toolboxPath_automapic, getresult:=False)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub btn_mgno_loadshpline_Click(sender As Object, e As EventArgs) Handles btn_mgno_loadshpline.Click
        Cursor.Current = Cursors.WaitCursor
        ' Crea y Elimina los elementos graficos previos en el mapa activo
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        Dim graphicsContainer As IGraphicsContainer = pMxDoc.FocusMap
        graphicsContainer.DeleteAllElements()
        ' Llama la ventana para seleccionar .Shp
        path_shp = openDialogBoxESRI(f_shapefile)
        If path_shp Is Nothing Then
            Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar()
        tbx_mgno_pathshpLine.Text = path_shp
        ' Almacena el .Shp seleccionado para leer sus propiedades
        Dim workspaceFactory As Gdb.IWorkspaceFactory2 = New ShapefileWorkspaceFactory()
        Dim workspace As Gdb.IWorkspace = workspaceFactory.OpenFromFile(IO.Path.GetDirectoryName(path_shp), 0)
        Dim featureWorkspace As Gdb.IFeatureWorkspace = DirectCast(workspace, Gdb.IFeatureWorkspace)
        Dim featureClass As Gdb.IFeatureClass = featureWorkspace.OpenFeatureClass(IO.Path.GetFileNameWithoutExtension(path_shp))

        ' Verificar si el feature class es de tipo polilínea
        If featureClass.ShapeType <> esriGeometryType.esriGeometryPolyline Then
            'Throw New Exception("El feature class no es de tipo polilínea.")
            MessageBox.Show("El feature class no es de tipo polilínea")
            runProgressBar("ini")
            Return
        End If
        ' Verificar si el shapefile tiene solo un registro
        Dim featureCount As Integer = featureClass.FeatureCount(Nothing)
        If featureCount <> 1 Then
            'Throw New Exception("El shapefile no tiene solo un registro.")
            MessageBox.Show("El shapefile no tiene solo un registro")
            runProgressBar("ini")
            Return
        End If

        ' Obtener el primer feature del shapefile
        Dim feature As Gdb.IFeature = featureClass.GetFeature(0)

        ' Obtener la geometría del feature
        Dim geometry As IGeometry = feature.ShapeCopy

        ' Verificar si la geometría es de tipo polilínea
        If TypeOf geometry Is IPolyline Then
            Dim polyline As IPolyline = DirectCast(geometry, IPolyline)
            ' Convertir la geometría a coleccion de puntos
            Dim pointCollection As IPointCollection = TryCast(geometry, IPointCollection)

            Dim aryTextFile(pointCollection.PointCount - 1) As String
            'Recorrer todos los puntos y agregarlos a la lista
            For i As Integer = 0 To pointCollection.PointCount - 1
                Dim point As IPoint = pointCollection.Point(i)
                aryTextFile(i) = String.Format("{0} {1}", point.X, point.Y)
            Next

            drawLine_wkt = String.Format("LINESTRING ({0})", String.Join(",", aryTextFile))
        End If

        ' Obtener la vista activa
        Dim activeView As IActiveView = My.ArcMap.Document.ActiveView
        ' Crar contenedor grafico y elemento
        Dim element As IElement = Nothing
        ' Crea la simbologis de tipo polilinea
        Dim lineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
        ' Asigna colores y estilos a la polilinea
        Dim rgbColor As IRgbColor = New RgbColor
        rgbColor.Red = 255
        lineSymbol.Color = rgbColor ' Rojo
        lineSymbol.Style = esriSimpleLineStyle.esriSLSDash
        lineSymbol.Width = 1.0
        ' Crea un elemento tipo linea y asignamos simbologia
        Dim lineElement As ILineElement = New LineElementClass()
        lineElement.Symbol = lineSymbol
        ' Asigna al elemento que ira dentro de contenedor el elemento tipo linea
        element = CType(lineElement, IElement)
        ' Asigna la geometria del shape cargado al elemento
        element.Geometry = geometry
        ' Agrega el elemento dentro del contenedor grafico
        graphicsContainer.AddElement(element, 0)
        ' Actualiza la vista activa
        My.ArcMap.Document.ActiveView.Refresh()
        ' Finaliza el proceso
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub cbx_mgno_inifin_sec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mgno_inifin_sec.SelectedIndexChanged

    End Sub

    Private Sub btn_mgno_draw_Click(sender As Object, e As EventArgs) Handles btn_mgno_draw.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleToolPolygon Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleToolPolygon = False
            btn_mgno_draw_sec.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        Form_mapa_peligros_geologicos.GetInstance().ToggleDataView()
        btn_mgno_draw_sec.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.IDs.DrawPolygon
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleToolPolygon = True
    End Sub

    Private Sub btn_mgno_draw_sec_Click(sender As Object, e As EventArgs) Handles btn_mgno_draw_sec.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleToolPolygon Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleToolPolygon = False
            btn_mgno_draw_sec.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        Form_mapa_peligros_geologicos.GetInstance().ToggleDataView()
        btn_mgno_draw_sec.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.IDs.DrawPolygon
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleToolPolygon = True
    End Sub

    Private Sub btn_mgno_profile_Click(sender As Object, e As EventArgs) Handles btn_mgno_profile.Click
        Try
            Dim pMxDoc As IMxDocument
            pMxDoc = My.ArcMap.Application.Document
            Dim maplexEngine As IAnnotateMap
            Dim seleccion As String
            maplexEngine = New MaplexAnnotateMap()
            pMxDoc.FocusMap.AnnotationEngine = maplexEngine

            If tbx_mgno_pathdata.Text = Nothing Or tbx_mgno_pathdata.Text = "" Then
                Throw New Exception("Debe especificar un DEM")
            End If
            If drawLine_wkt = Nothing Or drawLine_wkt = "" Then
                Throw New Exception("Debe trazar una línea de sección")
            End If
            seleccion = cbx_mgno_inifin_sec.Text

            runProgressBar()
            params.Clear()
            params.Add(tbx_mgno_pathdata.Text)
            params.Add(drawLine_wkt)
            params.Add(codHoja_mgno50k)
            params.Add(zona)
            params.Add(nup_mgno_pog_tolerancia.Value.ToString & ";" & seleccion)
            params.Add(drawPolygon_x_min)
            params.Add(drawPolygon_y_min)
            params.Add(nup_mgno_height.Value.ToString)
            Dim response = ExecuteGP(_tool_generateProfileNo50K, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                'MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'runProgressBar("ini")
                Return
            End If
            'params.Clear()
            'params.Add(responseJson.Item("response").Item("seccion"))
            'ExecuteGP(_tool_addFeatureToMap, params, _toolboxPath_automapic)
            'params.Clear()
            'params.Add(responseJson.Item("response").Item("pog"))
            'ExecuteGP(_tool_addFeatureToMap, params, _toolboxPath_automapic)
            runProgressBar("ini")
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbx_mgno_tiporoca_DropDown(sender As Object, e As EventArgs) Handles cbx_mgno_tiporoca.DropDown
        UpdateRocTypeOptions()
    End Sub

    Private Sub cbx_mgno_dominio_DropDown(sender As Object, e As EventArgs) Handles cbx_mgno_dominio.DropDown
        UpdateDomainsOptions()
    End Sub

    Private Sub UpdateDomainsOptions()
        Try
            params.Clear()
            params.Add(codHoja_mgno50k)

            Dim rocktype = (CType(cbx_mgno_tiporoca.SelectedItem, KeyValuePair(Of String, String))).Key
            'currentModuleName = (CType(cbx_mg_tiporoca.SelectedItem, KeyValuePair(Of Integer, String))).Value

            params.Add(rocktype)
            Dim response = ExecuteGP(_tool_getListDomainsMg, params, _toolboxPath_mapa_geologico, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If
            cbx_mgno_dominio.Items.Clear()
            For Each current In responseJson.Item("response")
                'topologyDict.Add(current.Item("id").value, current.Item("name").value)
                cbx_mgno_dominio.Items.Add(current)
            Next
            btn_mgno_generar_leyenda.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub UpdateRocTypeOptions()
        Try
            Dim rockTypeDictByCombobox As New Dictionary(Of String, String)
            params.Clear()
            params.Add(codHoja_mgno50k)
            Dim response = ExecuteGP(_tool_getListRockTypeMg, params, _toolboxPath_mapa_geologico, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If
            For Each current In responseJson.Item("response")
                rockTypeDictByCombobox.Add(current.Item("key"), current.Item("value"))
            Next
            If rockTypeDictByCombobox.Count = 0 Then
                cbx_mgno_dominio.Enabled = False
                cbx_mgno_dominio.Items.Clear()
                btn_mgno_generar_leyenda.Enabled = False
                Return
            End If
            cbx_mgno_dominio.Enabled = True
            cbx_mgno_tiporoca.DataSource = New BindingSource(rockTypeDictByCombobox, Nothing)
            cbx_mgno_tiporoca.DisplayMember = "Value"
            cbx_mgno_tiporoca.ValueMember = "Key"
            'cbx_mg_tiporoca.Items.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btn_mgno_generar_leyenda_Click(sender As Object, e As EventArgs) Handles btn_mgno_generar_leyenda.Click
        Try
            runProgressBar()
            Dim pMxDoc As IMxDocument
            pMxDoc = My.ArcMap.Application.Document
            Dim graphicsContainer As IGraphicsContainer = pMxDoc.FocusMap
            graphicsContainer.DeleteAllElements()

            Dim maplexEngine As IAnnotateMap
            maplexEngine = New MaplexAnnotateMap()
            pMxDoc.FocusMap.AnnotationEngine = maplexEngine

            params.Clear()
            params.Add(codHoja_mgno50k)
            Dim rocktype = (CType(cbx_mgno_tiporoca.SelectedItem, KeyValuePair(Of String, String))).Key
            params.Add(rocktype)
            Dim domain = cbx_mgno_dominio.SelectedItem.ToString()
            params.Add(domain)
            params.Add(drawPolygon_x_min)
            params.Add(drawPolygon_y_min)
            params.Add(typeCoverage)
            params.Add(tbx_mgno_pathshp.Text)
            params.Add(zona)
            Dim response = ExecuteGP(_tool_generateLegendNo50k, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                runProgressBar("ini")
                Return
            End If
            'Agregamos estilos a los poligonos de la capa > GPO_MG_FORM
            runProgressBar("ini")
            'Dim _lyr_name As String = "GPO_MG_FORM"
            'Dim query As String = "CODHOJA='" & codhoja & "'"
            'MatchPersonalizedSymbolsUlito(_lyr_name, "CODI", _style_path, query)
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try
    End Sub

    Private Sub btn_mgno_ver_tabla_Click(sender As Object, e As EventArgs) Handles btn_mgno_ver_tabla.Click
        Try
            params.Clear()
            params.Add(codHoja_mgno50k)
            Dim response = ExecuteGP(_tool_addTableLegendNo50k, params, _toolboxPath_mapa_geologico_no_50k, showCancel:=False)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_mgno_drawline_Click(sender As Object, e As EventArgs) Handles btn_mgno_drawline.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleToolPolygon Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleToolPolygon = False
            btn_mgno_drawline.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        Form_mapa_peligros_geologicos.GetInstance().ToggleDataView()
        btn_mgno_drawline.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.IDs.DrawLine
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleToolPolygon = True
    End Sub

    Private Sub rbtn_mgno_drawLine_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgno_drawLine.CheckedChanged
        btn_mgno_loadshpline.Enabled = False
        btn_mgno_drawline.Enabled = True
        tbx_mgno_pathshpLine.Text = ""
    End Sub

    Private Sub rbtn_mgno_loadShpLine_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgno_loadShpLine.CheckedChanged
        btn_mgno_drawline.Enabled = False
        btn_mgno_loadshpline.Enabled = True
        My.ArcMap.Application.CurrentTool = Nothing
    End Sub

    Private Sub rbtn_mgno_area_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgno_xhojas.CheckedChanged
        typeCoverage = "0"
    End Sub

    Private Sub rbtn_mgno_ae_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgno_ae.CheckedChanged
        typeCoverage = "1"
    End Sub

    Private Sub tbx_mgno_pathshp_TextChanged(sender As Object, e As EventArgs) Handles tbx_mgno_pathshp.TextChanged

    End Sub
End Class