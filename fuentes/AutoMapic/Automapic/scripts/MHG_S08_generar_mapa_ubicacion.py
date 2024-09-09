# Importar librerias
import arcpy
import json
import os
import settings_aut as st
import automapic as aut

response = dict()
response['status'] = 1
response['message'] = 'success'

tipo = arcpy.GetParameterAsText(0)
cuencas = arcpy.GetParameterAsText(1)
dataframe = arcpy.GetParameterAsText(2)

try:
    zoom_nacional = True
    zoom_cuenca = False
    layer_name = "GPO_DEP_DEPARTAMENTO_NACIONAL.lyr"
    cuencas = "('{}')".format("', '".join(cuencas.split(',')))
    query = "{} IN {}".format(st._CD_CUENCA, cuencas)
    if tipo == "2":
        layer_name = "GPO_DEP_DEPARTAMENTO_CUENCA.lyr"
        zoom_nacional = False
        zoom_cuenca = True

    layer_departamento = os.path.join(st._LAYERS_DIR, layer_name)
    # bdgeocat_conn = st._BDGEOCAT_SDE_DEV if st.__status__ == 'Development' else st._BDGEOCAT_SDE
    name_feature_departamento = "GPO_DEP_DEPARTAMENTO"

    # Agregando mapa de ubicacion nacional
    aut.add_layer_with_new_datasource(layer_departamento, name_feature_departamento, st._BDGEOCAT_SDE, "SDE_WORKSPACE", df_name=dataframe, query=None, zoom=zoom_nacional)

    # Agregando mapa de ubicacion cuenca
    layer_cuencas = os.path.join(st._LAYERS_DIR, "PO_01_cuencas_hidrograficas_mu.lyr")
    name_feature_cuenca = os.path.basename(st._PL_01_CUENCAS_HIDROGRAFICAS_PATH)
   
    aut.add_layer_with_new_datasource(layer_cuencas, name_feature_cuenca, st._GDB_PATH_HG, "FILEGDB_WORKSPACE", df_name=dataframe, query=query, zoom=zoom_cuenca)
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
