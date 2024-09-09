Imports System.Windows.Forms
Imports PORTAL_Clases
Imports System.Threading.Tasks
Imports System.Drawing
Imports Oracle
Imports ESRI.ArcGIS.Framework

Public Class form_mantenimiento_areas_restringidas
    Public papp As IApplication

    Private conn As New cls_oracle
    Private eval As New Cls_evaluacion
    Private opciones_cbox As New DataTable
    Dim _params As New List(Of Object)
    Dim opt As Object

    'Campos usados
    Private _FIELD_CG_CODIGO As String = "CODIGO"
    Private _FIELD_PROCESAR As String = "PROC"
    Private _FIELD_RE_CODEST As String = "CODEST"
    Private _FIELD_RE_SECUEN As String = "ID"
    Private _FIELD_RE_ARCGRA As String = "ARCHIVO"
    Private _FIELD_RE_MODREG As String = "MODREG"
    '@Cqh
    Private _FIELD_RE_INDICA As String = "MINERIA"
    '@CQH
    Private _FIELD_RE_ZONA As String = "ZONA"
    '@CQH

    Private _FIELD_RE_SELECT As String = "SEL."

    '@CQH
    Private _FIELD_RE_CLASE As String = "CLASE"
    'Valores de campos
    Private _ANDE = "DELETE"

    'Nombre de boton procesar
    Private _BT_NAME_PROC As String = "Procesar"
    Private _BT_NAME_CORR As String = "Corregido"

    'Estados
    Private _EST_NOPROCTEMP As Integer = 0
    Private _EST_PROCTEMP As Integer = 1
    Private _EST_NOPROCPROD As Integer = 2
    Private _EST_PROCPROD As Integer = 3
    Private _EST_PROCESADO As Integer = 4
    Private _EST_ERRTEMP As Integer = 96
    Private _EST_ERRPROD As Integer = 97
    Private _EST_LOADTEMP As Integer = 98
    Private _EST_LOADPROD As Integer = 99

    'Opciones de filtros
    Private _FILT_PROCTEMP As Integer = 1
    Private _FILT_PROCPROD As Integer = 2
    Private _FILT_PROCESADO As Integer = 3
    Private _FILT_ERRORES As Integer = 4
    Private _FILT_DATUM_WGS As Integer = 2
    Private _FILT_ARE_RESE As Integer = 1
    Private _FILT_ARE_URBA As Integer = 2

    'Filtros
    Public _OPT_DATUM As Integer = 1
    Public _OPT_ZONA As Integer = 2
    Public _OPT_FILTRO As Integer = 3
    Public _OPT_FILTRO_CREG As Integer = 4
    Public _OPT_FILTRO_ENV As Integer = 5
    Public _OPT_FILTRO_FEATURES As Integer = 6

    'Tags para radio buttons de configuracion
    Public _TAG_CURRENT As Integer = 3
    Public _TAG_EXEENDDAY As Integer = 4
    Public _TAG_NOEXE As Integer = 5

    'CQ
    Private Const Col_Sel_R As Integer = 0
    'Public _EXE_CTRL_PROC_PROD As Integer = "Procesar Produccion (manual)"
    'Public _EXE_CTRL_SET_CONFIG As Integer = "Aplicar cambios en configuración"


    Private Sub form_mantenimiento_areas_restringidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.FT_Registro(1, String.Format("{0}*Open", Me.Text))
        Me.pb_loader.Visible = False

        'Carga de opciones a los combobox
        'Opciones Datum
        opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_DATUM)
        get_option_cbox(Me.cb_datum, opciones_cbox)

        'Opciones Zona Geografica
        opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_ZONA)
        get_option_cbox(Me.cb_zona, opciones_cbox)

        'Opciones Filtro de procesamiento
        opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_FILTRO)
        get_option_cbox(Me.cb_filtro, opciones_cbox)

        opciones_cbox = conn.P_SEL_FILTRO_USUARIO()
        get_option_cbox(Me.cb_usuarios_are, opciones_cbox)

        Me.cb_usuarios_are.SelectedValue = gstrUsuarioAcceso.ToUpper()

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged


        Dim indexOfSelectedTab As Integer = TabControl1.SelectedIndex
        'Si se selecciona el segundo tabcontrol (1)
        If indexOfSelectedTab = 1 Then
            Try
                Shell(path_loader_proceso_buscar, 1)
                'Opciones Filtro de procesamiento
                opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_FILTRO_CREG)
                get_option_cbox(Me.cb_filtro_controlreg, opciones_cbox)

                opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_FILTRO_ENV)
                get_option_cbox(Me.cb_env, opciones_cbox)

                cambia_opciones_cbox_features()
            Catch ex As Exception
                MessageBox.Show(ex.Message, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                eval.KillProcess(loader_proceso_buscar)
            End Try
        ElseIf indexOfSelectedTab = 5 Then
            Me.rb_exeahora.Tag = _TAG_CURRENT
            Me.rb_exefindia.Tag = _TAG_EXEENDDAY
            Me.rb_noexe.Tag = _TAG_NOEXE
            Me.lbl_nreg_historico.Text = ""
            get_configuracion()

        ElseIf indexOfSelectedTab = 3 Then
            Try
                opciones_cbox = conn.P_SEL_OPCIONES_CBOX(_OPT_FILTRO_CREG)
                get_option_cbox(Me.cb_filtro_tipo, opciones_cbox)
            Catch
            End Try



        ElseIf indexOfSelectedTab = 4 Then
            Me.rb_exeahora.Tag = _TAG_CURRENT
            Me.rb_exefindia.Tag = _TAG_EXEENDDAY
            Me.rb_noexe.Tag = _TAG_NOEXE
            Me.lbl_nreg_historico.Text = ""
            get_configuracion()



        End If

    End Sub

    Private Sub btn_procesar_Click(sender As Object, e As EventArgs) Handles btn_procesar.Click
        Try
            Dim msg As String
            Dim res As String
            opt = Me.cb_filtro.SelectedValue
            If opt = _FILT_PROCTEMP Then

                msg = "Esta seguro que desea enviar la información a la base de datos temporal?"
                Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If response = DialogResult.Cancel Then
                    Exit Sub
                End If

                conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_procesamiento.Text, btn_procesar.Text))

                Shell(path_loader_proceso_general, 1)

                'Me.pb_loader.Visible = True
                Me.gb_control_visualizacion.Enabled = False
                Me.btn_procesar.Enabled = False
                Me.btn_select.Enabled = False
                Me.btn_unselect.Enabled = False
                Me.gb_filtro.Enabled = False
                Me.dgrid_are_procesamiento.Enabled = False


                Me.shapeToFeatureTMP()
                cb_filtro_SelectedIndexChanged(sender, e)

                eval.KillProcess(loader_proceso_general)

                msg = "El proceso finalizo con exito " & vbCrLf & DateTime.Now.TimeOfDay.ToString
                MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Information)

            ElseIf opt = _FILT_ERRORES Then
                conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_procesamiento.Text, btn_procesar.Text))
                Dim idreg = Me.dgrid_are_procesamiento.SelectedRows(0).Cells(_FIELD_RE_SECUEN).Value
                conn.P_UPD_ESTADO_CORREGIDOS(idreg)
                cb_filtro_SelectedIndexChanged(sender, e)
            End If

        Catch ex As Exception
            eval.KillProcess(loader_proceso_general)
            MessageBox.Show(ex.Message, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.gb_control_visualizacion.Enabled = True
            Me.btn_procesar.Enabled = True
            Me.btn_select.Enabled = True
            Me.btn_unselect.Enabled = True
            Me.gb_filtro.Enabled = True
            Me.dgrid_are_procesamiento.Enabled = True
            eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub tb_anmproduccion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_anmproduccion.KeyPress
        'Si se presiona la tecla enter 
        If e.KeyChar = Chr(13) Then
            Dim v_usuario = Me.cb_usuarios_are.SelectedValue
            '@CQH
            Dim v_codigo = Me.tb_anmproduccion.Text
            'nuevo 230620
            Dim v_clase = Me.tb_anmproduccion.Text
            'antes del 230620
            'Dim table As DataTable = conn.P_SEL_PROGIS_FILTRO_CODIGO(v_usuario, v_codigo)
            'ahora 230620
            Dim table As DataTable = conn.P_SEL_PROGIS_FILTRO_CODIGO(v_usuario, v_codigo, v_clase)
            Me.dgrid_are_procesamiento.DataSource = table
            Dim rows = table.Rows.Count()
            Me.lbl_rowcount.Text = "Se encontraron " & rows.ToString() & " registros"
        End If
    End Sub


    Private Function shapeToFeatureTMP()

        Dim tb = Me.dgrid_are_procesamiento.Rows
        For Each row As DataGridViewRow In Me.dgrid_are_procesamiento.Rows
            If row.Cells(_FIELD_RE_CODEST).Value = 1 Then

                Dim oid = row.Cells(_FIELD_RE_SECUEN).Value
                Dim codigo = row.Cells(_FIELD_CG_CODIGO).Value

                obtenerDetalleEnProceso(codigo)

                _params.Clear()
                _params.Add(oid)
                _params.Add(gstrUsuarioAcceso)
                _params.Add(gstrUsuarioClave)
                Dim result = ExecuteGP(_tool_are_cargargdbtemporal, _params, _toolboxPathAre)
                Dim response = Split(result, ";")
                If response(0) <> 1 Then
                    error_scripttool_as_messagebox(response(1))
                Else
                    '@Daguado 20/06/2019
                    ExecuteGP(_tool_are_generarImagenes, _params, _toolboxPathAre, False)
                    '@End
                End If
                Me.tb_detalle_are.Text = ""
            End If
        Next

        For Each row As DataGridViewRow In Me.dgrid_are_procesamiento.Rows
            If row.Cells(_FIELD_RE_CODEST).Value = 1 Then
                Dim oid As Integer = row.Cells(_FIELD_RE_SECUEN).Value
                conn.P_UPD_NOPROCESADOS(oid)
            End If
        Next
    End Function



    Private Sub cb_filtro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_filtro.SelectedIndexChanged
        Try
            opt = Me.cb_filtro.SelectedValue
            Dim UserSelect = Me.cb_usuarios_are.SelectedValue

            ' PROCEDIMIENTO QUE ACTUALIZA LA TABLA DE PROCESAMIENTO SEGUN USUARIO Y ESTADOS
            Dim table As DataTable = conn.P_SEL_PROGIS_FILTRO(opt, UserSelect)
            Me.dgrid_are_procesamiento.DataSource = table
            '@END

            Dim rows = table.Rows.Count()

            Me.lbl_rowcount.Text = "Se encontraron " & rows.ToString() & " registros"

            For i As Int32 = 0 To dgrid_are_procesamiento.Columns.Count - 1
                dgrid_are_procesamiento.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                If dgrid_are_procesamiento.Columns(i).Name = _FIELD_PROCESAR Then
                    dgrid_are_procesamiento.Columns(i).ReadOnly = False
                Else
                    dgrid_are_procesamiento.Columns(i).ReadOnly = True
                End If
            Next

            If Not dgrid_are_procesamiento.Columns.Contains(_FIELD_PROCESAR) Then
                Dim column As New DataGridViewCheckBoxColumn
                With column
                    .HeaderText = _FIELD_PROCESAR
                    .Name = _FIELD_PROCESAR
                    .Width = 50
                End With
                dgrid_are_procesamiento.Columns.Insert(dgrid_are_procesamiento.Columns.Count(), column)
            End If

            'Configuracion de la columna tipo checkbox a partir del campo ESTADO
            For Each row As DataGridViewRow In dgrid_are_procesamiento.Rows
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_PROCTEMP Then
                    row.Cells(_FIELD_PROCESAR).Value = True
                End If
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_PROCPROD Then
                    row.Cells(_FIELD_PROCESAR).Value = True
                End If
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_NOPROCTEMP Then
                    row.Cells(_FIELD_PROCESAR).Value = False
                End If
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_NOPROCPROD Then
                    row.Cells(_FIELD_PROCESAR).Value = False
                End If
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_ERRTEMP Then
                    row.Cells(_FIELD_PROCESAR).Value = False
                    row.Cells(_FIELD_PROCESAR).ReadOnly = True
                    row.DefaultCellStyle.BackColor = Drawing.Color.Pink
                End If
                If row.Cells(_FIELD_RE_CODEST).Value = _EST_ERRPROD Then
                    row.Cells(_FIELD_PROCESAR).Value = False
                    row.Cells(_FIELD_PROCESAR).ReadOnly = True
                    row.DefaultCellStyle.BackColor = Drawing.Color.Pink
                End If
            Next

            'Control de visualizacion de botones segun tipo de filtro
            If opt = _FILT_PROCTEMP Then
                Me.btn_procesar.Enabled = True
                Me.gb_control_visualizacion.Enabled = True
                Me.gb_procesar.Visible = True
                Me.btn_procesar.Visible = True
                Me.btn_procesar.Text = _BT_NAME_PROC
                Me.cb_datum.Enabled = False
                Me.cb_zona.Enabled = False
                Me.btn_select.Enabled = True
                Me.btn_unselect.Enabled = True
                Me.tb_anmproduccion.Visible = False
            ElseIf opt = _FILT_PROCPROD Then
                Me.btn_procesar.Enabled = True
                Me.gb_control_visualizacion.Enabled = True
                Me.gb_procesar.Visible = True
                Me.gb_procesar.Enabled = True
                Me.btn_procesar.Visible = False
                Me.cb_datum.Enabled = True
                Me.cb_zona.Enabled = True
                Me.btn_select.Enabled = True
                Me.btn_unselect.Enabled = True
                Me.tb_anmproduccion.Visible = False
            ElseIf opt = _FILT_PROCESADO Then
                'se comento esta parte
                ' dgrid_are_procesamiento.Columns.Remove(_FIELD_PROCESAR)
                Me.gb_control_visualizacion.Enabled = True
                Me.gb_procesar.Visible = False
                Me.cb_datum.Enabled = True
                Me.cb_zona.Enabled = True
                Me.tb_anmproduccion.Visible = True
            ElseIf opt = _FILT_ERRORES Then
                dgrid_are_procesamiento.Columns.Remove(_FIELD_PROCESAR)
                Me.gb_control_visualizacion.Enabled = True
                Me.gb_procesar.Visible = True
                Me.btn_procesar.Visible = True
                Me.btn_procesar.Text = _BT_NAME_CORR
                Me.gb_procesar.Enabled = True
                Me.btn_select.Enabled = False
                Me.btn_unselect.Enabled = False
                Me.tb_anmproduccion.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgrid_are_procesamiento_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid_are_procesamiento.CellValueChanged
        'Si selecciona la cabecera el evento debe finalizar
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        'Si la columna PROCESAR no existe el evento debe finalizar
        If Not dgrid_are_procesamiento.Columns.Contains(_FIELD_PROCESAR) Then
            Exit Sub
        End If

        'Si se establece un registro con estados no aplicables a esta casuistica el evento debe finalizar
        Dim estado = Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value
        If estado = _EST_ERRTEMP Or estado = _EST_ERRPROD Then
            Exit Sub
        End If

        'Si no se hace click sobre la columna con checkbox el evento debe finalizar
        If e.ColumnIndex = Me.dgrid_are_procesamiento.Columns(_FIELD_PROCESAR).Index Then
            Dim dgvCheckBoxCell As DataGridViewCheckBoxCell = dgrid_are_procesamiento.Rows(e.RowIndex).Cells(e.ColumnIndex)
            dgrid_are_procesamiento.CommitEdit(DataGridViewDataErrorContexts.Commit)
            v_estado_Ar = "0"
            Dim checked As Boolean = CType(dgvCheckBoxCell.Value, Boolean)

            Dim filtro = cb_filtro.SelectedValue


            If checked Then
                'Si el checkbox es marcado
                If filtro = _FILT_PROCTEMP Then
                    'Si el filtro seleccionado es Procesar temporal
                    Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value = _EST_PROCTEMP
                   
                ElseIf filtro = _FILT_PROCPROD Then
                    'Si el filtro seleccionado es Procesar produccion
                    Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value = _EST_PROCPROD
                    
                ElseIf filtro = _FILT_PROCESADO Then
                    v_estado_Ar = "5"
                    Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value = 5

                End If
            Else
                'Si el checkbox es desmarcado
                If filtro = _FILT_PROCTEMP Then
                    'Si el filtro seleccionado es Procesar temporal
                    Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value = _EST_NOPROCTEMP
                    
                ElseIf filtro = _FILT_PROCPROD Then
                    'Si el filtro seleccionado es Procesar produccion
                    Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value = _EST_NOPROCPROD
                   

                End If
            End If
            If v_estado_Ar <> "5" Then
                Dim codreg = Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_SECUEN).Value
                estado = Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value

                conn.P_UPD_ESTADO_PROGIS(codreg, estado)

                conn.FT_Registro(1, String.Format("{0}*{1}*{2}*{3}*{4}", Me.Text, Me.tp_procesamiento.Text, filtro, codreg, checked.ToString()))
            End If

        End If
    End Sub

    Private Sub dgrid_are_procesamiento_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgrid_are_procesamiento.CellMouseUp
        'Si selecciona la cabecera el evento debe finalizar
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_MODREG).Value = _ANDE Then
            Me.btn_visualizar.Enabled = False
        Else
            Me.btn_visualizar.Enabled = True
        End If

        Dim idreg = Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_SECUEN).Value

        Me.obtenerDetalle(idreg)

        'Si el filtro seleccionado es ERRORES
        If opt = _FILT_ERRORES Then
            Dim estado = Me.dgrid_are_procesamiento.Rows(e.RowIndex).Cells(_FIELD_RE_CODEST).Value
            If estado = _EST_ERRPROD Then
                Me.cb_datum.Enabled = True
                Me.cb_zona.Enabled = True
            Else
                Me.cb_datum.Enabled = False
                Me.cb_zona.Enabled = False
            End If
        End If

        'Si la columna PROCESAR no existe el evento debe finalizar
        If Not dgrid_are_procesamiento.Columns.Contains(_FIELD_PROCESAR) Then
            Exit Sub
        End If
        'Si la columna a PROCESAR es seleccionada se debe esperar a que se cambie el checkbox para avanzar 
        If e.ColumnIndex = Me.dgrid_are_procesamiento.Columns(_FIELD_PROCESAR).Index Then
            dgrid_are_procesamiento.EndEdit()
            Me.obtenerDetalle(idreg)
        End If
    End Sub

    Private Function obtenerDetalle(ByVal idreg As Integer)
        Me.tb_detalle_are.ForeColor = Drawing.Color.DarkGreen
        Dim detalle = conn.P_SEL_DETALLE_REG(idreg)
        Dim detalle_array = Split(detalle, ",")
        Dim text As String
        For Each i As String In detalle_array
            text = text & i & vbCrLf
        Next

        Me.tb_detalle_are.Text = text
    End Function

    Private Function obtenerDetalleEnProceso(ByVal codigo As String)
        Me.tb_detalle_are.ForeColor = Drawing.Color.OrangeRed
        Me.tb_detalle_are.Text = vbCrLf & "Procesando " & codigo & "..."
    End Function

    Private Sub cb_datum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_datum.SelectedIndexChanged
        'Se debe inhabilitar la seleccion de zona geografica cuando el usuario selecciona un datum GCS
        Try
            opt = Me.cb_datum.SelectedValue
            If opt <= _FILT_DATUM_WGS Then
                Me.cb_zona.Enabled = True
            Else
                Me.cb_zona.Enabled = False
            End If
        Catch
        End Try
    End Sub

    Private Sub btn_unselect_Click(sender As Object, e As EventArgs) Handles btn_unselect.Click
        'Deselecciona todos los registros a excepcion de los tipo 96 y 97; almacena los cambios en base de datos
        Try
            For Each row As DataGridViewRow In dgrid_are_procesamiento.Rows
                If row.Cells(_FIELD_RE_CODEST).Value <> _EST_ERRTEMP And row.Cells(_FIELD_RE_CODEST).Value <> _EST_ERRPROD Then
                    row.Cells(_FIELD_PROCESAR).Value = False
                End If
            Next
        Catch
        End Try
    End Sub

    Private Sub btn_select_Click(sender As Object, e As EventArgs) Handles btn_select.Click
        'Selecciona todos los registros a excepcion de los tipo 96 y 97; almacena los cambios en base de datos
        Try
            For Each row As DataGridViewRow In dgrid_are_procesamiento.Rows
                If row.Cells(_FIELD_RE_CODEST).Value <> _EST_ERRTEMP And row.Cells(_FIELD_RE_CODEST).Value <> _EST_ERRPROD Then
                    row.Cells(_FIELD_PROCESAR).Value = True
                End If
            Next
        Catch
        End Try
    End Sub

    Private Sub btn_visualizar_Click(sender As Object, e As EventArgs) Handles btn_visualizar.Click
        Try

            If Me.dgrid_are_procesamiento.RowCount() = 0 Then
                Dim msg = "No se encontraron registros"
                MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Se obtienen los parametros necesarios para mostrar los feature layer correctos
            Dim nameshp = Me.dgrid_are_procesamiento.SelectedRows(0).Cells(_FIELD_RE_ARCGRA).Value()  'nombre del shapefile de entrada
            Dim codeare = Me.dgrid_are_procesamiento.SelectedRows(0).Cells(_FIELD_CG_CODIGO).Value() 'Codigo de registro en la GDB
            Dim estado = Me.dgrid_are_procesamiento.SelectedRows(0).Cells(_FIELD_RE_CODEST).Value() 'Codigo de registro en la GDB
            Dim datum = Me.cb_datum.SelectedValue 'Datum
            Dim zona = Me.cb_zona.SelectedValue 'Zona geografica

            If datum > 2 Then
                zona = "#"
            End If

            'filtro seleccionado
            opt = Me.cb_filtro.SelectedValue

            Dim feature_shp = conn.P_SEL_PATH_SHAPEFILE(codeare, nameshp)

            If opt = _FILT_PROCTEMP Or opt = _FILT_PROCPROD Or opt = _FILT_ERRORES Then
                If estado <> _EST_ERRPROD Then
                    _params.Clear()
                    _params.Add(feature_shp)
                    Dim result = ExecuteGP(_tool_gen_agregarfeaturetoc, _params)
                    Dim response = Split(result, ";")
                    If response(0) <> 1 Then
                        error_scripttool_as_messagebox(response(1))
                    End If
                End If
            End If

            If opt = _FILT_PROCPROD Or opt = _FILT_PROCESADO Or opt = _FILT_ERRORES Then
                If estado <> _EST_ERRTEMP Then
                    Dim feature_gdb = conn.P_SEL_NOMBRE_FEATURE(codeare, _FILT_PROCPROD, datum, zona)
                    _params.Clear()
                    _params.Add(codeare)
                    _params.Add(nameshp)
                    _params.Add(feature_gdb)
                    _params.Add(0)
                    Dim result = ExecuteGP(_tool_are_agregarfeaturetocare, _params, _toolboxPathAre)
                    Dim response = Split(result, ";")
                    If response(0) <> 1 Then
                        error_scripttool_as_messagebox(response(1))
                    End If
                End If
            End If

            If opt = _FILT_PROCESADO Then
                Dim feature_gdb = conn.P_SEL_NOMBRE_FEATURE(codeare, _FILT_PROCESADO, datum, zona)
                _params.Clear()
                _params.Add(codeare)
                _params.Add(nameshp)
                _params.Add(feature_gdb)
                _params.Add(1)
                Dim result = ExecuteGP(_tool_are_agregarfeaturetocare, _params, _toolboxPathAre)
                Dim response = Split(result, ";")
                If response(0) <> 1 Then
                    error_scripttool_as_messagebox(response(1))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_removerlayer_Click(sender As Object, e As EventArgs) Handles btn_removerlayer.Click
        Try
            Dim msg As String = "Esta seguro que desea remover todo los layers de la tabla de contenidos (TOC)"
            Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If response = DialogResult.OK Then
                _params.Clear()
                ExecuteGP(_tool_gen_removerfeaturetoc, _params)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cb_usuarios_are_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_usuarios_are.SelectedIndexChanged
        cb_filtro_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cb_filtro_controlreg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_filtro_controlreg.SelectedIndexChanged
        Try
            Dim tableprod As DataTable
            Dim tabletemp As DataTable
            Dim opt = Me.cb_filtro_controlreg.SelectedValue

            cambia_opciones_cbox_features()

            If opt = _FILT_ARE_RESE Then
                tableprod = conn.P_SEL_NUMREG_RESE_PROD()
                tabletemp = conn.P_SEL_NUMREG_RESE_TEMP()
            ElseIf opt = _FILT_ARE_URBA Then
                tableprod = conn.P_SEL_NUMREG_URBA_PROD()
                tabletemp = conn.P_SEL_NUMREG_URBA_TEMP()
            End If
            Me.dgv_registros_tmp.DataSource = tabletemp
            Me.dgv_registros_prod.DataSource = tableprod


            get_diferencia_anm()

        Catch

        End Try
    End Sub

    Private Function cambia_opciones_cbox_features()
        Dim _tipo = Me.cb_filtro_controlreg.SelectedValue
        Dim _env = Me.cb_env.SelectedValue
        Dim _table As DataTable = conn.P_SEL_FILTRO_FEATURES(_env, _tipo)
        get_option_cbox(Me.cb_features, _table)
    End Function

    Private Function get_diferencia_anm()
        Dim _table As DataTable
        Try
            Dim _tipo = Me.cb_filtro_controlreg.SelectedValue
            Dim _feature = Me.cb_features.SelectedValue
            If _tipo = _FILT_ARE_RESE Then
                _table = conn.P_SEL_RESE_DIFREG(_feature)
            Else
                _table = conn.P_SEL_URBA_DIFREG(_feature)
            End If

            Me.lbl_nroreg.Text = "Se encontraron " & _table.Rows.Count.ToString() & " registros"

            Me.dgv_difregistros.DataSource = _table
        Catch ex As Exception

        End Try

    End Function

    Private Sub cb_env_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_env.SelectedIndexChanged
        Try
            cambia_opciones_cbox_features()
            get_diferencia_anm()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cb_features_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_features.SelectedIndexChanged
        get_diferencia_anm()
    End Sub

    Private Sub btn_resedir_Click(sender As Object, e As EventArgs) Handles btn_resedir.Click
        If (Me.browserDialog.ShowDialog() = DialogResult.OK) Then
            tb_resepath.Text = browserDialog.SelectedPath
        End If
    End Sub

    Private Sub btn_urbadir_Click(sender As Object, e As EventArgs) Handles btn_urbadir.Click
        If (Me.browserDialog.ShowDialog() = DialogResult.OK) Then
            tb_urbapath.Text = browserDialog.SelectedPath
        End If
    End Sub



    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Try
            Dim msg As String = "Esta seguro que desea aplicar los cambios?" & vbCrLf & "Se almacenará los cambios registrados en la base de datos"
            Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If response = DialogResult.Cancel Then
                Exit Sub
            End If
            conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_config.Text, btn_save.Text))
            set_configuracion()
            get_configuracion()
            Dim _successmsg As String = "El módulo se configuró satisfactoriamente"
            MessageBox.Show(_successmsg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rb_x_CheckedChanged(sender As Object, e As EventArgs) Handles rb_exeahora.CheckedChanged, rb_exefindia.CheckedChanged, rb_noexe.CheckedChanged
        Select Case sender.Tag
            Case _TAG_CURRENT
                Me.btn_procesarprod.Visible = True
                Me.lbl_aviso_ejecucion.Visible = True
            Case _TAG_EXEENDDAY
                Me.btn_procesarprod.Visible = False
                Me.lbl_aviso_ejecucion.Visible = False
            Case _TAG_NOEXE
                Me.btn_procesarprod.Visible = False
                Me.lbl_aviso_ejecucion.Visible = False
        End Select

        If Me.Text = "" Then
            Exit Sub
        End If

        If sender.Checked = True Then
            conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_config.Text, sender.Text))
        End If

    End Sub

    Private Function get_configuracion()
        Try
            Dim _pathrese = conn.P_SEL_PATH_RESE()
            Dim _pathurba = conn.P_SEL_PATH_URBA()
            Dim _pathimag = conn.P_SEL_PATH_IMAGE()
            Dim _tipoexe = conn.P_SEL_TIPO_EXE()

            Me.tb_resepath.Text = _pathrese
            Me.tb_urbapath.Text = _pathurba
            Me.tb_imagpath.Text = _pathimag

            If rb_exeahora.Tag.ToString = _tipoexe Then
                rb_exeahora.Checked = True
                btn_procesarprod.Enabled = True
                Exit Function
            Else
                rb_exeahora.Checked = False
            End If

            If rb_exefindia.Tag.ToString = _tipoexe Then
                rb_exefindia.Checked = True
                Me.btn_procesarprod.Enabled = False
                Exit Function
            Else
                rb_exefindia.Checked = False
            End If

            If rb_noexe.Tag.ToString = _tipoexe Then
                rb_noexe.Checked = True
                Me.btn_procesarprod.Enabled = False
                Exit Function
            Else
                rb_noexe.Checked = False
            End If
        Catch ex As Exception
            Dim _msg = "VisualError" & vbCrLf & "No se pudo obtener la configuración establecida por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New System.Exception(_msg)
        End Try
    End Function

    Private Function set_configuracion()
        Try
            Dim _resepath = Me.tb_resepath.Text
            Dim _urbapath = Me.tb_urbapath.Text
            Dim _imagpath = Me.tb_imagpath.Text
            Dim _tiporese As String

            If rb_exeahora.Checked Then
                _tiporese = rb_exeahora.Tag.ToString
            ElseIf rb_exefindia.Checked Then
                _tiporese = rb_exefindia.Tag.ToString
            ElseIf rb_noexe.Checked Then
                _tiporese = rb_noexe.Tag.ToString
            End If

            conn.P_UPD_CONFIGURACION(_resepath, _urbapath, _imagpath, _tiporese)
        Catch ex As Exception
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la configuración del módulo por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New System.Exception(_msg)
        End Try
    End Function

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Dim msg As String = "Esta seguro que desea cancelar esta operación; se volverá a la configuración anterior?"
        Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If response = DialogResult.Cancel Then
            Exit Sub
        End If
        conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_config.Text, btn_cancel.Text))
        get_configuracion()
    End Sub

    Private Sub ckb_fechas_CheckedChanged(sender As Object, e As EventArgs) Handles ckb_fechas.CheckedChanged
        If Me.ckb_fechas.Checked = True Then
            Me.dtp_ini.Enabled = True
            Me.dtp_fin.Enabled = True
        Else
            Me.dtp_ini.Enabled = False
            Me.dtp_fin.Enabled = False
        End If
    End Sub

    Private Sub btn_buscarhistorico_Click(sender As Object, e As EventArgs) Handles btn_buscarhistorico.Click
        Try
            Dim _fecini As String = "#"
            Dim _fecfin As String = "#"
            If Me.ckb_fechas.Checked = True Then
                _fecini = Me.dtp_ini.Value.ToString("yyyy/MM/dd")
                _fecfin = Me.dtp_fin.Value.ToString("yyyy/MM/dd")
            End If

            Dim _codigo = Me.tb_codigo.Text
            If _codigo = "" Then
                _codigo = "#"
            End If

            Dim _table As DataTable = conn.P_FILTRO_HISTORICO(_codigo, _fecini, _fecfin)

            Me.lbl_nreg_historico.Text = "Se encontraron " & _table.Rows.Count.ToString() & " registros"
            Me.lbl_nreg_historico.Visible = True

            Me.dgv_historico.DataSource = _table
        Catch ex As Exception
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la consulta por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tb_codigo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_codigo.KeyPress
        'Si se presiona la tecla enter 
        If e.KeyChar = Chr(13) Then
            'Llama a la funcion buscar 
            Call btn_buscarhistorico_Click(sender, e)
        End If
    End Sub

    Private Function registros_as_string_historico()
        Dim identiarray As New List(Of Object)
        Dim identi As String = Nothing
        If Me.ckb_appselected.Checked = True Then
            'Se aplica para lo registros seleccionados
            For Each i As DataGridViewRow In Me.dgv_historico.SelectedRows
                identiarray.Add(i.Cells(_FIELD_RE_SECUEN).Value().ToString)
            Next
        Else
            'Se aplica para todos los registros
            For Each i As DataGridViewRow In Me.dgv_historico.Rows
                identiarray.Add(i.Cells(_FIELD_RE_SECUEN).Value().ToString)
            Next
        End If
        If identiarray.Count = 0 Then
            Dim _msg As String = "No existen registros"
            If Me.ckb_appselected.Checked = True Then
                _msg = _msg & " seleccionados"
            End If
            eval.KillProcess(loader_proceso_general)
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return identi
            Exit Function
        End If
        If identiarray.Count > 400 Then
            Dim _msg As String = "La cantidad maxima de registros a procesar es de 999"
            eval.KillProcess(loader_proceso_general)
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return identi
            Exit Function
        End If
        identi = String.Join(",", identiarray.ToArray())
        Return identi
    End Function

    Private Sub btn_export_shp_historico_Click(sender As Object, e As EventArgs) Handles btn_export_shp_historico.Click
        Try
            Shell(path_loader_proceso_general, 1)
            Dim identi As String = registros_as_string_historico()
            If identi = Nothing Then
                Exit Sub
            End If
            'Ejecucion de operacion
            _params.Clear()
            _params.Add(identi)
            _params.Add(gstrUsuarioAcceso)
            _params.Add(gstrUsuarioClave)
            ExecuteGP(_tool_are_exportarfeaturehistorico, _params, _toolboxPathAre)
        Catch ex As Exception
            eval.KillProcess(loader_proceso_general)
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la operación por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub btn_export_xls_historico_Click(sender As Object, e As EventArgs) Handles btn_export_xls_historico.Click
        Try
            Shell(path_loader_proceso_general, 1)
            Dim identi As String = registros_as_string_historico()
            If identi = Nothing Then
                Exit Sub
            End If
            'Ejecucion de operacion
            _params.Clear()
            _params.Add(identi)
            _params.Add(gstrUsuarioAcceso)
            _params.Add(gstrUsuarioClave)
            ExecuteGP(_tool_are_exportarexcelhistorico, _params, _toolboxPathAre)
        Catch ex As Exception
            eval.KillProcess(loader_proceso_general)
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la operación por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub btn_procesarprod_Click(sender As Object, e As EventArgs) Handles btn_procesarprod.Click
        Dim msg As String = "Esta seguro que desea ejecutar el procedimiento de actualización a producción?"
        Dim response = MessageBox.Show(msg, title_messagebox, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If response = DialogResult.Cancel Then
            Exit Sub
        End If
        Try
            conn.FT_Registro(1, String.Format("{0}*{1}*{2}", Me.Text, tp_config.Text, btn_procesarprod.Text))
            Shell(path_loader_proceso_general, 1)
            conn.P_UPD_PRODUCCION()
            eval.KillProcess(loader_proceso_general)
            Dim _msg = "El proceso termino satisfactoriamente, verificar resultados en el panel de " & tp_procesamiento.Text.ToUpper()
            _msg = _msg & vbCrLf & "Desea ir al panel de " & tp_procesamiento.Text.ToUpper() & "?"
            response = MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If response = DialogResult.Yes Then
                Me.TabControl1.SelectedTab = tp_procesamiento
            End If
        Catch ex As Exception
            eval.KillProcess(loader_proceso_general)
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la operación por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub tp_edit_registros_Click(sender As Object, e As EventArgs) Handles tp_edit_registros.Click

    End Sub

    Private Sub btn_buscar_Click(sender As Object, e As EventArgs) Handles btn_buscar.Click
        '@CQ
        Try
            Dim conn As New cls_Oracle
            ' Dim _v_indica As String = "1"
            Dim _codigo As String

            _codigo = Me.txtdato1.Text
            If _codigo = "" Then
                MsgBox("Debe Ingresar un codigo para buscar", MsgBoxStyle.Information, "Observación")
                Exit Sub

            End If

            Dim estadoGrafDictionary As New Dictionary(Of String, String)
            Dim leyendaDictionary As New Dictionary(Of String, String)

            estadoGrafDictionary.Add("REFERENCIAL", "REFERENCIAL")
            estadoGrafDictionary.Add("PROVISIONAL", "PROVISIONAL")
            estadoGrafDictionary.Add("DEFINITIVO", "DEFINITIVO")

            leyendaDictionary.Add("REFERENCIAL", "REFERENCIAL")
            leyendaDictionary.Add("DEFINITIVO", "DEFINITIVO")



            '  Dim _table As DataTable = conn.P_SEL_DATOS_AR("1", _codigo, "DATA_CAT.GPO_ARE_AREA_RESERVADA_W_18_T")
            Dim _table As DataTable = conn.P_SEL_DATOS_AR("1", _codigo)
            Me.lbl_nreg_historico.Text = "Se encontraron " & _table.Rows.Count.ToString() & " registros"

            _table.Columns.Add("SELEC", Type.GetType("System.String"))

            PT_Estilo_Grilla_AreasRestringidas1(_table) : PT_Cargar_Grilla_AreasRestringidas1(_table)
            PT_Agregar_Funciones_AreasRestringidas1() : PT_Forma_Grilla_Funciones_AreasRestringidasL1()
            Me.grid_editreg.DataSource = _table

            If _table.Rows.Count.ToString = 0 Then

                Dim _table1 As DataTable = conn.P_SEL_DATOS_AR("2", _codigo)
                Me.lbl_nreg_historico.Text = "Se encontraron " & _table1.Rows.Count.ToString() & " registros"
                Me.grid_editreg.DataSource = _table1

                _table1.Columns.Add("SELEC", Type.GetType("System.String"))

                PT_Estilo_Grilla_AreasRestringidas1(_table1) : PT_Cargar_Grilla_AreasRestringidas1(_table1)
                PT_Agregar_Funciones_AreasRestringidas1() : PT_Forma_Grilla_Funciones_AreasRestringidasL1()

            End If

            '_table.Columns.Add("SELEC", Type.GetType("System.String"))
            'PT_Estilo_Grilla_AreasRestringidas1(_table) : PT_Cargar_Grilla_AreasRestringidas1(_table)
            'PT_Agregar_Funciones_AreasRestringidas1() : PT_Forma_Grilla_Funciones_AreasRestringidasL1()

            ' definimos combobox para  estado de graficacion
            Dim vestadoGraf As C1.Win.C1TrueDBGrid.ValueItemCollection = New C1.Win.C1TrueDBGrid.ValueItemCollection()

            For Each kvp As KeyValuePair(Of String, String) In estadoGrafDictionary
                Dim clave As String = kvp.Key
                Dim valor As String = kvp.Value
                vestadoGraf.Add(New C1.Win.C1TrueDBGrid.ValueItem(clave, valor))
            Next

            grid_editreg.Columns("EST_GRAF").ValueItems.Values = vestadoGraf
            grid_editreg.Columns("EST_GRAF").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox


            'definimos combobox para leyenda
            Dim vleyenda As C1.Win.C1TrueDBGrid.ValueItemCollection = New C1.Win.C1TrueDBGrid.ValueItemCollection()

            For Each kvp As KeyValuePair(Of String, String) In leyendaDictionary
                Dim clave As String = kvp.Key
                Dim valor As String = kvp.Value
                vleyenda.Add(New C1.Win.C1TrueDBGrid.ValueItem(clave, valor))
            Next

            grid_editreg.Columns("LEYENDA").ValueItems.Values = vleyenda
            grid_editreg.Columns("LEYENDA").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox

            'grid_editreg.Columns("LEYENDA").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox

            'DEFINIMOS COMBOBOX PARA MINERIA SI NO
            grid_editreg.Columns("MINERIA").ValueItems.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("SI", "SI"))
            grid_editreg.Columns("MINERIA").ValueItems.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("NO", "NO"))
            grid_editreg.Columns("MINERIA").ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox










            Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            For i As Integer = 0 To Me.grid_editreg.RowCount - 1
                grid_editreg.Item(i, "SELEC") = False
            Next
            Me.grid_editreg.AllowUpdate = True
            grid_editreg.Focus()


        Catch ex As Exception
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la consulta por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '@CQ
    End Sub

    Private Sub PT_Estilo_Grilla_AreasRestringidas1(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_Sel_R).DefaultValue = ""


    End Sub



    Private Sub PT_Cargar_Grilla_AreasRestringidas1(ByVal padtbDetalle As DataTable)
        Dim dvwDetalle As New DataView(padtbDetalle)
        Me.grid_editreg.DataSource = dvwDetalle
        ' Me.dgdDetalle.Columns(Col_Sel_R).Value = True
    End Sub

    Private Sub PT_Agregar_Funciones_AreasRestringidas1()
        Me.grid_editreg.Columns(Col_Sel_R).DefaultValue = ""

    End Sub




    Private Sub PT_Forma_Grilla_Funciones_AreasRestringidasL1()
        Me.grid_editreg.Splits(0).DisplayColumns(Col_Sel_R).Width = 30


        Me.grid_editreg.Columns("SELEC").Caption = "SEL."


        Me.grid_editreg.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.grid_editreg.Splits(0).HeadingStyle.BackColor = Color.FromArgb(238, 238, 238)
        Me.grid_editreg.Splits(0).HeadingStyle.Font = New Font("Tahoma", 8.25, FontStyle.Bold)



        Me.grid_editreg.Splits(0).DisplayColumns(Col_Sel_R).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center


        Me.grid_editreg.Splits(0).DisplayColumns(Col_Sel_R).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center



    End Sub


    Private Sub btn_grabar_Click(sender As Object, e As EventArgs)
        ' Dim VO_OPCION As String
        Dim v_layer_1 As String
        Dim v_cod_rese As String
        Dim v_nombre As String
        Dim v_nm_rese As String
        Dim v_tprese As String
        Dim v_categori As String
        Dim v_clase As String
        Dim v_titular As String
        Dim v_obs As String
        Dim v_norma As String
        Dim v_entidad As String
        Dim v_uso As String
        Dim v_estgraf As String
        Dim v_leyenda As String
        Dim v_fecpub As String
        Dim v_env As String
        Dim v_identi As String
        Dim v_fecing As String
        Dim v_mineria As String
        Dim cls_Oracle As New cls_Oracle

        Dim lodbtExiste_tpnormarese As String


        'v_layer_1 = "DESA_GIS.GPO_ARE_AREA_RESERVADA_W18_T"

        '  v_layer_1 = "DATA_CAT.GPO_ARE_AREA_RESERVADA_W_18_T"
        ' VO_OPCION = "1"
        For w As Integer = 0 To grid_editreg.BindingContext(grid_editreg.DataSource, grid_editreg.DataMember).Count - 1


            If grid_editreg.Item(w, "SELEC") = True Then

                v_cod_rese = grid_editreg.Item(w, "CODIGO").ToString.ToUpper
                v_nombre = grid_editreg.Item(w, "NOMBRE").ToString
                If (v_nombre = "") Then
                    v_nombre = " "
                End If
                v_nm_rese = grid_editreg.Item(w, "NM_RESE").ToString
                If (v_nm_rese = "") Then
                    v_nm_rese = " "
                End If
                v_tprese = grid_editreg.Item(w, "TP_RESE").ToString.ToUpper
                If (v_tprese = "") Then
                    v_tprese = " "
                End If
                v_categori = grid_editreg.Item(w, "CATEGORI").ToString.ToUpper
                If (v_categori = "") Then
                    v_categori = " "
                End If
                v_clase = grid_editreg.Item(w, "CLASE").ToString.ToUpper
                If (v_clase = "") Then
                    v_clase = " "
                End If
                v_titular = grid_editreg.Item(w, "TITULAR").ToString.ToUpper
                If (v_titular = "") Then
                    v_titular = " "
                End If
                v_obs = grid_editreg.Item(w, "OBS").ToString.ToUpper
                If (v_obs = "") Then
                    v_obs = " "
                End If
                v_norma = grid_editreg.Item(w, "NORMA").ToString.ToUpper
                If (v_norma = "") Then
                    v_norma = " "
                End If
                v_entidad = grid_editreg.Item(w, "ENTIDAD").ToString.ToUpper
                If (v_entidad = "") Then
                    v_entidad = " "
                End If
                v_uso = grid_editreg.Item(w, "USO").ToString.ToUpper
                If (v_uso = "") Then
                    v_uso = " "
                End If
                v_estgraf = grid_editreg.Item(w, "EST_GRAF").ToString.ToUpper
                If (v_estgraf = "") Then
                    v_estgraf = " "
                End If
                v_leyenda = grid_editreg.Item(w, "LEYENDA").ToString.ToUpper
                If (v_leyenda = "") Then
                    v_leyenda = " "
                End If

                v_fecing = grid_editreg.Item(w, "FEC_ING").ToString
                If (v_fecing = "") Then
                    v_fecing = " "
                End If

                v_mineria = grid_editreg.Item(w, "MINERIA").ToString.ToUpper
                If (v_mineria = "") Then
                    v_mineria = " "
                End If


                If IsDate(v_fecing) = True Then
                    v_fecing = v_fecing.ToString.ToUpper
                    'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)

                Else
                    v_fecing = " "
                End If




                v_env = grid_editreg.Item(w, "ENV").ToString
                If (v_env = "") Then
                    v_env = "1"
                End If
                v_identi = grid_editreg.Item(w, "IDENTI").ToString.ToUpper
                If (v_identi = "") Then
                    MsgBox("Imposible, debe tener un identificador unico", MsgBoxStyle.Critical, "SIGCATMIN")
                End If



                lodbtExiste_tpnormarese = cls_Oracle.FT_INSERTADATOS_AR(VO_OPCION, v_identi, v_cod_rese, v_nombre, v_nm_rese, v_tprese, v_clase, v_categori, v_obs, v_norma, v_fecing, v_entidad, v_uso, v_estgraf, v_leyenda, v_mineria)
                ' lodbtExiste_tpnormarese = cls_Oracle.FT_INSERTADATOS_AR1(VO_OPCION, v_layer_1, v_identi, v_cod_rese, v_nombre)
                MsgBox("Ha finalizado su Actualización Satisfactoriamente en las Ocho Capas...", MsgBoxStyle.Critical, "SIGCATMIN")

            Else
                MsgBox("No Seleccionó ningun registro para su actualización", MsgBoxStyle.Critical, "SIGCATMIN")
                Exit Sub

            End If

        Next w
    End Sub

    Private Sub chkEstado_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.grid_editreg.Columns("SELEC").ValueItems
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

    Private Sub TableLayoutPanel5_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel5.Paint

    End Sub

    Private Sub tp_control_registros_Click(sender As Object, e As EventArgs) Handles tp_control_registros.Click

    End Sub

    Private Sub cb_filtro_tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_filtro_tipo.SelectedIndexChanged

        Try
            Dim opt = Me.cb_filtro_tipo.SelectedValue
            ' MessageBox.Show(opt, title_messagebox)
            If opt = "1" Then
                VO_OPCION = "1"
            Else
                VO_OPCION = "2"
            End If

        Catch ex As Exception

        End Try

        'cambia_opciones_cbox_features()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ' Dim VO_OPCION As String
        Dim v_layer_1 As String
        Dim v_cod_rese As String
        Dim v_nombre As String
        Dim v_nm_rese As String
        Dim v_tprese As String
        Dim v_categori As String
        Dim v_clase As String
        Dim v_titular As String
        Dim v_obs As String
        Dim v_norma As String
        Dim v_entidad As String
        Dim v_uso As String
        Dim v_estgraf As String
        Dim v_leyenda As String
        Dim v_fecpub As String
        Dim v_env As String
        Dim v_identi As String
        Dim v_fecing As String
        Dim v_mineria As String
        Dim cls_Oracle As New cls_Oracle

        Dim lodbtExiste_tpnormarese As String

        Dim anyselected As Boolean = False
        For row As Integer = 0 To grid_editreg.BindingContext(grid_editreg.DataSource, grid_editreg.DataMember).Count - 1
            anyselected = (anyselected Or grid_editreg.Item(row, "SELEC"))
        Next row

        If anyselected = False Then
            MsgBox("No Seleccionó ningun registro para su actualización", MsgBoxStyle.Critical, "SIGCATMIN")
            Exit Sub
        End If
        'v_layer_1 = "DESA_GIS.GPO_ARE_AREA_RESERVADA_W18_T"

        '  v_layer_1 = "DATA_CAT.GPO_ARE_AREA_RESERVADA_W_18_T"
        ' VO_OPCION = "1"
        For w As Integer = 0 To grid_editreg.BindingContext(grid_editreg.DataSource, grid_editreg.DataMember).Count - 1

            '  Dim tb = Me.dgrid_are_procesamiento.Rows
            ' For Each row As DataGridViewRow In Me.dgrid_are_procesamiento.Rows
            'If row.Cells(_FIELD_RE_CODEST).Value = 1 Then


            If grid_editreg.Item(w, "SELEC") = True Then
                If VO_OPCION = "1" Then
                    Try
                        v_cod_rese = grid_editreg.Item(w, "CODIGO").ToString.ToUpper
                        v_nombre = grid_editreg.Item(w, "NOMBRE").ToString.ToUpper
                        If (v_nombre = "") Then
                            v_nombre = " "
                        End If
                        v_nm_rese = grid_editreg.Item(w, "NM_RESE").ToString.ToUpper
                        If (v_nm_rese = "") Then
                            v_nm_rese = " "
                        End If
                        v_tprese = grid_editreg.Item(w, "TP_RESE").ToString.ToUpper
                        If (v_tprese = "") Then
                            v_tprese = " "
                        End If
                        v_categori = grid_editreg.Item(w, "CATEGORI").ToString.ToUpper
                        If (v_categori = "") Then
                            v_categori = " "
                        End If
                        v_clase = grid_editreg.Item(w, "CLASE").ToString.ToUpper
                        If (v_clase = "") Then
                            v_clase = " "
                        End If
                        v_titular = grid_editreg.Item(w, "TITULAR").ToString.ToUpper
                        If (v_titular = "") Then
                            v_titular = " "
                        End If
                        v_obs = grid_editreg.Item(w, "OBS").ToString.ToUpper
                        If (v_obs = "") Then
                            v_obs = " "
                        End If
                        v_norma = grid_editreg.Item(w, "NORMA").ToString.ToUpper
                        If (v_norma = "") Then
                            v_norma = " "
                        End If
                        v_entidad = grid_editreg.Item(w, "ENTIDAD").ToString.ToUpper
                        If (v_entidad = "") Then
                            v_entidad = " "
                        End If
                        v_uso = grid_editreg.Item(w, "USO").ToString.ToUpper
                        If (v_uso = "") Then
                            v_uso = " "
                        End If
                        v_estgraf = grid_editreg.Item(w, "EST_GRAF").ToString.ToUpper
                        If (v_estgraf = "") Then
                            v_estgraf = " "
                        End If
                        v_leyenda = grid_editreg.Item(w, "LEYENDA").ToString.ToUpper
                        If (v_leyenda = "") Then
                            v_leyenda = " "
                        End If

                        v_fecing = grid_editreg.Item(w, "FEC_ING").ToString
                        If (v_fecing = "") Then
                            v_fecing = " "
                        End If



                        If IsDate(v_fecing) = True Then
                            v_fecing = v_fecing.ToString.ToUpper
                            'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)

                        Else
                            v_fecing = " "
                        End If



                        v_env = grid_editreg.Item(w, "ENV").ToString
                        If (v_env = "") Then
                            v_env = "1"
                        End If
                        v_identi = grid_editreg.Item(w, "IDENTI").ToString
                        If (v_identi = "") Then
                            MsgBox("Imposible, debe tener un identificador unico, Solo permite actualizar codigos con IDENTI", MsgBoxStyle.Critical, "SIGCATMIN")
                        End If

                        v_mineria = grid_editreg.Item(w, "MINERIA").ToString.ToUpper
                        If (v_mineria = "") Then
                            v_mineria = " "
                        End If



                        lodbtExiste_tpnormarese = cls_Oracle.FT_INSERTADATOS_AR(VO_OPCION, v_identi, v_cod_rese, v_nombre, v_nm_rese, v_tprese, v_clase, v_categori, v_obs, v_norma, v_fecing, v_entidad, v_uso, v_estgraf, v_leyenda, v_mineria)
                        ' lodbtExiste_tpnormarese = cls_Oracle.FT_INSERTADATOS_AR1(VO_OPCION, v_layer_1, v_identi, v_cod_rese, v_nombre)
                        MsgBox("Ha finalizado su Actualización Satisfactoriamente en las Ocho Capas...", MsgBoxStyle.Information, "SIGCATMIN")

                    Catch ex As Exception
                        MsgBox("Eror, verifique la capa que desea actualizar", MsgBoxStyle.Critical, "SIGCATMIN")
                        Exit Sub


                    End Try


                ElseIf VO_OPCION = "2" Then
                    Try
                        v_cod_rese = grid_editreg.Item(w, "CODIGO").ToString.ToUpper
                        v_nombre = grid_editreg.Item(w, "NOMBRE").ToString.ToUpper
                        If (v_nombre = "") Then
                            v_nombre = " "
                        End If
                        v_nm_rese = grid_editreg.Item(w, "NM_URBA").ToString.ToUpper
                        If (v_nm_rese = "") Then
                            v_nm_rese = " "
                        End If
                        v_tprese = grid_editreg.Item(w, "TP_URBA").ToString.ToUpper
                        If (v_tprese = "") Then
                            v_tprese = " "
                        End If
                        v_categori = grid_editreg.Item(w, "CATEGORI").ToString.ToUpper
                        If (v_categori = "") Then
                            v_categori = " "
                        End If
                        '  v_clase = grid_editreg.Item(w, "CLASE").ToString.ToUpper                                                                                                           
                        ' If (v_clase = "") Then
                        'v_clase = " "
                        'End If
                        ' v_titular = grid_editreg.Item(w, "TITULAR").ToString.ToUpper
                        ' If (v_titular = "") Then
                        'v_titular = " "
                        'End If
                        v_obs = grid_editreg.Item(w, "OBS").ToString.ToUpper
                        If (v_obs = "") Then
                            v_obs = " "
                        End If
                        v_norma = grid_editreg.Item(w, "ORDENANZA").ToString.ToUpper
                        If (v_norma = "") Then
                            v_norma = " "
                        End If
                        v_entidad = grid_editreg.Item(w, "ENTIDAD").ToString.ToUpper
                        If (v_entidad = "") Then
                            v_entidad = " "
                        End If
                        v_uso = grid_editreg.Item(w, "USO").ToString.ToUpper
                        If (v_uso = "") Then
                            v_uso = " "
                        End If
                        v_estgraf = grid_editreg.Item(w, "EST_GRAF").ToString.ToUpper
                        If (v_estgraf = "") Then
                            v_estgraf = " "
                        End If
                        v_leyenda = grid_editreg.Item(w, "LEYENDA").ToString.ToUpper
                        If (v_leyenda = "") Then
                            v_leyenda = " "
                        End If

                        v_fecing = grid_editreg.Item(w, "FEC_ING").ToString
                        If (v_fecing = "") Then
                            v_fecing = " "
                        End If



                        If IsDate(v_fecing) = True Then
                            v_fecing = v_fecing.ToString.ToUpper
                            'Dim todaysdate As String = String.Format("{dd/MM/yyyy}", DateTime.Now)

                        Else
                            v_fecing = " "
                        End If



                        v_env = grid_editreg.Item(w, "ENV").ToString
                        If (v_env = "") Then
                            v_env = "1"
                        End If
                        v_identi = grid_editreg.Item(w, "IDENTI").ToString
                        If (v_identi = "") Then
                            MsgBox("Imposible, debe tener un identificador unico, Solo permite actualizar codigos con IDENTI", MsgBoxStyle.Critical, "SIGCATMIN")
                            Exit Sub
                        End If

                        v_mineria = grid_editreg.Item(w, "MINERIA").ToString.ToUpper
                        If (v_mineria = "") Then
                            v_mineria = " "
                        End If


                        lodbtExiste_tpnormarese = cls_Oracle.FT_INSERTADATOS_AR(VO_OPCION, v_identi, v_cod_rese, v_nombre, v_nm_rese, v_tprese, "", v_categori, v_obs, v_norma, v_fecing, v_entidad, v_uso, v_estgraf, v_leyenda, v_mineria)
                        MsgBox("Ha finalizado su Actualización Satisfactoriamente en las Ocho Capas...", MsgBoxStyle.Information, "SIGCATMIN")

                    Catch ex As Exception
                        MsgBox("Eror, verifique la capa que desea actualizar", MsgBoxStyle.Critical, "SIGCATMIN")
                        Exit Sub
                    End Try

                End If

            End If

        Next w
    End Sub

    Private Sub Btn_busqueda_Click(sender As Object, e As EventArgs) Handles Btn_busqueda.Click
        Try
            Dim conn As New cls_Oracle
            'Dim _v_indica As String = "1"
            Dim _codigo As String

            _codigo = Me.txt_dato1.Text
            If _codigo = "" Then
                MsgBox("Debe Ingresar un codigo para buscar", MsgBoxStyle.Information, "Observación")
                Exit Sub

            End If

            'Dim _table As DataTable = conn.P_SEL_DATOS_AR(_v_indica, _codigo, "DATA_CAT.GPO_ARE_AREA_RESERVADA_W_18_T")
            Dim _table As DataTable = conn.P_SEL_ALFANUMERICO_AR_("1", _codigo)
            Me.lbl_demarca.Text = "Se encontraron " & _table.Rows.Count.ToString() & " registros" & " Para la Demarcación del Area Restringida"

            Me.grid_demarca.DataSource = _table

            Dim _table1 As DataTable = conn.P_SEL_ALFANUMERICO_AR_("2", _codigo)
            Me.lbt_carta.Text = "Se encontraron " & _table1.Rows.Count.ToString() & " registros" & " Para la Carta del Area Restringida"

            Me.grid_carta.DataSource = _table1

            ' _table.Columns.Add("SELEC", Type.GetType("System.String"))

            ' PT_Estilo_Grilla_AreasRestringidas1(_table) : PT_Cargar_Grilla_AreasRestringidas1(_table)
            ' PT_Agregar_Funciones_AreasRestringidas1() : PT_Forma_Grilla_Funciones_AreasRestringidasL1()


            'Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
            'For i As Integer = 0 To Me.grid_editreg.RowCount - 1
            'grid_editreg.Item(i, "SELEC") = False
            'Next
            'Me.grid_editreg.AllowUpdate = True
            'grid_editreg.Focus()


        Catch ex As Exception
            Dim _msg = "VisualError" & vbCrLf & "No se pudo realizar la consulta por:" & vbCrLf & ex.Message
            MessageBox.Show(_msg, title_messagebox, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lnk_userguide_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnk_userguide.LinkClicked
        get_userguide()
    End Sub

    Private Sub btn_imagdir_Click(sender As Object, e As EventArgs) Handles btn_imagdir.Click
        If (Me.browserDialog.ShowDialog() = DialogResult.OK) Then
            tb_imagpath.Text = browserDialog.SelectedPath
        End If
    End Sub

    Private Sub btn_planos_Click(sender As Object, e As EventArgs) Handles btn_planos.Click
        val_opcion_plano_Ar = "1"



        Dim frm_planocaram As New Frm_Plano_Caram
        frm_planocaram.m_application = papp
        frm_planocaram.StartPosition = FormStartPosition.CenterScreen
        frm_planocaram.Show()


        Exit Sub


        Try
            Dim cls_catastro As New cls_DM_1
            Dim tb = Me.dgrid_are_procesamiento.Rows


            ' DATA_GIS.GPO_ARE_AREA_RESERVADA_WGS_17()
            gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_WGS_"
            'gstrFC_AReservada = "GPO_ARE_AREA_RESERVADA_W_"
            gstrFC_ZUrbana = "GPO_ZUR_ZONA_URBANA_WGS_"
            Dim msg As String
            Dim res As String
            Dim archivo As String
            Dim codigo As String
            Dim p_zona1 As String

            Dim cuenta As Integer
            Dim cls_eval As New Cls_evaluacion
            Dim cls_planos As New Cls_planos
            cls_catastro.Borra_Todo_Feature("", papp)
            cls_catastro.Limpiar_Texto_Pantalla(papp)
            Dim lodtbArea_Reserva As DataTable
            Dim cls_oracle As New cls_Oracle

            For Each row As DataGridViewRow In Me.dgrid_are_procesamiento.Rows
                'If row.Cells(_FIELD_RE_CODEST).Value = 1 Or row.Cells(_FIELD_RE_CODEST).Value = 3 Then
                If row.Cells(_FIELD_RE_CODEST).Value = 5 Then
                    cuenta = cuenta + 1
                    'If row.Cells(_FIELD_RE_CODEST).Value = 1 Then
                    'Dim oid = row.Cells(_FIELD_RE_SECUEN).Value
                    '  MsgBox(oid, MsgBoxStyle.Critical, "1")
                    codigo = row.Cells(_FIELD_CG_CODIGO).Value.ToString.ToUpper
                    v_usuario_Ar = row.Cells("USUARIO").Value.ToString.ToUpper
                    ' MsgBox(codigo, MsgBoxStyle.Critical, "2")
                    v_archivo = row.Cells(_FIELD_RE_ARCGRA).Value.ToString.ToUpper
                    ' archivo = UCase(archivo)
                    'MsgBox(archivo, MsgBoxStyle.Critical, "3")
                    p_zona1 = row.Cells(_FIELD_RE_ZONA).Value
                    'MsgBox(p_zona1, MsgBoxStyle.Critical, "4")
                    cod_opcion_Rese = Microsoft.VisualBasic.Left(codigo, 2)
                    v_clase_rese = row.Cells(_FIELD_RE_CLASE).Value.ToString
                    v_clase_rese = Microsoft.VisualBasic.Left(v_clase_rese, 1)
                    '  MsgBox(codigo, MsgBoxStyle.Critical, p_zona1)
                    caso_consulta = "CATASTRO MINERO" & cuenta
                    caso_plano = "PLANO UBICACION" & cuenta
                    cls_eval.adicionadataframe(caso_consulta)

                    v_zona_dm = p_zona1
                    If v_usuario_Ar = "FLAT1065" Then

                        v_usuario_Ar = "Frank Latorraca"
                    ElseIf v_usuario_Ar = "WVAL0398" Then
                        v_usuario_Ar = "William Valverde"
                    ElseIf v_usuario_Ar = "JFLO0569" Then
                        v_usuario_Ar = "José Flores"
                    ElseIf v_usuario_Ar = "CARI0213" Then
                        v_usuario_Ar = "Carlos Ari"
                    End If
                    'capturando nombre de area restringida
                    'Antes de 06-21
                    ' lodtbArea_Reserva = cls_oracle.F_Obtiene_Area_Reserva("CODIGO", codigo)

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

                    cls_catastro.consulta_arearestringida_PIM(codigo, v_archivo, p_zona1, papp)
                    Dim A As Integer = 2
                    'ojo falta mas indicador en el plano
                    cls_catastro.Zoom_to_Layer("Zona Reservada")
                    cls_catastro.Zoom_to_Layer("Zona Reservada1")

                    cls_planos.generaplanos_arearestringida_masivo(papp)

                    'NO ESTA SALIENDO EL MAPA UBICACION, COLOCAR ARCHIVO A LA FOTO
                    pMxDoc.UpdateContents()
                    pMxDoc.ActiveView.Refresh()
                    '     Exit Sub

                    '  cls_catastro.Genera_Imagen_DM(codigo, "restringida")
                    cls_catastro.Genera_Imagen_planopdf(codigo, "restringida")

                    'Exit Sub

                    'cls_catastro.Borra_Todo_Feature("", papp)
                    ' cls_catastro.Limpiar_Texto_Pantalla(papp)
                    pMxDoc.UpdateContents()

                    '  cls_eval.Eliminadataframe_masivo(caso_consulta)
                    ' cls_eval.Eliminadataframe_masivo(caso_plano)
                    pMxDoc.UpdateContents()
                    cls_planos.CambiaADataView(papp)

                    'cls_catastro.Genera_Imagen_DM(codigo, "regional")
                    '   cls_catastro.Borra_Todo_Feature("", papp)
                    '  cls_catastro.Limpiar_Texto_Pantalla(papp)
                    ' pMxDoc.UpdateContents()
                    ' cls_eval.Eliminadataframe_masivo(caso_consulta)
                    'cls_eval.Eliminadataframe_masivo(caso_plano)
                End If

            Next
            '  v_zona_dm = p_zona1
            'cls_catastro.consulta_arearestringida_PIM(codigo, archivo, p_zona1, papp)

            'Dim cls_planos As New Cls_planos
            'caso_consulta = "CASTASTRO MINERO"
            'falla este scrito
            'cls_planos.generaplanos_arearestringida(papp
            MsgBox("El Proceso de Generación de Planos Automaticos ha finalizado...", MsgBoxStyle.Information, "SIGCATMIN")
            val_opcion_plano_Ar = "0"
        Catch
            MsgBox("Error en el procedimiento de Generar Planos", MsgBoxStyle.Critical, "SIGCATMIN")
        End Try


    End Sub

    Private Sub dgrid_are_procesamiento_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrid_are_procesamiento.CellContentClick

    End Sub
End Class