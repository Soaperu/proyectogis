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

Public Class Frm_Plano_Caram
    Private dt As New DataTable
    Public pApp As IApplication
    Public p_App As IApplication
    Public m_application As IApplication

    Private Const Col_Sel_R As Integer = 0
    Private Const Col_conta As Integer = 1
    Private Const Col_Codigo As Integer = 2
    Private Const Col_modred As Integer = 3
    Private Const Col_fecha As Integer = 4
    Private Const Col_clase As Integer = 5
    Private Const Col_zona As Integer = 6
    Private Const Col_archivo As Integer = 7





    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Dim pDatum As String = ""
        Dim vCodigo As String = ""


        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0


        Dim archivo As String
        Dim codigo As String
        Dim p_zona1 As String


        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        v_opcion_mas = "2"
        val_opcion_plano_Ar = "1"
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
        Dim lodtbArea_Reserva As DataTable
        Dim cls_oracle As New cls_Oracle

        Dim contador_lista As Integer = 0

        'Para solo contar los marcados por el usuario
        '  cls_catastro.creandotabla_Rep_Libredenu("Regional")

        v_usuario_Ar = "Frank Latorraca"

        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                contador_lista = contador_lista + 1
               
            End If

        Next w1


        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                'contador_lista = contador_lista + 1
                cuenta = cuenta + 1
                codigo = dgdDetalle.Item(w1, "CODIGO").ToString
                v_codigo = codigo

                v_archivo = dgdDetalle.Item(w1, "ARCHIVO").ToString
                tp_archivo = dgdDetalle.Item(w1, "MODREG").ToString
                v_clase_rese = dgdDetalle.Item(w1, "CLASE").ToString
                p_zona1 = dgdDetalle.Item(w1, "ZONA").ToString
                fec_archivo = dgdDetalle.Item(w1, "FECHA").ToString


                pMxDoc = m_application.Document
              


                caso_consulta = "CATASTRO MINERO" & cuenta
                caso_plano = "PLANO UBICACION" & cuenta
                cls_eval.adicionadataframe(caso_consulta)

                v_zona_dm = p_zona1

                lodtbArea_Reserva = cls_oracle.FT_Ver_Area_Restringida(codigo, v_clase_rese)

                Dim lodbtExiste_tpnormarese As DataTable
                'SELECCIONANDO LA DEMARCACION POLITICA
                '   Dim lista_cadena_dist As String
                '   Dim consulta_lista_dist As String

                If lodtbArea_Reserva.Rows.Count >= 1 Then
                    For contadorx As Integer = 0 To lodtbArea_Reserva.Rows.Count - 1
                        v_nom_rese_sele = lodtbArea_Reserva.Rows(contadorx).Item("PE_NOMARE")

                        v_tp_ar_sele = lodtbArea_Reserva.Rows(contadorx).Item("TN_DESTIP")
                        val_nltipnorma = lodtbArea_Reserva.Rows(contadorx).Item("NL_TIPNOR").ToString

                        If val_nltipnorma <> "" Then
                            Try
                                lodbtExiste_tpnormarese = cls_oracle.FT_OBTIENE_NORMARESE(codigo, val_nltipnorma)

                                If lodbtExiste_tpnormarese.Rows.Count >= 1 Then

                                    For b As Integer = 0 To lodbtExiste_tpnormarese.Rows.Count - 1
                                        ' v_cod_rese = lodbtExiste_tpnormarese.Rows(a).Item("CG_CODIGO").ToString
                                        val_TIPO_norma = lodbtExiste_tpnormarese.Rows(b).Item("NL_DESNOR").ToString
                                        val_TIP_RESO_norma = lodbtExiste_tpnormarese.Rows(b).Item("AN_RESPLM").ToString

                                    Next b
                                    val_cadena_norma = val_TIP_RESO_norma & val_TIPO_norma
                                End If

                            Catch
                            End Try
                        Else
                            '  val_TIPO_norma = "describa    "
                            ' val_TIP_RESO_norma = "22222 "

                            '  val_TIPO_norma = "describa    "
                            '  val_TIP_RESO_norma = "22222 "
                            val_cadena_norma = val_TIP_RESO_norma & val_TIPO_norma
                        End If

                    Next contadorx

                Else
                    MsgBox("ERROR EN CONSULTAR NOMBRE DEL AREA RESTRINGIDA, VERFIICAR", MsgBoxStyle.Critical, "PLANO")
                    Exit Sub

                End If


                m_application.Caption = "PROCESO  MASIVO DE PLANOS :  " & v_codigo & "      " & cuenta.ToString & "  De  " & contador_lista
                cls_catastro.consulta_arearestringida_PIM(codigo, v_archivo, p_zona1, m_application)
                Dim A As Integer = 2
                'ojo falta mas indicador en el plano
                cls_catastro.Zoom_to_Layer("Zona Reservada")
                cls_catastro.Zoom_to_Layer("Zona Reservada1")

                cls_planos.generaplanos_arearestringida_masivo(m_application)

                'NO ESTA SALIENDO EL MAPA UBICACION, COLOCAR ARCHIVO A LA FOTO
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()

                cls_catastro.Genera_Imagen_planopdf(codigo, "restringida")
                ' pMxDoc.UpdateContents()
                ' cls_planos.CambiaADataView(pApp)

                cls_catastro.Borra_Todo_Feature("", m_application)
                '  cls_catastro.Limpiar_Texto_Pantalla(m_application)
                pMxDoc.UpdateContents()
                cls_eval.Eliminadataframe_masivo(caso_consulta)
                cls_eval.Eliminadataframe_masivo(caso_plano)

                pMxDoc.UpdateContents()
                cls_planos.CambiaADataView(m_application)



            End If

        Next w1

        MsgBox("El Proceso de Generación de Planos Automaticos ha finalizado...", MsgBoxStyle.Information, "SIGCATMIN")
        val_opcion_plano_Ar = "0"



    End Sub

    Private Sub btnIndividual_Click(sender As Object, e As EventArgs) Handles btnIndividual.Click
        Dim pDatum As String = ""
        Dim vCodigo As String = ""


        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0


        Dim archivo As String
        Dim codigo As String
        Dim p_zona1 As String


        Dim cls_planos As New Cls_planos
        Dim cls_catastro As New cls_DM_1
        v_opcion_mas = "2"
        val_opcion_plano_Ar = "1"
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
        Dim lodtbArea_Reserva As DataTable
        Dim cls_oracle As New cls_Oracle

        Dim contador_lista As Integer = 0

        'Para solo contar los marcados por el usuario
        '  cls_catastro.creandotabla_Rep_Libredenu("Regional")

        v_usuario_Ar = "Frank Latorraca"

        For w1 As Integer = 0 To dgdDetalle.BindingContext(dgdDetalle.DataSource, dgdDetalle.DataMember).Count - 1
            If dgdDetalle.Item(w1, "SELEC") = True Then
                contador_lista = contador_lista + 1
                codigo = dgdDetalle.Item(w1, "CODIGO").ToString
                v_archivo = dgdDetalle.Item(w1, "ARCHIVO").ToString
                tp_archivo = dgdDetalle.Item(w1, "MODREG").ToString
                v_clase_rese = dgdDetalle.Item(w1, "CLASE").ToString
                p_zona1 = dgdDetalle.Item(w1, "ZONA").ToString
                fec_archivo = dgdDetalle.Item(w1, "FECHA").ToString


                pMxDoc = m_application.Document



                caso_consulta = "CATASTRO MINERO" & cuenta
                caso_plano = "PLANO UBICACION" & cuenta
                cls_eval.adicionadataframe(caso_consulta)

                v_zona_dm = p_zona1

                lodtbArea_Reserva = cls_oracle.FT_Ver_Area_Restringida(codigo, v_clase_rese)

                Dim lodbtExiste_tpnormarese As DataTable
                'SELECCIONANDO LA DEMARCACION POLITICA
                '   Dim lista_cadena_dist As String
                '   Dim consulta_lista_dist As String

                If lodtbArea_Reserva.Rows.Count >= 1 Then
                    For contadorx As Integer = 0 To lodtbArea_Reserva.Rows.Count - 1
                        v_nom_rese_sele = lodtbArea_Reserva.Rows(contadorx).Item("PE_NOMARE")

                        v_tp_ar_sele = lodtbArea_Reserva.Rows(contadorx).Item("TN_DESTIP")
                        val_nltipnorma = lodtbArea_Reserva.Rows(contadorx).Item("NL_TIPNOR").ToString

                        If val_nltipnorma <> "" Then
                            Try
                                lodbtExiste_tpnormarese = cls_oracle.FT_OBTIENE_NORMARESE(codigo, val_nltipnorma)

                                If lodbtExiste_tpnormarese.Rows.Count >= 1 Then

                                    For b As Integer = 0 To lodbtExiste_tpnormarese.Rows.Count - 1
                                        ' v_cod_rese = lodbtExiste_tpnormarese.Rows(a).Item("CG_CODIGO").ToString
                                        val_TIPO_norma = lodbtExiste_tpnormarese.Rows(b).Item("NL_DESNOR").ToString
                                        val_TIP_RESO_norma = lodbtExiste_tpnormarese.Rows(b).Item("AN_RESPLM").ToString

                                    Next b
                                    val_cadena_norma = val_TIP_RESO_norma & val_TIPO_norma
                                End If

                            Catch
                            End Try
                        Else
                            '  val_TIPO_norma = "describa    "
                            ' val_TIP_RESO_norma = "22222 "

                            '  val_TIPO_norma = "describa    "
                            '  val_TIP_RESO_norma = "22222 "
                            val_cadena_norma = val_TIP_RESO_norma & val_TIPO_norma
                        End If

                    Next contadorx

                Else
                    MsgBox("ERROR EN CONSULTAR NOMBRE DEL AREA RESTRINGIDA, VERFIICAR", MsgBoxStyle.Critical, "PLANO")
                    Exit Sub

                End If



                cls_catastro.consulta_arearestringida_PIM(codigo, v_archivo, p_zona1, m_application)
                Dim A As Integer = 2
                'ojo falta mas indicador en el plano
                ' cls_catastro.Zoom_to_Layer("Zona Reservada")
                ' cls_catastro.Zoom_to_Layer("Zona Reservada1")

                cls_catastro.Zoom_to_Layer("Caram")
                cls_catastro.Zoom_to_Layer("Caram1")



                cls_planos.generaplanos_arearestringida_masivo(m_application)

                'NO ESTA SALIENDO EL MAPA UBICACION, COLOCAR ARCHIVO A LA FOTO
                pMxDoc.UpdateContents()
                pMxDoc.ActiveView.Refresh()

                '  cls_catastro.Genera_Imagen_planopdf(codigo, "restringida")
              

                '  cls_catastro.Borra_Todo_Feature("", m_application)

                ' pMxDoc.UpdateContents()
                

                'pMxDoc.UpdateContents()
                'cls_planos.CambiaADataView(m_application)



            End If

        Next w1

        MsgBox("El Proceso de Generación de Planos Automaticos ha finalizado...", MsgBoxStyle.Information, "SIGCATMIN")
        val_opcion_plano_Ar = "0"
    End Sub

    Private Sub Frm_Plano_Caram_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        lodtTabla.Columns.Add("MODREG", Type.GetType("System.String"))
        lodtTabla.Columns.Add("FECHA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CLASE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ARCHIVO", Type.GetType("System.String"))
        
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

            ' lostrRetorno1 = cls_datos.FT_consultar_proceso_Anm("001", fecha)
            lostrRetorno1 = cls_datos.FT_consultar_proceso_caram("001", fecha)

            'lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA(fecha)
            'lostrRetorno1 = cls_datos.FT_consultar_INGRESO_DMXDIA("13/05/2019")
            ' FT_consultar_INGRESO_DMXDIA()
        Catch ex As Exception

        End Try

        conta_lista = lostrRetorno1.Rows.Count
        'If lostrRetorno1.Rows.Count > 0 Then
        'For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
        'dRow = lodtTabla.NewRow
        'cuenta2 = cuenta2 + 1
        'valor_codigo = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
        ''valor_nombre = lostrRetorno1.Rows(contador1).Item("PE_NOMDER")
        'v_nombre_dm = lostrRetorno1.Rows(contador1).Item("MODREG")
        'valor_zona = lostrRetorno1.Rows(contador1).Item("FECHA")
        'valor_carta = lostrRetorno1.Rows(contador1).Item("TIPO_GRA")
        'val_grafica = lostrRetorno1.Rows(contador1).Item("ARCHIVO")
        '
        '       dRow.Item("CODIGO") = valor_codigo
        '      ' dRow.Item("NOMBRE") = valor_nombre
        '     dRow.Item("MODREG") = v_nombre_dm
        '    'dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
        '   dRow.Item("FECHA") = valor_zona
        '  dRow.Item("TIPO_GRA") = valor_carta
        ' dRow.Item("CONTA") = cuenta2
        'dRow.Item("ARCHIVO") = fecha1 & fecha2
        ' '  dRow.Item("GRAFICA") = val_grafica
        ' lodtTabla.Rows.Add(dRow)
        ' Next contador1

        '        End If

        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                cadena = lostrRetorno1.Rows(contador1).Item("RE_MODREG")

                ' If (cadena = "ANDG") Then
                'valor_accion = "INGRESO"
                'v_tipoproceso = "INGRESAR"

                'ElseIf (cadena = "ANDM") Then
                'valor_accion = "MODIFICACION"
                'v_tipoproceso = "MODIFICAR"
                'ElseIf (cadena = "ANDE") Then
                'valor_accion = "ELIMINACION"
                'v_tipoproceso = "ELIMINAR"
                'End If


                valor_txt = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                nomb_archivo = lostrRetorno1.Rows(contador1).Item("RE_ARCGRA")
                tp_archivo = lostrRetorno1.Rows(contador1).Item("RE_TIPGRA")
                zona_archivo = lostrRetorno1.Rows(contador1).Item("RE_NUMZON")
                fec_archivo = lostrRetorno1.Rows(contador1).Item("RE_FECREG")


                dRow.Item("CODIGO") = valor_txt
                dRow.Item("ARCHIVO") = nomb_archivo
                dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
                dRow.Item("ZONA") = zona_archivo
                dRow.Item("MODREG") = cadena
                dRow.Item("CONTA") = cuenta2
                dRow.Item("FECHA") = fec_archivo
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If



        Me.dgdDetalle.DataSource = lodtTabla
        PT_Estilo_Grilla_caram(lodtTabla) : PT_Cargar_Grilla_caram(lodtTabla)
        PT_Agregar_Funciones_caram() : PT_Forma_Grilla_Funciones_caram()

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

    Private Sub PT_Estilo_Grilla_caram(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_Codigo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_modred).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_fecha).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_clase).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_zona).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_archivo).DefaultValue = ""


    End Sub
    Private Sub PT_Cargar_Grilla_caram(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = dvwDetalle
        Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_caram()
        Me.dgdDetalle.Columns(Col_Sel_R).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_Codigo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_modred).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_fecha).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_clase).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_zona).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_archivo).DefaultValue = ""

        ' Me.dgdDetalle.Columns(Col_carta).DefaultValue = ""


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

    Private Sub PT_Forma_Grilla_Funciones_caram()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Width = 30
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Width = 40
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_modred).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Width = 60
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Width = 60
        '  Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Width = 100

        Me.dgdDetalle.Columns("SELEC").Caption = "SEL."
        Me.dgdDetalle.Columns("CONTA").Caption = "NRO."
        Me.dgdDetalle.Columns("CODIGO").Caption = "CODIGO"
        Me.dgdDetalle.Columns("MODREG").Caption = "MODREG"
        Me.dgdDetalle.Columns("FECHA").Caption = "FECHA"
        Me.dgdDetalle.Columns("CLASE").Caption = "CLASE"
        Me.dgdDetalle.Columns("ZONA").Caption = "ZONA"
        Me.dgdDetalle.Columns("ARCHIVO").Caption = "ARCHIVO"


        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Blue
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_modred).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Locked = True
        '  Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Locked = True


        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_modred).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        ' Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_conta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_Codigo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_modred).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_fecha).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_clase).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zona).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_archivo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        '   Me.dgdDetalle.Splits(0).DisplayColumns(Col_carta).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

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

    Private Sub dtpFecha1_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha1.ValueChanged
        fecsup = Microsoft.VisualBasic.Left(Format(dtpFecha1.Value), 10)
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
        lodtTabla.Columns.Add("MODREG", Type.GetType("System.String"))
        lodtTabla.Columns.Add("FECHA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("CLASE", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ZONA", Type.GetType("System.String"))
        lodtTabla.Columns.Add("ARCHIVO", Type.GetType("System.String"))

        '    lodtTabla.Columns.Add("ACCION", Type.GetType("System.String"))


        Dim cls_datos As New cls_Oracle
        Dim lostrRetorno1 As New DataTable

        MyDate = Now

        Dim cuenta As Integer = 0
        Dim cuenta1 As Integer = 0
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & RellenarComodin(MyDate.Month, 2, "0") & RellenarComodin(MyDate.Year, 2, "0")
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        ' fecha = "25/05/2019"
        '  fecha = "28/05/2019"
        'fecha = RellenarComodin(MyDate.Day, 2, "0") & "/" & RellenarComodin(MyDate.Month, 2, "0") & "/" & RellenarComodin(MyDate.Year, 2, "0")
        fecha = fecsup
        Dim contador_lineas As Long


        Dim nomb_archivo As String
        Dim tp_archivo As String
        Dim zona_archivo As String
        Dim fec_archivo As String
        Dim cuenta2 As Integer = 0
        Dim valor_txt As String = ""
        Dim val_grafica As String = ""
        ' fecha = Microsoft.VisualBasic.Left(fecha, 10).ToString
        ' Dim fecha1 As String
        ' Dim fecha2 As String
        ' fecha1 = Microsoft.VisualBasic.Left(fecha, 2).ToString
        ' fecha2 = Microsoft.VisualBasic.Right(fecha, 8).ToString
        ' If (fecha1 = "01") Then
        ' fecha1 = "31"
        ' End If
        ' fecha1 = fecha1 - 1
        Dim conta_lista As Integer

        ' fecha = fecha1 - 1
        Try


            lostrRetorno1 = cls_datos.FT_consultar_proceso_caram("001", fecha)

        Catch ex As Exception

        End Try

        conta_lista = lostrRetorno1.Rows.Count


        If lostrRetorno1.Rows.Count > 0 Then
            For contador1 As Integer = 0 To lostrRetorno1.Rows.Count - 1
                dRow = lodtTabla.NewRow
                cuenta2 = cuenta2 + 1
                cadena = lostrRetorno1.Rows(contador1).Item("RE_MODREG")




                valor_txt = lostrRetorno1.Rows(contador1).Item("CG_CODIGO")
                nomb_archivo = lostrRetorno1.Rows(contador1).Item("RE_ARCGRA")
                tp_archivo = lostrRetorno1.Rows(contador1).Item("RE_TIPGRA")
                zona_archivo = lostrRetorno1.Rows(contador1).Item("RE_NUMZON")
                fec_archivo = lostrRetorno1.Rows(contador1).Item("RE_FECREG")


                dRow.Item("CODIGO") = valor_txt
                dRow.Item("ARCHIVO") = nomb_archivo
                dRow.Item("CLASE") = Microsoft.VisualBasic.Left(tp_archivo, 1)
                dRow.Item("ZONA") = zona_archivo
                dRow.Item("MODREG") = cadena
                dRow.Item("CONTA") = cuenta2
                dRow.Item("FECHA") = fec_archivo
                lodtTabla.Rows.Add(dRow)
            Next contador1

        End If



        Me.dgdDetalle.DataSource = lodtTabla
        PT_Estilo_Grilla_caram(lodtTabla) : PT_Cargar_Grilla_caram(lodtTabla)
        PT_Agregar_Funciones_caram() : PT_Forma_Grilla_Funciones_caram()

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
End Class