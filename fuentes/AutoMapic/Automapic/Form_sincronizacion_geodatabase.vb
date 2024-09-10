Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports Newtonsoft.Json
Imports System.Drawing


Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.IO

Module DataTableExt
    <Extension()>
    Sub ConvertColumnType(ByVal dt As DataTable, ByVal columnName As String, ByVal newType As Type)
        Using dc As DataColumn = New DataColumn(columnName & "_new", newType)
            Dim ordinal As Integer = dt.Columns(columnName).Ordinal
            dt.Columns.Add(dc)
            dc.SetOrdinal(ordinal)

            For Each dr As DataRow In dt.Rows
                dr(dc.ColumnName) = Convert.ChangeType(dr(columnName), newType)
            Next

            dt.Columns.Remove(columnName)
            dc.ColumnName = columnName
        End Using
    End Sub
End Module

Public Class Form_sincronizacion_geodatabase
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim ruta_origen As String
    Dim ruta_destino As String
    Dim csv_result As String
    Dim params As New List(Of Object)
    Dim dataset As String
    Private HeaderCheckBox As CheckBox = Nothing

    Private Sub Form_sincronizacion_geodatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar las opciones de filtro
        params.Clear()
        cbx_sg_filtrocapas.Items.Clear()
        ruta_origen = tbx_origen.Text()

        Dim response = ExecuteGP(_tool_getFilterModeOptions, params, _toolboxPath_sincronizacion_geodatabase)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If

        Dim filteroptions As String = responseJson.Item("response")
        filteroptions = Trim(filteroptions)
        Dim filteroptions_arr = filteroptions.Split(",")

        For Each i In filteroptions_arr
            cbx_sg_filtrocapas.Items.Add(i)
        Next
    End Sub

    Private Sub btn_sg_origen_Click(sender As Object, e As EventArgs) Handles btn_sg_origen.Click

        ruta_origen = openDialogBoxESRI(f_workspace)
        If ruta_origen Is Nothing Then
            'Cursor.Current = Cursors.Default
            Return
        End If
        tbx_origen.Text = ruta_origen

        If ruta_destino IsNot Nothing Then
            cbx_sg_filtrocapas.Enabled = True
        End If


    End Sub

    Private Sub btn_sg_destino_Click(sender As Object, e As EventArgs) Handles btn_sg_destino.Click

        ruta_destino = openDialogBoxESRI(f_workspace)
        If ruta_destino Is Nothing Then
            'Cursor.Current = Cursors.Default
            Return
        End If
        tbx_destino.Text = ruta_destino

        If ruta_origen IsNot Nothing Then
            cbx_sg_filtrocapas.Enabled = True
        End If

    End Sub

    Private Sub AdjustColumnOrder()
        'dg_sg_capas.Columns("existe_origen").ValueType = GetType(Boolean)
        dg_sg_capas.Columns("enviar").DisplayIndex = 0
        dg_sg_capas.Columns("existe_origen").Visible = False
        dg_sg_capas.Columns("origen").DisplayIndex = 1
        dg_sg_capas.Columns("origen").HeaderText = "ORIGEN"
        dg_sg_capas.Columns("existe_destino").Visible = False
        dg_sg_capas.Columns("nombre_destino").DisplayIndex = 2
        dg_sg_capas.Columns("nombre_destino").HeaderText = "DESTINO"
        dg_sg_capas.Columns("num_origen").DisplayIndex = 3
        dg_sg_capas.Columns("num_origen").HeaderText = "REG. ORIGEN"
        dg_sg_capas.Columns("num_destino").DisplayIndex = 4
        dg_sg_capas.Columns("num_destino").HeaderText = "REG. DESTINO"
        dg_sg_capas.Columns("source").DisplayIndex = 5
        dg_sg_capas.Columns("source").Visible = False



        'Configuracion de la visualizacion del datagrid
        dg_sg_capas.AllowUserToAddRows = False
        dg_sg_capas.RowHeadersVisible = False
        dg_sg_capas.BackgroundColor = Color.FromArgb(240, 240, 240)
        dg_sg_capas.Dock = DockStyle.Fill
        dg_sg_capas.Columns(0).Resizable = False
        'dg_sg_capas.AllowUserToResizeColumns = False

        dg_sg_capas.Columns("enviar").Width = 48
        dg_sg_capas.Columns("origen").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dg_sg_capas.Columns("nombre_destino").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dg_sg_capas.Columns("num_origen").Width = 75
        'dg_sg_capas.Columns("num_origen").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dg_sg_capas.Columns("num_destino").MinimumWidth = 80
        dg_sg_capas.Columns("num_destino").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        For Each row As DataGridViewRow In dg_sg_capas.Rows

            If Convert.ToInt32(row.Cells("num_destino").Value) < Convert.ToInt32(row.Cells("num_origen").Value) Then
                row.DefaultCellStyle.ForeColor = Color.DarkOrange
            End If

            If Convert.ToInt32(row.Cells("existe_destino").Value) = 0 Then
                row.DefaultCellStyle.ForeColor = Color.Green
            End If

            If Convert.ToInt32(row.Cells("existe_origen").Value) = 0 Then
                'row.ReadOnly = True
                row.DefaultCellStyle.ForeColor = Color.FromArgb(160, 160, 160)
                row.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            End If

            If Convert.ToString(row.Cells("source").Value) = "0" Then
                row.ReadOnly = True
                row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 0, 250)

            End If

        Next
        For Each column As DataGridViewColumn In dg_sg_capas.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

    End Sub

    Private Sub cbx_sg_filtrocapas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_sg_filtrocapas.SelectedIndexChanged
        'dg_sg_capas.DataSource = vbNull
        dg_sg_capas.Columns.Clear()

        Dim filtro_value As String = cbx_sg_filtrocapas.SelectedItem
        If rbtn_sg_estds.Checked = True Then
            dataset = "si"
        ElseIf rbtn_sg_estraiz.Checked = True Then
            dataset = "no"
        End If
        If filtro_value = "Archivo CSV" Then
            filtro_value = openDialogBoxESRI(f_table)
            If filtro_value Is Nothing Then
                Return
            End If
        ElseIf filtro_value = "Archivo MXD Externo" Then
            filtro_value = openDialogBoxESRI(f_mxd)
            If filtro_value Is Nothing Then
                Return
            End If
        End If
        runProgressBar()

        params.Clear()
        params.Add(ruta_origen)
        params.Add(ruta_destino)
        params.Add(filtro_value)
        params.Add(dataset)


        Dim response = ExecuteGP(_tool_getListOfLayers, params, _toolboxPath_sincronizacion_geodatabase)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If

        Dim TablaCapas = responseJson.Item("response")
        Dim firstline = TablaCapas(0)
        Dim firstline_dict As Dictionary(Of String, String) = firstline.ToObject(Of Dictionary(Of String, String))()

        'Modo de agregar por datatable
        Dim dt As DataTable = New DataTable()
        For Each column As String In firstline_dict.Keys
            dt.Columns.Add(column)
        Next

        For Each valor In TablaCapas
            Dim dataRow As DataRow = dt.NewRow()
            Dim valor_dict As Dictionary(Of String, String) = valor.ToObject(Of Dictionary(Of String, String))()

            For Each column As String In valor_dict.Keys
                dataRow(column) = valor_dict(column)
            Next

            dt.Rows.Add(dataRow)
        Next

        dt.ConvertColumnType("enviar", GetType(Integer))
        dt.ConvertColumnType("enviar", GetType(Boolean))
        dt.ConvertColumnType("num_origen", GetType(Integer))
        dt.ConvertColumnType("num_destino", GetType(Integer))


        'reordenamos el datatable
        dg_sg_capas.DataSource = dt
        AdjustColumnOrder()
        runProgressBar("ini")

    End Sub
    Private Sub ResetHeaderCheckBoxLocation(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim oRectangle As Rectangle = Me.dg_sg_capas.GetCellDisplayRectangle(ColumnIndex, RowIndex, True)
        Dim oPoint As Point = New Point()
        oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1
        oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1
        HeaderCheckBox.Location = oPoint
    End Sub

    Private Sub HeaderCheckBoxClick(ByVal HCheckBox As CheckBox)
        'IsHeaderCheckBoxClicked = True

        For Each Row As DataGridViewRow In dg_sg_capas.Rows
            CType(Row.Cells("checked"), DataGridViewCheckBoxCell).Value = HCheckBox.Checked
        Next

        dg_sg_capas.RefreshEdit()
        'TotalCheckedCheckBoxes = If(HCheckBox.Checked, TotalCheckBoxes, 0)
        'IsHeaderCheckBoxClicked = False
    End Sub



    Private Sub btn_sg_enviar_Click(sender As Object, e As EventArgs) Handles btn_sg_enviar.Click
        If dg_sg_capas.Rows.Count > 0 Then
            Dim fileName As String = String.Format("{0}\file_dgrid.csv", __tempdir__)
            'Dim FileName As String = "D:\prueba1\file_dgrid.csv"
            'sfd.FileName = "Output.csv"
            Dim fileError As Boolean = False

            If True Then
                'If sfd.ShowDialog() = DialogResult.OK Then

                If File.Exists(FileName) Then

                    Try
                        File.Delete(FileName)
                    Catch ex As IOException
                        fileError = True
                        MessageBox.Show("No fue posible escribir el archivo csv resultado" & ex.Message)
                    End Try
                End If

                If Not fileError Then

                    Try
                        Dim columnCount As Integer = dg_sg_capas.Columns.Count
                        Dim columnNames As String = ""
                        Dim outputCsv As String() = New String(dg_sg_capas.Rows.Count + 1 - 1) {}

                        For num As Integer = 0 To columnCount - 1
                            columnNames += dg_sg_capas.Columns(num).Name.ToString() & ","
                        Next

                        outputCsv(0) += columnNames
                        Dim i As Integer = 1

                        While (i - 1) < dg_sg_capas.Rows.Count

                            For j As Integer = 0 To columnCount - 1
                                outputCsv(i) += dg_sg_capas.Rows(i - 1).Cells(j).Value.ToString() & ","
                            Next

                            i += 1
                        End While

                        File.WriteAllLines(FileName, outputCsv, Encoding.UTF8)
                        csv_result = FileName
                        Dim usuario = user

                        runProgressBar()
                        params.Clear()
                        params.Add(ruta_origen)
                        params.Add(ruta_destino)
                        params.Add(csv_result)
                        params.Add(usuario)
                        Dim response = ExecuteGP(_tool_sendFilesToGDB, params, _toolboxPath_sincronizacion_geodatabase)
                        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
                        If responseJson.Item("status") = 0 Then
                            RuntimeError.PythonError = responseJson.Item("message")
                            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

                            runProgressBar("ini")
                            'Cursor.Current = Cursors.Default
                            Return
                        End If

                        Dim numeroCapasExportadas = responseJson.Item("response")
                        Dim mensaje_exportadas As String = "Se exportaron satisfactoriamente " & numeroCapasExportadas & "capas"

                        runProgressBar("end")
                        runProgressBar("ini")
                        MessageBox.Show(mensaje_exportadas, "Sinc. Geodatabase")
                    Catch ex As Exception
                        runProgressBar("ini")
                        MessageBox.Show("Error :" & ex.Message)
                    End Try
                End If
            End If
        Else
            MessageBox.Show("No Record To Export !!!", "Info")
        End If


    End Sub
End Class
