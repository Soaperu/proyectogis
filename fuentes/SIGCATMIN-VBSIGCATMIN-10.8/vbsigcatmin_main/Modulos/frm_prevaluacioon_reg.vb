
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
Imports ESRI.ArcGIS.DataSourcesFile


Public Class frm_prevaluacioon_reg
    Private dt As New DataTable
    Public pApp As IApplication
    
    Public m_application As IApplication

    Private Const Col_Sel_R As Integer = 0
    Private Const Col_conta As Integer = 1
    Private Const Col_Codigo As Integer = 2
    Private Const Col_nombre As Integer = 3
    Private Const Col_fecha As Integer = 4
    Private Const Col_zona As Integer = 5
    Private Const Col_grafica As Integer = 6
    Private Const Col_carta As Integer = 7
    Private Sub frm_prevaluacioon_reg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fecha As String
        Dim cadena As String
        Dim lodtTabla As New DataTable
        Dim dRow As DataRow
        Dim MyDate As Date

        Dim valor_codigo As String = ""
        Dim valor_nombre As String = ""
        Dim valor_zona As String = ""
        Dim valor_carta As String = ""


        Dim codigo As String
        lodtTabla.Clear()
        lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CONTA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("FECHA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("GRAFICA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CARTA", Type.GetType("System.String"))
        '    lodtTabla.Columns.Add("ACCION", Type.GetType("System.String"))


        Dim cls_datos As New cls_Oracle
        Dim lostrRetorno1 As New DataTable

        MyDate = Now

        Dim cuenta As Integer = 0
        Dim cuenta1 As Integer = 0
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & RellenarComodin(MyDate.Month, 2, "0") & RellenarComodin(MyDate.Year, 2, "0")
        fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        ' fecha = "25/05/2019"
        '  fecha = "28/05/2019"
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")

        Dim contador_lineas As Long


        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0
        Dim valor_txt As String = ""
        Dim val_grafica As String = ""
        fecha = Microsoft.VisualBasic.Left(fecha, 10).ToString
        Dim fecha1 As String
        Dim fecha2 As String
        fecha1 = Microsoft.VisualBasic.Left(fecha, 2).ToString
        fecha2 = Microsoft.VisualBasic.Right(fecha, 8).ToString
        If (fecha1 = "01") Then
            fecha1 = "31"
        End If
        fecha1 = fecha1 - 1
        Dim conta_lista As Integer

        ' fecha = fecha1 - 1
        Try

            ' lostrRetorno1 = cls_Conexion.FT_consultar_proceso_Anm("001", fecha)
            lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA(fecha)
            'lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA("13/05/2019")
            ' FT_consultar_INGRESO_DMXDIA()
        Catch ex As Exception

        End Try

        conta_lista = lostrRetorno1.Rows.Count
        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                valor_codigo = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                'valor_nombre = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                v_nombre_dm = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                valor_zona = lostrRetorno1.Rows(contador1).Item("PE_ZONCAT")
                valor_carta = lostrRetorno1.Rows(contador1).Item("CA_CODCAR")
                val_grafica = lostrRetorno1.Rows(contador1).Item("PE_VIGCAT")

                dRow.Item("CODIGO") = valor_codigo
                ' dRow.Item("NOMBRE") = valor_nombre
                dRow.Item("NOMBRE") = v_nombre_dm
                'dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
                dRow.Item("ZONA") = valor_zona
                dRow.Item("CARTA") = valor_carta
                dRow.Item("CONTA") = cuenta2
                dRow.Item("FECHA") = fecha1 & fecha2
                dRow.Item("GRAFICA") = val_grafica
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If


        Me.dgdDetalle.DataSource = lodtTabla
        PT_Estilo_Grilla_prevaluacion(lodtTabla) : PT_Cargar_Grilla_prevaluacion(lodtTabla)
        PT_Agregar_Funciones_prevaluacion() : PT_Forma_Grilla_Funciones_prevaluacion()

        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
            dgdDetalle.Item(i, "SELEC") = True
        Next
        Me.dgdDetalle.AllowUpdate = True
        lbl_lista.Text = conta_lista
        dRow = Nothing
        dgdDetalle.Focus()
        colecciones_txtfiles.Clear()
    End Sub


    Private Sub PT_Estilo_Grilla_prevaluacion(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_nombre).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_fecha).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_zona).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_grafica).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_carta).DefaultValue = ""

    End Sub
    Private Sub PT_Cargar_Grilla_prevaluacion(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_prevaluacion()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_nombre).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_fecha).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_zona).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_grafica).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_carta).DefaultValue = ""


    End Sub
    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged


        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("SELEC").ValueItems
        If Me.chkEstado.Checked Then
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked

        Else
            items.Translate = True
            items.CycleOnClick = True
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
        End If
    End Sub



    Private Sub PT_Forma_Grilla_Funciones_prevaluacion()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nombre).Width = 120
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Width = 50
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Width = 100

        Me.dgdDetalle.Columns("SELEC").Caption = "SEL."
        Me.dgdDetalle.Columns("CONTA").Caption = "NRO."
        Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("NOMBRE").Caption = "NOMBRE"
        Me.dgdDetalle.Columns("ZONA").Caption = "ZONA"
        Me.dgdDetalle.Columns("FECHA").Caption = "FECHA"
        Me.dgdDetalle.Columns("GRAFICA").Caption = "GRAFICA"
        Me.dgdDetalle.Columns("CARTA").Caption = "CARTA"

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nombre).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Locked = True


        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nombre).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_nombre).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grafica).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

    End Sub


    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        Dim pDatum As String = ""
        Dim vCodigo As String = ""
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        v_opcion_mas = "2"
        Dim cuenta As Integer

        cls_catastro.Borra_Todo_Feature("", m_application)
        cls_catastro.Limpiar_Texto_Pantalla(m_application)


        gstrFC_CDistrito = "GPO_CDI_CAPITAL_DISTRITO_18"
        

        gstrFC_Departamento_WGS = "GPO_DEP_DEPARTAMENTO_WGS_"
        gstrFC_Provincia_WGS = "GPO_PRO_PROVINCIA_WGS_"
        gstrFC_Distrito_WGS = "GPO_DIS_DISTRITO_WGS_"
        gstrFC_Rios = "GLI_RIO_RIOS_18"
        gstrFC_Carretera = "GLI_VIA_VIAS_18"
        gstrFC_CPoblado = "GPT_CPO_CENTRO_POBLADO_18"
        'gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Cuadricula = "GPO_CRE_CUADRICULA_REGIONAL_18"     ''''''''''''''''''''''''''''''''''''
        'gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        'gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_ZUrbana56 = "GPO_ZUR_ZONA_URBANA_G84"
        gstrFC_AReservada56 = "GPO_ARE_AREA_RESERVADA_G84"
        gstrFC_ZTraslape = "GPO_ZTR_ZONA_TRASLAPE_18"
        gstrFC_Frontera_Z = "GLI_FRO_FRONTERA_WGS_"
        gstrFC_Frontera_10 = "GLI_FRO_FRONTERA_10K_WGS18"
        gstrFC_Frontera_25 = "GLI_FRO_FRONTERA_25K_WGS18"
        gstrFC_LHojas = "GPO_HOJ_HOJAS_18"
        gstrFC_Carta = "GPO_HOJ_HOJAS_18"

        gstrFC_CARAM = "GPO_CAR_CARAM_WGS_"
        gstrFC_CARAM56 = "GPO_CAR_CARAM_"
        Dim cls_eval As New Cls_evaluacion


        Dim contador_lista As Integer = 0

        'Para solo contar los marcados por el usuario
        cls_catastro.creandotabla_Rep_Libredenu("Regional")



        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                contador_lista = contador_lista + 1
                v_vigcat = dgdDetalle.Item(w1, "GRAFICA").ToString
                If v_vigcat <> "G" Then
                    MsgBox("Debe Seleccionar solo los grafican, sino el sistema no procesará...", MsgBoxStyle.Critical, "SIGCATMIN")
                    Exit Sub

                End If
            End If

        Next w1



        'Se Aumento para crear una tabla para reporte de cantidad de PR,Areas Restringidas, etc con el DM evaluado
        '--------------------------------------------------------------------------------------------

        Dim pTable As ITable
        Dim pWorkspaceFactory1 As IWorkspaceFactory
        pWorkspaceFactory1 = New ShapefileWorkspaceFactory
        Dim pFWS As IFeatureWorkspace
        Dim carpeta As IWorkspace

        carpeta = pWorkspaceFactory1.OpenFromFile(glo_pathTEMP & "\regional\", 0)
        pFWS = carpeta
        pTable = pFWS.OpenTable("Reporte_Prioritarios" & fecha_archi_lib)
        Dim ptableCursor As ICursor
        Dim pfields3 As Fields
        Dim pRow As IRow


        'Termino contador de los registros marcados

        'Empieza a recorrar
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1


            If dgdDetalle.Item(w, "SELEC") = True Then
                v_masivo_eval = "1"
                lista_forestal = ""
                cuenta = cuenta + 1

                v_codigo = dgdDetalle.Item(w, "CODIGO").ToString.ToUpper
                '  v_codigo = "010039303"
                ' v_clase_rese = dgdDetalle.Item(w, "CLASE").ToString
                'v_zona_rese = dgdDetalle.Item(w, "ZONA").ToString


                v_fecha_dm = dgdDetalle.Item(w, "FECHA").ToString
                v_zona_dm = dgdDetalle.Item(w, "ZONA").ToString
                ' v_nombre_dm = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                v_nombre_dm = dgdDetalle.Item(w, "NOMBRE").ToString
                '  v_vigcat = "G"
                v_vigcat = dgdDetalle.Item(w, "GRAFICA").ToString
                v_carta_dm = dgdDetalle.Item(w, "CARTA").ToString
                pMxDoc = m_application.Document
                caso_consulta = "CATASTRO MINERO" & cuenta
                cls_eval.adicionadataframe(caso_consulta)
                ' cls_eval.activadataframe(caso_consulta)


                'Se almacena en la tabla el codigo y Nombre
                pRow = pTable.CreateRow
                pfields3 = pTable.Fields
                pRow.Value(pfields3.FindField("CG_CODIGO")) = v_codigo
                pRow.Value(pfields3.FindField("NOMBRE")) = v_nombre_dm.ToString
                pRow.Store()

                'Termino ingreso de datos





                m_application.Caption = "PROCESO  MASIVO DE PRE EVALUACION PARA GOB. REGIONALES :  " & v_codigo & "      " & cuenta.ToString & "  De  " & contador_lista
                cls_catastro.Consulta_Evaluacion_DM_masivo(m_application)
                ' Exit Sub
                '   cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion")
                '  Exit Sub

                ' Exit Sub



                Dim A As Integer = 2
                cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)

                Dim CLS_PRUEBA As cls_Prueba

                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
                cls_catastro.Genera_Imagen_DM(v_codigo, "regional")
                cls_catastro.Color_AR_Plano(m_application, "Zona Reservada")

                cls_catastro.Genera_Imagen_planopdf(v_codigo, "regional")
                
                cls_catastro.Borra_Todo_Feature("", m_application)
                cls_catastro.Limpiar_Texto_Pantalla(m_application)
                pMxDoc.UpdateContents()
                cls_eval.Eliminadataframe_masivo(caso_consulta)
                pMxDoc.UpdateContents()
                cls_planos.CambiaADataView(m_application)
            End If
        Next w

        MsgBox("EL PROCESO HA FINALIZADO SATISFACTORIAMENTE...", MsgBoxStyle.Exclamation, "SIGCATMIN")



    End Sub

 
    Private Sub btn_desmarca_Click(sender As Object, e As EventArgs)
        Try


            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                dgdDetalle.Item(i, "SELEC") = False

            Next
            Me.dgdDetalle.AllowUpdate = True


            dgdDetalle.Focus()
        Catch
        End Try
    End Sub

    Private Sub dgdDetalle_Click(sender As Object, e As EventArgs) Handles dgdDetalle.Click

    End Sub

    Private Sub btndeseleccion_Click(sender As Object, e As EventArgs) Handles btndeseleccion.Click
        Try

            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                dgdDetalle.Item(i, "SELEC") = False
            Next
            Me.dgdDetalle.AllowUpdate = True
            dgdDetalle.Focus()
        Catch
        End Try
    End Sub

    Private Sub btnseleccionar_Click(sender As Object, e As EventArgs) Handles btnseleccionar.Click
        Try

            For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                dgdDetalle.Item(i, "SELEC") = True
            Next
            Me.dgdDetalle.AllowUpdate = True
            dgdDetalle.Focus()
        Catch
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()


    End Sub

    Private Sub dtpFecha1_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha1.ValueChanged
        fecsup = Microsoft.VisualBasic.Left(Format(dtpFecha1.Value), 10)

        ' fecsup = dtpFecha1.Value
        ' Dim con1 As String
        'con1 = Len(fecsup)
        '' If con1 = "10" Then
        'If con1 = "19" Then
        ''  fecsup = "0" & fecsup
        'fecsup = Microsoft.VisualBasic.Left(Format(dtpFecha1.Value), 10)
        'ElseIf con1 = "18" Then
        'fecsup = "0" & Microsoft.VisualBasic.Left(Format(dtpFecha1.Value), 10)
        'End If

        Dim fecha As String
        Dim cadena As String
        Dim lodtTabla As New DataTable
        Dim dRow As DataRow
        Dim MyDate As Date

        Dim valor_codigo As String = ""
        Dim valor_nombre As String = ""
        Dim valor_zona As String = ""
        Dim valor_carta As String = ""


        Dim codigo As String
        lodtTabla.Clear()
        lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CONTA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("FECHA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("GRAFICA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CARTA", Type.GetType("System.String"))
        '    lodtTabla.Columns.Add("ACCION", Type.GetType("System.String"))


        Dim cls_datos As New cls_Oracle
        Dim lostrRetorno1 As New DataTable

        MyDate = Now

        Dim cuenta As Integer = 0
        Dim cuenta1 As Integer = 0

        '  fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        fecha = fecsup

        ' fecha = "25/05/2019"

        Dim contador_lineas As Long


        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0
        Dim valor_txt As String = ""
        Dim val_grafica As String = ""
        'fecha = Microsoft.VisualBasic.Left(fecha, 10).ToString
        'Dim fecha1 As String
        'Dim fecha2 As String
        'Dim fecha3 As String
        'Dim fecha4 As String
        'Dim fecha5 As String
        'fecha1 = Microsoft.VisualBasic.Left(fecha, 2).ToString
        'fecha2 = Microsoft.VisualBasic.Right(fecha, 8).ToString
        'fecha3 = Microsoft.VisualBasic.Left(fecha2, 3).ToString
        'fecha4 = Microsoft.VisualBasic.Right(fecha3, 2).ToString
        'fecha5 = Microsoft.VisualBasic.Right(fecha, 4).ToString
        'If (fecha1 = "01") Then
        '    fecha1 = "31"
        '    fecha4 = fecha4 - 1
        '    If Len(fecha4 = 1) Then
        '        fecha4 = "0" & fecha4
        '    End If
        '    fecha = fecha1 & "/" & fecha4 & "/" & fecha5
        'Else
        '    fecha = fecha1 & fecha2
        'End If

        Dim conta_lista As Integer

        Try


            lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA_UNICO(fecha)

            'lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA("13/05/2019")

        Catch ex As Exception

        End Try
        '    fecha1 = fecha1 - 1

        conta_lista = lostrRetorno1.Rows.Count
        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                valor_codigo = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                'valor_nombre = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                v_nombre_dm = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                valor_zona = lostrRetorno1.Rows(contador1).Item("PE_ZONCAT")
                valor_carta = lostrRetorno1.Rows(contador1).Item("CA_CODCAR")
                val_grafica = lostrRetorno1.Rows(contador1).Item("PE_VIGCAT")

                dRow.Item("CODIGO") = valor_codigo
                ' dRow.Item("NOMBRE") = valor_nombre
                dRow.Item("NOMBRE") = v_nombre_dm
                'dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
                dRow.Item("ZONA") = valor_zona
                dRow.Item("CARTA") = valor_carta
                dRow.Item("CONTA") = cuenta2
                ' dRow.Item("FECHA") = fecha1 & fecha2
                dRow.Item("FECHA") = fecha
                dRow.Item("GRAFICA") = val_grafica
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If


        Me.dgdDetalle.DataSource = lodtTabla
        PT_Estilo_Grilla_prevaluacion(lodtTabla) : PT_Cargar_Grilla_prevaluacion(lodtTabla)
        PT_Agregar_Funciones_prevaluacion() : PT_Forma_Grilla_Funciones_prevaluacion()

        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
            dgdDetalle.Item(i, "SELEC") = True
        Next
        Me.dgdDetalle.AllowUpdate = True
        lbl_lista.Text = conta_lista
        dRow = Nothing
        dgdDetalle.Focus()
        colecciones_txtfiles.Clear()
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub btnIndividual_Click(sender As Object, e As EventArgs) Handles btnIndividual.Click

        Dim pDatum As String = ""
        Dim vCodigo As String = ""
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        Dim cls_eval As New Cls_evaluacion
        v_opcion_mas = "1"
        Dim cuenta As Integer = 10
        Dim cuenta1 As Integer = 0
        cls_catastro.Borra_Todo_Feature("", m_application)
        cls_catastro.Limpiar_Texto_Pantalla(m_application)


        cls_eval.Eliminadataframe("CATASTRO MINERO21")
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        gstrFC_CDistrito = "GPO_CDI_CAPITAL_DISTRITO_18"


        ' cls_eval.ActivaDataframe_Opcion("CATASTRO MINERO1", m_application)
        ' pMxDoc.UpdateContents()
        ' cls_eval.Eliminadataframe("CATASTRO MINERO1")
        ' pMxDoc.UpdateContents()
        ' pMxDoc.ActiveView.Refresh()

        gstrFC_Departamento_WGS = "GPO_DEP_DEPARTAMENTO_WGS_"
        gstrFC_Provincia_WGS = "GPO_PRO_PROVINCIA_WGS_"
        gstrFC_Distrito_WGS = "GPO_DIS_DISTRITO_WGS_"
        gstrFC_Rios = "GLI_RIO_RIOS_18"
        gstrFC_Carretera = "GLI_VIA_VIAS_18"
        gstrFC_CPoblado = "GPT_CPO_CENTRO_POBLADO_18"
        'gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Cuadricula = "GPO_CRE_CUADRICULA_REGIONAL_18"     ''''''''''''''''''''''''''''''''''''
        'gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        'gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_ZUrbana56 = "GPO_ZUR_ZONA_URBANA_G84"
        gstrFC_AReservada56 = "GPO_ARE_AREA_RESERVADA_G84"
        gstrFC_ZTraslape = "GPO_ZTR_ZONA_TRASLAPE_18"
        gstrFC_Frontera_Z = "GLI_FRO_FRONTERA_WGS_"
        gstrFC_Frontera_10 = "GLI_FRO_FRONTERA_10K_WGS18"
        gstrFC_Frontera_25 = "GLI_FRO_FRONTERA_25K_WGS18"
        gstrFC_LHojas = "GPO_HOJ_HOJAS_18"
        gstrFC_Carta = "GPO_HOJ_HOJAS_18"

        gstrFC_CARAM = "GPO_CAR_CARAM_WGS_"
        gstrFC_CARAM56 = "GPO_CAR_CARAM_"

        '  Dim cls_eval As New Cls_evaluacion


        Dim contador_lista As Integer = 0

        'Para solo contar los marcados por el usuario

        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                contador_lista = contador_lista + 1
                v_vigcat = dgdDetalle.Item(w1, "GRAFICA").ToString
                If v_vigcat <> "G" Then
                    MsgBox("Debe Seleccionar solo los grafican, sino el sistema no procesará...", MsgBoxStyle.Critical, "SIGCATMIN")
                    Exit Sub

                End If

                If contador_lista > 1 Then
                    MsgBox("Debe Seleccionar solo un caso para realizarlo de manera individual...", MsgBoxStyle.Critical, "SIGCATMIN")
                    Exit Sub

                End If
            End If


        Next w1

        'Termino contador de los registros marcados
      
        'Empieza a recorrar
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1


            If dgdDetalle.Item(w, "SELEC") = True Then
                lista_forestal = ""
                v_masivo_eval = "1"
                cuenta = cuenta + 1
                cuenta1 = cuenta1 + 1
                v_codigo = dgdDetalle.Item(w, "CODIGO").ToString.ToUpper
                '  v_codigo = "010039303"
                ' v_clase_rese = dgdDetalle.Item(w, "CLASE").ToString
                'v_zona_rese = dgdDetalle.Item(w, "ZONA").ToString


                v_fecha_dm = dgdDetalle.Item(w, "FECHA").ToString
                v_zona_dm = dgdDetalle.Item(w, "ZONA").ToString
                ' v_nombre_dm = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                v_nombre_dm = dgdDetalle.Item(w, "NOMBRE").ToString
                '  v_vigcat = "G"
                v_vigcat = dgdDetalle.Item(w, "GRAFICA").ToString
                v_carta_dm = dgdDetalle.Item(w, "CARTA").ToString
                pMxDoc = m_application.Document
                caso_consulta = "CATASTRO MINERO" & cuenta


                cls_eval.adicionadataframe(caso_consulta)
                ' cls_eval.activadataframe(caso_consulta)

                m_application.Caption = "PROCESO  MASIVO DE PRE EVALUACION PARA GOB. REGIONALES :  " & v_codigo & "      " & cuenta1.ToString & "  De  " & contador_lista
                cls_catastro.Consulta_Evaluacion_DM_masivo(m_application)
                Dim A As Integer = 2
                '   Exit Sub
                cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
                cls_catastro.Genera_Imagen_DM(v_codigo, "regional")
                MsgBox("EL PROCESO HA FINALIZADO SATISFACTORIAMENTE...", MsgBoxStyle.Exclamation, "SIGCATMIN")
                Exit Sub
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
                cls_catastro.Genera_Imagen_DM(v_codigo, "regional")
                cls_catastro.Borra_Todo_Feature("", m_application)
                cls_catastro.Limpiar_Texto_Pantalla(m_application)
                pMxDoc.UpdateContents()
                cls_eval.Eliminadataframe_masivo(caso_consulta)
                pMxDoc.UpdateContents()
                cls_planos.CambiaADataView(m_application)
            End If
        Next w

        ' MsgBox("EL PROCESO HA FINALIZADO SATISFACTORIAMENTE...", MsgBoxStyle.Exclamation, "SIGCATMIN")
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        v_sistema = "WGS-84"

        '  fecsup = Microsoft.VisualBasic.Left(Format(dtpFecha1.Value), 10)

       

        Dim fecha As String
        Dim cadena As String
        Dim lodtTabla As New DataTable
        Dim dRow As DataRow
        Dim MyDate As Date

        Dim valor_codigo As String = ""
        Dim valor_nombre As String = ""
        Dim valor_zona As String = ""
        Dim valor_carta As String = ""


        Dim codigo As String
        lodtTabla.Clear()
        lodtTabla.Columns.Add("SELEC", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CONTA", Type.GetType("System.Double"))
        lodtTabla.Columns.Add("CODIGO", Type.GetType("System.String"))
        lodtTabla.Columns.Add("NOMBRE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("FECHA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("GRAFICA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CARTA", Type.GetType("System.String"))

        Dim cls_datos As New cls_Oracle
        Dim lostrRetorno1 As New DataTable

        MyDate = Now

        Dim cuenta As Integer = 0
        Dim cuenta1 As Integer = 0

        '  fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        'fecha = fecsup

        ' fecha = "25/05/2019"

        Dim contador_lineas As Long


        codigo = Me.txt_codigo.Text
        If codigo = "" Then
            MsgBox("No ingreso codigo", MsgBoxStyle.Critical, "SIGCATMIN")
            Exit Sub

        End If

        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0
        Dim valor_txt As String = ""
        Dim val_grafica As String = ""
       

        Dim conta_lista As Integer

        Try


            'lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA_UNICO(fecha)

            lostrRetorno1 = cls_datos.FT_consultar_SEGUN_CODIGO(codigo)

        Catch ex As Exception

        End Try

        conta_lista = lostrRetorno1.Rows.Count
        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                valor_codigo = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                v_nombre_dm = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
                valor_zona = lostrRetorno1.Rows(contador1).Item("PE_ZONCAT")
                valor_carta = lostrRetorno1.Rows(contador1).Item("CA_CODCAR")
                val_grafica = lostrRetorno1.Rows(contador1).Item("PE_VIGCAT")
                fecha = lostrRetorno1.Rows(contador1).Item("CG_FECREG")
                dRow.Item("CODIGO") = valor_codigo
                dRow.Item("NOMBRE") = v_nombre_dm
                dRow.Item("ZONA") = valor_zona
                dRow.Item("CARTA") = valor_carta
                dRow.Item("CONTA") = cuenta2
                dRow.Item("FECHA") = fecha
                dRow.Item("GRAFICA") = val_grafica
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If


        Me.dgdDetalle.DataSource = lodtTabla
        PT_Estilo_Grilla_prevaluacion(lodtTabla) : PT_Cargar_Grilla_prevaluacion(lodtTabla)
        PT_Agregar_Funciones_prevaluacion() : PT_Forma_Grilla_Funciones_prevaluacion()

        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
            dgdDetalle.Item(i, "SELEC") = True
        Next
        Me.dgdDetalle.AllowUpdate = True
        lbl_lista.Text = conta_lista
        dRow = Nothing
        dgdDetalle.Focus()
        colecciones_txtfiles.Clear()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnIndcuad.Click
        Dim pDatum As String = ""
        Dim vCodigo As String = ""
        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        Dim cls_eval As New Cls_evaluacion
        v_opcion_mas = "3"
        Dim cuenta As Integer = 20
        Dim cuenta1 As Integer = 0
        cls_catastro.Borra_Todo_Feature("", m_application)
        cls_catastro.Limpiar_Texto_Pantalla(m_application)

        cls_eval.Eliminadataframe("CATASTRO MINERO11")
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        gstrFC_CDistrito = "GPO_CDI_CAPITAL_DISTRITO_18"


        gstrFC_Departamento_WGS = "GPO_DEP_DEPARTAMENTO_WGS_"
        gstrFC_Provincia_WGS = "GPO_PRO_PROVINCIA_WGS_"
        gstrFC_Distrito_WGS = "GPO_DIS_DISTRITO_WGS_"
        gstrFC_Rios = "GLI_RIO_RIOS_18"
        gstrFC_Carretera = "GLI_VIA_VIAS_18"
        gstrFC_CPoblado = "GPT_CPO_CENTRO_POBLADO_18"
        'gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
        gstrFC_Cuadricula = "GPO_CRE_CUADRICULA_REGIONAL_18"     ''''''''''''''''''''''''''''''''''''
        'gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        'gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
        gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
        gstrFC_ZUrbana56 = "GPO_ZUR_ZONA_URBANA_G84"
        gstrFC_AReservada56 = "GPO_ARE_AREA_RESERVADA_G84"
        gstrFC_ZTraslape = "GPO_ZTR_ZONA_TRASLAPE_18"
        gstrFC_Frontera_Z = "GLI_FRO_FRONTERA_WGS_"
        gstrFC_Frontera_10 = "GLI_FRO_FRONTERA_10K_WGS18"
        gstrFC_Frontera_25 = "GLI_FRO_FRONTERA_25K_WGS18"
        gstrFC_LHojas = "GPO_HOJ_HOJAS_18"
        gstrFC_Carta = "GPO_HOJ_HOJAS_18"

        gstrFC_Cuadriculas = "GPO_CUA_CUADRICULAS_WGS_"
        Dim cls_DM_2 As New cls_DM_2


        Dim contador_lista As Integer = 0

        'Para solo contar los marcados por el usuario

        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                contador_lista = contador_lista + 1
                v_vigcat = dgdDetalle.Item(w1, "GRAFICA").ToString
                If v_vigcat <> "G" Then
                    MsgBox("Debe Seleccionar solo los grafican, sino el sistema no procesará...", MsgBoxStyle.Critical, "SIGCATMIN")
                    Exit Sub

                End If

                If contador_lista > 1 Then
                    MsgBox("Debe Seleccionar solo un caso para realizarlo de manera individual...", MsgBoxStyle.Critical, "SIGCATMIN")
                    Exit Sub

                End If
            End If


        Next w1

        'Termino contador de los registros marcados

        'Empieza a recorrar
        For w As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1


            If dgdDetalle.Item(w, "SELEC") = True Then
                v_masivo_eval = "1"
                cuenta = cuenta + 1
                cuenta1 = cuenta1 + 1
                v_codigo = dgdDetalle.Item(w, "CODIGO").ToString.ToUpper

                v_fecha_dm = dgdDetalle.Item(w, "FECHA").ToString
                v_zona_dm = dgdDetalle.Item(w, "ZONA").ToString
                v_nombre_dm = dgdDetalle.Item(w, "NOMBRE").ToString
                v_vigcat = dgdDetalle.Item(w, "GRAFICA").ToString
                v_carta_dm = dgdDetalle.Item(w, "CARTA").ToString
                pMxDoc = m_application.Document
                caso_consulta = "CATASTRO MINERO" & cuenta
                'If v_zona_dm = "17" Then

                '    caso_consulta = "CATASTRO MINERO" & cuenta & v_zona_dm
                'ElseIf v_zona_dm = "18" Then
                '    caso_consulta = "CATASTRO MINERO" & cuenta & v_zona_dm

                'ElseIf v_zona_dm = "19" Then

                '    caso_consulta = "CATASTRO MINERO" & cuenta & v_zona_dm
                'End If


                cls_eval.adicionadataframe(caso_consulta)
                m_application.Caption = "PROCESO  MASIVO DE PRE EVALUACION PARA GOB. REGIONALES :  " & v_codigo & "      " & cuenta1.ToString & "  De  " & contador_lista

                cls_catastro.Load_FC_GDB("gpt_Vertice_DM_" & v_zona_dm, "", m_application, True)
                cls_catastro.Delete_Rows_FC_GDB("gpt_Vertice_DM_" & v_zona_dm)




                cls_DM_2.Genera_Catastro_DM_reg(v_codigo, v_zona_dm, m_application)
                '  cls_catastro.Quitar_Layer("gpt_Vertice_DM_" & v_zona_dm, m_application)
                cls_catastro.Consulta_Evaluacion_DM_masivo(m_application)

                ' Exit Sub

                'hoy 02-09-19

                If contador_cua = 0 Then
                    Dim A As Integer = 2

                    cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)

                    pMxDoc.UpdateContents()
                    pMxDoc.ActiveView.Refresh()
                    cls_catastro.Genera_Imagen_DM(v_codigo, "regional")

                Else
                    For A As Integer = 1 To 2

                        If A = 1 Then
                            'cls_catastro.Add_ShapeFile1(loStrShapefile_cat, m_application, "Cuadriculas")
                            cls_catastro.Add_ShapeFile1(loStrShapefile_cat_cuad, m_application, "Cuadriculas")
                            cls_catastro.Color_Poligono_Simple(m_application, "Cuadri_Suptot")

                            pMxDoc.UpdateContents()
                            pMxDoc.ActiveView.Refresh()
                            cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)

                            pMxDoc.UpdateContents()
                            pMxDoc.ActiveView.Refresh()
                            cls_catastro.Genera_Imagen_DM(v_codigo & "_" & A, "regional")
                            cls_catastro.Genera_Imagen_planopdf(v_codigo & "_" & A, "regional")
                            cls_catastro.Quitar_Layer("Cuadri_Suptot", m_application)
                            pMxDoc.UpdateContents()
                            pMxDoc.ActiveView.Refresh()
                        Else

                            cls_catastro.Quitar_Layer("Cuadriculas", m_application)
                            cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)
                            pMxDoc.UpdateContents()
                            pMxDoc.ActiveView.Refresh()
                            cls_catastro.Genera_Imagen_DM(v_codigo, "regional")
                            cls_catastro.Color_AR_Plano(m_application, "Zona Reservada")
                            cls_catastro.Genera_Imagen_planopdf(v_codigo, "regional")
                            cls_catastro.Add_ShapeFile1(loStrShapefile_cat_cuad, m_application, "Cuadriculas")
                            cls_catastro.Color_Poligono_Simple(m_application, "Cuadri_Suptot")

                        End If
                        pMxDoc.UpdateContents()
                        pMxDoc.ActiveView.Refresh()

                        'cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion", A)

                        'pMxDoc.UpdateContents()
                        'pMxDoc.ActiveView.Refresh()
                        ' cls_catastro.Genera_Imagen_DM(v_codigo & "_" & A, "regional")

                    Next A

                End If
                contador_cua = 0
                v_opcion_mas = ""

                ' cls_planos.genera_planoevaluacion_masivo(m_application, "Evaluacion")

                MsgBox("EL PROCESO HA FINALIZADO SATISFACTORIAMENTE...", MsgBoxStyle.Exclamation, "SIGCATMIN")

                Exit Sub
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()
                cls_catastro.Genera_Imagen_DM(v_codigo, "regional")
                cls_catastro.Borra_Todo_Feature("", m_application)
                cls_catastro.Limpiar_Texto_Pantalla(m_application)
                pMxDoc.UpdateContents()
                cls_eval.Eliminadataframe_masivo(caso_consulta)
                pMxDoc.UpdateContents()
                cls_planos.CambiaADataView(m_application)
            End If
        Next w

    End Sub

    Private Sub btnLibredenu_Click(sender As Object, e As EventArgs) Handles btnLibredenu.Click
        Me.Close()
        caso_ldmasivo = "1"
        Dim pForm As New Frm_excelLibredenu
        pForm.m_Application = m_application
        pForm.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
        pForm.Show()
        SetWindowLong(pForm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd)
    End Sub
End Class