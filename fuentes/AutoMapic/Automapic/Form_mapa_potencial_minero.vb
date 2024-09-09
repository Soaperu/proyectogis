Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports System.Data.SQLite
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriUnits
'Imports ESRI.ArcGIS.ArcMapUI
'Imports System.Drawing

Public Class Form_mapa_potencial_minero
    Dim params As New List(Of Object)
    Dim path_shapefile As String
    Dim sheet_orientation As String
    Dim sheet_scale As Integer
    Dim sheet_size As String
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim revisoresDict As New Dictionary(Of String, String)
    'Dim controller_document As Integer = 98
    'Dim controller_document_copy As Integer
    'Private m_documentEvents As IDocumentEvents_Event
    Dim report_xls As String
    Dim urlDashboard As String = "https://geocatmin.ingemmet.gob.pe/portal/apps/dashboards/80cb99a98b784557b86f5e677fa85c75"


    Private Sub llbl_mpm_otras_opc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbl_mpm_otras_opc.LinkClicked
        If pnl_mpm_otras_opc.Visible Then
            pnl_mpm_otras_opc.Visible = False
            llbl_mpm_otras_opc.Text = "Más opciones..."
        Else
            pnl_mpm_otras_opc.Visible = True
            llbl_mpm_otras_opc.Text = "Ocultar opciones..."
        End If
    End Sub

    Private Sub btn_mpm_load_Click(sender As Object, e As EventArgs) Handles btn_mpm_load.Click
        report_xls = Nothing

        'Abre el explorador de archivos de arcmap y permite seleccionar un shapefile
        path_shapefile = openDialogBoxESRI(f_shapefile)
        If path_shapefile Is Nothing Then
            Return
        End If

        'runProgressBar()
        params.Clear()
        params.Add(path_shapefile)

        'Realiza una preconfiguracion de la capa (define src, reproyecta, etc)
        Dim response As String = ExecuteGP(_tool_preProcessingMp, params, _toolboxPath_mapa_potencialminero, getresult:=True, showCancel:=True)

        'Si el proceso falla entonces muestra un mensaje 
        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.VisualError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'runProgressBar("ini")
            Return
        End If

        'Si el proceso no falla se obtiene el resultado de la operacion
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)


        'Si el proceso tiene como respuesta el valor 0, quiere decir que se ha generado un problema en el script de python
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'runProgressBar("ini")
            Return
        End If

        If responseJson.Item("status") = 99 Then
            MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'runProgressBar("ini")
            Return
        End If

        'Si no se generaron errores, se rescatan todos los resultados obtenidos
        'Ubicacion del archivo procesado
        path_shapefile = responseJson.Item("response").item("feature")
        'Orientacion del mapa (vertical (v), horizontal(h))
        sheet_orientation = responseJson.Item("response").item("orientation")
        'Escala del mapa
        sheet_scale = responseJson.Item("response").item("scale")
        'Tamanio de la hoja (a3, a4)
        sheet_size = responseJson.Item("response").item("size")

        'Se configura el formulario segun los resultados
        'Orientacion
        If sheet_orientation = "v" Then
            rbt_mpm_vertical.Checked = True
        Else
            rbt_mpm_horizontal.Checked = True
        End If

        'Escala
        nud_mpm_escala.Value = sheet_scale

        'Tamanio de la hoja
        If sheet_size = "a3" Then
            rbt_mpm_a3.Checked = True
        Else
            rbt_mpm_a4.Checked = True
        End If

        'Ubicacion del archivo shapefile
        tbx_mpm_archivo.Text = path_shapefile

        'Se ejecuta el proceso para agregar el archivo procesado al mapa
        params.Clear()
        params.Add(path_shapefile)
        ExecuteGP(_tool_addFeatureToMap, params, _toolboxPath_automapic, getresult:=False)

        'runProgressBar("ini")
    End Sub

    Private Sub Form_mapa_potencial_minero_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim mxDocument As IMxDocument = DirectCast(My.ArcMap.Application.Document, IMxDocument)
        'm_documentEvents = DirectCast(mxDocument, IDocumentEvents_Event)
        'AddHandler m_documentEvents.OpenDocument, AddressOf OpenDocument
        'AddHandler m_documentEvents.BeforeCloseDocument, AddressOf CloseDocument

        'El nombre del autor por defecto es el nombre del usuario que inicio sesion
        tbx_mpm_autor.Text = nameUser

        'Realiza la conexion a la base de datos sqlite
        'Dim connection As String = "Data Source=" & _path_sqlite
        'Dim SQLConn As New SQLiteConnection(connection)
        'Dim SQLcmd As New SQLiteCommand(SQLConn)
        ''Dim SQLdr As SQLiteDataReader
        'SQLConn.Open()

        'Query que obtiene el nombre de los revisores
        'Dim SQLstr_revisores As String = "select B.user, B.nombres||' '||B.apepat revisor from tb_access as A left join tb_user as B on A.id_user = B.id_user where A.id_modulo = 9 and A.id_perfil in (2, 3, 4);"
        Dim SQLstr_revisores As String = "select B.usuario, B.nombres||' '||B.apepat revisor  from ugeo1749.tb_osi_aut_access A left join ugeo1749.tb_osi_aut_usuarios B on A.Id_Usuario = B.Id_Usuario where A.id_modulo = 9 and A.id_perfil IN (3, 4)"
        'Obtiene el nombre de los revisores desde sqlite 
        'Los revisores solo pueden ser usuarios que tengan el rol administrador (2)

        Dim dt = New DataTable()

        dt = SelectSqlcommand(SQLstr_revisores)
        modulosDict.Clear()
        For Each row As DataRow In dt.Rows
            revisoresDict.Add(row(0).ToString(), row(1).ToString())
        Next

        'SQLcmd.CommandText = SQLstr_revisores
        'SQLdr = SQLcmd.ExecuteReader()
        'revisoresDict.Clear()
        'While SQLdr.Read()
        '    revisoresDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
        'End While
        'SQLdr.Close()

        'Carga los nombres de los revisores en el combobox cbx_mpm_revisores
        cbx_mpm_revisores.DataSource = New BindingSource(revisoresDict, Nothing)
        cbx_mpm_revisores.DisplayMember = "Value"
        cbx_mpm_revisores.ValueMember = "Key"

        'Configura el mapa actual en metros
        Dim pMap As IMap
        pMap = My.ArcMap.Document.FocusMap
        pMap.DistanceUnits = esriMeters
        pMap.MapUnits = esriMeters

        'Configuracion segun Rol de usuario
        'Si el usuario es visualizador (1) no debe poder acceder a la funcionalidad de
        ' - Generar mapa
        ' - Eliminar mapa
        ' - Reportes
        ' - Reporte general
        If modulosPerfilDict.Item(currentModule) = 1 Then
            btn_mpm_generar_mapa.Enabled = False
            btn_mpm_delete_map.Enabled = False
            btn_mpm_reportes.Enabled = False
            btn_mpm_reporte_general.Enabled = False
            gbx_mpm_informacion.Enabled = False
        End If

    End Sub

    Private Sub SetMxdScale(mxd As String, name_scale As String, Optional ByVal unit_label As String = Nothing, Optional ByVal division As Integer = Nothing, Optional ByVal units As String = Nothing)
        Dim mapDoc As IMapDocument = New MapDocument()
        mapDoc.Open(mxd)

        Dim IMap = mapDoc.ActiveView.FocusMap

        For index As Integer = 0 To IMap.MapSurroundCount - 1
            Dim element = IMap.MapSurround(index)
            If element.Name = name_scale Then
                Dim ScaleBar As IScaleBar = element
                If unit_label IsNot Nothing Then
                    ScaleBar.UnitLabel = unit_label
                End If
                If unit_label IsNot Nothing Then
                    ScaleBar.Division = division
                End If
                If unit_label IsNot Nothing Then
                    ScaleBar.Units = units
                End If
                Exit For
            End If
        Next

        Dim activeView = mapDoc.ActiveView
        Dim pageLayout As IPageLayout = activeView
        Dim graphicContainer As IGraphicsContainer = pageLayout
        Dim frameElement = graphicContainer.FindFrame(IMap)
        Dim mapFrame As IMapFrame = frameElement
        Dim mapGrids As IMapGrids = mapFrame
        Dim mapGridsList As New List(Of Integer)
        For index As Integer = 0 To mapGrids.MapGridCount - 1
            mapGridsList.Add(Convert.ToInt32(mapGrids.MapGrid(index).Name))
        Next
        mapGridsList.Sort()
        Dim mapGridsListFilter = mapGridsList.FindAll(Function(p) p < nud_mpm_escala.Value)
        Dim lastElement = mapGridsList(mapGridsListFilter.Count - 1)

        For index As Integer = 0 To mapGrids.MapGridCount - 1
            If mapGrids.MapGrid(index).Name = lastElement.ToString() Then
                mapGrids.MapGrid(index).Visible = True
            Else
                mapGrids.MapGrid(index).Visible = False
            End If
        Next
        mapDoc.Save()

    End Sub

    Private Function generate_map(state As Integer)
        Try
            If tbx_mpm_archivo.Text = "" Then
                MessageBox.Show("Debe agregrar un archivo shapefile", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If
            If state = 1 Then
                If cbx_mpm_formato.Text = "" Then
                    MessageBox.Show("Debe seleccionar el tipo de formato de origen", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return 0
                End If
            End If

            If tbx_mpm_autor.Text = "" Then
                MessageBox.Show("Debe ingresar el nombre del autor", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If


            If tbx_mpm_nombre_mapa.Text = "" Then
                MessageBox.Show("Debe ingresar un nombre de mapa", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If tbx_mpm_documento.Text = "" Then
                MessageBox.Show("Debe ingresar el documento", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return 0
            End If

            If state = 1 Then
                Dim r = MessageBox.Show("¿Desea generar el mapa de potencial minero?. Recuerde que los datos serán registrados en la Base de Datos", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If r = DialogResult.No Then
                    Return 0
                End If
            End If

            If state = 2 Then
                Dim r = MessageBox.Show("¿Desea registrar un mapa sin informacón?. Recuerde que los datos serán registrados en la Base de Datos", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If r = DialogResult.No Then
                    Return 0
                End If
            End If

            'Cursor.Current = Cursors.WaitCursor
            runProgressBar()
            params.Clear()

            params.Add(path_shapefile)

            If cbx_mpm_formato.Text = "" Then
                cbx_mpm_formato.Text = "shp"
            End If
            params.Add(cbx_mpm_formato.Text)
            params.Add(tbx_mpm_autor.Text)
            params.Add(cbx_mpm_revisores.Text)
            params.Add(tbx_mpm_nombre_mapa.Text)
            params.Add(tbx_mpm_documento.Text)
            params.Add(nud_mpm_escala.Value)


            If rbt_mpm_vertical.Checked = True Then
                params.Add("v")
            Else
                params.Add("h")
            End If

            If rbt_mpm_a3.Checked = True Then
                params.Add("a3")
            Else
                params.Add("a4")
            End If

            params.Add(state)


            Dim response As String = ExecuteGP(_tool_generateMapPotencial, params, _toolboxPath_mapa_potencialminero, showCancel:=True)

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                report_xls = Nothing
                MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                runProgressBar("ini")
                Return 0
            End If
            If responseJson.Item("status") = 99 Then
                MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
                runProgressBar("ini")
                Return 0
            End If

            If state = 1 Then
                report_xls = responseJson.Item("report")
            Else
                report_xls = Nothing
            End If

            If state = 0 Or state = 1 Then
                SetMxdScale(
                    responseJson.Item("mxd"),
                    name_scale:=responseJson.Item("scale_params").Item("name_scale"),
                    unit_label:=responseJson.Item("scale_params").Item("UnitLabel"),
                    division:=responseJson.Item("scale_params").Item("Division"),
                    units:=responseJson.Item("scale_params").Item("Units")
                )

                Dim mxd_path As String = responseJson.Item("mxd")
                'controller_document = 2
                'If state = 0 Then
                '    controller_document_copy = 99
                'Else
                '    controller_document_copy = 1
                'End If
                My.ArcMap.Application.OpenDocument(mxd_path)
            Else
                MessageBox.Show("Se registro la atención satisfactoriamente", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return 1
        Catch ex As Exception
            'controller_document = 98
            report_xls = Nothing
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return 0
        End Try
    End Function

    Private Sub btn_mpm_generar_mapa_Click(sender As Object, e As EventArgs) Handles btn_mpm_generar_mapa.Click
        Dim state_map As Integer
        If rbt_mpm_coninfo.Checked Then
            state_map = 1
        Else
            state_map = 2
        End If
        generate_map(state_map)
        'Dim r As Integer = generate_map(state_map)
        'If r = 0 Then
        '    Return
        'End If
    End Sub

    'Private Sub OpenDocument()
    '    controller_document = controller_document_copy
    'End Sub

    'Function CloseDocument() As Boolean
    '    If controller_document = 2 Or controller_document = 1 Or controller_document = 98 Then
    '        Return False
    '    End If
    '    Dim response = MessageBox.Show("No ha generado el mapa definitivo, ¿está seguro que desea cerrar la aplicación?", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '    If response = DialogResult.No Then
    '        Return True
    '    End If
    '    Return False
    'End Function

    Private Sub btn_mpm_generar_mapa_vp_Click(sender As Object, e As EventArgs) Handles btn_mpm_generar_mapa_vp.Click
        'If controller_document = 99 Then
        '    Dim response = MessageBox.Show("No ha generado el mapa definitivo, ¿está seguro que desea continuar?", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        '    If response = DialogResult.No Then
        '        Return
        '    End If
        'End If
        generate_map(0)
        'Dim r As Integer = generate_map(0)
        'If r = 0 Then
        '    Return
        'End If
    End Sub

    Private Sub btn_mpm_delete_map_Click(sender As Object, e As EventArgs) Handles btn_mpm_delete_map.Click
        Dim r = MessageBox.Show("Esta sección permite eliminar un mapa generado por código, ¿está seguro que desea continuar?", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If r = DialogResult.No Then
            Return
        End If
        Dim mapa_potencial_minero_eliminar_registro = New Form_mapa_potencial_minero_eliminar_registro()
        mapa_potencial_minero_eliminar_registro.ShowDialog()
    End Sub

    Private Sub btn_mpm_reportes_Click(sender As Object, e As EventArgs) Handles btn_mpm_reportes.Click
        If report_xls Is Nothing Then
            MessageBox.Show("Los reportes se crean cuando se genera un mapa definivo", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Process.Start(report_xls)
    End Sub

    Private Sub btn_mpm_dashboard_Click(sender As Object, e As EventArgs) Handles btn_mpm_dashboard.Click
        Process.Start(urlDashboard)
    End Sub

    Private Sub btn_mpm_reporte_general_Click(sender As Object, e As EventArgs) Handles btn_mpm_reporte_general.Click
        Cursor.Current = Cursors.WaitCursor
        runProgressBar()
        params.Clear()

        Dim response As String = ExecuteGP(_tool_generalReportMp, params, _toolboxPath_mapa_potencialminero)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            report_xls = Nothing
            MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If

        Process.Start(responseJson.Item("response"))
        runProgressBar("ini")
    End Sub

    Private Sub rbt_mpm_sininfo_Click(sender As Object, e As EventArgs) Handles rbt_mpm_sininfo.Click
        If rbt_mpm_sininfo.Checked Then
            btn_mpm_generar_mapa.Text = "Registrar atención"
        End If
    End Sub

    Private Sub rbt_mpm_coninfo_Click(sender As Object, e As EventArgs) Handles rbt_mpm_coninfo.Click
        If rbt_mpm_coninfo.Checked Then
            btn_mpm_generar_mapa.Text = "Generar mapa"
        End If
    End Sub

    Private Sub tbx_mpm_archivo_TextChanged(sender As Object, e As EventArgs) Handles tbx_mpm_archivo.TextChanged

    End Sub

    Private Sub cbx_mpm_formato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mpm_formato.SelectedIndexChanged

    End Sub
End Class