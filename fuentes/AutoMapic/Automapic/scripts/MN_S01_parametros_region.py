# Importar librerias
import arcpy
import json
import settings_aut as st
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'

cd_depa = arcpy.GetParameterAsText(0)

try:
    # Insertar procesos
    query = "{} = '{}'".format(st._CD_DEPA_FIELD, cd_depa)
    
    fields = [
        st._CD_DEPA_FIELD,
        st._NM_DEPA_FIELD,
        st._ESCALA_FIELD,
        st._HOJA_FIELD,
        st._ORIENTACION_FIELD,
        st._ZONA_FIELD
    ]
    cursor = map(lambda i: i, arcpy.da.SearchCursor(st._TB_REGION_CONFIG, fields, query))
    if not len(cursor):
        raise RuntimeError(msg._ERROR_EMPTY_REGION)
    
    result = dict(zip(fields, cursor[0]))
    response['response'] = result

except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    print response
    arcpy.SetParameterAsText(1, response)
