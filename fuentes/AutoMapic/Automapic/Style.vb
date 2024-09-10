
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports System.Windows.Forms
'Imports ESRI.ArcGIS.

Module Styles

    Public Sub MatchPersonalizedSymbolsUlito(ByVal nombreCapa As String, ByVal fieldName As String, ByVal stylePath As String,
                                             Optional ByVal query As String = "1=1", Optional ByVal dictLabels As Dictionary(Of String, String) = Nothing)
        'Obtener la referencia al documento actual
        Dim pMxdoc As IMxDocument = My.ArcMap.Document
        Dim pMap As IMap = pMxdoc.ActivatedView.FocusMap
        Dim pactiveView As IActiveView = pMxdoc.ActiveView

        'Obtener la referencia a la capa deseada
        Dim Layer As IGeoFeatureLayer = DirectCast(ObtenerCapaPorNombre(pMap, nombreCapa), IGeoFeatureLayer)
        If Layer Is Nothing Then
            Throw New Exception("No se encontró la capa con el nombre especificado.")
        End If

        ' Obtener la referencia al documento activo (IMxDocument)
        'Dim pMxDoc As IMxDocument = My.ArcMap.Document

        ' Obtener la referencia a la Style Gallery del documento (IStyleGallery)
        Dim pStyleGallery As IStyleGallery = pMxDoc.StyleGallery
        Dim pStyleGalleryStorage As IStyleGalleryStorage = pStyleGallery
        Dim qQFilter As IQueryFilter
        qQFilter = New QueryFilter
        qQFilter.WhereClause = query
        Dim pFeatureCursor As IFeatureCursor = Layer.Search(qQFilter, False)
        Dim listaDatos As New List(Of Integer)

        'Obtener una lista de los codigo de simbolos en la capa
        Dim pFeature As IFeature = New Feature
        Dim nfm1 As Long
        'Obtenemos la cantida de registros de la capa para utilizar en el cursor
        'nfm1 = Layer.FeatureClass.FeatureCount(qQFilter)
        Dim table As ITable = CType(Layer, ITable)
        nfm1 = table.RowCount(Nothing)
        'Corremos el cursor para agregar los codigo de etiqueta a >> listaDatos
        For i = 0 To nfm1 - 1
            'Do Until pFeature Is Nothing
            pFeature = pFeatureCursor.NextFeature
            Dim dato As String = pFeature.Value(pFeature.Fields.FindField(fieldName)).ToString()
            dato = Convert.ToInt32(dato)
            If Not listaDatos.Contains(dato) Then
                listaDatos.Add(dato)
            End If
            'Loop
        Next i
        'Cargar el archivo .style en la Style Gallery
        pStyleGalleryStorage.AddFile(stylePath)

        Dim pRenderer As IUniqueValueRenderer = New UniqueValueRenderer 'Crear un nuevo renderizador de valor único

        pRenderer.FieldCount = 1 'Establecer el número de campos
        pRenderer.Field(0) = fieldName 'Establecer el nombre del campo que contiene los valores coincidentes

        '#Aplicamos los estilos a los casos encontrados
        Dim pEnumStyleGalleryItem As IEnumStyleGalleryItem = pStyleGallery.Items("Fill Symbols", stylePath, "") 'Obtener los símbolos de polígono de la galería de estilos
        Dim pStyleGalleryItem As IStyleGalleryItem = pEnumStyleGalleryItem.Next 'Obtener el primer símbolo de polígono
        While Not pStyleGalleryItem Is Nothing 'Bucle a través de todos los símbolos de polígono
            Dim codi As Long
            Dim label As String
            Dim len_ As Integer
            Dim name As String = pStyleGalleryItem.Name
            len_ = name.Split("_").Length
            If len_ = 2 Then
                codi = Convert.ToInt32(name.Split("_")(0))
                label = name.Split("_")(1).ToString()
                If dictLabels IsNot Nothing Then
                    If ulitoLabelsDictionary.ContainsKey(codi.ToString()) Then
                        label = ulitoLabelsDictionary(codi.ToString())
                    End If
                End If

                If listaDatos.Contains(codi) Then
                    Dim pPolySym As IFillSymbol = pStyleGalleryItem.Item
                    pRenderer.AddValue(pStyleGalleryItem.ID, fieldName, pPolySym) 'Agrega el símbolo al renderizador con el valor coincidente
                    pRenderer.Label(codi) = label 'Cambia la etiqueta del simbolo coincidente
                End If
            ElseIf len_ = 1 Then
                codi = Convert.ToInt32(name.Split("_")(0))
                If listaDatos.Contains(codi) Then
                    Dim pPolySym As IFillSymbol = pStyleGalleryItem.Item
                    pRenderer.AddValue(pStyleGalleryItem.ID, fieldName, pPolySym)
                    pRenderer.Label(codi) = "No definido"
                End If
            End If
            pStyleGalleryItem = pEnumStyleGalleryItem.Next 'Obtener el siguiente símbolo de polígono
        End While
        Layer.Renderer = CType(pRenderer, IFeatureRenderer) 'Asigne el renderizador a la capa de polígono

        'define transparencia
        Dim layerEffects As ILayerEffects = TryCast(Layer, ILayerEffects)
        layerEffects.Transparency = 0
        'Refresca la tabla de contenido del Arcmap - TOC
        pactiveView.ContentsChanged()
        pactiveView.Refresh() 'Refresca la vista de las capas en el mapa

    End Sub


    Public Sub MatchPersonalizedSymbolsPOG(ByVal nombreCapa As String, ByVal fieldName As String, ByVal stylePath As String,
                                             Optional ByVal query As String = "1=1", Optional ByVal dictLabels As Dictionary(Of String, String) = Nothing)
        'Obtener la referencia al documento actual
        Dim pMxdoc2 As IMxDocument = My.ArcMap.Document
        Dim pMap2 As IMap = pMxdoc2.ActivatedView.FocusMap
        Dim pactiveView As IActiveView = pMxdoc2.ActiveView

        'Obtener la referencia a la capa deseada
        Dim Layer2 As IGeoFeatureLayer = DirectCast(ObtenerCapaPorNombre(pMap2, nombreCapa), IGeoFeatureLayer)
        If Layer2 Is Nothing Then
            Throw New Exception("No se encontró la capa con el nombre especificado.")
        End If
        ' Obtener la referencia al documento activo (IMxDocument)
        'Dim pMxDoc As IMxDocument = My.ArcMap.Document
        ' Obtener la referencia a la Style Gallery del documento (IStyleGallery)
        Dim pStyleGallery2 As IStyleGallery = pMxdoc2.StyleGallery
        Dim pStyleGalleryStorage2 As IStyleGalleryStorage = pStyleGallery2
        Dim qQFilter2 As IQueryFilter
        qQFilter2 = New QueryFilter
        qQFilter2.WhereClause = query
        Dim pFeatureCursor2 As IFeatureCursor = Layer2.Search(qQFilter2, False)
        Dim listaDatos As New List(Of Integer)

        'Obtener una lista de los codigo de simbolos en la capa
        Dim pFeature2 As IFeature
        Dim nfm2 As Long
        'Obtenemos la cantida de registros de la capa para utilizar en el cursor
        nfm2 = Layer2.FeatureClass.FeatureCount(qQFilter2)
        'Corremos el cursor para agregar los codigo de etiqueta a >> listaDatos
        For i = 0 To nfm2 - 1
            pFeature2 = pFeatureCursor2.NextFeature
            Dim dato As String = pFeature2.Value(pFeature2.Fields.FindField(fieldName)).ToString()
            dato = Convert.ToInt32(dato)
            If Not listaDatos.Contains(dato) Then
                listaDatos.Add(dato)
            End If
        Next i
        'Cargar el archivo .style en la Style Gallery
        pStyleGalleryStorage2.AddFile(stylePath)

        Dim pRenderer As IUniqueValueRenderer = New UniqueValueRenderer 'Crear un nuevo renderizador de valor único

        pRenderer.FieldCount = 1 'Establecer el número de campos
        pRenderer.Field(0) = fieldName 'Establecer el nombre del campo que contiene los valores coincidentes

        '#Aplicamos los estilos a los casos encontrados
        Dim pEnumStyleGalleryItem2 As IEnumStyleGalleryItem = pStyleGallery2.Items("Marker Symbols", stylePath, "") 'Obtenga los símbolos del punto (marker) de la galería de estilos
        Dim pStyleGalleryItem2 As IStyleGalleryItem = pEnumStyleGalleryItem2.Next 'Obtener el primer símbolo del punto (marker)
        While Not pStyleGalleryItem2 Is Nothing 'Bucle a través de todos los símbolos de punto (marker)
            Dim codi As Long
            Dim label As String = ""
            Dim name As String = pStyleGalleryItem2.Name
            'Debido a la estructura de los estilos del archivo .style de los POG's realizamos una separacion entre " " y "."
            Dim indiceEspacio As Integer = name.IndexOf(" ")
            Dim indicePunto As Integer = name.IndexOf(".")
            If indiceEspacio >= 0 AndAlso (indicePunto < 0 OrElse indiceEspacio < indicePunto) Then
                Try
                    codi = Convert.ToInt32(name.Substring(0, indiceEspacio))
                    label = name.Substring(indiceEspacio + 1)
                Catch ex As Exception
                End Try
            ElseIf indicePunto >= 0 AndAlso (indiceEspacio < 0 OrElse indicePunto < indiceEspacio) Then
                Try
                    codi = Convert.ToInt32(name.Substring(0, indicePunto))
                    label = name.Substring(indicePunto + 1)
                Catch ex As Exception
                End Try
            Else
                codi = pStyleGalleryItem2.ID
                label = "No definido"
            End If
            If dictLabels IsNot Nothing Then
                If PGOLabelsDictionary.ContainsKey(codi.ToString()) Then
                    label = PGOLabelsDictionary(codi.ToString())
                End If
            End If
            If listaDatos.Contains(codi) Then
                Dim pMarkerSym As IMultiLayerMarkerSymbol = pStyleGalleryItem2.Item
                pRenderer.AddValue(pStyleGalleryItem2.ID, fieldName, pMarkerSym) 'Agregue el símbolo al renderizador con el valor coincidente
                pRenderer.Label(codi) = label
            End If
            pStyleGalleryItem2 = pEnumStyleGalleryItem2.Next 'Obtener el siguiente símbolo de polígono
        End While
        pEnumStyleGalleryItem2.Reset()
        Dim pRotationRender As IRotationRenderer = TryCast(pRenderer, IRotationRenderer) 'Asigne la rotación del renderizador a la capa de puntos
        pRotationRender.RotationField = "AZIMUT" 'Se asigna el campo que dara el valor a la rotacion
        pRotationRender.RotationType = esriSymbolRotationType.esriRotateSymbolGeographic 'Se elige el modo de rotacion geografica

        Layer2.Renderer = CType(pRenderer, IFeatureRenderer) 'Asignar todos los estilos existentes a la capa
        'Refresh the table of contents - TOC
        pactiveView.ContentsChanged()
        pactiveView.Refresh() 'Refresh the map view

    End Sub

    '
    Public Sub MatchPersonalizedSymbolsGeomorphology(ByVal nombreCapa As String, ByVal fieldName As String, ByVal stylePath As String,
                                             Optional ByVal query As String = "1=1", Optional ByVal dictLabels As Dictionary(Of String, String) = Nothing)

        'Obtener la referencia al documento actual
        Dim pMxdoc As IMxDocument = My.ArcMap.Document
        Dim pMap As IMap = pMxdoc.ActivatedView.FocusMap
        Dim pactiveView As IActiveView = pMxdoc.ActiveView

        'Obtener la referencia a la capa deseada
        Dim Layer As IGeoFeatureLayer = DirectCast(ObtenerCapaPorNombre(pMap, nombreCapa), IGeoFeatureLayer)
        If Layer Is Nothing Then
            Throw New Exception("No se encontró la capa con el nombre especificado.")
        End If

        ' Obtener la referencia al documento activo (IMxDocument)
        'Dim pMxDoc As IMxDocument = My.ArcMap.Document

        ' Obtener la referencia a la Style Gallery del documento (IStyleGallery)
        Dim pStyleGallery As IStyleGallery = pMxdoc.StyleGallery
        Dim pStyleGalleryStorage As IStyleGalleryStorage = pStyleGallery
        Dim qQFilter As IQueryFilter
        qQFilter = New QueryFilter
        qQFilter.WhereClause = query
        Dim pFeatureCursor As IFeatureCursor = Layer.Search(qQFilter, False)
        Dim listaDatos As New List(Of String)

        'Obtener una lista de los codigo de simbolos en la capa
        Dim pFeature As IFeature = New Feature
        Dim nfm1 As Long
        'Obtenemos la cantida de registros de la capa para utilizar en el cursor
        'nfm1 = Layer.FeatureClass.FeatureCount(qQFilter)
        Dim table As ITable = CType(Layer, ITable)
        nfm1 = table.RowCount(Nothing)
        'Corremos el cursor para agregar los codigo de etiqueta a >> listaDatos
        For i = 0 To nfm1 - 1
            pFeature = pFeatureCursor.NextFeature
            Dim dato As String = pFeature.Value(pFeature.Fields.FindField(fieldName)).ToString()
            'dato = Convert.ToInt32(dato)
            If Not listaDatos.Contains(dato) Then
                listaDatos.Add(dato)
            End If
        Next i
        'Cargar el archivo .style en la Style Gallery
        pStyleGalleryStorage.AddFile(stylePath)

        Dim pRenderer As IUniqueValueRenderer = New UniqueValueRenderer 'Crear un nuevo renderizador de valor único

        pRenderer.FieldCount = 1 'Establecer el número de campos
        pRenderer.Field(0) = fieldName 'Establecer el nombre del campo que contiene los valores coincidentes

        '#Aplicamos los estilos a los casos encontrados
        Dim pEnumStyleGalleryItem As IEnumStyleGalleryItem = pStyleGallery.Items("Fill Symbols", stylePath, "") 'Obtener los símbolos de polígono de la galería de estilos
        Dim pStyleGalleryItem As IStyleGalleryItem = pEnumStyleGalleryItem.Next 'Obtener el primer símbolo de polígono
        While Not pStyleGalleryItem Is Nothing 'Bucle a través de todos los símbolos de polígono
            Dim codi As String = ""
            Dim label As String
            Dim len_ As Integer
            Try
                Dim name As String = pStyleGalleryItem.Name
                len_ = name.Split(", ").Length
                If len_ = 2 Then
                    codi = name.Split(", ")(0).ToString()
                    label = name.Split(", ")(1).ToString()
                    If dictLabels IsNot Nothing Then
                        If ulitoLabelsDictionary.ContainsKey(codi.ToString()) Then
                            label = ulitoLabelsDictionary(codi.ToString())
                        End If
                    End If

                    If listaDatos.Contains(codi) Then
                        Dim pPolySym As IFillSymbol = pStyleGalleryItem.Item
                        pRenderer.AddValue(codi, fieldName, pPolySym) 'Agrega el símbolo al renderizador con el valor coincidente
                        pRenderer.Label(codi) = codi & " -" & label 'Cambia la etiqueta del simbolo coincidente
                    End If
                ElseIf len_ = 1 Then
                    codi = name.Split(", ")(0).ToString()
                    If listaDatos.Contains(codi) Then
                        Dim pPolySym As IFillSymbol = pStyleGalleryItem.Item
                        pRenderer.AddValue(codi, fieldName, pPolySym)
                        pRenderer.Label(codi) = "No definido"
                    End If
                End If
                pStyleGalleryItem = pEnumStyleGalleryItem.Next 'Obtener el siguiente símbolo de polígono
            Catch ex As Exception
                MessageBox.Show(ex.Message + codi, __title__, MessageBoxButtons.OK, MessageBoxIcon.Error)
                pStyleGalleryItem = Nothing
            End Try
        End While
        Layer.Renderer = CType(pRenderer, IFeatureRenderer) 'Asigne el renderizador a la capa de polígono

        'define transparencia
        Dim layerEffects As ILayerEffects = TryCast(Layer, ILayerEffects)
        layerEffects.Transparency = 15
        'Refresca la tabla de contenido del Arcmap - TOC
        pactiveView.ContentsChanged()
        pactiveView.Refresh() 'Refresca la vista de las capas en el mapa
    End Sub

    'Funcion para buscar la capa deseada para modificar la simbologia y etiqueta de las mismas
    Private Function ObtenerCapaPorNombre(ByVal pMapa As IMap, ByVal nombreCapa As String) As ILayer
        For i As Integer = 0 To pMapa.LayerCount - 1
            Dim pLayer As ILayer = pMapa.Layer(i)
            If pLayer.Name = nombreCapa Then
                Return pLayer
            End If
        Next
        Return Nothing
    End Function


End Module
