Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Desktop.AddIns
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry

Public Class DrawPolygon
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool
    'Public Event HERIDGrabbed()
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
        screenDisplay.StartDrawing(screenDisplay.hDC, CShort(esriScreenCache.esriNoScreenCache))
        Dim rgbColor As IRgbColor = New RgbColorClass
        rgbColor.Red = 255
        rgbColor.Transparency = 0.9

        'Dim fillColor As IRgbColor = New RgbColorClass
        'fillColor.Transparency = 1

        Dim color As IColor = rgbColor ' Implicit Cast
        Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
        simpleFillSymbol.Color = color
        simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSNull

        Dim symbol As ISymbol = TryCast(simpleFillSymbol, ISymbol) ' Dynamic Cast
        Dim rubberBand As IRubberBand = New RubberRectangularPolygonClass
        Dim geometry As ESRI.ArcGIS.Geometry.IGeometry = rubberBand.TrackNew(screenDisplay, symbol)

        Try
            screenDisplay.SetSymbol(symbol)
            screenDisplay.DrawPolygon(geometry)


            Form_mapa_peligros_geologicos.GetInstance().tbx_xmax.Text = geometry.Envelope.XMax
            Form_mapa_peligros_geologicos.GetInstance().tbx_ymax.Text = geometry.Envelope.YMax
            Form_mapa_peligros_geologicos.GetInstance().tbx_xmin.Text = geometry.Envelope.XMin
            Form_mapa_peligros_geologicos.GetInstance().tbx_ymin.Text = geometry.Envelope.YMin
            Form_mapa_peligros_geologicos.GetInstance().Update()

            drawPolygon_x_min = geometry.Envelope.XMin
            drawPolygon_y_min = geometry.Envelope.YMin

            Dim element As IElement = New PolygonElement()
            element.Geometry = geometry
            Dim PolElement As IPolygonElement = element
            Dim fillShapeElement As IFillShapeElement = TryCast(PolElement, IFillShapeElement)
            fillShapeElement.Symbol = simpleFillSymbol

            graphicsContainer.AddElement(fillShapeElement, 0)
            activeView.Refresh()
        Catch ex As Exception

        Finally
            screenDisplay.FinishDrawing()
        End Try
        MyBase.OnMouseDown(arg)
    End Sub

End Class
