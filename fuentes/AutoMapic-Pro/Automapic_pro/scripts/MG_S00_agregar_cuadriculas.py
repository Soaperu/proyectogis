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
    capa= ""
    # st._CUADRICULAS_MG_PATH = os.path.join(gdb_path, st._CUADRICULAS_MG_PATH)
    # mxd = arcpy.mapping.MapDocument("CURRENT")
    aprx = arcpy.mp.ArcGISProject('current')
    map_active = aprx.activeMap
    map_active.spatialReference = arcpy.SpatialReference(4326)

    name_layer = os.path.basename(st._CUADRICULAS_MG_PATH)
    layers = map_active.listLayers("*{}*".format(name_layer))
    #lyr= arcpy.
    if layers:
        for i in layers:
            map_active.removeLayer(i)
    capa = map_active.addDataFromPath(st._CUADRICULAS_MG_PATH)
    capa.visible = True
    if not arcpy.Exists(st._CUADRICULAS_MG_PATH):
        raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
    response['response'] = st._CUADRICULAS_MG_PATH
    
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(0, capa)
    arcpy.SetParameterAsText(1, response)