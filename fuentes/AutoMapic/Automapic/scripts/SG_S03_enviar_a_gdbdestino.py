#!/usr/bin/env python
# -*- coding: utf-8-sig -*-
import sys
import arcpy
import os
import settings_aut as st
import messages_aut as msg
from packages_aut import conn
import traceback
import json
import pandas as pd
import datetime

arcpy.env.overwriteOutput= True

gdb_origen = arcpy.GetParameterAsText(0)
gdb_destino = arcpy.GetParameterAsText(1)
rutaarchivo = arcpy.GetParameterAsText(2)
usuario = arcpy.GetParameterAsText(3)

response = dict()
response['status'] = 1
response['message'] = 'success'

def enviardatos(gdbini, gdbfin, rutacsv, usuario):
    df = pd.read_csv(rutacsv, encoding='UTF-8-sig')
    contador = 0
    for index, row in df.iterrows():
        if row["enviar"]== True:
            fc_inicial = row["source"]
            fc_destino = os.path.join(gdbfin, row["nombre_destino"].split('.')[0])

            desc = arcpy.Describe(fc_inicial)
            if desc.dataType in [u'FeatureClass', u'ShapeFile']:
                arcpy.CopyFeatures_management(fc_inicial,fc_destino)
            elif desc.dataType == u'Table':
                arcpy.CopyRows_management(fc_inicial,fc_destino)
            elif desc.dataType in [u'TextFile',u'File']:
                path, file = os.path.split(fc_destino)
                arcpy.TableToTable_conversion(fc_inicial, path, file)
            else:
                pass
            
            contador +=1
    
    df2 = df[df["enviar"]==True].copy()
    df2 = df2.drop(df2.columns[-1], axis=1)

    df2["tipo"] = "UPDATE"
    df2.loc[(df.existe_destino == 0) | (df2.existe_origen == 0), "tipo"] = "INSERT"
    df2["usuario"] = usuario
    df2["fecha"] = datetime.datetime.now()
    df2.to_sql('tb_sinc_gdb', con=conn, if_exists='append', index=False)

    return contador


try:
    num_capas_actualizadas = enviardatos(gdb_origen, gdb_destino, rutaarchivo, usuario)
    response['response'] = num_capas_actualizadas
    
except Exception as e:
    response['status'] = 0
    # response['message'] = e.message
    response['message'] = traceback.format_exc()
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(4, response)