import arcpy
import os
import json
import settings_aut as st
import automapic as aut

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    pathFeatureClass = arcpy.GetParameterAsText(0) # r"DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_NACIONAL"
    fileLyrxName = arcpy.GetParameterAsText(1) # r"GLI_MTC_VIA_NACIONAL.lyrx" #
    lyrName = arcpy.GetParameterAsText(2) # r"Vias Nacional (Geocatmin - MTC)" #

    # Define el path al Feature Class y al archivo .lyrx de simbología
    feature_class_path = os.path.join(st._BDGEOCAT_SDE, pathFeatureClass) 
    lyrx_file_path = os.path.join(st._SERVER_LAYER_PRO, fileLyrxName)

    # Carga el archivo .lyrx
    lyrx = arcpy.mp.LayerFile(lyrx_file_path)

    # Primero, necesitamos acceder al proyecto actual y al mapa activo para poder añadir la capa
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    mapa_activo = aprx.activeMap

    # Añade la capa al mapa activo (esto la hace aparecer en la Tabla de Contenidos)
    capa_anadida = mapa_activo.addLayer(lyrx, "TOP")

    # Obtenemos el nombre del featureClass 
    splitPathFeatureCLass = pathFeatureClass.split("\\")
    if len(splitPathFeatureCLass)>1:
        lyrName_sde = splitPathFeatureCLass[1]
    else:
        lyrName_sde = pathFeatureClass

    for layer in mapa_activo.listLayers():
        if layer.name == fileLyrxName[:-5]:
            layer.name = lyrName
            if not layer.visible:
                layer.visible = True
            # Configurar la nueva fuente de datos para la capa
            aut.new_datasource(layer,lyrName)

    # Opcional: Guarda los cambios en el proyecto de ArcGIS Pro si es necesario
    # aprx.save()
except Exception as e:
    response['status'] = 0
    response['message'] = str(e)
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(3, response)
