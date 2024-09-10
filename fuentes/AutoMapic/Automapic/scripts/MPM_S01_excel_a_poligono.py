# Importar librerias
import arcpy
import json
import pandas as pd
import settings_aut as st
import os
import re

response = dict()
response['status'] = 1
response['message'] = 'success'

excel = arcpy.GetParameterAsText(0)
column_x = arcpy.GetParameterAsText(1)
column_y = arcpy.GetParameterAsText(2)
column_o = arcpy.GetParameterAsText(3)
src = arcpy.GetParameterAsText(4)
output = arcpy.GetParameterAsText(5)

try:
    # Obteniendo el nombre del archivo excel
    name = os.path.basename(excel).split('.')[0]
    name = name.strip()
    name = name.replace(' ', '_')

    # Leyendo datos de archivo excel
    df = pd.read_excel(excel)

    # Ordenando en funcion del campo orden
    df = df.sort([column_o])

    # Generando poligono a partir de coordenadas
    points = [arcpy.Point(float(r[column_x]), float(r[column_y])) for i, r in df.iterrows()]
    polygon = arcpy.Polygon(arcpy.Array(points))

    # Exportando poligono como shapefile
    feature = arcpy.CopyFeatures_management(polygon, output)

    # Definiendo la proyeccion
    src = re.findall('\(([^)]+)', src)[0]
    arcpy.DefineProjection_management(feature, arcpy.SpatialReference(int(src)))

    # Output
    # response["response"] = shp_output
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    arcpy.SetParameterAsText(6, output)
