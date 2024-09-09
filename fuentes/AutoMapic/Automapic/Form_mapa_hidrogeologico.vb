Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports Newtonsoft.Json
Imports System.Drawing
Imports System.ComponentModel
Imports ESRI.ArcGIS.Maplex

Public Class Form_mapa_hidrogeologico
    'Dim controller As Integer = 0
    Dim cuencasDictSelected As New Dictionary(Of String, String)
    Dim params As New List(Of Object)
    Dim uharray As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim cuencasDictByCombobox As New Dictionary(Of String, String)
    Dim autoresDictByCheckListBox As New Dictionary(Of String, String)
    Delegate Sub ProcessItemCheck(ByRef ListBoxObject As CheckedListBox)
    Dim dset As New DataSet
    Dim dtable As New DataTable
    Dim dtableTv As New DataTable
    Dim clasificacionDescriptDict As New Dictionary(Of String, String)
    Dim clickedNode As String
    Dim zona_geografica As String
    Dim queryCuencas As String
    Dim FIELD_CD_CUENCA As String = "cd_cuenca"
    Dim cuencas As String

    Private Sub Form_mapa_hidrogeologico_load(sender As Object, e As EventArgs) Handles Me.Load
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        RemoveHandler cbx_mh_cuencas.SelectedIndexChanged, AddressOf cbx_mh_cuencas_SelectedIndexChanged
        params.Clear()
        Dim response = ExecuteGP(_tool_getCodewatershedsMhg, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        For Each current In responseJson.Item("response")
            cuencasDictByCombobox.Add(current(0), current(1))
        Next
        cbx_mh_cuencas.DataSource = New BindingSource(cuencasDictByCombobox, Nothing)
        cbx_mh_cuencas.DisplayMember = "Value"
        cbx_mh_cuencas.ValueMember = "Key"

        Dim response2 = ExecuteGP(_tool_getAutoresMgh, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson2 = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response2)
        If responseJson2.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson2.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        For Each current In responseJson2.Item("response")
            autoresDictByCheckListBox.Add(current(0), current(1))
        Next

        clb_mh_autores.DataSource = New BindingSource(autoresDictByCheckListBox, Nothing)
        clb_mh_autores.DisplayMember = "Value"
        clb_mh_autores.ValueMember = "Key"


        tc_mh_tools.Enabled = False
        'tbx_mh_title2.Text = "MAPA HIDROGEOLÓGICO"
        AddHandler cbx_mh_cuencas.SelectedIndexChanged, AddressOf cbx_mh_cuencas_SelectedIndexChanged
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Function getIdMapa()
        Dim id_mapa As String = String.Join("_", cuencasDictSelected.Keys)
        Return id_mapa
    End Function

    Private Sub cbx_mh_cuencas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mh_cuencas.SelectedIndexChanged
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        Dim cuenca_sel_key As String = (CType(cbx_mh_cuencas.SelectedItem, KeyValuePair(Of String, String))).Key
        Dim cuenca_sel_value As String = (CType(cbx_mh_cuencas.SelectedItem, KeyValuePair(Of String, String))).Value
        'Cuando el control se carga por primera vez el index seleccionado es -1, 
        'por tanto no deberia ejecutar proceso alguno
        If cbx_mh_cuencas.SelectedIndex = -1 Then
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If

        Dim idx = cbx_mh_cuencas.SelectedIndex
        'Si se vuelve a seleccionar la misma cuenca, debe informar y cortar el proceso
        'No se puede seleccionar una misma cuenca mas de una vez
        If cuencasDictSelected.ContainsKey(cuenca_sel_key) Then
            MessageBox.Show("La cuenca ya fue seleccionada", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        'Se agrega la cuenca seleccionada al diccionario de registqueryCuencasro
        cuencasDictSelected.Add(cuenca_sel_key, cuenca_sel_value)

        'Se construye el titulo del mapa asociado solo a las cuencas, se omiten las intercuencas
        Dim codcuencasArray As New List(Of Object)
        Dim nomcuencasArray As New List(Of String)
        For Each ikey As String In cuencasDictSelected.Keys
            codcuencasArray.Add(ikey)
            If Not cuencasDictSelected(ikey).ToLower().Contains("intercuenca") Then
                Dim nm_cuenca = Trim(cuencasDictSelected(ikey).ToLower.Replace("cuenca", "").Split("-")(0))
                nomcuencasArray.Add(nm_cuenca.ToUpper())
            End If
        Next
        tbx_mh_title1.Text = SetTitle1(nomcuencasArray)

        'Se construyen o reconstruyen algunos parametros para multiples funcion o geoprocesamiento
        'Esto se realiza cada vez que hay cambios en la cuenca
        cuencas = String.Join(",", codcuencasArray)
        Dim cuencas_as_string As String = String.Join(",", codcuencasArray)
        queryCuencas = String.Format("{0} in ('{1}')", FIELD_CD_CUENCA, cuencas_as_string)
        'Dim query As String = String.Join(",", codcuencasArray)

        params.Clear()
        params.Add(cuencas)
        Dim response = ExecuteGP(_tool_addFeatureWatershedsToMapMhg, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        zona_geografica = responseJson.Item("response")

        If cuencasDictSelected.Count > 0 Then
            tc_mh_tools.Enabled = True
            loadFormacionesHidrogeologicas()
        End If

        UserControl_CheckBoxAddLayers1.LoadOptions("0, 1", zona:=zona_geografica, query:=queryCuencas, addOrRemove:=True)

        lbx_mh_cuencas.DataSource = New BindingSource(cuencasDictSelected, Nothing)
        lbx_mh_cuencas.DisplayMember = "Value"
        lbx_mh_cuencas.ValueMember = "Key"
        cbx_mh_cuencas.SelectedIndex = -1
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Function SetTitle1(arrayCuencas As IList(Of String))
        Dim response As String = "HIDROGEOLOGÍA DE LA CUENCA DEL RÍO " & String.Join(", ", arrayCuencas)
        Return response
    End Function

    Private Sub lbx_mh_cuencas_doubleclick(sender As Object, e As EventArgs) Handles lbx_mh_cuencas.DoubleClick
        If lbx_mh_cuencas.Items.Count = 0 Then
            Return
        End If
        Dim cuenca_sel_idx = lbx_mh_cuencas.SelectedItem
        If cuencasDictSelected.Count = 0 Then
            Return
        End If
        If cuenca_sel_idx.key = -1 Then
            Return
        End If
        Dim cuenca_key As String = (CType(cuenca_sel_idx, KeyValuePair(Of String, String))).Key
        cuencasDictSelected.Remove(cuenca_key)
        lbx_mh_cuencas.DataSource = New BindingSource(cuencasDictSelected, Nothing)
        lbx_mh_cuencas.DisplayMember = "Value"
        lbx_mh_cuencas.ValueMember = "Key"

        If cuencasDictSelected.Count = 0 Then
            tc_mh_tools.Enabled = False
        Else
            loadFormacionesHidrogeologicas()
        End If

        Dim codcuencasArray As New List(Of Object)
        Dim nomcuencasArray As New List(Of String)
        For Each ikey As String In cuencasDictSelected.Keys
            codcuencasArray.Add(ikey)
            If Not cuencasDictSelected(ikey).ToLower().Contains("intercuenca") Then
                Dim nm_cuenca = Trim(cuencasDictSelected(ikey).ToLower.Replace("cuenca", "").Split("-")(0))
                nomcuencasArray.Add(nm_cuenca.ToUpper())
            End If
        Next
        tbx_mh_title1.Text = SetTitle1(nomcuencasArray)
        cuencas = String.Join(",", codcuencasArray)
        Dim cuencas_as_string As String = String.Join("','", codcuencasArray)
        If cuencas = "" Then
            cuencas = "6caa6007e67b4c05a3333fcce39ef2e6"
            cuencas_as_string = cuencas
        End If
        queryCuencas = String.Format("{0} in ('{1}')", FIELD_CD_CUENCA, cuencas_as_string)
        params.Clear()
        params.Add(cuencas)
        Dim response = ExecuteGP(_tool_addFeatureWatershedsToMapMhg, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
    End Sub

    Private Sub ProcessItemCheckSub(ByRef ListBoxObject As CheckedListBox)
        Dim nomautores As New List(Of String)

        For i As Integer = 0 To clb_mh_autores.Items.Count - 1
            If i < 0 Then
                Continue For
            End If
            Dim st As CheckState = clb_mh_autores.GetItemCheckState(i)
            Dim val As String = clb_mh_autores.GetItemText(clb_mh_autores.Items.Item(i))
            If st = CheckState.Checked Then
                nomautores.Add(val)
            End If
        Next
        tbx_mh_autores.Text = String.Join(" & ", nomautores)
    End Sub

    Private Sub clb_mh_autores_ItemCheck(sender As Object, e As EventArgs) Handles clb_mh_autores.ItemCheck
        Dim Objects As Object() = {clb_mh_autores}
        BeginInvoke(New ProcessItemCheck(AddressOf ProcessItemCheckSub), Objects)
    End Sub

    Private Sub btn_mh_grotulo_Click(sender As Object, e As EventArgs) Handles btn_mh_grotulo.Click
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        params.Clear()
        Dim tamanio As String
        If rbt_mh_pequenio.Checked Then
            tamanio = "1"
        Else
            tamanio = "2"
        End If
        params.Add(tamanio)
        params.Add(tbx_mh_autores.Text)
        params.Add(tbx_mh_title1.Text)
        params.Add(tbx_mh_title2.Text)
        params.Add(nud_mh_numero.Value)
        Dim response = ExecuteGP(_tool_generateRotuloMhg, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If

        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        If TypeOf pMxDoc.ActiveView IsNot IPageLayout Then
            pMxDoc.ActiveView = pMxDoc.PageLayout
        End If

        pMxDoc.PageLayout.Page.Units = ESRI.ArcGIS.esriSystem.esriUnits.esriCentimeters
        pMxDoc.ActivatedView.Refresh()

        Dim graphicsContainer As IGraphicsContainer = pMxDoc.PageLayout
        Dim PictureElement As IPictureElement = New JpgPictureElement()

        Dim pEnv As IEnvelope = New Envelope()
        If tamanio = "1" Then
            pEnv.PutCoords(0, 0, 7.1, 3.6)
        Else
            pEnv.PutCoords(0, 0, 14.1, 9.1)
        End If

        PictureElement.ImportPictureFromFile(responseJson("response"))
        Dim IElement As IElement = PictureElement
        IElement.Geometry = pEnv
        graphicsContainer.AddElement(IElement, 0)

        pMxDoc.ActivatedView.Refresh()
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Private Sub loadFormacionesHidrogeologicas()
        params.Clear()
        'Dim codcuencasArray As New List(Of Object)
        'For Each ikey As String In cuencasDictSelected.Keys
        '    codcuencasArray.Add(ikey)
        'Next
        'Dim cuencas As String = String.Join("','", codcuencasArray)
        'queryCuencas = String.Format("{0} in ('{1}')", FIELD_CD_CUENCA, cuencas)
        params.Add(cuencas)
        params.Add(zona_geografica)
        Dim response = ExecuteGP(_tool_getListFormHidrogMgh, params, _toolboxPath_mapa_hidrogeologico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If


        If responseJson.Item("response").count = 0 Then
            Return
        End If

        dgv_mh_leyenda.Rows.Clear()

        dgv_mh_leyenda.ColumnCount = 5
        dgv_mh_leyenda.Columns(0).Name = "Leyenda"
        dgv_mh_leyenda.Columns(0).ReadOnly = True
        dgv_mh_leyenda.Columns(1).Name = "ID"
        dgv_mh_leyenda.Columns(1).ValueType = GetType(String)
        dgv_mh_leyenda.Columns(1).ReadOnly = True
        dgv_mh_leyenda.Columns(2).Name = "Nombre"
        dgv_mh_leyenda.Columns(2).ValueType = GetType(String)
        dgv_mh_leyenda.Columns(3).Name = "Descripcion"
        dgv_mh_leyenda.Columns(3).ValueType = GetType(String)
        dgv_mh_leyenda.Columns(4).Name = "Litologia"
        dgv_mh_leyenda.Columns(4).ValueType = GetType(String)

        For Each kvp In responseJson.Item("response").Item("fhidrog")
            Dim row As String() = New String() {"", kvp.item("id_fhidrog"), kvp.item("n_fhidrog"), kvp.item("d_fhidrog"), kvp.item("litologia_g")}
            dgv_mh_leyenda.Rows.Add(row)
            dgv_mh_leyenda.Rows(dgv_mh_leyenda.RowCount - 1).Cells(0).Style.BackColor = Color.FromArgb(kvp.item("red"), kvp.item("green"), kvp.item("blue"))
        Next

        dgv_mh_leyenda.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgv_mh_leyenda.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgv_mh_leyenda.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgv_mh_leyenda.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgv_mh_leyenda.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgv_mh_leyenda.Sort(dgv_mh_leyenda.Columns(1), ListSortDirection.Ascending)
        dgv_mh_leyenda.Columns(2).Frozen = True

        lbl_mh_uhcount.Text = String.Format("Se encontraron {0} formaciones hidrogeológicas únicas", dgv_mh_leyenda.RowCount)

        tvw_mh_descriph.Nodes.Clear()
        uharray.Clear()
        clasificacionDescriptDict.Clear()


        For Each kvp In responseJson.Item("response").Item("chidrog")
            If tvw_mh_descriph.Nodes.ContainsKey(kvp.item("nombre")) Then
                Continue For
            End If
            Dim treeNode As TreeNode = New TreeNode(kvp.item("nombre"))
            treeNode.Name = kvp.item("id")
            treeNode.Tag = kvp.item("id_padre")
            If kvp.item("id_padre") = "999" Then
                uharray.Add(treeNode)
                tvw_mh_descriph.Nodes.Add(treeNode)
                Continue For
            End If
            clasificacionDescriptDict.Add(kvp.item("id"), "")
            For Each uh In uharray
                If uh.Name = kvp.item("id_padre") Then
                    uh.Nodes.Add(treeNode)
                End If
            Next
        Next




    End Sub
    Private Sub dgv_mh_leyenda_deletingrow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgv_mh_leyenda.UserDeletingRow
        Dim mgs As String = "¿Está seguro que desea eliminar este registro?"
        Dim r As DialogResult = MessageBox.Show(mgs, __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If r = DialogResult.No Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub dgv_mh_leyenda_deleterow(sender As Object, e As EventArgs) Handles dgv_mh_leyenda.UserDeletedRow
        lbl_mh_uhcount.Text = String.Format("Se encontraron {0} formaciones hidrogeológicas únicas", dgv_mh_leyenda.RowCount)
    End Sub

    Private Sub btn_mh_leyenda_Click(sender As Object, e As EventArgs) Handles btn_mh_leyenda.Click
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        dtable.Clear()
        For Each column As DataGridViewColumn In dgv_mh_leyenda.Columns
            If column.Name = "Leyenda" Then
                Continue For
            End If
            If dtable.Columns.Contains(column.Name) Then
                Continue For
            End If
            dtable.Columns.Add(column.HeaderText, column.ValueType)
        Next
        For Each row As DataGridViewRow In dgv_mh_leyenda.Rows
            dtable.Rows.Add()
            For Each cell As DataGridViewCell In row.Cells
                If cell.ColumnIndex = 0 Then
                    Continue For
                End If
                dtable.Rows(dtable.Rows.Count - 1)(cell.ColumnIndex - 1) = cell.Value.ToString()
            Next
        Next
        'dset.Clear()
        If dset.Tables.Count > 0 Then
            dset.Tables.Remove(dtable)
        End If
        dset.Tables.Add(dtable)
        Dim jsonstring As String = JsonConvert.SerializeObject(dset)

        'Dim maplex_annotate_map As IMaplexAnnotateMap = New MaplexAnnotateMapClass
        Dim maplexEngine As IAnnotateMap
        maplexEngine = New MaplexAnnotateMap()
        'Dim maplexEngine As IAnnotateMap = CType(New MaplexAnnotateMapClass(), IAnnotateMap)
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        'pMxDoc.AnnotationEngine = maplexEngine
        pMxDoc.FocusMap.AnnotationEngine = maplexEngine
        'MaplexAnnotateMapClass()
        'Map.AnnotationEngine = maplex_annotate_map As IAnnotateMap;
        Dim name_dataframe = UserControl_ComboBoxDataframes1.getDataframeSelected()


        Dim id_mapa = getIdMapa()
        Dim clasificacion_json As String = JsonConvert.SerializeObject(clasificacionDescriptDict)

        params.Clear()
        params.Add(jsonstring)
        params.Add(clasificacion_json)
        params.Add(id_mapa)
        params.Add(name_dataframe)

        ExecuteGP(_tool_generateLegendMhg, params, _toolboxPath_mapa_hidrogeologico, getresult:=False)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")

    End Sub
    Private Sub tvw_mh_descriph_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvw_mh_descriph.AfterSelect
        clickedNode = tvw_mh_descriph.SelectedNode.Name
        If tvw_mh_descriph.SelectedNode.Tag = "999" Then
            tbx_mh_descriph.Enabled = False
            Return
        End If
        tbx_mh_descriph.Enabled = True
        Dim text = clasificacionDescriptDict.Item(clickedNode)
        tbx_mh_descriph.Text = text
    End Sub

    Private Sub tbx_mh_descriph_TextChanged(sender As Object, e As EventArgs) Handles tbx_mh_descriph.TextChanged
        clasificacionDescriptDict(clickedNode) = tbx_mh_descriph.Text
    End Sub

    Private Sub btn_mgh_extrerdatos_Click(sender As Object, e As EventArgs) Handles btn_mgh_extrerdatos.Click
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        Dim features As List(Of String) = UserControl_CheckBoxAddLayers1.getLayerSelected()
        Dim features_as_string As String = String.Join(",", features)
        Dim codcuencasArray As New List(Of Object)
        For Each ikey As String In cuencasDictSelected.Keys
            codcuencasArray.Add(ikey)
        Next
        'Dim cuencas As String = String.Join(", ", codcuencasArray)
        params.Clear()
        params.Add(features_as_string)
        params.Add(cuencas)
        Dim dataframe As String = UserControl_CheckBoxAddLayers1.getNameDataFrame()
        params.Add(dataframe)
        ExecuteGP(_tool_clipLayerSelectedByCuenca, params, _toolboxPath_automapic, getresult:=False)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
        'MessageBox.Show(features_as_string, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btn_mgh_generar_mu_Click(sender As Object, e As EventArgs) Handles btn_mgh_generar_mu.Click
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        params.Clear()
        If rbt_mhg_nacional.Checked = True Then
            params.Add("1")
        Else
            params.Add("2")
        End If
        Dim codcuencasArray As New List(Of Object)
        For Each ikey As String In cuencasDictSelected.Keys
            codcuencasArray.Add(ikey)
        Next
        'Dim cuencas As String = String.Join(", ", codcuencasArray)
        params.Add(cuencas)
        Dim dataframe As String = UserControl_ComboBoxDataframes2.getDataframeSelected()
        If dataframe Is Nothing Then
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            MessageBox.Show("Debe seleccionar un dataframe", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        params.Add(dataframe)
        ExecuteGP(_tool_generateMapLocation, params, _toolboxPath_automapic, getresult:=False)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub
End Class