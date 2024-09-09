import arcpy
import settings_aut as st
import os
import json
import messages_aut as msg

codhoja = arcpy.GetParameterAsText(0)
# geodatabase = arcpy.GetParameterAsText(1)

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    if not arcpy.Exists(st._CUADRICULAS_MG_PATH):
        raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
    

    row = arcpy.da.SearchCursor(st._CUADRICULAS_MG_PATH, ['ZONA'], "{} = '{}'".format(st._CODHOJA_FIELD, codhoja))
    row = map(lambda i: i[0], row)[0]

    wkid = int('327{}'.format(row))

    response['response'] = row

    mxd = arcpy.mapping.MapDocument("CURRENT")
    mxd.activeDataFrame.spatialReference = arcpy.SpatialReference(wkid)

except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(1, response)
