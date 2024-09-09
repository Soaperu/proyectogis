Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI

Public Class UserControl_ComboBoxDataframes
    Dim name_data_frame As String = Nothing
    Private Sub cbx_uc_dataframes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_uc_dataframes.SelectedIndexChanged
        name_data_frame = cbx_uc_dataframes.SelectedItem()
    End Sub
    Public Function getDataframeSelected()
        Return name_data_frame
    End Function
    Private Sub UpdateDataframesOptions()
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        cbx_uc_dataframes.Items.Clear()
        For i As Integer = 0 To pMxDoc.Maps.Count - 1
            Dim df As String = pMxDoc.Maps.Item(i).Name
            cbx_uc_dataframes.Items.Add(df)
        Next
    End Sub
    Private Sub cbx_uc_dataframes_(sender As Object, e As EventArgs) Handles cbx_uc_dataframes.DropDown
        UpdateDataframesOptions()
    End Sub
End Class
