import arcpy
import settings_aut as st
import os
import json
import messages_aut as msg
import automapic as aut

arcpy.env.addOutputsToMap = True

# Parametro de codigos separados por comas
codigos_cuencas = arcpy.GetParameterAsText(0)

response = dict()
response['status'] = 1
response['message'] = 'success'

# try:
query = "{} IN ('{}')".format(st._CD_CUENCA, "', '".join(codigos_cuencas.split(',')))
if not arcpy.Exists(st._PL_01_CUENCAS_HIDROGRAFICAS_PATH):
    raise RuntimeError(msg._ERROR_FEATURE_CUENCAS_HG)

feature_path = st._PL_01_CUENCAS_HIDROGRAFICAS_PATH
feature_name = os.path.basename(feature_path)
layer = os.path.join(st._LAYERS_DIR, 'PO_01_cuencas_hidrograficas.lyr')

zonas = arcpy.da.SearchCursor(st._PL_01_CUENCAS_HIDROGRAFICAS_PATH, [st._ZONA_FIELD], query)
zona = map(lambda i: i[0], zonas)
if len(zona):
    zona = zona[0]
else:
    zona = 0

aut.add_layer_with_new_datasource(layer, feature_name, st._GDB_PATH_HG, "FILEGDB_WORKSPACE", df_name=None, query=query, zoom=True)
# aut.check_layer_inside_data_frame(features, symbols, df_name=None, query=query, zoom=True)
response['response'] = zona
    
# except Exception as e:
#     response['status'] = 0
#     response['message'] = e.message
# finally:
response = json.dumps(response)
arcpy.SetParameterAsText(1, response)
