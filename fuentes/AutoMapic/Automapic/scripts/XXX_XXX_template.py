# Importar librerias
import arcpy
import json

response = dict()
response['status'] = 1
response['message'] = 'success'

param = arcpy.GetParameterAsText(0)

try:
    # Insertar procesos 
    response["response"]="response"
    arcpy.AddMessage("Hello World!")
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
