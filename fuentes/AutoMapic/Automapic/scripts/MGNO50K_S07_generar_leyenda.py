import sys
reload(sys)
sys.setdefaultencoding("utf-8")
# Importar librerias
import arcpy
import json
# from MG_S00_model import tb_dgr_leyenda
import MG_S00_model as model
import pandas as pd
from shapely.geometry import Polygon
from shapely import ops
from PG_S01_mapa_peligros_geologicos import set_detalle
import automapic_template_json as auttmp
# import matplotlib.pyplot as plt
import automapic as aut
import settings_aut as st
import os
import re
import traceback


def featureTable_to_dataFrame(table, query="#"):
    """
    Convierte un FeatureTable en un objeto dataframe de pandas
    @table: ruta del featuretable
    @query (#): Si se aplica alguna definicion de consulta 
    """
    cursor = arcpy.da.SearchCursor(table, ["*"], query)
    fields = arcpy.ListFields(table)
    data = list()
    for i in cursor:
        row = dict()
        for idx, field in enumerate(fields):
            row[field.name] = i[idx]
        data.append(row)
    df = pd.DataFrame(data)
    return df

def generate_diagram(row):
    """
    Determina el poligono que define el diagrama
    @row: file de un objeto pandas data frame
    """
    global _H_CURRENT, _TB_LEYENDA, _FIELD_GROSOR, _TYPE_BOX, _X_INI_DIAGRAMA
    response = dict()
    response['codi'] = row[_TB_LEYENDA.codi]
    response['nombre'] = row[_TB_LEYENDA.etiqueta]
    x_min = _X_INI_DIAGRAMA
    y_min = _H_CURRENT
    x_max = _X_INI_DIAGRAMA + _W_RE_DIAGRAMA
    response['y_max'] = _H_CURRENT + row[_FIELD_GROSOR] * factor
    coords = [(x_min, y_min), (x_min, response['y_max']), (x_max, response['y_max']), (x_max, y_min)]
    response['polygon'] = Polygon(coords)
    response['height'] = response['y_max'] - y_min
    response['tipo'] = _TYPE_BOX['diagrama']
    _H_CURRENT = response['y_max']
    return response

def generate_description_label(pol, description):
    """
    Obtiene el texto, coordenadas x, y, tipo, horientacion y rotacion del 
    texto de descripcion de diagramas
    """
    global _X_INI_DESCRIPCION_LABEL, _TYPE_BOX, _SPLIT_DESCRIPTION
    response = dict()
    _, y_ctr = pol['polygon'].centroid.xy
    response['x'] = _X_INI_DESCRIPCION_LABEL
    response['y'] = y_ctr[0]
    # description = df_leyenda[df_leyenda['CODI'] == pol['codi']]['DESCRIP'].iloc[0]
    response['nombre'] = set_detalle(description, _SPLIT_DESCRIPTION)
    response['etiqueta'] = pol['nombre']
    response['tipo'] = _TYPE_BOX['descripcion']
    response['rotation'] = 0
    response['horizontalalignment'] = 'left'
    return response

def generate_polygon_serie(h, y_max):
    """
    Construye el poligono de serie
    """
    global _X_INI_SERIE, _X_INI_SPACE
    y_min = y_max - h
    coords = [(_X_INI_SERIE, y_min), (_X_INI_SERIE, y_max), (_X_INI_SPACE, y_max), (_X_INI_SPACE, y_min)]
    plg = Polygon(coords)
    return plg

def generate_polygon_unidad(h, y_max):
    """
    Construye el poligono de unidad
    """
    global _X_INI_UNIDAD, _X_INI_DIAGRAMA
    y_min = y_max - h
    coords = [(_X_INI_UNIDAD, y_min), (_X_INI_UNIDAD, y_max), (_X_INI_DIAGRAMA, y_max), (_X_INI_DIAGRAMA, y_min)]
    plg = Polygon(coords)
    return plg

def generate_polygon_sistema(array_serie):
    """
    Construye los datos necesarios para el sistema
    """
    global _DF_EDADES, _TB_EDADES, _X_INI_SISTEMA, _TYPE_BOX
    response = dict()
    response['id_edad'] = array_serie[0]['id_edad'][:3]
    response['nombre'] = _DF_EDADES[_DF_EDADES[_TB_EDADES.id_edad] == response['id_edad']][_TB_EDADES.nombre].item()
    y_min = array_serie[0]['polygon'].bounds[1]
    y_max = array_serie[-1]['polygon'].bounds[-1]
    coords = [(_X_INI_SISTEMA, y_min), (_X_INI_SISTEMA, y_max), (_X_INI_SERIE, y_max), (_X_INI_SERIE, y_min)]
    response['polygon'] = Polygon(coords)
    response['tipo'] = _TYPE_BOX['sistema']
    return response

def generate_polygon_edad(array_edad):
    """
    Construye los datos necesarios para el poligono de edad
    """
    global _X_INI_EDAD, _W_RNE_EDAD_SPACE, _TYPE_BOX
    response = dict()
    # response['id_edad'] = array_serie[0]['id_edad'][:2]
    response['nombre'] = array_edad[_TB_LEYENDA.edad]
    y_min = array_edad['polygon'].bounds[1]
    y_max = array_edad['polygon'].bounds[-1]
    coords = [(_X_INI_EDAD, y_min), (_X_INI_EDAD, y_max), (_X_INI_EDAD + _W_RNE_EDAD_SPACE, y_max), (_X_INI_EDAD + _W_RNE_EDAD_SPACE, y_min)]
    response['polygon'] = Polygon(coords)
    response['tipo'] = _TYPE_BOX['edad']
    return response

def generate_polygon_eratema(array_serie):
    """
    Construye los datos necesarios para el eratema
    """
    global _DF_EDADES, _X_INI_ERATEMA, _TB_EDADES, _TYPE_BOX
    response = dict()
    response['id_edad'] = array_serie[0]['id_edad'][:2]
    response['nombre'] = _DF_EDADES[_DF_EDADES[_TB_EDADES.id_edad] == response['id_edad']][_TB_EDADES.nombre].item()
    y_min = array_serie[0]['polygon'].bounds[1]
    y_max = array_serie[-1]['polygon'].bounds[-1]
    coords = [(_X_INI_ERATEMA, y_min), (_X_INI_ERATEMA, y_max), (_X_INI_SISTEMA, y_max), (_X_INI_SISTEMA, y_min)]
    response['polygon'] = Polygon(coords)
    response['tipo'] = _TYPE_BOX['eratema']
    return response

def generate_legend_by_area(Ulito, area):
    fc_clip = arcpy.Clip_analysis(Ulito, area, "in_memory/GPO_DGR_ULITO_area_estudio")
    lista_codi= [ x[0] for x in arcpy.da.SearchCursor(fc_clip, [st._CODI_FIELD])]
    mxd_c = arcpy.mapping.MapDocument("CURRENT")
    df = arcpy.mapping.ListDataFrames(mxd_c,"DFMAPAPRINCIPAL*")[0]
    layer= arcpy.mapping.Layer("in_memory/GPO_DGR_ULITO_area_estudio")
    arcpy.mapping.AddLayer(df,layer,"BOTTOM")
    df_filtro = pd.DataFrame(lista_codi, columns=[st._CODI_FIELD])
    return df_filtro

try:
    response = dict()
    response['status'] = 1
    response['message'] = 'success'
    codhoja = arcpy.GetParameterAsText(0)
    tipo_leyenda = arcpy.GetParameterAsText(1)
    dominio = arcpy.GetParameterAsText(2)
    x_ini = float(arcpy.GetParameterAsText(3))
    y_ini = float(arcpy.GetParameterAsText(4))
    tipo_cobertura = arcpy.GetParameterAsText(5)
    area_estudio= arcpy.GetParameterAsText(6)
    zona = arcpy.GetParameterAsText(7)

    #codhoja = '29r4'
    #tipo_leyenda = '1'
    # dominio = '1'

    _FIELD_GROSOR = 'GROSOR'
    _H_CURRENT = 0

    # Valor a aplicar a la altura de los diagramas
    factor = 1

    # Dimensiones verticales (altura) de componentes de la leyenda
    h_upper_separator = 125         # Altura del separador obligatorio en el parte alta
    h_lower_separator = 125         # Altura del separador obligatorio en la parte baja
    h_separator = 125               # Altura del separador entre diagramas
    h_header = 700                  # Altura del header
    h_re = 300                      # Altura del box de rocas estratificadas
    h_re_sub = 400                  # Altura del box de diagrama y descripcion

    # dimension horizontal (width) de componentes de la leyenda
    w_eratema = 250                 # Ancho del box de eratema
    w_sistema = 250                 # Ancho del box de sistema
    w_serie = 250                   # Ancho del box de serie
    w_space = 100                   # Ancho del box de espacio en blanco entre los box serie y unidad
    w_unidad = 350                  # Ancho del box de unidad
    _W_RE_DIAGRAMA = 1000           # Ancho del box de diagrama para rocas estratificadas (re)
    w_re_descripcion = 3275 if tipo_leyenda == '1' else  1550  # Ancho del box de descripcion para rocas estratificadas (re)
    w_re_descripcion_space = 750 #100    # Ancho de la sangria para los textos de descripcion para rocas estratificadas (re)
    w_rne_diagrama = 1000           # Ancho del box de diagrama para rocas no estratificadas (rne)
    # w_rne_descripcion = 1550        # Ancho del box de descripcion para rocas no estratificadas (rne)
    # w_rne_descripcion_space = 100   # Ancho de la sangria para los textos de descripcion para rocas no estratificadas (rne)
    _W_RNE_EDAD_SPACE = 250          # Ancho del box de edad para rocas no estratificadas (rne)

    _X_INI_ERATEMA = x_ini   # Coordenada inicial de la columna eratema
    _X_INI_SISTEMA = _X_INI_ERATEMA + w_eratema   # Coordenada X inicial de la columna sistema
    _X_INI_SERIE = _X_INI_SISTEMA + w_sistema     # Coordenada X inicial de la columna serie
    _X_INI_SPACE = _X_INI_SERIE + w_serie         # Coordenada X inicial de la columna de espacio obligatorio
    _X_INI_UNIDAD = _X_INI_SPACE + w_space        # Coordenada X inicial del la columna unidades
    _X_INI_DIAGRAMA = _X_INI_UNIDAD + w_unidad    # Coordenada X inicial del la columna diagrama
    x_ini_descripcion = _X_INI_DIAGRAMA + _W_RE_DIAGRAMA  # Coordenada X inicial del la columna descripcion
    _X_INI_DESCRIPCION_LABEL = _X_INI_DIAGRAMA + _W_RE_DIAGRAMA + w_re_descripcion_space  # Coordenada X inicial para los textos de descripcion em RE
    _X_INI_EDAD = x_ini_descripcion + w_re_descripcion

    _SPLIT_DESCRIPTION = int()
    if tipo_leyenda == '1':
        _SPLIT_DESCRIPTION = 53
    elif tipo_leyenda == '2':
        _SPLIT_DESCRIPTION = 23

    _TYPE_BOX = {
        'eratema': 1,
        'sistema': 2,
        'serie': 3,
        'unidad': 4,
        'diagrama': 5,
        'descripcion': 6,
        'header_eratema': 7,
        'header_sistema': 8,
        'header_serie': 9,
        'header_unidad': 10,
        'header_diagrama': 11,
        'header_descripcion': 12,
        'header_rocas_estratificadas': 13,
        'header_rocas_no estratificadas': 14,
        'header_edad': 15,
        'auxiliar': 16,
        'edad': 17
    }

    _NAME_HEADER = {
        'eratema': 'ERATEMA',
        'sistema': 'SISTEMA',
        'serie': 'SERIE',
        'unidad': 'UNIDAD\n(+Grosor)',
        'diagrama': 'DIAGRAMA',
        're': 'ROCAS ESTRATIFICADAS',
        'rne': 'ROCAS NO ESTRATIFICADAS',
        'descripcion': 'DESCRIPCION GEOLOGICA',
        'edad': 'EDAD\n(Ma)'
    }

    # Obtenemos los datos de leyenda como DataFrame
    _TB_LEYENDA = model.tb_dgr_leyenda()
    query = "({} IN ('{}')) and ({} = 1) and ({} = {}) and ({} = '{}')".format(_TB_LEYENDA.codhoja, "','".join(codhoja.split(",")),
                                                                                _TB_LEYENDA.estado, _TB_LEYENDA.dominio,
                                                                                  dominio, _TB_LEYENDA.tipo, tipo_leyenda)
    df_leyenda = featureTable_to_dataFrame(_TB_LEYENDA.path, query)
    # Si la opcion de covertura es por area de estudio filtramos la tabla solo con
    #  las litologias dentro del area de estudio para la generaicon de la leyenda
    if tipo_cobertura == "1": # 1: Leyenda Geologica solo para area de estudio
        df_filtro_area = generate_legend_by_area(st._ULITO_MG_PATH.format(zona, zona), area_estudio)
        df_leyenda = df_leyenda[df_leyenda[st._CODI_FIELD].isin(df_filtro_area[st._CODI_FIELD])].copy()

    # Obtenemos los datos de edades como DataFrame
    _TB_EDADES = model.tb_edades()
    _DF_EDADES = featureTable_to_dataFrame(_TB_EDADES.path)

    # Obtenemos los datos de acuerdo al tipo de leyenda seleccionado
    df_leyenda = df_leyenda[df_leyenda[_TB_LEYENDA.tipo] == tipo_leyenda]

    # El usuario ordena la leyenda en la _TB_LEYENDA desde el mas reciente al mas antiguo
    # Para la construccion de la leyenda necesitamos ordenarlo del mas antiguo al mas nuevo
    df_leyenda.sort([_TB_LEYENDA.orden], ascending=False, inplace=True)
    df_leyenda.reset_index(inplace=True, drop=True)

    # Obtener la altura a utilizar por cada diagrama segun lo especificado en la tabla de leyenda
    # Se estable una jerarquia donde el grosor de usuario es mas importe (grosor_u), de no tener algun valor
    # se tomara el grosor medido (grosor_m) y por ultimo el grosor indirecto (grosor_i)
    height_diagram = list()
    dict_grosor=dict()
    for idx, row in df_leyenda.iterrows():
        if row[_TB_LEYENDA.grosor_u]:
            height_diagram.append(row[_TB_LEYENDA.grosor_u])
        elif row[_TB_LEYENDA.grosor_m]:
            height_diagram.append(row[_TB_LEYENDA.grosor_m])
        elif row[_TB_LEYENDA.grosor_i]:
            height_diagram.append(row[_TB_LEYENDA.grosor_i])
        
    # Agregamos la lista height_diagram como una columna del dataframe de leyenda
    df_leyenda[_FIELD_GROSOR] = height_diagram
    for r, rowx in df_leyenda.iterrows():
        dict_grosor[rowx[_TB_LEYENDA.unidad]]=rowx[_FIELD_GROSOR]
    # Obtener la altura total de la leyenda
    altura_diagramas = sum(map(lambda i: factor * i, df_leyenda[_FIELD_GROSOR].tolist()))
    altura_separadores = len(df_leyenda[df_leyenda[_TB_LEYENDA.separador] == '1']) * h_separator
    altura_obligatoria = h_lower_separator + h_upper_separator + h_header

    h_total = altura_diagramas + altura_separadores + altura_obligatoria

    h_unidad = 0
    # if tipo_leyenda == '1':
    #     # Altura de la caja inferior de unidad
    _H_CURRENT = y_ini + h_lower_separator
    h_unidad = _H_CURRENT

    # ###############################
    # ### Construccion de leyenda ###
    # ###############################

    # Lista que contendra todos los poligonos de diagramas
    diagrama_arr = list()

    # Variables para generacion de poligonos de unidad
    unidad = None
    unidad_arr = list()
    diagrama_arr_h_unidad = list()

    # Variables para generacion de serie
    serie = None
    serie_arr = list()
    diagrama_arr_h_serie = list()
    serie_first = 1

    # Lista que contendra todas las etiquetas de la leyenda
    etiquetas_arr = list()

    for i, row in df_leyenda.iterrows():
        if not serie:
            serie = row[_TB_LEYENDA.serie]
        if serie != row[_TB_LEYENDA.serie]: #or i == len(df_leyenda) - 1:
            # Construir poligono de serie
            y_max_serie = diagrama_arr[-1]['y_max']
            if serie_first == 1:
                diagrama_arr_h_serie.append(h_upper_separator)
                serie_first = 0
            serie_pol = generate_polygon_serie(sum(diagrama_arr_h_serie), y_max_serie)
            nombre_serie = _DF_EDADES[_DF_EDADES[_TB_EDADES.id_edad] == serie][_TB_EDADES.nombre].item()
            serie_arr.append({'id_edad': serie, 'nombre': nombre_serie, 'polygon': serie_pol, 'tipo': _TYPE_BOX['serie']})
            serie = row[_TB_LEYENDA.serie]
            diagrama_arr_h_serie = []
        if i == len(df_leyenda) - 1:
            y_max_serie = _H_CURRENT
            # y_max_serie = diagrama_arr[-1]['y_max']
            # if serie_first == 1:
            #     diagrama_arr_h_serie.append(h_upper_separator)
            #     serie_first = 0
            y_max_serie = y_max_serie + row[_FIELD_GROSOR] + h_upper_separator
            diagrama_arr_h_serie.append(row[_FIELD_GROSOR])
            diagrama_arr_h_serie.append(h_upper_separator)
            serie_pol = generate_polygon_serie(sum(diagrama_arr_h_serie), y_max_serie)
            nombre_serie = _DF_EDADES[_DF_EDADES[_TB_EDADES.id_edad] == serie][_TB_EDADES.nombre].item()
            serie_arr.append({'id_edad': serie, 'nombre': nombre_serie, 'polygon': serie_pol, 'tipo': _TYPE_BOX['serie']})
            serie = row[_TB_LEYENDA.serie]
            diagrama_arr_h_serie = []
        if not unidad:
            unidad = row[_TB_LEYENDA.unidad]
        if unidad != row[_TB_LEYENDA.unidad]: #or i == len(df_leyenda) - 1:
            # Construir poligono de unidad
            y_max_unidad = diagrama_arr[-1]['y_max']
            # if i == len(df_leyenda) - 1:
            #     y_max_unidad = y_max_unidad + row[_FIELD_GROSOR]
            #     diagrama_arr_h_unidad.append(row[_FIELD_GROSOR])
            unidad_pol = generate_polygon_unidad(sum(diagrama_arr_h_unidad), y_max_unidad)
            unidad_arr.append({'nombre': unidad + '\n(+-{}m)'.format(dict_grosor[unidad]), 'polygon': unidad_pol, 'tipo': _TYPE_BOX['unidad']})
            unidad = row[_TB_LEYENDA.unidad]
            diagrama_arr_h_unidad = []
        if i == len(df_leyenda) - 1:
            # y_max_unidad = diagrama_arr[-1]['y_max']
            y_max_unidad = _H_CURRENT
            y_max_unidad = y_max_unidad + row[_FIELD_GROSOR]
            diagrama_arr_h_unidad.append(row[_FIELD_GROSOR])
            unidad_pol = generate_polygon_unidad(sum(diagrama_arr_h_unidad), y_max_unidad)
            unidad_arr.append({'nombre': unidad + '\n(+-{}m)'.format(dict_grosor[unidad]), 'polygon': unidad_pol, 'tipo': _TYPE_BOX['unidad']})
            unidad = row[_TB_LEYENDA.unidad]
            diagrama_arr_h_unidad = []

        # Construir poligono de diagrama
        pol = generate_diagram(row)
        diagrama_arr.append(pol)
        description = df_leyenda[df_leyenda[_TB_LEYENDA.codi] == pol['codi']][_TB_LEYENDA.descripcion].iloc[0]
        lab = generate_description_label(pol, description)
        etiquetas_arr.append(lab)
        diagrama_arr_h_unidad.append(pol['height'])
        diagrama_arr_h_serie.append(pol['height'])
        if row[_TB_LEYENDA.separador] == '1':
            _H_CURRENT = _H_CURRENT + h_separator

    # Obtenemos las formaciones que aparecen mas de una vez, esto quiere decir
    # que pertenecen a mas de una serie
    df_mergin = df_leyenda.groupby(by=_TB_LEYENDA.etiqueta).size().reset_index(name="count")
    df_mergin = df_mergin[df_mergin['count'] == 2]

    for idx, row in df_mergin.iterrows():
        elms = [(i, pol) for i, pol in enumerate(diagrama_arr) if pol['nombre'] == row[_TB_LEYENDA.etiqueta]]
        elms_etiqueta = [(i, pol) for i, pol in enumerate(etiquetas_arr) if pol['etiqueta'] == row[_TB_LEYENDA.etiqueta]]
        elm_first = elms[0]
        elm_first_etiqueta = elms_etiqueta[0]
        new_pol = ops.cascaded_union([pol[1]['polygon'] for pol in elms])
        elm_first[1]['polygon'] = new_pol

        description = df_leyenda[df_leyenda[_TB_LEYENDA.codi] == elm_first[1]['codi']][_TB_LEYENDA.descripcion].iloc[0]
        new_row = generate_description_label(elm_first[1], description)

        diagrama_arr = filter(lambda pol: pol['nombre'] != row[_TB_LEYENDA.etiqueta], diagrama_arr)
        diagrama_arr.insert(elm_first[0], elm_first[1])

        etiquetas_arr = filter(lambda pol: pol['etiqueta'] != row[_TB_LEYENDA.etiqueta], etiquetas_arr)
        etiquetas_arr.insert(elm_first_etiqueta[0], new_row)

    # Construir poligono de sistema
    sistema_arr_unique = set(map(lambda i:i['id_edad'][:3], serie_arr))
    sistema_arr = list()

    for sistema in sistema_arr_unique:
        array_serie = filter(lambda i: i['id_edad'].startswith(sistema), serie_arr)
        pol_sistema = generate_polygon_sistema(array_serie)
        sistema_arr.append(pol_sistema)

    # Construir poligono de eratema
    eratema_arr_unique = set(map(lambda i:i['id_edad'][:2], serie_arr))
    eratema_arr = list()

    for eratema in eratema_arr_unique:
        array_serie = filter(lambda i: i['id_edad'].startswith(eratema), serie_arr)
        pol_eratema = generate_polygon_eratema(array_serie)
        eratema_arr.append(pol_eratema)


    # Construir poligono de edad
    edad_arr = list()
    if tipo_leyenda == '2':
        for dgm in diagrama_arr:
            row = df_leyenda[df_leyenda[_TB_LEYENDA.codi] == dgm['codi']].iloc[0]
            if row[_TB_LEYENDA.edad]:
                array_edad = dict()
                array_edad[_TB_LEYENDA.edad] = row[_TB_LEYENDA.edad]
                array_edad['polygon'] = dgm['polygon']
                pol_edad = generate_polygon_edad(array_edad)
                edad_arr.append(pol_edad)


    # Header
    auxiliares_arr = list()
    y_min_aux = serie_arr[-1]['polygon'].bounds[-1]
    y_max_aux = y_min_aux + h_header

    x_min_main = eratema_arr[0]['polygon'].bounds[0]
    y_min_main = eratema_arr[0]['polygon'].bounds[1]

    # Agregar header eratema
    # if tipo_leyenda == '1':
    coords = [(_X_INI_ERATEMA, y_min_aux), (_X_INI_ERATEMA, y_max_aux), (_X_INI_SISTEMA, y_max_aux), (_X_INI_SISTEMA, y_min_aux)]
    plg_eratema = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['eratema'], 'polygon': plg_eratema, 'tipo': _TYPE_BOX['header_eratema']})

    # Agregar header sistema
    coords = [(_X_INI_SISTEMA, y_min_aux), (_X_INI_SISTEMA, y_max_aux), (_X_INI_SERIE, y_max_aux), (_X_INI_SERIE, y_min_aux)]
    plg_sistema = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['sistema'], 'polygon': plg_sistema, 'tipo': _TYPE_BOX['header_sistema']})

    # Agregar header serie
    coords = [(_X_INI_SERIE, y_min_aux), (_X_INI_SERIE, y_max_aux), (_X_INI_SPACE, y_max_aux), (_X_INI_SPACE, y_min_aux)]
    plg_serie= Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['serie'], 'polygon': plg_serie, 'tipo': _TYPE_BOX['header_serie']})

    # Agregar header unidad
    coords = [(_X_INI_UNIDAD, y_min_aux), (_X_INI_UNIDAD, y_max_aux), (_X_INI_DIAGRAMA, y_max_aux), (_X_INI_DIAGRAMA, y_min_aux)]
    plg_unidad = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['unidad'], 'polygon': plg_unidad, 'tipo': _TYPE_BOX['header_unidad']})

    # Agregar header roca estratificada
    y_min_re = y_min_aux + h_re_sub
    x_max_re = _X_INI_DIAGRAMA + _W_RE_DIAGRAMA + w_re_descripcion
    coords = [(_X_INI_DIAGRAMA, y_min_re), (_X_INI_DIAGRAMA, y_max_aux), (x_max_re, y_max_aux), (x_max_re, y_min_re)]
    plg_re = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['re'] if tipo_leyenda == '1' else _NAME_HEADER['rne'] , 'polygon': plg_re, 'tipo': _TYPE_BOX['header_rocas_estratificadas']})

    # Agregar header roca estratificada Diagrama
    y_max_diagrama = y_max_aux - h_re
    coords = [(_X_INI_DIAGRAMA, y_min_aux), (_X_INI_DIAGRAMA, y_max_diagrama), (x_ini_descripcion, y_max_diagrama), (x_ini_descripcion, y_min_aux)]
    plg_re_diagrama = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['diagrama'], 'polygon': plg_re_diagrama, 'tipo': _TYPE_BOX['header_diagrama']})

    # Agregar header roca estratificada Descripcion
    coords = [(x_ini_descripcion, y_min_aux), (x_ini_descripcion, y_max_diagrama), (x_max_re, y_max_diagrama), (x_max_re, y_min_aux)]
    plg_re_descripcion = Polygon(coords)
    auxiliares_arr.append({'nombre': _NAME_HEADER['descripcion'], 'polygon': plg_re_descripcion, 'tipo': _TYPE_BOX['header_descripcion']})

    # Agregar header edad
    if tipo_leyenda == '2':
        coords = [(_X_INI_EDAD, y_min_aux), (_X_INI_EDAD, y_max_aux), (_X_INI_EDAD + _W_RNE_EDAD_SPACE, y_max_aux), (_X_INI_EDAD + _W_RNE_EDAD_SPACE, y_min_aux)]
        plg_rne_edad = Polygon(coords)
        auxiliares_arr.append({'nombre': _NAME_HEADER['edad'], 'polygon': plg_rne_edad, 'tipo': _TYPE_BOX['edad']})

    # Main border
    # No tiene nombre porque solo hace referencia a la cuadricula exterior de la leyenda
    if tipo_leyenda == '2':
        x_max_re = x_max_re + _W_RNE_EDAD_SPACE
    coords = [(x_min_main, y_ini), (x_min_main, y_max_aux), (x_max_re, y_max_aux), (x_max_re, y_ini)]
    plg_re_main = Polygon(coords)
    auxiliares_arr.append({'nombre': '', 'polygon': plg_re_main, 'tipo': _TYPE_BOX['auxiliar']})

    def re_depositos(text):
        patron = r'[Dd]ep.*sit[os]'
        # Buscar si existe una coincidencia con el patrón
        existe_coincidencia = bool(re.search(patron, text, re.IGNORECASE))
        return existe_coincidencia

    # Se agrupan todas las etiquetas
    annotation_plot = list()
    for i in diagrama_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['diagrama'], 'rotation': rotation, 'horizontalalignment': 'center'})
    for i in unidad_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 0 if i['tipo']==4 and re_depositos(i['nombre']) else 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['unidad'], 'rotation': rotation, 'horizontalalignment': 'center'})
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0] + 1200, 'y': y[0], 'tipo': _TYPE_BOX['unidad'], 'rotation': rotation, 'horizontalalignment': 'left'})
    for i in serie_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['serie'], 'rotation': rotation, 'horizontalalignment': 'center'})
    for i in sistema_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['sistema'], 'rotation': rotation, 'horizontalalignment': 'center'})
    # if tipo_leyenda == '1':
    for i in eratema_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['eratema'], 'rotation': rotation, 'horizontalalignment': 'center'})
    if tipo_leyenda == '2':
        for i in edad_arr:
            x, y = i['polygon'].centroid.xy
            rotation = 270
            annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': _TYPE_BOX['edad'], 'rotation': rotation, 'horizontalalignment': 'center'})
    for i in auxiliares_arr:
        x, y = i['polygon'].centroid.xy
        rotation = 90 if i['tipo'] in [1, 2, 3, 4, 7, 8, 9, 10, 15, 17] else 0
        annotation_plot.append({'nombre': i['nombre'], 'x': x[0], 'y': y[0], 'tipo': i['tipo'], 'rotation': rotation, 'horizontalalignment': 'center'})


    annotation_plot.extend(etiquetas_arr)

    # # Objeto que representa las etiquetas
    # print(annotation_plot)

    auttmp._PT_LEYENDA_TEMPLATE_MG['features'] = []

    for ann in annotation_plot:
        data = {
            "attributes": {
                "ETIQUETA": ann['nombre'],
                "ESTILO": ann['tipo'],
                "ANGULO": ann['rotation'],
                "ALINEACION": ann['horizontalalignment'],
                "ESTADO": 1,
                "TIPO": tipo_leyenda,
                "DOMINIO": int(dominio),
                "HOJA": codhoja.split(",")[0][:-1],
                "CUADRANTE": codhoja.split(",")[0][-1],
                "CODHOJA": codhoja.split(",")[0]
            },
            "geometry": {
                "x": ann['x'],
                "y": ann['y']
            }
        }
        auttmp._PT_LEYENDA_TEMPLATE_MG['features'].append(data)

    gpt_mg_label = model.gpt_mg_label()
    rows_ann = arcpy.AsShape(auttmp._PT_LEYENDA_TEMPLATE_MG, True)
    gpt_mg_label_mfl = arcpy.MakeFeatureLayer_management(gpt_mg_label.path, 'gpl_ann_path_{}'.format(codhoja.split(",")[0]), query)
    arcpy.DeleteRows_management(gpt_mg_label_mfl)
    arcpy.Append_management(rows_ann, gpt_mg_label.path, "NO_TEST")


    # Poligonos que representan los diagramas
    # print(diagrama_arr[0])

    auttmp._PO_LEYENDA_TEMPLATE_MG['features'] = []

    for dgm in diagrama_arr:
        diagram_data = df_leyenda[df_leyenda[_TB_LEYENDA.codi] == dgm['codi']].iloc[0]
        diagram_json = dgm['polygon'].__geo_interface__['coordinates']
        data = {
            "attributes": {
                "CODI": dgm['codi'],
                "ETIQUETA": dgm['nombre'],
                "UNIDAD": diagram_data[_TB_LEYENDA.unidad],
                "DESCRIP": diagram_data[_TB_LEYENDA.descripcion],
                # "SERIE": diagram_data[_TB_LEYENDA.serie],
                "TIPO": tipo_leyenda,
                "DOMINIO": int(dominio),
                # "EDAD": diagram_data[_TB_LEYENDA.edad],
                # "ORDEN": diagram_data[_TB_LEYENDA.orden],
                # "GROSOR_M": diagram_data[_TB_LEYENDA.grosor_m],
                # "GROSOR_I": diagram_data[_TB_LEYENDA.grosor_i],
                # "GROSOR_U": diagram_data[_TB_LEYENDA.grosor_u],
                "CODHOJA": codhoja.split(",")[0],
                # "SEP": diagram_data[_TB_LEYENDA.separador],
                "ESTADO": 1
            },
            "geometry": {
                "rings": diagram_json
            }
        }
        auttmp._PO_LEYENDA_TEMPLATE_MG['features'].append(data)

    gpo_form = model.gpo_mg_form()
    rows_diagram = arcpy.AsShape(auttmp._PO_LEYENDA_TEMPLATE_MG, True)

    columns = [_TB_LEYENDA.codhoja, _TB_LEYENDA.estado, _TB_LEYENDA.dominio, _TB_LEYENDA.tipo]
    with arcpy.da.UpdateCursor(gpo_form.path, columns, query) as cursor_uc:
        for row in cursor_uc:
            cursor_uc.deleteRow()
        del cursor_uc

    # gpo_diagram_mfl = arcpy.MakeFeatureLayer_management(gpo_form.path, 'gpo_diagram_path_{}'.format(codhoja), query)
    # arcpy.DeleteRows_management(gpo_diagram_mfl)
    arcpy.Append_management(rows_diagram, gpo_form.path, "NO_TEST")

    # # Estos deben ser un grupo y representarse como lineas
    list_aux = list()
    list_aux.extend(unidad_arr)
    list_aux.extend(serie_arr)
    list_aux.extend(sistema_arr)
    if tipo_leyenda == '2':
        list_aux.extend(edad_arr)
    # if tipo_leyenda == '1':
    list_aux.extend(eratema_arr)
    list_aux.extend(auxiliares_arr)

    auttmp._PL_LEYENDA_TEMPLATE_MG['features'] = []

    for aux in list_aux:
        aux_json = aux['polygon'].boundary.__geo_interface__['coordinates']
        data = {
            "attributes": {
                "CODI": 1,
                "TIPO": tipo_leyenda,
                "DOMINIO": int(dominio),
                "HOJA": codhoja.split(",")[0][:-1],
                "CUADRANTE": codhoja.split(",")[0][-1],
                "CODHOJA": codhoja.split(",")[0],
                "ESTADO": 1
            },
            "geometry": {
                "paths": [aux_json]
            }
        }
        auttmp._PL_LEYENDA_TEMPLATE_MG['features'].append(data)

    gpl_mg_celd = model.gpl_mg_celd()
    rows_aux = arcpy.AsShape(auttmp._PL_LEYENDA_TEMPLATE_MG, True)
    gpl_aux_mfl = arcpy.MakeFeatureLayer_management(gpl_mg_celd.path, 'gpl_aux_path_{}'.format(codhoja.split(",")[0]), query)
    arcpy.DeleteRows_management(gpl_aux_mfl)
    arcpy.Append_management(rows_aux, gpl_mg_celd.path, "NO_TEST")

    mxd = arcpy.mapping.MapDocument("CURRENT")
    #adf = mxd.activeDataFrame

    lyr_ann = os.path.join(st._LAYERS_DIR, gpt_mg_label.name + '.lyr')
    lyr_lin = os.path.join(st._LAYERS_DIR, gpl_mg_celd.name + '.lyr')
    lyr_pol = os.path.join(st._LAYERS_DIR, gpo_form.name + '.lyr')

    query_view = "({} IN ('{}')) and ({} = 1)".format(_TB_LEYENDA.codhoja, "','".join(codhoja.split(",")), _TB_LEYENDA.estado, _TB_LEYENDA.dominio)

    aut.add_layer_with_new_datasource(lyr_ann, gpt_mg_label.name, gpt_mg_label.workspace, 'FILEGDB_WORKSPACE', df_name="LEYENDA GEOLOGICA", query=query_view, mxd=mxd)
    aut.add_layer_with_new_datasource(lyr_lin, gpl_mg_celd.name, gpl_mg_celd.workspace, 'FILEGDB_WORKSPACE', df_name="LEYENDA GEOLOGICA", query=query_view, mxd=mxd)
    aut.add_layer_with_new_datasource(lyr_pol, gpo_form.name, gpo_form.workspace, 'FILEGDB_WORKSPACE', df_name="LEYENDA GEOLOGICA", query=query_view, mxd=mxd)
    
    df_lg=arcpy.mapping.ListDataFrames(mxd,"*LEYENDA GEOLOGICA*")[0]
    layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(gpl_mg_celd.name), df_lg)

    if layers:
        df_lg.extent= layers[0].getExtent()
        layers[0].definitionQuery = query_view
        df_lg.scale = df_lg.scale *1.1
        arcpy.RefreshActiveView()
    
except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(8, response)
