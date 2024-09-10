# Importar librerias
import arcpy
import json
import os
from settings_aut import _CD_DEPA_FIELD
import settings_aut as st
import messages_aut as msg
import arcobjects as arc
import PG_S01_mapa_peligros_geologicos as mpg
import uuid
import tempfile
import packages_aut_arc as pkga
import packages_aut_ora as pkgo
import packages_aut as pkg
from MHG_S07_cortar_features_por_cuencas import split_data_by_polygon
import pandas as pd

_ERROR_STATE = 0    # Cuando el proceso falla
_SUCCESS_STATE = 1  # Cuando el proceso se ejecuta sin fallos
_DELETE_STATE = 99  # Cuando se elimina un registro de la tabla de mapas de potencial minero
_CREATION_STATE = 9  # Cuando se crea un nuevo codigo pero aun no se genera el mapa

_TYPE_INFO = 1  # El mapa tiene informacion de interes
_TYPE_NO_INFO = 2   # El area no tiene informacion de interes


def create_map_code(tipo, titulo, autor, revisor, escala, orientacion, hoja, src, original_format, cd_depa, document):
    """
    Genera el codigo correlativo de cada mapa de potencial minero en la base de datos
    coorporativa de Ingemmet
    :param tipo: Tipo de mapa (0: mapa de prueba, 1: mapa de produccion)
    :param titulo: Titulo del mapa
    :param autor: Autor del mapa
    :param revisor: Revisor del mapa
    :param escala: Escala del mapa como un entero
    :param orientacion: Orientacion del mapa (h: horizontal, v: vertical)
    :param hoja: Tamanio de la hoja del mapa (a4, a3)
    :param src: Sistema de Referencia Espacial del mapa (epsg: 32717, 32718, 32719)
    :return: Codigo del mapa generado
    """
    from datetime import datetime
    last_code = pkga.get_last_code(getcursor=True)
    year = datetime.now().year
    last_year = int(last_code.split('-')[-2])
    if last_code == 'null' or last_year != int(str(year)[2:]):
        end_number = '001'
    else:
        end_number = last_code.split('-')[-1]
        end_number = str(int(end_number) + 1).zfill(3)
    last_code = 'MAF-SIGE-{}-{}'.format(str(year)[2:], end_number)
    pkga.insert_code(last_code, tipo, titulo, autor, revisor, escala, orientacion, hoja, src, original_format, cd_depa, document, iscommit=True)
    return last_code

def insert_geometry_to_database(feature, codigo):
    global temp_folder
    _FIELD_CD_MAPA = 'CD_MAPA'
    fields = map(lambda i: i.name.upper(), arcpy.ListFields(feature))
    if _FIELD_CD_MAPA in fields:
        arcpy.DeleteField_management(feature, _FIELD_CD_MAPA)
    arcpy.AddField_management(feature, _FIELD_CD_MAPA, 'TEXT', field_length=20)
    arcpy.CalculateField_management(feature, _FIELD_CD_MAPA, '\'' + codigo + '\'', 'PYTHON')
    feature_output = os.path.join(temp_folder, '{}.shp'.format(codigo))
    arcpy.Project_management(feature, feature_output, arcpy.SpatialReference(4326))
    arcpy.Append_management(feature_output, st._PT_DGAR_REGISTRO_MP_TEMP, 'NO_TEST')


def get_departament(layer_interes, layer_depa):
    """
    Obtiene el departamento del mapa de potencial minero
    :param layer_interes: Capa de interes
    :param layer_depa: Capa de departamentos
    :return: Codigo del departamento
    """
    arcpy.SelectLayerByLocation_management(layer_depa, 'INTERSECT', layer_interes, '', 'NEW_SELECTION')
    if arcpy.GetCount_management(layer_depa).getOutput(0) == '1':
        cd_depa = arcpy.SearchCursor(layer_depa).next().getValue(st._CD_DEPA_FIELD)
        return cd_depa
    diss = arcpy.Dissolve_management(layer_interes, 'in_memory/dissolve')
    clip = arcpy.Intersect_analysis([diss, layer_depa], 'in_memory/clip')
    rows = map(lambda m: [m[0], m[1]], arcpy.da.SearchCursor(clip, [st._CD_DEPA_FIELD, 'SHAPE@LENGTH']))
    if len(rows) == 0:
        return []
    rows.sort(key=lambda x: x[1], reverse=True)
    return rows[0][0]



if __name__ == '__main__':
    try:
    # Insertar procesos
        _ELM_MAP_TITLE = 'ELM_TITULO'
        _ELM_REVIEWER = 'ELM_REVISOR'
        _ELM_AUTHOR = 'ELM_AUTOR'
        _ELM_MAP_CODE = 'ELM_CODIGO'
        _ELM_SCALEBAR = 'ELM_BARRAESCALA'
        _NAME_LAYER_INTERES = 'gpo_mpm_area_interes'
        _NAME_LAYER_DEPARTAMENTO = 'GPO_DEP_DEPARTAMENTOS'
        _NAME_LAYER_DEPARTAMENTO_AREA = 'GPO_DEP_DEPARTAMENTO_AREA'

        response = dict()
        response['status'] = _SUCCESS_STATE
        response['message'] = 'success'

        feature = arcpy.GetParameterAsText(0)
        original_format = arcpy.GetParameterAsText(1)
        author = arcpy.GetParameterAsText(2)
        reviewer = arcpy.GetParameterAsText(3)
        map_title = arcpy.GetParameterAsText(4)
        document = arcpy.GetParameterAsText(5)
        scale = int(arcpy.GetParameterAsText(6))
        orientation = arcpy.GetParameterAsText(7)
        map_size = arcpy.GetParameterAsText(8)
        map_type = int(arcpy.GetParameterAsText(9))

        temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()

        desc = arcpy.Describe(feature)
        epsg = desc.spatialReference.factoryCode

        mxd_name = 'T01_MPM_{}_{}_{}.mxd'.format(epsg, orientation, map_size)
        mxd_path = os.path.join(st._MXD_DIR, mxd_name.upper())

        mxd = arcpy.mapping.MapDocument(mxd_path)

        df_principal, df_ubicacion = arcpy.mapping.ListDataFrames(mxd)
        lyrs_interes = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_INTERES, df_principal)
        lyrs_departamento = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO, df_principal)

        lyrs_departamento_area = arcpy.mapping.ListLayers(mxd, _NAME_LAYER_DEPARTAMENTO_AREA, df_ubicacion)

        shape_dir = os.path.dirname(feature)
        shape_name =  os.path.basename(feature)
        shape_name_without_ext = shape_name.split('.')[0]

        for i in lyrs_interes:
            i.replaceDataSource(shape_dir, "SHAPEFILE_WORKSPACE", shape_name_without_ext, False)
            i.visible = True

        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()

        # Determinar el departamento
        cd_depa = get_departament(lyrs_interes[0], lyrs_departamento[0])
        if cd_depa == []:
            raise RuntimeError(msg._MPM_ERROR_OUT_PERU)
        arcpy.SelectLayerByAttribute_management(lyrs_departamento[0], "CLEAR_SELECTION")

        lyrs_departamento_area[0].definitionQuery = "{} = '{}'".format(_CD_DEPA_FIELD, cd_depa)

        map_code = 'MXD_{}'.format(uuid.uuid4().hex)

        if map_type:
            map_code = create_map_code(map_type, map_title, author, reviewer, scale, orientation, map_size, epsg, original_format, cd_depa, document)

        if map_type != _TYPE_NO_INFO:

            text_elements = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")

            for elm in text_elements:
                if elm.name == _ELM_MAP_TITLE:
                    elm.text = map_title 
                elif elm.name == _ELM_REVIEWER:
                    elm.text = reviewer
                elif elm.name == _ELM_AUTHOR:
                    elm.text = author
                elif elm.name == _ELM_MAP_CODE:
                    elm.text = map_code if map_type else 'Preview'
            
            arcpy.RefreshTOC()
            arcpy.RefreshActiveView()


            ext = lyrs_interes[0].getExtent()
            df_principal.extent = ext

            if not scale:
                scale = mpg.get_scale(df_principal.scale)
            df_principal.scale = scale

            arcpy.RefreshTOC()
            arcpy.RefreshActiveView()

            df_layers = pkg.get_layers_by_parent(9, as_dataframe=True)

            area = arcpy.FromWKT(df_principal.extent.polygon.WKT, arcpy.SpatialReference(epsg))
            area = arcpy.CopyFeatures_management(area, os.path.join(temp_folder, 'area.shp'))
            area = arcpy.MakeFeatureLayer_management(area, 'area')

            # for i, r in df_layers.iterrows():
            #     split_data_by_polygon(r, area, mxd.activeDataFrame.name, mxd=mxd)

            arcpy.RefreshActiveView()
            arcpy.RefreshTOC()


            # Habilitando la visualizacion de capas
            for layer in arcpy.mapping.ListLayers(mxd, '*'):  
                if layer.supports("VISIBLE"):
                    if not layer.visible:
                        layer.visible = True

            arcpy.RefreshActiveView()
            arcpy.RefreshTOC()

            output_dir_mxd = os.path.join(temp_folder, map_code)
            if os.path.exists(output_dir_mxd):
                import shutil
                shutil.rmtree(output_dir_mxd)
            os.mkdir(output_dir_mxd)
            name_out = '{}.mxd'.format(map_code)
            response['mxd'] = os.path.join(output_dir_mxd, name_out)
            mxd.saveACopy(response['mxd'])

            del mxd
            
            response['scale_params'] = mpg.set_scale_bar(scale)
            response['scale_params']['name_scale'] = _ELM_SCALEBAR

        if map_type in (_TYPE_NO_INFO, _TYPE_INFO):
            insert_geometry_to_database(feature, map_code)
            pkga.update_state_row(map_code, response['status'], iscommit=True)

            if map_type == _TYPE_INFO:
                output_report_dir = os.path.join(output_dir_mxd, 'reportes')
                os.mkdir(output_report_dir)
                
                # Generando reportes
                # df_principal= pkgo.report_drme_pm_data_general(getdataframe=True)
                df_ocumin_m = pkgo.report_ocu_mineral_metalico(map_code, getdataframe=True)
                df_ocumin_nm = pkgo.report_ocu_mineral_no_metalico(map_code, getdataframe=True)
                df_rocmin = pkgo.report_roc_min_ind(map_code, getdataframe=True)
                df_geoquimica = pkgo.report_geoquimica_sedimentos(map_code, getdataframe=True)
                response['report'] = os.path.join(output_report_dir, 'reporte_{}.xls'.format(map_code)) 
                writer = pd.ExcelWriter(response['report'], encoding='utf-8')
                # df_principal.to_excel(writer, index=False, sheet_name='datos_generales')
                df_geoquimica.to_excel(writer, index=False, sheet_name='geoquimica_sedimentos')
                df_ocumin_m.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_metalico')
                df_ocumin_nm.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_no_metalico')
                df_rocmin.to_excel(writer, index=False, sheet_name='rocas_minerales_industriales')
                writer.save()


        response["response"]="response"
    except Exception as e:
        response['status'] = _ERROR_STATE
        response['message'] = e.message
        if map_type:
            pkga.update_state_row(map_code, response['status'], detail=e.message, iscommit=True)
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(10, response)