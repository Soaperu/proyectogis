# -*- coding: utf-8 -*-
import sys
reload(sys)
sys.setdefaultencoding("utf-8")
import settings_aut as st
import arcpy
import json
import traceback
import matplotlib.pyplot as plt
import mplstereonet as stereo
import uuid
import os
import numpy as np
import math

response = dict()
response['status'] = 1
response['message'] = 'success'
_scratch = arcpy.env.scratchFolder
#print _scratch
param = sys.argv[1]#arcpy.GetParameterAsText(0) #

try:
    # Make a figure with a single stereonet axes
    fig, ax = stereo.subplots()
    checkeds = param.split("_")[0]
    checked_polo = checkeds[0]
    checked_rake = checkeds[1]
    data = param.split("_")[1].split("\\n")
    str_strike, str_dip = data[:2]
    strikes = [int(x) for x in str_strike.split(";")]
    dips = [int(x.replace("\\","")) for x in str_dip.split(";")]

    # Plot the planes.
    ax.plane(strikes, dips)

    # Plot rake
    if len(data[2].split(";")) > 0 and checked_rake == "1":
        str_rake= data[2]
        rakes= [int(x) for x in str_rake.split(";")]
        ax.rake(strikes, dips, rakes)
        x,y = stereo.rake(strikes, dips, rakes)
        mag = np.hypot(x,y)
        u,v = x / mag, y/mag
        # Plot arrows
        ax.quiver(x,y,u,v, width=2, headwidth=5, units='dots', minlength=10, zorder=2.5)

    # Make only a single "N" azimuth tick label.
    ax.set_azimuth_ticks([0], labels=['N'])
    if checked_polo == "1":
        ax.pole(strikes, dips, 'go', markersize=2)
    ax.grid()
    diagram ='diagrama_esfuerzo_{}.png'.format(uuid.uuid4().hex)
    path_diagram = os.path.join(_scratch, diagram)
    plt.savefig(path_diagram, bbox_inches='tight')
    response['response']= path_diagram
    print(path_diagram)
    del stereo
except Exception as e:
    response['status'] = 0
    response['message'] = traceback.format_exc()#e.message
    print(traceback.format_exc())
finally:
    response = json.dumps(response)#, default=str)#, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)