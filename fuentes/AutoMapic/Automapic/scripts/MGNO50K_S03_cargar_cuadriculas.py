import arcpy
import settings_aut as st
import os
import json
import messages_aut as msg
import traceback

arcpy.env.addOutputsToMap = True

# gdb_path = arcpy.GetParameterAsText(0)

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    feature = arcpy.GetParameterAsText(0)

    mxd = arcpy.mapping.MapDocument("CURRENT")
    df= arcpy.mapping.ListDataFrames(mxd, "*")[0] 
    layer = arcpy.mapping.Layer(feature)
    desc_lyr= arcpy.Describe(layer)
    layer_extent = desc_lyr.extent
    df.extent = layer_extent
    mxd.activeDataFrame.scale *= 1.2
    mxd.activeDataFrame.spatialReference = arcpy.SpatialReference(4326)
    arcpy.RefreshActiveView()
    arcpy.RefreshTOC()
    
    lista_capas =[st._CUADRICULAS_MG_PATH, st._DGR_ULITO_50K_, st._DGR_POG_50K]
    for capa in lista_capas:
        name_layer = os.path.basename(capa)
        layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(name_layer))
        if layers:
            for i in layers:
                arcpy.mapping.RemoveLayer(mxd.activeDataFrame, i)

        if not arcpy.Exists(capa):
            raise RuntimeError(capa)

    # lista_hojas = [x[0] for x in arcpy.da.SearchCursor(mk_lyr_hojas,[st._CODHOJA_FIELD])]
    # response['codes'] = ",".join(lista_hojas)

except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()
finally:
    response = json.dumps(response)
    #arcpy.SetParameterAsText(1, st._DGR_ULITO_50K_)
    #arcpy.SetParameterAsText(2, st._DGR_POG_50K)
    arcpy.SetParameterAsText(1, feature)
    arcpy.SetParameterAsText(2, st._CUADRICULAS_MG_PATH)
    arcpy.SetParameterAsText(3, response)
