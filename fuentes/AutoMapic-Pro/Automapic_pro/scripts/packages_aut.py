# -*- coding: utf-8 -*-
from credentials import *
import cx_Oracle
import os

conn = cx_Oracle.connect(connoragis())
cursor = conn.cursor()

def packageDecore(func):
    def decorator(*args, **kwargs):
        global conn, cursor    
        package = func(*args, **kwargs)
        if kwargs.get('iscommit'):
            cursor.execute(package)
            return
        elif kwargs.get('getcursor'):
            cursor.execute(package)
            return cursor
        elif kwargs.get('returnsql'):
            return package
        elif kwargs.get('one'):
            cursor.execute(package)
            return cursor.fetchone()[0]
        elif kwargs.get('as_dataframe'):
            import pandas as pd
            return pd.read_sql(package, conn)
        else:
            cursor.execute(package)
            return cursor.fetchall()

    return decorator


@packageDecore
def get_config_param_value(name, one=True):
    return "SELECT VALUE FROM TB_CONFIG WHERE STATE = 1 AND NAME = '{}'".format(name)

@packageDecore
def get_validate_user(user, one=True):
    return "SELECT COUNT(*) FROM TB_USER WHERE USER='{}'".format(user)

@packageDecore
def get_validate_user_pass(user, password, one=True):
    return "SELECT COUNT(*) FROM TB_USER WHERE USER  ='{}' AND PASSWORD = '{}'".format(user, password)

@packageDecore
def get_perfil(user):
    return "select id_modulo, modulo from vw_access where user = '{}'".format(user)

# @packageDecore
# def get_user_login(one=True):
#     return "SELECT USER FROM TB_USER WHERE LOGIN = 1"

# @packageDecore
# def set_user_login(user, iscommit=True):
#     return "UPDATE TB_USER SET LOGIN = 1 WHERE USER = '{}'".format(user)

# @packageDecore
# def set_all_user_logout(iscommit=True):
#     return "UPDATE TB_USER SET LOGIN = 0"

@packageDecore
def get_user_login(one=True):
    return "SELECT USUARIO FROM TB_LOGIN WHERE LOGIN = 1"

@packageDecore
def set_user_login(user, iscommit=True):
    return "INSERT INTO TB_LOGIN(USUARIO, LOGIN) VALUES('{}',1)".format(user)

@packageDecore
def set_all_user_logout(iscommit=True):
    return "DELETE FROM TB_LOGIN WHERE LOGIN = 1"

@packageDecore
def get_config_by_user(user, as_dataframe=False):
    return "select config, name from VW_USER_CONFIG WHERE USER = '{}'".format(user)

@packageDecore
def get_config_by_modules(modules, as_dataframe=False):
    return """select b.id config, b.name 
                from 
              tb_config b where b.id_modulo in ({0}) and B.id not in (3)
              union all 
              select B.id config, B.name
               from  
              tb_config B where 11 in ({0}) and B.id = 3""".format(modules)

@packageDecore
def set_config_param(id_parameter, value, iscommit=True):
    return "UPDATE TB_CONFIG SET VALUE = '{}' WHERE ID = {}".format(value, id_parameter)

@packageDecore
def get_tree_layers(category, as_dataframe=True):
    #return "SELECT * FROM TB_LAYERS WHERE CATEGORY IN ({}) ORDER BY ID".format(category)
    return "SELECT * FROM ged_m_lista WHERE tipo LIKE '%AUTOMAPICPRO_LAYERS%' AND ORDEN IN ({}) ORDER BY CODIGO".format(category)

@packageDecore
def get_layers_by_parent(parent, as_dataframe=True):
    return "SELECT * FROM TB_LAYERS WHERE PARENT = {}".format(parent)


@packageDecore
def set_datasources_tree_layers(datasource, category, settable, iscommit=True):
    return "UPDATE TB_LAYERS SET DATASOURCE = '{}' WHERE (CATEGORY = {}) AND (SETTABLE = {})".format(datasource, category, settable)

@packageDecore
def get_layers_selected(where, as_dataframe=True):
    return "SELECT * FROM TB_LAYERS WHERE {}".format(where)

@packageDecore
def get_topology_items_by_module(id_modulo, as_dataframe=True):
    return "SELECT ID, NAME FROM TB_TOPOLOGY WHERE ID_MODULO = {}".format(id_modulo)

@packageDecore
def get_topology_items(query, as_dataframe=True):
    return "SELECT ID, NAME FROM TB_TOPOLOGY WHERE ID IN ({})".format(query)


@packageDecore
def get_dic_dominio(dom_name):
    return "SELECT DOMINIO, KEY, VALUE FROM TB_MHQ_GDB_DOMINIOS WHERE DOMINIO = '{}'".format(dom_name)

@packageDecore
def get_relacion_campos():
    return "SELECT campo_fc, tipo, largo, dominio FROM TB_MHQ_GDB_RELACION_CAMPOS "

@packageDecore
def get_tabla_equivalencias():
    return "SELECT campo_lab_2, campo_fc FROM TB_MHQ_GDB_RELACION_CAMPOS "

