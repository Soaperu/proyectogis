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

Public Class frm_AreaDisponible
    Public m_Application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private clsBarra As New cls_Barra
    Private lodtbDatos As New DataTable

    Private Sub frm_AreaDisponible_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    

        btnProcesar.Enabled = True
        btnProcesar.Visible = True
        btnExpAreaDispDM.Enabled = False
        btnExpAreaDispDM.Visible = False
        btnExpFC.Enabled = False
        btnExpFC.Visible = False
        btnCerrar.Enabled = False
        btnCerrar.Visible = False

        'dgvDMPMA.Enabled = False
        'dgvDMPMA.Visible = False
        'dgvPMA.Enabled = False
        'dgvPMA.Visible = False


        Label1.Enabled = False
        Label1.Visible = False
        Label2.Enabled = False
        Label2.Visible = False

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        'cls_planos.CambiaADataView(m_Application)
        'cls_Catastro.Borra_Todo_Feature("", m_Application)
        'cls_Catastro.Limpiar_Texto_Pantalla(m_Application)

        Dim num_dm_areadisponible As String
        Dim dm_areadisponible As String
        Dim dm_prioritario As String
        Dim dm_areadis_pri As String

        Dim RetVal7 = Shell(path_loader_proceso_general, 1)

        ' Indica la cantidad de registros en la tabla: data_cat.dm_areadisponible
        num_dm_areadisponible = cls_Oracle.FT_NUM_DM_AREADISPONIBLE()
        'Dim vbase As String = "1"
        If num_dm_areadisponible = "0" Then

            ' Inserta valores en la   Tabla  data_cat.dm_areadisponible 
            ' Almacena todos los DM X CALCULAR en PSAD56
            dm_areadisponible = cls_Oracle.FT_INS_DM_AREADISPONIBLE()

            '''''''''''' se comenta '''''''''''
            '' RELACION DE DM X CALCULAR PSAD56

            'Dim dt_DMxCal56_17 As New DataTable
            'Dim dt_DMxCal56_18 As New DataTable
            'Dim dt_DMxCal56_19 As New DataTable

            '' El parametro de la función sera:  zona
            'dt_DMxCal56_17 = cls_Oracle.FT_SEL_DMXCALCULAR("17")
            'dt_DMxCal56_18 = cls_Oracle.FT_SEL_DMXCALCULAR("18")
            'dt_DMxCal56_19 = cls_Oracle.FT_SEL_DMXCALCULAR("19")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''

            ' Inserta valores en la tabla  data_cat.dm_prioritario
            ' Almacena todos los DM PRIORITARIOS en PSAD56

            dm_prioritario = cls_Oracle.FT_INS_DM_PRIORITARIO()  ' se comenta ---> la Tabla debe ser llenada a partir de los FC generados con la vista ---> se llena la tabla con el query 10032023

            '''''''''''' se comenta '''''''''''
            '' RELACION DE DM PRIORITARIOS PSAD56

            'Dim dt_DMPrio56_17 As New DataTable
            'Dim dt_DMPrio56_18 As New DataTable
            'Dim dt_DMPrio56_19 As New DataTable

            '' El parametro de la función sera:  zona
            'dt_DMPrio56_17 = cls_Oracle.FT_SEL_DM_PRIORITARIO("17")
            'dt_DMPrio56_18 = cls_Oracle.FT_SEL_DM_PRIORITARIO("18")
            'dt_DMPrio56_19 = cls_Oracle.FT_SEL_DM_PRIORITARIO("19")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Se generan las vistas:
            '    data_cat.CM_CATASTRO_MINERO_C_17_V
            '    data_cat.CM_CATASTRO_MINERO_C_18_V
            '    data_cat.CM_CATASTRO_MINERO_C_19_V
            '    data_cat.CM_CATASTRO_MINERO_P_17_V
            '    data_cat.CM_CATASTRO_MINERO_P_18_V
            '    data_cat.CM_CATASTRO_MINERO_P_19_V

            '    data_cat.CM_CATASTRO_MINERO_C1_17_V
            '    data_cat.CM_CATASTRO_MINERO_C2_17_V
            '    data_cat.CM_CATASTRO_MINERO_C1_18_V
            '    data_cat.CM_CATASTRO_MINERO_C2_18_V
            '    data_cat.CM_CATASTRO_MINERO_C1_19_V
            '    data_cat.CM_CATASTRO_MINERO_C2_19_V
            '    data_cat.CM_CATASTRO_MINERO_P1_17_V
            '    data_cat.CM_CATASTRO_MINERO_P2_17_V
            '    data_cat.CM_CATASTRO_MINERO_P1_18_V
            '    data_cat.CM_CATASTRO_MINERO_P2_18_V
            '    data_cat.CM_CATASTRO_MINERO_P1_19_V
            '    data_cat.CM_CATASTRO_MINERO_P2_19_V
            ' Que identifican los DM x Calcular y los DM Prioritarios (Base)

            ' Se ejecuta el batchero, que ingresan los DM identificados en las vistas generadas
            ' En los features class C y P (17, 18, 19)
            Dim RetVal1
            While RetVal1 Is Nothing
                glo_Path_EXE = "U:\Sigcatmin\FCPadron\batchfiles\"
                RetVal1 = Shell(glo_Path_EXE & "padron_base.bat", 1, True)
            End While

            '''' este procedimiento se implemento en python ''''
            'Se asigna status = 4 a las capas DM X CALCULAR y DM PRIORITARIOS (segùn zona)
            'Dim UPD_C_P As String = cls_Oracle.P_UPD_DM_CAL_PRIO()

            'Tabla llenada con los FC generados con la vistas de Prioritarios
            'dm_prioritario = cls_Oracle.FT_INS_DM_PRIORITARIO()    ' se comenta 10032023

            ' Proceso de intersecciòn de las capas:  DM X CALCULAR vs DM PRIORITARIOS (segùn zona)
            Dim dm_intersec56_17, dm_intersec56_18, dm_intersec56_19 As String

            ' Se almacena en la tabla   data_cat.dm_areaintersectada 
            ' Y en los feature class DATA_CAT.GPO_CM_CATASTRO_MINERO_I1_17, I2_17... (17,18,19)
            ' la intersección de los DM x calcular vs DM Prioritarios
            ' En la tabla: data_cat.dm_areaintersectada y en los FC se graba el status = '1' de 
            ' los DMs intersectados

            ' Los parametros de la función seran:  datum, zona
            dm_intersec56_17 = cls_Oracle.FT_INS_DM_AREAINTERSEC("01", "17")
            dm_intersec56_18 = cls_Oracle.FT_INS_DM_AREAINTERSEC("01", "18")
            dm_intersec56_19 = cls_Oracle.FT_INS_DM_AREAINTERSEC("01", "19")

            Dim dm_dissolve56_17, dm_dissolve56_18, dm_dissolve56_19 As String

            ' Dissolve de los feature class  DATA_CAT.GPO_CM_CATASTRO_MINERO_I1_17, I2_17   (17,18,19)
            ' Se almacena en los feature class   data_cat.DATA_CAT.GPO_CM_CATASTRO_MINERO_D1_17, D2_17   (17,18,19)
            ' No considera status, el FC almacena el area.

            ' Los parametros de la función seran:  datum, zona
            dm_dissolve56_17 = cls_Oracle.FT_INS_FC_AREADISSOLVE("01", "17")
            dm_dissolve56_18 = cls_Oracle.FT_INS_FC_AREADISSOLVE("01", "18")
            dm_dissolve56_19 = cls_Oracle.FT_INS_FC_AREADISSOLVE("01", "19")


            ' Se comenta 1/11/2021
            'cls_eval.cierra_ejecutable()



            Dim upd_dm_int, upd_dm_lib As String

            ' Actualiza el campo AREA_DISP  de la tabla:   data_cat.dm_areadisponible
            ' Se guarda el status = '1'
            upd_dm_int = cls_Oracle.FT_UPD_DM_AREADISPONIBLE_INT()   '---> se modifico 09/12/2021

            ' Actualiza el campo AREA_DISP  de la tabla:   data_cat.dm_areadisponible
            ' Se guarda el status = '0'
            'upd_dm_lib = cls_Oracle.FT_UPD_DM_AREADISPONIBLE_LIB()      ---> se comenta 09/12/2021

        End If

        ' Primero blanquea tabla y luego inserta valores en la tabla  data_cat.dm_areadisponiblexdia
        ' Almacena todos los DM X CALCULAR DEL DIA en PSAD56
        dm_areadisponible = cls_Oracle.FT_INS_DM_AREADISPONIBLEXDIA()

        ' Se actualiza la tabla   data_cat.dm_areadisponible  con los DM x Calcular del dia
        ' asignando al campo STATUS = '2'
        dm_areadisponible = cls_Oracle.FT_INS_UPD_DM_AREADISPONIBLE()

        ' Primero blanquea tabla y luego inserta valores en la tabla  data_cat.dm_prioritarioxdia
        ' Almacena todos los PRIORITARIOS DEL DIA en PSAD56
        dm_prioritario = cls_Oracle.P_INS_DM_PRIORITARIOXDIA()

        ' Se actualiza la tabla   data_cat.dm_prioritario  con los Prioritarios del dia
        ' asignando al campo STATUS = '2'
        dm_prioritario = cls_Oracle.P_INS_UPD_DM_PRIORITARIO()

        

        If dm_areadisponible = "1" Or dm_prioritario = "1" Then
            ' Se generan las vistas:
            '       data_cat.CM_CATASTRO_MINERO_C_17_VV
            '       data_cat.CM_CATASTRO_MINERO_C_18_VV
            '       data_cat.CM_CATASTRO_MINERO_C_19_VV
            '       data_cat.CM_CATASTRO_MINERO_P_17_VV
            '       data_cat.CM_CATASTRO_MINERO_P_18_VV
            '       data_cat.CM_CATASTRO_MINERO_P_19_VV

            '    data_cat.CM_CATASTRO_MINERO_C1_17_VV
            '    data_cat.CM_CATASTRO_MINERO_C2_17_VV
            '    data_cat.CM_CATASTRO_MINERO_C1_18_VV
            '    data_cat.CM_CATASTRO_MINERO_C2_18_VV
            '    data_cat.CM_CATASTRO_MINERO_C1_19_VV
            '    data_cat.CM_CATASTRO_MINERO_C2_19_VV
            '    data_cat.CM_CATASTRO_MINERO_P1_17_VV
            '    data_cat.CM_CATASTRO_MINERO_P2_17_VV
            '    data_cat.CM_CATASTRO_MINERO_P1_18_VV
            '    data_cat.CM_CATASTRO_MINERO_P2_18_VV
            '    data_cat.CM_CATASTRO_MINERO_P1_19_VV
            '    data_cat.CM_CATASTRO_MINERO_P2_19_VV
            ' Que identifican los nuevos DM x Calcular con respecto a la Base
            ' Y los nuevos DM Prioritarios con respecto a la Base

            ' Con las vistas generadas, ejecutamos el batchero x dia
            Dim RetVal2
            While RetVal2 Is Nothing
                glo_Path_EXE = "U:\Sigcatmin\FCPadron\batchfiles\"
                RetVal2 = Shell(glo_Path_EXE & "padron_dia.bat", 1, True)
            End While

            'Se asigna status = 3 a los DMs ingresados en las capas  C y P 
            Dim upd_areadisponible As String = cls_Oracle.P_UPD_AREADISPONIBLE()


            ' Se actualiza la tabla   data_cat.dm_areadisponible  con los DM x Calcular  afectados por  
            ' los DM Prioritarios modificados o ingresados, asignando al campo status = '5'
            dm_areadis_pri = cls_Oracle.P_INS_UPD_DM_AREADIS_PRI()


            Dim dm_intersecxdia56_17, dm_intersecxdia56_18, dm_intersecxdia56_19 As String

            ' Se almacena en la tabla   data_cat.dm_areaintersectada 
            ' Y en los feature class DATA_CAT.GPO_CM_CATASTRO_MINERO_I_17 (18,19)
            ' la intersección de los DM x calcular vs DM Prioritarios

            ' Los parametros de la función seran:  datum, zona
            dm_intersecxdia56_17 = cls_Oracle.FT_INS_DM_AREAINTERSECXDIA("01", "17")
            dm_intersecxdia56_18 = cls_Oracle.FT_INS_DM_AREAINTERSECXDIA("01", "18")
            dm_intersecxdia56_19 = cls_Oracle.FT_INS_DM_AREAINTERSECXDIA("01", "19")

            '' PASO 3: EJECUTAMOS LOS DISSOLVE

            Dim dm_dissolvexdia56_17, dm_dissolvexdia56_18, dm_dissolvexdia56_19 As String

            ' Dissolve de los feature class  DATA_CAT.GPO_CM_CATASTRO_MINERO_I_17   (18,19)
            ' Se almacena en los feature class   data_cat.DATA_CAT.GPO_CM_CATASTRO_MINERO_D_17   (18,19)

            ' Los parametros de la función seran:  datum, zona
            dm_dissolvexdia56_17 = cls_Oracle.FT_INS_FC_AREADISSOLVEXDIA("01", "17")
            dm_dissolvexdia56_18 = cls_Oracle.FT_INS_FC_AREADISSOLVEXDIA("01", "18")
            dm_dissolvexdia56_19 = cls_Oracle.FT_INS_FC_AREADISSOLVEXDIA("01", "19")


            Dim upd_dmxcal_int, upd_dmxcal_lib As String
            ' Actualiza el campo AREA_DISP  de la tabla:   data_cat.dm_areadisponible    
            upd_dmxcal_int = cls_Oracle.FT_UPD_DMAREADISPONIBXDIA_INT()



            '''' esta para revisar este procedimiento '''''' Se comenta procedimiento 10/11/2021
            'upd_dmxcal_lib = cls_Oracle.FT_UPD_DMAREADISPONIBXDIA_LIB()

            dm_areadisponible = "0"
            dm_prioritario = "0"

        End If  'termino proceso del padrón

        dgdResultado.Enabled = True
        dgdResultado.Visible = True
        dgdResultado1.Enabled = True
        dgdResultado1.Visible = True

        'Procedimiento que calcula los campos: th_codhec, he_canhec y caso_dm = 'AD'
        'de todos los DMs de la tabla: data_cat.dm_areadisponible
        Dim HR As String = "0"
        '''''''''''' Se comenta
        HR = cls_Oracle.P_DM_AREADISP_HR()
        ''''''''''''

        'Procedimiento que actualiza los campos: th_codhec, he_canhec y caso_dm = 'AD'
        'de los DMs nuevos o modificados de la tabla: data_cat.dm_areadisponible
        HR = cls_Oracle.P_UPD_DM_AREADISPONIBLE_HR()

        'Lista todos los DMs de la tabla: data_cat.dm_areadisponible         
        Dim reporte_dm_areadisponible As New DataTable
        reporte_dm_areadisponible = cls_Oracle.FT_REPO_DM_AREADISP()

        dgdResultado.DataSource = reporte_dm_areadisponible

        'Inserta en la tabla data_cat.dm_areadisponible_cata  todos los DMs 
        'del Catastro Minero que no esten en la tabla  data_cat.dm_areadisponible
        ''''''''''''' Se comenta
        HR = cls_Oracle.P_INS_DM_AREADISPONIBLE_CATA()
        '''''''''''''

        'Procedimiento que actualiza los campos: th_codhec, he_canhec y caso_dm = 'FC'
        'de los DMs nuevos o modificados de la tabla: data_cat.dm_areadisponible_cata
        HR = cls_Oracle.P_UPD_DM_AREADISP_CATA_HR()

        'Lista todos los DMs de la tabla: data_cat.dm_areadisponible         
        Dim reporte_dm_areadisponible_cata As New DataTable
        reporte_dm_areadisponible_cata = cls_Oracle.P_REPO_DM_AREADISP_CATA()

        dgdResultado1.DataSource = reporte_dm_areadisponible_cata

        cls_eval.KillProcess(loader_proceso_general)

        btnExpAreaDispDM.Enabled = True
        btnExpAreaDispDM.Visible = True
        btnExpFC.Enabled = True
        btnExpFC.Visible = True
        btnCerrar.Enabled = True
        btnCerrar.Visible = True

        Label1.Enabled = True
        Label1.Visible = True
        Label2.Enabled = True
        Label2.Visible = True

    End Sub

    Private Sub btnExpAreaDispDM_Click(sender As Object, e As EventArgs) Handles btnExpAreaDispDM.Click
        ExportarDatosExcel(dgdResultado, "REPORTE ÁREAS DISPONIBLES DE DERECHOS MINEROS POR CALCULAR")
    End Sub

    Private Sub btnExpFC_Click(sender As Object, e As EventArgs) Handles btnExpFC.Click
        ExportarDatosExcel(dgdResultado, "REPORTE TIPO DE HECTAREA DE DERECHOS MINEROS DEL CATASTRO")
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

End Class