# Importar librerias
import arcpy
import json
import sys
import os

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    # Insertar procesos 
    response["response"]=os.path.join(sys.prefix, 'python.exe')
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(0, response)
