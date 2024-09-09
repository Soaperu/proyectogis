# Importar librerias
import sys
reload(sys)
sys.setdefaultencoding("utf-8")
import arcpy
import os
import json
import uuid
import pandas as pd
import packages_aut_arc as pa
import packages_aut_ora as pa_ora
import settings_aut as st

response = dict()
response['status'] = 1
response['message'] = 'success'

param = arcpy.GetParameterAsText(0)

def getusersdatagrid():
    """
    Get users from database with extra information
    """
    respuesta = pa.getusersdatagrid(getcursor=True)
    return respuesta

def getuserstb():
    """
    Get users from tb database
    """
    respuesta = pa.get_users_list_tb(getcursor=True)
    return respuesta

def getmaxid():
    """
    Get max id from database
    """
    respuesta = pa.get_max_id(getcursor=True)
    return respuesta

def getoficinanames():
    """
    Get oficinas names from database
    """
    respuesta = pa.get_oficinas_names(getcursor=True)
    return respuesta

def getperfiles():
    """
    Get perfiles from database
    """
    respuesta = pa.get_perfiles(getcursor=True)
    return respuesta

def getusersfromactivedirectory():
    """
    Get users from active directory
    """
    respuesta = pa_ora.get_users_from_active_directory(getdataframe=False)
    lista2 = []
    lista2.append([" ", " "])
    for i in respuesta:
        lista2.append([i[0],i[1].replace(" ",";")])
    return lista2

def getreporte():
    """
    Get reporte
    """
    path = os.path.join(st._TEMP_FOLDER,"reporte_{}.xls".format(uuid.uuid4().get_hex()))
    df = pa_ora.get_report_users_automapic(getdataframe=True)
    writer = pd.ExcelWriter(path, encoding='utf-8')
    df.to_excel(writer,sheet_name='Reporte', index=False)
    writer.save()
    return path

def selectfunction(param):
    if param == 'getusersdatagrid':
        return getusersdatagrid()
    elif param == 'getuserstb':
        return getuserstb()
    elif param == 'getmaxid':
        return getmaxid()
    elif param == 'getoficinanames':
        return getoficinanames()
    elif param == 'getusersfromactivedirectory':
        return getusersfromactivedirectory()
    elif param == 'getperfiles':
        return getperfiles()
    elif param == 'getreporte':
        return getreporte()

try:
    respuesta= selectfunction(param)
    response["response"]=respuesta
    # arcpy.AddMessage("Hello World!")
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
