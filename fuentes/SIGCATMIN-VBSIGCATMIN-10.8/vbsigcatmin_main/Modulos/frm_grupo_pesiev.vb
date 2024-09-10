Imports System.Drawing
Imports PORTAL_Clases
Imports ESRI.ArcGIS.Framework
'Imports ESRI.ArcGIS.GISClient
Imports ESRI.ArcGIS.Display
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Output
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.CartoUI
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports stdole
Imports Newtonsoft.Json
Imports System.Windows.Forms



Public Class frm_grupo_pesiev
    Public m_application As IApplication
    'Public papp As IApplication
    ' Simultaneo ced
    Private Const Col_flg_sel = 0
    Private Const Col_grupo = 1
    Private Const Col_num_dm = 2
    Private Const Col_zonac = 3
    Private clsOracle As New cls_Oracle
    Private cls_Catastro As New cls_DM_1
    Private cls_eval As New Cls_evaluacion
    Dim _params As New List(Of Object)


    Public Sub PT_Inicializar_Grilla_Simultaneoc()
        Dim v_gpesiev As New DataTable
        v_gpesiev.Columns.Add("FLG_SEL", GetType(String))
        v_gpesiev.Columns.Add("GRUPO", GetType(String))
        v_gpesiev.Columns.Add("NUM_DM", GetType(String))
        v_gpesiev.Columns.Add("ZONAC", GetType(String))
        PT_Estilo_Grilla_Simultaneoc(v_gpesiev) : PT_Cargar_Grilla_Simultaneoc(v_gpesiev)
        PT_Agregar_Funciones_Simultaneoc() : PT_Forma_Grilla_Simultaneoc()
    End Sub

    Private Sub PT_Estilo_Grilla_Simultaneoc(ByRef padtbDetalle As DataTable)
        padtbDetalle.Columns.Item(Col_flg_sel).DefaultValue = 0
        padtbDetalle.Columns.Item(Col_grupo).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_num_dm).DefaultValue = ""
        padtbDetalle.Columns.Item(Col_zonac).DefaultValue = ""
    End Sub

    Private Sub PT_Cargar_Grilla_Simultaneoc(ByVal padtbDetalle As DataTable)
        Dim v_gpesiev As New DataView(padtbDetalle)
        Me.dgdDetalle.DataSource = v_gpesiev
        Me.dgdDetalle.Columns(Col_flg_sel).Value = False
        'Pinta_Grilla_Ubigeo()
    End Sub

    Private Sub PT_Agregar_Funciones_Simultaneoc()
        Me.dgdDetalle.Columns(Col_flg_sel).DefaultValue = 0
        Me.dgdDetalle.Columns(Col_grupo).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_num_dm).DefaultValue = ""
        Me.dgdDetalle.Columns(Col_zonac).DefaultValue = ""
    End Sub

    Private Sub PT_Forma_Grilla_Simultaneoc()
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Width = 20
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Width = 80
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Width = 50

        Me.dgdDetalle.Columns("FLG_SEL").Caption = "Sel."
        Me.dgdDetalle.Columns("GRUPO").Caption = "Grupo"
        Me.dgdDetalle.Columns("NUM_DM").Caption = "Número_DM"
        Me.dgdDetalle.Columns("ZONAC").Caption = "Zona"

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Locked = True
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Locked = True

        Me.dgdDetalle.Splits(0).HeadingStyle.ForeColor = Color.Black
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

        Me.dgdDetalle.Splits(0).DisplayColumns(Col_flg_sel).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_grupo).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_num_dm).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.General
        Me.dgdDetalle.Splits(0).DisplayColumns(Col_zonac).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
    End Sub

    Private Sub frm_grupo_pesiev_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''' NEW CODIGO - JSON
        Dim p As New Process
        Dim output As String
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.FileName = _bat_simultaneidad   '"U:\DATOS\SHAPE\process\execute\simultaneidad.bat"'
        'p.StartInfo.FileName = "C:\pysigcatmin\Install\execute\simultaneidad.bat"
        'p.StartInfo.Arguments = "# " & var_dm & " " & v_Zona & " " & Datum_PSAD
        'p.StartInfo.Arguments = String.Format("# {0} {1} {2}", loCodigosim, v_zona_dm, v_sistema)
        p.StartInfo.Arguments = String.Format("# {0} {1} {2} {3} {4}", gstrUsuarioAcceso, gstrUsuarioClave, loCodigosim, v_zona_dm, v_sistema)
        p.StartInfo.CreateNoWindow = True
        p.Start()

        Dim RetVal
        'btnGraficar.Cursor = System.Windows.Forms.Cursors.AppStarting
        RetVal = Shell(glo_Path & "\estilos\Barra_Proceso.exe", 1)
        'Me.Hide()

        output = p.StandardOutput.ReadToEnd()
        p.WaitForExit()

        cls_eval.cierra_ejecutable()

        Dim json = JsonConvert.DeserializeObject(output)

        If json(0)("state").ToString() = "1" Then
            v_identi = json(0)("id").ToString()
        Else
            MessageBox.Show(json(0)("msg").ToString())
        End If

        'Dim dtsim_ev As New DataTable
        'Dim json_ev As String = "{'Table2':" & output & "}"
        'Dim dsetsim As DataSet = JsonConvert.DeserializeObject(Of DataSet)(json_ev)
        'dtsim_ev = dsetsim.Tables("Table2")

        Dim v_gpesiev As New DataTable
        Dim v_tipo As String = "2"
        'Dim del_tabla As String
        'Dim s_grusim As String = ""
        'Dim s_grupof As String = ""
        'Dim s_cuadric As String = ""
        'Dim s_zona As String = ""
        'Dim s_codi As String = ""
        'Dim s_subgrupo As String = ""
        'Dim RetornoTabla As String

        'del_tabla = clsOracle.P_DEL_TABLA_SIMUL(v_tipo)     'ELIMIMA TODOS LOS REGISTROS DE LA TABLA DONDE SE ALMACENARA  dtsim_ev

        'For i As Integer = 0 To dtsim_ev.Rows.Count - 1
        '    s_grusim = dtsim_ev.Rows(i).Item("GRUPO").ToString 'p_ListBox.Items(i).SubItems(0).Text
        '    s_cuadric = dtsim_ev.Rows(i).Item("CUADRICULA").ToString 'p_ListBox.Items(i).SubItems(2).Text
        '    s_grupof = dtsim_ev.Rows(i).Item("SUBGRUPO_NUM").ToString 'p_ListBox.Items(i).SubItems(2).Text
        '    s_zona = dtsim_ev.Rows(i).Item("ZONA").ToString ' p_ListBox.Items(i).SubItems(3).Text
        '    s_codi = dtsim_ev.Rows(i).Item("CODIGOU").ToString 'p_ListBox.Items(i).SubItems(0).Text
        '    s_subgrupo = dtsim_ev.Rows(i).Item("SUBGRUPO").ToString 'p_ListBox.Items(i).SubItems(0).Text

        '    'RetornoTabla1 = cls_Oracle.P_INS_SIMULTANEOS(fecsimul, grusim, sgrupo_sim, vnum_dm, num_cuasim)
        '    RetornoTabla = clsOracle.P_INS_SEL_GRUPOSIM_LD(fecsimul, v_tipo, s_grusim, s_cuadric, s_zona, s_codi, s_grupof, s_subgrupo)
        'Next

        v_gpesiev = clsOracle.FT_REPO_PETSIM_LD(v_tipo, v_identi)

        dgdDetalle.DataSource = v_gpesiev
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'PT_Inicializar_Grilla_Simultaneoc()     ' ojo   esto debe estar comentado
        PT_Agregar_Funciones_Simultaneoc()
        Me.chkEstado.Checked = False : Me.chkEstado.Checked = True
        Me.dgdDetalle.AllowUpdate = True
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'chkEstado_CheckedChanged(Nothing,Nothing)
        ''End If




        ' Se agrega código para Código de Remate:

        'Me.btnModificar.Enabled = True
        'Me.txtArea.Enabled = False
        ''Me.txtArea.Visible = False
        'Me.txtCodRemate.Enabled = False
        ''Me.txtCodRemate.Visible = False
        'Me.lstDM.Enabled = True
        ''Me.lstDM.Visible = False
        'Me.btnGrabar.Visible = False
        'Me.btnGrabar.Enabled = False
        'Me.lblRelacionCodRemate.Visible = False


        Me.dgvCodRemate.Visible = False
        Me.GroupBox1.Visible = False
        Me.lblDMSimul.Visible = False
        Me.lblArea.Visible = False
        Me.lstDM.Visible = False
        Me.txtArea.Visible = False
        Me.txtCodRemate.Visible = False
        Me.Label1.Visible = False
        Me.Label2.Visible = False
        Me.Label3.Visible = False
        Me.btnModificar.Visible = False
        Me.btnGrabar.Visible = False


    End Sub

    Private Sub chkEstado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        Dim items As C1.Win.C1TrueDBGrid.ValueItems = Me.dgdDetalle.Columns("FLG_SEL").ValueItems
        If Me.chkEstado.Checked Then
            ' we're going to translate values - the datasource needs to hold at least 3 states
            items.Translate = True
            ' each click will cycle thru the various checkbox states
            items.CycleOnClick = True
            ' display the cell as a checkbox
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox
            ' now associate underlying db values with the checked state
            items.Values.Clear()
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("0", False)) ' unchecked
            items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("1", True)) ' checked
            '  items.Values.Add(New C1.Win.C1TrueDBGrid.ValueItem("2", "INDETERMINATE")) ' indeterminate state
        Else
            items.Translate = False
            items.CycleOnClick = False
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.Normal
        End If
    End Sub

    Private Sub btn_Graficar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Graficar.Click
        Dim cls_planos As New Cls_planos
        Dim loBoo_flg As Boolean = False
        op_sim = 1
        For w As Integer = 0 To Me.dgdDetalle.RowCount - 1
            If Me.dgdDetalle.Item(w, "FLG_SEL") Then
                loBoo_flg = True
            End If
        Next
        If loBoo_flg = False Then
            MsgBox("Seleccione un Item de la Lista", MsgBoxStyle.Information, "[BDGeocatmin]")
            Exit Sub
        End If
        Me.Hide() : Dim RetVal
        'btnGraficar.Cursor = System.Windows.Forms.Cursors.AppStarting

        cls_Catastro.Consulta_DM_Simultaneocev(m_application, Me.dgdDetalle, txtExiste)
        'If Val(num_cuasim) = "1" Then
        '    cls_Catastro.ShowLabel_DM3("Cuadricula_sim", m_application)
        'Else
        '    cls_Catastro.ShowLabel_DM3("Cuadricula_dsim", m_application)
        'End If
        cls_Catastro.rotulatexto_dm("Catastro_sim", m_application)
        If Val(num_cuasim) = "1" Then
            cls_Catastro.rotulatexto_dm("Cuadricula_sim", m_application)
            cls_Catastro.Color_Poligono_Simple_2(m_application, "Cuadricula_sim")    'SE MOVIO DEL PASO SIGUIENTE
        Else
            cls_Catastro.rotulatexto_dm("Cuadricula_dsim", m_application)
            cls_Catastro.Color_Poligono_Simple_2(m_application, "Cuadricula_dsim")    'SE MOVIO DEL PASO SIGUIENTE
        End If
        cls_Catastro.Zoom_to_Layer("Catastro_sim")
        cls_Catastro.Color_Poligono_Simple_3(m_application, "Catastro_sim")
        pMxDoc.UpdateContents()

        cls_planos.generaplanosimultaneoev(m_application)

        Me.Show()
        'Dim lostrRetorno As String = cls_Oracle.FT_Registro(1, lostrOpcioncbo)
        cls_eval.cierra_ejecutable()
        op_sim = 0
        esc_sim = ""
        'Abre los botones de las herramientas
        'BOTON_MENU(True)
        'btnGraficar.Cursor = System.Windows.Forms.Cursors.Default


        'Se agrega para Códigos de Remate
        Dim dt_codremate As New DataTable

        dt_codremate = clsOracle.P_SEL_DMXHAGRSIMUL()

        dgvCodRemate.DataSource = dt_codremate

        Me.dgvCodRemate.Visible = True
        Me.GroupBox1.Visible = True
        Me.lblDMSimul.Visible = True
        Me.lblArea.Visible = True
        Me.lstDM.Visible = True
        Me.txtArea.Visible = True
        Me.txtCodRemate.Visible = True
        Me.Label1.Visible = True
        Me.Label2.Visible = True
        Me.Label3.Visible = True
        Me.btnModificar.Visible = True
        Me.btnGrabar.Visible = True

        Exit Sub
    End Sub

    Private Sub btn_cerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cerrar.Click
        'statusxcambio = "0"
        Me.Close()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

        'Me.GroupBox1.Visible = True
        'Me.txtArea.Visible = True
        Me.txtArea.Enabled = True
        'Me.txtCodRemate.Visible = True
        Me.txtCodRemate.Enabled = True
        'Me.lstDM.Visible = False
        Me.lstDM.Enabled = True
        Me.btnGrabar.Visible = True
        Me.btnGrabar.Enabled = True
        Me.btnModificar.Enabled = True

        txtArea.Text = ""
        txtCodRemate.Text = ""
        lstDM.Items.Clear()

        For i As Integer = 0 To listaDmSim.Count - 1
            lstDM.Items.Add(listaDmSim(i).ToString)
        Next

        txtCodRemate.Text = clsOracle.P_GET_CODREMATE()

        ''''' Se agrega para una solucion temporal
        Dim msg As String = "¿Desea modificar el área simultanea?"
        Dim style As MsgBoxStyle = MsgBoxStyle.YesNo
        Dim response As MsgBoxResult = MsgBox(msg, style)

        If response = MsgBoxResult.Yes Then   ' User choose Yes.
            Dim RetVal2
            While RetVal2 Is Nothing
                'glo_Path_EXE = "U:\Sigcatmin\Procedure_script\batchfiles\"
                'RetVal2 = Shell(glo_Path_EXE & "cuadricula_simultanea.bat", 1)

                _params.Add("0")
                RetVal2 = ExecuteGP(_tool_gen_partircuadsim, _params, _toolboxPathGen, False)
            End While
        End If
        ''''''''''''''''''''''''''''
        'Dim RetVal2
        'While RetVal2 Is Nothing
        '    'glo_Path_EXE = "U:\Sigcatmin\Procedure_script\batchfiles\"
        '    'RetVal2 = Shell(glo_Path_EXE & "cuadricula_simultanea.bat", 1)

        '    _params.Add("0")
        '    RetVal2 = ExecuteGP(_tool_gen_partircuadsim, _params, _toolboxPathGen, False)
        'End While

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Dim num_codremate As String = clsOracle.P_NUM_CODREMATE(txtCodRemate.Text)

        If num_codremate = "0" Then

            For k As Integer = 0 To lstDM.Items.Count - 1
                Dim codigo As String = lstDM.Items(k).ToString

                clsOracle.P_INS_SG_D_DMXHAGRSIMUL(vzona, gstrUsuarioAcceso, codigo, txtArea.Text, txtCodRemate.Text)
            Next

        Else
            MessageBox.Show("El Código de Remate Existe, Ingrese otro Código de Remate")
        End If

        txtArea.Text = ""
        'txtCodRemate.Text = ""
        'lstDM.Items.Clear()

        'Se agrega para Códigos de Remate
        Dim dt_codremate As New DataTable

        dt_codremate = clsOracle.P_SEL_DMXHAGRSIMUL()

        dgvCodRemate.DataSource = dt_codremate

    End Sub
End Class