# import arcpy
import json
import settings_aut as st
from automapic import *
#import MG_S00_model as model

response = dict()
response['status'] = 1
response['message'] = 'success'

# geodatabase = arcpy.GetParameterAsText(0)
layer = arcpy.GetParameterAsText(0)

try:
    mxd= arcpy.mapping.MapDocument('CURRENT')
    df= arcpy.mapping.ListDataFrames(mxd,"*")[0]
    lyr= arcpy.mapping.ListLayers(mxd,"{}*".format(layer))[0]
    #funcion para asignar simbologias exactas desde un archivo o capa layer
    show_only_styled_values(lyr.name, os.path.join(st._BASE_DIR, 'layers/pog2.lyr'), "CODI")
    response['response'] = True
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(1, response)
