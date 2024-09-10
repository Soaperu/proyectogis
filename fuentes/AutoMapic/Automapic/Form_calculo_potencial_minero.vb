Imports System.Data.SQLite
Imports System.Linq
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class Form_calculo_potencial_minero
    Dim lbl_departamento_proy_nuevo As String = "Seleccione el departamento"
    Dim lbl_departamento_proy_existente As String = "Seleccione el proyecto existente"
    Dim lbl_cargar As String = "Cargar"
    Dim lbl_crear As String = "Crear"
    Dim controller_proyect As String = 1 '1: crear; 2: existente
    Dim workspace_dir As String = Nothing
    Dim departament_cd As String = Nothing
    Dim params As New List(Of Object)
    Dim regionesCombobox As New Dictionary(Of String, String)
    Dim geodatabasesCombobox As New Dictionary(Of String, String)
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim title As String = "CÁLCULO DEL PORTENCIAL MINERO DEL DEPARTAMENTO DE ${NM_DEPA} (UTM WGS84 ${ZONA}S)"
    Private Sub Form_calculo_potencial_minero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbl_cpm_titulo.Visible = False
        lbl_cpm_cargar_info.Visible = False
        tct_cpm_carga_informacion.Visible = False
        lbl_cpm_calculo_potencial.Visible = False
        lbl_cpm_pmm.Visible = False
        lbl_cpm_pmnm.Visible = False
        M_RAS_PotencialMineroMetalico.Visible = False
        M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_M_RAS_PotencialMineroMetalico.Visible = False
        btn_M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_cpm_calcular_potencial.Visible = False
        btn_cpm_open_folder.Visible = False
        btn_cpm_nuevo_Click(sender, e)
    End Sub
    Private Sub btn_cpm_nuevo_Click(sender As Object, e As EventArgs) Handles btn_cpm_nuevo.Click
        Try
            btn_cpm_nuevo.FlatAppearance.BorderSize = 1
            btn_cpm_existente.FlatAppearance.BorderSize = 0
            btn_cpm_directorio.Enabled = False
            cbx_cpm_departamento_proy.Enabled = False
            btn_cpm_nuevo.Enabled = False
            btn_cpm_existente.Enabled = False
            btn_cpm_crear_cargar.Enabled = False
            controller_proyect = 1
            runProgressBar()
            lbl_cpm_departamento_proy.Text = lbl_departamento_proy_nuevo
            btn_cpm_crear_cargar.Text = lbl_crear

            RemoveHandler cbx_cpm_departamento_proy.SelectedIndexChanged, AddressOf cbx_cpm_departamento_proy_SelectedIndexChanged

            'Cargar departamento a combobox
            params.Clear()
            If regionesCombobox.Count = 0 Then

                Dim response As String = ExecuteGP(_tool_getRegions, params, tbxpath:=_toolboxPath_automapic)

                If response Is Nothing Then
                    RuntimeError.VisualError = message_runtime_error
                    Throw New System.Exception(RuntimeError.PythonError)
                End If

                Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

                If responseJson.Item("status") = 0 Then
                    RuntimeError.PythonError = responseJson.Item("message")
                    Throw New System.Exception(RuntimeError.PythonError)
                End If
                regionesCombobox.Add("-1", "----- Seleccione una región -----")
                For Each current In responseJson.Item("response")
                    regionesCombobox.Add(current(0), current(1))
                Next
            End If

            cbx_cpm_departamento_proy.DataSource = New BindingSource(regionesCombobox, Nothing)
            cbx_cpm_departamento_proy.DisplayMember = "Value"
            cbx_cpm_departamento_proy.ValueMember = "Key"

            cbx_cpm_departamento_proy.Enabled = True

            AddHandler cbx_cpm_departamento_proy.SelectedIndexChanged, AddressOf cbx_cpm_departamento_proy_SelectedIndexChanged
            btn_cpm_directorio.Enabled = True
            btn_cpm_nuevo.Enabled = True
            btn_cpm_existente.Enabled = True
            cbx_cpm_departamento_proy.Enabled = True
            btn_cpm_crear_cargar.Enabled = True
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
        End Try

    End Sub

    Private Sub btn_cpm_existente_Click(sender As Object, e As EventArgs) Handles btn_cpm_existente.Click
        Try
            btn_cpm_nuevo.FlatAppearance.BorderSize = 0
            btn_cpm_existente.FlatAppearance.BorderSize = 1
            controller_proyect = 2
            lbl_cpm_departamento_proy.Text = lbl_departamento_proy_existente
            tbx_cpm_directorio.Text = ""
            btn_cpm_crear_cargar.Text = lbl_cargar
            cbx_cpm_departamento_proy.DataSource = Nothing
            cbx_cpm_departamento_proy.Items.Clear()

            cbx_cpm_departamento_proy.Enabled = False
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub



    Private Sub btn_cpm_directorio_Click(sender As Object, e As EventArgs) Handles btn_cpm_directorio.Click
        Try
            Dim workspace_dir_temp = openDialogBoxESRI(f_folder)
            If workspace_dir_temp Is Nothing Then
                Return
            End If
            workspace_dir = workspace_dir_temp
            tbx_cpm_directorio.Text = workspace_dir

            If controller_proyect = 2 Then
                runProgressBar()
                RemoveHandler cbx_cpm_departamento_proy.SelectedIndexChanged, AddressOf cbx_cpm_departamento_proy_SelectedIndexChanged

                'Cargar departamento a combobox
                params.Clear()

                params.Add(workspace_dir_temp)
                Dim response As String = ExecuteGP(_tool_getListGeodatabases, params, tbxpath:=_toolboxPath_automapic)

                If response Is Nothing Then
                    RuntimeError.VisualError = message_runtime_error
                    Throw New System.Exception(RuntimeError.PythonError)
                End If

                Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)



                If responseJson.Item("status") = 0 Then
                    RuntimeError.PythonError = responseJson.Item("message")
                    Throw New System.Exception(RuntimeError.PythonError)
                End If

                geodatabasesCombobox.Clear()

                geodatabasesCombobox.Add("-1", "----- Seleccione una geodatabase -----")
                For Each current In responseJson.Item("response")
                    geodatabasesCombobox.Add(current(0), current(1))
                Next

                cbx_cpm_departamento_proy.DataSource = Nothing
                cbx_cpm_departamento_proy.Items.Clear()
                cbx_cpm_departamento_proy.DataSource = New BindingSource(geodatabasesCombobox, Nothing)
                cbx_cpm_departamento_proy.DisplayMember = "Value"
                cbx_cpm_departamento_proy.ValueMember = "Key"

                cbx_cpm_departamento_proy.Enabled = True

                AddHandler cbx_cpm_departamento_proy.SelectedIndexChanged, AddressOf cbx_cpm_departamento_proy_SelectedIndexChanged
            End If
        Catch ex As Exception
            runProgressBar("ini")
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
        End Try
    End Sub

    Private Sub btn_cpm_crear_cargar_Click(sender As Object, e As EventArgs) Handles btn_cpm_crear_cargar.Click
        Try
            'Dim connection As String = "Data Source=" & _path_sqlite
            'Dim SQLConn As New SQLiteConnection(connection)
            'Dim SQLcmd As New SQLiteCommand(SQLConn)
            'SQLConn.Open()
            'Dim SQLstr_config_workspace As String = String.Format("UPDATE TB_CONFIG SET VALUE = '{0}' WHERE ID = 9", workspace_dir)
            'SQLcmd.CommandText = SQLstr_config_workspace
            'SQLcmd.ExecuteNonQuery()

            If controller_proyect = 1 Then
                'Se crea el proyecto
                workspace_dir = tbx_cpm_directorio.Text
                If workspace_dir Is Nothing Then
                    Throw New Exception("Debe especificar un directorio como espacio de trabajo")
                End If

                Dim cd_depa = (CType(cbx_cpm_departamento_proy.SelectedItem, KeyValuePair(Of String, String))).Key
                If cd_depa = -1 Then
                    Throw New Exception("Debe seleccionar un departamento")
                End If

                'Ejecutar proceso que crea la estructura de base de datos
                runProgressBar()
                Cursor.Current = Cursors.WaitCursor

                'Realiza una preconfiguracion de la capa (define src, reproyecta, etc)
                params.Clear()
                params.Add(workspace_dir)
                params.Add(cd_depa)
                Dim response As String = ExecuteGP(_tool_generateProjectCPM, params, _toolboxPath_calculo_potencialminero, getresult:=True, showCancel:=False)

                'Si el proceso falla entonces muestra un mensaje 
                If response Is Nothing Then
                    RuntimeError.VisualError = message_runtime_error
                    Throw New Exception(RuntimeError.VisualError)
                    Return
                End If

                'Si el proceso no falla se obtiene el resultado de la operacion
                Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)


                'Si el proceso tiene como respuesta el valor 0, quiere decir que se ha generado un problema en el script de python
                If responseJson.Item("status") = 0 Then
                    RuntimeError.PythonError = responseJson.Item("message")
                    Throw New Exception(RuntimeError.VisualError)
                    Return
                End If

                Dim nm_depa As String = responseJson.Item("data").item("nm_depa")
                Dim zona = responseJson.Item("data").item("zona")

                Dim title_current = title.Replace("${NM_DEPA}", nm_depa)
                title_current = title_current.Replace("${ZONA}", zona)

                lbl_cpm_titulo.Text = title_current

                validatFeatures()

            ElseIf controller_proyect = 2 Then
                'Se agrega un proyecto existente
                workspace_dir = tbx_cpm_directorio.Text
                If workspace_dir Is Nothing Then
                    Throw New Exception("Debe especificar un directorio como espacio de trabajo")
                End If

                Dim id_geodatabase = (CType(cbx_cpm_departamento_proy.SelectedItem, KeyValuePair(Of String, String))).Key
                If id_geodatabase = -1 Then
                    Throw New Exception("Debe seleccionar una geodatabase")
                End If

                'Ejecutar proceso de verificacion de base de datos
                runProgressBar()
                Cursor.Current = Cursors.WaitCursor

                'Realiza una preconfiguracion de la capa (define src, reproyecta, etc)
                params.Clear()
                params.Add(workspace_dir)
                params.Add(geodatabasesCombobox.Item(id_geodatabase))
                Dim response As String = ExecuteGP(_tool_loadProjectCPM, params, _toolboxPath_calculo_potencialminero, getresult:=True, showCancel:=False)

                'Si el proceso falla entonces muestra un mensaje 
                If response Is Nothing Then
                    RuntimeError.VisualError = message_runtime_error
                    Throw New Exception(RuntimeError.VisualError)
                    Return
                End If

                'Si el proceso no falla se obtiene el resultado de la operacion
                Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)


                'Si el proceso tiene como respuesta el valor 0, quiere decir que se ha generado un problema en el script de python
                If responseJson.Item("status") = 0 Then
                    RuntimeError.PythonError = responseJson.Item("message")
                    Throw New Exception(RuntimeError.VisualError)
                    Return
                End If

                For Each current In responseJson.Item("response")

                    Dim btn As Object = Me.Controls.Find(current(0).ToString(), True).FirstOrDefault()
                    If current(1) = 1 Then
                        btn.ImageIndex = current(1)
                        btn.visible = True
                    Else
                        btn.visible = False
                    End If
                Next


                Dim nm_depa As String = responseJson.Item("data").item("nm_depa")
                Dim zona = responseJson.Item("data").item("zona")

                Dim title_current = title.Replace("${NM_DEPA}", nm_depa)
                title_current = title_current.Replace("${ZONA}", zona.ToString().Substring(3, 2))

                lbl_cpm_titulo.Text = title_current

                'validatFeatures()

            End If
            lbl_cpm_titulo.Visible = True
            lbl_cpm_cargar_info.Visible = True
            tct_cpm_carga_informacion.Visible = True
            lbl_cpm_calculo_potencial.Visible = True
            lbl_cpm_pmm.Visible = True
            lbl_cpm_pmnm.Visible = True
            'M_RAS_PotencialMineroMetalico.Visible = True
            'M_RAS_PotencialMineroNoMetalico.Visible = True
            btn_M_RAS_PotencialMineroMetalico.Visible = True
            btn_M_RAS_PotencialMineroNoMetalico.Visible = True
            btn_cpm_calcular_potencial.Visible = True
            btn_cpm_open_folder.Visible = True
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Function validatFeatures()
        'workspace_dir = tbx_cpm_directorio.Text
        'If workspace_dir Is Nothing Then
        '    Throw New Exception("Debe especificar un directorio como espacio de trabajo")
        'End If

        'Dim id_geodatabase = (CType(cbx_cpm_departamento_proy.SelectedItem, KeyValuePair(Of String, String))).Key
        'If id_geodatabase = -1 Then
        '    Throw New Exception("Debe seleccionar una geodatabase")
        'End If
        'Realiza una preconfiguracion de la capa (define src, reproyecta, etc)
        params.Clear()
        params.Add(Nothing)
        params.Add(Nothing)
        params.Add("true")
        Dim response As String = ExecuteGP(_tool_loadProjectCPM, params, _toolboxPath_calculo_potencialminero, getresult:=True, showCancel:=False)
        'Si el proceso falla entonces muestra un mensaje 
        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            Throw New Exception(RuntimeError.VisualError)
            Return Nothing
        End If

        'Si el proceso no falla se obtiene el resultado de la operacion
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)


        'Si el proceso tiene como respuesta el valor 0, quiere decir que se ha generado un problema en el script de python
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            Throw New Exception(RuntimeError.VisualError)
            Return Nothing
        End If

        For Each current In responseJson.Item("response")
            Dim btn As Object = Me.Controls.Find(current(0).ToString(), True).FirstOrDefault()
            If current(1) = 1 Then
                btn.ImageIndex = current(1)
                btn.visible = True
            Else
                btn.visible = False
            End If
        Next
    End Function

    Private Sub btn_M_01_GPO_Litologia_Click(sender As Object, e As EventArgs) Handles btn_M_01_GPO_Litologia.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_pmmLitologia, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub btn_M_02_GPL_FallaGeologica_Click(sender As Object, e As EventArgs) Handles btn_M_02_GPL_FallaGeologica.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_pmmFallasGeologicas, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub btn_M_03_GPO_DepositoMineral_Click(sender As Object, e As EventArgs) Handles btn_M_03_GPO_DepositoMineral.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_pmmDepositosMinerales, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_M_VAR_RAS_Geoquimica_Click(sender As Object, e As EventArgs) Handles btn_M_VAR_RAS_Geoquimica.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_pmmGeoquimica, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_M_05_GPO_SensorRemoto_Click(sender As Object, e As EventArgs) Handles btn_M_05_GPO_SensorRemoto.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_pmmSensoresRemotos, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_RMI_06_GPO_Litologia_Click(sender As Object, e As EventArgs) Handles btn_RMI_06_GPO_Litologia.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_rmiLitologia, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_RMI_07_GPT_Sustancias_Click(sender As Object, e As EventArgs) Handles btn_RMI_07_GPT_Sustancias.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_rmiSustancias, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_RMI_09_GPO_SensorRemoto_Click(sender As Object, e As EventArgs) Handles btn_RMI_09_GPO_SensorRemoto.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_rmiSensoresremotos, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_RMI_10_GPL_Accesos_Click(sender As Object, e As EventArgs) Handles btn_RMI_10_GPL_Accesos.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_rmiAccesos, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_M_RAS_PotencialMineroMetalico_Click(sender As Object, e As EventArgs) Handles btn_M_RAS_PotencialMineroMetalico.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_calculatePmm, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_M_RAS_PotencialMineroNoMetalico_Click(sender As Object, e As EventArgs) Handles btn_M_RAS_PotencialMineroNoMetalico.Click
        Try
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_calculateRmi, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
            validatFeatures()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_cpm_calcular_potencial_Click(sender As Object, e As EventArgs) Handles btn_cpm_calcular_potencial.Click
        Try
            If M_RAS_PotencialMineroMetalico.Visible = False Or M_RAS_PotencialMineroNoMetalico.Visible = False Then
                MessageBox.Show("Debe realizar el cálculo del potencial minero metálico y no metálico antes de seguir con esta operación", __title__, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            'Ejecutar proceso de verificacion de base de datos
            Cursor.Current = Cursors.WaitCursor
            GPToolDialog(_tool_PotencialMinero, modal:=True, tbxpath:=_toolboxPath_calculo_potencialminero)
            runProgressBar()
        Catch ex As Exception
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            runProgressBar("ini")
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub btn_cpm_open_folder_Click(sender As Object, e As EventArgs) Handles btn_cpm_open_folder.Click
        Try
            workspace_dir = tbx_cpm_directorio.Text
            If workspace_dir Is Nothing Or workspace_dir = "" Then
                Throw New Exception("Debe especificar un directorio como espacio de trabajo")
            End If
            Process.Start(workspace_dir)
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub

    Private Sub tbx_cpm_directorio_TextChanged(sender As Object, e As EventArgs) Handles tbx_cpm_directorio.TextChanged
        lbl_cpm_titulo.Visible = False
        lbl_cpm_cargar_info.Visible = False
        tct_cpm_carga_informacion.Visible = False
        lbl_cpm_calculo_potencial.Visible = False
        lbl_cpm_pmm.Visible = False
        lbl_cpm_pmnm.Visible = False
        M_RAS_PotencialMineroMetalico.Visible = False
        M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_M_RAS_PotencialMineroMetalico.Visible = False
        btn_M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_cpm_calcular_potencial.Visible = False
        btn_cpm_open_folder.Visible = False
    End Sub
    Private Sub cbx_cpm_departamento_proy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_cpm_departamento_proy.SelectedIndexChanged
        lbl_cpm_titulo.Visible = False
        lbl_cpm_cargar_info.Visible = False
        tct_cpm_carga_informacion.Visible = False
        lbl_cpm_calculo_potencial.Visible = False
        lbl_cpm_pmm.Visible = False
        lbl_cpm_pmnm.Visible = False
        M_RAS_PotencialMineroMetalico.Visible = False
        M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_M_RAS_PotencialMineroMetalico.Visible = False
        btn_M_RAS_PotencialMineroNoMetalico.Visible = False
        btn_cpm_calcular_potencial.Visible = False
        btn_cpm_open_folder.Visible = False
    End Sub
End Class