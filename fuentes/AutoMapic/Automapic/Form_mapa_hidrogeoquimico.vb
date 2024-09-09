Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports Newtonsoft.Json
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.IO

Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports System.Drawing
Imports System.ComponentModel
Imports ESRI.ArcGIS.Maplex


Public Class Form_mapa_hidrogeoquimico
    Dim RuntimeError As AutomapicExceptions = New AutomapicExceptions()
    Dim ruta_gdb_mhq As String
    Dim ruta_gdb_mhq_graphics As String
    Dim ruta_xls_mhq As String
    Dim ruta_xlsout_mhq As String
    Dim ruta_shape_micro As String
    Dim ruta_shape_sub As String
    Dim params As New List(Of Object)
    Dim valid_gdb As Integer = 1
    Dim valid_xls As Integer = 0
    Dim csv_diagramas As String
    Dim zona_mhq As String
    Dim textos_rotulo As String
    Dim zonasDictByCombobox As New Dictionary(Of String, String)
    Dim cuencasDictByCombobox As New Dictionary(Of String, String)
    Dim subcuencasDictByCombobox As New Dictionary(Of String, String)
    Dim microcuencasDictByCombobox As New Dictionary(Of String, String)


    Dim cod_cuenca As String
    Dim cod_subcuenca As String
    Dim cod_microcuenca As String


    Private Sub btn_mhq_gdb_Click(sender As Object, e As EventArgs) Handles btn_mhq_gdb.Click

        ruta_gdb_mhq = openDialogBoxESRI(f_workspace)
        If ruta_gdb_mhq Is Nothing Then
            'Cursor.Current = Cursors.Default
            valid_gdb = 0
            btn_mhq_cargar.Enabled = False
            Return
        End If
        tbx_mhq_gdb.Text = ruta_gdb_mhq
        valid_gdb = 1
        If valid_gdb = 1 And valid_xls = 1 Then
            btn_mhq_cargar.Enabled = True
        End If


    End Sub

    Private Sub btn_mhq_excel_Click(sender As Object, e As EventArgs) Handles btn_mhq_excel.Click

        'OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Dim validador = OpenFileDialog1.ShowDialog()
        ruta_xls_mhq = OpenFileDialog1.FileName

        If validador = 2 Then
            'Cursor.Current = Cursors.Default
            valid_xls = 0
            btn_mhq_cargar.Enabled = False
            Return
        End If
        tbx_mhq_excel.Text = ruta_xls_mhq
        btn_mhq_calc_xls.Enabled = True
        valid_xls = 1
        If valid_gdb = 1 And valid_xls = 1 Then
            btn_mhq_cargar.Enabled = True
        End If
    End Sub

    Private Sub btn_mhq_cargar_Click(sender As Object, e As EventArgs) Handles btn_mhq_cargar.Click
        runProgressBar()

        params.Clear()
        params.Add(ruta_gdb_mhq)
        params.Add(ruta_xls_mhq)


        Dim response = ExecuteGP(_tool_insertarDatosGdb, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar("ini")
    End Sub

    Private Sub btn_mhq_calc_xls_Click(sender As Object, e As EventArgs) Handles btn_mhq_calc_xls.Click
        Dim validador = SaveFileDialog1.ShowDialog()
        ruta_xlsout_mhq = SaveFileDialog1.FileName

        If validador = 2 Then
            'Cursor.Current = Cursors.Default
            Return
        End If

        runProgressBar()

        params.Clear()
        params.Add(ruta_xls_mhq)
        params.Add(ruta_xlsout_mhq)

        Dim response = ExecuteGP(_tool_calculoAnionesXls, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar("ini")
    End Sub

    Private Sub Form_mapa_hidrogeoquimico_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tc_mhq_procesos.SelectedIndex = 1
        cuencasDictByCombobox.Clear()
        zonasDictByCombobox.Clear()

        ' agregamos valores a ucl autores
        ucl_mhq_autores.populateChekListBox(_tool_getAutoresMhq, _toolboxPath_mapa_hidrogeoquimico, params)


        'runProgressBar()
        'Cursor.Current = Cursors.WaitCursor
        RemoveHandler cbx_mhq_cuenca.SelectedIndexChanged, AddressOf cbx_mhq_cuenca_SelectedIndexChanged
        params.Clear()
        Dim response = ExecuteGP(_tool_getCodeCuencas, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            ruta_gdb_mhq = responseJson.Item("gdb")
            tbx_mhq_gdb.Text = ruta_gdb_mhq
            valid_gdb = 1
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If

        'Seteamos texto para gdb ingreso de datos
        If responseJson.Item("gdb") IsNot Nothing Then
            ruta_gdb_mhq = responseJson.Item("gdb")
            tbx_mhq_gdb.Text = ruta_gdb_mhq
            valid_gdb = 1
        End If

        For Each current In responseJson.Item("response")
            cuencasDictByCombobox.Add(current(0), current(1))
        Next
        cbx_mhq_cuenca.DataSource = New BindingSource(cuencasDictByCombobox, Nothing)
        cbx_mhq_cuenca.DisplayMember = "Value"
        cbx_mhq_cuenca.ValueMember = "Key"

        AddHandler cbx_mhq_cuenca.SelectedIndexChanged, AddressOf cbx_mhq_cuenca_SelectedIndexChanged
        'Cursor.Current = Cursors.Default
        'runProgressBar("ini")

        If cuencasDictByCombobox.Count > 1 Then
            cbx_mhq_cuenca.SelectedIndex = 1
        Else
            cbx_mhq_cuenca.SelectedIndex = 0
        End If


        ' agregamos valores a combobox zona
        RemoveHandler cbx_mhq_zona.SelectedIndexChanged, AddressOf cbx_mhq_zona_SelectedIndexChanged

        zonasDictByCombobox.Add("17", "17 S")
        zonasDictByCombobox.Add("18", "18 S")
        zonasDictByCombobox.Add("19", "19 S")

        'cbx_mhq_zona.DataSource = Nothing
        cbx_mhq_zona.DataSource = New BindingSource(zonasDictByCombobox, Nothing)
        cbx_mhq_zona.DisplayMember = "Value"
        cbx_mhq_zona.ValueMember = "Key"
        AddHandler cbx_mhq_zona.SelectedIndexChanged, AddressOf cbx_mhq_zona_SelectedIndexChanged

        cbx_mhq_zona.SelectedIndex = 1
        zona_mhq = cbx_mhq_zona.SelectedValue
        btn_mhq_generar_mapa.Enabled = True
    End Sub

    Private Sub cbx_mhq_cuenca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mhq_cuenca.SelectedIndexChanged
        subcuencasDictByCombobox.Clear()

        cod_cuenca = cbx_mhq_cuenca.SelectedValue
        cod_subcuenca = cbx_mhq_subcuenca.SelectedValue
        cod_microcuenca = cbx_mhq_microcuenca.SelectedValue

        'runProgressBar()
        'Cursor.Current = Cursors.WaitCursor
        RemoveHandler cbx_mhq_subcuenca.SelectedIndexChanged, AddressOf cbx_mhq_subcuenca_SelectedIndexChanged

        params.Clear()
        params.Add(cod_cuenca)
        Dim response = ExecuteGP(_tool_getCodeCuencas, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        For Each current In responseJson.Item("response")
            subcuencasDictByCombobox.Add(current(0), current(1))
        Next
        cbx_mhq_subcuenca.DataSource = New BindingSource(subcuencasDictByCombobox, Nothing)
        cbx_mhq_subcuenca.DisplayMember = "Value"
        cbx_mhq_subcuenca.ValueMember = "Key"

        AddHandler cbx_mhq_subcuenca.SelectedIndexChanged, AddressOf cbx_mhq_subcuenca_SelectedIndexChanged
        'Cursor.Current = Cursors.Default
        'runProgressBar("ini")

        If subcuencasDictByCombobox.Count > 1 Then
            cbx_mhq_subcuenca.SelectedIndex = 1
        Else
            'cbx_mhq_subcuenca.SelectedIndex = 0
            cbx_mhq_subcuenca.DataSource = Nothing
            cbx_mhq_microcuenca.DataSource = Nothing

        End If
    End Sub

    Private Sub cbx_mhq_subcuenca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mhq_subcuenca.SelectedIndexChanged
        microcuencasDictByCombobox.Clear()

        cod_cuenca = cbx_mhq_cuenca.SelectedValue
        cod_subcuenca = cbx_mhq_subcuenca.SelectedValue
        cod_microcuenca = cbx_mhq_microcuenca.SelectedValue

        'runProgressBar()
        'Cursor.Current = Cursors.WaitCursor
        RemoveHandler cbx_mhq_microcuenca.SelectedIndexChanged, AddressOf cbx_mhq_microcuenca_SelectedIndexChanged

        params.Clear()
        params.Add(cod_cuenca)
        params.Add(cod_subcuenca)

        Dim response = ExecuteGP(_tool_getCodeCuencas, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
            runProgressBar("ini")
            Return
        End If
        For Each current In responseJson.Item("response")
            microcuencasDictByCombobox.Add(current(0), current(1))
        Next
        cbx_mhq_microcuenca.DataSource = New BindingSource(microcuencasDictByCombobox, Nothing)
        cbx_mhq_microcuenca.DisplayMember = "Value"
        cbx_mhq_microcuenca.ValueMember = "Key"

        AddHandler cbx_mhq_microcuenca.SelectedIndexChanged, AddressOf cbx_mhq_microcuenca_SelectedIndexChanged
        'Cursor.Current = Cursors.Default
        'runProgressBar("ini")
        If microcuencasDictByCombobox.Count > 1 Then
            cbx_mhq_microcuenca.SelectedIndex = 1
        Else
            cbx_mhq_microcuenca.SelectedIndex = 0
        End If

        ' cambiamos el valor de subcuenca del rotulo
        Dim strArr() As String
        Dim textotemp As String
        textotemp = cbx_mhq_subcuenca.Text
        strArr = textotemp.Split("-")
        tbx_mhq_rotulo_subcuenca.Text = UCase(strArr(0))
    End Sub

    Private Sub cbx_mhq_microcuenca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mhq_microcuenca.SelectedIndexChanged
        cod_cuenca = cbx_mhq_cuenca.SelectedValue
        cod_subcuenca = cbx_mhq_subcuenca.SelectedValue
        cod_microcuenca = cbx_mhq_microcuenca.SelectedValue
    End Sub

    Private Sub gdb_to_csv()
        runProgressBar()

        params.Clear()

        Dim response = ExecuteGP(_tool_FeatureClasstoCSV, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar("ini")

        csv_diagramas = responseJson.Item("response")
    End Sub

    Private Sub generar_graficos_mhq()

        Dim response
        Dim responseJson
        params.Clear()
        params.Add(csv_diagramas)
        params.Add(cod_cuenca)
        params.Add(cod_subcuenca)
        params.Add(cod_microcuenca)
        ' Diagrama de gibbs
        If chx_mhq_gibbs.Checked Then

            runProgressBar()
            response = ExecuteGP(_tool_gibbsDiagram, params, _toolboxPath_mapa_hidrogeoquimico)
            responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

                runProgressBar("ini")
                'Cursor.Current = Cursors.Default
                Return
            End If
            runProgressBar("ini")
        End If


        ' Diagrama de piper

        If chx_mhq_piper.Checked Then

            runProgressBar()
            response = ExecuteGP(_tool_piperDiagram, params, _toolboxPath_mapa_hidrogeoquimico)
            responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                RuntimeError.PythonError = responseJson.Item("message")
                MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

                runProgressBar("ini")
                'Cursor.Current = Cursors.Default
                Return
            End If
            runProgressBar("ini")

        End If
    End Sub


    Private Sub AgregarImagenes(pathimg As String, xmin As Integer, ymin As Integer, xmax As Integer, ymax As Integer)
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        If TypeOf pMxDoc.ActiveView IsNot IPageLayout Then
            pMxDoc.ActiveView = pMxDoc.PageLayout
        End If

        pMxDoc.PageLayout.Page.Units = ESRI.ArcGIS.esriSystem.esriUnits.esriCentimeters
        pMxDoc.ActivatedView.Refresh()

        Dim graphicsContainer As IGraphicsContainer = pMxDoc.PageLayout
        Dim PictureElement As IPictureElement = New JpgPictureElement()

        Dim pEnv As IEnvelope = New Envelope()
        'pEnv.PutCoords(0, 0, 16.2, 15.0)
        pEnv.PutCoords(xmin, ymin, xmax, ymax)

        PictureElement.ImportPictureFromFile(pathimg)
        Dim IElement As IElement = PictureElement
        IElement.Geometry = pEnv
        graphicsContainer.AddElement(IElement, 0)

        pMxDoc.ActivatedView.Refresh()
    End Sub

    Private Sub btn_mhq_ggraf_Click(sender As Object, e As EventArgs) Handles btn_mhq_ggraf.Click
        'cuenca = "Alto Apurímac"
        'subcuenca = "Hornillos Alto"
        'microcuenca = "Apurímac 2"
        'cuenca = "134"
        'subcuenca = "13491"
        'microcuenca = "13491_02"
        'cuenca = "134"
        'subcuenca = "13491"
        'microcuenca = "13491_02"
        gdb_to_csv()
        generar_graficos_mhq()

    End Sub

    Private Sub btn_mhq_shape_Click(sender As Object, e As EventArgs) Handles btn_mhq_shape_micro.Click
        ruta_shape_micro = openDialogBoxESRI(f_shapefile)
        If ruta_shape_micro Is Nothing Then
            'Cursor.Current = Cursors.Default
            Return
        End If
        tbx_mhq_shp_micro.Text = ruta_shape_micro

    End Sub

    Private Sub btn_mhq_shape_sub_Click(sender As Object, e As EventArgs) Handles btn_mhq_shape_sub.Click
        ruta_shape_sub = openDialogBoxESRI(f_shapefile)
        If ruta_shape_sub Is Nothing Then
            'Cursor.Current = Cursors.Default
            Return
        End If
        tbx_mhq_shp_sub.Text = ruta_shape_sub
    End Sub

    Private Sub btn_mhq_generar_mapa_Click(sender As Object, e As EventArgs) Handles btn_mhq_generar_mapa.Click
        ' generamos csvdiagramas
        gdb_to_csv()

        runProgressBar()
        params.Clear()
        params.Add(tbx_mhq_shp_sub.Text)
        params.Add(tbx_mhq_shp_micro.Text)
        params.Add(cod_cuenca)
        params.Add(cod_subcuenca)
        params.Add(cod_microcuenca)
        params.Add(zona_mhq)


        Dim response = ExecuteGP(_tool_generateMapHidroquimico, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar("ini")

        Dim mxd_path As String = responseJson.Item("response")
        Dim arcmapmxd = My.ArcMap.Application
        arcmapmxd.OpenDocument(mxd_path)
        'Cursor.Current = Cursors.Default
        runProgressBar("ini")

        generar_graficos_mhq()

    End Sub

    Private Sub cbx_mhq_zona_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_mhq_zona.SelectedIndexChanged
        zona_mhq = cbx_mhq_zona.SelectedValue
    End Sub

    Private Sub btn_mhq_rotulo_Click(sender As Object, e As EventArgs) Handles btn_mhq_rotulo.Click
        Dim lista_textos As New List(Of String)
        Dim titulo_rotulo As String = tbx_mhq_title.Text
        Dim subcuenca_rotulo As String = tbx_mhq_rotulo_subcuenca.Text
        Dim subtitulo_rotulo As String = tbx_mhq_subtitle.Text
        Dim autores As String = ucl_mhq_autores.getAutorsCheked()

        lista_textos.Add(titulo_rotulo)
        lista_textos.Add(subcuenca_rotulo)
        lista_textos.Add(subtitulo_rotulo)
        lista_textos.Add(autores)

        textos_rotulo = String.Join(";", lista_textos)

        runProgressBar()
        params.Clear()
        params.Add(textos_rotulo)

        Dim response = ExecuteGP(_tool_actualizarRotulo, params, _toolboxPath_mapa_hidrogeoquimico)
        Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
        If responseJson.Item("status") = 0 Then
            RuntimeError.PythonError = responseJson.Item("message")
            MessageBox.Show(RuntimeError.PythonError, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)

            runProgressBar("ini")
            'Cursor.Current = Cursors.Default
            Return
        End If
        runProgressBar("ini")

    End Sub


End Class