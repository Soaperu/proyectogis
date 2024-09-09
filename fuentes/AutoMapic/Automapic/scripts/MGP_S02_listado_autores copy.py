# Importar librerias
import arcpy
import json
import settings_aut as st
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    # Insertar procesos
    feature = st._TB_AUTOR_MGP
    if not arcpy.Exists(feature):
        raise RuntimeError(msg._ERROR_FEATURE_AUTORES_HG)
    cursor = arcpy.da.SearchCursor(feature, [st._ID_AUTOR_MN, st._AB_AUTOR_MN])
    response['response'] = list(map(lambda i: [i[0], i[1]], cursor))
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(0, response)
