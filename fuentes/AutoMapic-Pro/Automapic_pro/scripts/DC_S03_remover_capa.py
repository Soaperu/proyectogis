import arcpy
import os
import settings_aut as st
import automapic as aut
import json

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    lyrName = arcpy.GetParameterAsText(0) # r"Vias Nacional (Geocatmin - MTC)" #
    # Primero, necesitamos acceder al proyecto actual y al mapa activo para poder añadir la capa
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    mapa_activo = aprx.activeMap
    for layer in mapa_activo.listLayers():
        if layer.name == lyrName:
            mapa_activo.removeLayer(layer) 

except Exception as e:
    response['status'] = 0
    response['message'] = str(e)
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(1, response)