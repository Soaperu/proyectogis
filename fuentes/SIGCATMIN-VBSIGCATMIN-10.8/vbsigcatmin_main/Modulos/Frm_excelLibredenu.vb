Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Geodatabase
Imports Microsoft.Office.Interop

Public Class Frm_excelLibredenu
    Public MxDocument As IMxDocument
    Private clsClase As New cls_excel
    Private lodtRegistro As New DataTable
    Private loNombreHoja As String = ""
    Public m_Application As IApplication




    Private Sub btnArchivo_Click(sender As Object, e As EventArgs) Handles btnArchivo.Click
        'Para Cualquier version del excel
        Try
            Dim openFD As New OpenFileDialog
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*.xlsx|Todos los archivos (*.*)|*.*"
                .Multiselect = False
                .InitialDirectory = "C:\" ' My.Computer.FileSystem.SpecialDirectories.Desktop
                '.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    glo_PathXLS = .FileName
                    txtNomArchivo.Text = glo_PathXLS
                    loNombreHoja = NombreHoja(.FileName)
                Else
                    Exit Sub
                End If
            End With
            cbocodigo.Items.Clear()
            'cboNorte.Items.Clear()
            'cboAgrupar.Items.Clear()
            'cboOrdenar.Items.Clear()
            Call importar1()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Frm_excelLibredenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lodtRegistro = New DataTable
    End Sub
    Private Function NombreHoja(ByVal fileName As String) As String
        Try
            Cursor = Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim wb As Excel.Workbook
            wb = xlApp.Workbooks.Open(fileName)
            ' cbo_Hojas.Items.Clear()
            For Each sheet As Excel.Worksheet In wb.Worksheets
                Return sheet.Name
                Exit For
            Next
            wb.Close()
            wb = Nothing
            xlApp.Quit()
            xlApp = Nothing
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Microsoft")
        End Try
    End Function
    Private Sub btnSelArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Para Cualquier version del excel
        Try
            Dim openFD As New OpenFileDialog
            With openFD
                .Title = "Seleccionar archivos"
                .Filter = "Archivos Excel(*.xls;*.xlsx)|*.xls;*.xlsx|Todos los archivos (*.*)|*.*"
                .Multiselect = False
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    glo_PathXLS = .FileName

                Else
                    Exit Sub
                End If
            End With
            Call importar1()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub importar1()
        Dim stConexion As String = ""
        Try
            stConexion = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (glo_PathXLS & ";Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=2"";")))
        Catch ex As Exception
            stConexion = ("Provider=Microsoft.ACE.OLEDB.12.0;" & ("Data Source=" & (glo_PathXLS & ";Extended Properties=""Excel 8.0 Xml;HDR=YES;IMEX=1"";")))
        End Try
        lodtRegistro = New DataTable
        Dim cnConex As New OleDbConnection(stConexion)
        ' Dim Cmd As New OleDbCommand("Select * from [Hoja1$]")
        Dim Cmd As New OleDbCommand("Select * from [" & loNombreHoja & "$]")
        Dim Ds As New DataSet
        Dim Da As New OleDbDataAdapter
        Dim Dt As New DataTable
        Try
            cnConex.Open()
            Cmd.Connection = cnConex
            Da.SelectCommand = Cmd
            Da.Fill(Ds)
            Dt = Ds.Tables(0)


            cbocodigo.Items.Add("--Seleccionar--")
            'cboNorte.Items.Add("--Seleccionar--")
            'cboOrdenar.Items.Add("--Seleccionar--")
            'cboAgrupar.Items.Add("--Seleccionar--")
            For i As Integer = 0 To Dt.Columns.Count - 1
                cbocodigo.Items.Add(Dt.Columns.Item(i).ColumnName)
                '   cboNorte.Items.Add(Dt.Columns.Item(i).ColumnName)
                '  cboOrdenar.Items.Add(Dt.Columns.Item(i).ColumnName)
                ' cboAgrupar.Items.Add(Dt.Columns.Item(i).ColumnName)

                lodtRegistro.Columns.Add(Dt.Columns.Item(i).ColumnName, Type.GetType("System.String"))

            Next



            Dim dRow As DataRow
            For i As Integer = 0 To Dt.Rows.Count - 1
                dRow = lodtRegistro.NewRow
                For J As Integer = 0 To Dt.Columns.Count - 1
                    dRow.Item(J) = Dt.Rows(i).Item(J)
                Next
                lodtRegistro.Rows.Add(dRow)
                '   lstlistado.Items.Add(Dt.Rows(r).Item("CODIGO")
            Next
            'MsgBox(Dt.Rows.Count)


            cbocodigo.SelectedIndex = 0

        Catch ex As Exception
            cnConex.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        cnConex.Close()
    End Sub

    Private Sub cbocodigo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbocodigo.SelectedIndexChanged
        gloNameCodigo = cbocodigo.Text

    End Sub

    Private Sub btnGraficar_Click(sender As Object, e As EventArgs) Handles btnGraficar.Click
        Dim pDatum As String = ""
        Dim vCodigo As String = ""
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        conta_libredenu = 0
        v_masivo_eval = "1"



        If cbocodigo.SelectedIndex = 0 Then
            cbocodigo.Focus()
            MsgBox("Seleccione campo codigo..", MsgBoxStyle.Information, "[Aviso]")
            Exit Sub
        End If

        gloNameCodigo = cbocodigo.Text

        Dim loStrShapefile As String = "P_" & DateTime.Now.Ticks.ToString()
        Try

            Me.Close()
            '    procesoautmatico_eval = "automatico"
            clsClase.Procesa_libredenunexcel(loStrShapefile, lodtRegistro, m_application)
            caso_ldmasivo = "0"
            MsgBox("El Proceso Termino Satisfactoriamente...", MsgBoxStyle.Exclamation, "SIGCATMIN LIBRENU...")


            DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            MsgBox("Error en leer el Archivo Excel, debes colocar los campos sin espacio", MsgBoxStyle.Information, "SIGCATMIN")
        End Try
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class