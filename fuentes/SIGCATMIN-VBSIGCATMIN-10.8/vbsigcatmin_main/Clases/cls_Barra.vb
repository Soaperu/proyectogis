Imports PORTAL_Clases
Imports System.Windows.Forms    'Importa libreria para funcionalidad de formularios
Imports SIGCATMIN.Frm_Eval_segun_codigo
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem


Public Class cls_Barra

    Private cls_Oracle As New cls_Oracle
    Public m_Application As IApplication

    Public Sub BOTON_MENU(ByVal p_Estado As Boolean)
        Dim lodtbBotones As New DataTable
        Dim v_loOpcion As String = ""
        Select Case GloInt_Opcion
            Case 0
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("XXX")
                Hide_Barra(m_Application, lodtbBotones, "Evaluación")
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("XXX")
                Hide_Barra(m_Application, lodtbBotones, "Opciones")

                tipo_seleccion = " "

                If v_opcion_modulo = "OP_28" Then
                    v_loOpcion = "OP_1"
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_loOpcion)
                Else
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                End If

                'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)

                If ((conta_botones_consulta = 1) And (tipo_seleccion = " ")) Then
                    'Create_Barra_1(m_Application, lodtbBotones)

                    'Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    'End If

                    'Formato Automático - BOTONES INFORMES TECNICOS DE EVALUACION
                    ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                    'Botones Principales

                    If v_opcion_modulo = "OP_28" Then
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_loOpcion)
                    Else
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                    End If

                    'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)

                    Create_Barra_1(m_Application, lodtbBotones)

                    'If tipo_seleccion = "OP_16" Then
                    'If (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then
                    '    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")


                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                    '    Create_Barra_2(m_Application, lodtbBotones, "Opciones")

                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")
                    '    Create_Barra_2(m_Application, lodtbBotones, "Libredenu")


                    'End If
                Else

                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                    'If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_16"))) Then
                    If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_18") Or (tipo_seleccion = "OP_20") Or (tipo_seleccion = "OP_29") Or (tipo_seleccion = "OP_30") Or (tipo_seleccion = "OP_32") Or (tipo_seleccion = "OP_34"))) Then
                        Create_Barra_2(m_Application, lodtbBotones, "Evaluación")

                        'Formato Automático
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                        Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                        'Botones Principales
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                        Create_Barra_1(m_Application, lodtbBotones)

                    ElseIf (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then
                        'Create_Barra_2(m_Application, lodtbBotones, "Opciones")

                        ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                        ' Create_Barra_2(m_Application, lodtbBotones, "Formato Tecnico")

                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")  'BOTONES PARA LIBRE DENU
                        Create_Barra_2(m_Application, lodtbBotones, "Libredenu")

                    End If

                    ''Botones Principales
                    ''  tipo_seleccion = ""  'por  mientras boton libredenu
                    'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)
                    'conta_botones_consulta = 1
                    'If ((conta_botones_consulta = 1) And (tipo_seleccion = "")) Then
                    '    'Create_Barra_1(m_Application, lodtbBotones)

                    '    'Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    '    'End If

                    '    'Formato Automático
                    '    ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    '    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                    '    'Botones Principales
                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)
                    '    Create_Barra_1(m_Application, lodtbBotones)
                    'End If


                End If

            Case 1
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                'If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_16"))) Then
                If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_18") Or (tipo_seleccion = "OP_20") Or (tipo_seleccion = "OP_29") Or (tipo_seleccion = "OP_30") Or (tipo_seleccion = "OP_32") Or (tipo_seleccion = "OP_34"))) Then
                    Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    'End If

                    'Formato Automático
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    Create_Barra_2(m_Application, lodtbBotones, "Informes Técnico")

                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA3")
                    Create_Barra_2(m_Application, lodtbBotones, "Acumulación_División_Renuncia")

                    'Botones Principales
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                    Create_Barra_1(m_Application, lodtbBotones)

                    'Modulo UEAs   
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA4")
                    Create_Barra_2(m_Application, lodtbBotones, "Modulo UEAs")

                ElseIf (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then 'BOTONES PARA LIBRE DENU

                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")
                    Create_Barra_2(m_Application, lodtbBotones, "Libredenu")
                End If
        End Select
        'v_loOpcion = Me.cbo_tipo.SelectedValue.ToString

    End Sub

    Public Sub BOTON_MENU(ByVal p_Estado As Boolean, m_Application As IApplication)
        Dim lodtbBotones As New DataTable
        Dim v_loOpcion As String = ""
        Select Case GloInt_Opcion
            Case 0
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("XXX")
                Hide_Barra(m_Application, lodtbBotones, "Evaluación")
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("XXX")
                Hide_Barra(m_Application, lodtbBotones, "Opciones")

                tipo_seleccion = " "

                If v_opcion_modulo = "OP_28" Then
                    v_loOpcion = "OP_1"
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_loOpcion)
                Else
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                End If

                'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)

                If ((conta_botones_consulta = 1) And (tipo_seleccion = " ")) Then
                    'Create_Barra_1(m_Application, lodtbBotones)

                    'Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    'End If

                    'Formato Automático - BOTONES INFORMES TECNICOS DE EVALUACION
                    ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                    'Botones Principales

                    If v_opcion_modulo = "OP_28" Then
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_loOpcion)
                    Else
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                    End If

                    'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)

                    Create_Barra_1(m_Application, lodtbBotones)

                    'If tipo_seleccion = "OP_16" Then
                    'If (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then
                    '    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")


                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                    '    Create_Barra_2(m_Application, lodtbBotones, "Opciones")

                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")
                    '    Create_Barra_2(m_Application, lodtbBotones, "Libredenu")


                    'End If
                Else

                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                    'If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_16"))) Then
                    If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_18") Or (tipo_seleccion = "OP_20") Or (tipo_seleccion = "OP_29") Or (tipo_seleccion = "OP_30") Or (tipo_seleccion = "OP_32") Or (tipo_seleccion = "OP_34"))) Then
                        Create_Barra_2(m_Application, lodtbBotones, "Evaluación")

                        'Formato Automático
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                        Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                        'Botones Principales
                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(v_opcion_modulo)
                        Create_Barra_1(m_Application, lodtbBotones)

                    ElseIf (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then
                        'Create_Barra_2(m_Application, lodtbBotones, "Opciones")

                        ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                        ' Create_Barra_2(m_Application, lodtbBotones, "Formato Tecnico")

                        lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")  'BOTONES PARA LIBRE DENU
                        Create_Barra_2(m_Application, lodtbBotones, "Libredenu")

                    End If

                    ''Botones Principales
                    ''  tipo_seleccion = ""  'por  mientras boton libredenu
                    'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)
                    'conta_botones_consulta = 1
                    'If ((conta_botones_consulta = 1) And (tipo_seleccion = "")) Then
                    '    'Create_Barra_1(m_Application, lodtbBotones)

                    '    'Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    '    'End If

                    '    'Formato Automático
                    '    ' lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    '    'Create_Barra_2(m_Application, lodtbBotones, "Opciones")
                    '    'Botones Principales
                    '    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)
                    '    Create_Barra_1(m_Application, lodtbBotones)
                    'End If


                End If

            Case 1
                lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA1")
                'If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_16"))) Then
                If ((conta_botones_evaluacion = 1) And ((tipo_seleccion = "OP_11") Or (tipo_seleccion = "OP_12") Or (tipo_seleccion = "OP_5") Or (tipo_seleccion = "OP_6") Or (tipo_seleccion = "OP_18") Or (tipo_seleccion = "OP_20") Or (tipo_seleccion = "OP_29") Or (tipo_seleccion = "OP_30") Or (tipo_seleccion = "OP_32") Or (tipo_seleccion = "OP_34"))) Then
                    Create_Barra_2(m_Application, lodtbBotones, "Evaluación")
                    'End If

                    'Formato Automático
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA2")
                    Create_Barra_2(m_Application, lodtbBotones, "Informes Técnico")

                    'Botones Principales
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(tipo_seleccion)
                    Create_Barra_1(m_Application, lodtbBotones)

                    'Acumulacion_Division_Renuncia
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA3")
                    Create_Barra_2(m_Application, lodtbBotones, "Acumulación_División_Renuncia")

                    'Modulo UEAs   
                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("EVA4")
                    Create_Barra_2(m_Application, lodtbBotones, "ModuloUEAs")

                    ''Botones Principales
                    'lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion(Me.cbo_tipo.SelectedValue)
                    'Create_Barra_1(m_Application, lodtbBotones)
                ElseIf (conta_botones_libredenu = 1) And (tipo_seleccion = "OP_16") Then 'BOTONES PARA LIBRE DENU

                    lodtbBotones = cls_Oracle.F_Obtiene_Menu_x_Opcion_1("LIBDEN")
                    Create_Barra_2(m_Application, lodtbBotones, "Libredenu")
                End If
        End Select
        'v_loOpcion = Me.cbo_tipo.SelectedValue.ToString

    End Sub

    Public Sub Hide_Barra(ByVal m_application As IApplication, ByVal p_Grilla As DataTable, ByVal p_NomVentana As String)
        Dim pMDocumento As IDocument
        Dim pTool_Bars As ICommandBars
        Dim pTool_Boton As ICommandBar = Nothing
        pMDocumento = m_application.Document
        pTool_Bars = pMDocumento.CommandBars
        Try
            pTool_Boton = pTool_Bars.Find("Project." & p_NomVentana, False, True)
            Select Case p_NomVentana
                Case "Consulta"
                    pTool_Boton.Dock(esriDockFlags.esriDockHide)
                Case "Evaluación"
                    pTool_Boton.Dock(esriDockFlags.esriDockHide)
                Case "Opciones"
                    'pTool_Boton.Dock(esriDockFlags.esriDockHide)
            End Select
            Exit Sub
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Create_Barra_1(ByVal m_application As IApplication, ByVal p_Grilla As DataTable)
        Dim pMDocumento As IDocument
        Dim pTool_Bars As ICommandBars
        Dim pTool_Boton As ICommandBar = Nothing
        pMDocumento = m_application.Document
        pTool_Bars = pMDocumento.CommandBars
        Try
            ' pTool_Bars.HideAllToolbars()
            pTool_Boton = pTool_Bars.Find("Project.BDGeocatmin", False, True)
            If pTool_Boton Is Nothing Then
                'pTool_Bars.HideAllToolbars()
                pTool_Boton = pTool_Bars.Create("BDGeocatmin", ESRI.ArcGIS.SystemUI.esriCmdBarType.esriCmdBarTypeToolbar)
                ' The built in ArcID module is used to find the ArcMap commands. 
                Dim pUID As UID
                For i As Integer = 0 To p_Grilla.Rows.Count - 1
                    pUID = New UID
                    pUID.Value = "{" & p_Grilla.Rows(i).Item("GUID") & "}" '"{22534c7c-6632-4c4f-9671-bce2954c82de}"
                    pTool_Boton.Add(pUID)
                Next

                If My.Computer.Info.OSFullName.Contains("7") Then
                    pTool_Boton.Dock(esriDockFlags.esriDockShow)
                End If

                'pTool_Boton.Dock(esriDockFlags.esriDockShow)
            Else
                If pTool_Boton.IsVisible Then
                    For w As Integer = 0 To pTool_Boton.Count - 1
                        pTool_Boton.Item(0).Delete()
                    Next
                    Dim pUID As UID
                    For i As Integer = 0 To p_Grilla.Rows.Count - 1
                        pUID = New UID
                        pUID.Value = "{" & p_Grilla.Rows(i).Item("GUID") & "}" '"{22534c7c-6632-4c4f-9671-bce2954c82de}"
                        pTool_Boton.Add(pUID)
                    Next
                Else

                    If My.Computer.Info.OSFullName.Contains("7") Then
                        pTool_Boton.Dock(esriDockFlags.esriDockShow)
                    End If

                    'pTool_Boton.Dock(esriDockFlags.esriDockShow)
                End If
            End If
        Catch ex As Exception
            ' MsgBox("Error")
        End Try
    End Sub

    Public Sub Create_Barra_2(ByVal m_application As IApplication, ByVal p_Grilla As DataTable, ByVal p_NomVentana As String)
        Dim pMDocumento As IDocument
        Dim pTool_Bars As ICommandBars
        Dim pTool_Boton As ICommandBar = Nothing
        pMDocumento = m_application.Document
        pTool_Bars = pMDocumento.CommandBars
        Try
            'pTool_Bars.HideAllToolbars()
            pTool_Boton = pTool_Bars.Find("Project." & p_NomVentana, False, True)
            If pTool_Boton Is Nothing Then
                pTool_Boton = pTool_Bars.Create(p_NomVentana, ESRI.ArcGIS.SystemUI.esriCmdBarType.esriCmdBarTypeToolbar)
                ' The built in ArcID module is used to find the ArcMap commands. 
                Dim pUID As UID
                For i As Integer = 0 To p_Grilla.Rows.Count - 1
                    pUID = New UID
                    pUID.Value = "{" & p_Grilla.Rows(i).Item("GUID") & "}" '"{22534c7c-6632-4c4f-9671-bce2954c82de}"
                    pTool_Boton.Add(pUID)
                Next

                If My.Computer.Info.OSFullName.Contains("7") Then
                    pTool_Boton.Dock(esriDockFlags.esriDockShow)
                End If

                'pTool_Boton.Dock(esriDockFlags.esriDockShow)
            Else
                If pTool_Boton.IsVisible Or pTool_Boton.Count <> 0 Then
                    For w As Integer = 0 To pTool_Boton.Count - 1
                        pTool_Boton.Item(0).Delete()
                    Next
                    Dim pUID As UID
                    For i As Integer = 0 To p_Grilla.Rows.Count - 1
                        Try
                            pUID = New UID
                            pUID.Value = "{" & p_Grilla.Rows(i).Item("GUID") & "}" '"{22534c7c-6632-4c4f-9671-bce2954c82de}"
                            pTool_Boton.Add(pUID)

                            If My.Computer.Info.OSFullName.Contains("7") Then
                                pTool_Boton.Dock(esriDockFlags.esriDockShow)
                            End If

                            'pTool_Boton.Dock(esriDockFlags.esriDockShow)
                            pTool_Boton.Dock(esriDockFlags.esriDockRight)
                        Catch ex As Exception
                            MsgBox("Error linea: " & i)
                        End Try
                    Next
                Else

                    If My.Computer.Info.OSFullName.Contains("7") Then
                        pTool_Boton.Dock(esriDockFlags.esriDockShow)
                    End If

                    'pTool_Boton.Dock(esriDockFlags.esriDockShow)
                End If
            End If
        Catch ex As Exception
            ' MsgBox("Error")
        End Try
    End Sub

End Class
