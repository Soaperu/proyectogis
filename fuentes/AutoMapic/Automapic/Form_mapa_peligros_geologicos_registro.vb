

Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class Form_mapa_peligros_geologicos_registro
    Public tableHist As New DataTable
    Dim solicitantesDict As New Dictionary(Of Integer, String)
    Dim tipoInformacionDict As New Dictionary(Of Integer, String)
    'Public departamentosDict As New Dictionary(Of String, String)
    Public distritosArray As New List(Of String)
    Dim estadoDict As New Dictionary(Of Integer, String)
    Dim params As New List(Of Object)

    Private Sub Form_mapa_peligros_geologicos_registro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Configurar combobox
        SQLConn.Open()
        SQLcmd.CommandText = "SELECT KEY, VALUE FROM TB_OPCION WHERE ""GROUP"" = 1"
        SQLdr = SQLcmd.ExecuteReader()
        While SQLdr.Read()
            solicitantesDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
        End While
        SQLdr.Close()

        cbx_pgr_solicitante.DataSource = New BindingSource(solicitantesDict, Nothing)
        cbx_pgr_solicitante.DisplayMember = "Value"
        cbx_pgr_solicitante.ValueMember = "Key"

        SQLcmd.CommandText = "SELECT KEY, VALUE FROM TB_OPCION WHERE ""GROUP"" = 2"
        SQLdr = SQLcmd.ExecuteReader()
        While SQLdr.Read()
            tipoInformacionDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
        End While
        SQLdr.Close()

        cbx_pgr_tipoinformacion.DataSource = New BindingSource(tipoInformacionDict, Nothing)
        cbx_pgr_tipoinformacion.DisplayMember = "Value"
        cbx_pgr_tipoinformacion.ValueMember = "Key"

        SQLcmd.CommandText = "SELECT KEY, VALUE FROM TB_OPCION WHERE ""GROUP"" = 3"
        SQLdr = SQLcmd.ExecuteReader()
        While SQLdr.Read()
            estadoDict.Add(SQLdr.GetValue(0), SQLdr.GetValue(1))
        End While
        SQLdr.Close()




        cbx_pgr_estadoatencion.DataSource = New BindingSource(estadoDict, Nothing)
        cbx_pgr_estadoatencion.DisplayMember = "Value"
        cbx_pgr_estadoatencion.ValueMember = "Key"


    End Sub

    Public Sub configureFormRegistro(departamentosDict, tableHist)
        cbx_pgr_departamentos.DataSource = New BindingSource(departamentosDict, Nothing)
        cbx_pgr_departamentos.DisplayMember = "Value"
        cbx_pgr_departamentos.ValueMember = "Key"

        'Configurar datagridview
        dgv_pgr_tablahistorica.DataSource = tableHist
        dgv_pgr_tablahistorica.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        TableLayoutPanel1.VerticalScroll.Value = 0
    End Sub

    Private Sub btn_pgr_registrar_Click(sender As Object, e As EventArgs) Handles btn_pgr_registrar.Click
        Try
            runProgressBar()
            params.Clear()
            'Codigo de solicitud
            If tbx_pgr_codigosolicitud.Text Is Nothing Then
                tbx_pgr_codigosolicitud.Text = ""
            End If
            params.Add(tbx_pgr_codigosolicitud.Text)
            'Sector
            If tbx_pgr_sector.Text Is Nothing Or tbx_pgr_sector.Text = "" Then
                Throw New Exception("Debe ingresar el Sector")
            End If
            params.Add(tbx_pgr_sector.Text)
            'Documento
            If tbx_pgr_documento.Text Is Nothing Or tbx_pgr_documento.Text = "" Then
                Throw New Exception("Debe ingresar el documento")
            End If
            params.Add(tbx_pgr_documento.Text)
            'Fecha solicitud
            If dtp_pgr_fechasolicitud.Text Is Nothing Or dtp_pgr_fechasolicitud.Text = "" Then
                Throw New Exception("Debe ingresar la fecha de solicitud")
            End If
            params.Add(dtp_pgr_fechasolicitud.Text)
            'Solicitante
            Dim solicitante_select = (CType(cbx_pgr_solicitante.SelectedItem, KeyValuePair(Of Integer, String))).Value
            If solicitante_select Is Nothing Then
                Throw New Exception("Debe seleccionar al solicitante")
            End If
            params.Add(solicitante_select)
            'Emisor de documento
            If tbx_pgr_emisor.Text Is Nothing Or tbx_pgr_emisor.Text = "" Then
                Throw New Exception("Debe ingresar el emisor del documento")
            End If
            params.Add(tbx_pgr_emisor.Text)
            'Asunto
            If tbx_pgr_asunto.Text Is Nothing Or tbx_pgr_asunto.Text = "" Then
                Throw New Exception("Debe ingresar el Sector")
            End If
            params.Add(tbx_pgr_asunto.Text)
            'Tipo de informacíón
            Dim tipoinfo_select = (CType(cbx_pgr_tipoinformacion.SelectedItem, KeyValuePair(Of Integer, String))).Value
            If tipoinfo_select Is Nothing Then
                Throw New Exception("Debe seleccionar el tipo de información")
            End If
            params.Add(tipoinfo_select)
            'Responsable
            If tbx_pgr_responsable.Text Is Nothing Or tbx_pgr_responsable.Text = "" Then
                Throw New Exception("Debe ingresar el nombre de los profesionales responsables")
            End If
            params.Add(tbx_pgr_responsable.Text)
            'Fecha asignacion
            If dtp_pgr_fechaasignacion.Text Is Nothing Or dtp_pgr_fechaasignacion.Text = "" Then
                Throw New Exception("Debe ingresar la fecha de asignación")
            End If
            params.Add(dtp_pgr_fechaasignacion.Text)
            'Estado
            Dim estado_select = (CType(cbx_pgr_estadoatencion.SelectedItem, KeyValuePair(Of Integer, String))).Value
            If estado_select Is Nothing Then
                Throw New Exception("Debe seleccionar el estado de la atención")
            End If
            params.Add(estado_select)
            'Fecha de atención
            If dtp_pgr_fechaatencion.Text Is Nothing Or dtp_pgr_fechaatencion.Text = "" Then
                Throw New Exception("Debe ingresar la fecha de atención")
            End If
            params.Add(dtp_pgr_fechaatencion.Text)
            'Asunto
            If tbx_pgr_observacion.Text Is Nothing Or tbx_pgr_observacion.Text = "" Then
                Throw New Exception("Debe ingresar el Sector")
            End If
            params.Add(tbx_pgr_observacion.Text)
            'departamento
            Dim cd_depa = (CType(cbx_pgr_departamentos.SelectedItem, KeyValuePair(Of String, String))).Key
            params.Add(cd_depa)
            If cd_depa <> "99" Then
                'coordenadas
                Dim i As Integer = dgv_pgr_tablahistorica.CurrentRow.Index
                'coordenada x
                params.Add(dgv_pgr_tablahistorica.Item(1, i).Value)
                'coordenda y
                params.Add(dgv_pgr_tablahistorica.Item(2, i).Value)
                'zona
                params.Add(dgv_pgr_tablahistorica.Item(4, i).Value)
            Else
                params.Add(0)
                params.Add(0)
                params.Add(18)
            End If

            Dim distritosArrayString = String.Join(",", distritosArray)
            params.Add(distritosArrayString)


            Dim response As String = ExecuteGP(_tool_registerGeologicalHazards, params, _toolboxPath_peligros_geologicos)

            Dim responseJson = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)
            If responseJson.Item("status") = 0 Then
                Throw New Exception(responseJson.Item("message"))
            End If
            MessageBox.Show("Se registro la atencion satisfactoriamente", __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
            runProgressBar("ini")
        Catch ex As Exception
            MessageBox.Show(ex.Message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
            runProgressBar("ini")
        End Try

    End Sub

    Private Sub cbx_pgr_departamentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_pgr_departamentos.SelectedIndexChanged
        Dim currentDepartamento = (CType(cbx_pgr_departamentos.SelectedItem, KeyValuePair(Of String, String))).Key
        If (currentDepartamento = "99") Then
            dgv_pgr_tablahistorica.Enabled = False
            Return
        End If
        dgv_pgr_tablahistorica.Enabled = True
    End Sub
End Class