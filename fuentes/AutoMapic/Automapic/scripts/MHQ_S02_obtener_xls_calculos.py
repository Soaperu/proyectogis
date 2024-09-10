#!/usr/bin/env python
# -*- coding: utf-8 -*-
# Importar librerias
import arcpy
import os
import json
from MHQ_S01_ingresar_datos import preparar_datos_csv, str_to_num
import sys  
import traceback
from subprocess import call
import datetime



xls_in = arcpy.GetParameterAsText(0)
xls_out = arcpy.GetParameterAsText(1)
csv_out = xls_out.split('.')[0]+'.csv'

response = dict()
response['status'] = 1
response['message'] = 'success'

###########################################################

def decore_subprocess(func):
    """
    Decora funciones que devuelvan una sentencia ejecutable del consola(cmd)
    :param func: Funcion a decorar
    :return: Nueva funcion
    """

    def decorator(*args):
        command = func(*args)
        p = call('{}\python.exe {}'.format(sys.exec_prefix, command), shell=True)

    return decorator

@decore_subprocess
def estilizar_xls(csvpath,xlsxpath):
    """
    Se ejecuta con subprocess debido a que al utilizarlo desde Arcmap  sobrepasa los 2 minutos de ejecucion
    """
    xlsxpath = xlsxpath.split('.')[0]+"_1."+xlsxpath.split('.')[1]
    folder = os.path.dirname(__file__)
    file = r"MHQ_estilizar.py"
    path = os.path.join(folder,file)
    return "{} {} {}".format(path, csvpath,xlsxpath)


try:
    preparar_datos_csv(xls_in, csv_out)
    estilizar_xls(csv_out,xls_out)

    response["response"]= xls_out
    arcpy.AddMessage("Hello World!")
except Exception as e:
    response['status'] = 0
    # response['message'] = e.message
    response['message'] = traceback.format_exc()

finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(2, response)
