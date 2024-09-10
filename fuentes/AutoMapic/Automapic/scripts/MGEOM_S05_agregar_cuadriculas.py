import sys
reload(sys)
sys.setdefaultencoding("utf-8")

import arcpy
import settings_aut as st
import messages_aut as msg
import traceback
import os
import json

# gdb_path = arcpy.GetParameterAsText(0)
root = arcpy.GetParameterAsText(0)

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    if not arcpy.Exists(st._CUADRICULAS_MG_PATH):
        raise RuntimeError(msg._ERROR_MGEOM_NOT_DATA)
    #name_layer = os.path.basename(st._CUADRICULAS_MG_PATH)
    # mxd = arcpy.mapping.MapDocument("CURRENT")
    # dfs = arcpy.mapping.ListDataFrames(mxd)[0]
    # layer= arcpy.mapping.Layer(root)
    # ObjectAddfc= arcpy.mapping.AddLayer(dfs,layer,"BOTTOM")
    # #lyrs = arcpy.mapping.ListLayers(mxd, "*{}*".format(name_layer))
    # dfs.extent = layer.getExtent()
    # arcpy.RefreshActiveView()
    # arcpy.RefreshTOC()
    # del ObjectAddfc
    # del layer
    # del dfs
    # del mxd
    # response['response'] = 'Feature agregado'

    params = arcpy.GetParameterInfo()

    # st._CUADRICULAS_MG_PATH = os.path.join(gdb_path, st._CUADRICULAS_MG_PATH)
    mxd = arcpy.mapping.MapDocument("CURRENT")
    # mxd.activeDataFrame.spatialReference = arcpy.SpatialReference(4326)

    name_layer = os.path.basename(st._CUADRICULAS_MG_PATH)
    layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(name_layer))
    if layers:
        for i in layers:
            arcpy.mapping.RemoveLayer(mxd.activeDataFrame, i)

    # if not arcpy.Exists(root):
    #     raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
    response['response'] = root
    
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(1, st._CUADRICULAS_MG_PATH)
    arcpy.SetParameterAsText(2, response)

