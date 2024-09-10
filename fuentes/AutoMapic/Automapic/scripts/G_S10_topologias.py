# Importar librerias
import arcpy
import json
import packages_aut as pkg

response = dict()
response['status'] = 1
response['message'] = 'success'

modulo = arcpy.GetParameterAsText(0)

try:
    df = pkg.get_topology_items_by_module(modulo, as_dataframe=True)
    response['response'] = df.to_dict('records')
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
