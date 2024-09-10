Imports System.Drawing
Imports PORTAL_Clases
Imports System.Windows.Forms
Imports System.Linq
Imports ESRI.ArcGIS.Framework

Public Class frm_ExcluirDM_UEA
    Private cls_oracle As New cls_Oracle
    Public m_Application As IApplication
    Private cls_Catastro As New cls_DM_1
    Private cls_eval As New Cls_evaluacion
    Private v_codi As String

    Private Sub frm_ExcluirDM_UEA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUEA.Text = v_codigo


        dt_DataUEA = cls_oracle.FT_DATOS_UEAS("8", v_codigo)
        'v_cod_x = dt_DataUEA.Rows(0).Item("COD_UEA").ToString
        dgvExcluidosUEA.DataSource = dt_DataUEA
        'dgvIntegraUEA.DataSource = dt_DataUEA

    End Sub

    'Private Sub dgvExcluidosUEA_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles dgvExcluidosUEA.RowStateChanged

    'End Sub

    Private Sub btnGraficarEx_Click(sender As Object, e As EventArgs) Handles btnGraficarEx.Click
        Dim contador As Integer

        For Each RW As DataGridViewRow In dgvExcluidosUEA.SelectedRows
            v_codi = RW.Cells(0).Value.ToString
            'v_codi = RW.Cells(1).Value.ToString

            If contador = 0 Then
                lista_exclusionUEA = "CODIGOU =  '" & v_codi & "'"
            ElseIf contador > 0 Then
                lista_exclusionUEA = lista_exclusionUEA & " OR " & "CODIGOU =  '" & v_codi & "'"
            End If
            contador += 1
        Next

        'For contador As Integer = 0 To Me.dgvExcluidosUEA.SelectedRows.Count - 1
        '    v_codi = dgvExcluidosUEA.Item(0, dgvExcluidosUEA.CurrentRow.Index).Value
        'Next contador
 
        cls_Catastro.Add_ShapeFile_tmp("IntegUEA" & fecha_archi, m_Application)
        'cls_eval.agregacampotema_tpm("IntegrantesUEA", "IntegrantesUEA") 'Agrega campo 
        'cls_Catastro.ClearLayerSelection(pFeatureLayer)
        cls_Catastro.Add_ShapeFile("IntegUEA" & fecha_archi, m_Application)
        cls_eval.consultacapaDM("IntegraUEA", "IntegraUEA", "Catastro")
        cls_Catastro.Expor_Tema("IntegUEA", sele_denu, m_Application)

        Exit Sub

    End Sub
End Class