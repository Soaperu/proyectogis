Imports System.Data.SQLite
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI

Module settings
    '1. Metadata
    '   - Variables que obtienen informacion sobre desarrollo, fecha, etc.

    Public __title__ As String = "Automapic " & Date.Today.Year.ToString()
    Public __author__ As String = "Daniel Fernando Aguado Huaccharaqui"
    Public __copyright__ As String = "INGEMMET " & Date.Today.Year.ToString()
    Public __credits__ As String = "Daniel Aguado H., Jorge Yupanqui H."
    Public __version__ As String = "1.0.1"
    Public __maintainer__ As String = __credits__
    Public __mail__ As String = "proyectososi01@ingemmet.gob.pe"
    Public __status__ As String = "Production"
    'Public __status__ As String = "Development"
    Public __tempdir__ As String = System.IO.Path.GetTempPath()

    '2. Variables globales estaticas
    '   - Estas variables no deben ser modificadas durante el proceso
    '   - Su nombre inicia con un guin bajo

    '* _path: Obtiene la ruta actual en donde se almacena la instalacion del addin
    Public _path As String = __file__()
    Public _scripts_path As String = _path & "\scripts"
    Public _layer_path As String = _scripts_path & "\layers"
    Public _image_path As String = _scripts_path & "\img"
    Public _path_sqlite As String = _scripts_path & "\automapic.db"
    Public _path_loader As String = _image_path & "\loader.gif"
    Public _url_SWLoginService As String = "https://srvstd.ingemmet.gob.pe/WS_Seguridad/SWLogin.svc"
    Public _url_InteractionAPI As String = "https://geocatminapp.ingemmet.gob.pe/bdgeocientifica/app/api/account/IteracionService/15/{0}"
    Public _style_path As String = _layer_path & "\ULITO_50K.style"
    Public _style_path_pog As String = _layer_path & "\POG_50K.style"
    Public _style_path_geom As String = _layer_path & "\GEOMORFOLOGIA.style"

    '3. Variables dinamicas alterables segun fin
    '   - Estas variables solo podran ser alteradas manejandolas dentro del contexto que fueron creados

    '* controller_sesion: Variable que toma valores de {0: "Sin incio de sesion, 1: "Usuario logeado"}
    Public user As String
    Public pass As String
    Public currentModule As Integer
    Public currentModuleName As String
    Public modulosDict As New Dictionary(Of Integer, String)
    Public modulosPerfilDict As New Dictionary(Of Integer, Integer)
    Public modulosManualDict As New Dictionary(Of Integer, String)
    Public controller_sesion As Integer = 0
    Public python_path As String = ""
    Public nameUser As String = ""


    '4. Variables globales dinamicas
    ' - Su valor puede alterarse en todo los procesos

    Public d_contador As Integer = 0
    Public d_standar_output As String
    Public _LOADER_CONTROL As System.Windows.Forms.ProgressBar
    Public drawLine_wkt As String
    Public codHoja_mgno50k As String
    Public drawPolygon_x_min As Decimal
    Public drawPolygon_y_min As Decimal
    Public usersList As New List(Of String)
    Public usersDictionary As New Dictionary(Of String, String)
    Public usersValuesDictionary As New Dictionary(Of String, String)
    Public ulitoLabelsDictionary As New Dictionary(Of String, String)
    Public PGOLabelsDictionary As New Dictionary(Of String, String)
    Public selectediduser As String
    Public mxdDoc As IMxDocument
    Public map As IMap


    Public xmin As Double
    Public ymin As Double
    Public xmax As Double
    Public ymax As Double

    '5. Nombre de formatos GIS

    Public f_shapefile As String = "shapefile"
    Public f_featureclass As String = "featureclass"
    Public f_geodatabase As String = "geodatabase"
    Public f_raster As String = "raster"
    Public f_workspace As String = "workspace"
    Public f_table As String = "table"
    Public f_file As String = "file"
    Public f_mxd As String = "mxd"
    Public f_folder As String = "folder"


    Public message_runtime_error As String = "¡Ocurrio un error inesperado!" + vbCrLf + "La herramiento de geoprocesamiento retorno un valor nulo"

    'connection database sqlite
    Public connection As String = "Data Source=" & _path_sqlite
    Public SQLConn As New SQLiteConnection(connection)
    Public SQLcmd As New SQLiteCommand(SQLConn)
    Public SQLdr As SQLiteDataReader

    '6. Funciones globales
    '   - Funciones que devuelven resultados y que puedes ser usados en cualquier parte del proceso

    '* __file__: Obtiene la ruta actual en donde se almacena la instalacion del addin
    '* parametros: No recibe parametros

    Public Function __file__()
        Dim codeBase As String = Reflection.Assembly.GetExecutingAssembly.CodeBase
        Dim uriBuilder As UriBuilder = New UriBuilder(codeBase)
        Dim path As String = Uri.UnescapeDataString(uriBuilder.Path)
        Return IO.Path.GetDirectoryName(path)
    End Function

    Public Function openFormByName(myForm As Form, container As Control)
        Dim existForm As Boolean = container.Controls.Contains(myForm)
        If (existForm) Then
            Return Nothing
        Else
            container.Controls.Clear()
        End If
        myForm.TopLevel = False
        myForm.AutoScroll = True
        myForm.Size = container.Size
        myForm.Dock = DockStyle.Fill
        container.Controls.Add(myForm)
        myForm.Show()
        myForm.Focus()
        Return Nothing
    End Function

    Public Function runProgressBar(Optional position As String = Nothing)
        If _LOADER_CONTROL Is Nothing Then
            Return Nothing
        End If

        If (position = "ini") Then
            _LOADER_CONTROL.Value = 0
        ElseIf (position = "end") Then
            _LOADER_CONTROL.Value = 100
        ElseIf (position Is Nothing) Then
            Dim number = New Random
            _LOADER_CONTROL.Value = number.Next(20, 80)
        Else
            _LOADER_CONTROL.Value = Int32.Parse(position)
        End If
        Return Nothing
    End Function
    Public Function successProcess()
        Dim message As String = "Proceso finalizado con exito"
        MessageBox.Show(message, __title__, MessageBoxButtons.OK, MessageBoxIcon.Information)
        runProgressBar("ini")
        Return Nothing
    End Function
    Public Function GetFilter(filetype As String) As Object
        Dim objfilter As IGxObjectFilter = Nothing
        Select Case filetype
            Case f_shapefile
                objfilter = New GxFilterShapefiles()
            Case f_featureclass
                objfilter = New GxFilterFGDBFeatureClasses()
            Case f_geodatabase
                objfilter = New GxFilterFileGeodatabases()
            Case f_raster
                objfilter = New GxFilterRasterDatasets()
            Case f_workspace
                objfilter = New GxFilterWorkspaces()
            Case f_table
                objfilter = New GxFilterTables()
            Case f_file
                objfilter = New GxFilterFiles()
            Case f_mxd
                objfilter = New GxFilterMaps()
            Case f_folder
                objfilter = New GxFilterFileFolder()
        End Select
        Return objfilter
    End Function

    Public Function openDialogBoxESRI(filetype As String, Optional textButton As String = "Agregar") As Object
        Dim pEnumGX As IEnumGxObject = Nothing
        Dim pGxDialog As IGxDialog = New GxDialogClass
        pGxDialog.AllowMultiSelect = False
        pGxDialog.Title = "Seleccionar"
        If filetype IsNot Nothing Then
            pGxDialog.Title = String.Format("Seleccionar {0}", filetype)
        End If
        pGxDialog.ObjectFilter = GetFilter(filetype)
        pGxDialog.ButtonCaption = textButton

        If Not pGxDialog.DoModalOpen(0, pEnumGX) Then
            Return Nothing
        End If

        Dim objGxObject As IGxObject = pEnumGX.Next
        Return objGxObject.FullName

    End Function
    Public Sub requestInteraction(name_module As String)
        Try
            Dim url_string = String.Format(_url_InteractionAPI, name_module)
            Dim webClient As New Net.WebClient
            webClient.DownloadString(url_string)
        Catch ex As Exception
            Console.WriteLine(ex.Message())
        End Try
    End Sub

End Module
