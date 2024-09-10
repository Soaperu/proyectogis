Imports System.IO
Imports System.Collections
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
Imports System.Drawing
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.DataSourcesOleDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports System.Xml
Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SIGCATMIN.form_ueas
Imports System.Linq

Public Class frm_PPM_PMA

    Public m_Application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private clsBarra As New cls_Barra
    Private lodtbDatos As New DataTable

    Private Sub frm_PPM_PMA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnProcesar.Enabled = True
        btnProcesar.Visible = True
        btnCerrar.Enabled = True
        btnCerrar.Visible = True

        btnExportPMA.Enabled = False
        btnExportPMA.Visible = False
        btnExportPPM.Enabled = False
        btnExportPPM.Visible = False
        btnExportPPM0.Enabled = False
        btnExportPPM0.Visible = False

        gboxPMA.Enabled = False
        gboxPMA.Visible = False
        gboxPPM.Enabled = False
        gboxPPM.Visible = False
        gboxPPM_0.Enabled = False
        gboxPPM_0.Visible = False

        dgvPMA.Enabled = False
        dgvPMA.Visible = False
        dgvPPM.Enabled = False
        dgvPPM.Visible = False
        dgvPPM0.Enabled = False
        dgvPPM0.Visible = False

        lblPMA.Enabled = False
        lblPMA.Visible = False
        lblPPM.Enabled = False
        lblPPM.Visible = False
        lblPPM_PMA.Enabled = False
        lblPPM_PMA.Visible = False

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        'Dim RetVal
        'RetVal = Shell(glo_Path & "\estilos\Barra_Proceso.exe", 1)---- cambio por proceso de batchero

        'Dim vdmxtitular_tmp As String = cls_Oracle.P_INS_SG_D_DMXTITULAR_TMP()---- cambio por proceso de batchero

        'Dim vdmxtitular_repo As String = cls_Oracle.P_INS_UPD_SG_D_DMXTITULAR_REPO()---- cambio por proceso de batchero

        Dim vdmxtitular_M As DataTable = cls_Oracle.P_REPO_SG_D_DMXTITULAR("M")

        Dim vdmxtitular_A As DataTable = cls_Oracle.P_REPO_SG_D_DMXTITULAR("A")

        Dim vdmxtitular_0 As DataTable = cls_Oracle.P_REPO_SG_D_DMXTITULAR("0")

        'For i As Integer = 0 To vdmxtitular_A.Rows.Count - 1
        '    dgvPMA.Rows(i).DefaultCellStyle.BackColor = Color.Gray
        'Next

        dgvPPM.DataSource = vdmxtitular_M
        dgvPMA.DataSource = vdmxtitular_A
        dgvPPM0.DataSource = vdmxtitular_0

        alternarColorFilasDatagridview(dgvPMA)
        alternarColorFilasDatagridview(dgvPPM)
        alternarColorFilasDatagridview(dgvPPM0)

        'cls_eval.cierra_ejecutable()---- cambio por proceso de batchero

        btnExportPMA.Enabled = True
        btnExportPMA.Visible = True
        btnExportPPM.Enabled = True
        btnExportPPM.Visible = True
        btnExportPPM0.Enabled = True
        btnExportPPM0.Visible = True

        gboxPMA.Enabled = True
        gboxPMA.Visible = True
        gboxPPM.Enabled = True
        gboxPPM.Visible = True
        gboxPPM_0.Enabled = True
        gboxPPM_0.Visible = True

        dgvPMA.Enabled = True
        dgvPMA.Visible = True
        dgvPPM.Enabled = True
        dgvPPM.Visible = True
        dgvPPM0.Enabled = True
        dgvPPM0.Visible = True

        dgvPMA.ScrollBars = ScrollBars.Both
        dgvPPM.ScrollBars = ScrollBars.Both
        dgvPPM0.ScrollBars = ScrollBars.Both

        'dgvPMA.FirstDisplayedScrollingRowIndex = dgvPPM0.RowCount - 1
        'dgvPPM.FirstDisplayedScrollingRowIndex = dgvPPM0.RowCount - 1
        'dgvPPM0.FirstDisplayedScrollingRowIndex = dgvPPM0.RowCount - 1

        lblPMA.Enabled = True
        lblPMA.Visible = True
        lblPPM.Enabled = True
        lblPPM.Visible = True
        lblPPM_PMA.Enabled = True
        lblPPM_PMA.Visible = True

        ' Forzamos ordenando por apellido paterno en las tablas para que se muestren las barras de desplazamiento
        dgvPPM.Sort(dgvPPM.Columns("JURNAT"), System.ComponentModel.ListSortDirection.Descending)
        dgvPMA.Sort(dgvPMA.Columns("JURNAT"), System.ComponentModel.ListSortDirection.Descending)
        dgvPPM0.Sort(dgvPPM0.Columns("JURNAT"), System.ComponentModel.ListSortDirection.Descending)

    End Sub

    Sub alternarColorFilasDatagridview(ByVal dgv As DataGridView)
        Try
            With dgv
                .RowsDefaultCellStyle.BackColor = Color.LightCyan
                .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExportPMA_Click(sender As Object, e As EventArgs) Handles btnExportPMA.Click
        ExportarDatosExcel(dgvPMA, "REPORTE DE TITULARES - CONYUGE CON CALIFICACIÓN PMA QUE SUPERAN LAS 1000 HA.")
    End Sub

    Private Sub btnExportPPM_Click(sender As Object, e As EventArgs) Handles btnExportPPM.Click
        ExportarDatosExcel(dgvPPM, "REPORTE DE TITULARES - CONYUGE CON CALIFICACIÓN PPM QUE SUPERAN LAS 2000 HA.")
    End Sub

    Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
        Dim m_Excel As New Excel.Application
        m_Excel.Cursor = Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)
        With objHojaExcel
            .Visible = Excel.XlSheetVisibility.xlSheetVisible
            .Activate()
            'Encabezado.
            .Range("A1:L1").Merge()
            .Range("A1:L1").Value = "INGEMMET"
            .Range("A1:L1").Font.Bold = True
            .Range("A1:L1").Font.Size = 16
            'Texto despues del encabezado.
            .Range("A2:L2").Merge()
            .Range("A2:L2").Value = titulo
            .Range("A2:L2").Font.Bold = True
            .Range("A2:L2").Font.Size = 10
            'Nombres
            'Estilo a titulos de la tabla.
            .Range("A6:P6").Font.Bold = True
            'Establecer tipo de letra al rango determinado.
            .Range("A1:P37").Font.Name = "Tahoma"
            'Los datos se registran a partir de la columna A, fila 4.
            Const primeraLetra As Char = "A"
            Const primerNumero As Short = 6
            Dim Letra As Char, UltimaLetra As Char
            Dim Numero As Integer, UltimoNumero As Integer
            Dim cod_letra As Byte = Asc(primeraLetra) - 1
            Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
            Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
            Dim strColumna As String = ""
            Dim LetraIzq As String = ""
            Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
            Letra = primeraLetra
            Numero = primerNumero
            Dim objCelda As Excel.Range
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    : If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        cod_LetraIzq += 1
                        LetraIzq = Chr(cod_LetraIzq)
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                    strColumna = LetraIzq + Letra + Numero.ToString
                    objCelda = .Range(strColumna, Type.Missing)
                    objCelda.Value = c.HeaderText
                    objCelda.EntireColumn.Font.Size = 10
                    'Establece un formato a los numeros por Default.
                    'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                    If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                    End If
                End If
            Next
            Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq
            'Cargar Datos del DataGridView.
            Dim i As Integer = Numero + 1
            For Each reg As DataGridViewRow In DataGridView1.Rows
                LetraIzq = ""
                cod_LetraIzq = Asc(primeraLetra) - 1
                Letra = primeraLetra
                cod_letra = Asc(primeraLetra) - 1
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra
                        'Aqui se realiza la carga de datos.
                        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", "'" & reg.Cells(c.Index).Value)
                        'Establece las propiedades de los datos del DataGridView por Default.
                        '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                        '.Range(strColumna + i, strColumna + i).In()
                    End If
                Next
                Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                objRangoReg.Rows.BorderAround()
                objRangoReg.Select()
                i += 1
            Next
            UltimoNumero = i
            'Dibujar las líneas de las columnas.
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                    objCelda.BorderAround()
                    If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        LetraIzq = Chr(cod_LetraIzq)
                        cod_LetraIzq += 1
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                End If
            Next
            'Dibujar el border exterior grueso de la tabla.
            Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
        End With
        m_Excel.Cursor = Excel.XlMousePointer.xlDefault
    End Sub

    Private Sub btnExportPPM0_Click(sender As Object, e As EventArgs) Handles btnExportPPM0.Click
        ExportarDatosExcel(dgvPPM0, "REPORTE DE TITULARES CON CALIFICACIÓN PMA/PPM CON 0 HA.")
    End Sub
End Class