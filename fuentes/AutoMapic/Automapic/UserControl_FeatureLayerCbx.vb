Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class UserControl_FeatureLayerCbx
    Private layerNames As New List(Of String)
    Private dictLayers As New Dictionary(Of String, ILayer)
    Dim selectedLayer As ILayer
    Dim selectedLayerName As String

    Private Sub UpdateLayersOptions()
        mxdDoc = My.ArcMap.Application.Document
        map = mxdDoc.ActiveView.FocusMap
        ComboBoxLayers.Items.Clear()
        layerNames.Clear()
        dictLayers.Clear()

        Dim layer As ILayer

        For i As Integer = 0 To map.LayerCount - 1
            layer = map.Layer(i)
            layerNames.Add(layer.Name)
            dictLayers.Add(layer.Name, layer)
        Next


        ComboBoxLayers.Items.AddRange(layerNames.ToArray())

    End Sub

    Public Function getLayerSelected()
        Return selectedLayer
    End Function

    Public Function getSelectedLayerName()
        Return selectedLayerName
    End Function

    Private Sub ComboBoxLayers_DropDown(sender As Object, e As EventArgs) Handles ComboBoxLayers.DropDown
        UpdateLayersOptions()
    End Sub

    Private Sub ComboBoxLayers_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxLayers.SelectionChangeCommitted
        selectedLayer = map.Layer(ComboBoxLayers.SelectedIndex)
        selectedLayerName = selectedLayer.Name
    End Sub
End Class
