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


Public Class frm_PMA
    Public m_Application As IApplication
    Private cls_Oracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_DM_2 As New cls_DM_2
    Private cls_Prueba As New cls_Prueba
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Private clsBarra As New cls_Barra
    Private lodtbDatos As New DataTable

    Private Sub frm_PMA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboDatum.Enabled = True
        cboDatum.Visible = True

        btnProcesar.Enabled = True
        btnProcesar.Visible = True
        btnExporDMPMA.Enabled = False
        btnExporDMPMA.Visible = False
        btnExporPMA.Enabled = False
        btnExporPMA.Visible = False
        btnCerrar.Enabled = False
        btnCerrar.Visible = False

        'dgvDMPMA.Enabled = False
        'dgvDMPMA.Visible = False
        'dgvPMA.Enabled = False
        'dgvPMA.Visible = False

        txtDatum.Enabled = True
        txtDatum.Visible = True
        Label1.Enabled = False
        Label1.Visible = False
        Label2.Enabled = False
        Label2.Visible = False

    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        v_sistema = Me.cboDatum.SelectedItem

        cls_Catastro.Borra_Todo_Feature("", m_Application)
        cls_Catastro.Limpiar_Texto_Pantalla(m_Application)
        pMxDoc.UpdateContents()

        fecha_archi = DateTime.Now.Ticks.ToString()
        v_zona_dm = "18"
        v_opcion_modulo = "OP_25"

        If v_sistema = "PSAD-56" Then
            gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_"
            gstrFC_Provincia = "GPO_PRO_PROVINCIA_"
        ElseIf v_sistema = "WGS-84" Then
            gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
            gstrFC_Provincia = "GPO_PRO_PROVINCIA_WGS_"
        End If

        pMxDoc = m_Application.Document
        pMap = pMxDoc.FocusMap
        caso_consulta = "CATASTRO MINERO"
        If pMap.Name <> "CATASTRO MINERO" Then
            cls_planos.buscaadataframe(caso_consulta, False)
            If valida = False Then
                pMap.Name = "CATASTRO MINERO"
                pMxDoc.UpdateContents()
            End If
            cls_eval.ActivaDataframe_Opcion(caso_consulta, m_Application)
            pMxDoc.UpdateContents()
        End If
        cls_eval.Eliminadataframe(caso_consulta)
        cls_planos.buscaadataframe("CATASTRO MINERO", False)
        If valida = False Then
            pMap.Name = "CATASTRO MINERO"
        End If
        cls_Catastro.Actualizar_DM(v_zona_dm)
        pMxDoc.UpdateContents()

        Dim RetVal
        RetVal = Shell(glo_Path & "\estilos\Barra_Proceso.exe", 1)

        v_petdmpma = cls_Oracle.P_SEL_DMXPMA(v_sistema)

        'dgvDMPMA.DataSource = v_petdmpma

        dgdResultado1.DataSource = v_petdmpma
        'btnexp_excel.Enabled = True    '---  Exportar a Excel  (boton)

        v_petpma = cls_Oracle.FT_petpma(v_sistema)

        'dgvPMA.DataSource = v_petpma
        dgdResultado.DataSource = v_petpma
        'btnexp_excel.Enabled = True    '---  Exportar a Excel  (boton)

        ''''''''''''''''''''''''''''''''''''''''''
        ' Crea .dbf de los DM con calificación PMA
        fecha_archi = DateTime.Now.Ticks.ToString()
        'fecha_archi = DateTime.Now.ToLocalTime.ToString()
        petdmpma = "DMcalPMA_" & fecha_archi
        cls_Catastro.creatabla_dbf(petdmpma)

        ' Crea .dbf de los DM con calificación PMA fuera de la Demarcación Calificada
        fecha_archi = DateTime.Now.Ticks.ToString()
        petpma = "PMAnoDEMA_" & fecha_archi
        cls_Catastro.creatabla_dbf(petpma)
        ''''''''''''''''''''''''''''''''''''''''''

        Dim codigo_eval As String = ""
        Dim codigo_prov As String = ""
        Dim codigo_provd As String = ""
        Dim codigo_titular As String = ""

        Select Case v_petpma.Rows.Count
            Case 0
                MsgBox("No Existe Petitorios PMA ubicados fuera de la Demarcación Calificada", MsgBoxStyle.Information, "Observación...")
                Exit Sub
            Case Else
                For contador As Integer = 0 To v_petpma.Rows.Count - 1
                    codigo_eval = v_petpma.Rows(contador).Item("CG_CODIGO")
                    codigo_prov = v_petpma.Rows(contador).Item("CODDEM_G")
                    codigo_provd = v_petpma.Rows(contador).Item("CODDEM")
                    codigo_titular = v_petpma.Rows(contador).Item("TITULAR")

                    If contador = 0 Then
                        lista_codigo_pma = "CODIGOU =  '" & codigo_eval & "'"
                        lista_codigo_provg = "CD_PROV =  '" & codigo_prov & "'"
                        lista_codigo_provd = "CD_PROV =  '" & codigo_provd & "'"
                        lista_codigo_titular = "TIT_CONCES =  '" & codigo_titular & "'"
                    ElseIf contador > 0 Then
                        lista_codigo_pma = lista_codigo_pma & " OR " & "CODIGOU =  '" & codigo_eval & "'"
                        lista_codigo_provg = lista_codigo_provg & " OR " & "CD_PROV =  '" & codigo_prov & "'"
                        lista_codigo_provd = lista_codigo_provd & " OR " & "CD_PROV =  '" & codigo_provd & "'"
                        lista_codigo_titular = lista_codigo_titular & " OR " & "TIT_CONCES =  '" & codigo_titular & "'"
                    End If
                Next contador
                lista_codigo_prov = lista_codigo_provd & " OR " & lista_codigo_provg

                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Provincia & v_zona_dm, m_Application, "1", False)
                cls_Catastro.Color_Poligono_Simple(m_Application, "Provincia")
                'cls_Catastro.ShowLabel_DM2("Provincia", m_Application)
                cls_eval.consultacapaDM("", "Provincia", "Provincia")
                cls_Catastro.Expor_Tema("provin_s", sele_denu, m_Application)
                cls_Catastro.ClearLayerSelection(pFeatureLayer)

                cls_Catastro.Add_ShapeFile_tmp("provin_s" & fecha_archi, m_Application)
                cls_eval.agregacampotema_tpm("provin_s" & fecha_archi, "provincia_PMA")
                cls_Catastro.Add_ShapeFile("provin_s" & fecha_archi, m_Application)
                cls_Catastro.Update_Value_Layer(m_Application, "provincia_PMA", "provincia_PMA")

                cls_Catastro.Layer_TableSort(m_Application, "provin_s" & fecha_archi, "provins_" & fecha_archi, "PROVIN")
                cls_Catastro.Add_ShapeFile_tmp("provins_" & fecha_archi, m_Application)
                cls_Catastro.Add_ShapeFile("provins_" & fecha_archi, m_Application)
                cls_Catastro.Update_Value_Layer(m_Application, "provincias_PMA", "provincias_PMA")
                cls_Catastro.Update_Value_Layer(m_Application, "provincias_PMA", "estilo")
                'cls_Catastro.DataTable_Layer(m_Application, "provincias_PMA", "prov_PMA", "ID")   'layer, tema, campo
                cls_Catastro.ShowLabel_DM2("provincias_PMA", m_Application)
                cls_Prueba.Poligono_Color_PMA("provincias_PMA", dt_PMA, glo_pathStyle & "\provincia_PMA.style", "LEYEN", "", m_Application)
                cls_Catastro.Zoom_to_Layer("provincias_PMA")

                cls_Catastro.Quitar_Layer("provincia_PMA", m_Application)
                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                cls_Catastro.Quitar_Layer("provins_" & fecha_archi, m_Application)
                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                lista_codigo_prov = ""

                'cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_Application, "1", True)
                'cls_Catastro.DefinitionExpression(lista_codigo_pma, m_Application, "Catastro")

                cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zona_dm, m_Application, "1", False)
                cls_eval.consultacapaDM("", "titular_catastro", "Catastro")
                cls_Catastro.Expor_Tema("tit_cata" & fecha_archi, sele_denu, m_Application)
                cls_Catastro.Layer_TableSort(m_Application, "tit_cata" & fecha_archi, "titu_cata" & fecha_archi, "TIT_CONCES")
                cls_Catastro.Add_ShapeFile_tmp("titu_cata" & fecha_archi, m_Application)
                cls_Catastro.Add_ShapeFile("titu_cata" & fecha_archi, m_Application)

                cls_Catastro.Quitar_Layer("titu_cata" & fecha_archi, m_Application)
                cls_Catastro.ClearLayerSelection(pFeatureLayer)

                cls_eval.consultacapaDM("", "catastro_PMA", "Catastro")
                cls_Catastro.Expor_Tema("cata_pma" & fecha_archi, sele_denu, m_Application)
                cls_Catastro.Add_ShapeFile_tmp("cata_pma" & fecha_archi, m_Application)
                cls_Catastro.Add_ShapeFile("cata_pma" & fecha_archi, m_Application)
                cls_Catastro.Quitar_Layer("Catastro", m_Application)

                cls_Catastro.Color_Poligono_Simple_2(m_Application, "titular_catastro")
                cls_Catastro.Color_Poligono_Simple_2(m_Application, "catastro_PMA")

                'dt_PMA.Clear()
                lista_codigo_pma = ""
                v_zona_dm = ""

                cls_planos.generaplano_PMA(m_Application, "CATASTRO MINERO", "Plano petitorios_PMA")

        End Select

        cls_eval.cierra_ejecutable()

        btnCerrar.Enabled = True
        btnCerrar.Visible = True
        btnExporDMPMA.Enabled = True
        btnExporDMPMA.Visible = True
        btnExporPMA.Enabled = True
        btnExporPMA.Visible = True

        'dgvDMPMA.Enabled = True
        'dgvDMPMA.Visible = True
        'dgvPMA.Enabled = True
        'dgvPMA.Visible = True

        Label1.Enabled = True
        Label1.Visible = True
        Label2.Enabled = True
        Label2.Visible = True




        '-----

        'Dim vdmxtitular_tmp As String = cls_Oracle.P_INS_SG_D_DMXTITULAR_TMP()

        'Dim vdmxtitular_repo As String = cls_Oracle.P_INS_UPD_SG_D_DMXTITULAR_REPO()

        'Dim vdmxtitular_M As DataTable = cls_Oracle.P_REPO_SG_D_DMXTITULAR("M")

        'Dim vdmxtitular_A As DataTable = cls_Oracle.P_REPO_SG_D_DMXTITULAR("A")


        'dgdResultado2.DataSource = vdmxtitular_M

        'dgdResultado3.DataSource = vdmxtitular_A
  


        'Exit Sub
    End Sub

    Private Sub btnExporDMPMA_Click(sender As Object, e As EventArgs) Handles btnExporDMPMA.Click
        ExportarDatosExcel(dgdResultado1, "REPORTE DE DERECHOS MINEROS CON CALIFICACIÓN PMA")
    End Sub

    Private Sub btnExporPMA_Click(sender As Object, e As EventArgs) Handles btnExporPMA.Click
        ExportarDatosExcel(dgdResultado, "REPORTE DE DERECHOS MINEROS CON CALIFICACIÓN PMA FUERA DE DEMARCACION CALIFICADA")
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

        Me.Close()

    End Sub

End Class