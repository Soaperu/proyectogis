import arcpy
import os
import json
import xmltodict
import messages_aut as msg

def check_layer_inside_data_frame(features, symbols, df_name=None, query=None, zoom=False):
    mxd = arcpy.mapping.MapDocument("CURRENT")
    if df_name :
        dfs = arcpy.mapping.ListDataFrames(mxd, '*{}*'.format(df_name))
        if not len(dfs):
            raise RuntimeError()
        df = dfs[0]
    else:
        df = mxd.activeDataFrame

    features = zip(features, symbols)

    for feature, symbol in features:
        name = os.path.basename(feature)
        lyrs = arcpy.mapping.ListLayers(mxd, "*{}*".format(name), df)
        if not len(lyrs):
            lyr = arcpy.mapping.Layer(feature)
        else:
            lyr = lyrs[0]
        
        if query:
            lyr.definitionQuery = query
        
        if symbol:
            arcpy.ApplySymbologyFromLayer_management(lyr, symbol)

        if not len(lyrs):
            arcpy.mapping.AddLayer(df, lyr)
        
        if zoom:
            mxd.activeDataFrame.extent = lyr.getExtent()
        
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()

def add_layer_with_new_datasource(layer, name_feature, workspace, typeWorkspace, df_name=None, query=None, zoom=False, scale=None, mxd=None):
    """
    Add a layer to the map document with a new data source.
    :param layer: path layer to add.
    :param name_feature: name of the feature class.
    :param workspace: workspace where the feature class is located.
    :param typeWorkspace: type of workspace (FILEGDB_WORKSPACE, SDE_WORKSPACE..).
    :param df_name: name of the data frame.
    :param query: query to apply to the layer.
    :param zoom: zoom to the layer.
    :param scale: scale of the layer.
    """
    # Mapa actual
    if not mxd:
        mxd = arcpy.mapping.MapDocument("CURRENT")

    # Si no se ingresa un nombre de daraframe
    if df_name:
        dfs = arcpy.mapping.ListDataFrames(mxd, "*{}*".format(df_name))
        if not len(dfs):
            raise RuntimeError(msg._ERROR_NO_SUCH_DATAFRAME)
        df = dfs[0]
    else:
        df = mxd.activeDataFrame
    
    # Obteniendo el nombre del layer
    name = os.path.basename(layer).split('.')[0]

    # Obteniendo las capas actuales en el mapa con el nombre del layer
    layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(name), df)
    # Si el layer ya existe en el mapa
    if len(layers):
        lyr = layers[0]
    else:
        # Si el layer no existe, crear un layer
        lyr = arcpy.mapping.Layer(layer)

    # Reemplaza el datasource
    # if replace:
    lyr.replaceDataSource(workspace, typeWorkspace, name_feature, False)
    
    # Si es necesario aplicar un filtro
    if query:
        lyr.definitionQuery = query

    lyr.visible = True
    
    # Se agrega si el layer no esta en el mapa
    if not len(layers):
        arcpy.mapping.AddLayer(df, lyr)
    
    if zoom:
        df.extent = lyr.getExtent()
        df.scale = df.scale *1.2
    
    if scale:
        df.scale = scale

    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()
    
def split_line_at_points(geometry_line, geometry_points):
    p_ini = geometry_line.firstPoint
    p_end = geometry_line.firstPoint

    parts = list()

    for i in geometry_points:
        if not i.within(geometry_line):
            continue
        if i.X ==  p_ini.X and i.Y == p_ini.Y:
            continue
        if i.X ==  p_end.X and i.Y == p_end.Y:
            continue
        m = geometry_line.measureOnLine(i)
        part_line = geometry_line.segmentAlongLine(0, m)
        parts.append(part_line)
    
    parts.append(geometry_line)
    
    parts.sort(key=lambda l: l.length)
    response = [b.symmetricDifference(a) for a, b in zip(parts, parts[1:])]
    response.insert(0, parts[0])
    return response



def generate_random_lines_by_area(polygon_path, feature_output, tolerance=0, n_lines=10):
    import random

    arcpy.env.overwriteOutput = 1

    outws = r"C:\path\to\your\filegeodatabase.gdb" # Where the output feature class and table will go. This assumes a file geodatabase
    # polygon = r"C:\path\to\your\filegeodatabase.gdb\utm15\polygon" # The study area polygon (Note this is in a feature dataset)
    # tolerance = 50 # The transect distance

    # Create random points offset from study area boundary by X distance
    # arcpy.Buffer_analysis(polygon_path, polygon_path, -tolerance) # negative buffer study area to get correct offset
    arcpy.CreateRandomPoints_management("in_memory", "rand_points", polygon_path, "", n_lines)

    # Add fields x, y, distance, and bearing
    arcpy.AddField_management("in_memory/rand_points", "x", "DOUBLE")
    arcpy.AddField_management("in_memory/rand_points", "y", "DOUBLE")
    arcpy.AddField_management("in_memory/rand_points", "distance", "FLOAT")
    arcpy.AddField_management("in_memory/rand_points", "bearing", "FLOAT")

    # Run cursor to update attribute table with pertinent data for bearing distance tool
    with arcpy.da.UpdateCursor("in_memory/rand_points", ["SHAPE@XY", "x", "y", "distance", "bearing"]) as cursor:
        for row in cursor:
            row[1] = row[0][0]
            row[2] = row[0][1]
            row[3] = tolerance
            row[4] = random.randint(1,360)
            cursor.updateRow(row)

    # Create a table to feed to Bearing Distance to line tool
    arcpy.TableToTable_conversion("in_memory/rand_points", arcpy.env.scratchGDB, "out_table")

    # Generate the transects
    arcpy.BearingDistanceToLine_management (os.path.join(arcpy.env.scratchGDB, "out_table"), feature_output, x_field = 'x', y_field = 'y', distance_field = 'distance', bearing_field = 'bearing', spatial_reference = "in_memory/rand_points")


def show_only_styled_values(featurelayer, layerfile, fieldname):
    
    """show_only_styled_values(featurelayer, layerfile, fieldname)
		Funcion que permite actualizar la simbologia de una capa y solo mostrar las clases que contienen valores
	INPUTS:
		featurelayer (Feature Layer) : nombre de layer presente en el mxd.
		layerfile (String): Ruta de archivo de tipo .lyr || O nombre de layer presente en el mxd.
		fieldname (String): Nombre de campo que se usa para la simbologia tipo texto	
	"""
    #Convertimos la ruta a un archivo tipo layer
    mxd = arcpy.mapping.MapDocument("current")
    flayer = arcpy.mapping.ListLayers(mxd,featurelayer)[0]
    lyr = arcpy.mapping.Layer(layerfile)

    # Creamos una lista de las classes existentes en el featurelayer
    classes_present = list(set([str(f[0]) for f in arcpy.da.SearchCursor(featurelayer, fieldname)]))

    sym_xml = lyr._arc_object.renderer
    xpars = xmltodict.parse(sym_xml)

    symbols = []
    values = xpars["Renderer"]["UniqueValueInfos"]["UniqueValueInfo"]

    for value in values:
        if value['Value'] in classes_present:
            symbols.append(value)

    xpars["Renderer"]["UniqueValueInfos"]["UniqueValueInfo"] = symbols
    sym_xml_final = xmltodict.unparse(xpars)

    flayer._arc_object.renderer = sym_xml_final
    arcpy.RefreshActiveView()


# polygon_path = r'Database Connections\arc_danielgis@data_gis.sde\arc_danielgis.data_gis.DS_BASE_CATASTRAL_GEOWGS84\arc_danielgis.data_gis.GPO_DEP_DEPARTAMENTO'
# feature_output = r'C:\daniel\proyectos\ingemmet\OSXXXX2021_2\neotectonica\gdb\data.gdb\random_lines'
# generate_random_lines_by_area(polygon_path, feature_output, tolerance=100000, n_lines=5)