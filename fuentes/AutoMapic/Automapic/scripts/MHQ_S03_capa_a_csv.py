#!/usr/bin/env python
# -*- coding: utf-8 -*-
# Importar librerias
import arcpy
import os
import json
import pandas as pd
# import openpyxl
import settings_aut as st
response = dict()
response['status'] = 1
response['message'] = 'success'

# gdb = arcpy.GetParameterAsText(0)
# nombre_salida = arcpy.GetParameterAsText(1)
tabla = st._BASE_EXCEL_LAB_FC
scratch = arcpy.env.scratchFolder

def capa_a_csv(gdb, capa, ruta, nombre_csv):
    arcpy.env.workspace = gdb

    campos = [x.name for x in arcpy.ListFields(capa) if not x.name.lower().startswith('shape')]
    data= [x for x in arcpy.da.SearchCursor(capa,campos)]
    df = pd.DataFrame(data)
    df.columns= campos
    salida = os.path.join(ruta, nombre_csv)
    df.to_csv(salida, index=False, encoding='utf-8-sig', sep =';', escapechar="\n")
    return salida



try:
    gdb = st._GDB_PATH_MHQ
    nombre_salida = st._CSV_GRAFICOS

    pathcsv = capa_a_csv(gdb, tabla, scratch, nombre_salida)
    response["response"]= pathcsv
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(0, response)
