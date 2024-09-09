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
Imports ESRI.ArcGIS.esriSystemcls_Oracle.FT_DATA_ACCEDITARIO
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
Imports System.Globalization

Imports Oracle.DataAccess.Client
Imports Microsoft.Office.Interop



Public Class frmPorcentajeUbicacion

    Public m_application As IApplication
    Private cls_oracle As New cls_Oracle
    Private cls_eval As New Cls_evaluacion
    Private flag As Integer = 0

    Private Sub frmPorcentajeUbicacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnExportarExcel.Enabled = False
        btnExportarExcel.Visible = False
        btnExportarTxt.Enabled = False
        btnExportarTxt.Visible = False

        btnExportarExcelControl.Enabled = False
        btnExportarExcelControl.Visible = False

        TextBox1.Enabled = False
        TextBox1.Visible = False

        Label7.Enabled = False
        Label7.Visible = False
        Label8.Enabled = False
        Label8.Visible = False
        Label9.Enabled = False
        Label9.Visible = False
        Label10.Enabled = False
        Label10.Visible = False

        Dim vtipo As String = "1"
        'Dim v_annoPU As String = ""
        dt_annoPU = cls_oracle.FT_ANNO_PORCENTAJE_UBI(vtipo, "")

        Me.cboAnnoConsulta.DataSource = dt_annoPU
        Me.cboAnnoConsulta.DisplayMember = "ap_anovig"
        Me.cboAnnoConsulta.ValueMember = "ap_anovig"
        cboAnnoConsulta.SelectedValue = Format(Now, "yyyy")

        Me.cboAnnoControl.DataSource = dt_annoPU
        Me.cboAnnoControl.DisplayMember = "ap_anovig"
        Me.cboAnnoControl.ValueMember = "ap_anovig"
        cboAnnoControl.SelectedValue = Format(Now, "yyyy")

        flag = 1

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAnnoConsulta.SelectedIndexChanged
        ' If Flag = 0 Then Exit Sub
        If Me.cboAnnoConsulta.SelectedValue.ToString = "" Then
            MsgBox("Debe Seleccionar Año")
        Else
            'Dim vtipo As String = "2"
            v_annoporcubi = Me.cboAnnoConsulta.SelectedValue.ToString
            'dt_annoPU = cls_oracle.FT_ANNO_PORCENTAJE_UBI(vtipo, v_annoporcubi)
            'dgvPorcentajeUbi.DataSource = dt_annoPU
        End If
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)
            btnExportarExcel.Enabled = True
            btnExportarTxt.Enabled = True

            Dim vtipo As String = "2"
            v_annoporcubi = Me.cboAnnoConsulta.SelectedValue.ToString
            dt_annoPU = cls_oracle.FT_ANNO_PORCENTAJE_UBI(vtipo, v_annoporcubi)
            'If Val(dt_annoPU) = "0" Then
            'End If
            If dt_annoPU.Rows.Count > 1 Then
                dgvPorcentajeUbi.DataSource = dt_annoPU
            Else
                Dim p As New Process
                Dim output As String
                p.StartInfo.UseShellExecute = False
                p.StartInfo.RedirectStandardOutput = True
                p.StartInfo.FileName = _bat_porcentaje_ubicacion
                'p.StartInfo.Arguments = "# " & var_dm & " " & v_Zona & " " & Datum_PSAD
                'p.StartInfo.Arguments = String.Format("{0} # {1} {2}", 1, gstrUsuarioAcceso, gstrUsuarioClave)
                'p.StartInfo.Arguments = String.Format("{0} {1} {2} {3}", 1, v_annoporcubi, gstrUsuarioAcceso, gstrUsuarioClave)

                Dim params As String = String.Format("{0} {1} {2} {3}", 1, v_annoporcubi, gstrUsuarioAcceso, gstrUsuarioClave)
                p.StartInfo.Arguments = params

                p.StartInfo.CreateNoWindow = True
                p.Start()

                'RetVal = Shell(glo_Path & "\estilos\Barra_Proceso.exe", 1)

                output = p.StandardOutput.ReadToEnd()
                p.WaitForExit()

                cls_eval.cierra_ejecutable()

                dt_annoPU = cls_oracle.FT_ANNO_PORCENTAJE_UBI(vtipo, v_annoporcubi)
                dgvPorcentajeUbi.DataSource = dt_annoPU

            End If

            btnExportarExcel.Enabled = True
            btnExportarExcel.Visible = True
            btnExportarTxt.Enabled = True
            btnExportarTxt.Visible = True

        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()

        '    'Frm_Eval_segun_codigo.Show()
        '    'cbo_tipo.SelectedIndex = 11
    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)
            Dim p As New Process
            Dim output As String
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.FileName = _bat_porcentaje_ubicacion
            p.StartInfo.Arguments = String.Format("{0} {1} {2} {3}", 3, v_annoporcubi, gstrUsuarioAcceso, gstrUsuarioClave)
            p.StartInfo.CreateNoWindow = True
            p.Start()

            output = p.StandardOutput.ReadToEnd()
            p.WaitForExit()
        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub btnExportarTxt_Click(sender As Object, e As EventArgs) Handles btnExportarTxt.Click
        Try
            Dim RetVal = Shell(path_loader_proceso_general, 1)
            Dim p As New Process
            Dim output As String
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.FileName = _bat_porcentaje_ubicacion
            p.StartInfo.Arguments = String.Format("{0} {1} {2} {3}", 2, v_annoporcubi, gstrUsuarioAcceso, gstrUsuarioClave)
            p.StartInfo.CreateNoWindow = True
            p.Start()
            output = p.StandardOutput.ReadToEnd()
            p.WaitForExit()
        Catch ex As Exception
            cls_eval.KillProcess(loader_proceso_general)
            MessageBox.Show("Error durante el proceso: " & vbCrLf & ex.Message)
        Finally
            cls_eval.KillProcess(loader_proceso_general)
        End Try
    End Sub

    Private Sub lnk_userguide_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnk_userguide.LinkClicked
        get_userguide()
    End Sub

    Private Sub btnProcesarControl_Click(sender As Object, e As EventArgs) Handles btnProcesarControl.Click

        Dim RetVal = Shell(path_loader_proceso_general, 1)
        'Parametros para determinar la relación de DMs - Distribución
        Dim tipo As String = "D"
        Dim fecinicio As String = Microsoft.VisualBasic.Left(Format(dtpFechaInicio.Value), 10)
        Dim fecfinal As String = Microsoft.VisualBasic.Left(Format(dtpFechaFinal.Value), 10)

        'Dim ini As Date = dtpFechaInicio.Value        --   mes/dia/año
        'Dim fin As Date = dtpFechaInicio.Value

        Dim ano_i As String = fecinicio.Substring(5, 4)
        Dim ano_f As String = fecfinal.Substring(6, 4)
        Dim mes_i As String = fecinicio.Substring(2, 2)
        Dim mes_f As String = fecfinal.Substring(3, 2)
        Dim dia_i As String = fecinicio.Substring(0, 1)
        Dim dia_f As String = fecfinal.Substring(0, 2)

        Dim fec_inicio As String = ano_i & mes_i & "0" & dia_i
        Dim fec_final As String = ano_f & mes_f & dia_f
        Dim ano_control As String = ""
        Dim dt_dmdistribucion As New DataTable

        If Me.cboAnnoControl.SelectedValue.ToString = "" Then
            MsgBox("Debe Seleccionar Año")
        Else
            ano_control = Me.cboAnnoControl.SelectedValue.ToString
        End If


        'Dim codigodis As String = "0100393118112"
        'Dim n As Integer = codigodis.Length()

        'If codigodis.Substring((codigodis.Length() - 2), 2) <> ano_f.Substring(2, 2) Then

        '    MsgBox("DM a insertar")

        'End If

        
        'Relación de DMs para distribución (según mes seleccionado) -- paso (1)
        dt_dmdistribucion = cls_oracle.FT_DM_DISTRIBUCION(fec_inicio, fec_final, tipo)

        Dim vtransac As String = ""
        Dim vcodi As String = ""
        Dim vanno As String = ""
        Dim v_delete As String = ""
        Dim v_ins_temp As String = ""
        Dim v_ins_demapubi As String = ""
        Dim v_upd_demapubi As String = ""
        Dim v_estado As String = "1"
        Dim dt_dmcontrol As New DataTable

        If dt_dmdistribucion.Rows.Count > 0 Then
            'Se elimina todos los registros de la tabla temporal:  sc_t_dmdistribucion  -- paso (2)
            v_delete = cls_oracle.FT_DEL_DM_DISTRIBUCION("1")

            For contador As Integer = 0 To dt_dmdistribucion.Rows.Count - 1
                vtransac = dt_dmdistribucion.Rows(contador).Item("TRANSACCION")
                vcodi = dt_dmdistribucion.Rows(contador).Item("CODIGO")

                If vcodi.Substring((vcodi.Length() - 2), 2) <> ano_f.Substring(2, 2) Then

                    'Se inserta en la tabla temporal:  sc_t_dmdistribucion   todos los DMs a ser distribuidos del paso (1)  -- paso (3)
                    v_ins_temp = cls_oracle.FT_INS_DM_DISTRIBUCION(vtransac, vcodi)

                End If

            Next contador

            'Se elimina todos los registros de la tabla:  por_ubi_   -- paso (4)
            v_delete = cls_oracle.FT_DEL_DM_DISTRIBUCION("2")

            'Se inserta en la tabla temporal:  por_ubi_   los DMs con sus demarcaciones  -- paso (5)
            v_ins_demapubi = cls_oracle.FT_INS_DEMAPUBI_TEMP("0")

            'Se actualiza el campo ESTADO de la tabla temporal:  por_ubi_   según año de comparación  -- paso (6)
            v_upd_demapubi = cls_oracle.FT_UPD_DEMAPUBI_TEMP(ano_control)

            'Lista los DMs que han cambiado de demarcación  -- paso (7)
            dt_dmcontrol = cls_oracle.FT_SEL_DATOS_DEMAPUBI_TEMP(v_estado)

            'Muestra en el DataGridView el paso (7)
            Me.dgvControl.DataSource = dt_dmcontrol

            btnExportarExcelControl.Enabled = True
            btnExportarExcelControl.Visible = True

        Else
            MsgBox("No Existen Derechos Mineros a ser Distribuidos", MsgBoxStyle.Information, "Observación...")
            cls_eval.KillProcess(loader_proceso_general)
            Exit Sub
        End If

        TextBox1.Enabled = True
        TextBox1.Visible = True
        Label7.Enabled = True
        Label7.Visible = True
        Label8.Enabled = True
        Label8.Visible = True
        Label9.Enabled = True
        Label9.Visible = True
        Label10.Enabled = True
        Label10.Visible = True

        cls_eval.KillProcess(loader_proceso_general)

    End Sub

    Private Sub CerrarControl_Click(sender As Object, e As EventArgs) Handles CerrarControl.Click
        Me.Close()
    End Sub

    Private Sub btnExportarExcelControl_Click(sender As Object, e As EventArgs) Handles btnExportarExcelControl.Click

        ExportarDatosExcel_pubi(dgvControl, "Reporte de derechos mineros cuyas demarcaciones difieren en relación a la base del Padrón")

    End Sub

    Public Sub ExportarDatosExcel_pubi(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
        Dim m_Excel As New Excel.Application
        m_Excel.Cursor = Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)
        With objHojaExcel
            .Visible = Excel.XlSheetVisibility.xlSheetVisible
            .Activate()
            'Encabezado.
            .Range("A1:L1").Merge()
            .Range("A1:L1").Value = "INGEMMET"
            .Range("A1:L1").Font.Bold = True
            .Range("A1:L1").Font.Size = 16
            'Texto despues del encabezado.
            .Range("A2:L2").Merge()
            .Range("A2:L2").Value = titulo
            .Range("A2:L2").Font.Bold = True
            .Range("A2:L2").Font.Size = 10
            'Nombres
            'Estilo a titulos de la tabla.
            .Range("A6:P6").Font.Bold = True
            'Establecer tipo de letra al rango determinado.
            .Range("A1:P37").Font.Name = "Tahoma"
            'Los datos se registran a partir de la columna A, fila 4.
            Const primeraLetra As Char = "A"
            Const primerNumero As Short = 6
            Dim Letra As Char, UltimaLetra As Char
            Dim Numero As Integer, UltimoNumero As Integer
            Dim cod_letra As Byte = Asc(primeraLetra) - 1
            Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
            Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
            Dim strColumna As String = ""
            Dim LetraIzq As String = ""
            Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
            Letra = primeraLetra
            Numero = primerNumero
            Dim objCelda As Excel.Range
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    : If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        cod_LetraIzq += 1
                        LetraIzq = Chr(cod_LetraIzq)
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                    strColumna = LetraIzq + Letra + Numero.ToString
                    objCelda = .Range(strColumna, Type.Missing)
                    objCelda.Value = c.HeaderText
                    objCelda.EntireColumn.Font.Size = 10
                    'Establece un formato a los numeros por Default.
                    'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                    If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                    End If
                End If
            Next
            Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq
            'Cargar Datos del DataGridView.
            Dim i As Integer = Numero + 1
            For Each reg As DataGridViewRow In DataGridView1.Rows
                LetraIzq = ""
                cod_LetraIzq = Asc(primeraLetra) - 1
                Letra = primeraLetra
                cod_letra = Asc(primeraLetra) - 1
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra
                        'Aqui se realiza la carga de datos.
                        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", "'" & reg.Cells(c.Index).Value)
                        'Establece las propiedades de los datos del DataGridView por Default.
                        '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                        '.Range(strColumna + i, strColumna + i).In()
                    End If
                Next
                Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                objRangoReg.Rows.BorderAround()
                objRangoReg.Select()
                i += 1
            Next
            UltimoNumero = i
            'Dibujar las líneas de las columnas.
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                    objCelda.BorderAround()
                    If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        LetraIzq = Chr(cod_LetraIzq)
                        cod_LetraIzq += 1
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                End If
            Next
            'Dibujar el border exterior grueso de la tabla.
            Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlMedium)
        End With
        m_Excel.Cursor = Excel.XlMousePointer.xlDefault
    End Sub

End Class