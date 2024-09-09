# Importar librerias
import json
import packages_aut_arc as pkga
import arcpy

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    # Insertar procesos
    regiones = pkga.get_all_regions(getcursor=True)
    response['response'] = regiones
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(0, response)
