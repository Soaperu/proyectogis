import arcpy
import json
import os

arcpy.env.overwriteOutput = True

in_dema_name = arcpy.GetParameterAsText(0)
in_query_string = arcpy.GetParameterAsText(1)
response = None

temp_folder = r"c:\bdgeocatmin\temporal"
path_aprx_plantillas = r"C:\bdgeocatmin\template\mapasPro\mapas.aprx"

def exportarImagenDemarcacion(dema_name, query_string):    
    

    aprx = arcpy.mp.ArcGISProject(path_aprx_plantillas)
    lyt = aprx.listLayouts(f"{dema_name}Layout")[0]    
    salidapng = f"{temp_folder}\{dema_name}.png"
    os.remove(salidapng)

    map = aprx.listMaps(f"{dema_name}Map")[0]
    lyr = map.listLayers(f"{dema_name}")[0]
    lyr.definitionQuery= query_string
    
    map_frame = lyt.listElements("MAPFRAME_ELEMENT", "Marco de mapa")[0]
    map_frame.camera.setExtent(map_frame.getLayerExtent(lyr, False, True))
    lyt.exportToPNG(salidapng,150, '24-BIT_TRUE_COLOR')
    return salidapng


if __name__ == '__main__':
    try:
        out_png = exportarImagenDemarcacion(in_dema_name, in_query_string)
        response = out_png
        arcpy.AddMessage("Satisfactorio")
    except Exception as e:
        arcpy.AddError("Error detalle: " + str(e))
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(2, response)