Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

Public Class frm_opcionplanos
    Public m_Application As IApplication

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dim valor_plano As String = ""
        Dim cadena_planos_espe1 As String

        If Me.Check_dato1.Checked = True Then
            sele_opcion_plano = "caso1"
            valor_plano = "V"
            cadena_planos_espe = "SITUACION =  '" & valor_plano & "'"
        ElseIf Me.Check_dato2.Checked = True Then
            sele_opcion_plano = "caso2"
            valor_plano = "V"
            cadena_planos_espe = "SITUACION =  '" & valor_plano & "'"
        ElseIf Me.Check_dato3.Checked = True Then
            valor_plano = "V"
            sele_opcion_plano = "caso3"
            'cadena_planos_espe = ("((SITUACION =  '" & valor_plano & "') OR "  ("SITUACION =  '" & valor_plano & "'  AND ESTADO =  'L'")
            'MsgBox(cadena_planos_espe)
            cadena_planos_espe1 = "SITUACION =  '" & valor_plano & "'"

            ' consulta.WhereClause = "((EVAL = 'EV' Or EVAL = '" & criterio1 & "' or EVAL = '" & criterio2 & "' or EVAL = '" & criterio3 & "' or EVAL = '" & criterio4 & "' or EVAL = '" & criterio5 & "' or EVAL = '" & criterio6 & "' or EVAL = '" & criterio7 & "' or EVAL = '" & criterio8 & "'"

            'cadena_query = cadena_query & " OR " & "CODIGO =  '" & cod_rese & "' AND CLASE =  '" & clase_sele & "'"

            '_expre = "((([situacion]= " + +"V".quote + ")) OR (([situacion]= " + +"X".quote + ") and ([ESTADO]= " + +"L".quote + ")))"
            cadena_planos_espe = cadena_planos_espe1 & " OR " & "SITUACION =  'X' AND ESTADO =  'L'"
            MsgBox(cadena_planos_espe)

        ElseIf Me.Check_dato4.Checked = True Then
            valor_plano = "V"
            sele_opcion_plano = "caso4"
            cadena_planos_espe1 = "SITUACION =  '" & valor_plano & "'"
            ' cadena_planos_espe = (("SITUACION =  '" & valor_plano & "') OR  ((SITUACION =  '" & valor_plano & "')  AND (ESTADO =  'L' OR ESTADO =  'J' OR ESTADO =  'H' OR ESTADO =  'X'"))

            ' MsgBox(cadena_planos_espe)
            cadena_planos_espe = cadena_planos_espe1 & " OR " & "SITUACION =  'X' AND ESTADO =  'L' OR ESTADO =  'J' OR ESTADO =  'H' OR ESTADO =  'X'"
            MsgBox(cadena_planos_espe)
            '_expre = "((([situacion]= " + +"V".quote + ")) OR ((([situacion]= " + +"X".quote + ")) and (([ESTADO]= " + +"L".quote + ") OR ([ESTADO]= " + +"J".quote + ") OR ([ESTADO]= " + +"H".quote + ") OR ([ESTADO]= " + +"X".quote + "))))"

        End If
    End Sub

    Private Sub Check_dato1_CheckedChanged(sender As Object, e As EventArgs) Handles Check_dato1.CheckedChanged
        If Me.Check_dato1.Checked = True Then
            v_checkbox_1 = Me.Check_dato1.Text
            Me.Check_dato2.Checked = False
            v_checkbox_2 = ""
            Me.Check_dato3.Checked = False
            v_checkbox_3 = ""
            Me.Check_dato4.Checked = False
            v_checkbox_4 = ""
        Else
            v_checkbox_1 = ""
        End If
    End Sub

    Private Sub Check_dato2_CheckedChanged(sender As Object, e As EventArgs) Handles Check_dato2.CheckedChanged

        If Me.Check_dato2.Checked = True Then
            v_checkbox_2 = Me.Check_dato2.Text
            Me.Check_dato1.Checked = False
            v_checkbox_1 = ""
            Me.Check_dato3.Checked = False
            v_checkbox_3 = ""
            Me.Check_dato4.Checked = False
            v_checkbox_4 = ""
        Else
            v_checkbox_2 = ""
        End If

    End Sub

    Private Sub Check_dato3_CheckedChanged(sender As Object, e As EventArgs) Handles Check_dato3.CheckedChanged


        If Me.Check_dato3.Checked = True Then
            v_checkbox_3 = Me.Check_dato3.Text
            Me.Check_dato1.Checked = False
            v_checkbox_1 = ""
            Me.Check_dato2.Checked = False
            v_checkbox_2 = ""
            Me.Check_dato4.Checked = False
            v_checkbox_4 = ""
        Else
            v_checkbox_3 = ""
        End If

    End Sub

    Private Sub Check_dato4_CheckedChanged(sender As Object, e As EventArgs) Handles Check_dato4.CheckedChanged
        If Me.Check_dato4.Checked = True Then
            v_checkbox_4 = Me.Check_dato4.Text
            Me.Check_dato1.Checked = False
            v_checkbox_1 = ""
            Me.Check_dato2.Checked = False
            v_checkbox_2 = ""
            Me.Check_dato3.Checked = False
            v_checkbox_3 = ""
        Else
            v_checkbox_4 = ""
        End If
    End Sub

    Private Sub frm_opcionplanos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class