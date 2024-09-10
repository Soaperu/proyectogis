<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl_CheckBoxAddLayers
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserControl_CheckBoxAddLayers))
        Me.tvw_layers = New System.Windows.Forms.TreeView()
        Me.img_layers = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UserControl_ComboBoxDataframes1 = New Automapic.UserControl_ComboBoxDataframes()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvw_layers
        '
        Me.tvw_layers.CheckBoxes = True
        Me.tvw_layers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvw_layers.ImageIndex = 0
        Me.tvw_layers.ImageList = Me.img_layers
        Me.tvw_layers.LineColor = System.Drawing.Color.Silver
        Me.tvw_layers.Location = New System.Drawing.Point(0, 44)
        Me.tvw_layers.Name = "tvw_layers"
        Me.tvw_layers.SelectedImageIndex = 1
        Me.tvw_layers.ShowLines = False
        Me.tvw_layers.Size = New System.Drawing.Size(366, 324)
        Me.tvw_layers.TabIndex = 0
        '
        'img_layers
        '
        Me.img_layers.ImageStream = CType(resources.GetObject("img_layers.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img_layers.TransparentColor = System.Drawing.Color.Transparent
        Me.img_layers.Images.SetKeyName(0, "LayerGroupNotVisible16.png")
        Me.img_layers.Images.SetKeyName(1, "LegendLayers16.png")
        Me.img_layers.Images.SetKeyName(2, "LayerGroupNew16.png")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tvw_layers)
        Me.Panel1.Controls.Add(Me.UserControl_ComboBoxDataframes1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(366, 368)
        Me.Panel1.TabIndex = 3
        '
        'UserControl_ComboBoxDataframes1
        '
        Me.UserControl_ComboBoxDataframes1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UserControl_ComboBoxDataframes1.Location = New System.Drawing.Point(0, 0)
        Me.UserControl_ComboBoxDataframes1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UserControl_ComboBoxDataframes1.Name = "UserControl_ComboBoxDataframes1"
        Me.UserControl_ComboBoxDataframes1.Size = New System.Drawing.Size(366, 44)
        Me.UserControl_ComboBoxDataframes1.TabIndex = 1
        '
        'UserControl_CheckBoxAddLayers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UserControl_CheckBoxAddLayers"
        Me.Size = New System.Drawing.Size(366, 368)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tvw_layers As System.Windows.Forms.TreeView
    Friend WithEvents UserControl_ComboBoxDataframes1 As UserControl_ComboBoxDataframes
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents img_layers As System.Windows.Forms.ImageList
End Class
