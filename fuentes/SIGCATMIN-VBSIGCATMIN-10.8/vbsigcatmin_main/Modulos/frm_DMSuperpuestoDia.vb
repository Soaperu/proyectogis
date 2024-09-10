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

Public Class frm_DMSuperpuestoDia
    Public m_Application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private clsBarra As New cls_Barra
    Private lodtbDatos As New DataTable

    Private Sub frm_DMSuperpuestoDia_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnProcesar.Enabled = False
        btnProcesar.Visible = True
        btnExporDM.Enabled = False
        btnExporDM.Visible = False
        btnCerrar.Enabled = False
        btnCerrar.Visible = False

        dtpfecha.Enabled = False
        dtpfecha.Visible = False
        dtpfecha_inicio.Enabled = False
        dtpfecha_inicio.Visible = False
        dtpfecha_fin.Enabled = False
        dtpfecha_fin.Visible = False

        rbfecha.Enabled = True
        rbfecha.Visible = True
        rbfechas.Enabled = True
        rbfechas.Visible = True

        lblfecha.Enabled = False
        lblfecha.Visible = False
        lblfecha_inicio.Enabled = False
        lblfecha_inicio.Visible = False
        lblfecha_fin.Enabled = False
        lblfecha_fin.Visible = False
        lblaviso.Enabled = False
        lblaviso.Visible = False
        lblmensaje.Enabled = False
        lblmensaje.Visible = False
        lblmensaje1.Enabled = False
        lblmensaje1.Visible = False

        dgdResultado1.Enabled = False
        dgdResultado1.Visible = False
        dgdResultado.Enabled = False
        dgdResultado.Visible = False
        btnExporDM.Enabled = False
        btnExporDM.Visible = False
        lblreporte.Enabled = False
        lblreporte.Visible = False
        btnExporDMdet.Enabled = False
        btnExporDMdet.Visible = False
        lblreportedet.Enabled = False
        lblreportedet.Visible = False

    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        dgdResultado1.Enabled = True
        dgdResultado1.Visible = True
        dgdResultado.Enabled = True
        dgdResultado.Visible = True
        btnExporDM.Enabled = True
        btnExporDM.Visible = True
        btnExporDMdet.Enabled = True
        btnExporDMdet.Visible = True
        lblreporte.Enabled = True
        lblreporte.Visible = True

        lblreportedet.Enabled = True
        lblreportedet.Visible = True


        btnCerrar.Enabled = True
        btnCerrar.Visible = True

        Dim v_dmsuperpuesto As New DataTable
        Dim v_dmsuperpuestodet As New DataTable
        ' La variable pública  v_estado , ya creada, es usada como elección del RadioButton y 
        ' como parametro de consulta del package

        fecrepo = Microsoft.VisualBasic.Left(Format(dtpfecha.Value), 10)
        fecrepo_inicio = Microsoft.VisualBasic.Left(Format(dtpfecha_inicio.Value), 10)
        fecrepo_fin = Microsoft.VisualBasic.Left(Format(dtpfecha_fin.Value), 10)

        If v_estado = "1" Then
            v_dmsuperpuesto = cls_Oracle.P_SEL_DMSUPERPUESTOXDIA(fecrepo, "", "", v_estado)
            lblreporte.Text = "Reporte de derechos mineros superpuestos (Si/No),  " & v_dmsuperpuesto.Rows.Count & " Reg."

            v_dmsuperpuestodet = cls_Oracle.P_SEL_DMSUPERPUESTOXDIA_DET(fecrepo, "", "", v_estado)
            lblreportedet.Text = "Detalle Reporte de derechos mineros superpuestos,  " & v_dmsuperpuestodet.Rows.Count & " Reg."
        Else
            v_dmsuperpuesto = cls_Oracle.P_SEL_DMSUPERPUESTOXDIA("", fecrepo_inicio, fecrepo_fin, v_estado)
            lblreporte.Text = "Reporte de derechos mineros superpuestos (Si/No),  " & v_dmsuperpuesto.Rows.Count & " Reg."

            v_dmsuperpuestodet = cls_Oracle.P_SEL_DMSUPERPUESTOXDIA_DET("", fecrepo_inicio, fecrepo_fin, v_estado)
            lblreportedet.Text = "Detalle Reporte de derechos mineros superpuestos,  " & v_dmsuperpuestodet.Rows.Count & " Reg."
        End If

        dgdResultado1.DataSource = v_dmsuperpuesto
        dgdResultado.DataSource = v_dmsuperpuestodet

    End Sub

    Private Sub rbfecha_CheckedChanged(sender As Object, e As EventArgs) Handles rbfecha.CheckedChanged
        If rbfecha.Checked = True Then

            v_estado = "1"

            btnProcesar.Enabled = True
            lblfecha.Enabled = True
            lblfecha.Visible = True
            dtpfecha.Enabled = True
            dtpfecha.Visible = True
            lblaviso.Enabled = True
            lblaviso.Visible = True
            lblmensaje.Enabled = True
            lblmensaje.Visible = True
            lblmensaje1.Enabled = False
            lblmensaje1.Visible = False

            lblfecha_inicio.Enabled = False
            lblfecha_inicio.Visible = False
            dtpfecha_inicio.Enabled = False
            dtpfecha_inicio.Visible = False
            lblfecha_fin.Enabled = False
            lblfecha_fin.Visible = False
            dtpfecha_fin.Enabled = False
            dtpfecha_fin.Visible = False

        End If

    End Sub

    Private Sub rbfechas_CheckedChanged(sender As Object, e As EventArgs) Handles rbfechas.CheckedChanged

        If rbfechas.Checked = True Then

            v_estado = "2"

            btnProcesar.Enabled = True
            lblfecha_inicio.Enabled = True
            lblfecha_inicio.Visible = True
            dtpfecha_inicio.Enabled = True
            dtpfecha_inicio.Visible = True
            lblfecha_fin.Enabled = True
            lblfecha_fin.Visible = True
            dtpfecha_fin.Enabled = True
            dtpfecha_fin.Visible = True
            lblaviso.Enabled = True
            lblaviso.Visible = True
            lblmensaje1.Enabled = True
            lblmensaje1.Visible = True
            lblmensaje.Enabled = False
            lblmensaje.Visible = False

            lblfecha.Enabled = False
            lblfecha.Visible = False
            dtpfecha.Enabled = False
            dtpfecha.Visible = False

        End If

    End Sub

    Private Sub btnExporDM_Click(sender As Object, e As EventArgs) Handles btnExporDM.Click
        ExportarDatosExcel(dgdResultado1, "REPORTE DE DERECHOS MINEROS SUPERPUESTOS POR DIA")
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

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click

        v_estado = ""

        Me.Close()

    End Sub

    Private Sub btnExporDMdet_Click(sender As Object, e As EventArgs) Handles btnExporDMdet.Click
        ExportarDatosExcel(dgdResultado, "DETALLE - REPORTE DE DERECHOS MINEROS SUPERPUESTOS POR DIA")
    End Sub
End Class