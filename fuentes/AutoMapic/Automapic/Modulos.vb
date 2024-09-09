Imports System.Windows.Forms

Public Class Modulos
    Private Sub btn_modulo_nuevo_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btn_cerrar_sesion_Click(sender As Object, e As EventArgs) Handles btn_cerrar_sesion.Click
        Dim response As DialogResult = MessageBox.Show("¿Está seguro que deasea cerrar sesión en Automapic?", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = DialogResult.No Then
            Return
        End If
        Dim LoginForm = New Login()
        openFormByName(LoginForm, Me.Parent)
        modulosDict.Clear()
    End Sub

    Private Sub Modulos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga opciones al combo box de modulos
        'Dim dictionary As New Dictionary(Of Integer, String)
        'dictionary.Add(1, "Peligros geologicos")
        'dictionary.Add(2, "Plano Topográfico 25000")
        'For Each current In modulosList
        '    dictionary.Add(current(0), current(1))
        'Next
        RemoveHandler cbx_modulos.SelectedIndexChanged, AddressOf cbx_modulos_SelectedIndexChanged
        cbx_modulos.DataSource = New BindingSource(modulosDict, Nothing)
        cbx_modulos.DisplayMember = "Value"
        cbx_modulos.ValueMember = "Key"
        AddHandler cbx_modulos.SelectedIndexChanged, AddressOf cbx_modulos_SelectedIndexChanged
        'cbx_modulos.SelectedIndex = 0
        cbx_modulos_SelectedIndexChanged(sender, e)
        _LOADER_CONTROL = Me.pgb_modulos
    End Sub

    Private Sub Modulos_resizeEnd(sender As Object, e As EventArgs) Handles MyBase.Resize
        'Permite cambiar el size del formulario en funcion del DockableWindow
        Dim numberControls As Integer = pnl_modulos_form.Controls.Count()
        If (numberControls) Then
            Dim control = pnl_modulos_form.Controls.Item(0)
            Try
                control.Size = pnl_modulos_form.Size
                control.Update()
            Catch ex As Exception
                MessageBox.Show(ex.Message())
            End Try
        End If
    End Sub

    Private Sub cbx_modulos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_modulos.SelectedIndexChanged
        currentModule = (CType(cbx_modulos.SelectedItem, KeyValuePair(Of Integer, String))).Key
        currentModuleName = (CType(cbx_modulos.SelectedItem, KeyValuePair(Of Integer, String))).Value
        If (currentModule = 1) Then
            Dim plano_topografico_form = New Form_plano_topografico_25k()
            openFormByName(plano_topografico_form, pnl_modulos_form)
        ElseIf (currentModule = 2) Then
            Dim mapa_peligros_geologicos = Form_mapa_peligros_geologicos.GetInstance()
            'Dim mapa_peligros_geologicos = New Form_mapa_peligros_geologicos()
            openFormByName(mapa_peligros_geologicos, pnl_modulos_form)
        ElseIf (currentModule = 3) Then
            Dim mapa_geologico_50k = New Form_mapa_geologico_50k()
            openFormByName(mapa_geologico_50k, pnl_modulos_form)
        ElseIf (currentModule = 4) Then
            Dim mapa_hidrogeologico = New Form_mapa_hidrogeologico()
            openFormByName(mapa_hidrogeologico, pnl_modulos_form)
        ElseIf (currentModule = 5) Then
            Dim mapa_hidrogeoquimico = New Form_mapa_hidrogeoquimico()
            openFormByName(mapa_hidrogeoquimico, pnl_modulos_form)
        ElseIf (currentModule = 6) Then
            Dim sincronizacion_gdb = New Form_sincronizacion_geodatabase()
            openFormByName(sincronizacion_gdb, pnl_modulos_form)
        ElseIf (currentModule = 7) Then
            Dim mapa_neotectonica = New Form_mapa_neotectonica()
            openFormByName(mapa_neotectonica, pnl_modulos_form)
        ElseIf (currentModule = 8) Then
            Dim mapa_geopatrimonio = New Form_mapa_neotectonica()
            openFormByName(mapa_geopatrimonio, pnl_modulos_form)
        ElseIf (currentModule = 9) Then
            Dim mapa_potencial = New Form_mapa_potencial_minero()
            openFormByName(mapa_potencial, pnl_modulos_form)
        ElseIf (currentModule = 10) Then
            Dim datos_campo_dgr = New Form_datos_campo_dgr()
            openFormByName(datos_campo_dgr, pnl_modulos_form)
        ElseIf (currentModule = 11) Then
            Dim gestion_usuarios = New Form_gestion_usuarios()
            openFormByName(gestion_usuarios, pnl_modulos_form)
        ElseIf (currentModule = 12) Then
            Dim potencial_minero = New Form_calculo_potencial_minero()
            openFormByName(potencial_minero, pnl_modulos_form)
        ElseIf (currentModule = 13) Then
            Dim datos_campo_dgar = New Form_datos_campo_dgar()
            openFormByName(datos_campo_dgar, pnl_modulos_form)
        ElseIf (currentModule = 14) Then
            Dim validacion_mapa_geologico_50k = New Form_validacion_mapa_geologico_50k()
            openFormByName(validacion_mapa_geologico_50k, pnl_modulos_form)
        ElseIf (currentModule = 15) Then
            Dim HerramientasDesarrollador = New FormHerramientasDesarrollador()
            openFormByName(HerramientasDesarrollador, pnl_modulos_form)
        ElseIf (currentModule = 16) Then
            Dim mapa_geologico_no_50k = New Form_mapa_geologico_no_50k()
            openFormByName(mapa_geologico_no_50k, pnl_modulos_form)
        ElseIf (currentModule = 17) Then
            Dim diagrama_esfuerzos = New Form_diagrama_esfuerzos()
            openFormByName(diagrama_esfuerzos, pnl_modulos_form)
        ElseIf (currentModule = 18) Then
            Dim mapa_geomorfologico = New Form_mapa_geomorfologico()
            openFormByName(mapa_geomorfologico, pnl_modulos_form)
        End If

        If modulosPerfilDict.Item(currentModule) <> 4 Then
            requestInteraction(currentModuleName)
        End If
    End Sub

    Private Sub pbx_add_Click(sender As Object, e As EventArgs) Handles pbx_add.Click
        Dim mgs As String = "¿Está seguro que desea realizar una nueva configuración? Es posible que los cambios realizados se pierdan."
        Dim r As DialogResult = MessageBox.Show(mgs, __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If r = DialogResult.No Then
            Return
        End If
        GPToolDialog(_tool_updateSettings, True, _toolboxPath_automapic)
        Call cbx_modulos_SelectedIndexChanged(sender, e)
        'Me.Refresh()
    End Sub

    Private Sub pbx_user_guide_Click(sender As Object, e As EventArgs) Handles pbx_user_guide.Click
        Try
            Process.Start(modulosManualDict.Item(currentModule))
        Catch ex As Exception
            MessageBox.Show("Muy pronto estará disponible el manual de uso para este módulo!", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class