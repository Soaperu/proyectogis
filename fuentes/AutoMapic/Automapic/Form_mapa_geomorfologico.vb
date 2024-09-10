Imports System.Drawing
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Maplex
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Geodatabase
Imports Newtonsoft.Json
Imports stdole

Public Class Form_mapa_geomorfologico

    Dim params As New List(Of Object)
    Dim fila_selected As String
    Dim columna_selected As String
    Dim cuadrante_selected As String
    Dim codhoja As String = Nothing
    Dim zonaUTM As String = Nothing
    Dim path_shapefile As String
    Dim mode As String ' 0: hojas ; 1: area shp.
    Dim nombre_capa_hojas As String = "GPO_DG_HOJAS_50K"
    Dim tipo_fuente_geom As String
    Dim color_not_enabled As Color
    Dim color_enabled As Color
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()


    Private Sub Form_mapa_geomorfologico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim maplexEngine As IAnnotateMap
        maplexEngine = New MaplexAnnotateMap()
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        pMxDoc.FocusMap.AnnotationEngine = maplexEngine
        btn_mgeom_generate.Enabled = False
        btn_mgeom_ae.Enabled = False
        cbx_mgeom_fila.Enabled = False
        cbx_mgeom_col.Enabled = False
        cbx_mgeom_cua.Enabled = False
        cbx_mgeom_fuente.SelectedIndex = 0
        color_not_enabled = Color.FromArgb(229, 229, 229)
        color_enabled = Color.FromArgb(0, 209, 179)
    End Sub

    Private Sub cbx_mgeom_fila_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mgeom_fila.SelectedIndexChanged
        params.Clear()
        cbx_mgeom_col.Items.Clear()
        cbx_mgeom_cua.Items.Clear()
        btn_mgeom_generate.Enabled = False
        fila_selected = cbx_mgeom_fila.SelectedItem.ToString()
        params.Add(fila_selected)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMgeom, params, _toolboxPath_mapa_geomorfologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        Dim columnas As String = responseJson.Item("response")
        columnas = Trim(columnas)
        Dim columnas_arr = columnas.Split(",")

        For Each i In columnas_arr
            cbx_mgeom_col.Items.Add(i)
        Next
    End Sub

    Private Sub cbx_mgeom_col_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mgeom_col.SelectedIndexChanged
        params.Clear()
        cbx_mgeom_cua.Items.Clear()
        btn_mgeom_generate.Enabled = True
        btn_mgeom_generate.BackColor = color_enabled
        columna_selected = cbx_mgeom_col.SelectedItem.ToString()
        params.Add(fila_selected)
        params.Add(columna_selected)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMgeom, params, _toolboxPath_mapa_geomorfologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        Dim cuadrante As String = responseJson.Item("response")
        Dim zona As String = responseJson.Item("zona")
        zonaUTM = "327" & zona
        cuadrante = Trim(cuadrante)
        Dim cuadrante_arr = cuadrante.Split(",")

        For Each i In cuadrante_arr
            cbx_mgeom_cua.Items.Add(i)
        Next
    End Sub

    Private Sub cbx_mgeom_cua_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mgeom_cua.SelectedIndexChanged
        params.Clear()
        cuadrante_selected = cbx_mgeom_cua.SelectedItem.ToString()
        params.Add(fila_selected)
        params.Add(columna_selected)
        params.Add(cuadrante_selected)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMgeom, params, _toolboxPath_mapa_geomorfologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If
    End Sub

    Private Sub rbtn_mgeom_hojas_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgeom_hojas.CheckedChanged
        tbx_mgeom_aepath.Enabled = False
        btn_mgeom_generate.Enabled = False
        btn_mgeom_ae.Enabled = False
        btn_mgeom_ae.BackColor = color_not_enabled
        cbx_mgeom_fila.Enabled = True
        cbx_mgeom_col.Enabled = True
        cbx_mgeom_cua.Enabled = True
        params.Clear()
        If rbtn_mgeom_hojas.Checked = True Then
            ' Verifamos si laca pa de hojas ya esta en el mapa activo
            Dim pMxdoc As IMxDocument = My.ArcMap.Document
            Dim pMap As IMap = pMxdoc.ActivatedView.FocusMap
            path_shapefile = nombre_capa_hojas
            'recorre todas las capas y de encontrar la cap de hojas retornara
            For i As Integer = 0 To pMap.LayerCount - 1
                Dim pLayer As ILayer = pMap.Layer(i)
                If pLayer.Name = nombre_capa_hojas Then
                    Return
                End If
            Next
            ' Al no encontrar la capa de hojas lo agragara al mapa activo
            Dim response_load_cuad = ExecuteGP(_tool_addGridFeature, params, _toolboxPath_mapa_geomorfologico, showCancel:=False)
            Dim response = ExecuteGP(_tool_getComponentCodeSheetMgeom, params, _toolboxPath_mapa_geomorfologico)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                cbx_mgeom_fila.Enabled = False
                cbx_mgeom_col.Enabled = False
                cbx_mgeom_cua.Enabled = False
                'tc_mg_50k.Enabled = False
                runProgressBar("ini")
                Cursor.Current = Cursors.Default
                Return
            End If
            Dim filas As String = responseJson.Item("response")
            filas = Trim(filas)
            Dim filas_arr = filas.Split(",")
            cbx_mgeom_fila.Items.Clear()
            For Each i In filas_arr
                cbx_mgeom_fila.Items.Add(i)
            Next
            mode = "0"
        End If
    End Sub

    Private Sub rbtn_mgeom__CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mgeom_.CheckedChanged
        cbx_mgeom_fila.Enabled = False
        cbx_mgeom_col.Enabled = False
        cbx_mgeom_cua.Enabled = False
        tbx_mgeom_aepath.Enabled = True
        btn_mgeom_ae.Enabled = True
        btn_mgeom_ae.BackColor = color_enabled
        btn_mgeom_generate.Enabled = True
        btn_mgeom_generate.BackColor = color_enabled
        path_shapefile = ""
        mode = "1"
    End Sub

    Private Sub btn_mgeom_ae_Click(sender As Object, e As EventArgs) Handles btn_mgeom_ae.Click
        'Abre el explorador de archivos de arcmap y permite seleccionar un shapefile
        path_shapefile = openDialogBoxESRI(f_shapefile)
        If path_shapefile Is Nothing Then
            Return
        End If
        tbx_mgeom_aepath.Text = path_shapefile
        params.Clear()
        params.Add(path_shapefile)
        ExecuteGP(_tool_addFeaturesTomap, params, _toolboxPath_mapa_geologico_no_50k, getresult:=False, showCancel:=False)
        'Realiza una preconfiguracion de la capa (define src, , etc)
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
    End Sub

    Private Sub btn_mgeom_generate_Click(sender As Object, e As EventArgs) Handles btn_mgeom_generate.Click
        Dim mxd As String = generate_map(mode)
        params.Clear()
        btn_mgeom_AsigSimbology.Enabled = True
        btn_mgeom_AsigSimbology.BackColor = color_enabled
    End Sub

    Private Function generate_map(modeMap As String)
        Try
            If path_shapefile = "" Then
                MessageBox.Show("Debe agregrar un archivo shapefile", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgeom_author.Text = "" Then
                MessageBox.Show("Debe ingresar el nombre del autor", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgeom_titleMap.Text = "" Then
                MessageBox.Show("Debe ingresar un nombre de mapa", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mgeom_codeMap.Text = "" Then
                MessageBox.Show("Debe ingresar el documento", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            'Cursor.Current = Cursors.WaitCursor
            runProgressBar()
            params.Clear()

            params.Add(path_shapefile)

            params.Add(tbx_mgeom_titleMap.Text)
            params.Add(tbx_mgeom_author.Text)
            params.Add(tbx_mgeom_codeMap.Text)

            params.Add(modeMap)
            params.Add(tipo_fuente_geom)
            'params.Add(nup_mgno_scale.Value)
            'params.Add(.Text)
            'params.Add(.Value)

            'If rbtn_mgno_vert.Checked = True Then
            '    params.Add("v")
            'Else
            '    params.Add("h")
            'End If

            'If rbtn_mgno_a1.Checked = True Then
            '    params.Add("a1")
            'Else
            '    params.Add("a2")
            'End If

            Dim response As String = ExecuteGP(_tool_generateMapGeom, params, _toolboxPath_mapa_geomorfologico, showCancel:=True)

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

            Dim mxd_path As String = responseJson.Item("mxd")
            'Dim query As String = responseJson.Item("query")
            My.ArcMap.Application.OpenDocument(mxd_path)
            'Else
            '    MessageBox.Show("Se registro la atención satisfactoriamente", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return mxd_path
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return 0
        End Try
    End Function

    Private Sub btn_mgeom_AsigSimbology_Click(sender As Object, e As EventArgs) Handles btn_mgeom_AsigSimbology.Click
        Try
            Dim mApp As IMxApplication = CType(My.ArcMap.Application, IMxApplication)
            Dim mapDoc As IMxDocument = CType(mApp.Document, IMxDocument)

            Dim _lyr_name As String = "Area_geomorfologica"
            Dim _legend_name As String = "Legend_geom"
            MatchPersonalizedSymbolsGeomorphology(_lyr_name, "ETIQUETA", _style_path_geom)

            Dim pLayer As ILayer = Nothing
            Dim IMap = mapDoc.ActiveView.FocusMap
            ' Buscar la capa por nombre "capabuscada"
            Dim pMap As IMap = mapDoc.FocusMap
            For i As Integer = 0 To pMap.LayerCount - 1
                If pMap.Layer(i).Name = _lyr_name Then
                    pLayer = pMap.Layer(i)
                    Exit For
                End If
            Next
            For index As Integer = 0 To IMap.MapSurroundCount - 1
                Dim element = IMap.MapSurround(index)
                If element.Name = _legend_name Then
                    'Dim ScaleBar As IScaleBar = element
                    Dim pLegend As ILegend = element
                    Dim pLegendItem As ILegendItem = New HorizontalLegendItemClass()
                    pLegendItem.Layer = pLayer
                    pLegend.AddItem(pLegendItem)
                    'Dim pLegendFormat As ILegendFormat = pLegend.Format
                    ' Activar la opción "only show classes that are visible in the current map extent"
                    'pLegendFormat..ShowOnlyVisibleLayers = True
                    ' Cambiar la fuente del texto de la leyenda
                    'Dim pFont As IFontDisp = New StdFont()
                    'pFont.Name = "Arial"  ' O el nombre del tipo de fuente que prefieras
                    'pFont.Size = 12       ' El tamaño de fuente que prefieras
                    'pLegendFormat.Font = pFont
                    Exit For
                End If
            Next
            mapDoc.UpdateContents()
            mapDoc.ActiveView.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbx_mgeom_fuente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mgeom_fuente.SelectedIndexChanged
        Select Case cbx_mgeom_fuente.SelectedIndex
            Case 0
                tipo_fuente_geom = "0"
                btn_mgeom_selectGeom.Enabled = False
                btn_mgeom_selectGeom.BackColor = color_not_enabled
                tbx_mgeom_geomselected.Enabled = False
            Case 1
                tipo_fuente_geom = "1"
                btn_mgeom_selectGeom.Enabled = True
                tbx_mgeom_geomselected.Enabled = True
                btn_mgeom_selectGeom.BackColor = color_enabled
        End Select
    End Sub

    Private Sub btn_mgeom_selectGeom_Click(sender As Object, e As EventArgs) Handles btn_mgeom_selectGeom.Click
        Dim pEnumGX As IEnumGxObject = Nothing
        Dim pGxDialog As IGxDialog = New GxDialog()
        Dim pFilterCollection As IGxObjectFilterCollection = CType(pGxDialog, IGxObjectFilterCollection)
        Dim pShapefileFilter As IGxObjectFilter = New GxFilterShapefiles()
        Dim pFeatureClassFilter As IGxObjectFilter = New GxFilterFeatureClasses()
        pGxDialog.AllowMultiSelect = False
        pGxDialog.Title = "Seleccionar"
        ' Añadir los filtros al cuadro de diálogo
        pFilterCollection.AddFilter(pShapefileFilter, True)
        pFilterCollection.AddFilter(pFeatureClassFilter, False)
        If Not pGxDialog.DoModalOpen(0, pEnumGX) Then
            Return
        End If
        Dim objGxObject As IGxObject = pEnumGX.Next
        tipo_fuente_geom = objGxObject.FullName
        tbx_mgeom_geomselected.Text = tipo_fuente_geom
        params.Clear()
        params.Add(tbx_mgeom_geomselected.Text)
        ExecuteGP(_tool_addSimpleFeature, params, _toolboxPath_mapa_geomorfologico, getresult:=False, showCancel:=False)
    End Sub
End Class