Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Newtonsoft.Json


Public Class Form_diagrama_esfuerzos

    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim ruta_gd As String
    Dim ruta_gds As String
    Dim ruta_data As String
    Dim valid_data As Integer
    Dim value_Checked As Integer
    Dim validador As Integer ' 1: si, 2: no
    Dim params As New List(Of Object)
    Dim ListAz As New List(Of String)
    Dim ListBz As New List(Of String)
    Dim ListRk As New List(Of String)
    Dim pathPicture As String
    Dim checkedPolo As String = 0 ' 0: no, 1: si
    Dim checkedRake As String = 0 ' 0: no, 1: si


    Private Sub rbtn_de_csv_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_de_csv.CheckedChanged
        If rbtn_de_csv.Checked = True Then
            value_Checked = 1
        End If
    End Sub

    Private Sub rbtn_de_txt_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_de_txt.CheckedChanged
        If rbtn_de_txt.Checked = True Then
            value_Checked = 2
        End If
    End Sub

    Private Sub rbtn_de_shp_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_de_shp.CheckedChanged
        If rbtn_de_shp.Checked = True Then
            value_Checked = 3
        End If
    End Sub

    Private Sub btn_de_updata_Click(sender As Object, e As EventArgs) Handles btn_de_updata.Click
        runProgressBar()
        dgv_de_data.Rows.Clear()
        Dim OpenFileDialog1 As OpenFileDialog
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        'validador = 2
        If (value_Checked = 1) Then
            OpenFileDialog1.Filter = "CSV files (*.csv)|*.csv"
        ElseIf (value_Checked = 2) Then
            OpenFileDialog1.Filter = "Text files (*.txt)|*.txt"
        Else value_Checked = 3
            tbx_de_pathfile.Text = openDialogBoxESRI(f_shapefile)
            If tbx_de_pathfile.Text <> "" Then
                validador = 1
            Else
                validador = 2
            End If
        End If

        If value_Checked = 1 Or value_Checked = 2 Then
            validador = OpenFileDialog1.ShowDialog()
            ruta_data = OpenFileDialog1.FileName
            If validador = 1 Then
                tbx_de_pathfile.Text = ruta_data
                btn_de_generar.Enabled = True
                valid_data = 1
            End If
        End If
        If validador = 2 Then
            valid_data = 0
            btn_de_generar.Enabled = False
            Return
        End If
        params.Clear()
        params.Add(tbx_de_pathfile.Text)
        Try
            dgv_de_data.Rows.Clear()
            Dim response = ExecuteGP(_tool_validationValues, params, _toolboxPath_diagrama_esfuerzos)
            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            Dim data = responseJson.Item("response")
            Dim obs = responseJson.Item("observado")
            If obs = "observado" Then
                Dim m_status As String = "Tabla con inconsistencias, verificar y corregir"
                MessageBox.Show(m_status, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Dim jArray As Linq.JArray = data
                ' Encontrar el número de filas necesarias
                Dim numRows As Integer = responseJson.Item("countrows")
            ' Agregar las filas vacías al DataGridView
            For i As Integer = 1 To numRows
                dgv_de_data.Rows.Add()
            Next
            ' Llenar el DataGridView con los datos
            Dim columnIndex As Integer = 1
            For Each column As Linq.JToken In jArray
                Dim columnList As List(Of String) = column.ToObject(Of List(Of String))()
                For rowIndex As Integer = 0 To columnList.Count - 1
                    dgv_de_data.Rows(rowIndex).Cells(columnIndex).Value = columnList(rowIndex)
                Next
                columnIndex += 1
            Next
            For Each row As DataGridViewRow In dgv_de_data.Rows
                row.Cells(0).Value = True
            Next

            ValidationDataGridView()
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            runProgressBar("ini")
        End Try
    End Sub

    Private Sub btn_de_generar_Click(sender As Object, e As EventArgs) Handles btn_de_generar.Click
        runProgressBar()
        ValidationDataGridView()
        If validador = 2 Then
            Dim message As String = "Tabla con inconsistencias, corregir..."
            MessageBox.Show(message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            runProgressBar("ini")
            Return
        End If
        ' Crear listas para almacenar los datos de cada columna
        Dim column_az As New List(Of String)
        Dim column_bz As New List(Of String)
        Dim column_rk As New List(Of String)

        ' Recorrer todas las columnas del DataGridView empezando desde la segunda columna
        For colIndex As Integer = 1 To dgv_de_data.ColumnCount - 1
            ' Variable para rastrear si toda la columna está vacía
            Dim isColumnEmpty As Boolean = True
            ' Recorrer todas las filas del DataGridView para la columna actual
            For Each row As DataGridViewRow In dgv_de_data.Rows
                ' Asegurarse de que la fila no esté vacía o no sea la última fila vacía
                If Not row.IsNewRow Then
                    Dim cellValue As String = row.Cells(colIndex).Value
                    If Not String.IsNullOrEmpty(cellValue) Then
                        isColumnEmpty = False
                        Exit For
                    End If
                End If
            Next
            ' Si la columna no está vacía, leer los datos
            If Not isColumnEmpty Then
                For Each row As DataGridViewRow In dgv_de_data.Rows
                    ' Asegurarse de que la fila no esté vacía o no sea la última fila vacía
                    If Not row.IsNewRow Then
                        If row.Cells(0).Value.ToString() = "True" Then
                            ' Almacenar los datos en la lista correspondiente
                            Select Case colIndex
                                Case 1
                                    column_az.Add(row.Cells(colIndex).Value.ToString())
                                Case 2
                                    column_bz.Add(row.Cells(colIndex).Value.ToString())
                                Case 3
                                    column_rk.Add(row.Cells(colIndex).Value.ToString())
                            End Select
                        End If
                    End If
                Next
            End If
        Next

        Dim columns As New List(Of List(Of String))
        columns.Add(column_az)
        columns.Add(column_bz)
        columns.Add(column_rk)
        'Dim objectList As New List(Of Object) From {1, "texto", 3.0}
        Dim sb As New StringBuilder()
        sb.Append(checkedPolo.ToString())
        sb.Append(checkedRake.ToString())
        sb.Append("_")
        For Each innerList As List(Of String) In columns
            For Each item As String In innerList
                sb.Append(item)
                sb.Append(";") ' ";" como separador; puedes cambiarlo si prefieres otro
            Next
            ' Quitar el último espacio si lo necesitas
            sb.Length -= 1
            'sb.AppendLine() ' Nueva línea para separar las listas internas
            sb.Append("\n")
        Next

        Dim finalString As String = sb.ToString()
        params.Clear()
        params.Add(finalString)
        Try
            'Dim response = ExecuteGP(_tool_generateDiagram, params, _toolboxPath_diagrama_esfuerzos)
            'Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            'Dim pathPicture = responseJson.Item("response")
            pathPicture = ejecutar_procesos(_bat_generateDiagram, finalString)
            pbox_de_diagrama.Image = Drawing.Image.FromFile(pathPicture)
            pbox_de_diagrama.SizeMode = PictureBoxSizeMode.StretchImage
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            runProgressBar("ini")
        End Try
        dgv_de_data.ReadOnly = True
    End Sub

    Public Sub ValidationDataGridView()
        ' Crear listas para almacenar los datos de cada columna
        ListAz.Clear()
        ListBz.Clear()
        ListRk.Clear()
        ' ... (tantas listas como columnas tenga tu DataGridView)
        validador = 1
        ' Recorrer todas las columnas del DataGridView empezando desde la segunda columna
        For colIndex As Integer = 1 To dgv_de_data.ColumnCount - 1
            ' Variable para rastrear si toda la columna está vacía
            Dim isColumnEmpty As Boolean = True

            ' Recorrer todas las filas del DataGridView para la columna actual
            For Each row As DataGridViewRow In dgv_de_data.Rows
                ' Asegurarse de que la fila no esté vacía o no sea la última fila vacía
                If Not row.IsNewRow Then
                    Dim cellValue As String = row.Cells(colIndex).Value
                    If Not String.IsNullOrEmpty(cellValue) Then
                        isColumnEmpty = False
                        Exit For
                    End If
                End If
            Next

            ' Si la columna no está vacía, leer los datos
            If Not isColumnEmpty Then
                For Each row As DataGridViewRow In dgv_de_data.Rows
                    ' Asegurarse de que la fila no esté vacía o no sea la última fila vacía
                    If Not row.IsNewRow Then
                        Dim valueInt As String = row.Cells(colIndex).Value
                        Dim valueString As String = row.Cells(colIndex).Value.ToString()
                        ' Almacenar los datos en la lista correspondiente
                        Select Case colIndex
                            Case 1
                                ListAz.Add(valueString)
                                row.Cells(colIndex).Style.BackColor = Drawing.Color.White
                                If valueInt > 360 Or valueInt < 0 Then
                                    'Pintar la celda de color 
                                    row.Cells(colIndex).Style.BackColor = Drawing.Color.FromArgb(241, 172, 196)
                                    validador = 2
                                End If
                            Case 2
                                ListBz.Add(valueString)
                                row.Cells(colIndex).Style.BackColor = Drawing.Color.White
                                If valueInt > 90 Or valueInt < 0 Then
                                    row.Cells(colIndex).Style.BackColor = Drawing.Color.FromArgb(241, 172, 196)
                                    validador = 2
                                End If
                            Case 3
                                ListRk.Add(valueString)
                                row.Cells(colIndex).Style.BackColor = Drawing.Color.White
                                If valueString > 180 Or valueString < -180 Then
                                    row.Cells(colIndex).Style.BackColor = Drawing.Color.FromArgb(241, 172, 196)
                                    validador = 2
                                End If
                                ' ... (tantas columnas como tenga tu DataGridView)
                        End Select
                    End If
                Next
            End If
        Next

        ' Ahora tienes los datos de cada columna almacenados en las listas
        ' Puedes manipular o usar estos datos como prefieras
    End Sub

    Private Sub btn_de_editDatagrid_Click(sender As Object, e As EventArgs) Handles btn_de_editDatagrid.Click
        Dim message As String = "Desea editar la tabla?"
        Dim edit = MessageBox.Show(message, __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If edit.ToString() = "Yes" Then
            dgv_de_data.ReadOnly = False
        End If
    End Sub

    Public Function ejecutar_procesos(bat As String, params As String)
        Dim p As New Process
        Dim output As String
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.FileName = bat
        p.StartInfo.Arguments = """" + params + """"
        p.StartInfo.CreateNoWindow = True
        p.Start()
        output = p.StandardOutput.ReadToEnd()
        p.WaitForExit()

        'Dim json As Linq.JObject = Linq.JObject.Parse(output)
        Return output

    End Function

    Private Sub btn_downloadImg_Click(sender As Object, e As EventArgs) Handles btn_downloadImg.Click
        ' Crear cuadro de diálogo para guardar archivo
        'Dim saveFileDialog As New SaveFileDialog()
        SaveFileImg.Filter = "Image files (*.jpg, *.jpeg, *.png) |*.jpg; *.jpeg; *.png"
        SaveFileImg.Title = "Guardar como"
        ' Mostrar cuadro de diálogo
        If SaveFileImg.ShowDialog() = DialogResult.OK Then
            ' Obtener la ruta definitiva donde se guardará el archivo
            Dim definitiveFilePath As String = SaveFileImg.FileName
            ' Mover el archivo desde la ubicación temporal a la ubicación definitiva
            Try
                Dim bmpImage As Bitmap = New Bitmap(pathPicture)
                bmpImage.Save(definitiveFilePath, Imaging.ImageFormat.Png)
                bmpImage.Dispose()
                'IO.File.Move(pathPicture, definitiveFilePath)
                'MessageBox.Show("Archivo guardado con éxito.")
            Catch ex As Exception
                MessageBox.Show("Error al guardar el archivo: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub cbx_de_polo_CheckedChanged(sender As Object, e As EventArgs) Handles cbx_de_polo.CheckedChanged
        If cbx_de_polo.Checked Then
            checkedPolo = 1
        Else
            checkedPolo = 0
        End If
    End Sub

    Private Sub cbx_de_rake_CheckedChanged(sender As Object, e As EventArgs) Handles cbx_de_rake.CheckedChanged
        If cbx_de_rake.Checked Then
            checkedRake = 1
        Else
            checkedRake = 0
        End If
    End Sub
End Class