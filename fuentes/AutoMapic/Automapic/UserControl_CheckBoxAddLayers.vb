Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports ESRI.ArcGIS.Maplex
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI

Public Class UserControl_CheckBoxAddLayers
    Dim params_ui_cbox As New List(Of Object)
    Dim arrayParents As New List(Of Object)
    Dim arrayChilds As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim zona_geografica_ui_cbox As String
    Dim query_ui_cbox As String
    Dim enabledAddOrRemove As Boolean
    'Dim enabledDataFrame As Boolean
    Private Sub UserControl_CheckBoxAddLayers_Load(sender As Object, e As EventArgs) Handles Me.Load
        'UserControl_ComboBoxDataframes1.Enabled = False
    End Sub
    Public Sub LoadOptions(category As String, Optional zona As String = Nothing, Optional query As String = Nothing, Optional addOrRemove As Boolean = True)
        If tvw_layers.Nodes.Count > 0 Then
            Return
        End If
        enabledAddOrRemove = addOrRemove
        'enabledDataFrame = selectDataFrame
        'If enabledAddOrRemove = False Then
        UserControl_ComboBoxDataframes1.Visible = enabledAddOrRemove
        'End If
        'UserControl_ComboBoxDataframes1.Enabled = enabledAddOrRemove
        params_ui_cbox.Clear()
        params_ui_cbox.Add(category)
        tvw_layers.Nodes.Clear()
        arrayChilds.Clear()
        zona_geografica_ui_cbox = zona
        query_ui_cbox = query
        Dim response = ExecuteGP(_tool_treeLayers, params_ui_cbox, _toolboxPath_automapic)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If

        RemoveHandler tvw_layers.BeforeCheck, AddressOf tvw_layers_BeforeCheck
        RemoveHandler tvw_layers.AfterCheck, AddressOf tvw_layers_AfterCheck
        For Each kvp In responseJson.Item("response")
            If tvw_layers.Nodes.ContainsKey(kvp.item("description")) Then
                Continue For
            End If
            Dim treeNode As TreeNode = New TreeNode(kvp.item("description"))
            treeNode.Name = kvp.item("id")
            treeNode.Tag = kvp.item("parent")
            If kvp.item("parent") = "999" Then
                arrayParents.Add(treeNode)
                tvw_layers.Nodes.Add(treeNode)
                treeNode.Checked = True
                'tvw_layers.Checked = e.Node.Checked
                Continue For
            End If
            'If kvp.item("state") = 1 Then
            '    treeNode.Checked = True
            'End If
            For Each prn In arrayParents
                If prn.Name = kvp.item("parent") Then
                    treeNode.Tag = kvp.item("datasource").value & "\" & kvp.item("feature").value
                    prn.Nodes.Add(treeNode)
                    arrayChilds.Add(kvp)
                    Continue For
                End If
            Next
        Next
        AddHandler tvw_layers.BeforeCheck, AddressOf tvw_layers_BeforeCheck
        AddHandler tvw_layers.AfterCheck, AddressOf tvw_layers_AfterCheck

        Dim maplexEngine As IAnnotateMap
        maplexEngine = New MaplexAnnotateMap()
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        pMxDoc.FocusMap.AnnotationEngine = maplexEngine
    End Sub
    Private Sub tvw_layers_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles tvw_layers.BeforeCheck
        Dim clickedNode = e.Node
        If clickedNode.Tag = "999" Then
            e.Cancel = True
        End If
    End Sub
    Private Sub tvw_layers_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvw_layers.AfterCheck
        Dim clickedNode = e.Node
        'If clickedNode Is Nothing Then
        '    Return
        'End If
        Dim df As String
        If enabledAddOrRemove = True Then
            df = UserControl_ComboBoxDataframes1.getDataframeSelected()
            If df Is Nothing Then
                MessageBox.Show("Seleccione un DataFrame", __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Else
            df = Nothing
        End If


        For Each kvp In arrayChilds
            If kvp.item("id") = Integer.Parse(clickedNode.Name) Then
                params_ui_cbox.Clear()
                If clickedNode.Checked Then
                    clickedNode.ImageIndex = 2
                    params_ui_cbox.Add(clickedNode.Name)
                    params_ui_cbox.Add(zona_geografica_ui_cbox)
                    params_ui_cbox.Add(df)
                    params_ui_cbox.Add(query_ui_cbox)

                    ExecuteGP(_tool_addLayerToDataFrame, params_ui_cbox, _toolboxPath_automapic, getresult:=False)
                Else
                    clickedNode.ImageIndex = 0
                    params_ui_cbox.Add(clickedNode.Name)
                    params_ui_cbox.Add(df)
                    ExecuteGP(_tool_removeFeatureOfTOC, params_ui_cbox, _toolboxPath_automapic, getresult:=False)
                End If

                Exit For
            End If


        Next
    End Sub
    Public Function getChildsLayerSelected(parentNode As TreeNode, nodes_checked As List(Of String))
        For Each childNode As TreeNode In parentNode.Nodes
            If childNode.Checked = True Then
                nodes_checked.Add(childNode.Name)
                getChildsLayerSelected(childNode, nodes_checked)
            End If
        Next
        Return Nothing
    End Function
    Public Function getLayerSelected()
        Dim nodes_checked As New List(Of String)
        For Each treenode In tvw_layers.Nodes
            If treenode.tag = "999" Then
                getChildsLayerSelected(treenode, nodes_checked)
            End If
        Next
        Return nodes_checked
    End Function
    Public Function getNameDataFrame()
        Dim nameDataFrame = UserControl_ComboBoxDataframes1.getDataframeSelected()
        Return nameDataFrame
    End Function

    Private Sub tvw_layers_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvw_layers.AfterSelect

    End Sub
    'Public Function getListChildsByState(checked As Boolean)
    '    For i in 
    'End Function
End Class
