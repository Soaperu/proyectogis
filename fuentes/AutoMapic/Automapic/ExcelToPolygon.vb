Public Class ExcelToPolygon
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        GPToolDialog(_tool_covertirExcelPoligono, False, _toolboxPath_mapa_potencialminero)
    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
