Imports System.Windows.Forms

Public Class form_ueas_consulta_nombre

    Private Sub dg_seleccionar_uea_CellMouseDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg_seleccionar_uea.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            coduea = dg_seleccionar_uea.Rows(e.RowIndex).Cells(0).Value
            Me.Close()
        End If
    End Sub
End Class