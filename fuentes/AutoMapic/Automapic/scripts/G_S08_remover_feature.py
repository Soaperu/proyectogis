# Importar librerias
import arcpy
import json
import packages_aut as pkg
import settings_aut as st
import os

response = dict()
response['status'] = 1
response['message'] = 'success'

# lyr_name = arcpy.GetParameterAsText(0)
# df_name = arcpy.GetParameterAsText(1)
featureid = arcpy.GetParameterAsText(0)
df_name = arcpy.GetParameterAsText(1)

try:
    df = pkg.get_layers_selected('{} = {}'.format(st._ID, featureid), as_dataframe=True)
    mxd = arcpy.mapping.MapDocument("CURRENT")
    df_name = "*{}*".format(df_name) if df_name else None
    df_objs = arcpy.mapping.ListDataFrames(mxd, df_name)
    if not len(df_objs):
        raise RuntimeError("El dataframe especificado no existe")
    df_obj = df_objs[0]
    lyr_name = df["layer"].item()
    if lyr_name:
        lyr_name = lyr_name.split('.')[0]
    else:
        lyr_name = os.path.basename(df["feature"].item())
    lyrs = arcpy.mapping.ListLayers(mxd, "*{}*".format(lyr_name), df_obj)
    if len(lyrs):
        arcpy.mapping.RemoveLayer(df_obj, lyrs[0])
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
