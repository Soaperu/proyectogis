import sys
reload(sys)
sys.setdefaultencoding("utf-8")

import arcpy
import settings_aut as st
import messages_aut as msg
import arcobjects as arc
import os
from bisect import bisect
import uuid
import json

arcpy.env.overwriteOutput = True

_DF_MAPAPRINCIPAL = 'DFMAPAPRINCIPAL'
_PT_LAYER_INTERES = 'PT_area_interes_{}'
_PL_LAYER_INTERES = 'PL_area_interes_{}'
_PO_LAYER_INTERES = 'PO_area_interes_{}'

_TITULO_MAPA_TEXT_ELEMENT = 'TITULOMAPA'
_AUTOR_TEXT_ELEMENT = 'AUTOR'
_NUMEROMAPA_TEXT_ELEMENT = 'NUMEROMAPA'
_BARRA_ESCALA = 'BARRAESCALA'
_DEPARTAMENTO_TEXT_ELEMENT = 'REGION'
_PROVINCIA_TEXT_ELEMENT = 'PROVINCIA'
_DISTRITO_TEXT_ELEMENT = 'DISTRITO'
_FIGURA_PUPUP_TEXT_ELEMENT = 'FIGURAPOPUP'
_SECTOR_TEXT_ELEMENT = 'TEXTSECTOR'

# _PGDETALLE_TEXT_ELEMENT = 'PGDETALLE'
_ZCDETALLE_TEXT_ELEMENT = 'ZCDETALLE'
_SMMDETALLE_TEXT_ELEMENT = 'SMMDETALLE'
_SIEFDETALLE_TEXT_ELEMENT = 'SIEFDETALLE'

_PGLEYENDA_TEXT_ELEMENT = 'PGLEYENDA'
_ZCLEYENDA_TEXT_ELEMENT = 'ZCLEYENDA'
_SMMLEYENDA_TEXT_ELEMENT = 'SMMLEYENDA'
_SIEFLEYENDA_TEXT_ELEMENT = 'SIEFLEYENDA'
_CPLEYENDA_TEXT_ELEMENT = 'CPLEYENDA'
_GMLEYENDA_TEXT_ELEMENT = 'GMLEYENDA'

_TAGDEPA_TEXT_ELEMENT = 'TAGDEPA'
_TAGPROV_TEXT_ELEMENT = 'TAGPROV'
_TAGDIST_TEXT_ELEMENT = 'TAGDIST'

_FIELD_CD_DIST = 'CD_DIST'
_FIELD_CD_PROV = 'CD_PROV'
_FIELD_CD_DEPA = 'CD_DEPA'
_FIELD_NM_DIST = 'NM_DIST'
_FIELD_NM_PROV = 'NM_PROV'
_FIELD_NM_DEPA = 'NM_DEPA'

_OPTION_PG = 'PG'
_OPTION_ZC = 'ZC'
_OPTION_SMM = 'SMM'
_OPTION_SIEF = 'SIEF'
_OPTION_CP = 'CP'
_OPTION_GM = 'GM'


def get_orientation():
    pass

def get_scale(scale):
    range_a = range(0, 1001, 500)
    range_b = range(5000, 100000, 5000)
    range_c = range(100000, 500000, 50000)
    range_d = range(500000, 10000000, 100000)
    scales = range_a + range_b + range_c + range_d
    idx = bisect(scales, scale)
    response = scales[idx]
    return response


def set_scale_bar(scale):
    _UNITS_KM = 10
    _UNITS_M = 9
    response = dict()
    scale_ratio = scale/100
    response['Division'] = scale_ratio/1000.0 if scale >= 100000 else scale_ratio
    response['UnitLabel'] = 'Kilometros' if scale >= 100000 else 'Metros'
    response['Units'] = _UNITS_KM if scale >= 100000 else _UNITS_M
    return response

def set_PG(mxd):
    return mxd

def set_ZC(mxd):
    return mxd

def set_SIEF(mxd):
    return mxd

def set_SSM(mxd):
    return mxd


def set_detalle(text, max_character_by_line, first_line=False, sep='\n'):
    if len(text) <= max_character_by_line:
        return text
    nlist = list()
    textList = text.split(' ')
    ini, end, tx = 0, 0, str()
    for i, t in enumerate(textList, 1):
        tx = ' '.join(textList[ini:i])
        if len(tx) > max_character_by_line:
            end = i - 1
            tx = ' '.join(textList[ini:end])
            nlist.append(tx)
            ini = end
    nlist.append(' '.join(textList[end:]))
    if first_line:
        return nlist[0] + sep + ' '.join(nlist[1:])
    return sep.join(nlist)

def generate_map():
    global titulo, autor, escala, numero, detalle
    response = dict()
    id_process = uuid.uuid4().get_hex()
    desc = arcpy.Describe(feature)
    wkid = desc.spatialReference.factoryCode

    if not wkid:
        raise RuntimeError(msg._ERROR_NOT_PROJECTION)

    if wkid == st._EPSG_W17S:
        mxd_path = st._MXD_PG_17
    elif wkid == st._EPSG_W18S:
        mxd_path = st._MXD_PG_18
    elif wkid == st._EPSG_W19S:
        mxd_path = st._MXD_PG_19
    else:
        raise RuntimeError(msg._ERROR_PROJECTION_NO_VALID)
    
    shapeType = desc.shapetype.lower()

    zona = str(wkid)[3:]

    if shapeType == 'point':
        _NAME_LAYER_INTERES = _PT_LAYER_INTERES
    elif shapeType == 'polyline':
        _NAME_LAYER_INTERES = _PL_LAYER_INTERES
    elif shapeType == 'polygon':
        _NAME_LAYER_INTERES = _PO_LAYER_INTERES

    _NAME_LAYER_INTERES = _NAME_LAYER_INTERES.format(zona)

    mxd = arcpy.mapping.MapDocument(mxd_path)
    df_principal, df_ubicacion = arcpy.mapping.ListDataFrames(mxd)
    # df_ubicacion = arcpy.mapping.ListDataFrames(mxd, '{}'.format(_DF_MAPAPRINCIPAL))[1]

    lyrs_interes = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_INTERES), df_principal)
    # lyr_interes = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_INTERES), df_principal)[0]

    shape_dir = os.path.dirname(feature)
    shape_name =  os.path.basename(feature)
    shape_name_without_ext = shape_name.split('.')[0]

    for i in lyrs_interes:
        i.replaceDataSource(shape_dir, "SHAPEFILE_WORKSPACE", shape_name_without_ext, False)
        i.visible = True

    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()

    coords = [xmin, ymin, xmax, ymax]
    coords = filter(lambda i: i, coords)

    ext = arcpy.Extent(*coords) if len(coords) == 4 else lyrs_interes[0].getExtent()
    # lyr_interes.visible = True

    response['extent'] = json.loads(ext.JSON)

    df_principal.extent = ext

    if not escala:
        escala = get_scale(df_principal.scale)
    df_principal.scale = escala

    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()

    area = arcpy.FromWKT(df_principal.extent.polygon.WKT, arcpy.SpatialReference(wkid))
    # area = arcpy.CopyFeatures_management(area, os.path.join(st._TEMP_FOLDER, 'area.shp'))
    # area = arcpy.MakeFeatureLayer_management(area, 'area')

    _NAME_LAYER_DEPARTAMENTOS = 'departamentos'
    _NAME_LAYER_PROVINCIAS = 'provincias'
    _NAME_LAYER_PROVINCIA = 'provincia'
    _NAME_LAYER_DISTRITOS = 'distritos'

    _NAME_LAYER_PG = 't_peligros_geologicos'
    _TITLE_PG = 'inventario de peligros geol\xc3\x93gicos'

    _NAME_LAYER_ZC = 't_zonas_criticas'
    _TITLE_ZC = 'zonas cr\xc3\x8dticas por peligros geol\xc3\x93gicos'

    _NAME_LAYER_SMM = 't_susc_movimientos_masa'
    _TITLE_SMM = 'susceptibilidad a movimientos en masa'

    _NAME_LAYER_SIEF = 't_susc_inundacion_erosion'
    _TITLE_SIEF = 'susceptibilidad a inundaciones'

    _NAME_LAYER_CP = 't_cartografia_peligros'
    _TITLE_CP = 'cartografia de peligros'

    _NAME_LAYER_GM = 't_geomorfologia'
    _TITLE_GM = 'Geomorfolog\xc3\x8da'

    # lyr_departamentos = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_DEPARTAMENTOS), df_principal)[0]
    lyr_provincias = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_PROVINCIAS), df_principal)[0]
    lyr_distritos = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_DISTRITOS), df_principal)[0]
    # arcpy.AddMessage(lyr_distritos.name)
    lyr_provincia = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_PROVINCIA), df_ubicacion)[0]

    # Establecer las etiquetas de departamentos, porvincias y distritos que se intersectan con el area de interes

    arcpy.SelectLayerByLocation_management(lyr_distritos, "INTERSECT", lyrs_interes[0], "#", "NEW_SELECTION")
    cursor_dem = arcpy.da.SearchCursor(lyr_distritos, [_FIELD_CD_DIST, _FIELD_CD_PROV, _FIELD_CD_DEPA, _FIELD_NM_DIST , _FIELD_NM_PROV, _FIELD_NM_DEPA])

    # distritos = map(lambda i: i[0], cursor_dem)
    demarcacion = [{
            'cd_depa': i[2],
            'nm_depa': i[5],
            'cd_prov': i[1],
            'nm_prov': i[4],
            'cd_dist': i[0],
            'nm_dist': i[3],
        } for i in cursor_dem]
    demarcacion.sort(key=lambda i: i['cd_dist'])

    ARR_DEPA = list(set([i['cd_depa'] for i in demarcacion]))
    ARR_DEPA_NM = list(set([i['nm_depa'] for i in demarcacion]))

    STR_DEPA = '/ '.join(ARR_DEPA_NM)
    ARR_PROV = list()
    ARR_DIST = list()
    for dep in ARR_DEPA:
        provs = [i['nm_prov'] for i in demarcacion if i['cd_depa'] == dep]
        provs_str = ', '.join(list(set(provs)))
        ARR_PROV.append(provs_str)
        dist = [i['nm_dist'] for i in demarcacion if i['cd_depa'] == dep]
        dist_str = ', '.join(list(set(dist)))
        ARR_DIST.append(dist_str)
    
    STR_PROV = '/ '.join(ARR_PROV)
    STR_DIST = '/ '.join(ARR_DIST)

    
    response['demarcation'] = [[i['cd_dist'], i['nm_depa']] for i in demarcacion]

    # Departamentos, provincias y distritos que se intersectan con el area total del mapa
    arcpy.SelectLayerByLocation_management(lyr_distritos, "INTERSECT", area, "#", "NEW_SELECTION")

    cursor = arcpy.da.SearchCursor(lyr_distritos, [_FIELD_CD_DIST, _FIELD_CD_PROV, _FIELD_CD_DEPA, _FIELD_NM_DIST , _FIELD_NM_PROV, _FIELD_NM_DEPA, 'SHAPE@'])

    # data = map(
    #     lambda i: {
    #         'cd_depa': i[2],
    #         'nm_depa': i[5],
    #         'cd_prov': i[1],
    #         'nm_prov': i[4],
    #         'cd_dist': i[0],
    #         'nm_dist': i[3],
    #     }, cursor)

    data = map(lambda i: i, cursor)

    # Querys a las capas
    arcpy.SelectLayerByAttribute_management(lyr_distritos, "CLEAR_SELECTION")

    query_distritos = "{} in ('{}')".format(_FIELD_CD_DIST, "', '".join(list(set([i[0] for i in data]))))
    lyr_distritos.definitionQuery += ' AND ' + query_distritos

    query_provincias = "{} in ('{}')".format(_FIELD_CD_PROV, "', '".join(list(set([i[1] for i in data]))))
    lyr_provincias.definitionQuery += ' AND ' + query_provincias

    if maptype == _OPTION_PG:
        lyr_pg = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_PG), df_principal)[0]
        # lyr_pg.definitionQuery = query_distritos
        lyr_pg.visible = True

    elif maptype == _OPTION_ZC:
        lyr_zc = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_ZC), df_principal)[0]
        # lyr_zc.definitionQuery = query_distritos
        lyr_zc.visible = True
    elif maptype == _OPTION_SMM:
        lyr_smm = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_SMM), df_principal)[0]
        # lyr_smm.definitionQuery = query_distritos
        lyr_smm.visible = True
    elif maptype == _OPTION_SIEF:
        lyr_sief = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_SIEF), df_principal)[0]
        # lyr_sief.definitionQuery = query_distritos
        lyr_sief.visible = True
    elif maptype == _OPTION_CP:
        lyr_cp = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_CP), df_principal)[0]
        # lyr_sief.definitionQuery = query_distritos
        lyr_cp.visible = True
    elif maptype == _OPTION_GM:
        lyr_gm = arcpy.mapping.ListLayers(mxd, '{}'.format(_NAME_LAYER_GM), df_principal)[0]
        # lyr_sief.definitionQuery = query_distritos
        lyr_gm.visible = True

    # query_departamentos = "{} in ('{}')".format(_FIELD_CD_DEPA, "', '".join(list(set([i[2] for i in data]))))
    # lyr_departamentos.definitionQuery = query_departamentos

    # metrica_selected = 0
    # idx = None
    # metrica = 0

    diss = arcpy.Dissolve_management(lyrs_interes[0], 'in_memory/dissolve')
    response['centroid'] = map(lambda coords: [coords[0], coords[1]], arcpy.da.SearchCursor(diss, ['SHAPE@X', 'SHAPE@Y']))
    response['zona'] = zona

    # for i, d in enumerate(data):
    #     if shapeType == 'point':
    #         arcpy.SelectLayerByLocation_management(lyrs_interes[0], "INTERSECT", d[-1], "#", "NEW_SELECTION", "NOT_INVERT")
    #         metrica = int(arcpy.GetCount_management(lyrs_interes[0]).getOutput(0))
    #         arcpy.SelectLayerByAttribute_management(lyrs_interes[0], "CLEAR_SELECTION")
    #     elif shapeType == 'polyline':
    #         clip = arcpy.Clip_analysis(diss, d[-1], 'in_memory/clip')
    #         rows = map(lambda m: m, arcpy.da.SearchCursor(clip, ['SHAPE@LENGTH']))
    #         if len(rows):
    #             metrica = rows[0]
    #     elif shapeType == 'polygon':
    #         clip = arcpy.Clip_analysis(diss, d[-1], 'in_memory/clip')
    #         rows = map(lambda m: m, arcpy.da.SearchCursor(clip, ['SHAPE@AREA']))
    #         if len(rows):
    #             metrica = rows[0]
            
    #     if metrica > metrica_selected:
    #         metrica_selected = metrica
    #         idx = i
    
    # ambito = data[idx]

    
    # cd_depa = ambito[2]
    # ubigeo = ambito[0]
    # nm_depa = ambito[5]
    # nm_prov = ambito[4]
    # nm_dist = ambito[3]
    

    # lyr_provincia.definitionQuery = "{} = '{}'".format(_FIELD_CD_PROV, ambito[1])
    lyr_provincia.definitionQuery = query_provincias

    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()

    if not titulo:
        titulo = 'Lorem Ipsum'
    if not autor:
        autor = 'Automapic'
    if not numero:
        numero = '1'

    mxd.title = titulo
    mxd.author = autor

    text_elements = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")

    prx = str()
    name_detalle = 'none'
    name_leyenda = str()
    len_text = 69
    if _OPTION_PG == maptype:
        prx = _TITLE_PG
        # name_detalle = _PGDETALLE_TEXT_ELEMENT
        name_leyenda = _PGLEYENDA_TEXT_ELEMENT
    elif _OPTION_ZC == maptype:
        prx = _TITLE_ZC
        # name_detalle = _ZCDETALLE_TEXT_ELEMENT
        name_leyenda = _ZCLEYENDA_TEXT_ELEMENT
        len_text = 51
    elif _OPTION_SMM == maptype:
        prx = _TITLE_SMM
        name_detalle = _SMMDETALLE_TEXT_ELEMENT
        name_leyenda = _SMMLEYENDA_TEXT_ELEMENT
    elif _OPTION_SIEF== maptype:
        prx = _TITLE_SIEF
        name_detalle = _SIEFDETALLE_TEXT_ELEMENT
        name_leyenda = _SIEFLEYENDA_TEXT_ELEMENT
    elif _OPTION_CP== maptype:
        prx = _TITLE_CP
        name_leyenda = _CPLEYENDA_TEXT_ELEMENT
    elif _OPTION_GM == maptype:
        prx = _TITLE_GM
        name_leyenda = _GMLEYENDA_TEXT_ELEMENT

    for elm in text_elements:
        if elm.name == _TITULO_MAPA_TEXT_ELEMENT:
            elm.text =  prx.upper() + '\n' + titulo.upper()
            elm.elementPositionY = 2.6   
        elif elm.name == _FIGURA_PUPUP_TEXT_ELEMENT:
            elm.text = set_detalle(titulo, 20).title()
        elif elm.name == _NUMEROMAPA_TEXT_ELEMENT:
            elm.text = numero.zfill(2)
        elif elm.name == _DEPARTAMENTO_TEXT_ELEMENT:
            elm.text += STR_DEPA.upper()
        elif elm.name == _PROVINCIA_TEXT_ELEMENT:
            elm.text += STR_PROV.upper()
        elif elm.name == _DISTRITO_TEXT_ELEMENT:
            elm.text += STR_DIST.upper()
        elif elm.name == name_detalle:
            if detalle:
                # if maptype == _OPTION_ZC:
                #     elm.text += '\n' + set_detalle(detalle, len_text)
                #     continue
                elm.text = set_detalle(detalle, len_text)
        elif elm.name == _TAGDEPA_TEXT_ELEMENT:
            elm.text += STR_DEPA.upper()
        elif elm.name == _TAGPROV_TEXT_ELEMENT:
            elm.text += STR_PROV.upper()
        elif elm.name == _TAGDIST_TEXT_ELEMENT:
            elm.text += STR_DIST.upper()
        elif elm.name == _SECTOR_TEXT_ELEMENT:
            elm.text = set_detalle(titulo.upper(), 20)
    
    # arcpy.AddMessage(name_leyenda)
    legend_element = arcpy.mapping.ListLayoutElements(mxd, "", name_leyenda)[0]
    # for i in legends_element:
    #     arcpy.AddMessage(i.name)
    #     if i.name == name_leyenda:
    legend_element.elementPositionX = 25.65
    legend_element.elementPositionY = 7.7

    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()

    for layer in arcpy.mapping.ListLayers(mxd, '*', df_principal):
        if layer.name.lower() == 'basemap':
            continue
        if (layer.isBroken) or ((layer.visible == False) and (layer.name.startswith('t_'))):
            arcpy.mapping.RemoveLayer(df_principal, layer)
            continue
        if layer.supports("VISIBLE"):
            try:
                # arcpy.AddMessage(layer.name)
                layer.visible = True
            except:
                pass

    for layer in arcpy.mapping.ListLayers(mxd, '*', df_ubicacion):
        if layer.isBroken:
            arcpy.mapping.RemoveLayer(df_ubicacion, layer)

    
    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()

    output_dir_mxd = os.path.join(st._TEMP_FOLDER, id_process)
    os.mkdir(output_dir_mxd)
    name = titulo.replace(' ', '')
    name_out = '{}.mxd'.format(name)
    response['mxd'] = os.path.join(output_dir_mxd, name_out)
    mxd.saveACopy(response['mxd'])

    params = set_scale_bar(escala)
    arc.set_scale_properties(response['mxd'], _BARRA_ESCALA, **params)
    arc.select_grid_by_scale(response['mxd'], escala)
    return response

if __name__ == '__main__':
    response = dict()
    response['status'] = 0
    feature = arcpy.GetParameterAsText(0)
    maptype = arcpy.GetParameterAsText(1)
    titulo = arcpy.GetParameterAsText(2)
    autor = arcpy.GetParameterAsText(3)
    escala = arcpy.GetParameter(4)
    numero = arcpy.GetParameterAsText(5)
    detalle = arcpy.GetParameterAsText(6)
    xmin = arcpy.GetParameter(7)
    ymin = arcpy.GetParameter(8)
    xmax = arcpy.GetParameter(9)
    ymax = arcpy.GetParameter(10)

    try:
        response['response'] = generate_map()
        response['status'] = 1
    except Exception as e:
        response['message'] = e.message
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(11, response)

