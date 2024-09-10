# Importar librerias
import arcpy
import json
import automapic as aut
import os
import settings_aut as st
import packages_aut as pkg

response = dict()
response['status'] = 1
response['message'] = 'success'

# feature = arcpy.GetParameterAsText(0)
# layer = arcpy.GetParameterAsText(1)
# workspace = arcpy.GetParameterAsText(2)
# typeworkspace = arcpy.GetParameterAsText(3)
# df_name = arcpy.GetParameterAsText(4)
# query = arcpy.GetParameterAsText(5)
# zoom = arcpy.GetParameter(6)

featureId = arcpy.GetParameterAsText(0)
zona = arcpy.GetParameterAsText(1)
df_name = arcpy.GetParameterAsText(2)
query = arcpy.GetParameterAsText(3)

# featureId = 204
# zona = 19

# try:
# Se obtiene los datos de la capa a agregar
df = pkg.get_layers_selected('{} = {}'.format(st._ID, featureId), as_dataframe=True)

# Se obtiene el feature (sin workspace)
feature = df["feature"].item()

# Se verifica si el nombre necesita especificar alguna zona geografica especifica
if df["withzone"].item() == 1:
    # Se agrega la zona al nombre (dataset y feature)
    feature = feature.format(zona)

# Se construye la ruta del feature
feature_path = os.path.join(df['datasource'].item(), feature)

# Se obtiene solo el nombre del feature
name_feature = os.path.basename(feature_path)

# Se obtiene el layer (simbolos)
layer = df['layer'].item()
if layer:
    # Se construye la ruta del archivo layer
    layer = os.path.join(st._LAYERS_DIR, layer)
else:
    # Se pasa la ruta del feature si no se tiene el archivo layer
    layer = feature_path

# Se obtiene el workspace
workspace = df['datasource'].item()
# Se obtiene el tipo de workspace
typeworkspace = df['typedatasource'].item()

# Se verifica si el feature acepta filtros
if df['query'].item() != "1":
    query = None

# Se agrega la capa a la TOC
aut.add_layer_with_new_datasource(layer, name_feature, workspace, typeworkspace, df_name=df_name, query=query, zoom=False)
# except Exception as e:
#     response['status'] = 0
#     response['message'] = e.message
# finally:
response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
arcpy.SetParameterAsText(4, response)
