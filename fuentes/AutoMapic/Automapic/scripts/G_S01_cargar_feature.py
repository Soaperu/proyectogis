import arcpy
import settings_aut as st
import os
import os

arcpy.env.addOutputsToMap = True

feature = arcpy.GetParameterAsText(0)
symbology = arcpy.GetParameterAsText(1)
out_feature = arcpy.GetParameterAsText(2)

state, message, response = 0, str(), str()

try:
    params = arcpy.GetParameterInfo()

    if params[1]:
        if not symbology.endswith(st._EXT_LAYER):
            symbology = '{}{}'.format(symbology, st._EXT_LAYER)
        params[2].symbology = os.path.join(st._LAYERS_DIR, symbology)
        
    state, message, response = 1, 'success', feature

    mxd = arcpy.mapping.MapDocument("CURRENT")
    desc = arcpy.Describe(feature)
    mxd.activeDataFrame.extent = desc.extent
    arcpy.RefreshActiveView()
    arcpy.RefreshTOC()
    
except Exception as e:
    message = e.message
finally:
    arcpy.SetParameterAsText(3, state)
    arcpy.SetParameterAsText(4, message)
    arcpy.SetParameterAsText(5, response)
