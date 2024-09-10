Imports System.Windows.Forms
'Imports ESRI.ArcGIS.Carto
'Imports ESRI.ArcGIS.Geodatabase
Imports Newtonsoft.Json

Public Class Form_mapa_neotectonica
    Dim params As New List(Of Object)
    Dim properties
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim regionesDictByCombobox As New Dictionary(Of String, String)
    Dim cd_depa As String
    Dim idx_zona As String
    Dim exclude_layers_name As New List(Of String)
    'Definir los identificadores de modulo en las propiedades de la clase 
    'para evitar conflictos posteriores si se cambian los id en Modulos.vb
    Dim id_modulo_neotectonica As Integer = 7
    Dim id_modulo_geopatrimonio As Integer = 8
    Dim numero_registros_horizontal As Integer = 50
    Dim numero_registros_vertical As Integer = 100
    Dim tool_selected As String
    Dim toolbox_selected As String
    Dim tool_autores As String
    Dim tool_region_config As String
    'Dim exlusion_layer As ISet(Of Object)
    Private Sub Form_mapa_neotectonica_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Si el modulo seleccionado es neotectonica
        'Es necesario ocultar controles del modulo de geopatrimonio
        UserControl_CheckBoxAddLayers1.LoadOptions(8, Nothing, addOrRemove:=False)
        If currentModule = id_modulo_neotectonica Then
            lbl_mgp_numero_registros.Visible = False
            nud_mgp_n_registros.Visible = False
            tool_selected = _tool_generateMapNeotectonica
            toolbox_selected = _toolboxPath_mapa_neotectonica
            tool_autores = _tool_getAutoresMn
            tool_region_config = _tool_getpropertiesRegion
        ElseIf currentModule = id_modulo_geopatrimonio Then
            tool_selected = _tool_generateMapGeopatrimonio
            toolbox_selected = _toolboxPath_mapa_geopatrimonio
            tool_autores = _tool_getAutoresMgp
            tool_region_config = _tool_getpropertiesRegionMgp
        End If

        RemoveHandler cbx_mn_region.SelectedIndexChanged, AddressOf cbx_mn_region_SelectedIndexChanged
        params.Clear()
        Dim response As String = ExecuteGP(_tool_getRegions, params, tbxpath:=_toolboxPath_automapic)

        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        regionesDictByCombobox.Add("-1", "----- Seleccione una región -----")
        For Each current In responseJson.Item("response")
            regionesDictByCombobox.Add(current(0), current(1))
        Next

        cbx_mn_region.DataSource = New BindingSource(regionesDictByCombobox, Nothing)
        cbx_mn_region.DisplayMember = "Value"
        cbx_mn_region.ValueMember = "Key"

        AddHandler cbx_mn_region.SelectedIndexChanged, AddressOf cbx_mn_region_SelectedIndexChanged

        params.Clear()
        UserControl_CheckListBoxAutores1.populateChekListBox(tool_autores, toolbox_selected, params)

    End Sub

    Private Sub cbx_mn_region_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mn_region.SelectedIndexChanged
        cd_depa = (CType(cbx_mn_region.SelectedItem, KeyValuePair(Of String, String))).Key

        If cd_depa = "-1" Then
            btn_mn_mapa.Enabled = False
            Return
        End If

        params.Clear()
        params.Add(cd_depa)
        Dim response As String = ExecuteGP(tool_region_config, params, tbxpath:=toolbox_selected)

        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        properties = responseJson.Item("response")

        'Validando la orientacion
        If properties.item("orientacion") = 1 Then
            rdb_mn_horizontal.Checked = True
        Else
            rdb_mn_vertical.Checked = True
        End If

        'Validando la escala
        nud_mn_escala.Value = Convert.ToInt32(properties.item("escala"))


        'Validando zona
        If properties.item("zona").value = 17 Then
            idx_zona = 0
        ElseIf properties.item("zona").value = 18 Then
            idx_zona = 1
        ElseIf properties.item("zona").value = 19 Then
            idx_zona = 2
        Else
            idx_zona = -1
        End If
        cbx_mn_zona.SelectedIndex = idx_zona

        'Si el modulo seleccionado es geopatrimonio
        'Se establecen los valores por defecto de la cantidad de registros segun orientacion
        If currentModule = id_modulo_geopatrimonio Then
            If rdb_mn_horizontal.Checked Then
                nud_mgp_n_registros.Value = numero_registros_horizontal
            ElseIf rdb_mn_vertical.Checked Then
                nud_mgp_n_registros.Value = numero_registros_vertical
            End If
        End If

        btn_mn_mapa.Enabled = True

    End Sub

    Private Sub btn_mn_mapa_Click(sender As Object, e As EventArgs) Handles btn_mn_mapa.Click
        'Cursor.Current = Cursors.WaitCursor
        runProgressBar()

        params.Clear()
        'parametro cd_depa
        params.Add(cd_depa)

        'parametro orientacion
        If rdb_mn_horizontal.Checked Then
            params.Add(1)
        Else
            params.Add(2)
        End If

        'Parametro de zona
        params.Add(Convert.ToInt32(cbx_mn_zona.Text))

        'Parametro de escala
        params.Add(Convert.ToInt32(nud_mn_escala.Value))

        'Parametro autores
        Dim autores As String = UserControl_CheckListBoxAutores1.getAutorsCheked()
        params.Add(autores)

        If currentModule = id_modulo_geopatrimonio Then
            'Disponible solo en el modulo de geopatrimonio
            'Parametro de numero de registros por cuadro de interes geologico
            params.Add(nud_mgp_n_registros.Value)
        End If

        Dim response As String = ExecuteGP(tool_selected, params, tbxpath:=toolbox_selected)

        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.VisualError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim mxd_path As String = responseJson.Item("response")

        'Si el modulo seleccionado es neotectonica
        'Se ingresa al mapa exportado y se configura la geometria de clipping layout
        'If currentModule = id_modulo_neotectonica Then
        '    'Configurando el layer de distrito como area de corte (clipgeometry) en el layout
        '    Dim mapDoc As IMapDocument = New MapDocument()
        '    mapDoc.Open(mxd_path)
        '    Dim map As IMap = mapDoc.Map(0)

        '    Dim pmap = mapDoc.ActiveView.FocusMap

        '    Dim pFeatureLayer As IFeatureLayer = Nothing
        '    Dim pFCursor As IFeatureCursor
        '    Dim fclas_tema As IFeatureClass
        '    Dim pQueryFilter As IQueryFilter

        '    pQueryFilter = New QueryFilter
        '    pQueryFilter.WhereClause = String.Format("cd_depa = '{0}'", cd_depa)

        '    Dim pEnumLayer As IEnumLayer
        '    pEnumLayer = map.Layers

        '    Dim pFeatureShape As IFeature

        '    pEnumLayer.Reset()
        '    pFeatureLayer = pEnumLayer.Next
        '    Do Until pFeatureLayer Is Nothing
        '        If pFeatureLayer.Name = "GPO_DEP_DEPARTAMENTO" Then
        '            'pFeatureSelection = pFeatureLayer

        '            fclas_tema = pFeatureLayer.FeatureClass
        '            pFCursor = pFeatureLayer.Search(Nothing, True)
        '            pFeatureShape = pFCursor.NextFeature
        '            'System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCursor)
        '            mapDoc.ActiveView.FocusMap.ClipGeometry = pFeatureShape.Shape
        '            mapDoc.ActiveView.Refresh()
        '            Exit Do
        '        End If
        '        pFeatureLayer = pEnumLayer.Next

        '    Loop


        '    mapDoc.ActiveView.Refresh()

        '    mapDoc.Save()
        '    mapDoc.Close()
        'End If

        My.ArcMap.Application.OpenDocument(mxd_path)

        'Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub cbx_mn_zona_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mn_zona.SelectedIndexChanged

    End Sub
End Class