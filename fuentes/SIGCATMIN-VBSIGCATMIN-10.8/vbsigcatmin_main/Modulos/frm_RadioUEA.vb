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
Imports SIGCATMIN.form_ueas
Imports System.IO


Public Class frm_RadioCC

    Public m_application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_eval As New Cls_evaluacion
    Private cls_planos As New Cls_planos
    Dim RuntimeError As SigcatminException = New SigcatminException()
    'agregado datagrid para eventos
    WithEvents bsData As New BindingSource
    Private RowIdentifier As String = ""
    Private headerCheckBox As CheckBox = New CheckBox()



    Private Sub cboradiosim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboradiosim.SelectedIndexChanged
        Me.cbozonasim.Enabled = True
    End Sub

    Private Sub cbozonasim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbozonasim.SelectedIndexChanged
        Me.txteste_sim.Enabled = True
    End Sub

    Private Sub txteste_sim_TextChanged(sender As Object, e As EventArgs) Handles txteste_sim.TextChanged
        Me.txtnorte_sim.Enabled = True
        togle_btn_graficar()
        togle_btn_intsim()
    End Sub

    Private Function togle_btn_graficar()
        If Me.txteste_sim.Text <> "" And Me.txtnorte_sim.Text <> "" Then
            btngrafica_sim.Enabled = True
        Else
            btngrafica_sim.Enabled = False
        End If
    End Function

    Private Function togle_btn_intsim()
        If Me.txteste_sim.Text <> "" And Me.txtnorte_sim.Text <> "" Then
            btn_int_sim.Enabled = True
        Else
            btn_int_sim.Enabled = False
        End If
    End Function

    Private Sub txtnorte_sim_TextChanged(sender As Object, e As EventArgs) Handles txtnorte_sim.TextChanged
        togle_btn_graficar()
        togle_btn_intsim()
    End Sub

    Private Sub txteste_sim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txteste_sim.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))
    End Sub

    Private Sub txtnorte_sim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnorte_sim.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))
    End Sub

    'funciones para que se mantengan seleccionados los checkbox
    Private Sub bsData_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bsData.PositionChanged
        RowIdentifier = CType(bsData.Current, DataRowView).Item("NRO").ToString()
    End Sub

    Private Sub DataGridsim_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridsim.Sorted
        bsData.Position = bsData.Find("NRO", RowIdentifier)
    End Sub

    Private Sub btn_int_sim_Click(sender As Object, e As EventArgs) Handles btn_int_sim.Click

        _activar_btn_reporte_circulo_uea = 1
        v_repocirculo = 0
        Dim v_estesim, v_nortesim, v_radiosim, num_letra As String

        v_estesim = txteste_sim.Text
        v_nortesim = txtnorte_sim.Text
        v_radiosim = Me.cboradiosim.Text
        v_zonasim = Me.cbozonasim.Text

        'If v_radiosim = "" Or v_zonasim = "" Or v_estesim = "" Or v_nortesim = "" Then
        '    MsgBox("No Ingreso Datos para Generar Circulo ", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        _fn_graficar = "graficar_circunferencia_aleatoria"
        _parametros = "--uea U --zona {0} --user {1} --password {2} {3} {4} {5} {6}"

        Dim cls_eval As New Cls_evaluacion
        Dim cls_orac As New cls_Oracle
        Try
            Dim frm As New form_ueas

            Dim RetVal = Shell(path_loader_proceso_general, 1)
            Dim params = String.Format(_parametros, v_zonasim, gstrUsuarioAcceso, gstrUsuarioClave, _fn_graficar, v_estesim, v_nortesim, v_radiosim)
            '_bat_ueas = unidad_economica_administrativa.py
            Dim json As Linq.JObject = frm.ejecutar_procesos(_bat_ueas, params)
            Dim estado As Integer = json.SelectToken("estado")
            'MessageBox.Show(json("result")("influencia").ToString())

            If estado = 0 Then
                RuntimeError.ValidationError = json.SelectToken("msg")
                Throw RuntimeError
            End If

            name_circulo = json("result")("influencia").ToString()
            name_tcirculo = json("result")("integrantes").ToString()

            'If v_radiosim = "" Or v_zonasim = "" Or v_estesim = "" Or v_nortesim = "" Then
            '    MsgBox("No Ingreso Datos para Generar Circulo ", MsgBoxStyle.Information)
            '    Exit Sub
            'Else
            v_repocirculo = 1
            fecha_archi = DateTime.Now.Ticks.ToString()


            Dim ficha As String = glo_pathTMP & "\" & name_tcirculo & ".txt"
            Dim texto As String
            Dim sr As New System.IO.StreamReader(ficha)
            Dim strarr() As String
            Dim conteo As Integer = 0

            texto = sr.ReadToEnd()
            strarr = texto.Split(",")
            'Borramos datos de la tabla temporal
            cls_orac.P_DEL_DM_SIMUL(1)
            For Each s As String In strarr
                'insertamos los dm en la tabla temporal
                cls_orac.P_INS_DM_SIMUL(s)

            Next
            'obtiene integrantes que intersectan el circulo
            tabla_integrantes = cls_orac.P_GET_INTEGRANTES_SIM(1)
            tabla_integrantes.Columns.Add(New DataColumn("Process", GetType(System.Boolean)))
            tabla_integrantes.Columns("Process").SetOrdinal(0)

            For Each row As DataRow In tabla_integrantes.Rows
                row("Process") = True
            Next


            bsData.DataSource = tabla_integrantes
            DataGridsim.DataSource = bsData
            'ocultamos la columna que muestra la flecha de ubicacion
            DataGridsim.RowHeadersVisible = False



            'bsData.DataSource = DataGridsim
            DataGridsim.DefaultCellStyle.ForeColor = Color.Blue



            'Find the Location of Header Cell.
            Dim headerCellLocation As Point = DataGridsim.GetCellDisplayRectangle(0, -1, True).Location

            'Place the Header CheckBox in the Location of the Header Cell.
            headerCheckBox.Location = New Point(headerCellLocation.X + 8, headerCellLocation.Y + 2)
            headerCheckBox.BackColor = Color.White
            headerCheckBox.Size = New Size(18, 18)
            headerCheckBox.Checked = True

            AddHandler headerCheckBox.Click, AddressOf HeaderCheckBox_Clicked
            DataGridsim.Controls.Add(headerCheckBox)



            For Each column As DataGridViewColumn In Me.DataGridsim.Columns
                If conteo = 0 Then
                    column.ReadOnly = False
                    column.Width = 45
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Else
                    column.ReadOnly = True
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End If
                conteo += 1
            Next


            For Each column As DataGridViewColumn In Me.DataGridsim.Columns
                Dim colw As Integer
                colw = column.Width
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                column.Width = colw
            Next


        Catch meEx As SigcatminException
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show(meEx.SigcatminError, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            Dim msg As String = "VisualError: " & vbCrLf & ex.Message
            MessageBox.Show(msg, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub

        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub
    ' funcion para que el selectall seleccione o deseleccione todos los checkbox
    Private Sub HeaderCheckBox_Clicked(ByVal sender As Object, ByVal e As EventArgs)
        'Necessary to end the edit mode of the Cell.
        DataGridsim.EndEdit()

        'Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
        For Each row As DataGridViewRow In DataGridsim.Rows
            Dim checkBox As DataGridViewCheckBoxCell = (TryCast(row.Cells("Process"), DataGridViewCheckBoxCell))
            checkBox.Value = headerCheckBox.Checked
        Next
    End Sub
    ' funcion para que se marque el selectall si todos los checks individuales lo estan
    Private Sub DataGridView_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        'Check to ensure that the row CheckBox is clicked.
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then

            'Loop to verify whether all row CheckBoxes are checked or not.
            Dim isChecked As Boolean = True
            For Each row As DataGridViewRow In DataGridsim.Rows
                If Convert.ToBoolean(row.Cells("Process").EditedFormattedValue) = False Then
                    isChecked = False
                    Exit For
                End If
            Next

            headerCheckBox.Checked = isChecked
        End If
    End Sub

    Private Function graficarSeleccionados()
        Dim checkarr As New List(Of String)()
        Dim cuenta As Integer = 0

        For Each row As DataGridViewRow In Me.DataGridsim.Rows
            If row.Cells("Process").Value = True Then
                Dim codigous As String = row.Cells("CODIGOU").Value
                checkarr.Add(codigous)

            End If
        Next
        For Each s As String In checkarr
            If cuenta = 0 Then
                lista_dm_sim = "CODIGOU =  '" & s & "'"
            Else
                lista_dm_sim = lista_dm_sim & " OR " & "CODIGOU =  '" & s & "'"
            End If
            cuenta += 1
        Next
    End Function
    Private Sub btngrafica_sim_Click(sender As Object, e As EventArgs) Handles btngrafica_sim.Click
        _activar_btn_reporte_circulo_uea = 1
        v_repocirculo = 0
        Dim v_estesim, v_nortesim, v_radiosim, num_letra As String

        v_estesim = txteste_sim.Text
        v_nortesim = txtnorte_sim.Text
        v_radiosim = Me.cboradiosim.Text
        v_zonasim = Me.cbozonasim.Text

        'If v_radiosim = "" Or v_zonasim = "" Or v_estesim = "" Or v_nortesim = "" Then
        '    MsgBox("No Ingreso Datos para Generar Circulo ", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        _fn_graficar = "graficar_circunferencia_aleatoria"
        _parametros = "--uea U --zona {0} --user {1} --password {2} {3} {4} {5} {6}"

        Dim cls_eval As New Cls_evaluacion
        Dim cls_orac As New cls_Oracle
        Try
            Dim frm As New form_ueas

            Dim RetVal = Shell(path_loader_proceso_general, 1)
            'Dim params = String.Format(_parametros, v_zonasim, gstrUsuarioAcceso, gstrUsuarioClave, _fn_graficar, v_estesim, v_nortesim, v_radiosim)
            ''_bat_ueas = unidad_economica_administrativa.py
            'Dim json As Linq.JObject = frm.ejecutar_procesos(_bat_ueas, params)
            'Dim estado As Integer = json.SelectToken("estado")
            ''MessageBox.Show(json("result")("influencia").ToString())

            'If estado = 0 Then
            '    RuntimeError.ValidationError = json.SelectToken("msg")
            '    Throw RuntimeError
            'End If

            'name_circulo = json("result")("influencia").ToString()
            'name_tcirculo = json("result")("integrantes").ToString()

            ''If v_radiosim = "" Or v_zonasim = "" Or v_estesim = "" Or v_nortesim = "" Then
            ''    MsgBox("No Ingreso Datos para Generar Circulo ", MsgBoxStyle.Information)
            ''    Exit Sub
            ''Else
            v_repocirculo = 1
            fecha_archi = DateTime.Now.Ticks.ToString()

            cls_planos.CambiaADataView(m_application)
            cls_Catastro.Borra_Todo_Feature("", m_application)
            cls_Catastro.Limpiar_Texto_Pantalla(m_application)

            esc_sim = "1"
            arch_cata = ""
            caso_consulta = "CIRCULO UEA"

            If pMap.Name <> "CIRCULO UEA" Then
                cls_planos.buscaadataframe(caso_consulta, False)
                If valida = False Then
                    pMap.Name = "CIRCULO UEA"
                    pMxDoc.UpdateContents()
                End If
                cls_eval.ActivaDataframe_Opcion(caso_consulta, m_application)
                pMxDoc.UpdateContents()
            End If
            cls_eval.Eliminadataframe(caso_consulta)
            cls_planos.buscaadataframe("CIRCULO UEA", False)
            If valida = False Then
                pMap.Name = "CIRCULO UEA"
            End If
            cls_Catastro.Actualizar_DM(zona)
            pMxDoc.UpdateContents()

            'Const ficha As String = "C:\BDGEOCATMIN\Temporal\tcirculo.txt"
            'Dim ficha As String = glo_pathTMP & "\" & name_tcirculo & ".txt"
            'Dim texto As String
            'Dim sr As New System.IO.StreamReader(ficha)
            'Dim strarr() As String
            'Dim cuenta As Integer = 0

            'texto = sr.ReadToEnd()
            'strarr = texto.Split(",")
            ''Borramos datos de la tabla temporal
            'cls_orac.P_DEL_DM_SIMUL(1)
            'For Each s As String In strarr
            '    'insertamos los dm en la tabla temporal
            '    cls_orac.P_INS_DM_SIMUL(s)

            'Next
            'obtiene integrantes que intersectan el circulo
            'For Each s As String In strarr
            'If cuenta = 0 Then
            'lista_dm_sim = "CODIGOU =  '" & s & "'"
            'Else
            'lista_dm_sim = lista_dm_sim & " OR " & "CODIGOU =  '" & s & "'"
            'End If
            'cuenta += 1
            'Next
            'Debe construirse el dataview para obtener los valores de registros integrantes de simulacion con checkbox para qe en graficar
            ' se reemplaze a lista_dm_sim

            graficarSeleccionados()
            'Agregando capas a la vista del mapa
            cls_eval.AddLayerFromFile_demarcacion(m_application, v_zonasim)

            cls_Catastro.Add_ShapeFile_tmp(name_circulo, m_application)
            cls_Catastro.Add_ShapeFile(name_circulo, m_application)
            cls_Catastro.Actualizar_DM(v_zonasim)
            cls_Catastro.Color_Poligono_Simple_2(m_application, "Circulo_simulado")

            gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_"
            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Catastro_Minero & v_zonasim, m_application, "1", False)

            cls_eval.consultacapaDM("Catastro_circulo", "Catastro_circulo", "Catastro")
            lista_dm_sim = ""
            cls_Catastro.Expor_Tema("cata_cir" & fecha_archi, sele_denu, m_application)
            cls_Catastro.Quitar_Layer("Catastro", m_application)

            cls_Catastro.Layer_TableSort(m_application, "cata_cir" & fecha_archi, "catas_cir" & fecha_archi, "CODIGOU")
            cls_Catastro.Quitar_Layer("catas_cir" & fecha_archi, m_application)

            cls_Catastro.Add_ShapeFile_tmp("catas_cir" & fecha_archi, m_application)
            cls_eval.agregacampotema_tpm("catas_cir" & fecha_archi, "Catastro_circulo")
            cls_Catastro.Add_ShapeFile("catas_cir" & fecha_archi, m_application)
            cls_Catastro.Update_Value_Layer(m_application, "Catastro_circulo", "Catastro_circulo")

            cls_Catastro.Color_Poligono_Simple_2(m_application, "Catastro_circulo")
            cls_Catastro.rotulatexto_dm("Catastro_circulo", m_application)
            cls_Catastro.Zoom_to_Layer("Circulo_simulado")
            pMxDoc.UpdateContents()
            'Me.Close()

            cls_planos.generaplano_UEA(m_application, "CIRCULO UEA", "Plano dm_circulo")

            'End If
        Catch meEx As SigcatminException
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show(meEx.SigcatminError, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub
        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            Dim msg As String = "VisualError: " & vbCrLf & ex.Message
            MessageBox.Show(msg, title_messagebox, Nothing, MessageBoxIcon.Error)
            Exit Sub

        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()
    End Sub


    Private Sub frm_RadioCC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class