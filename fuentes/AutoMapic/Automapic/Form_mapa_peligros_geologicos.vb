Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form_mapa_peligros_geologicos
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim path_shapefile As String
    Dim distritos_container As New List(Of String)
    Dim departamentos_container As New Dictionary(Of String, String)
    Dim params As New List(Of Object)
    Dim layer As String
    Dim toggleTool As Boolean = False
    Dim maptype As String = Nothing
    Dim xmin As Double
    Dim ymin As Double
    Dim xmax As Double
    Dim ymax As Double
    Dim tableHist As New DataTable
    Dim form_registro = New Form_mapa_peligros_geologicos_registro()
    Dim form_reportes = New Form_mapa_peligros_geologicos_atenciones()
    Dim contador As Integer = 0
    Dim contador_atencion As Integer = 0
    Private Shared _instance As Form_mapa_peligros_geologicos
    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Shared Function GetInstance() As Form_mapa_peligros_geologicos

        If _instance Is Nothing Then
            _instance = New Form_mapa_peligros_geologicos()
        End If

        Return _instance

    End Function
    Private Sub Form_mapa_peligros_geologicos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbx_autor_pg.Text = nameUser

        tableHist.Columns.Add("id", GetType(Integer))
        tableHist.Columns.Add("x", GetType(Double))
        tableHist.Columns.Add("y", GetType(Double))
        tableHist.Columns.Add("sector", GetType(String))
        tableHist.Columns.Add("zona", GetType(Integer))

        If modulosPerfilDict.Item(currentModule) = 3 Then
            btn_pg_reportes.Enabled = True
            btn_pg_registro.Enabled = True
        End If
        'Dim rowId As Integer = DataGridView1.Rows.Add()
        'Dim row As DataGridViewRow = DataGridView1.Rows(rowId)
        'row.Cells("id").Value = "1"
        'row.Cells("x").Value = "76.962"
        'row.Cells("y").Value = "11.173"
        'row.Cells("sector").Value = "Primavera 1"

        'Dim rowId2 As Integer = DataGridView1.Rows.Add()
        'Dim row2 As DataGridViewRow = DataGridView1.Rows(rowId2)
        'row2.Cells("id").Value = "1"
        'row2.Cells("x").Value = "75.542"
        'row2.Cells("y").Value = "12.122"
        'row2.Cells("sector").Value = "Salto del fraile"

        departamentos_container.Add("99", "Nacional")
    End Sub
    Private Sub btn_loadshp_Click(sender As Object, e As EventArgs) Handles btn_loadshp.Click
        Cursor.Current = Cursors.WaitCursor
        Dim path_shapefile_temp = openDialogBoxESRI(f_shapefile)
        If path_shapefile_temp Is Nothing Then
            Return
        End If
        path_shapefile = path_shapefile_temp
        If contador_atencion > 0 Then
            Dim r1 As DialogResult = MessageBox.Show("¿El área a cargar pertenece a una nueva solicitud?", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r1 = DialogResult.No Then

            Else
                departamentos_container.Clear()
                departamentos_container.Add("99", "Nacional")
                distritos_container.Clear()
            End If
        End If
        runProgressBar()
        tbx_pathshp.Text = path_shapefile
        params.Clear()
        params.Add(path_shapefile)
        ExecuteGP(_tool_addFeatureToMap, params, _toolboxPath_automapic, getresult:=False)

        btn_generar_mapa_pg.Enabled = True
        If tbx_xmin.Text <> 0 Then
            Dim r As DialogResult = MessageBox.Show("Desea mantener la extensión especificada", __title__, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = DialogResult.No Then
                tbx_xmin.Text = 0
                tbx_ymin.Text = 0
                tbx_xmax.Text = 0
                tbx_ymax.Text = 0
            End If
        End If
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
    End Sub

    Public Sub ToggleDataView()
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        If TypeOf pMxDoc.ActiveView Is IPageLayout Then
            pMxDoc.ActiveView = pMxDoc.FocusMap
        Else
            Return
        End If
    End Sub

    Private Sub btn_draw_Click(sender As Object, e As EventArgs) Handles btn_draw.Click
        Cursor.Current = Cursors.WaitCursor
        If toggleTool Then
            My.ArcMap.Application.CurrentTool = Nothing
            toggleTool = False
            btn_draw.FlatAppearance.BorderColor = Drawing.Color.Gray
            runProgressBar("ini")
            Return
        End If
        ToggleDataView()
        btn_draw.FlatAppearance.BorderColor = Drawing.Color.Red
        Dim dockWinID As UID = New UIDClass()
        dockWinID.Value = My.ThisAddIn.IDs.DrawPolygon
        Dim commandItem As ICommandItem = My.ArcMap.Application.Document.CommandBars.Find(dockWinID, False, False)
        Cursor.Current = Cursors.Default
        My.ArcMap.Application.CurrentTool = commandItem
        toggleTool = True
    End Sub

    Private Sub rbt_pg_Click(sender As Object, e As EventArgs) Handles rbt_pg.Click
        If rbt_pg.Checked Then
            tbx_detalle_pg.Enabled = False
            tbx_detalle_pg.Text = ""
        End If
    End Sub

    Private Sub rbt_zc_Click(sender As Object, e As EventArgs) Handles rbt_zc.Click
        If rbt_zc.Checked Then
            tbx_detalle_pg.Enabled = False
            tbx_detalle_pg.Text = ""
        End If
    End Sub

    Private Sub rbt_smm_Click(sender As Object, e As EventArgs) Handles rbt_smm.Click
        If rbt_smm.Checked Then
            tbx_detalle_pg.Enabled = True
        End If
    End Sub
    Private Sub rbt_sief_Click(sender As Object, e As EventArgs) Handles rbt_sief.Click
        If rbt_sief.Checked Then
            tbx_detalle_pg.Enabled = True
        End If
    End Sub

    Private Sub btn_generar_mapa_pg_Click(sender As Object, e As EventArgs) Handles btn_generar_mapa_pg.Click
        Try
            contador = contador + 1
            Cursor.Current = Cursors.WaitCursor
            runProgressBar()
            params.Clear()

            params.Add(path_shapefile)

            If rbt_pg.Checked Then
                maptype = "PG"
            ElseIf rbt_zc.Checked Then
                maptype = "ZC"
            ElseIf rbt_smm.Checked Then
                maptype = "SMM"
            ElseIf rbt_sief.Checked Then
                maptype = "SIEF"
            ElseIf rbt_pg_cp.Checked Then
                maptype = "CP"
            ElseIf rbt_pg_gm.Checked Then
                maptype = "GM"
            Else
                Throw New Exception("Debe marcar el tipo de mapa que desea generar")
                'MessageBox.Show("Debe marcar el tipo de mapa que desea generar", __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                'runProgressBar("ini")
                'Return
            End If

            params.Add(maptype)
            params.Add(tbx_title_pg.Text)
            params.Add(tbx_autor_pg.Text)
            params.Add(tbx_escala_pg.Text)
            params.Add(tbx_numero_pg.Text)
            params.Add(tbx_detalle_pg.Text)
            xmin = CDbl(Val(tbx_xmin.Text))
            params.Add(xmin)
            ymin = CDbl(Val(tbx_ymin.Text))
            params.Add(ymin)
            xmax = CDbl(Val(tbx_xmax.Text))
            params.Add(xmax)
            ymax = CDbl(Val(tbx_ymax.Text))
            params.Add(ymax)

            Dim response As String = ExecuteGP(_tool_mapGeologicalHazards, params, _toolboxPath_peligros_geologicos)

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Throw New Exception(responseJson.Item("message"))
                'MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'runProgressBar("ini")
                'Return
            End If

            Dim coords = responseJson.Item("response").Item("centroid")
            Dim zona = responseJson.Item("response").Item("zona")
            tableHist.Rows.Add(contador, coords.item(0).item(0), coords.item(0).item(1), tbx_title_pg.Text, zona.value)
            'btn_pg_registro.Enabled = True


            Dim demarcacion = responseJson.Item("response").Item("demarcation")

            'For i As Integer = 0 To demarcacion.Count - 1
            '    Dim ubigeo = demarcacion.item(i).item(0).ToString()
            '    distritos_container.Add(ubigeo)
            'Next

            For Each i As JArray In demarcacion
                distritos_container.Add(i.Item(0).ToString())
                Dim cd_depa = i.Item(0).ToString().Substring(0, 2)
                If departamentos_container.TryGetValue(cd_depa, Nothing) Then
                    Continue For
                End If
                departamentos_container.Add(cd_depa, i.Item(1).ToString())
            Next
            contador_atencion = contador_atencion + 1
            Dim mxd_path As String = responseJson.Item("response").Item("mxd")
            My.ArcMap.Application.OpenDocument(mxd_path)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try

    End Sub

    Private Sub btn_blank_extent_Click(sender As Object, e As EventArgs) Handles btn_blank_extent.Click
        tbx_xmin.Text = 0
        tbx_ymin.Text = 0
        tbx_xmax.Text = 0
        tbx_ymax.Text = 0
    End Sub

    Private Sub btn_pg_export_Click(sender As Object, e As EventArgs) Handles btn_pg_export.Click
        runProgressBar()
        Cursor.Current = Cursors.WaitCursor
        params.Clear()
        params.Add("CURRENT")

        Dim response = ExecuteGP(_tool_exportMXDToMPK, params, _toolboxPath_automapic)
        If response Is Nothing Then
            RuntimeError.VisualError = message_runtime_error
            MessageBox.Show(RuntimeError.VisualError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            runProgressBar("ini")
            Return
        End If

        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        'Si ocurrio un error durante el proceso este devuelve el primer valor como 0
        'Se imprime el error como PythonError
        If responseJson.Item("status") = 0 Then
            MessageBox.Show(responseJson.Item("message"), __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
            Return
        End If
        'response = Split(response, ";")
        ''Si ocurrio un error durante el proceso este devuelve el primer valor como 0
        ''Se imprime el error como PythonError
        'If response(0) = 0 Then
        '    RuntimeError.PythonError = response(2)
        '    MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return
        '    'Throw RuntimeError
        'End If

        Dim path_dirname As String = responseJson.Item("response") 'LTrim(response(2).ToString())
        Process.Start(path_dirname)
        Cursor.Current = Cursors.Default
        runProgressBar("ini")
        successProcess()
    End Sub

    Private Sub btn_pg_registro_Click(sender As Object, e As EventArgs) Handles btn_pg_registro.Click



        'form_registro.tableHist = tableHist
        'form_registro.departamentosDict = departamentos_container
        form_registro.distritosArray = distritos_container
        form_registro.configureFormRegistro(departamentos_container, tableHist)
        Dim navegacion_form = New navegacion()
        navegacion_form.form_current = form_registro
        navegacion_form.form_back = GetInstance()
        openFormByName(navegacion_form, Me.Parent)
        'Dim mapa_peligros_geologicos_registro = New Form_mapa_peligros_geologicos_registro()
        'openFormByName(mapa_peligros_geologicos_registro, Me.Parent)
        'Dim mapa_peligros_geologicos_registro = New Form_mapa_peligros_geologicos_registro()
        'mapa_peligros_geologicos_registro.Show()
    End Sub

    Private Sub btn_pg_reportes_Click(sender As Object, e As EventArgs) Handles btn_pg_reportes.Click
        Dim navegacion_form = New navegacion()
        navegacion_form.form_current = form_reportes
        navegacion_form.form_back = GetInstance()
        openFormByName(navegacion_form, Me.Parent)
        'Dim mapa_peligros_geologicos_atenciones = New Form_mapa_peligros_geologicos_atenciones()
        'openFormByName(mapa_peligros_geologicos_atenciones, Me.Parent)
    End Sub

    'Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
    '    If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
    '        Dim selectedRow = DataGridView1.Rows(e.RowIndex)
    '        Dim mapa_peligros_geologicos_registro = New Form_mapa_peligros_geologicos_registro()
    '        mapa_peligros_geologicos_registro.Show()
    '    End If
    'End Sub
End Class