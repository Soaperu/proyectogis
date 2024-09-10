Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Public Class DrawLine
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

    Public Sub New()


    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
    Protected Overrides Sub OnMouseDown(arg As MouseEventArgs)
        Dim pMxDoc As IMxDocument
        pMxDoc = My.ArcMap.Application.Document
        Dim graphicsContainer As IGraphicsContainer = pMxDoc.FocusMap
        graphicsContainer.DeleteAllElements()
        Dim activeView As IActiveView = pMxDoc.ActiveView
        Dim screenDisplay As IScreenDisplay = activeView.ScreenDisplay
        'screenDisplay.StartDrawing(screenDisplay.hDC, CShort(esriScreenCache.esriScreenRecording))
        screenDisplay.StartDrawing(screenDisplay.hDC, CShort(esriScreenCache.esriNoScreenCache))
        Dim rgbColor As IRgbColor = New RgbColorClass
        rgbColor.Red = 255
        'rgbColor.Green = 89
        'rgbColor.Blue = 69

        Dim color As IColor = rgbColor ' Implicit Cast
        Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
        simpleLineSymbol.Color = color
        simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDash
        simpleLineSymbol.Width = 1

        Dim symbol As ISymbol = TryCast(simpleLineSymbol, ISymbol) ' Dynamic Cast
        Dim rubberBand As IRubberBand = New RubberLineClass()
        Dim geometry As IGeometry = rubberBand.TrackNew(screenDisplay, symbol)

        Try
            screenDisplay.SetSymbol(symbol)
            screenDisplay.DrawPolyline(geometry)
            Dim pointCollection As IPointCollection = TryCast(geometry, IPointCollection)

            Dim aryTextFile(pointCollection.PointCount - 1) As String

            For i As Integer = 0 To pointCollection.PointCount - 1
                Dim point As IPoint = pointCollection.Point(i)
                aryTextFile(i) = String.Format("{0} {1}", point.X, point.Y)
            Next

            drawLine_wkt = String.Format("LINESTRING ({0})", String.Join(",", aryTextFile))

            Dim element As IElement = New LineElement()
            element.Geometry = geometry
            Dim LineElement As ILineElement = element
            LineElement.Symbol = simpleLineSymbol

            graphicsContainer.AddElement(LineElement, 0)
            activeView.Refresh()
        Catch ex As Exception

        Finally
            screenDisplay.FinishDrawing()
        End Try

        MyBase.OnMouseDown(arg)
    End Sub
End Class
