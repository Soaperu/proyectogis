import sys
reload(sys)
sys.setdefaultencoding("utf-8")
# Generacion del Perfil Geologico a partir de la linea de seccion
# puntos de observacion geologica, litologia y Modelo de Elevacion 
# Digital (DEM). Esta herramienta solo es funcional dentro de una
# sesion de Arcmap Forma parte del conjunto de modulos y procesos 
# automatizados de Automapic

# Importacion de librerias
from osgeo import gdal
import arcpy
import os
import json
import math
import pandas as pd
import numpy as np
import uuid
import settings_aut as st
import automapic_template_json as auttmp
import automapic as aut
import messages_aut as msg
import MG_S00_model as model
import traceback

# Habilitando la sobreescritura
arcpy.env.overwriteOutput = True

# Definiendo parametros
raster_dem = arcpy.GetParameterAsText(0)
linestring_wkt = arcpy.GetParameterAsText(1)
codhoja = arcpy.GetParameterAsText(2)
zona = arcpy.GetParameterAsText(3)
tolerancia = arcpy.GetParameterAsText(4) # aqui tambien llega la Letra de inicio y fin de perfil
x_Ini = float(arcpy.GetParameterAsText(5))
y_Ini = float(arcpy.GetParameterAsText(6))
h_ini = int(arcpy.GetParameterAsText(7))
_MARGEN_INFERIOR = 8000

# Objeto a retornar
response = dict()
response['status'] = 1
response['message'] = 'success'

# EPSG
wkid = int('327{}'.format(zona))

# Puntos cardinales ordenados por cuadrante
puntos_cardinales = [['SO', 'NE'], ['SE', 'NO'], ['NE', 'SO'], ['NO', 'SE']]


def get_angle_by_azimut(azimut):
    """
    obtener el angulo de pendiente en radianes a partir del azimut
    :param azimut: Azimut en grados decimales
    :return: angulo real en sentido antihorario
    """
    quad = math.ceil(azimut / 90.0)
    # Regla para obtener el angulo a partir del azimut
    ang_s = 90 * (quad + int(not quad % 2)) - azimut + (180 if quad in (2, 3) else 0)
    ang_r = ang_s * math.pi / 180
    return ang_r

def get_angle_by_coords(x_ini, y_ini, x_fin, y_fin):
    """
    obtener el angulo de pendiente en radianes de una linea a partir de sus coordenadas
    :param x_ini: coordenada x inicial
    :param y_ini: coordenada y inicial
    :param x_fin: coordenada x final
    :param x_fin: coordenada y final
    :return: angulo real en sentido antihorario
    """
    # El math.atan2()método devuelve el arco tangente de y / x, en radianes. Donde x e y son las
    # coordenadas de un punto (x, y). ver https://www.w3schools.com/python/ref_math_atan2.asp
    ang_r = math.atan2(y_fin - y_ini, x_fin - x_ini)
    return ang_r

def getanglebtwnlines(m1, m2):
    """
    obtener angulo sexagesimal entre rectas a partir de sus pendientes
    :param m1: pendiente 1
    :param m2: pendiente 2
    :return: retorna el menor ángulo sexagesimal, que se forma en la intersección de las rectas.
    """
    # propiedad entre dos rectas que intersectan
    # https://www.basic-mathematics.com/angle-between-two-lines.html
    tan_angulo = abs((m2 - m1) / (1 + (m2 * m1)))
    return math.degrees(math.atan(tan_angulo))

def dataframe_to_feature(dataframe, output, geometry_column, src=None):
    """
    Transforma un objeto pandas dataframe en un feature
    :param dataframe: pandas dataframe
    :param output: Ubicacion de almacenamiento del feature
    :param geometry_column: lista de columnas de geometria X e Y 
    :return: Objeto feature
    """
    arr = dataframe.to_records(index=False)
    dt = arr.dtype
    desc = dt.descr
    # Es necesario reemplazar las columnas de tipo Object a unicode para ser legibles por numpy
    for i in range(len(desc)):
        if desc[i][1] == '|O':
            desc[i] = (desc[i][0], '<U300')

    dt = np.dtype(desc)
    arr = arr.astype(dt)
    feature = arcpy.da.NumPyArrayToFeatureClass(arr, output, geometry_column)
    return feature

def buzamiento_aparente(buzamiento_real, ang_seccion_azimut, df_buzamiento_aparente):
    """
    Obtiene el buzamiento corregido segun la matriz de buzamientos
    :param buzamiento_real: valor del buzamiento (sexagesimal) de pog
    :param ang_seccion_azimut: angulo en sexagesimales de interseccion entre el azimut y la linea de seccion
    :param df_buzamiento_aparente: pandas dataframe de la matriz de buzamiento aparente
    :return: buzamiento aparente (corregido)
    """
    df = df_buzamiento_aparente
    bz_real_list = df[st._BZ_REAL_FIELD].unique()
    bz_real_sel = min(bz_real_list, key=lambda x: abs(x - buzamiento_real))
    ang_sec_azm_list = df[df[st._BZ_REAL_FIELD] == bz_real_sel][st._A_BZ_SECC_FIELD].unique()
    ang_sec_azm_sel = min(ang_sec_azm_list, key=lambda x: abs(x - ang_seccion_azimut))
    response = df.loc[(df[st._BZ_REAL_FIELD] == bz_real_sel) & (df[st._A_BZ_SECC_FIELD] == ang_sec_azm_sel)][st._BZ_APAR_DD_FIELD]
    response = response.tolist()
    return response[0]

def dotVectorSeccPogByAngle(ang_sec, ang_pog):
    """
    Obtiene el producto punto a partir del angulo de pendientes de dos rectas y sus vectores unitarios
    :param ang_sec: angulo de la pendiente de la seccion en radianes
    :param ang_pog: angulo de la pendiente del POG en radianes
    :return: producto punto
    """
    # Obteniendo el vector unitario para la pendiente de la seccion
    A = np.array([math.sin(ang_sec), math.cos(ang_sec)])
    # Obteniendo el vector unitario para la pendiente de pog
    B = np.array([math.sin(ang_pog), math.cos(ang_pog)])
    # Producto punto de vectores
    AXB = np.dot(A, B)
    return AXB

def remove_duplicated(list_geometry):
    """
    Obtiene las geometrias unicas a partir de una lista de geometrias
    Surge a partir de no tener inconvenientes con niveles de licencia en arcmap
    :param list_geometry: lista de geometrias
    :return: lista de geometria unicas
    """
    list_geometry_unique = list()
    for idx, geometry in enumerate(list_geometry):
        if idx == 0:
            list_geometry_unique.append(geometry)
            continue
        controller = False
        for geometry_unique in list_geometry_unique:
            # Compara si las geometrias son equivalentes
            if geometry.equals(geometry_unique):
                controller = True
                break

        if not controller:
            list_geometry_unique.append(geometry)
    return list_geometry_unique

def proyeccionPOG(az,distance,point):
    """
    Obtiene las proyecciones tomando como referencia el Azimut
    de los POG como segmentos que inteceptaran a la linea de perfil
    :param az: dato de azitmut
    :param distance: distancia en metros a los que se proyectara el POG
    :param point: geometria del POG (punto)
    :return: lista de geometria unicas
    """
    # Convertir el azimut a radianes
    azimuth_radians = math.radians(az)
    half_distance_meters = distance * 2
    #puntos x ,y
    mid_x= point.centroid.X
    mid_y= point.centroid.Y
    # Calcular las coordenadas del punto de inicio
    start_x = mid_x - (half_distance_meters * math.sin(azimuth_radians))
    start_y = mid_y - (half_distance_meters * math.cos(azimuth_radians))
    # Calcular las coordenadas del punto final
    end_x = mid_x + (half_distance_meters * math.sin(azimuth_radians))
    end_y = mid_y + (half_distance_meters * math.cos(azimuth_radians))
    # Crear objetos Point para el punto medio, punto de inicio y punto final
    mid_point = arcpy.Point(mid_x, mid_y)
    start_point = arcpy.Point(start_x, start_y)
    end_point = arcpy.Point(end_x, end_y)
    # Crear un objeto Polyline a partir de los puntos de inicio y final
    segment = arcpy.Polyline(arcpy.Array([start_point, end_point]))
    return segment

# Procesamiento
try:
    # Documento de mapa actual
    mxd = arcpy.mapping.MapDocument('CURRENT')

    # Obteniendo el sistema de referencia (src) del modelo de elevacion digital (DEM)
    raster_dem_src = arcpy.Describe(raster_dem).spatialReference

    if raster_dem_src.factoryCode not in (32717, 32718, 32719):
        raise RuntimeError(msg._ERROR_WKID_DEM_PROFILE_GEOLOGY)

    # Ubicacion de features inputs
    # pog_path = st._POG_MG_PATH.format(zona, zona)
    pog_path = model.gpt_dgr_pog(str(zona)).path
    pog_seccion_path = st._POG_MG_PERFIL_PATH
    gpl_seccion_path = st._GPL_MG_PERFIL_PATH
    # ulito_path = st._ULITO_MG_PATH.format(zona, zona)
    ulito_path = model.gpo_dgr_ulito(str(zona)).path
    cuadriculas_path = st._CUADRICULAS_MG_PATH

    # Conversion de la linea de seccion de wkt a geometry
    linestring_geom = arcpy.FromWKT(linestring_wkt, mxd.activeDataFrame.spatialReference)

    # Filtro a nivel de codigo de hoja, reutilizable
    query = "{} IN ('{}')".format(st._CODHOJA_FIELD, "','".join(codhoja.split(",")))

    cuadricula = arcpy.da.SearchCursor(cuadriculas_path, ['shape@'], query, raster_dem_src)
    cuadricula_geom = map(lambda i: i[0], cuadricula)[0]

    # Transformacion del featureTable de buzamiento aparente a objeto pandas data frame
    # buzamiento_aparente_path = st._TB_MG_BUZAMIENTO_APARENTE_PATH
    np_buzamiento_aparente = arcpy.da.TableToNumPyArray(st._TB_MG_BUZAMIENTO_APARENTE_PATH, ["*"])
    df_buzamiento_aparente = pd.DataFrame(np_buzamiento_aparente)

    # xmin y ymin de la hoja
    xmin_cuad, ymin_cuad = cuadricula_geom.extent.XMin, cuadricula_geom.extent.YMin

    # Coordenada inicial para graficacion del perfil
    y_ini = y_Ini #ymin_cuad - _MARGEN_INFERIOR

    # Punto de altura máxima que puede tomar el perfil
    y_top = 0


    # :PROCESO DE CAPTURA DE VALORES Z DEL DEM

    # Si el DEM y el dataframe actual no tienen el mismo SRC, es necesario reproyectar 
    # la linea de seccion
    if raster_dem_src.factoryCode != mxd.activeDataFrame.spatialReference.factoryCode:
        linestring_geom = linestring_geom.projectAs(raster_dem_src)

    # Accediento a los datos del DEM
    raster = gdal.Open(raster_dem)
    gt = raster.GetGeoTransform()               # necesario para obtencion de valores z

    width_resolution = gt[1]                    # resolucion horizontal
    heigth_resolution = gt[-1]                  # resolucion vertical

    coords = list()

    # Se obtienen todas las divisiones de longitud igual a la resolucion horizontal
    #  a los largo de la longitud de la linea de seccion
    iterable = range(0, int(linestring_geom.length), int(width_resolution))
    iterable.append(linestring_geom.length)

    first_point = linestring_geom.firstPoint    # punto inicial de la linea de seccion
    end_point = linestring_geom.lastPoint       # punto final de la linea de seccion

    for i, r in enumerate(iterable, 1):
        # se obtiene el punto a una distancia width_resolution del punto inicial de la linea de seccion
        pnt = linestring_geom.positionAlongLine(r, False)
        x, y = pnt.centroid.X, pnt.centroid.Y

        raster_x = int((x - gt[0]) / gt[1])     # coordenada X equivalentes en el DEM
        raster_y = int((y - gt[3]) / gt[5])     # coordenada Y equivalentes en el DEM

        # Se obtiene el valor de z a partir de las coordenadas equivalentes
        z_arr = raster.GetRasterBand(1).ReadAsArray(raster_x, raster_y, 1, 1)
        z = z_arr[0][0] + y_ini - h_ini

        # Se almacena el valor z en una lista
        #coords.append('{} {}'.format(first_point.X + r, z))
        coords.append('{} {}'.format(x_Ini + r, z))

        # Se captura el valor con mayor altura
        if z_arr[0][0] - h_ini > y_top:
            y_top = z_arr[0][0] - h_ini


    # :PROCESO DE PROYECCION DE POG SOBRE EL PERFIL
    nombres_geograficos = [st._IGN_TOP_CERRO, st._IGN_TOP_CORDILLERA, st._IGN_TOP_LOMA, st._IGN_TOP_NEVADO, st._IGN_HID_RIO_QUEBRADA]
    temp_perfil = arcpy.CreateFeatureclass_management("in_memory", "temp_perfil", "POLYLINE", "", "DISABLED", "DISABLED", wkid, "", "0", "0", "0")
    insertcursor = arcpy.da.InsertCursor(temp_perfil,['SHAPE@'])
    insertcursor.insertRow([linestring_geom])
    del insertcursor

    pog_layer = arcpy.MakeFeatureLayer_management(pog_path, os.path.basename(pog_path + '_mfl'))
    # Se seleccionan nen todos los POG a una distancia de tolerancia
    arcpy.SelectLayerByLocation_management(pog_layer,"INTERSECT", linestring_geom, "{} METERS".format(tolerancia.split(";")[0]), "NEW_SELECTION")

    # Se seleccionan solo los POG clasificados con CODI 201 y 204
    query2 = '({}) AND ({} IN (201, 204))'.format(query, st._CODI_FIELD)
    cursor = arcpy.da.SearchCursor(pog_layer, ["OID@", st._AZIMUT_FIELD, 'SHAPE@', st._BUZAMIENTO_FIELD, st._CODI_FIELD], query2)
    cursor = map(lambda i: i, cursor)

    # Pendiente de la linea general, solo sera utilizado si no existen POGs que se intersecten
    linestring_geom_angle = get_angle_by_coords(first_point.X, first_point.Y, end_point.X, end_point.Y)
    linestring_geom_angle_s = linestring_geom_angle*180/math.pi                 # angulo en sexagesimales
    linestring_geom_angle_q = int(math.ceil(linestring_geom_angle_s / 90.0))    # cuadrante al que pertenece el angulo
    linestring_geom_m = math.tan(linestring_geom_angle)                         # pendiente del angulo
    #########################

    rows = list()

    pog_proyecciones= arcpy.CreateFeatureclass_management("in_memory", "pog_proyecciones", "POLYLINE", "{}".format(pog_path), "DISABLED", "DISABLED", wkid, "", "0", "0", "0")
    fields= ["OID@", st._AZIMUT_FIELD, 'SHAPE@', st._BUZAMIENTO_FIELD, st._CODI_FIELD]
    #with arcpy.da.SearchCursor(cursor, fields) as cursor:
    with arcpy.da.InsertCursor(pog_proyecciones, fields+['POG']) as insertcursor:
        for i in cursor:
            if i[1] is not None:
                seg=proyeccionPOG(i[1],linestring_geom.length,i[2])
                insertcursor.insertRow([i[0],i[1],seg,i[3],i[4], i[0]])

    intersect_P= arcpy.Intersect_analysis("{} #;{} #".format(pog_proyecciones, temp_perfil), "in_memory\Intersect_proy_pog", "ALL", "", "POINT")
    intersect_P= arcpy.management.MultipartToSinglepart(intersect_P,"in_memory\Intersect_proy_pog_explode")
    proy_cursor= arcpy.da.SearchCursor(intersect_P,fields+['POG'])

    for pog in proy_cursor:
        if pog[1] is none:#if not pog[1]:
            continue
        pog_angle = get_angle_by_azimut(pog[1])                                 # angulo de pendiente del azimut del pog en radianes
        pog_m, pnt = math.tan(pog_angle), pog[2]                                # pendiente y coordenada del pog respectivamente
        
        # Obteniendo las coordenadas de la interseccion entre la linea de seccion y la proyeccion del pog
        # https://www.math-only-math.com/point-of-intersection-of-two-lines.html
        x = pog[2].centroid.X#((linestring_geom_m * end_point.X) - (pog_m * pnt.centroid.X) - end_point.Y + pnt.centroid.Y) / (linestring_geom_m - pog_m)
        y = pog[2].centroid.Y#linestring_geom_m * (x - end_point.X) + end_point.Y

        point = arcpy.Point()
        point.X = x
        point.Y = y
        bufferx= pog[2].buffer(100)
        if linestring_geom.intersect(bufferx, 2):
            segmento=linestring_geom.clip(bufferx.extent)
        # Si el punto no esta en la extension de la linea de seccion
        # if not linestring_geom.extent.contains(point):
        #     continue
        first_point_seg = segmento.firstPoint    # punto inicial de la linea de seccion
        end_point_seg = segmento.lastPoint 
        linestring_geom_angle = get_angle_by_coords(first_point_seg.X, first_point_seg.Y, end_point_seg.X, end_point_seg.Y)
        linestring_geom_angle_s = linestring_geom_angle*180/math.pi                 # angulo en sexagesimales
        linestring_geom_angle_q = int(math.ceil(linestring_geom_angle_s / 90.0))    # cuadrante al que pertenece el angulo
        linestring_geom_m = math.tan(linestring_geom_angle)                         # pendiente del angulo
        # Angulo entre la seccion y el la proyeccion de azimut del POG
        ang_secc = getanglebtwnlines(linestring_geom_m, pog_m)

        # Distancia entre el punto de interseccion y vertice inicial de la linea de seccion
        distance = linestring_geom.measureOnLine(point)

        raster_x = int((x - gt[0]) / gt[1])     # coordenada X equivalentes en el DEM
        raster_y = int((y - gt[3]) / gt[5])     # coordenada Y equivalentes en el DEM

        
        z_arr = raster.GetRasterBand(1).ReadAsArray(raster_x, raster_y, 1, 1)

        if not z_arr:
            continue
        if pog[3] is None:
            continue
        z = z_arr[0][0] + y_ini - h_ini
        #coords.append('{} {}'.format(first_point.X + distance, z))
        coords.append('{} {}'.format(x_Ini + distance, z))
        dot_seccion_pog = dotVectorSeccPogByAngle(linestring_geom_angle, pog_angle)
        buz_aparente = buzamiento_aparente(pog[3], ang_secc, df_buzamiento_aparente)
        p_secc = 'derecha' if dot_seccion_pog > 0 else 'izquierda'
        bz_secc = buz_aparente if dot_seccion_pog > 0 else 180 - buz_aparente

        rows.append({
            # 'x': first_point.X + distance,
            'x': x_Ini + distance,
            'y': z,
            st._CODI_FIELD: 1801,
            st._POG_FIELD: pog[5],
            st._BZ_REAL_FIELD: pog[3], 
            st._A_BZ_SECC_FIELD: ang_secc,
            st._BZ_APAR_FIELD: buz_aparente,
            st._BZ_SEC_FIELD: bz_secc,
            st._P_SECC_FIELD: p_secc,
            st._CODHOJA_FIELD: codhoja.split(",")[0]
        })
        if z_arr[0][0] - h_ini > y_top:
            y_top = z_arr[0][0] - h_ini


    tmp_name = 'pog_proyectados_{}'.format(uuid.uuid4().get_hex())
    pog_seccion_mfl = arcpy.MakeFeatureLayer_management(pog_seccion_path, tmp_name, query)
    arcpy.DeleteRows_management(pog_seccion_mfl)

    if len(cursor):
        df = pd.DataFrame(rows)
        shp_output = os.path.join(st._TEMP_FOLDER, tmp_name + '.shp')
        dataframe_to_feature(df, shp_output, ['x', 'y'], mxd.activeDataFrame.spatialReference)
        arcpy.Append_management(shp_output, pog_seccion_path, "NO_TEST")

    # :PROCESO DE PROYECCION DE NOMBRES GEOGRAFICOS
    for ng in nombres_geograficos:
        ng1 = arcpy.MakeFeatureLayer_management(ng,'ng1_{}'.format(uuid.uuid4().get_hex()))
        intersect= arcpy.Intersect_analysis("{} #;{} #".format(temp_perfil, ng1), "in_memory\Intersect_ng", "ALL", "", "POINT")
        intersect= arcpy.management.MultipartToSinglepart(intersect,"in_memory\Intersect_ng_explode")
        points_intersect= arcpy.da.SearchCursor(intersect,[st._RAS_PRINCI, st._NOMBRE, 'SHAPE@'])
        insertcursor2= arcpy.da.InsertCursor(pog_seccion_path,['SHAPE@XY', st._ETIQUETA_FIELD, st._CODHOJA_FIELD, st._CODI_FIELD])
        for point_intersect in points_intersect:
            r = linestring_geom.measureOnLine(point_intersect[2].firstPoint)
            x,y = str(point_intersect[2].firstPoint).split(" ")[:2]
            raster_x = int((float(x) - gt[0]) / gt[1])
            raster_y = int((float(y) - gt[3]) / gt[5])
            z_arr = raster.GetRasterBand(1).ReadAsArray(raster_x, raster_y, 1, 1)
            z = z_arr[0][0] + y_ini - h_ini
            if  len(point_intersect[1]) <= 1:
                etiqueta = point_intersect[0].lower().title()
            else: etiqueta = point_intersect[1].lower().title()
            insertcursor2.insertRow([(x_Ini + r, z), etiqueta, codhoja.split(",")[0], 0])
        del insertcursor2

    # :PUNTOS DE INTERSECCION ENTRE LA LINEA DE PERFIL Y UNIDADES LITOLOGICAS
    ulito_mfl = arcpy.MakeFeatureLayer_management(ulito_path, 'ulito_{}'.format(codhoja.split(",")[0]), query)
    arcpy.SelectLayerByLocation_management(ulito_mfl, "INTERSECT", linestring_geom, None, "NEW_SELECTION")

    puntos_contacto = list()
    puntos_unidades = list()
    ulito_cursor = arcpy.da.SearchCursor(ulito_mfl, ['SHAPE@', 'ETIQUETA'])
    for ulito in ulito_cursor:
        points = linestring_geom.intersect(ulito[0], 1)
        for point in points:
            r = linestring_geom.measureOnLine(point)
            x = point.X
            y = point.Y
            raster_x = int((x - gt[0]) / gt[1])
            raster_y = int((y - gt[3]) / gt[5])

            z_arr = raster.GetRasterBand(1).ReadAsArray(raster_x, raster_y, 1, 1)
            z = z_arr[0][0] + y_ini - h_ini
            coords.append('{} {}'.format(x_Ini + r, z)) #first_point.X <> x_Ini
            punto_contacto = arcpy.Point()
            punto_contacto.X = x_Ini + r #first_point.X <> x_Ini
            punto_contacto.Y = z
            puntos_contacto.append(punto_contacto)

            if z_arr[0][0] - h_ini > y_top:
                y_top = z_arr[0][0] - h_ini
        lineas = linestring_geom.intersect(ulito[0], 2)
        for lineaArr in lineas:
            linea = arcpy.Polyline(lineaArr)
            r = linestring_geom.measureOnLine(linea.centroid)
            x = linea.centroid.X
            y = linea.centroid.Y
            raster_x = int((x - gt[0]) / gt[1])
            raster_y = int((y - gt[3]) / gt[5])
            z_arr = raster.GetRasterBand(1).ReadAsArray(raster_x, raster_y, 1, 1)
            z = z_arr[0][0] + y_ini - h_ini
            coords.append('{} {}'.format(x_Ini + r, z)) #first_point.X <> x_Ini
            puntos_unidad = arcpy.Point()
            puntos_unidad.X = x_Ini + r #first_point.X <> x_Ini
            puntos_unidad.Y = z
            puntos_unidades.append([puntos_unidad, ulito[1]])

            if z_arr[0][0] - h_ini > y_top:
                y_top = z_arr[0][0] - h_ini


    coords.sort(key=lambda i: i.split(' ')[0])

    y_top = y_top * 1.2 + y_ini

    sec_lines_string = ','.join(coords)
    wkt = "LINESTRING ({})".format(sec_lines_string)

    geometry_line = arcpy.FromWKT(wkt)

    puntos_contacto_unique = remove_duplicated(puntos_contacto)
    geometry_line_split = aut.split_line_at_points(geometry_line, puntos_contacto_unique)


    auttmp._PL_PERFIL_TEMPLATE['features'] = []

    # Perfil dividido por unidades litologicas
    for line in geometry_line_split:
        line_json = json.loads(line.JSON)
        line_json['paths']
        contador = 0
        controler = True
        for punto_unidad in puntos_unidades:
            within = punto_unidad[0].within(line)
            if within:
                data = {
                    "attributes": {
                        "CODI": 1303,
                        "HOJA": codhoja.split(",")[0][:3],
                        "CUADRANTE": codhoja.split(",")[0][-1],
                        "CODHOJA": codhoja.split(",")[0],
                        "DESCRIP": '1303',
                        "ETIQUETA": punto_unidad[1]
                    }, 
                    "geometry": {
                        "paths": line_json['paths']
                    }
                }
                auttmp._PL_PERFIL_TEMPLATE['features'].append(data)
                break

    # Linea horizontal
    linea_seccion = {
                    "attributes": {
                        "CODI": 1304,
                        "HOJA": codhoja.split(",")[0][:3],
                        "CUADRANTE": codhoja.split(",")[0][-1],
                        "CODHOJA": codhoja.split(",")[0],
                        "DESCRIP": '1304',
                        "ETIQUETA": ''
                    }, 
                    "geometry": {
                        "paths": [[[x_Ini, y_ini], [x_Ini + linestring_geom.length, y_ini]]] #first_point.X <> x_Ini
                    }
                }
    auttmp._PL_PERFIL_TEMPLATE['features'].append(linea_seccion)

    # Linea de altitud A
    linea_altura1 = {
                    "attributes": {
                        "CODI": 1305,
                        "HOJA": codhoja.split(",")[0][:3],
                        "CUADRANTE": codhoja.split(",")[0][-1],
                        "CODHOJA": codhoja.split(",")[0],
                        "DESCRIP": '1305',
                        "ETIQUETA": "{} {}".format(puntos_cardinales[linestring_geom_angle_q][0],tolerancia.split(";")[1])
                    }, 
                    "geometry": {
                        "paths": [[[x_Ini, y_top], [x_Ini, y_ini]]] #first_point.X <> x_Ini
                    }
                }
    auttmp._PL_PERFIL_TEMPLATE['features'].append(linea_altura1)

    # Linea de altitud A'
    linea_altura2 = {
                    "attributes": {
                        "CODI": 1305,
                        "HOJA": codhoja.split(",")[0][:3],
                        "CUADRANTE": codhoja.split(",")[0][-1],
                        "CODHOJA": codhoja.split(",")[0],
                        "DESCRIP": 1305,
                        "ETIQUETA": "{} {}'".format(puntos_cardinales[linestring_geom_angle_q][1],tolerancia.split(";")[1])
                    }, 
                    "geometry": {
                        "paths": [[[x_Ini + linestring_geom.length, y_top], [x_Ini + linestring_geom.length, y_ini]]] #first_point.X <> x_Ini
                    }
                }
    auttmp._PL_PERFIL_TEMPLATE['features'].append(linea_altura2)

    # Altura total del perfil
    htot = int(math.ceil(y_top - y_ini))


    for i in  range(h_ini, htot):
        if i % 500 != 0:
            continue
        y_mark = y_ini + abs(h_ini - i)
        if y_mark > y_top:
            continue
        xA_ini = x_Ini #first_point.X <> x_Ini
        xA_end = xA_ini - 100
        xB_ini = x_Ini + linestring_geom.length #first_point.X <> x_Ini
        xB_end = xB_ini + 100
        
        # Marcas de altitud en linea de altitud A
        hmark_line = {
                    "attributes": {
                        "CODI": 1315,
                        "HOJA": codhoja.split(",")[0][:3],
                        "CUADRANTE": codhoja.split(",")[0][-1],
                        "CODHOJA": codhoja.split(",")[0],
                        "DESCRIP": 1315,
                        "ETIQUETA": str(i)
                    }, 
                    "geometry": {
                        "paths": [[[xA_ini, y_mark], [xA_end, y_mark]]]
                    }
                }
        auttmp._PL_PERFIL_TEMPLATE['features'].append(hmark_line)

        # Marcas de altitud en linea de altitud A'
        hmark_line2 = {
                "attributes": {
                    "CODI": 1315,
                    "HOJA": codhoja.split(",")[0][:3],
                    "CUADRANTE": codhoja.split(",")[0][-1],
                    "CODHOJA": codhoja.split(",")[0],
                    "DESCRIP": 1315,
                    "ETIQUETA": str(i)
                }, 
                "geometry": {
                    "paths": [[[xB_ini, y_mark], [xB_end, y_mark]]]
                }
            }
        auttmp._PL_PERFIL_TEMPLATE['features'].append(hmark_line2)

    # Cargando las linea de perfil a la base de datos
    rows_seccion = arcpy.AsShape(auttmp._PL_PERFIL_TEMPLATE, True)
    gpl_seccion_mfl = arcpy.MakeFeatureLayer_management(gpl_seccion_path, 'gpl_seccion_path_{}'.format(codhoja.split(",")[0]), query)
    arcpy.DeleteRows_management(gpl_seccion_mfl)
    arcpy.Append_management(rows_seccion, gpl_seccion_path, "NO_TEST")

    # Agregando capa de pog al mapa
    name_pog = os.path.basename(pog_seccion_path)
    lyer_pog = os.path.join(st._BASE_DIR, 'layers/{}.lyr'.format(name_pog))
    aut.add_layer_with_new_datasource(lyer_pog, name_pog, st._GDB_PATH_MG, "FILEGDB_WORKSPACE", df_name ="PERFIL GEOLOGICO", query=query)

    # Agregando capa de seccion al mapa
    name_gpl = os.path.basename(gpl_seccion_path)
    lyer_gpl = os.path.join(st._BASE_DIR, 'layers/{}.lyr'.format(name_gpl))
    aut.add_layer_with_new_datasource(lyer_gpl, name_gpl, st._GDB_PATH_MG, "FILEGDB_WORKSPACE", df_name ="PERFIL GEOLOGICO", query=query, zoom=True)

    # Etiquetas de Marcas de altitud - show
    mxd2 = arcpy.mapping.MapDocument("CURRENT")
    dfs = arcpy.mapping.ListDataFrames(mxd2)[0]
    lyrs = arcpy.mapping.ListLayers(mxd2, "{}*".format(name_gpl))
    layer= lyrs[0]
    cursor= [int(x[0]) for x in arcpy.da.SearchCursor(layer,[st._ETIQUETA_FIELD], "{}=1315".format(st._CODI_FIELD))]
    uniq_cursor = sorted(list(set(cursor)), key=lambda x: x)
    multiplos_1k = len(list(filter(lambda x: x % 1000 == 0, uniq_cursor)))
    multiplos_otros = len(uniq_cursor)- multiplos_1k
    if layer.supports("LABELCLASSES"):
                            # el archivo layer de marcas de altitud tiene tres clases de etiquetas 
                            for lblClass in layer.labelClasses:
                                if lblClass.className=="L01_marcas_altitud":
                                    lblClass.SQLQuery = "{}=1315".format(st._CODI_FIELD)
                                    Max = max(uniq_cursor)
                                    Min = min(uniq_cursor)
                                    if Max-Min >= 4000:
                                        if multiplos_1k >= multiplos_otros:
                                            condi = "=="
                                        else:
                                            condi = "!="
                                        lblClass.expression = st._ETIQUETA_FUNCTION_MARK_ALTITUD1.format(condi, Min, Max)
                                    else: lblClass.expression = st._ETIQUETA_FUNCTION_MARK_ALTITUD2
                                    lblClass.showClassLabels = True
                                else:
                                    pass

    response['response'] = True
except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()#e.message
finally:
    response = json.dumps(response)
    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()
    arcpy.SetParameterAsText(8, response)