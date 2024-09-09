import sys
reload(sys)
sys.setdefaultencoding("utf-8")
# Importar librerias
import arcpy
import json
import tempfile
import messages_aut as msg
import os
import settings_aut as st
import PG_S01_mapa_peligros_geologicos as mpg
import traceback

arcpy.env.overwriteOutput = True

response = dict()
response['status'] = 1
response['message'] = 'success'

shapefile = arcpy.GetParameterAsText(0)

def sheet_orientation(feature):
    desc = arcpy.Describe(feature)
    xmax, xmin, ymax, ymin = desc.extent.XMax, desc.extent.XMin, desc.extent.YMax, desc.extent.YMin
    distance_h = abs(abs(xmax) - abs(xmin))
    distance_v = abs(abs(ymax) - abs(ymin))
    factor = round(float(distance_h) / float(distance_v), 2)
    ori = 'v' if factor <= 1 else 'h'
    return ori

def sheet_scale_map(feature):
    mxd = arcpy.mapping.MapDocument("CURRENT")
    desc = arcpy.Describe(feature)
    mxd.activeDataFrame.extent = desc.extent
    mxd.activeDataFrame.scale *= 1.2
    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()
    scale_tmp = mxd.activeDataFrame.scale
    scale = mpg.get_scale(scale_tmp)
    return scale

def sheet_size(feature):
    count = int(arcpy.GetCount_management(feature).getOutput(0))
    size_sheet = 'a2' if count >= 3 else 'a1'
    return size_sheet

try:
    # Insertar procesos 
    
    # Verificando si el shapefile ingresado existe
    if not arcpy.Exists(shapefile):
        raise RuntimeError(msg._MPM_FEATURE_NOT_EXIST)

    # Obteniendo propiedades
    desc = arcpy.Describe(shapefile)

    # Validacion de geometria
    if desc.shapeType != 'Polygon':
        raise RuntimeError(msg._MPM_FEATURE_NOT_POLYGON)

    # Validacion de sistema de referencia
    wkid = desc.spatialReference.factoryCode
    if not wkid:
        raise RuntimeError(msg._MPM_SRC_NOT_EXIST)

    zone = 0

    basename_shapefile = os.path.basename(shapefile)
    name, ext = os.path.splitext(basename_shapefile)

    # Validacion de nombre de archivo shapefile
    name = arcpy.ValidateFieldName(name)

    # Dividir los registros que sean multipart
    mptsp = arcpy.MultipartToSinglepart_management(shapefile, 'in_memory\mptsp_mg')
    #temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()


    orientation = sheet_orientation(mptsp)
    scale = sheet_scale_map(mptsp)
    size = sheet_size(mptsp)

    response['response']=dict()
    response['response']['orientation'] = orientation
    response['response']['scale'] = scale
    response['response']['size'] = size

except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()
finally:
    response = json.dumps(response) #, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
