# import arcpy
import json
import settings_aut as st
from automapic import *
import MG_S00_model as model

response = dict()
response['status'] = 1
response['message'] = 'success'

# geodatabase = arcpy.GetParameterAsText(0)
zona = arcpy.GetParameterAsText(0)
codhoja = arcpy.GetParameterAsText(1)

fc_ulito = model.gpo_dgr_ulito(str(zona))
fc_pog = model.gpt_dgr_pog(str(zona))

try:
    features = [
        fc_ulito.path,
        fc_pog.path
        # st._ULITO_MG_PATH,
        # st._POG_MG_PATH
    ]
    features = map(lambda i: i.format(zona, zona), features)
    query = "{} = '{}'".format(fc_ulito.codhoja, codhoja)
    name_ulito = os.path.basename(features[0])
    layers = [
        os.path.join(st._BASE_DIR, 'layers/{}_G.lyr'.format(name_ulito)),
        os.path.join(st._BASE_DIR, 'layers/pog2.lyr')
    ]
    check_layer_inside_data_frame(features, layers, df_name=None, query=query)

    mxd= arcpy.mapping.MapDocument('CURRENT')
    df= arcpy.mapping.ListDataFrames(mxd,"*")[0]
    lyr= arcpy.mapping.ListLayers(mxd,"GPT_DGR_POG*")[0]
    #funcion para asignar simbologias exactas desde un archivo o capa layer
    show_only_styled_values(lyr.name, os.path.join(st._BASE_DIR, 'layers/pog2.lyr'), "CODI")
    response['response'] = True
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(2, response)
