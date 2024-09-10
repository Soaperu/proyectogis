'@Autor: Daniel Aguado H.
'@Fecha: 02/08/2018

Imports System.Windows.Forms    'Importa libreria para funcionalidad de formularios
Imports Newtonsoft.Json   'Importa la libreria para trabajar con JSON
Imports System.Text.RegularExpressions
Imports System.Text
Imports PORTAL_Clases
Imports System.Drawing
'Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports System.Linq


Public Class form_ueas
    Private cls_oracle As New cls_Oracle
    'Private WithEvents filter As New MyFilter

    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private clsBarra As New cls_Barra
    Private cls_eval As New Cls_evaluacion

    Private cls_Prueba As New cls_Prueba
    Private cls_planos As New Cls_planos

    Private _min_numero_caracteres_busqueda As Integer = 3  'Numero minimo de caracteres para proceder con la busqueda de la UEA

    'Private _bat_ueas As String = "U:\DATOS\SHAPE\process\execute\unidad_economica_administrativa.bat" 'Proceso bat que tiene funcionalidad sobre UEAS
    'Private _parametros As String = "--uea {0} --zona {1} {2} --token 70dad6bd-057f-4512-b7f3-82fb588a1721"
    Private _parametros As String = "--uea {0} --zona {1} --user {2} --password {3} {4}"

    '_parametros = "--uea {0} --zona {1} --user {2} --password {3} {4}"
    Private _fn_graficar As String = "graficar"
    Private _fn_reporte_uea As String = "reporte_datos"
    Private _fn_reporte_otras_uea As String = "reporte_integrantes_otras_ueas"

    Private _situacion_extinguida As String = "X"

    Private _cambios As Integer = 0

    'Private _loader_proceso_general As String = "proceso_general_sigcatmin.exe"
    'Private _loader_proceso_buscar As String = "proceso_buscar_sigcatmin.exe"
    'Private _path_loader_proceso_general As String = glo_Path & "\loaders\" & _loader_proceso_general
    'Private _path_loader_proceso_buscar As String = glo_Path & "\loaders\" & _loader_proceso_buscar

    ' Realiza la busqueda de UEA al dar click sobre el boton buscar
    Private Sub button_buscar_Click(sender As Object, e As EventArgs) Handles button_buscar.Click

        'El codigo de UEA siempre debe inicializarce como vacio
        coduea = ""

        'Se reinicializa el controlador de cambios
        _cambios = 0

        'Captura el valor ingresado en el texbox
        Dim value As String = text_box_consulta_uea.Text

        'Mensaje que indica la busqueda en proceso
        label_estado.Text = String.Format("Realizando consulta de la UEA: {0}", value)
        label_estado.ForeColor = Color.Gray
        Me.Refresh()

        'Determina el tipo de busqueda
        If Not radio_button_codigo.Checked Then
            Dim table As System.Data.DataTable
            'Busqueda por nombre*
            Try
                'Consulta la busqueda por nombre en la base de datos
                table = cls_oracle.P_BUSCAR_UEA_POR_NOMBRE(value)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'Se agregan los datos obtenidos desde la base de datos a un data grid
            Dim form_ueas_consulta As New form_ueas_consulta_nombre
            form_ueas_consulta.dg_seleccionar_uea.DataSource = table

            'Configura la posicion donde se desplegara el formulario
            form_ueas_consulta.StartPosition = FormStartPosition.CenterParent
            form_ueas_consulta.ShowDialog()
        Else
            'Busqueda por codigo*
            coduea = value
        End If

        Try
            Dim RetVal = Shell(path_loader_proceso_buscar, 1)
            'Si se selecciono un codigo de uea
            If coduea <> "" Then
                'Consulta las propiedades de la uea en la base de datos
                Dim table = cls_oracle.P_GET_PROPIEDADES(coduea)

                'Si la consulta no devuelve registros se entiende que la uea no existe
                If table.Rows.count = 0 Then
                    cls_eval.KillProcess(loader_proceso_buscar)
                    MessageBox.Show("EL codigo de la UEA ingresada no existe.")
                    'Termina el proceso
                    Exit Sub
                End If

                'Se asginan los valores devueltos de la base de datos a cada textbox
                tbox_nombre_uea.Text = table.Rows(0).Item("NOMBRE").ToString()
                tbox_codigo_uea.Text = table.Rows(0).Item("CODIGO").ToString()
                v_nombre = table.Rows(0).Item("NOMBRE").ToString()

                Dim situacion = table.Rows(0).Item("SITUACION").ToString()

                '@START
                'En esta seccion se obtiene el valor de la zona y se configura su valor en el combobox
                'Si la uea no tiene una zona asignada se habilita el combobox para que el usuario pueda especificarla
                'ob = identificador de elementos obligatorios: {0: la uea ya tiene zona, 1: necesita especificar la zona}

                Dim ob = 0

                'Captura el valor de la zona geografica de la uea
                zona = table.Rows(0).Item("ZONA").ToString()
                cbox_zona_uea.Enabled = False
                If zona <> "" Or situacion = _situacion_extinguida Then
                    'Si existe la zona
                    cbox_zona_uea.SelectedIndex = cbox_zona_uea.FindStringExact(zona)
                    label_zona.ForeColor = SystemColors.ControlText
                Else
                    'Si la uea no tiene zona
                    cbox_zona_uea.SelectedIndex = -1
                    'cbox_zona_uea.Enabled = True
                    label_zona.ForeColor = Drawing.Color.Red
                    ob = 1
                    '_cambios = _cambios + 1
                End If
                '@END

                'Se asginan los valores devueltos a cada textbox
                tbox_sustancia_uea.Text = table.Rows(0).Item("NATURALEZA").ToString()
                tbox_estado_uea.Text = table.Rows(0).Item("ESTADO").ToString()
                tbox_situacion_uea.Text = situacion

                'op = identificador de elementos opcionales: {0: no se necesita especificar, 1: puede especificarlo}
                Dim op = 0
                Dim radio = table.Rows(0).Item("RADIO").ToString()
                cbox_radio_uea.Enabled = False
                If radio <> "" Or situacion = _situacion_extinguida Then
                    'Si la uea tiene radio
                    cbox_radio_uea.SelectedIndex = cbox_radio_uea.FindStringExact(radio)
                    label_radio.ForeColor = SystemColors.ControlText
                Else
                    'Si la uea no tiene radio
                    cbox_radio_uea.SelectedIndex = -1
                    'cbox_radio_uea.Enabled = True
                    label_radio.ForeColor = Color.Green
                    op = 1
                    '_cambios = _cambios + 1
                End If

                tbox_este_uea.Text = table.Rows(0).Item("ESTE").ToString()
                tbox_este_uea.ReadOnly = True
                label_este.ForeColor = SystemColors.ControlText
                If tbox_este_uea.Text = "" And situacion <> _situacion_extinguida Then
                    'Si la uea no tiene coordenada este del circulo
                    'tbox_este_uea.ReadOnly = False
                    label_este.ForeColor = Color.Green
                    op = 1
                    '_cambios = _cambios + 1
                End If

                tbox_norte_uea.Text = table.Rows(0).Item("NORTE").ToString()
                tbox_norte_uea.ReadOnly = True
                label_norte.ForeColor = SystemColors.ControlText
                If tbox_norte_uea.Text = "" And situacion <> _situacion_extinguida Then
                    'Si la uea no tiene coordenada norte del circulo
                    'tbox_norte_uea.ReadOnly = False
                    label_norte.ForeColor = Color.Green
                    op = 1
                    '_cambios = _cambios + 1
                End If

                'Mensajes a usuario informando sobre los datos oblkigatorios y opcionales que
                'necesita editar
                If ob = 1 Then
                    label_obligatorio.Visible = True
                Else
                    label_obligatorio.Visible = False
                End If

                If op = 1 Then
                    label_opcional.Visible = True
                Else
                    label_opcional.Visible = False
                End If

                'Dim tabla_demarcacion = cls_oracle.P_GET_DEMARCACION(coduea)
                tabla_demarcacion = cls_oracle.P_GET_DEMARCACION(coduea)
                dgrid_demarcacion.DataSource = tabla_demarcacion

                '''''''''''

                Dim nm_dist As String = ""
                Dim nm_prov As String = ""
                Dim nm_depa As String = ""

                ListaDep.Clear()
                ListaProv.Clear()
                ListaDist.Clear()

                If tabla_demarcacion.Rows.Count >= 1 Then
                    For contador As Integer = 0 To tabla_demarcacion.Rows.Count - 1
                        nm_dist = tabla_demarcacion.Rows(contador).Item("NM_DIST")
                        nm_prov = tabla_demarcacion.Rows(contador).Item("NM_PROV")
                        nm_depa = tabla_demarcacion.Rows(contador).Item("NM_DEPA")

                        If nm_dist = "FUERA DEL PERU" Or nm_prov = "FUERA DEL PERU" Or nm_depa = "FUERA DEL PERU" Then

                        Else
                            ListaDist.Add(nm_dist)
                            ListaProv.Add(nm_prov)
                            ListaDep.Add(nm_depa)
                        End If
                    Next contador

                    Dim lista_dep7 As String = ""
                    Dim arrDep = ListaDep.Distinct.Reverse.ToArray
                    For j As Integer = 0 To arrDep.length - 1
                        lista_dep7 = arrDep(j).ToString & "," & lista_dep7
                    Next
                    lista_dep7 = lista_dep7.TrimEnd(",")
                    lista_depa = lista_dep7

                    Dim lista_prov7 As String = ""
                    Dim arrProv = ListaProv.Distinct.Reverse.ToArray
                    For j As Integer = 0 To arrProv.length - 1
                        lista_prov7 = arrProv(j).ToString & "," & lista_prov7
                    Next
                    lista_prov7 = lista_prov7.TrimEnd(",")
                    lista_prov = lista_prov7

                    Dim lista_dist7 As String = ""
                    Dim arrDist = ListaDist.Distinct.Reverse.ToArray
                    For j As Integer = 0 To arrDist.length - 1
                        lista_dist7 = arrDist(j).ToString & "," & lista_dist7
                    Next
                    lista_dist7 = lista_dist7.TrimEnd(",")
                    lista_dist = lista_dist7
                Else

                End If
                '''''''''''

                

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '''''''''''' Se inserta cambio ''''''''''''''''''''''''


                If statusxcambio = "0" Then
                    'Dim tabla_integrantes = cls_oracle.P_GET_INTEGRANTES(coduea)
                    tabla_integrantes = cls_oracle.P_GET_INTEGRANTES(coduea)
                    Dim tablatemp As String = cls_oracle.P_INS_INTEGRANTES_UEA(coduea)
                    tabla_integrantes = cls_oracle.P_SEL_INTEGRANTESXUEA(coduea)

                Else
                    tabla_integrantes = cls_oracle.P_SEL_INTEGRANTESXUEA(coduea)

                End If

                dgrid_integrantes.DataSource = tabla_integrantes

                Dim result As Object
                result = tabla_integrantes.Compute("SUM(AREA)", "")

                If IsNumeric(result) Then
                    v_areaUEA = result
                End If

                label_nro_integrantes.Text = dgrid_integrantes.RowCount - 1
                label_nro_integrantes.Visible = True

                If zona = "" Or situacion = _situacion_extinguida Then
                    btn_graficar.Enabled = False
                    tblpanel_reporte.Enabled = False
                Else
                    btn_graficar.Enabled = True
                    tblpanel_reporte.Enabled = True
                End If
            End If

            'Mensaje que muestra el fin del proceso
            label_estado.Text = "Consulta realizada con exito"
            label_estado.ForeColor = Color.Black
            Me.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
            label_estado.Text = ex.Message
            label_estado.ForeColor = Color.Red
        Finally
            cls_eval.KillProcess(loader_proceso_buscar)
        End Try


        Dim dm_integrantes As New DataTable
        dm_integrantes = cls_oracle.P_SEL_DM_INTEGRANTES(coduea)

        Me.dgvExcluir.DataSource = dm_integrantes

        Me.btnaddExcluir.Enabled = True

        Me.btnaddIncluir.Visible = True
        Me.btnaddIncluir.Enabled = False

        Me.lstExcluir.Enabled = True
        Me.lstIncluir.Enabled = True
        Me.btnbuscar.Enabled = True
        Me.btnlimpiarInclusion.Visible = True
        Me.btnlimpiarInclusion.Enabled = False
        Me.btnlimpiarExclusion.Visible = True
        Me.btnlimpiarExclusion.Enabled = False

        'Me.dgvIncluir.Rows.Clear()

        Me.cboDM.Enabled = True
        cboDM.Items.Add("Código")
        cboDM.Items.Add("Nombre")
        cboDM.SelectedIndex = 0

    End Sub

    Private Function togle_read_only(tbox As TextBox)
        If tbox.ReadOnly Then
            tbox.ReadOnly = False
        Else
            tbox.ReadOnly = True
        End If
    End Function


    'Permite la ejecucion de procesos bat para hacer llamadas a Python
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

        Dim json As Linq.JObject = Linq.JObject.Parse(output)
        Return json

    End Function

    ''Configuracion del textbox para ejecutar la busqueda al presionar la tecla Enter
    'Private Sub text_box_consulta_uea_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles text_box_consulta_uea.PreviewKeyDown
    '    'Si se presiona la tecla enter y el boton buscar esta habilitado
    '    If e.KeyCode = Keys.Enter And button_buscar.Enabled Then
    '        'Llama a la funcion buscar 
    '        Call button_buscar_Click(sender, e)
    '    End If
    'End Sub

    'Configuracion del textbox para ejecutar la busqueda al presionar la tecla Enter
    Private Sub text_box_consulta_uea_KeyPress(sender As Object, e As KeyPressEventArgs) Handles text_box_consulta_uea.KeyPress
        'Si se presiona la tecla enter y el boton buscar esta habilitado
        If e.KeyChar = Chr(13) And button_buscar.Enabled Then
            'Llama a la funcion buscar 
            Call button_buscar_Click(sender, e)
        End If
    End Sub

    'Configuracion paraa cerrar la ventana al presionar la tecla escape
    Private Sub ueas_form_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        'Si se presiona la tecla escape
        If e.KeyChar = Chr(27) Then
            Me.Close()
        End If
    End Sub

    'Configuracion del texbox para habilitar el botton de busqueda cuando se supere la cantidad minima de caracteres necesarios
    'Se determina automaticamente cuando se requiere ingresar la busqueda por nombre o por codigo, teniendo en cuenta
    'que un codigo de UEA solo puede tener un caracter alfabetico
    Private Sub text_box_consulta_uea_TextChanged(sender As Object, e As EventArgs) Handles text_box_consulta_uea.TextChanged
        If text_box_consulta_uea.Text.Length > _min_numero_caracteres_busqueda Then
            button_buscar.Enabled = True
        Else
            button_buscar.Enabled = False
        End If

        radio_button_nombre.AutoCheck = False
        radio_button_codigo.AutoCheck = False

        Dim letras = Regex.Replace(text_box_consulta_uea.Text, "\d", "")

        If letras.ToString.Length > 1 Then
            radio_button_nombre.Checked = True
            radio_button_codigo.Checked = False
        Else
            radio_button_nombre.Checked = False
            radio_button_codigo.Checked = True
        End If

        radio_button_nombre.AutoCheck = True
        radio_button_codigo.AutoCheck = True
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.Close()
    End Sub

    Private Sub cbox_zona_uea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_zona_uea.SelectedIndexChanged
        btn_graficar.Enabled = True
        tblpanel_reporte.Enabled = True
        _cambios = _cambios + 1
    End Sub

    Private Sub form_ueas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Application.AddMessageFilter(filter)
        Me.KeyPreview = True
    End Sub

    'Private Sub filter_DoubleClick() Handles filter.DoubleClick
    '    Try
    '        Dim rc_zona As Rectangle = cbox_zona_uea.RectangleToScreen(cbox_zona_uea.ClientRectangle)
    '        Dim rc_radio As Rectangle = cbox_radio_uea.RectangleToScreen(cbox_radio_uea.ClientRectangle)
    '        Dim rc_este As Rectangle = tbox_este_uea.RectangleToScreen(tbox_este_uea.ClientRectangle)
    '        Dim rc_norte As Rectangle = tbox_norte_uea.RectangleToScreen(tbox_norte_uea.ClientRectangle)

    '        Dim values As New List(Of String) From {"", _situacion_extinguida}

    '        If Not values.Contains(tbox_situacion_uea.Text) Then

    '            If rc_zona.Contains(System.Windows.Forms.Cursor.Position) AndAlso Not cbox_zona_uea.Enabled Then
    '                cbox_zona_uea.Enabled = True
    '                cbox_zona_uea.Focus()
    '            End If

    '            If rc_radio.Contains(System.Windows.Forms.Cursor.Position) AndAlso Not cbox_radio_uea.Enabled Then
    '                cbox_radio_uea.Enabled = True
    '                cbox_radio_uea.Focus()
    '            End If

    '            If rc_este.Contains(System.Windows.Forms.Cursor.Position) AndAlso tbox_este_uea.ReadOnly Then
    '                tbox_este_uea.ReadOnly = False
    '                tbox_este_uea.Focus()
    '            End If

    '            If rc_norte.Contains(System.Windows.Forms.Cursor.Position) AndAlso tbox_norte_uea.ReadOnly Then
    '                tbox_norte_uea.ReadOnly = False
    '                tbox_norte_uea.Focus()
    '            End If
    '        End If
    '    Catch
    '    End Try

    'End Sub

    'Private Class MyFilter
    '    Implements IMessageFilter

    '    Public Event DoubleClick()
    '    Private Const WM_LBUTTONDBLCLK As Integer = &H203

    '    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
    '        Select Case m.Msg
    '            Case WM_LBUTTONDBLCLK
    '                RaiseEvent DoubleClick()

    '        End Select
    '        Return False
    '    End Function

    'End Class

    Private Sub btn_graficar_Click(sender As Object, e As EventArgs) Handles btn_graficar.Click

        statusxcambio = "0"

        Dim pform As New Frm_Eval_segun_codigo
        Dim control_visualizacion As Integer

        _activar_btn_reporte_circulo_uea = 0

        v_repocirculo = 0

        tipo_seleccion = "OP_34"
        GloInt_Opcion = 0
        Select Case tipo_seleccion
            Case "OP_34" 'UEAs
                conta_botones_evaluacion = conta_botones_evaluacion + 1
                GloInt_Opcion = 1
        End Select

        Try
            'Me.WindowState = FormWindowState.Minimized
            Me.Hide()

            coduea = tbox_codigo_uea.Text.ToString
            zona = cbox_zona_uea.Text
            Dim radio_tmp As String = cbox_radio_uea.Text

            If radio_tmp <> "Ninguno" And radio_tmp <> "" Then
                radio = Convert.ToDecimal(radio_tmp) / 1000.0
            End If

            Dim este_tmp As String = tbox_este_uea.Text.ToString
            If este_tmp <> "" Then
                este = Convert.ToDecimal(este_tmp)
            End If

            Dim norte_tmp As String = tbox_norte_uea.Text.ToString
            If norte_tmp <> "" Then
                norte = Convert.ToDecimal(norte_tmp)
            End If

            'If _cambios > 4 Then
            If _cambios > 0 Then
                Dim res As String = cls_oracle.P_UPD_PROPIEDADES_UEA(coduea, radio, zona, este, norte)
            End If

            '_cambios = 0

            Dim RetVal = Shell(path_loader_proceso_general, 1)
            Dim params = String.Format(_parametros, tbox_codigo_uea.Text.ToString, cbox_zona_uea.Items(cbox_zona_uea.SelectedIndex).ToString(), gstrUsuarioAcceso, gstrUsuarioClave, _fn_graficar)
            Dim json As Linq.JObject = ejecutar_procesos(_bat_ueas, params)
            Dim estado As Integer = json.SelectToken("estado")

            If estado = 1 Then
                '@Ini
                '@Cesar
                'Espacio donde se agregan los shapefiles generados como capas dentro de ArcMap     
                fecha_archi = DateTime.Now.Ticks.ToString()
                v_areaextUEA = 0

                Dim v_areac, v_areaex, v_areatot_integra, v_area_inclusion, v_area_exclusion As String
                Dim v_codiDM, v_tipmov As String

                'in_DataUEA = cls_oracle.FT_INCLUIDO_EXCLUIDO_UEA(coduea, "Z")    'versión old
                'ex_DataUEA = cls_oracle.FT_INCLUIDO_EXCLUIDO_UEA(coduea, "S")

                'in_DataUEA = cls_oracle.FT_INCLUIDO_EXCLUIDO_UEAS(coduea, "Z")   'version actual
                'ex_DataUEA = cls_oracle.FT_INCLUIDO_EXCLUIDO_UEAS(coduea, "S")

                in_DataUEA = cls_oracle.P_INCLUIDO_EXCLUIDO_UEAS("Z")  ' Cambio 24/12/2020
                ex_DataUEA = cls_oracle.P_INCLUIDO_EXCLUIDO_UEAS("S")

                'Try
                '    v_areatot_integra = cls_oracle.FT_areatot_integrantesUEA(coduea, "ALL")  ' --- area de integrantes
                '    v_areaUEA = Convert.ToSingle(v_areatot_integra.Replace(",", "."))
                'Catch ex As Exception

                'End Try

                'Try
                '    v_area_inclusion = cls_oracle.FT_areatot_integrantesUEA(coduea, "Z")  ' --- area de incluidos
                '    v_areainclusionUEA = Convert.ToSingle(v_area_inclusion.Replace(",", "."))
                'Catch ex As Exception

                'End Try

                'Try
                '    v_area_exclusion = cls_oracle.FT_areatot_integrantesUEA(coduea, "S")  ' --- area de excluidos
                '    v_areaexclusionUEA = Convert.ToSingle(v_area_exclusion.Replace(",", "."))
                'Catch ex As Exception

                'End Try

                Try
                    v_areac = cls_oracle.FT_area_circuloUEA(coduea)     ' --- area del circulo
                    v_area_circuloUEA = Convert.ToSingle(v_areac.Replace(",", "."))
                Catch ex As Exception

                End Try

                'Try
                '    v_areaex = cls_oracle.FT_area_externaUEA(coduea)     ' --- area externa
                '    v_areaextUEA = Convert.ToSingle(v_areaex.Replace(",", "."))
                'Catch ex As Exception

                'End Try

                Dim area_ext_value As String = json.SelectToken("result").SelectToken("area_ext_value")
                If area_ext_value IsNot Nothing Then
                    v_areaextUEA = area_ext_value / 10000
                End If


                '''''''''''  INTEGRANTES UEA  '''''''''''
                '''''''''''''''''''''''''''''''''''''''''
                cls_planos.CambiaADataView(m_application)
                cls_Catastro.Limpiar_Texto_Pantalla(m_application)
                cls_Catastro.Borra_Todo_Feature("", m_application)

                For contador As Integer = 0 To tabla_integrantes.Rows.Count - 1
                    v_codiDM = tabla_integrantes.Rows(contador).Item("CODIGOU")
                    v_tipmov = tabla_integrantes.Rows(contador).Item("TIP_MOV")

                    If v_tipmov = "C" Then
                        If contador = 0 Then
                            lista_integraUEA = "CODIGOU =  '" & v_codiDM & "'"
                        ElseIf contador > 0 Then
                            lista_integraUEA = lista_integraUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                        End If
                    End If
                Next

                pMxDoc = m_application.Document
                pMap = pMxDoc.FocusMap
                caso_consulta = "CATASTRO MINERO"
                If pMap.Name <> "CATASTRO MINERO" Then
                    cls_planos.buscaadataframe(caso_consulta, False)
                    If valida = False Then
                        pMap.Name = "CATASTRO MINERO"
                        pMxDoc.UpdateContents()
                    End If
                    cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
                    pMxDoc.UpdateContents()
                End If
                cls_eval.Eliminadataframe(caso_consulta)
                cls_planos.buscaadataframe("CATASTRO MINERO", False)
                If valida = False Then
                    pMap.Name = "CATASTRO MINERO"
                End If
                cls_Catastro.Actualizar_DM(zona)
                pMxDoc.UpdateContents()

                cls_Catastro.Borra_Todo_Feature("", m_application)
                cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                'Agregando capas a la vista del mapa
                Dim influencia As String = json.SelectToken("result").SelectToken("influencia")
                If influencia IsNot Nothing Then
                    cls_Catastro.Add_ShapeFile(influencia, m_application)
                End If

                Dim integrantes As String = json.SelectToken("result").SelectToken("integrantes")
                If integrantes IsNot Nothing Then
                    cls_Catastro.Add_ShapeFile(integrantes, m_application)
                End If

                cls_eval.consultacapaDM("integraUEA", "integraUEA", "integrantes_" & coduea)
                cls_Catastro.Expor_Tema("integraUEA", sele_denu, m_application)
                cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)

                cls_Catastro.ClearLayerSelection(pFeatureLayer)
                cls_Catastro.Quitar_Layer(integrantes, m_application)

                'cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                'cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                'Aplicando el zoom a la capa necesaria
                If influencia IsNot Nothing Then
                    cls_Catastro.Zoom_to_Layer(influencia)
                Else
                    cls_Catastro.Zoom_to_Layer("integraUEA")
                End If

                cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                'cls_Prueba.Poligono_Color_UEA("integrantes_" & coduea, tabla_integrantes, glo_pathStyle & "\Catastro_UEA.style", "TIP_MOV", "", m_application)
                cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                If influencia IsNot Nothing Then
                    cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                End If

                ' AREA EXTERNA - PLANO ANALISIS
                If v_areaextUEA > 0 Then

                    arch_cata = ""
                    caso_consulta = "ANALISIS UEA"
                    cls_eval.adicionadataframe("ANALISIS UEA")
                    cls_eval.activadataframe("ANALISIS UEA")
                    cls_Catastro.Actualizar_DM(zona)
                    pMxDoc.UpdateContents()

                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                    For contador As Integer = 0 To tabla_integrantes.Rows.Count - 1
                        v_codiDM = tabla_integrantes.Rows(contador).Item("CODIGOU")
                        v_tipmov = tabla_integrantes.Rows(contador).Item("TIP_MOV")

                        If v_tipmov <> "S" Then
                            If contador = 0 Then
                                lista_integrainUEA = "CODIGOU =  '" & v_codiDM & "'"
                            ElseIf contador > 0 Then
                                lista_integrainUEA = lista_integrainUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                            End If
                        End If
                    Next

                    cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(influencia, m_application)
                    End If

                    cls_Catastro.Add_ShapeFile(integrantes, m_application)

                    Dim area_ext As String = json.SelectToken("result").SelectToken("area_ext_geom")
                    If area_ext IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(area_ext, m_application)
                    End If

                    cls_eval.agregacampotema_tpm("area_ext_" & coduea, "Area_Externa")
                    cls_Catastro.Update_Value_Layer(m_application, "area_ext_" & coduea, "Area_Externa")

                    cls_eval.consultacapaDM("integrainUEA", "integrainUEA", "integrantes_" & coduea)
                    cls_Catastro.Expor_Tema("integrainUEA", sele_denu, m_application)
                    cls_Catastro.Add_ShapeFile_tmp("integrainUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("integrainUEA" & fecha_archi, m_application)

                    cls_Catastro.ClearLayerSelection(pFeatureLayer)
                    cls_Catastro.Quitar_Layer(integrantes, m_application)

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "integrainUEA")
                    cls_Catastro.rotulatexto_dm("integrainUEA", m_application)

                    'Aplicando el zoom a la capa necesaria
                    If influencia IsNot Nothing Then
                        cls_Catastro.Zoom_to_Layer(influencia)
                    Else
                        cls_Catastro.Zoom_to_Layer("integrainUEA")
                    End If

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "area_ext_" & coduea)
                    cls_Catastro.rotulatexto_dm("area_ext_" & coduea, m_application)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    End If
                End If

                ' EXCLUSION
                If ex_DataUEA.Rows.Count > 0 Then

                    For contador As Integer = 0 To ex_DataUEA.Rows.Count - 1
                        v_codiDM = ex_DataUEA.Rows(contador).Item("CODIGOU")    'SE CAMBIA ---> CODIGO
                        If contador = 0 Then
                            lista_exclusionUEA = "CODIGOU =  '" & v_codiDM & "'"
                        ElseIf contador > 0 Then
                            lista_exclusionUEA = lista_exclusionUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                        End If
                    Next

                    arch_cata = ""
                    caso_consulta = "EXCLUSION UEA"
                    cls_eval.adicionadataframe("EXCLUSION UEA")
                    cls_eval.activadataframe("EXCLUSION UEA")
                    cls_Catastro.Actualizar_DM(zona)
                    pMxDoc.UpdateContents()

                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                    cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(influencia, m_application)
                    End If

                    cls_Catastro.Add_ShapeFile(integrantes, m_application)

                    cls_eval.consultacapaDM("ExclusionUEA", "ExclusionUEA", "integrantes_" & coduea)
                    cls_Catastro.Expor_Tema("ExcluUEA", sele_denu, m_application)
                    cls_Catastro.ClearLayerSelection(pFeatureLayer)
                    cls_Catastro.Quitar_Layer(integrantes, m_application)

                    cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)

                    cls_Catastro.Add_ShapeFile_tmp("ExcluUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("ExcluUEA" & fecha_archi, m_application)

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                    cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    End If

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "ExclusionUEA")
                    cls_Catastro.rotulatexto_dm("ExclusionUEA", m_application)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Zoom_to_Layer(influencia)
                    Else
                        cls_Catastro.Zoom_to_Layer("integraUEA")
                    End If
                End If

                ' INCLUSION
                If in_DataUEA.Rows.Count > 0 Then
                    arch_cata = ""
                    caso_consulta = "INCLUSION UEA"
                    cls_eval.adicionadataframe("INCLUSION UEA")
                    cls_eval.activadataframe("INCLUSION UEA")
                    cls_Catastro.Actualizar_DM(zona)
                    pMxDoc.UpdateContents()

                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                    For contador As Integer = 0 To in_DataUEA.Rows.Count - 1
                        v_codiDM = in_DataUEA.Rows(contador).Item("CODIGOU")    'SE CAMBIA ---> CODIGO
                        If contador = 0 Then
                            lista_inclusionUEA = "CODIGOU =  '" & v_codiDM & "'"
                        ElseIf contador > 0 Then
                            lista_inclusionUEA = lista_inclusionUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                        End If
                    Next

                    cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(influencia, m_application)
                    End If

                    cls_Catastro.Add_ShapeFile(integrantes, m_application)

                    If statusxinclusion = "0" Then
                        cls_eval.consultacapaDM("InclusionUEA", "InclusionUEA", "integrantes_" & coduea)
                        cls_Catastro.Expor_Tema("IncluUEA", sele_denu, m_application)
                        cls_Catastro.ClearLayerSelection(pFeatureLayer)
                        cls_Catastro.Quitar_Layer(integrantes, m_application)

                        cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)

                        cls_Catastro.Add_ShapeFile_tmp("incluUEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("incluUEA" & fecha_archi, m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                        cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "InclusionUEA")
                        cls_Catastro.rotulatexto_dm("InclusionUEA", m_application)
                    Else
                        cls_eval.consultacapaDM("inclu_UEA", "inclu_UEA", "integrantes_" & coduea)
                        cls_Catastro.Expor_Tema("inclu_UEA", sele_denu, m_application)
                        cls_Catastro.ClearLayerSelection(pFeatureLayer)
                        cls_Catastro.Quitar_Layer(integrantes, m_application)

                        cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)

                        cls_Catastro.Add_ShapeFile_tmp("inclu_UEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("inclu_UEA" & fecha_archi, m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                        cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "inclusion_UEA")
                        cls_Catastro.rotulatexto_dm("inclu_UEA", m_application)
                    End If

                    If influencia IsNot Nothing Then
                        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    End If

                    If influencia IsNot Nothing Then
                        cls_Catastro.Zoom_to_Layer(influencia)
                    Else
                        cls_Catastro.Zoom_to_Layer("integraUEA")
                    End If

                    'If _integrado = 1 Then

                    '    cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                    '    cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)
                    '    cls_Catastro.Quitar_Layer(integrantes, m_application)

                    '    cls_Catastro.Add_ShapeFile_tmp("InclusionUEA" & fecha_archi, m_application)    ' IncluUEA
                    '    cls_Catastro.Add_ShapeFile("InclusionUEA" & fecha_archi, m_application)        ' IncluUEA

                    '    cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                    '    cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                    '    cls_Catastro.Color_Poligono_Simple_2(m_application, "InclusionUEA")
                    '    cls_Catastro.rotulatexto_dm("InclusionUEA", m_application)

                    '    If influencia IsNot Nothing Then
                    '        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    '    End If

                    '    If influencia IsNot Nothing Then
                    '        cls_Catastro.Zoom_to_Layer(influencia)
                    '    Else
                    '        cls_Catastro.Zoom_to_Layer("integraUEA")
                    '    End If
                    'Else
                    '    For contador As Integer = 0 To tabla_integrantes.Rows.Count - 1
                    '        v_codiDM = tabla_integrantes.Rows(contador).Item("CODIGOU")
                    '        v_tipmov = tabla_integrantes.Rows(contador).Item("TIP_MOV")

                    '        If v_tipmov = "C" Then
                    '            If contador = 0 Then
                    '                lista_integraUEA = "CODIGOU =  '" & v_codiDM & "'"
                    '            ElseIf contador > 0 Then
                    '                lista_integraUEA = lista_integraUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                    '            End If
                    '        End If
                    '    Next

                    '    'SE AGREGA PARA PROBAR
                    '    cls_Catastro.Actualizar_DM(zona)
                    '    cls_Catastro.Add_ShapeFile_tmp("integrantes_" & coduea, m_application)
                    '    cls_Catastro.Add_ShapeFile("integrantes_" & coduea, m_application)

                    '    cls_eval.consultacapaDM("integraUEA", "integraUEA", "integrantes_" & coduea)
                    '    cls_Catastro.Expor_Tema("integraUEA", sele_denu, m_application)
                    '    cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                    '    cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)
                    '    cls_Catastro.ClearLayerSelection(pFeatureLayer)
                    '    cls_Catastro.Quitar_Layer(integrantes, m_application)

                    '    cls_Catastro.Add_ShapeFile_tmp("IncluUEA" & fecha_archi, m_application)
                    '    cls_Catastro.Add_ShapeFile("IncluUEA" & fecha_archi, m_application)

                    '    cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                    '    cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                    '    cls_Catastro.Color_Poligono_Simple_2(m_application, "InclusionUEA")
                    '    cls_Catastro.rotulatexto_dm("InclusionUEA", m_application)

                    '    If influencia IsNot Nothing Then
                    '        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    '    End If

                    '    If influencia IsNot Nothing Then
                    '        cls_Catastro.Zoom_to_Layer(influencia)
                    '    Else
                    '        cls_Catastro.Zoom_to_Layer("integraUEA")
                    '    End If
                    'End If
                End If

                ' INTEGRADO
                If in_DataUEA.Rows.Count > 0 Then

                    For contador As Integer = 0 To tabla_integrantes.Rows.Count - 1
                        v_codiDM = tabla_integrantes.Rows(contador).Item("CODIGOU")
                        v_tipmov = tabla_integrantes.Rows(contador).Item("TIP_MOV")

                        If v_tipmov <> "S" Then
                            If contador = 0 Then
                                lista_integradoUEA = "CODIGOU =  '" & v_codiDM & "'"
                            ElseIf contador > 0 Then
                                lista_integradoUEA = lista_integradoUEA & " OR " & "CODIGOU =  '" & v_codiDM & "'"
                            End If
                        End If
                    Next

                    arch_cata = ""
                    caso_consulta = "INTEGRADO UEA"
                    cls_eval.adicionadataframe("INTEGRADO UEA")
                    cls_eval.activadataframe("INTEGRADO UEA")
                    cls_Catastro.Actualizar_DM(zona)
                    pMxDoc.UpdateContents()

                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                    cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(influencia, m_application)
                    End If

                    cls_Catastro.Add_ShapeFile(integrantes, m_application)

                    cls_eval.consultacapaDM("integradoUEA", "integradoUEA", "integrantes_" & coduea)
                    cls_Catastro.Expor_Tema("integradoUEA", sele_denu, m_application)
                    cls_Catastro.Add_ShapeFile_tmp("integradoUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("integradoUEA" & fecha_archi, m_application)
                    cls_Catastro.ClearLayerSelection(pFeatureLayer)
                    cls_Catastro.Quitar_Layer(integrantes, m_application)

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "integradoUEA")
                    cls_Catastro.rotulatexto_dm("integradoUEA", m_application)

                    'Aplicando el zoom a la capa necesaria
                    If influencia IsNot Nothing Then
                        cls_Catastro.Zoom_to_Layer(influencia)
                    Else
                        cls_Catastro.Zoom_to_Layer("integradoUEA")
                    End If

                    If influencia IsNot Nothing Then
                        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    End If
                End If

                'INCLUIDOS Y EXCLUIDOS  
                If in_DataUEA.Rows.Count > 0 And ex_DataUEA.Rows.Count > 0 Then

                    arch_cata = ""
                    caso_consulta = "INCLUSION_EXCLUSION UEA"
                    cls_eval.adicionadataframe("INCLUSION_EXCLUSION UEA")
                    cls_eval.activadataframe("INCLUSION_EXCLUSION UEA")
                    cls_Catastro.Actualizar_DM(zona)
                    pMxDoc.UpdateContents()

                    cls_Catastro.Borra_Todo_Feature("", m_application)
                    cls_Catastro.Limpiar_Texto_Pantalla(m_application)

                    cls_eval.AddLayerFromFile_demarcacion(m_application, zona)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Add_ShapeFile(influencia, m_application)
                    End If

                    cls_Catastro.Add_ShapeFile_tmp("integraUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("integraUEA" & fecha_archi, m_application)

                    If influencia IsNot Nothing Then
                        cls_Catastro.Color_Poligono_Simple_2(m_application, "influencia_" & coduea)
                    End If

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "integraUEA")
                    cls_Catastro.rotulatexto_dm("integraUEA", m_application)

                    cls_Catastro.Add_ShapeFile_tmp("ExcluUEA" & fecha_archi, m_application)
                    cls_Catastro.Add_ShapeFile("ExcluUEA" & fecha_archi, m_application)

                    cls_Catastro.Color_Poligono_Simple_2(m_application, "ExclusionUEA")
                    cls_Catastro.rotulatexto_dm("ExclusionUEA", m_application)

                    If statusxinclusion = "0" Then
                        cls_Catastro.Add_ShapeFile_tmp("IncluUEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("IncluUEA" & fecha_archi, m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "InclusionUEA")
                        cls_Catastro.rotulatexto_dm("InclusionUEA", m_application)
                    Else
                        cls_Catastro.Add_ShapeFile_tmp("inclu_UEA" & fecha_archi, m_application)
                        cls_Catastro.Add_ShapeFile("inclu_UEA" & fecha_archi, m_application)

                        cls_Catastro.Color_Poligono_Simple_2(m_application, "inclusion_UEA")
                        cls_Catastro.rotulatexto_dm("inclu_UEA", m_application)
                    End If

                    If influencia IsNot Nothing Then
                        cls_Catastro.Zoom_to_Layer(influencia)
                    Else
                        cls_Catastro.Zoom_to_Layer("integraUEA")
                    End If
                    statusxinclusion = "0"
                End If

                arch_cata = ""
                caso_consulta = "CATASTRO MINERO"
                cls_eval.activadataframe("CATASTRO MINERO")

                'pform.BOTON_MENU(True, m_application)

                clsBarra.BOTON_MENU(True, m_application)

                'controlador de visualizacion del formulario actual
                'control_visualizacion = 0
                '@End
            Else
                Dim msg_error As String = json.SelectToken("msg")
                Throw New System.Exception(msg_error)
            End If
        Catch ex As Exception
            'control_visualizacion = 1
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            'cls_eval.KillProcess(loader_proceso_general)
            'Me.Show()
            _cambios = 0
            cls_eval.KillProcess(loader_proceso_general)
            Me.Show()
            'If control_visualizacion = 0 Then
            'Me.Close()
            'Else

            'Me.WindowState = FormWindowState.Maximized
            'End If
        End Try

    End Sub

    Private Sub pbox_reporte_otras_ueas_Click(sender As Object, e As EventArgs) Handles pbox_reporte_otras_ueas.Click
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)

            'If _cambios > 4 Then

            '@Daniel-cambio
            If _cambios = 1 Then
                Dim radio_tmp As String = cbox_radio_uea.Text
                Dim radio As Double
                Dim este As Double
                Dim norte As Double

                If radio_tmp <> "Ninguno" And radio_tmp <> "" Then
                    radio = Convert.ToDecimal(radio_tmp) / 1000.0
                End If

                Dim este_tmp As String = tbox_este_uea.Text.ToString
                If este_tmp <> "" Then
                    este = Convert.ToDecimal(este_tmp)
                End If

                Dim norte_tmp As String = tbox_norte_uea.Text.ToString
                If norte_tmp <> "" Then
                    norte = Convert.ToDecimal(norte_tmp)
                End If

                Dim res As String = cls_oracle.P_UPD_PROPIEDADES_UEA(coduea, radio, zona, este, norte)
            End If

            '_cambios = 0

            'Dim params = String.Format(_parametros, tbox_codigo_uea.Text.ToString, cbox_zona_uea.Items(cbox_zona_uea.SelectedIndex).ToString(), _fn_reporte_otras_uea)
            Dim params = String.Format(_parametros, tbox_codigo_uea.Text.ToString, cbox_zona_uea.Items(cbox_zona_uea.SelectedIndex).ToString(), gstrUsuarioAcceso, gstrUsuarioClave, _fn_reporte_otras_uea)
            Dim json As Linq.JObject = ejecutar_procesos(_bat_ueas, params)
            Dim estado As Integer = json.SelectToken("estado")
            If estado = 0 Then
                Dim msg_error As String = json.SelectToken("msg")
                Throw New System.Exception(msg_error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub pbox_reporte_uea_Click(sender As Object, e As EventArgs) Handles pbox_reporte_uea.Click
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)

            'If _cambios > 4 Then
            '@Daniel-cambio
            If _cambios = 1 Then
                Dim radio_tmp As String = cbox_radio_uea.Text
                Dim radio As Double
                Dim este As Double
                Dim norte As Double

                If radio_tmp <> "Ninguno" And radio_tmp <> "" Then
                    radio = Convert.ToDecimal(radio_tmp) / 1000.0
                End If

                Dim este_tmp As String = tbox_este_uea.Text.ToString
                If este_tmp <> "" Then
                    este = Convert.ToDecimal(este_tmp)
                End If

                Dim norte_tmp As String = tbox_norte_uea.Text.ToString
                If norte_tmp <> "" Then
                    norte = Convert.ToDecimal(norte_tmp)
                End If

                Dim res As String = cls_oracle.P_UPD_PROPIEDADES_UEA(coduea, radio, zona, este, norte)
            End If

            '_cambios = 0

            'Dim params = String.Format(_parametros, tbox_codigo_uea.Text.ToString, cbox_zona_uea.Items(cbox_zona_uea.SelectedIndex).ToString(), _fn_reporte_uea)
            Dim params = String.Format(_parametros, tbox_codigo_uea.Text.ToString, cbox_zona_uea.Items(cbox_zona_uea.SelectedIndex).ToString(), gstrUsuarioAcceso, gstrUsuarioClave, _fn_reporte_uea)
            Dim json As Linq.JObject = ejecutar_procesos(_bat_ueas, params)
            Dim estado As Integer = json.SelectToken("estado")
            If estado = 0 Then
                Dim msg_error As String = json.SelectToken("msg")
                Throw New System.Exception(msg_error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    'Private Sub cbox_radio_uea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_radio_uea.SelectedIndexChanged
    '    _cambios = _cambios + 1
    'End Sub

    'Private Sub tbox_este_uea_TextChanged(sender As Object, e As EventArgs) Handles tbox_este_uea.TextChanged
    '    _cambios = _cambios + 1
    'End Sub

    'Private Sub tbox_norte_uea_TextChanged(sender As Object, e As EventArgs) Handles tbox_norte_uea.TextChanged
    '    _cambios = _cambios + 1
    'End Sub

    '@Daniel-cambio

    Private Sub btn_activa_controles_editables_Click(sender As Object, e As EventArgs) Handles btn_activa_controles_editables.Click
        Dim values As New List(Of String) From {"", _situacion_extinguida}
        If values.Contains(tbox_situacion_uea.Text) Then
            Exit Sub
        End If

        Dim msg As String = "El sistema almacenará cualquier cambio realizado en las casillas editables"
        Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If response = DialogResult.OK Then
            _cambios = 1
        Else
            Exit Sub
        End If

        If cbox_zona_uea.Enabled = False Then
            cbox_zona_uea.Enabled = True
            cbox_zona_uea.Focus()
        End If

        If cbox_radio_uea.Enabled = False Then
            cbox_radio_uea.Enabled = True
            cbox_radio_uea.Focus()
        End If

        If tbox_este_uea.ReadOnly Then
            tbox_este_uea.ReadOnly = False
            tbox_este_uea.Focus()
        End If

        If tbox_norte_uea.ReadOnly Then
            tbox_norte_uea.ReadOnly = False
            tbox_norte_uea.Focus()
        End If
    End Sub

    Private Sub lnk_userguide_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnk_userguide.LinkClicked
        get_userguide()
    End Sub

    Private Sub btnaddExcluir_Click(sender As Object, e As EventArgs) Handles btnaddExcluir.Click

        For j As Integer = 0 To dgvExcluir.Rows.GetRowCount(DataGridViewElementStates.Selected) - 1
            lstExcluir.Items.Add(dgvExcluir.CurrentRow.Cells(0).Value.ToString)
        Next

        If lstExcluir.Items.Count > 0 Then
            Me.btngrabarExclusion.Enabled = True
            Me.btnlimpiarExclusion.Enabled = True
        Else
            Me.btnlimpiarExclusion.Enabled = False
        End If

    End Sub

    Private Sub btnlimpiarExclusion_Click(sender As Object, e As EventArgs) Handles btnlimpiarExclusion.Click
        Me.lstExcluir.Items.Clear()
        Me.btnlimpiarExclusion.Enabled = False
        Me.btngrabarExclusion.Enabled = False
    End Sub

    Private Sub btngrabarExclusion_Click(sender As Object, e As EventArgs) Handles btngrabarExclusion.Click

        Dim p As Integer = 0
        Dim q As Integer = 0
        While p < lstExcluir.Items.Count
            q = p + 1
            While q < lstExcluir.Items.Count
                If lstExcluir.Items(q) = lstExcluir.Items(p) Then
                    lstExcluir.Items.Remove(q)
                Else
                    q += 1
                End If
            End While
            p += 1
        End While

        For k As Integer = 0 To lstExcluir.Items.Count - 1
            Dim codi_ex As String = lstExcluir.Items(k).ToString
            ' llama a funcion delete que elimina dm de la  tabla:  data_cat.dm_integrantes_uea
            cls_oracle.P_DEL_INTEGRANTEUEA(codi_ex)

            statusxcambio = "1"
        Next

        Me.lstExcluir.Items.Clear()
        Me.btnlimpiarExclusion.Enabled = False
        Me.btngrabarExclusion.Enabled = False

        Me.button_buscar.Focus()

    End Sub

    Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click

        Dim dt_DM As New DataTable

        dt_DM = cls_oracle.F_OBTIENE_DM_UNIQUE(txtDM.Text, CType(cboDM.SelectedIndex, Integer) + 1)

        Me.dgvIncluir.DataSource = dt_DM
        txtDM.Text = ""

        Me.btnaddIncluir.Enabled = True

    End Sub

    Private Sub btnaddIncluir_Click(sender As Object, e As EventArgs) Handles btnaddIncluir.Click

        For j As Integer = 0 To dgvIncluir.Rows.GetRowCount(DataGridViewElementStates.Selected) - 1
            lstIncluir.Items.Add(dgvIncluir.CurrentRow.Cells(0).Value.ToString)
        Next

        If lstIncluir.Items.Count > 0 Then
            Me.btngrabarInclusion.Enabled = True
            Me.btnlimpiarInclusion.Enabled = True
        Else
            Me.btnlimpiarInclusion.Enabled = False
        End If

    End Sub

    Private Sub btngrabarInclusion_Click(sender As Object, e As EventArgs) Handles btngrabarInclusion.Click

        statusxinclusion = "1"

        Dim p As Integer = 0
        Dim q As Integer = 0
        While p < lstIncluir.Items.Count
            q = p + 1
            While q < lstIncluir.Items.Count
                If lstIncluir.Items(q) = lstIncluir.Items(p) Then
                    lstIncluir.Items.Remove(q)
                Else
                    q += 1
                End If
            End While
            p += 1
        End While

        For k As Integer = 0 To lstIncluir.Items.Count - 1
            Dim codi_in As String = lstIncluir.Items(k).ToString

            cls_oracle.P_INS_INCLUSIONXUEA(codi_in)
            statusxcambio = "1"
        Next

        Me.lstIncluir.Items.Clear()
        Me.btnlimpiarInclusion.Enabled = False
        Me.btngrabarInclusion.Enabled = False

        Me.button_buscar.Focus()

    End Sub

    Private Sub btnlimpiarInclusion_Click(sender As Object, e As EventArgs) Handles btnlimpiarInclusion.Click
        Me.lstIncluir.Items.Clear()
        Me.btnlimpiarInclusion.Enabled = False
        Me.btngrabarInclusion.Enabled = False
    End Sub

    Private Sub lstIncluir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstIncluir.SelectedIndexChanged

    End Sub
End Class