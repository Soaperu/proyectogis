# Importar librerias
import json
import arcpy
import os
# import packages_aut as pkg

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    # Insertar procesos
    workspace = arcpy.GetParameterAsText(0)
    arcpy.env.workspace = workspace
    geodatabases = arcpy.ListWorkspaces("*", "FileGDB")
    geodatabases = [[i, os.path.basename(v)] for i, v in enumerate(geodatabases, 1)]

    # pkg.set_config_param(id_parameter, workspace, iscommit=True)
    response['response'] = geodatabases
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
