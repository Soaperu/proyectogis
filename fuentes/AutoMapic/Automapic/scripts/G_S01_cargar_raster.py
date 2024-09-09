import arcpy
import settings_aut as st
import os

arcpy.env.addOutputsToMap = True

raster = arcpy.GetParameterAsText(0)

state, message, response = 0, str(), str()

try:        
    state, message, response = 1, 'success', feature
    
except Exception as e:
    message = e.message
finally:
    arcpy.SetParameterAsText(1, raster)
    arcpy.SetParameterAsText(2, response)
