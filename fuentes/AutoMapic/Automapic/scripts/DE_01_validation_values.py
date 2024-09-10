# -*- coding: utf-8 -*-
import settings_aut as st
import arcpy
import json
import traceback
import pandas as pd

response = dict()
response['status'] = 1
response['message'] = 'success'
response['observado']= 'no'

file = arcpy.GetParameterAsText(0) #r"C:\Users\proyectososi10\Documents\ArcGIS\Libro1.csv" #
count_rows= None

def validation(df):
    global count_rows
    len_columnas = len(df.columns)
    columnas = list()
    # if columnas<=2:
    az= [z for z in df.iloc[:,0]]
    bz= [b for b in df.iloc[:,1]]
    azFilter= [z for z in df.iloc[:,0] if z >=0 and z<=360]
    bzFilter= [b for b in df.iloc[:,1] if b >=0 and b<=90]
    rk =list()
    rkFilter= list()
    columnas.append(az)
    columnas.append(bz)
    count_rows = len(az)
    if len(az) != len(azFilter) or len(bz) != len(bzFilter):
        response['observado']= 'observado'

    if len_columnas > 2:
        rk= [r for r in df.iloc[:,2]]
        rkFilter = [r for r in df.iloc[:,2] if r >=-180 and r <=180]
        if len(rk) == len(rkFilter):
            response['observado']= 'observado'
        columnas.append(rk)       
    return columnas

def validation_csv(File):
    """
    Valida los campos de Azimut, Buzimiento y Rake del archivo tipo CSV
    """
    df = pd.read_csv(File, ";")
    columnas=validation(df)
    return columnas

def validation_txt(File):
    """
    Valida los campos de Azimut, Buzimiento y Rake del archivo tipo TXT
    """
    df= pd.read_csv(File, sep='\\t', header=None)
    columnas=validation(df)
    return columnas

def validation_shp(File):
    """
    Valida los campos de Azimut, Buzamiento y Rake del archivo tipo Shapefile
    """
    global count_rows
    fields = [st._AZIMUT_FIELD, st._BUZAMIENTO_FIELD]
    if len(fields) < 2:
        return
    #count_rows = len(fields)
    az, bz=list(), list()
    azFilter, bzFilter = list(), list()
    with arcpy.da.SearchCursor(File, fields) as cursor:
        for i in cursor:
            az.append(i[0])
            bz.append(i[1])
            if i[0] >=0 and i[0] <=360:
                azFilter.append(i[0])
            if i[1] >=0 and i[1] <=90:
                bzFilter.append(i[1])
    count_rows = len(az)
    if len(az) != len(azFilter) or len(bz) != len(bzFilter):
        response['observado']= 'observado'
    return [az,bz]

try:
    if file.endswith(".csv"):
        response['response'] = validation_csv(file)
    elif file.endswith(".txt"):
        response['response'] = validation_txt(file)
    elif file.endswith(".shp"):
        response['response'] = validation_shp(file)
    response["countrows"] = count_rows
    
except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()#e.message
finally:
    response = json.dumps(response, default=str)#, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
    #print(response)