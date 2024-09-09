# import sys
# reload(sys)
# sys.setdefaultencoding("utf-8")

import arcpy
import settings_aut as st
# import messages_aut as msg
# import traceback
import os
import json
import messages_aut as msg

# gdb_path = arcpy.GetParameterAsText(0)
fila = arcpy.GetParameterAsText(0)
columna = arcpy.GetParameterAsText(1)
cuadrante = arcpy.GetParameterAsText(2)
zoom = arcpy.GetParameterAsText(3)

response = dict()
response['status'] = 1
response['message'] = 'success'

try:

    # st._CUADRICULAS_MG_PATH = os.path.join(gdb_path, st._CUADRICULAS_MG_PATH)
    if not arcpy.Exists(st._CUADRICULAS_MG_PATH):
        raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
    
    query = list()
    field = [st._FILA_FIELD]

    if fila:
        query.append("({} = '{}')".format(st._FILA_FIELD, fila))
        field = [st._COLUMNA_FIELD]
    if columna:
        query.append("({} = '{}')".format(st._COLUMNA_FIELD, columna))
        field = [st._CUADRANTE_FIELD]
    if cuadrante:
        query.append("({} = '{}')".format(st._CUADRANTE_FIELD, cuadrante))
        field = [st._CODHOJA_FIELD]

    query_string = ' AND '.join(query)

    cursor = arcpy.da.SearchCursor(st._CUADRICULAS_MG_PATH, field, query_string)
    codigos = list(set(list(map(lambda i: i[0], cursor))))
    codigos.sort()

    if zoom:
        name_layer = os.path.basename(st._CUADRICULAS_MG_PATH)
        aprx = arcpy.mp.ArcGISProject('current')
        # mxd = arcpy.mapping.MapDocument("CURRENT")
        #dfs = arcpy.mapping.ListDataFrames(mxd)[0]
        map_view = aprx.activeView
        map_active = aprx.activeMap
        camera = map_view.camera
        
        #for Map in aprx.listMaps():
        lyrs = map_active.listLayers("*{}*".format(name_layer))
        if len(lyrs):
            if fila:
                arcpy.SelectLayerByAttribute_management(lyrs[0], "NEW_SELECTION", query_string)
                # df.extent = lyrs[0].getSelectedExtent()
                aprx.activeView.zoomToAllLayers(True)
                #arcpy.SelectLayerByAttribute_management(lyrs[0], "CLEAR_SELECTION")
                if cuadrante:
                    for lyr in lyrs:
                        if lyr.supports("SHOWLABELS"):
                            # el archivo layer de cuadrantes tiene dos clases de etiquetas > main y sec
                            for lblClass in lyr.labelClasses:
                                if lblClass.name=="main":
                                    lblClass.SQLQuery = "{} = '{}'".format(st._CODHOJA_FIELD, fila+columna+cuadrante )
                                    lblClass.expression ="$feature.{}".format(st._CODHOJA_FIELD)
                                elif lblClass.name == "sec":
                                    lblClass.SQLQuery = ""
                                    lblClass.expression =""
                                else:
                                    lblClass.visible = False
                                    lblClass.expression = ""
                                # lblClass.showClassLabels = True
                            lyr.showLabels = True
                    arcpy.SelectLayerByAttribute_management(lyrs[0], "CLEAR_SELECTION")
                #arcpy.RefreshActiveView()
                #arcpy.RefreshTOC()
            else:
                #dfs.extent = lyrs[0].getExtent()
                pass



    response['response'] = ','.join(codigos)
    
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)
    arcpy.SetParameterAsText(4, response)