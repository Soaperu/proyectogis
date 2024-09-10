Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class UserControl_ComboBoxRegiones
    Dim params_ui_cbox As New List(Of Object)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim regionesDictByCombobox As New Dictionary(Of String, String)
    Dim cd_depa As String
    Private Sub UserControl_ComboBoxRegiones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setOptionsComboBox()
        'RemoveHandler cbx_ucr_region.SelectedIndexChanged, AddressOf cbx_ucr_region_SelectedIndexChanged
        'params_ui_cbox.Clear()
        'Dim response As String = ExecuteGP(_tool_getRegions, params_ui_cbox, tbxpath:=_toolboxPath_automapic)
        'Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        'If responseJson.Item("status") = 0 Then
        '    RuntimeError.PythonError = responseJson.Item("message")
        '    MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return
        'End If

        'For Each current In responseJson.Item("response")
        '    regionesDictByCombobox.Add(current(0), current(1))
        'Next

        'cbx_ucr_region.DataSource = New BindingSource(regionesDictByCombobox, Nothing)
        'cbx_ucr_region.DisplayMember = "Value"
        'cbx_ucr_region.ValueMember = "Key"

        'AddHandler cbx_ucr_region.SelectedIndexChanged, AddressOf cbx_ucr_region_SelectedIndexChanged
    End Sub
    Public Sub setOptionsComboBox()
        RemoveHandler cbx_ucr_region.SelectedIndexChanged, AddressOf cbx_ucr_region_SelectedIndexChanged
        params_ui_cbox.Clear()
        Dim response As String = ExecuteGP(_tool_getRegions, params_ui_cbox, tbxpath:=_toolboxPath_automapic)
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

        cbx_ucr_region.DataSource = New BindingSource(regionesDictByCombobox, Nothing)
        cbx_ucr_region.DisplayMember = "Value"
        cbx_ucr_region.ValueMember = "Key"

        AddHandler cbx_ucr_region.SelectedIndexChanged, AddressOf cbx_ucr_region_SelectedIndexChanged
    End Sub
    Private Sub cbx_ucr_region_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ucr_region.SelectedIndexChanged
        cd_depa = (CType(cbx_ucr_region.SelectedItem, KeyValuePair(Of String, String))).Key
    End Sub

    Public Function getRegionSelected()
        Return cd_depa
    End Function
End Class
