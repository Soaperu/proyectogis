import arcpy
import settings_aut as st
import os
import json
import messages_aut as msg

arcpy.env.addOutputsToMap = True

# gdb_path = arcpy.GetParameterAsText(0)

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    params = arcpy.GetParameterInfo()

    # st._CUADRICULAS_MG_PATH = os.path.join(gdb_path, st._CUADRICULAS_MG_PATH)
    mxd = arcpy.mapping.MapDocument("CURRENT")
    mxd.activeDataFrame.spatialReference = arcpy.SpatialReference(4326)

    name_layer = os.path.basename(st._CUADRICULAS_MG_PATH)
    layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(name_layer))
    if layers:
        for i in layers:
            arcpy.mapping.RemoveLayer(mxd.activeDataFrame, i)

    if not arcpy.Exists(st._CUADRICULAS_MG_PATH):
        raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
    response['response'] = st._CUADRICULAS_MG_PATH
    
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(0, st._CUADRICULAS_MG_PATH)
    arcpy.SetParameterAsText(1, response)
