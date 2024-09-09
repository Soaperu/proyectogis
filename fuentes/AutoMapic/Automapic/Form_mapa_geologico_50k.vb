Imports System.Drawing
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Maplex
Imports ESRI.ArcGIS.Geometry
Imports Newtonsoft.Json
Imports Gdb = ESRI.ArcGIS.Geodatabase ' se importa de esta manera ya que tiene ambiguedad en el metodo Cursor
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.SystemUI

Public Class Form_mapa_geologico_50k
    'Inherits MetroFramework.Forms.MetroForm

    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim path_raster As String = Nothing
    Dim path_shp As String = Nothing
    Dim path_geodatabase As String
    Dim params As New List(Of Object)
    Dim toggleTool As Boolean = False
    Dim toggleToolPolygon As Boolean = False
    Dim mgs_workspace As String = "Desea establecer una nueva geodatabase de trabajo"
    Dim msg_load_legend As String = "¿Está seguro que desea volver a cargar datos en la tabla de leyenda? esta operación eliminará los datos existentes en la tabla de leyenda de la hoja seleccionada"
    Dim fila_selected As String
    Dim columna_selected As String
    Dim cuadrante_selected As String
    Dim codhoja As String = Nothing
    Dim zona As String = Nothing
    Dim topologyDict As New Dictionary(Of String, String)
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle
    Private insertLastRows As Boolean = False
    Private registros_eliminados As New List(Of Object)


    Private Sub Form_mapa_geologico_50k_Load(sender As Object, e As EventArgs) Handles Me.Load
        load_form_tabpage()

        Dim maplexEngine As IAnnotateMap
        maplexEngine = New MaplexAnnotateMap()
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        pMxDoc.FocusMap.AnnotationEngine = maplexEngine
        btn_mg_generar_leyenda.Enabled = False
        rbtn_mg_drawline.Checked = True
    End Sub


    Private Sub btn_mg_loaddata_Click(sender As Object, e As EventArgs) Handles btn_mg_loaddata.Click
        Cursor.Current = Cursors.WaitCursor
        path_raster = openDialogBoxESRI(f_raster)
        If path_raster Is Nothing Then
            Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar()
        tbx_mg_pathdata.Text = path_raster
        params.Clear()
        params.Add(path_raster)
        ExecuteGP(_tool_addRasterToMap, params, _toolboxPath_automapic, getresult:=False)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub btn_mg_drawline_Click(sender As Object, e As EventArgs) Handles btn_mg_drawline.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleTool Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleTool = False
            btn_mg_drawline.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        Form_mapa_peligros_geologicos.GetInstance().ToggleDataView()
        btn_mg_drawline.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.IDs.DrawLine
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleTool = True
    End Sub

    Private Sub btn_mp_seccion_Click(sender As Object, e As EventArgs) Handles btn_mp_seccion.Click
        Try
            Dim pMxDoc As IMxDocument
            pMxDoc = My.ArcMap.Application.Document
            Dim maplexEngine As IAnnotateMap
            Dim seleccion As String
            maplexEngine = New MaplexAnnotateMap()
            pMxDoc.FocusMap.AnnotationEngine = maplexEngine

            If tbx_mg_pathdata.Text = Nothing Or tbx_mg_pathdata.Text = "" Then
                Throw New Exception("Debe especificar un DEM")
            End If
            If drawLine_wkt = Nothing Or drawLine_wkt = "" Then
                Throw New Exception("Debe trazar una línea de sección")
            End If
            seleccion = cbx_mg_inifin_sec.Text
            'MessageBox.Show(drawLine_wkt, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            runProgressBar()
            params.Clear()
            params.Add(tbx_mg_pathdata.Text)
            params.Add(drawLine_wkt)
            params.Add(codhoja)
            'params.Add(path_geodatabase)
            params.Add(zona)
            params.Add(nud_mg_tolerancia.Value.ToString & ";" & seleccion)
            params.Add(nud_mg_altura.Value.ToString)
            Dim response = ExecuteGP(_tool_generateProfile, params, _toolboxPath_mapa_geologico, showCancel:=True)
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

    Private Sub load_form_tabpage()
        'Dim response_open_dialog As String
        'If path_geodatabase IsNot Nothing Then
        '    Dim r As DialogResult = MessageBox.Show(mgs_workspace, __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '    If r = DialogResult.No Then
        '        runProgressBar("ini")
        '        Return
        '    End If
        'End If
        'response_open_dialog = openDialogBoxESRI(f_geodatabase)
        'If response_open_dialog Is Nothing Then
        '    runProgressBar("ini")
        '    Return
        'End If
        'path_geodatabase = response_open_dialog
        'runProgressBar()
        'Cursor.Current = Cursors.WaitCursor
        ''Cargar cuadriculas
        params.Clear()
        'params.Add(path_geodatabase)
        Dim response_load_cuad = ExecuteGP(_tool_addFeatureQuadsToMapMg, params, _toolboxPath_mapa_geologico, showCancel:=True)
        Dim response_load_cuad_split = Split(response_load_cuad, ";")
        Dim responseJson_load_cuad = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response_load_cuad_split(1))
        If responseJson_load_cuad.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson_load_cuad.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'cbx_mg_fila.Enabled = False
            'cbx_mg_col.Enabled = False
            'cbx_mg_cuad.Enabled = False
            tc_mg_50k.Enabled = True
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            Return
        End If

        'Cargar la lista de filas
        params.Clear()
        cbx_mg_fila.Items.Clear()
        cbx_mg_col.Items.Clear()
        cbx_mg_cuad.Items.Clear()

        'params.Add(path_geodatabase)
        'params.Add(True)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMg, params, _toolboxPath_mapa_geologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbx_mg_fila.Enabled = False
            cbx_mg_col.Enabled = False
            cbx_mg_cuad.Enabled = False
            tc_mg_50k.Enabled = False
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            Return
        End If

        Dim filas As String = responseJson.Item("response")
        filas = Trim(filas)
        Dim filas_arr = filas.Split(",")

        For Each i In filas_arr
            cbx_mg_fila.Items.Add(i)
        Next

        'Habilitar la seleccion de codigos
        cbx_mg_fila.Enabled = True
        cbx_mg_col.Enabled = True
        cbx_mg_cuad.Enabled = True
        btn_load_code.Enabled = False
        tc_mg_50k.Enabled = False
        codhoja = Nothing


        'UserControl_CheckBoxAddLayers2.LoadOptions(2, zona, addOrRemove:=False)

        'fin de proceso
        runProgressBar()
        Cursor.Current = Cursors.Default
        runProgressBar("ini")

    End Sub

    Private Sub cbx_mg_fila_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mg_fila.SelectedIndexChanged
        params.Clear()
        cbx_mg_col.Items.Clear()
        cbx_mg_cuad.Items.Clear()
        fila_selected = cbx_mg_fila.SelectedItem.ToString()
        'params.Add(path_geodatabase)
        params.Add(fila_selected)
        'params.Add(True)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMg, params, _toolboxPath_mapa_geologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        Dim columnas As String = responseJson.Item("response")
        'Dim columnas = response(2).ToString()
        columnas = Trim(columnas)
        Dim columnas_arr = columnas.Split(",")

        For Each i In columnas_arr
            cbx_mg_col.Items.Add(i)
        Next
        btn_load_code.Enabled = False
    End Sub

    Private Sub cbx_mg_col_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mg_col.SelectedIndexChanged
        params.Clear()
        cbx_mg_cuad.Items.Clear()
        columna_selected = cbx_mg_col.SelectedItem.ToString()
        'params.Add(path_geodatabase)
        'params.Add(True)
        params.Add(fila_selected)
        params.Add(columna_selected)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMg, params, _toolboxPath_mapa_geologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        Dim cuadrante As String = responseJson.Item("response")
        cuadrante = Trim(cuadrante)
        Dim cuadrante_arr = cuadrante.Split(",")

        For Each i In cuadrante_arr
            cbx_mg_cuad.Items.Add(i)
        Next
        btn_load_code.Enabled = False
    End Sub

    Private Sub cbx_mg_cuad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mg_cuad.SelectedIndexChanged
        params.Clear()
        'cbx_mg_cuad.Items.Clear()
        cuadrante_selected = cbx_mg_cuad.SelectedItem.ToString()
        'params.Add(path_geodatabase)
        'params.Add(True)
        params.Add(fila_selected)
        params.Add(columna_selected)
        params.Add(cuadrante_selected)
        Dim response = ExecuteGP(_tool_getComponentCodeSheetMg, params, _toolboxPath_mapa_geologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        btn_load_code.Enabled = True
        'Dim cuadrante As String = responseJson.Item("response")
        'cuadrante = Trim(cuadrante)
        'Dim cuadrante_arr = cuadrante.Split(",")

        'For Each i In cuadrante_arr
        '    cbx_mg_cuad.Items.Add(i)
        'Next
    End Sub

    Private Sub btn_load_code_MouseHover(sender As Object, e As EventArgs) Handles btn_load_code.MouseHover
        Dim tt_load_code As ToolTip = New ToolTip()
        tt_load_code.SetToolTip(btn_load_code, "Cargar código de hoja a utilizar")
    End Sub

    Private Sub btn_load_code_Click(sender As Object, e As EventArgs) Handles btn_load_code.Click
        Try
            runProgressBar()
            If codhoja IsNot Nothing Then
                Dim r As DialogResult = MessageBox.Show("Esta seguro que desea cambiar de hoja", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If r = DialogResult.Yes Then
                    codhoja = Nothing
                    cbx_mg_fila.Enabled = True
                    cbx_mg_col.Enabled = True
                    cbx_mg_cuad.Enabled = True
                    tc_mg_50k.Enabled = False
                End If
                runProgressBar("ini")
                Return
            End If
            cbx_mg_fila.Enabled = False
            cbx_mg_col.Enabled = False
            cbx_mg_cuad.Enabled = False
            tc_mg_50k.Enabled = True
            codhoja = String.Concat(cbx_mg_fila.SelectedItem.ToString(), cbx_mg_col.SelectedItem.ToString(), cbx_mg_cuad.SelectedItem.ToString())
            params.Clear()
            params.Add(codhoja)
            'params.Add(path_geodatabase)
            Dim response = ExecuteGP(_tool_setSrcDataframeByCodHoja, params, _toolboxPath_mapa_geologico)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 1 Then
                zona = responseJson.Item("response")
                params.Clear()
                'params.Add(path_geodatabase)
                params.Add(zona.ToString)
                params.Add(codhoja)
                ExecuteGP(_tool_addFeaturesByCodHoja, params, _toolboxPath_mapa_geologico, getresult:=False)
            End If

            Dim _lyr_name As String = String.Format("GPO_DGR_ULITO_{0}S", responseJson.Item("response"))

            Dim query As String = "CODHOJA='" & codhoja & "'"
            MatchPersonalizedSymbolsUlito(_lyr_name, "CODI", _style_path, query)
            'Comentado por bloquedo de herramientas como makefeaturelayer de arcpy
            'Dim _lyr_name_pog As String = String.Format("GPT_DGR_POG_{0}S", responseJson.Item("response"))
            'MatchPersonalizedSymbolsPOG(_lyr_name_pog, "CODI", _style_path_pog, query)

            If clb_mg_topologias.Items.Count = 0 Then
                params.Clear()
                params.Add(currentModule)
                Dim response2 As String = ExecuteGP(_tool_getListTopologyByModule, params, _toolboxPath_automapic)
                Dim responseJson2 = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response2)
                If responseJson2.Item("status") = 0 Then
                    Throw New Exception(responseJson2.Item("message"))
                    'RuntimeError.PythonError = responseJson2.Item("message")
                    'MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Cursor.Current = Cursors.Default
                End If

                For Each current In responseJson2.Item("response")
                    topologyDict.Add(current.Item("id").value, current.Item("name").value)
                Next

                clb_mg_topologias.DataSource = New BindingSource(topologyDict, Nothing)
                clb_mg_topologias.DisplayMember = "Value"
                clb_mg_topologias.ValueMember = "Key"
            End If

            'uncoment
            UserControl_CheckBoxAddLayers2.LoadOptions(String.Format("2, {0}, 10", zona), zona, addOrRemove:=False)

            runProgressBar("ini")
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub btn_mg_SelectlayerByLocation_Click(sender As Object, e As EventArgs) Handles btn_mg_SelectlayerByLocation.Click
    '    Dim myUid As UID = New UID()
    '    myUid.Value = "esriArcMapUI.SelectFeaturesTool"
    '    Dim ThisDoc As IDocument = My.ArcMap.Application.Document
    '    Dim CommandBars As ICommandBars = TryCast(ThisDoc.CommandBars, ICommandBars)
    '    CommandBars.Find(myUid)
    '    Dim myItem As ICommandItem = TryCast(CommandBars.Find(myUid), ICommandItem)
    '    myItem.Execute()
    'End Sub

    Private Sub rbt_mg_seleccion_CheckedChanged(sender As Object, e As EventArgs) Handles rbt_mg_seleccion.CheckedChanged
        btn_mg_SelectlayerByLocation.Enabled = rbt_mg_seleccion.Checked
    End Sub

    Private Sub btn_mg_run_topology_Click(sender As Object, e As EventArgs) Handles btn_mg_run_topology.Click
        Try
            runProgressBar()
            Cursor.Current = Cursors.WaitCursor
            Dim topologias As New List(Of String)
            For i As Integer = 0 To clb_mg_topologias.Items.Count - 1
                If i < 0 Then
                    Continue For
                End If
                Dim st As CheckState = clb_mg_topologias.GetItemCheckState(i)
                Dim val As String = clb_mg_topologias.Items.Item(i).key
                If st = CheckState.Checked Then
                    topologias.Add(val)
                End If
            Next
            If topologias.Count = 0 Then
                Cursor.Current = Cursors.Default
                runProgressBar("ini")

                Throw New Exception("Debe seleccionar al menos un tipo de análisis topológico")
                'MessageBox.Show("Debe seleccionar al menos un tipo de análisis topológico", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim topologias_string As String = String.Join(",", topologias)
            params.Clear()
            params.Add(codhoja)
            params.Add(topologias_string)
            params.Add(zona)
            'params.Add(path_geodatabase)
            Dim response = ExecuteGP(_tool_applyTopology, params, _toolboxPath_mapa_geologico, showCancel:=True)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                runProgressBar("ini")
                Throw New Exception(responseJson.Item("message"))
                'RuntimeError.PythonError = responseJson.Item("message")
                'MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            lbl_mg_topology_res.Text = responseJson.Item("response")

            Cursor.Current = Cursors.Default
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btn_mg_filtro_Click(sender As Object, e As EventArgs) Handles btn_mg_filtro.Click
        'uncomment
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        Dim features As List(Of String) = UserControl_CheckBoxAddLayers2.getLayerSelected()
        Dim features_as_string As String = String.Join(",", features)
        params.Clear()
        params.Add(codhoja)
        If features_as_string = "" Or features_as_string = Nothing Then
            MessageBox.Show("Debe seleccionar al menos una capa", __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            runProgressBar("ini")
            Return
        End If
        params.Add(features_as_string)
        Dim response = ExecuteGP(_tool_filterFeaturesBySheets, params, _toolboxPath_mapa_geologico, showCancel:=True)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub btn_mg_seleccion_Click(sender As Object, e As EventArgs) Handles btn_mg_seleccion.Click
        Dim myUid As UID = New UID()
        myUid.Value = "esriArcMapUI.SelectFeaturesTool"
        Dim ThisDoc As IDocument = My.ArcMap.Application.Document
        Dim CommandBars As ICommandBars = TryCast(ThisDoc.CommandBars, ICommandBars)
        CommandBars.Find(myUid)
        Dim myItem As ICommandItem = TryCast(CommandBars.Find(myUid), ICommandItem)
        myItem.Execute()
    End Sub

    Private Sub btn_mg_ver_tabla_Click(sender As Object, e As EventArgs) Handles btn_mg_ver_tabla.Click
        Try
            params.Clear()
            params.Add(codhoja)
            Dim response = ExecuteGP(_tool_addLeyendTableToTOC, params, _toolboxPath_mapa_geologico, showCancel:=False)
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

    Private Sub btn_mg_cargar_datos_Click(sender As Object, e As EventArgs) Handles btn_mg_cargar_datos.Click
        Try
            Dim r As DialogResult = MessageBox.Show(msg_load_legend, __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If r = DialogResult.No Then
                Return
            End If
            params.Clear()
            params.Add(codhoja)
            Dim response = ExecuteGP(_tool_loadDtaLeyendTable, params, _toolboxPath_mapa_geologico, showCancel:=True)
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

    Private Sub UpdateDomainsOptions()
        Try
            params.Clear()
            params.Add(codhoja)

            Dim rocktype = (CType(cbx_mg_tiporoca.SelectedItem, KeyValuePair(Of String, String))).Key
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
            cbx_mg_dominio.Items.Clear()
            For Each current In responseJson.Item("response")
                'topologyDict.Add(current.Item("id").value, current.Item("name").value)
                cbx_mg_dominio.Items.Add(current)
            Next
            btn_mg_generar_leyenda.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub UpdateRocTypeOptions()
        Try
            Dim rockTypeDictByCombobox As New Dictionary(Of String, String)
            params.Clear()
            params.Add(codhoja)
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
                cbx_mg_dominio.Enabled = False
                cbx_mg_dominio.Items.Clear()
                btn_mg_generar_leyenda.Enabled = False
                Return
            End If
            cbx_mg_dominio.Enabled = True
            cbx_mg_tiporoca.DataSource = New BindingSource(rockTypeDictByCombobox, Nothing)
            cbx_mg_tiporoca.DisplayMember = "Value"
            cbx_mg_tiporoca.ValueMember = "Key"
            'cbx_mg_tiporoca.Items.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cbx_mg_dominio_DropDown(sender As Object, e As EventArgs) Handles cbx_mg_dominio.DropDown
        UpdateDomainsOptions()
    End Sub

    Private Sub cbx_mg_tiporoca_DropDown(sender As Object, e As EventArgs) Handles cbx_mg_tiporoca.DropDown
        UpdateRocTypeOptions()
    End Sub

    Private Sub btn_mg_draw_Click(sender As Object, e As EventArgs) Handles btn_mg_draw.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleToolPolygon Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleToolPolygon = False
            btn_mg_drawline.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        Form_mapa_peligros_geologicos.GetInstance().ToggleDataView()
        btn_mg_drawline.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.IDs.DrawPolygon
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleToolPolygon = True
    End Sub

    Private Sub btn_mg_generar_leyenda_Click(sender As Object, e As EventArgs) Handles btn_mg_generar_leyenda.Click
        Try
            Dim pMxDoc As IMxDocument
            pMxDoc = My.ArcMap.Application.Document
            Dim graphicsContainer As IGraphicsContainer = pMxDoc.FocusMap
            graphicsContainer.DeleteAllElements()

            Dim maplexEngine As IAnnotateMap
            maplexEngine = New MaplexAnnotateMap()
            pMxDoc.FocusMap.AnnotationEngine = maplexEngine

            params.Clear()
            params.Add(codhoja)
            Dim rocktype = (CType(cbx_mg_tiporoca.SelectedItem, KeyValuePair(Of String, String))).Key
            params.Add(rocktype)
            Dim domain = cbx_mg_dominio.SelectedItem.ToString()
            params.Add(domain)
            params.Add(drawPolygon_x_min)
            params.Add(drawPolygon_y_min)
            Dim response = ExecuteGP(_tool_generateGeologyLegendMap, params, _toolboxPath_mapa_geologico, showCancel:=True)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Cursor.Current = Cursors.Default
                RuntimeError.PythonError = responseJson.Item("message")
                Throw New Exception(RuntimeError.PythonError)
                Return
            End If
            'Agregamos estilos a los poligonos de la capa > GPO_MG_FORM
            Dim _lyr_name As String = "GPO_MG_FORM"
            Dim query As String = "CODHOJA='" & codhoja & "'"
            MatchPersonalizedSymbolsUlito(_lyr_name, "CODI", _style_path, query)
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btn_mg_loadShp_Click(sender As Object, e As EventArgs) Handles btn_mg_loadShp.Click

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
        tbx_mg_loadShp.Text = path_shp
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

    Private Sub rbtn_mg_drawline_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mg_drawline.CheckedChanged
        btn_mg_loadShp.Enabled = False
        btn_mg_drawline.Enabled = True
        tbx_mg_loadShp.Text = ""
    End Sub

    Private Sub rbtn_mg_loadshp_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_mg_loadshp.CheckedChanged
        btn_mg_drawline.Enabled = False
        btn_mg_loadShp.Enabled = True
        My.ArcMap.Application.CurrentTool = Nothing
    End Sub

    Private Sub cbx_mg_XX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbx_mg_inifin_sec.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            ' Si el carácter ingresado no es una letra y no es la tecla Backspace, se cancela el evento
            e.Handled = True
        End If
    End Sub

    Private Sub cbx_mg_inifin_sec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mg_inifin_sec.SelectedIndexChanged

    End Sub




    'Private Sub dgv_mg_leyenda_geologica_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgv_mg_leyenda_geologica.DragDrop
    '    If dgv_mg_leyenda_geologica.SelectedRows.Item(0).IsNewRow Then
    '        Return
    '    End If
    '    Dim p As Point = dgv_mg_leyenda_geologica.PointToClient(New Point(e.X, e.Y))
    '    dragIndex = dgv_mg_leyenda_geologica.HitTest(p.X, p.Y).RowIndex
    '    If dragIndex = -1 Then
    '        Return
    '    End If
    '    If dragIndex - dgv_mg_leyenda_geologica.RowCount() = -1 Then
    '        dgv_mg_leyenda_geologica.Rows.Add("", "", "")
    '        insertLastRows = True
    '    End If
    '    If (e.Effect = DragDropEffects.Move) Then
    '        Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
    '        dgv_mg_leyenda_geologica.Rows.RemoveAt(fromIndex)
    '        dgv_mg_leyenda_geologica.Rows.Insert(dragIndex, dragRow)
    '        dgv_mg_leyenda_geologica.Rows(dragIndex).Selected = True
    '    End If
    '    If insertLastRows Then
    '        dgv_mg_leyenda_geologica.Rows.RemoveAt(dragIndex - 1)
    '        insertLastRows = False
    '    End If
    'End Sub
    'Private Sub dgv_mg_leyenda_geologica_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgv_mg_leyenda_geologica.DragOver
    '    e.Effect = DragDropEffects.Move
    'End Sub
    'Private Sub dgv_mg_leyenda_geologica_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgv_mg_leyenda_geologica.MouseDown
    '    fromIndex = dgv_mg_leyenda_geologica.HitTest(e.X, e.Y).RowIndex
    '    If fromIndex > -1 Then
    '        Dim dragSize As Size = SystemInformation.DragSize
    '        dragRect = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
    '    Else
    '        dragRect = Rectangle.Empty
    '    End If
    'End Sub

    'Private Sub dgv_mg_leyenda_geologica_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgv_mg_leyenda_geologica.MouseMove
    '    If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
    '        If (dragRect <> Rectangle.Empty AndAlso Not dragRect.Contains(e.X, e.Y)) Then
    '            dgv_mg_leyenda_geologica.DoDragDrop(dgv_mg_leyenda_geologica.Rows(fromIndex), DragDropEffects.Move)
    '        End If
    '    End If
    'End Sub
    'Private Sub deletingRowPrevent(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgv_mg_leyenda_geologica.UserDeletingRow
    '    Dim mnuitem As New ToolStripMenuItem

    '    Dim rowSelected = dgv_mg_leyenda_geologica.SelectedRows.Item(0)
    '    If rowSelected.Cells(0).Value <> "-" Then

    '        mnuitem.Name = registros_eliminados.Count()
    '        mnuitem.Text = rowSelected.Cells(0).Value
    '        AddHandler(mnuitem.Click), AddressOf ToolMenuItem_Click
    '        RegistrosEliminadosToolStripMenuItem.DropDownItems.Add(mnuitem)
    '        registros_eliminados.Add(rowSelected)
    '        dgv_mg_leyenda_geologica.SelectedRows.Item(0).Visible = False
    '        e.Cancel = True
    '    End If
    'End Sub
    'Private Sub ToolMenuItem_Click(sender As Object, ByVal e As EventArgs)
    '    For Each row As DataGridViewRow In dgv_mg_leyenda_geologica.Rows
    '        If row.Cells(0).Value = sender.Text Then
    '            row.Visible = True
    '            row.Selected = True
    '            RegistrosEliminadosToolStripMenuItem.DropDownItems.Remove(sender)
    '            Exit For
    '        End If
    '    Next
    'End Sub
    'Private Sub btn_mg_agregar_registro_vacio_Click(sender As Object, e As EventArgs) Handles btn_mg_agregar_registro_vacio.Click
    '    dgv_mg_leyenda_geologica.Rows.Insert(dgv_mg_leyenda_geologica.SelectedRows.Item(0).Index, "-", "", "")
    '    dgv_mg_leyenda_geologica.Rows(dgv_mg_leyenda_geologica.SelectedRows.Item(0).Index - 1).DefaultCellStyle.BackColor = Color.FromArgb(243, 245, 245)
    '    dgv_mg_leyenda_geologica.Rows(dgv_mg_leyenda_geologica.SelectedRows.Item(0).Index - 1).ReadOnly = True
    'End Sub

    'Private Sub btn_mg_duplicar_registro_Click(sender As Object, e As EventArgs) Handles btn_mg_duplicar_registro.Click
    '    Dim row = dgv_mg_leyenda_geologica.SelectedRows.Item(0)
    '    If row.Cells(0).Value <> "-" Then

    '        Dim rowClone As DataGridViewRow = row.Clone()
    '        For index As Int32 = 0 To row.Cells.Count - 1
    '            rowClone.Cells(index).Value = row.Cells(index).Value
    '        Next
    '        dgv_mg_leyenda_geologica.Rows.Insert(row.Index + 1, rowClone)
    '    End If
    'End Sub

    'Private Sub dgv_mg_leyenda_geologica_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles dgv_mg_leyenda_geologica.ColumnAdded
    '    e.Column.SortMode = DataGridViewColumnSortMode.NotSortable
    'End Sub
End Class