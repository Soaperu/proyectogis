# -*- coding: utf-8 -*-
"""
Proceso que permite determinar los limites regionales
en funcion de los cuadrantes de 1km.
Trabaja sobre proyecciones UTM (17S, 18S, 19S),
por lo que todas las distancias (e.g. 4 = 4 metros,
10 = 10 metros) tienen sentido en la unidad métrica.
"""

import os
import sys
import uuid
import shutil
import warnings
import traceback
import multiprocessing

import arcpy
import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.dates as mdates

from datetime import datetime, timedelta

# Desactiva advertencias futuras que aparecían en versiones anteriores
warnings.simplefilter(action='ignore', category=FutureWarning)

# Supongamos que estas funciones y constantes vienen de "model.py"
# from model import (Suplies, regiones, EXTENTION_SHP, EXTENTION_LOG, 
#                    EXTENTION_PNG, TEMP_DIR, msg, get_path_output, get_path_tmp)

# Ejemplo (placeholder), para que la estructura sea consistente:
EXTENTION_SHP = ".shp"
EXTENTION_LOG = ".log"
EXTENTION_PNG = ".png"
TEMP_DIR = r"C:\temp"
# Clases / funciones supuestas
class Suplies:
    def __init__(self):
        self.quads = "cuadriculas"
        self.sea = "mar_peruano"
        self.region = "region"
        self.regions = "regiones"

class regiones:
    def __init__(self):
        # nm_depa podría ser un objeto con un 'name' que devuelva el nombre del campo
        self.nm_depa = type("NmDepa", (object,), {"name": "NOMBRE_DEP"})()

def get_path_output():
    return r"C:\salida"

def get_path_tmp():
    return r"C:\trabajo_temporal"

# Mensajes (placeholder) 
class msg:
    TITLE_AXIS_X_GRAPH = "Tiempo"
    TITLE_AXIS_Y_GRAPH = "Procesos"
    TITLE_GRAPH = "Duración del procesamiento"


def clear_selection(feature_layer):
    """Encapsula la limpieza de la selección en ArcPy."""
    arcpy.SelectLayerByAttribute_management(feature_layer, "CLEAR_SELECTION")


def get_cuadriculas_regionales(folder_path):
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

    print(f"Procesando carpeta: {response['label']}")

    # Parámetros configurables
    _ADMINISTRATION_PERCENTAGE = 0.5     # Umbral para área de intersección
    _SEARCH_DISTANCE = '10 METERS'       # Distancia de búsqueda
    _INTERCECT_DISTANCE = 4             # Tolerancia en la operación de intersección (4 m)
    _FID_FIELD = 'FID'                  # Campo FID u OID
    _foo = Suplies()
    _region = regiones()

    # Suponemos que el nombre del folder padre indica el "datum"
    _DATUM = os.path.basename(os.path.dirname(folder_path))
    _OUTPUT_DIR = get_path_output()
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
        query = f"{_FID_FIELD} IN ({', '.join(cuadriculas_externas)})"
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


def filelog(*args):
    """
    Genera un archivo .log con la traza del error.
    Lo abre automáticamente en Windows.
    """
    pathfile = os.path.join(TEMP_DIR, uuid.uuid4().hex + EXTENTION_LOG)
    with open(pathfile, 'w', encoding='utf-8') as f:
        f.write('Sucedió un error:\n')
        for k in args:
            f.write(k + '\n')

    # En Windows
    os.startfile(pathfile)


def generar_grafico(response, datum):
    """
    Genera un gráfico tipo diagrama de Gantt sencillo con matplotlib,
    mostrando los tiempos de inicio y fin de cada proceso.
    """
    _COLOR_VLINES = '#919190'
    _COLOR_HLINES = '#ff5959'
    _COLOR_SCATTER = '#ff5959'
    _LINESTYLE_VLINES = '--'
    _MARKER_SCATTER = 'o'

    pathgraph = os.path.join(TEMP_DIR, uuid.uuid4().hex + EXTENTION_PNG)

    df = pd.DataFrame(response)
    df['start'] = pd.to_datetime(df['start'])
    df['end'] = pd.to_datetime(df['end'])

    fig, ax = plt.subplots(figsize=(12, 8))
    plt.grid(True)

    # Dibujamos líneas horizontales desde 'start' hasta 'end'
    # y detectamos conexiones verticales (cuando un start coincide con un end).
    yvaluesmin = []
    yvaluesmax = []
    xvalues = []

    for i, row in df.iterrows():
        # Buscar si existe algún proceso cuyo start = row['end']
        # (la lógica original detecta coincidencias con df[df['start'] == v['end']])
        end_val = row['end']
        subset = df[df['start'] == end_val]
        if not subset.empty:
            yvaluesmin.append(i)
            yvaluesmax.append(subset.index.values[0])
            xvalues.append(subset['start'].values[0])  # el 'start' del subset

    # Convertimos xvalues en formato numérico para vlines
    if xvalues:
        xvalues_datetime = pd.to_datetime(xvalues)
        ax.vlines(
            x=xvalues_datetime,
            ymin=yvaluesmin,
            ymax=yvaluesmax,
            linestyle=_LINESTYLE_VLINES,
            color=_COLOR_VLINES
        )

    # Líneas horizontales
    ax.hlines(
        y=[i for i in range(len(df))],
        xmin=mdates.date2num(df['start']),
        xmax=mdates.date2num(df['end']),
        linewidth=2,
        color=_COLOR_HLINES
    )

    # Marcamos los puntos finales
    ax.scatter(
        df['end'].values,
        [i for i in range(len(df))],
        color=_COLOR_SCATTER,
        marker=_MARKER_SCATTER
    )

    # Añadimos el texto con la duración
    for i, value in enumerate(df['time'].tolist()):
        ax.text(
            df['end'].iloc[i],
            i + 0.2,
            value,
            fontsize=9
        )

    # Ajustamos eje Y con los labels de cada proceso
    plt.yticks(range(len(df)), df['label'].tolist())

    # Formateador para el eje X (fechas)
    ax.xaxis.set_major_formatter(mdates.DateFormatter('%Y-%m-%d %H:%M:%S'))
    plt.xlabel(msg.TITLE_AXIS_X_GRAPH)
    plt.ylabel(msg.TITLE_AXIS_Y_GRAPH)
    plt.title(f"{msg.TITLE_GRAPH} - {datum.upper()}", fontsize=14)

    plt.xticks(rotation=30, ha='right')
    plt.tight_layout()
    plt.savefig(pathgraph, bbox_inches='tight')
    os.startfile(pathgraph)  # Abre la imagen en Windows


def main(num_cores, folders, datum):
    """
    Ejecuta la lógica en paralelo para cada carpeta en 'folders'
    usando 'num_cores' procesos, luego genera el gráfico de seguimiento.
    """
    pool = multiprocessing.Pool(num_cores)
    response = pool.map(get_cuadriculas_regionales, folders)
    pool.close()
    pool.join()

    # Generar gráfico final
    generar_grafico(response, datum)


if __name__ == '__main__':
    try:
        # sys.argv[1]: "datum1;datum2;..."
        # sys.argv[2]: número de cores
        datums = sys.argv[1].split(';')
        cores = int(sys.argv[2])

        work_dir = get_path_tmp() # carpeta de insumos *JCruz
        output_dir = get_path_output()

        for datum in datums:
            print(f"DATUM: {datum}")
            path = os.path.join(output_dir, datum)
            # Limpia la carpeta de salida si existe
            if os.path.exists(path):
                shutil.rmtree(path)
            os.mkdir(path)

            datum_dir = os.path.join(work_dir, datum)
            if not os.path.exists(datum_dir):
                print(f"No existe la carpeta de trabajo: {datum_dir}")
                continue

            # Lista de subcarpetas a procesar
            folders = [os.path.join(datum_dir, i) for i in os.listdir(datum_dir)]
            main(cores, folders, datum)

    except Exception as e:
        # Si ocurre un error, generamos un archivo de log
        filelog(traceback.format_exc())
