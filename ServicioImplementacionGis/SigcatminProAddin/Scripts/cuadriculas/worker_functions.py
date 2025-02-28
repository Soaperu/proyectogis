import arcpy
import os
from datetime import datetime, timedelta
import uuid

EXTENTION_SHP = ".shp"
EXTENTION_LOG = ".log"
EXTENTION_PNG = ".png"
TEMP_DIR = r"C:\bdgeocatmin\Temporal"
# Clases / funciones supuestas
class Suplies:
    def __init__(self):
        self.quads = "cuadriculas"
        self.sea = "mar"
        self.region = "region"
        self.regions = "regiones"

class regiones:
    def __init__(self):
        # nm_depa podría ser un objeto con un 'name' que devuelva el nombre del campo
        self.nm_depa = type("NmDepa", (object,), {"name": "NM_DEPA"})()

# def get_path_tmp():
#     return arcpy.GetParameterAsText(0) # Insumos 

# def get_path_output():
#     return arcpy.GetParameterAsText(1)#r"C:\salida"


# Mensajes (placeholder) 
class msg:
    TITLE_AXIS_X_GRAPH = "Tiempo"
    TITLE_AXIS_Y_GRAPH = "Procesos"
    TITLE_GRAPH = "Duración del procesamiento"


def clear_selection(feature_layer):
    """Encapsula la limpieza de la selección en ArcPy."""
    arcpy.SelectLayerByAttribute_management(feature_layer, "CLEAR_SELECTION")

def get_cuadriculas_regionales(folder_path, get_path_output):
    """
    Permite determinar las cuadriculas regionales a un 90% (o 50% en el threshold)
    dentro de la región, considerando su intersección con el mar y con otras regiones.
    :param folder_path: Ruta a la carpeta con los shapefiles
    :return: Un diccionario con información de tiempo de inicio/fin y etiqueta (para gráficas).
    """
    arcpy.env.overwriteOutput = True
    start = datetime.now()

    response = dict()
    response['label'] = os.path.basename(folder_path)
    response['start'] = str(start)  # En Python 3, str() es suficiente

    arcpy.AddMessage(f"Procesando carpeta: {response['label']}")

    # Parámetros configurables
    _ADMINISTRATION_PERCENTAGE = 0.5     # Umbral para área de intersección
    _SEARCH_DISTANCE = '10 METERS'       # Distancia de búsqueda
    _INTERCECT_DISTANCE = 4             # Tolerancia en la operación de intersección (4 m)
    _FID_FIELD = 'FID'                  # Campo FID u OID
    _foo = Suplies()
    _region = regiones()

    # Suponemos que el nombre del folder padre indica el "datum"
    _DATUM = os.path.basename(os.path.dirname(folder_path))
    _OUTPUT_DIR = get_path_output
    # Nombre de shapefile de salida
    _OUTPUT_SHP = os.path.join(_OUTPUT_DIR, _DATUM, os.path.basename(folder_path) + EXTENTION_SHP)

    # Si deseas regenerar la salida siempre, se podría borrar:
    # if arcpy.Exists(_OUTPUT_SHP):
    #     arcpy.Delete_management(_OUTPUT_SHP)

    # Rutas de shapefiles
    cuadriculas_path = os.path.join(folder_path, _foo.quads + EXTENTION_SHP)
    marperuano_path = os.path.join(folder_path, _foo.sea + EXTENTION_SHP) # capa no existe se podria resolver aplicando un select by location y exportando la capa * Jcruz
    region_path = os.path.join(folder_path, _foo.region + EXTENTION_SHP)
    cregiones_path = os.path.join(folder_path, _foo.regions + EXTENTION_SHP)

    # Crear FeatureLayers
    # En Python 3, uuid4().hex reemplaza uuid4().get_hex()
    cuadriculas_mfl = arcpy.MakeFeatureLayer_management(cuadriculas_path, uuid.uuid4().hex)
    region_mfl = arcpy.MakeFeatureLayer_management(region_path, uuid.uuid4().hex)
    regiones_mfl = arcpy.MakeFeatureLayer_management(cregiones_path, uuid.uuid4().hex)

    # Obtener la geometría de la región
    region_geom = [row[0] for row in arcpy.da.SearchCursor(region_mfl, ['SHAPE@'])][0]
    # Obtener el nombre de la región
    region_name = [row[0] for row in arcpy.da.SearchCursor(region_mfl, [_region.nm_depa.name])][0]

    # Determinar cuadrículas que intersectan con el mar
    arcpy.SelectLayerByLocation_management(cuadriculas_mfl, 'INTERSECT', marperuano_path,
                                           None, 'NEW_SELECTION')
    cuadriculas_mar = [row[0] for row in arcpy.da.SearchCursor(cuadriculas_mfl, ['OID@'])]
    clear_selection(cuadriculas_mfl)

    # Cuadrículas que son cruzadas por el contorno de la región
    arcpy.SelectLayerByLocation_management(cuadriculas_mfl, 'CROSSED_BY_THE_OUTLINE_OF',
                                           region_mfl, None, 'NEW_SELECTION')

    cuadriculas_externas = []
    arcpy.AddMessage(f"Procesando: {_OUTPUT_SHP}")
    for shape_obj, oid in arcpy.da.SearchCursor(cuadriculas_mfl, ['SHAPE@', 'OID@']):
        # Evitar las que ya intersectan con el mar
        if oid not in cuadriculas_mar:
            intersection = region_geom.intersect(shape_obj, _INTERCECT_DISTANCE)
            # Verificamos si el área de intersección es menor o igual a 50% del área total
            if intersection.area <= shape_obj.area * _ADMINISTRATION_PERCENTAGE:
                # Revisar cuántas regiones intersectan dicha cuadrícula
                arcpy.SelectLayerByLocation_management(regiones_mfl, 'INTERSECT',
                                                       shape_obj, _SEARCH_DISTANCE,
                                                       'NEW_SELECTION')
                count_regiones = int(arcpy.GetCount_management(regiones_mfl).getOutput(0))

                if count_regiones > 2:
                    # Hallar región con mayor área de intersección
                    max_area = 0
                    region_mayor_area = ""
                    for r_shape, r_name in arcpy.da.SearchCursor(regiones_mfl, ['SHAPE@', _region.nm_depa.name]):
                        area_inter = r_shape.intersect(shape_obj, _INTERCECT_DISTANCE).area
                        if area_inter > max_area:
                            max_area = area_inter
                            region_mayor_area = r_name
                    # Si la región con mayor intersección no es la principal, la marcamos como externa
                    if region_mayor_area != region_name:
                        cuadriculas_externas.append(str(oid))
                else:
                    # Si intersecta con 2 o menos regiones, se marca también
                    cuadriculas_externas.append(str(oid))

    clear_selection(regiones_mfl)

    # Eliminar del shapefile original las cuadrículas externas
    if len(cuadriculas_externas) > 0:
        desc = arcpy.Describe(cuadriculas_path)
        oid_field = desc.OIDFieldName
        query = f"{oid_field} IN ({', '.join(cuadriculas_externas)})" # _FID_FIELD
        with arcpy.da.UpdateCursor(cuadriculas_path, ['OID@'], query) as cursor:
            for _ in cursor:
                cursor.deleteRow()
        clear_selection(cuadriculas_mfl)

    # Copiamos el resultado a la carpeta de salida
    arcpy.CopyFeatures_management(cuadriculas_path, _OUTPUT_SHP)

    end_time = datetime.now()
    response['end'] = str(end_time)
    delta = end_time - start
    response['time'] = str(timedelta(seconds=delta.total_seconds()))

    return response