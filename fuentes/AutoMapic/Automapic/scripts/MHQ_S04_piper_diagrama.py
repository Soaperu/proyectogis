#!/usr/bin/env python
# -*- coding: utf-8-sig -*-
# import sys  
# reload(sys)   
# sys.setdefaultencoding('utf8')
import pandas as pd
import numpy as np
import os, math
import matplotlib.pyplot as plt
# import imageio
import matplotlib.lines as mlines
import matplotlib.patches as mpatches
import matplotlib as mpl
import arcpy
import settings_aut as st
import messages_aut as msg
import json
import uuid


response = dict()
response['status'] = 1
response['message'] = 'success'

mpl.rcParams['legend.numpoints'] = 1
# from matplotlib import rc
# # Activar LatEx
# rc('text', usetex = True)

# diccionarios que sirven para gestionar etiquetas, marcadores y colores
dic_microcuencas_color = dict()
dic_microcuencas_label = dict()
dict_tip_fuente= msg._TIP_FUENTES_MHQ
dic_colores = {1:[241,103,130],
              2:[0,184,165],
              3:[179,130,186],
              4:[247,173,195],
              5:[151,213,201],
              6:[191,228,118],
              7:[255,240,90]}
dic_colores_porc = {index: list(map(lambda x: x/255.0, value)) for index, value in dic_colores.items()}
index_color = 0

#funcion de las coordenadas
def coordenada(Ca,Mg,Cl,SO4,Label,microcuenca):
    global index_color
    global dic_microcuencas_color
    global dic_microcuencas_label
    xcation = 40 + 360 - (Ca + Mg / 2) * 3.6
    ycation = 40 + (math.sqrt(3) * Mg / 2)* 3.6
    xanion = 40 + 360 + 100 + (Cl + SO4 / 2) * 3.6
    yanion = 40 + (SO4 * math.sqrt(3) / 2)* 3.6
    xdiam = 0.5 * (xcation + xanion + (yanion - ycation) / math.sqrt(3))
    ydiam = 0.5 * (yanion + ycation + math.sqrt(3) * (xanion - xcation))
    #print(str(xanion) + ' ' + str(yanion))
      
    if Label == 'TIP_FT_SUP':
        marcador = "^"
    else:
        marcador = "o"
        
    label_symbol = Label+"_"+marcador
    
    if not dic_microcuencas_label.get(microcuenca):
        dic_microcuencas_label[microcuenca] = [label_symbol]
    else :
        if label_symbol not in dic_microcuencas_label[microcuenca]:
            dic_microcuencas_label[microcuenca].append(label_symbol)
    if dic_microcuencas_color.get(microcuenca) is None:
#         color =np.random.rand(3,1).ravel()
#         color=np.array([color])
        index_color += 1
        color = dic_colores_porc[index_color]
        ## descomentar para python 3
        # color=np.array([color])
        dic_microcuencas_color[microcuenca] = color
    else:
        color = dic_microcuencas_color.get(microcuenca)

    
#     c=np.random.rand(3,1).ravel()
    listagraph=[]
    # listagraph.append(plt.scatter(xcation,ycation,zorder=1,c=color, s=60,marker=marcador))
    plt.scatter(xcation,ycation,zorder=1,c=color, s=150,marker=marcador)
    listagraph.append(plt.scatter(xanion,yanion,zorder=1,c=color, s=150,marker=marcador))
    listagraph.append(plt.scatter(xdiam,ydiam,zorder=1,c=color, s=150,marker=marcador))
#     listagraph.append(plt.scatter(xdiam,ydiam,zorder=1,c=color, s=60,marker=marcador, edgecolors='#4b4b4b'))
#     listagraph.append(plt.scatter(xdiam,ydiam,zorder=1,c=color, s=60,marker=marcador, edgecolors='none'))

    return listagraph


def diagrama_piper(csv_file, temporada,cuenca,subcuenca,microcuenca=None):
    global index_color
    index_color =0

    cuenca = cuenca if cuenca != '0' else ''
    subcuenca = subcuenca if subcuenca != '0' else ''
    microcuenca = microcuenca if microcuenca != '0' else ''

    f, ax = plt.subplots(figsize=(20,15))
    img =plt.imread(st._IMG_THICK_PIPER)
    path_csv = csv_file

    _scratch = arcpy.env.scratchFolder
    # name_fig = 'piper_{}_{}_{}.png'.format(subcuenca,microcuenca,temporada)
    name_fig = 'piper_{}.png'.format(uuid.uuid4().hex)
    path_fig = os.path.join(_scratch, name_fig)
    # cuando el texto es modificado desde el aplicativo excel
    # df = pd.read_csv(path_csv, sep = ';' , encoding= 'latin-1')
    # cuando el csv es generado automaticamente
    df = pd.read_csv(path_csv, sep = ';' , encoding= 'utf-8-sig')
    df = df[["Ca_MEQP", "Mg_MEQP", "Cl_MEQP", "SO4_MEQP", "TIP_FUENTE","CU_IN_HIDR","SUBCUENCA","MICROCU","TEMPORADA",
            "COD_CUENCA","COD_SUBC","COD_MICROC"]].copy()
    

    df = df[df["TEMPORADA"].astype(str)==temporada]
    
    if cuenca != '':
        df = df[df["COD_CUENCA"].astype(str)==cuenca]

    if subcuenca != '':
        df = df[df["COD_SUBC"].astype(str)==subcuenca]
    # df = df[(df["COD_SUBC"].astype(str)==subcuenca) & (df["TEMPORADA"]==temporada)]

    if microcuenca:
        if microcuenca != '':
            df = df[(df["COD_MICROC"].astype(str) == microcuenca)]
#     df = df[(df["MICROCU"]=='Hornillos')|(df["MICROCU"]=='Apurímac 1')|(df["MICROCU"]=='Apurímac 2')]
    
#     df=df.set_index("Código Corto")
    df.head()
    
    # plt.figure(figsize=(20,15))
    plt.title(temporada, fontsize=25)

    plt.imshow(np.flipud(img),zorder=0)
    for index, row in df.iterrows():
        coordenada(row['Ca_MEQP'],row['Mg_MEQP'],row['Cl_MEQP'],row['SO4_MEQP'],row["TIP_FUENTE"],row["MICROCU"])
    plt.ylim(0,830)
    plt.xlim(0,900)
    plt.axis('off')
    # plt.legend(loc='upper right',prop={'size':10}, frameon=False, scatterpoints=1)
    
    # Leyenda
    etiquetas= []
    for micro in sorted(dic_microcuencas_color.keys()):
        white_patch = mpatches.Patch(color='1', label=micro)
        etiquetas.append(white_patch)
        for value in dic_microcuencas_label[micro]:
        	## descomentar para python 3
            # colorcito = tuple(dic_microcuencas_color[micro].tolist()[0])
            color_leyenda = dic_microcuencas_color[micro]
            fuente = u'{}'.format(dict_tip_fuente.get(value[:-2]))
            etiqueta = mlines.Line2D([], [], color=color_leyenda, marker=value[-1],
                              markersize=15, label=fuente ,linestyle = 'None')
            etiquetas.append(etiqueta)

    ax.legend(handles=etiquetas, title = "Microcuencas", fontsize=15)
    ax.get_legend().get_title().set_fontsize(20)
    # plt.legend(handles=etiquetas, title = "Microcuencas")

    if os.path.exists(path_fig):
        os.remove(path_fig)

    # plt.savefig(path_fig, dpi=600, bbox_inches='tight', pad_inches=0)
    plt.savefig(path_fig, bbox_inches='tight', pad_inches=0.3)

    # plt.savefig('jorgito_piper.png')
    # plt.show()
    dic_microcuencas_color.clear()
    dic_microcuencas_label.clear()
    return path_fig


if __name__ == '__main__':
    # path_csv = r"D:\JYUPANQUI\PROYECTOS\dgar\piper\prueba_export_dom2.csv"
    path_csv = arcpy.GetParameterAsText(0)
    # temporada = arcpy.GetParameterAsText(1)
    cuenca = arcpy.GetParameterAsText(1)
    subcuenca = arcpy.GetParameterAsText(2)
    microcuenca = arcpy.GetParameterAsText(3)


    try:
        # Insertar procesos 
        # img_file = diagrama_piper(path_csv, temporada, cuenca, subcuenca, microcuenca)
        # path_csv = r"D:\jyupanqui\proyectos\dgar\datos_graf.csv"
        # cuenca = '134'
        # subcuenca= '13491'
        # microcuenca= '13491_02'

        img_piper1 = diagrama_piper(path_csv, 'Estiaje', cuenca, subcuenca, microcuenca)
        img_piper2 = diagrama_piper(path_csv, 'Avenida', cuenca, subcuenca, microcuenca)
        img_files = [img_piper1, img_piper2]

        mxd = arcpy.mapping.MapDocument("current")
        elm_piper_1 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*piper_estiaje*')[0]
        elm_piper_2 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*piper_avenida*')[0]
        
    
        elm_piper_1.sourceImage = img_piper1
        elm_piper_2.sourceImage = img_piper2
        arcpy.RefreshActiveView()
        mxd.save()

        
        
        response["response"] = ','.join(img_files)
        arcpy.AddMessage("Hello World!")
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(4, response)