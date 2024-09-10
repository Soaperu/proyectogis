# Importar librerias
import arcpy
import json
import settings_aut as st
import packages_aut as pkg
import automapic as aut
import pythonaddins
import os
import messages_aut as msg


def split_data_by_polygon(row, geom, df_name, mxd=None):
    """
    Funcion que divide los datos de una capa por un poligono
    :param row: 
    :param geom: Poligono
    :param df_name: Nombre del data frame en donde se agregaran los datos
    :param mxd: Mapa de donde se agregara los mapas (como objeto mxd)
    """
    if row['query'] in ('1', '9'):
        return
    # response = os.path.join(arcpy.env.scratchGDB, row['layer_name'])
    
    feature = os.path.join(row['datasource'], row['feature'])
    if not feature.startswith(r'\\'):
        feature = feature.replace('\\\\', '\\')
    name_feature= os.path.basename(feature)
    name_feature = name_feature.split('.')[1] if '.' in name_feature else name_feature

    desc = arcpy.Describe(feature)
    workspace = str()
    typeworkspace = str()

    try:
        if desc.dataType == 'RasterDataset':
            workspace = arcpy.env.scratchGDB
            typeworkspace = "FILEGDB_WORKSPACE"
            avaliable = arcpy.CheckOutExtension('Spatial')
            if avaliable == "CheckedOut":
                response = os.path.join(workspace, row['layer_name'])
                mask = arcpy.sa.ExtractByMask(feature, geom)
                mask.save(response)
            else:
                raise RuntimeError(msg._LICENCE_SPATIAL_ANALYST_ERROR)
        else:
            workspace = st._TEMP_FOLDER if st._TEMP_FOLDER else arcpy.env.scratchFolder
            typeworkspace = "SHAPEFILE_WORKSPACE"
            response = os.path.join(arcpy.env.scratchFolder, row['layer_name'] + '.shp')
            arcpy.Clip_analysis(feature, geom, response)
        aut.add_layer_with_new_datasource(row['layer_name'], name_feature, workspace, typeworkspace, df_name=df_name, mxd=mxd)
    except Exception as e:
        pythonaddins.MessageBox("Ocurrio un error con el feature {}\n{}".format(row['feature'], e.message), st.__title__)
        return


if __name__ == '__main__':
    try:
        arcpy.env.overwriteOutput = True
        response = dict()
        response['status'] = 1
        response['message'] = 'success'

        features = arcpy.GetParameterAsText(0)
        cuencas = arcpy.GetParameterAsText(1)
        dataframe = arcpy.GetParameterAsText(2)
        # Obteniendo area geografica de las cuencas
        cuencas = cuencas.replace("'", '').replace(' ', '')
        cuencas = "('{}')".format("', '".join(cuencas.split(',')))
        query_cuencas = "{} IN {}".format(st._CD_CUENCA, cuencas)
        cuencas_mfl = arcpy.MakeFeatureLayer_management(st._PL_01_CUENCAS_HIDROGRAFICAS_PATH, "cuencas_mfl", query_cuencas)
        cuencas_diss = arcpy.Dissolve_management(cuencas_mfl, 'in_memory\\cuencas_diss')

        # Se obtiene todas las capas seleccionadas
        query_features = "{} IN ({})".format(st._ID, ','.join(features.split(",")))
        df = pkg.get_layers_selected(query_features, as_dataframe=True)
        for i, r in df.iterrows():
            split_data_by_polygon(r, cuencas_diss, dataframe)
    # Insertar procesos
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
