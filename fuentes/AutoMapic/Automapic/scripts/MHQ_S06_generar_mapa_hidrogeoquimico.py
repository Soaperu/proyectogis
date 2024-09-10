# Importar librerias
import arcpy
import json
import settings_aut as st
import messages_aut as msg
import packages_aut as pkg
import uuid
import os
# import MHQ_S05_gibbs_diagrama as gibbs_aut
# import MHQ_S04_piper_diagrama as piper_aut
import traceback
import math
from PG_S01_mapa_peligros_geologicos import get_scale
from MHG_S07_cortar_features_por_cuencas import split_data_by_polygon
import arcobjects as arc

response = dict()
response['status'] = 1
response['message'] = 'success'

shape_subc = arcpy.GetParameterAsText(0)
shape_micro = arcpy.GetParameterAsText(1)
cuenca = arcpy.GetParameterAsText(2)
subcuenca = arcpy.GetParameterAsText(3)
microcuenca = arcpy.GetParameterAsText(4)
zona = arcpy.GetParameterAsText(5)


_DFMAPAPRINCIPAL = "MAPA PRINCIPAL"
_DFMAUBICACION = "DFMAPAUBICACION"
_DFMAUBICACION2 = "DFMAPAUBICACION2"
_ZONA_ESTUDIO = "ZONA_ESTUDIO"
_ELM_PROYECTO = "ELM_PROYECTO"
_ELM_TITLE = "ELM_TITLE"
_ELM_TITLE_ORIG = "ELM_TITLE_ORIG"
_ELM_AUTOR = "ELM_AUTOR"
_ELM_AUTOR_ORIG = "ELM_AUTOR_ORIG"
_ELM_DATUM = "ELM_DATUM"
_ELM_DATUM_ORIG = "ELM_DATUM_ORIG"
_ELM_TABLA_FALLAS = "ELM_TABLA_FALLAS"
_ELM_IMG = "ELM_IMG_"
_distancia_100k = 54196.4486
escala_mapa =0


def getscale_geom(geomobject):
    extent = geomobject.extent
    distancia =((extent.YMax - extent.YMin)**2 +(extent.XMax - extent.XMin)**2)**0.5
    distancia = distancia*1.25
    escalareal = (distancia/ _distancia_100k)* (10**5)
    escala_grilla = 100 if escalareal/1000<=100 else 200
    return [escalareal, escala_grilla]

def actualizar_escalas():
    zona_estudio = st._PO_SUBCUENCAS
    sr = arcpy.SpatialReference(32718)
    # geometria = [x[0] for x in arcpy.da.SearchCursor(shape,["SHAPE@"], spatial_reference = sr)][0]
    # escala, escala_grilla = getscale_geom(geometria)

    with arcpy.da.UpdateCursor(zona_estudio,["SHAPE@","escala_","escala_mapa"], spatial_reference = sr) as cursor:
        for i in cursor:
            escala, escala_grilla = getscale_geom(i[0])
            i[1] = escala
            i[2] = escala_grilla
            cursor.updateRow(i)

def validar_shapes(shape_subcuenca, shape_microcuenca, subcuenca, microcuenca):
    lista_excepciones = []
    arcpy.AddMessage(shape_subcuenca)
    base_subcuencas = st._PO_SUBCUENCAS
    base_microcuencas = st._PO_MICROCUENCAS
    if not shape_subcuenca or shape_subcuenca=='':
        query =  "COD_SUBC = '{}'".format(subcuenca)
        subcuencas = [x[0] for x in arcpy.da.SearchCursor(base_subcuencas,["COD_SUBC"], where_clause =query)]
        if len(subcuencas)==0:
            lista_excepciones.append(msg._ERROR_FEATURE_SUBCUENCA_MHQ)
    
    if not shape_microcuenca or shape_microcuenca=='':
        query =  "COD_MICROC = '{}'".format(microcuenca)
        microcuencas = [x[0] for x in arcpy.da.SearchCursor(base_microcuencas,["COD_SUBC"], where_clause =query)]
        if len(microcuencas)==0:
            lista_excepciones.append(msg._ERROR_FEATURE_MICROCUENCA_MHQ)
    
    if len(lista_excepciones) >0:
        exception_msg = '\n'.join(lista_excepciones)
        raise Exception(exception_msg)



# def agregar_graficos(cuenca, subcuenca, microcuenca, mxd):

#     scratch = arcpy.env.scratchFolder
#     path_csv = os.path.join(scratch, st._CSV_GRAFICOS)

#     img_piper1 = piper_aut.diagrama_piper(path_csv, 'Estiaje', cuenca, subcuenca, microcuenca)
#     img_piper2 = piper_aut.diagrama_piper(path_csv, 'Avenida', cuenca, subcuenca, microcuenca)

#     img_gibbs1 = gibbs_aut.gibbs(path_csv, 'Estiaje', u'Cl', cuenca, subcuenca, microcuenca)
#     img_gibbs2 = gibbs_aut.gibbs(path_csv, 'Avenida', u'Cl', cuenca, subcuenca, microcuenca)

#     img_gibbs3 = gibbs_aut.gibbs(path_csv, 'Estiaje', u'Na', cuenca, subcuenca, microcuenca)
#     img_gibbs4 = gibbs_aut.gibbs(path_csv, 'Avenida', u'Na', cuenca, subcuenca, microcuenca)

#     # lista_imagenes = [img_piper1, img_piper2, img_gibbs1, img_gibbs2, img_gibbs3, img_gibbs4]
#     # return lista_imagenes

#     elm_piper_1 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*piper_estiaje*')[0]
#     elm_piper_2 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*piper_avenida*')[0]
#     elm_gibbs_1 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_estiaje_cl*')[0]
#     elm_gibbs_2 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_avenida_cl*')[0]
#     elm_gibbs_3 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_estiaje_na*')[0]
#     elm_gibbs_4 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_avenida_na*')[0]

#     elm_piper_1.sourceImage = img_piper1
#     elm_piper_2.sourceImage = img_piper2
#     elm_gibbs_1.sourceImage = img_gibbs1
#     elm_gibbs_2.sourceImage = img_gibbs2
#     elm_gibbs_3.sourceImage = img_gibbs3
#     elm_gibbs_4.sourceImage = img_gibbs4

def agregar_shape_micro(shape, subcuenca, microcuenca):
    if arcpy.Exists(shape):
        gdb = st._GDB_PATH_MHQ
        zona_estudio = st._PO_MICROCUENCAS

        if not arcpy.Exists(zona_estudio):
            raise RuntimeError(msg._ERROR_GDB_CONFIG_MHQ)

        geometria = [x[0] for x in arcpy.da.SearchCursor(shape,["SHAPE@"])][0]

        with arcpy.da.UpdateCursor(zona_estudio,["COD_SUBC","COD_MICROC"]) as cursor:
            for i in cursor:
                if i[0] == subcuenca and i[1]== microcuenca:
                    cursor.deleteRow()

        insertcursor = arcpy.da.InsertCursor(zona_estudio, ["SHAPE@", "COD_SUBC", "COD_MICROC"])
        row = (geometria, subcuenca, microcuenca)
        insertcursor.insertRow(row)
        del insertcursor

def agregar_shape_subcuenca(shape, subcuenca):
    if arcpy.Exists(shape):
        gdb = st._GDB_PATH_MHQ
        zona_estudio = st._PO_SUBCUENCAS

        if not arcpy.Exists(zona_estudio):
            raise RuntimeError(msg._ERROR_GDB_CONFIG_MHQ)

        geometria = [x[0] for x in arcpy.da.SearchCursor(shape,["SHAPE@"])][0]

        with arcpy.da.UpdateCursor(zona_estudio,["COD_SUBC"]) as cursor:
            for i in cursor:
                if i[0] == subcuenca :
                    cursor.deleteRow()

        insertcursor = arcpy.da.InsertCursor(zona_estudio, ["SHAPE@", "COD_SUBC"])
        row = (geometria, subcuenca)
        insertcursor.insertRow(row)
        del insertcursor

def generarmapa(subcuenca, microcuenca, zona):
    global escala_mapa
    query_subcuenca = "COD_SUBC = '{}'".format(subcuenca)


    escala_mapa = [x[1] for x in arcpy.da.SearchCursor(st._PO_SUBCUENCAS,["COD_SUBC","escala_mapa"], where_clause = query_subcuenca)][0]

    mxd_path = os.path.join(st._MXD_DIR, st._MXD_MHQ.format(zona, str(int(escala_mapa))))

    # Cargar el mxd
    mxd = arcpy.mapping.MapDocument(mxd_path)

    #########################################################
    # DataFrame activo _DFMAPAPRINCIPAL
    df_mapa_principal = arcpy.mapping.ListDataFrames(mxd, '*{}*'.format(_DFMAPAPRINCIPAL))[0] 

    # Configurar datasource de capas filtrables

    capital_distrital = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PT_CAPITAL_DISTRITAL), df_mapa_principal)[0]
    yacimientos_mineros = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PT_01_YACIMIENTOS_MINEROS), df_mapa_principal)[0]
    pam_mineros = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PT_02_PASIVOS_AMBIENTALES_MINEROS), df_mapa_principal)[0]
    vias_vecinales = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_Vias_PL_03_VIAS_VECINALES), df_mapa_principal)[0]
    vias_distritales = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_Vias_PL_04_VIAS_DISTRITALES), df_mapa_principal)[0]
    vias_nacionales = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_Vias_PL_05_VIAS_NACIONALES), df_mapa_principal)[0]
    laguna = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_LAGUNA), df_mapa_principal)[0]
    bofedal = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_BOFEDAL), df_mapa_principal)[0]
    centro_urbano = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_CENTRO_URBANO), df_mapa_principal)[0]
    zona_estudio = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_ZONA_ESTUDIO), df_mapa_principal)[0]
    po_microcuencas = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_MICROCUENCAS), df_mapa_principal)[0]
    curvas_nivel = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PL_curvas_nivel_47C), df_mapa_principal)[0]
    buzamiento = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PL_BUZAMIENTO), df_mapa_principal)[0]
    pliegues = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PL_PLIEGUES), df_mapa_principal)[0]
    fallas = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PL_FALLAS), df_mapa_principal)[0]
    geologia = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_GEOLOGIA_47C), df_mapa_principal)[0]
    estaciones = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_ESTACIONES), df_mapa_principal)[0]
    hidrotipos = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_HIDROTIPOS), df_mapa_principal)[0]


    capital_distrital.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PT_CAPITAL_DISTRITAL, False)
    yacimientos_mineros.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PT_01_YACIMIENTOS_MINEROS, False)
    pam_mineros.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PT_02_PASIVOS_AMBIENTALES_MINEROS, False)
    vias_vecinales.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_Vias_PL_03_VIAS_VECINALES, False)
    vias_distritales.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_Vias_PL_04_VIAS_DISTRITALES, False)
    vias_nacionales.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_Vias_PL_05_VIAS_NACIONALES, False)
    laguna.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_LAGUNA, False)
    bofedal.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_BOFEDAL, False)
    centro_urbano.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_CENTRO_URBANO, False)
    zona_estudio.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_ZONA_ESTUDIO, False)
    po_microcuencas.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_MICROCUENCAS, False)
    curvas_nivel.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PL_curvas_nivel_47C, False)
    buzamiento.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PL_BUZAMIENTO, False)
    pliegues.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PL_PLIEGUES, False)
    fallas.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PL_FALLAS, False)
    geologia.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_GEOLOGIA_47C, False)
    estaciones.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_ESTACIONES, False)
    hidrotipos.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_HIDROTIPOS, False)

    
    zona_estudio.definitionQuery = query_subcuenca
    df_mapa_principal.extent = zona_estudio.getExtent()
    df_mapa_principal.scale = get_scale(df_mapa_principal.scale)
    escala_mapa = 100 if df_mapa_principal.scale/1000<=100 else 200
    arcpy.RefreshActiveView()

    query_features = "PARENT = 7 AND QUERY = 0"
    df_layers = pkg.get_layers_selected(query_features, as_dataframe=True)

    for i, r in df_layers.iterrows():
        split_data_by_polygon(r, zona_estudio, mxd.activeDataFrame.name, mxd=mxd)

    arcpy.RefreshActiveView()
    arcpy.RefreshTOC()



    # fallas.definitionQuery = query_departamento
    # desidad_poblacional.definitionQuery = query_departamento
    # centrales_hidroelectricas_proy.definitionQuery = query_departamento
    # centrales_hidroelectricas_ejec.definitionQuery = query_departamento
    # gaseoducto_construccion.definitionQuery = query_departamento
    # telecomunicaciones.definitionQuery = query_departamento
    # mecanismos_focales.definitionQuery = query_departamento
    ##############################################################

    #########################################################
    # Dataframe del mapa de ubicacion
    df_mapa_ubicacion = arcpy.mapping.ListDataFrames(mxd, '*{}*'.format(_DFMAUBICACION))[0] 

    po_subcuencas = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_SUBCUENCAS), df_mapa_ubicacion)[0]
    po_subcuencas.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_SUBCUENCAS, False)


    #########################################################
    # Dataframe del mapa de ubicacion2
    df_mapa_ubicacion2 = arcpy.mapping.ListDataFrames(mxd, '*{}*'.format(_DFMAUBICACION2))[0] 

    lyr_zona_estudio = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_ZONA_ESTUDIO), df_mapa_ubicacion2)[0]
    po_subcuencas = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_PO_SUBCUENCAS), df_mapa_ubicacion2)[0]

    lyr_zona_estudio.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_ZONA_ESTUDIO, False)
    po_subcuencas.replaceDataSource(st._GDB_PATH_MHQ, "FILEGDB_WORKSPACE", st._LAYER_PO_SUBCUENCAS, False)

    lyr_zona_estudio.definitionQuery = query_subcuenca
    df_mapa_ubicacion2.extent = po_subcuencas.getExtent()
    arcpy.AddMessage(df_mapa_ubicacion2.scale)
    df_mapa_ubicacion2.scale = get_scale(df_mapa_ubicacion2.scale)
    arcpy.RefreshActiveView()

    # elementos del rotulo
    elm_title = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT",_ELM_TITLE)[0]

    subcuencanames = {x[0]:x[1] for x in arcpy.da.SearchCursor(st._BASE_EXCEL_LAB_FC, ["COD_SUBC", "SUBCUENCA"])}
    elm_title.text = elm_title.text.format(subcuencanames.get(subcuenca)).upper()

    

    return mxd



try:
    validar_shapes(shape_subc, shape_micro, subcuenca, microcuenca)
    agregar_shape_subcuenca(shape_subc, subcuenca)
    agregar_shape_micro(shape_micro, subcuenca, microcuenca) 
    actualizar_escalas()
    mxd = generarmapa(subcuenca,microcuenca, zona)
    # agregar_graficos(cuenca, subcuenca, microcuenca, mxd)
    response["response"]="response"

    response['response'] = os.path.join(st._TEMP_FOLDER, 'response_{}.mxd'.format(uuid.uuid4().hex))
    response['response'] = response['response'].replace('\\\\', '\\')
    mxd.saveACopy(response['response'])
    del mxd

    # Seleccionar la grilla del mapa principal
    # arc.select_grid_by_name(response['response'], '327{}_{}k'.format(zona, str(escala_mapa)), exclude_grids=['4326'])
except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()
    # response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(6, response)
