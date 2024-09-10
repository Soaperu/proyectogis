import sys
reload(sys)
sys.setdefaultencoding("utf-8")
# Importar librerias
import arcpy
import json
import tempfile
import messages_aut as msg
import os
import settings_aut as st
import MPM_S03_mapa_potencial_minero as mpm
import traceback
import uuid
import PG_S01_mapa_peligros_geologicos as mpg

arcpy.env.overwriteOutput = True

response = dict()
response['status'] = 1
response['message'] = 'success'

def get_departament(layer_interes, layer_depa):
    """
    Obtiene el o los departamentos del mapa geologico <> 50000
    :param layer_interes: Capa de interes
    :param layer_depa: Capa de departamentos
    :return: Codigo del departamento
    """
    arcpy.SelectLayerByLocation_management(layer_depa, 'INTERSECT', layer_interes, '', 'NEW_SELECTION')
    if arcpy.GetCount_management(layer_depa).getOutput(0) == '1':
        cd_depa = arcpy.SearchCursor(layer_depa).next().getValue(st._CD_DEPA_FIELD)
        return [cd_depa]
        
    rows = [m[0] for m in  arcpy.da.SearchCursor(layer_depa, [st._CD_DEPA_FIELD])]
    if len(rows) == 0:
        return []
    return rows

def clip_area_interes(layer_interes, layer_interes2, field):
    """
    Recorta las capas de POG's y Litologia con el area de interes del usuario y devuelve la lista
     de codigos CODI unicos
    """
    lyr_clip= arcpy.Clip_analysis(layer_interes2, layer_interes, "in_memory\clip_main")
    lista_codi = list(set([codi[0] for codi in arcpy.da.SearchCursor(lyr_clip,[field])]))
    return lista_codi


if __name__=='__main__':
    try:
        # Insertar procesos 
        _ELM_MAP_TITLE = 'ELM_TITULO'
        _ELM_AUTHOR = 'ELM_AUTOR'
        _ELM_REVIEWER = 'ELM_REVISOR'
        _ELM_MAP_CODE = 'ELM_CODIGO'
        _ELM_SCALEBAR = 'ELM_BARRAESCALA'
        _NAME_LAYER_INTERES = 'gpo_mgno50k_area_interes'
        _NAME_LAYER_HOJAS = 'GPO_DG_HOJAS_50K'
        _NAME_LAYER_DEPARTAMENTO = 'GPO_DEP_DEPARTAMENTOS'
        _NAME_LAYER_DEPARTAMENTO_AREA = 'GPO_DEP_DEPARTAMENTO'
        _NAME_LAYER_LITOLOGIA = 'GPO_DGR_ULITO'
        _NAME_LAYER_POG = 'GPT_DGR_POG'
        
        feature = arcpy.GetParameterAsText(0)
        map_title = arcpy.GetParameterAsText(1)
        author = arcpy.GetParameterAsText(2)
        document = arcpy.GetParameterAsText(3)
        scale = int(arcpy.GetParameterAsText(4))
        orientation = arcpy.GetParameterAsText(5)
        map_size = arcpy.GetParameterAsText(6)

        temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()

        desc = arcpy.Describe(feature)
        layer_extent = desc.extent
        epsg = desc.spatialReference.factoryCode

        mxd_name = 'T01_MGNO50K_{}_{}.mxd'.format(orientation, map_size)
        mxd_path = os.path.join(st._MXD_DIR, mxd_name.upper())

        mxd = arcpy.mapping.MapDocument(mxd_path)
        df_principal, df_ubicacion, df_pg, df_lg = arcpy.mapping.ListDataFrames(mxd)
        lyrs_departamento = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO, df_principal)
        lyrs_interes = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_INTERES, df_principal)
        lyrs_interes2 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_HOJAS, df_principal)
        lyrs_interes3 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_LITOLOGIA, df_principal)
        lyrs_interes4 = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_POG, df_principal)
        df_principal.extent = layer_extent
        df_principal.scale = df_principal.scale * 1.15
        lyrs_departamento_area = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO_AREA, df_ubicacion)

        shape_dir = os.path.dirname(feature)
        shape_name =  os.path.basename(feature)
        shape_name_without_ext = shape_name.split('.')[0]
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()

        for i in lyrs_interes:
            i.replaceDataSource(shape_dir, "SHAPEFILE_WORKSPACE", shape_name_without_ext, False)
            i.visible = True

        for i in lyrs_interes2:
            hojas=st._CUADRICULAS_MG_PATH
            dataset_hojas = hojas.split("\\")[-2]
            gdb_hojas= hojas.split(".gdb")[0]+".gdb"
            i.replaceDataSource(gdb_hojas, "FILEGDB_WORKSPACE", dataset_hojas + "//" + _NAME_LAYER_HOJAS, False)
            i.visible = True
            
        arcpy.SelectLayerByLocation_management(lyrs_interes2[0], "INTERSECT", lyrs_interes[0],"", "NEW_SELECTION", "NOT_INVERT")
        lista_hojas = [x[0] for x in arcpy.da.SearchCursor(lyrs_interes2[0],[st._CODHOJA_FIELD])]
        arcpy.SelectLayerByAttribute_management(lyrs_interes2[0], "CLEAR_SELECTION")
        
        query_string = "{} IN ('{}')".format(st._CODHOJA_FIELD, "','".join(lista_hojas))
        response['query']= ','.join(lista_hojas)
        lyrs_interes2[0].definitionQuery = query_string
        lyrs_interes3[0].definitionQuery = query_string
        lyrs_interes4[0].definitionQuery = query_string

        #Determinar departamento
        cd_depas = get_departament(lyrs_interes[0], lyrs_departamento[0])
        if cd_depas == []:
            raise RuntimeError(msg._MPM_ERROR_OUT_PERU)
        arcpy.SelectLayerByAttribute_management(lyrs_departamento[0], "CLEAR_SELECTION")

        lyrs_departamento_area[0].definitionQuery = "{} IN ({})".format(st._CD_DEPA_FIELD, ", ".join(["'{}'".format(cd_depa) for cd_depa in cd_depas]))

        # Modificacion de elementos texto de membrete
        text_elements = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")

        for elm in text_elements:
            if elm.name == _ELM_MAP_TITLE:
                elm.text = map_title 
            elif elm.name == _ELM_AUTHOR:
                elm.text = author
            elif elm.name == _ELM_MAP_CODE:
                elm.text = document
            elif elm.name == _ELM_REVIEWER:
                elm.text = author

        # Habilitando la visualizacion de capas
        # for layer in arcpy.mapping.ListLayers(mxd, '*'):
        #     if layer.supports("VISIBLE"):
        #         if not layer.visible:
        #             layer.visible = True
        
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()
        output_dir_mxd = os.path.join(temp_folder, document)
        if os.path.exists(output_dir_mxd):
            import shutil
            shutil.rmtree(output_dir_mxd)
        os.mkdir(output_dir_mxd)
        name_out = r'{}_{}.mxd'.format(document, uuid.uuid4().hex)
        response['mxd'] = os.path.join(output_dir_mxd, name_out)
        mxd.saveACopy(response['mxd'])
        #del mxd

        response['scale_params'] = mpg.set_scale_bar(scale)
        response['scale_params']['name_scale'] = _ELM_SCALEBAR

    except Exception as e:
        response['status'] = 0
        response['message'] = traceback.format_exc()
    finally:
        arcpy.AddMessage(response)
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(7, response)
