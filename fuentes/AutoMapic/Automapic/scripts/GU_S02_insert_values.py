# Importar librerias
import sys
reload(sys)
sys.setdefaultencoding("utf-8")
import arcpy
import json
import packages_aut_arc as pa
response = dict()
response['status'] = 1
response['message'] = 'success'

param = arcpy.GetParameterAsText(0)
values =arcpy.GetParameterAsText(1)

def insert_tb_usuarios(values):
    """
    Insert user into tb_usuarios
    """
    values = values.split(";")
    id = values[0]
    usuario = values[1]
    oficina = values[2]
    nombre = values[3]
    apepat = values[4]
    apemat = values[5]
    respuesta = pa.insert_row_tb_osi_usuarios(id, usuario, oficina, nombre, apepat, apemat, iscommit=True)
    return respuesta    

def edit_tb_usuarios(values):
    """
    Edit user into tb_usuarios
    """
    values = values.split(";")
    id = values[0]
    usuario = values[1]
    oficina = values[2]
    nombre = values[3]
    apepat = values[4]
    apemat = values[5]
    respuesta = pa.edit_row_tb_osi_usuarios(id, usuario, oficina, nombre, apepat, apemat, iscommit=True)
    return respuesta

def delete_tb_usuarios(values):
    """
    Delete user into tb_usuarios
    """
    values = values.split(";")
    id = values[0]
    respuesta = pa.delete_row_tb_osi_usuarios(id, iscommit=True)
    respuesta = pa.delete_row_tb_osi_access(id, iscommit=True)
    return respuesta

def get_access_information_by_id(values):
    """
    Get user information by id
    """
    values = values.split(";")
    id = values[0]
    respuesta = pa.get_access_information_by_id(id, getcursor=True)    
    return respuesta

def insert_row_tb_osi_access(values):
    """
    Insertar o actualizar perfiles de acceso para usuarios en tb_osi_aut_access
    """
    values = values.split(";")
    for value in values:
        value_ = value.split(",")
        id = value_[0]
        modulo = value_[1]
        perfil = value_[2]
        respuesta = pa.insert_row_tb_osi_access(id, modulo, perfil, iscommit=True)

    return respuesta

def selectfunction(param, valores):
    if param == 'insert_tb_usuarios':
        return insert_tb_usuarios(valores)
    elif param == 'edit_tb_usuarios':
        return edit_tb_usuarios(valores)
    elif param == 'delete_tb_usuarios':
        return delete_tb_usuarios(valores)
    elif param == 'get_access_information_by_id':
        return get_access_information_by_id(valores)
    elif param == 'insert_row_tb_osi_access':
        return insert_row_tb_osi_access(valores)


try:
    respuesta = selectfunction(param, values)
    response["response"]=respuesta
    # arcpy.AddMessage("Hello World!")
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(2, response)