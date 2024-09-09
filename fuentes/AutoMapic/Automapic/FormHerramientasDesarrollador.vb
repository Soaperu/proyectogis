Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports Newtonsoft.Json
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.IO

Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports System.Drawing
Imports System.ComponentModel
Imports ESRI.ArcGIS.Maplex

Public Class FormHerramientasDesarrollador
    Dim layer_name As String = ""

    Private Sub btn_load_style_Click(sender As Object, e As EventArgs) Handles btn_load_style.Click
        'Cursor.Current = Cursors.WaitCursor

        Dim validador = OpenFileDialog1.ShowDialog()
        Dim path_style_temp = OpenFileDialog1.FileName

        If validador = 2 Then
            Return
        End If
        'Dim path_style_temp = openDialogBoxESRI(f_file)
        'If path_style_temp Is Nothing Then
        '    Return
        'End If
        tbx_estilos.Text = path_style_temp
    End Sub

    Private Sub btn_apply_style_Click(sender As Object, e As EventArgs) Handles btn_apply_style.Click
        Dim query As String = "1=1"
        layer_name = UserControl_FeatureLayerCbx1.getSelectedLayerName()
        If RadioButton2.Checked Then
            MatchPersonalizedSymbolsUlito(layer_name, "CODI", tbx_estilos.Text, query, ulitoLabelsDictionary)
            Return
        End If
        MatchPersonalizedSymbolsUlito(layer_name, "CODI", tbx_estilos.Text)
    End Sub

    Private Sub FormHerramientasDesarrollador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbx_estilos.Text = _style_path
    End Sub
End Class