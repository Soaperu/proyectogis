Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class UserControl_ViewerAttachment
    Dim imageListAttachaments As ImageList = New ImageList()
    Dim pathsAttachaments As String() = {}
    Dim controller As Integer
    Dim dirPathProp As String
    Dim attachmentsSelected As New List(Of String)
    Public Sub populateListView(dirpath As String)

        dirPathProp = dirpath

        lvw_uc_attachments.Clear()
        imageListAttachaments.Images.Clear()

        'lvw_uc_attachments.View = View.Details
        'If lvw_uc_attachments.Columns.Count = 0 Then
        '    lvw_uc_attachments.Columns.Add("Attachments")
        '    lvw_uc_attachments.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize)
        'End If

        imageListAttachaments.ImageSize = New Size(100, 100)
        pathsAttachaments = Directory.GetFiles(dirPathProp)
        If pathsAttachaments.Length = 0 Then
            Return
        End If

        controller = 0
        For Each attach In pathsAttachaments
            imageListAttachaments.Images.Add(Image.FromFile(attach))
            lvw_uc_attachments.Items.Add(Path.GetFileName(attach), controller)
            controller = controller + 1
        Next
        lvw_uc_attachments.LargeImageList = imageListAttachaments
        'For index As Integer = 0 To pathsAttachaments.Length - 1
        '    lvw_uc_attachments.Items.Add(pathsAttachaments.GetValue(index), index)
        '    'controller = controller + 1
        'Next
    End Sub

    Private Sub lvw_uc_attachments_DoubleClick(sender As Object, e As EventArgs) Handles lvw_uc_attachments.DoubleClick
        Try
            Dim textSelected As String = lvw_uc_attachments.SelectedItems(0).SubItems(0).Text
            Process.Start(String.Format("{0}\{1}", dirPathProp, textSelected))
        Catch ex As Exception
            Return
        End Try

    End Sub

    Public Function getAttachmentsSelected()
        attachmentsSelected.Clear()
        For Each item As ListViewItem In lvw_uc_attachments.SelectedItems()
            attachmentsSelected.Add(String.Format("{0}\{1}", dirPathProp, item.Text))
        Next
        Return String.Join(",", attachmentsSelected)
    End Function
End Class
