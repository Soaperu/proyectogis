Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports PORTAL_Clases
Imports System.Drawing
Imports ESRI.ArcGIS.esriSystem
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.IO

Imports System.Windows.Forms
Imports ESRI.ArcGIS.DataSourcesFile


Structure Punto_DM2
    Dim v As Integer
    Dim x As Double
    Dim y As Double
End Structure
Public Class Frm_EstadisticasMineria

    Private dt As New DataTable
    Public pApp As IApplication
    Public pEste As Double
    Public pNorte As Double
    Private lodtbUbigeo As New DataTable
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_Prueba As New cls_Prueba
    Private cls_DM_2 As New cls_DM_2
    Public m_application As IApplication
    Private Const Col_Sel_R As Integer = 0
    Private Const Col_Codigo As Integer = 1
    Private Const Col_Nombre As Integer = 2
    Private Const Col_tprese As Integer = 3
    Private Const Col_nm_tprese As Integer = 4

    'Se aumento debido a 2 columnas mas
    Private Const Col_Area As Integer = 5
    Private Const Col_Cantidad As Integer = 6
    Private Const Col_Areasup As Integer = 7
    Private Const Col_Por_Sup As Integer = 8

    Private lodtbReporte_Excel As New DataTable

    Private Property tipo_selec_catnomin As String



    'PROCESOS
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, ByVal lpExitCode As Long) As Long
    ' Private dt As New DataTable
    'PAUSA
    Private Declare Function GetTickCount Lib "Kernel32.dll" () As Long
    Const STILL_ACTIVE = &H103
    Const PROCESS_QUERY_INFORMATION = &H400

    ''FUNCION DE SLEEP PARA ESPERAR UNOS MILISEGUNDOS
    Sub Pausa(ByVal HowLong As Long)
        Dim u, tick As Long
        tick = GetTickCount()
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < GetTickCount
    End Sub
    Function ExeEspera(ByVal COMANDO As String)
        Dim hProcess As Long
        Dim RetVal As Long
        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(COMANDO, vbMinimizedNoFocus))
        Do
            GetExitCodeProcess(hProcess, RetVal)
            Application.DoEvents()
            Pausa(100)
        Loop While RetVal = STILL_ACTIVE
    End Function
    Private Sub Frm_EstadisticasMineria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btncalcular.Enabled = False

        Me.cbotipo.SelectedIndex = 1
        ' Me.cbodatum.SelectedIndex = 0
        ' Me.cboZona.Items.Clear()
        ' Me.cboZona.Enabled = False

        Me.btnExcel.Enabled = True
    End Sub
    Private Sub PT_Estilo_Grilla_EVAL(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_tprese).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_nm_tprese).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Area).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Cantidad).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Areasup).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_Por_Sup).DefaultValue = 0

    End Sub
    Private Sub PT_Cargar_Grilla_EVAL(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_EVAL()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_tprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_nm_tprese).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Area).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Cantidad).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Areasup).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Por_Sup).DefaultValue = ""

    End Sub
    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged

    End Sub

    Private Sub PT_Forma_Grilla_EVAL()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 25
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Width = 100
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Width = 80
        Me.dgdDetalle.Columns("SELEC").Caption = "SELEC"
        Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "NOMBRE"
        Me.dgdDetalle.Columns("TP_RESE").Caption = "TP_RESE"
        Me.dgdDetalle.Columns("NM_TPRESE").Caption = "NM_TPRESE"
        Me.dgdDetalle.Columns("AREA").Caption = "AREAINI"
        Me.dgdDetalle.Columns("CANTI").Caption = "CANTI"
        ' Me.dgdDetalle.Columns("AREA_SUP").Caption = "AREA_SUP"
        Me.dgdDetalle.Columns("AREA_NETA").Caption = "AREA_NETA"
        Me.dgdDetalle.Columns("PORCEN").Caption = "PORCEN"

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Locked = True

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_tprese).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nm_tprese).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Area).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Cantidad).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Areasup).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Por_Sup).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center


    End Sub
    Public Sub ExportarDatosExcel(ByVal p_ListBox As Object, ByVal titulo As String)

        'Generando datos en txt
        Const fic As String = "C:\reporte.txt"
        Dim sw As New System.IO.StreamWriter(fic)
        'escrile los elementos al txt

        For i As Integer = 0 To p_ListBox.ListCount - 1
            p_ListBox.Selected(i) = True

            sw.WriteLine(p_ListBox.List(p_ListBox.ListIndex))

        Next
        'Close #1  'cierra txt
        sw.Close()



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
            '  Dim lodbtExiste_SupAR As New DataTable
            ' lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
            ' For t As Integer = 0 To lodbtExiste_SupAR.Columns
            'v_areasup_rese = lodbtExiste_SupAR.Rows(contador1).Item("AREASUPER")
            'v_codigo_depa = lodbtExiste_SupAR.Rows(contador1).Item("CODIGO")
            'v_areaini_depa = lodbtExiste_SupAR.Rows(contador1).Item("AREAINI")
            'v_cantidad = lodbtExiste_SupAR.Rows(contador1).Item("CANTIDAD")
            'dRow = lodtTabla.NewRow
            'dRow.Item("CODIGO") = v_codigo_depa
            'dRow.Item("NOMBRE") = v_nm_depa1
            'dRow.Item("AREA") = v_areaini_depa
            'dRow.Item("CANTIDAD") = v_cantidad
            'dRow.Item("AREA_SUP") = v_areasup_rese
            'dRow.Item("PORCEN") = (Format(Math.Round((v_areasup_rese / v_areaini_depa) * 100, 2), "###,###.00")).ToString

            'lodtTabla.Rows.Add(dRow)
            ' Next a


            ' For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

            'lostCGCODEVA = lostCGCODEVA & w + 1 & " | " & dgdDetalle.Item(w, "COD_RESE") & " || "
            'valida_caso = dgdDetalle.Item(w, "CASO").ToString

            'If valida_caso = "COMPATIBLE" Then

            'ElseIf valida_caso = "INCOMPATIBLE" Then

            'Else
            '    MsgBox("NO ESTA INDICADO SI ES COMPATIBLE O INCOMPATIBLE UNAS DE LAS AREAS DEL LISTADO, POR FAVOR VERIFICAR..", MsgBoxStyle.Critical, "GEOCATMIN...")
            '    Exit Sub
            'End If

            'For w As Integer = 0 To dgdDetalle.DataSource.
            '    C1.Win.C1TrueDBGrid.C1DataColumn = 5
            '    For c As Integer = 0 To 5


            'For Each c As DataGridViewColumn In DataGridView1.Columns
            For Each c As C1.Win.C1TrueDBGrid.C1DisplayColumn In dgdDetalle.Splits(0).DisplayColumns
                'Col_Area.style.frecolor = Color.bkacl

                'If c.Visible Then
                If Letra = "Z" Then
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
                'objCelda.Value = c.HeaderText
                objCelda.EntireColumn.Font.Size = 10
                'Establece un formato a los numeros por Default.
                'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                ' If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                'End If
                'End If

            Next
            Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq
            'Cargar Datos del DataGridView.
            Dim i As Integer = Numero + 1
            ' For reg As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            'For Each reg As DataGridViewRow In DataGridView1.Rows
            '   C1.Win.C1TrueDBGrid()
            ' For Each reg As C1.Win.C1TrueDBGrid In C1.Win.C1TrueDBGrid.C1TrueDBGrid.

            LetraIzq = ""
            cod_LetraIzq = Asc(primeraLetra) - 1
            Letra = primeraLetra
            cod_letra = Asc(primeraLetra) - 1
            'For Each c As DataGridViewColumn In DataGridView1.Columns
            For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
                ' If c.Visible Then
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
                '.Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                .Cells(i, strColumna) = dgdDetalle.Item(w1, "COD_RESE").ToString







                'Establece las propiedades de los datos del DataGridView por Default.
                '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                '.Range(strColumna + i, strColumna + i).In()
                ' End If
            Next
            Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
            objRangoReg.Rows.BorderAround()
            objRangoReg.Select()
            i += 1
            '  Next
            UltimoNumero = i
            'Dibujar las líneas de las columnas.
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            ' For Each c As DataGridViewColumn In DataGridView1.Columns
            For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1

                'If c.Visible Then
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
                ' End If
            Next
            'Dibujar el border exterior grueso de la tabla.
            Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
        End With
        m_Excel.Cursor = Excel.XlMousePointer.xlDefault

    End Sub
    Private Sub dgdDetalle_Click(sender As Object, e As EventArgs) Handles dgdDetalle.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub imgMenu_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub imgMenu_Click_1(sender As Object, e As EventArgs) Handles imgMenu.Click

    End Sub

    Private Sub btncalcular_Click(sender As Object, e As EventArgs) Handles btncalcular.Click

    End Sub

    Private Sub cbotipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotipo.SelectedIndexChanged

    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ' Try
        Application.DoEvents()
        Dim RetVal
        If tipo_selec_catnomin = "A NIVEL NACIONAL" Then
            RetVal = Shell(glo_Path_EXE & "procesareportetodo.bat", 1)
        ElseIf tipo_selec_catnomin = "SEGUN DEPARTAMENTO" Then

            RetVal = Shell(glo_Path_EXE & "procesareporte.bat", 1)
        End If

    End Sub
End Class