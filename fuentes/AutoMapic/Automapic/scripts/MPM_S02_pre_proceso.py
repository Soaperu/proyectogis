# Importar librerias
import arcpy
import json
import settings_aut as st
import tempfile
import os
import PG_S01_mapa_peligros_geologicos as mpg
import messages_aut as msg

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
    mxd.activeDataFrame.scale *= 1.4
    arcpy.RefreshTOC()
    arcpy.RefreshActiveView()
    scale_tmp = mxd.activeDataFrame.scale
    scale = mpg.get_scale(scale_tmp)
    return scale

def sheet_size(feature):
    count = int(arcpy.GetCount_management(feature).getOutput(0))
    size_sheet = 'a3' if count >= 3 else 'a4'
    return size_sheet

def field_name(feature):
    fields = map(lambda i: i.name.lower(), arcpy.ListFields(feature))
    if 'nombre' not in fields:
        arcpy.AddField_management(feature, 'nombre', 'TEXT', '', '', '100')
    return feature

if __name__ == '__main__':
    try:
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
        mptsp = arcpy.MultipartToSinglepart_management(shapefile, 'in_memory\mptsp')
        temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()

        temp_folder = os.path.join(temp_folder, 'temp')

        # Crear carpeta temporal
        if not os.path.exists(temp_folder):
            os.makedirs(temp_folder)

        output_shp = os.path.join(temp_folder, '{}{}'.format(name, ext))

        # Reproyectando
        if wkid in (4326, 4248):
            zone_sc = arcpy.da.SearchCursor(st._GPO_ZUT_ZONAS_UTM_PATH, [st._ZONA_FIELD, "SHAPE@"])
            diss = arcpy.Dissolve_management(shapefile, 'in_memory\\dis')
            diss_shp = arcpy.da.SearchCursor(diss, ["SHAPE@"])
            diss_shp = [i[0] for i in diss_shp][0]

            if wkid == 4248:
                diss_shp = diss_shp.projectAs(arcpy.SpatialReference(4326))

            area_sel = 0

            for i in zone_sc:
                area = diss_shp.intersect(i[1], 4).area
                if area > area_sel:
                    area_sel = area
                    zone = i[0]
            
            epsg = int('327{}'.format(zone))
            
            if wkid == 4248:
                p4326_output = os.path.join(temp_folder, 'p4326.shp')
                arcpy.Project_management(mptsp, p4326_output, arcpy.SpatialReference(4326))
                arcpy.Project_management(p4326_output, output_shp, arcpy.SpatialReference(epsg))
            else:
                arcpy.Project_management(mptsp, output_shp, arcpy.SpatialReference(epsg))

        if wkid in (24877, 24878, 24879):
            zone = wkid - 24860
            epsg = int('327{}'.format(zone))
            p4248_output = os.path.join(temp_folder, 'p4248.shp')
            arcpy.Project_management(mptsp, p4248_output, arcpy.SpatialReference(4248))
            p4326_output = os.path.join(temp_folder, 'p4326.shp')
            arcpy.Project_management(p4248_output, p4326_output, arcpy.SpatialReference(4326))
            arcpy.Project_management(p4326_output, output_shp, arcpy.SpatialReference(epsg))

        if wkid in (32717, 32718, 32719):
            arcpy.CopyFeatures_management(shapefile, output_shp)

        orientation = sheet_orientation(output_shp)
        scale = sheet_scale_map(output_shp)
        size = sheet_size(output_shp)

        output_shp = field_name(output_shp)

        response['response'] = dict()

        response['response']['feature'] = output_shp
        response['response']['orientation'] = orientation
        response['response']['scale'] = scale
        response['response']['size'] = size
        
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
