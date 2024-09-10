#!/usr/bin/env python
# -*- coding: utf-8-sig -*-
# import sys  
# reload(sys)   
# sys.setdefaultencoding('utf8')
import pandas as pd
import numpy as np
import arcpy
import os, math
import matplotlib.pyplot as plt
import matplotlib.lines as mlines
import matplotlib.patches as mpatches
from matplotlib.ticker import (MultipleLocator, AutoMinorLocator, ScalarFormatter)
import json
import traceback
import settings_aut as st
import messages_aut as msg
import uuid

response = dict()
response['status'] = 1
response['message'] = 'success'

# definimos colores de las facies hidroquimicas 
dic_colores = {11: [0, 176, 240],
               12: [50, 74, 178],
               13: [0, 32, 80],
               21: [255, 255, 0],
               22: [255, 102, 0],
               23: [154, 105, 0],
               31: [86, 130, 0],
               32: [0, 157, 113],
               33: [0, 128, 0]}
dic_colores_porc = {index: list(map(lambda x: x / 255.0, value)) for index, value in dic_colores.items()}


def coordenada(Na, K, Ca, Cl, HCO3, TDS, fuente, fachidro, ion):
    if HCO3 == 0:
        return 0
    if ion == u'Na':
        x = (Na + K) / (Na + K + Ca)
    else:
        x = Cl / (Cl + HCO3)
    if fuente == u'TIP_FT_SUB':
        marcador = 'o'
    else:
        marcador = '^'
    y = TDS
    color_fac_hidro = np.array([dic_colores_porc[int(fachidro)]])
    print color_fac_hidro
    print type(color_fac_hidro)
    print "x"
    plt.scatter(x, y, zorder=1, c=color_fac_hidro, s=200, marker=marcador, edgecolors='#4b4b4b')


def gibbs(csv_file, temporada, ion, cuenca,subcuenca, microcuenca=None):
    _scratch = arcpy.env.scratchFolder
    # name_fig = u'gibbs_{}_{}_{}_{}.png'.format(subcuenca,microcuenca,ion, temporada)
    

    cuenca = cuenca if cuenca != '0' else ''
    subcuenca = subcuenca if subcuenca != '0' else ''
    microcuenca = microcuenca if microcuenca != '0' else ''

    name_fig = 'gibbs_{}.png'.format(uuid.uuid4().hex)
    path_fig = os.path.join(_scratch, name_fig)
    # temporada = unicode(temporada)
    # ion = unicode(ion)
    # subcuenca = unicode(subcuenca)
    df = pd.read_csv(csv_file, sep = ';' , encoding= 'utf-8-sig')
    path_csv = os.path.join(_scratch, csv_file)
    # df = pd.read_csv(path_csv, sep=';', encoding='latin-1')


    df = df[["Na_MEQL", "K_MEQL", "Ca_MEQL", "ION_Cl_MEQL", "HCO3_MEQL", "TIP_FUENTE", "TDS", "CU_IN_HIDR", "SUBCUENCA", "MICROCU",
             "TEMPORADA", "FAC_HIDRO","COD_CUENCA","COD_SUBC","COD_MICROC"]].copy()

    # df = df[(df["COD_SUBC"].astype(str) == subcuenca) & (df["TEMPORADA"] == temporada)]

    df = df[df["TEMPORADA"].astype(str)==temporada]
    
    if cuenca != '':
        df = df[df["COD_CUENCA"].astype(str)==cuenca]

    if subcuenca != '':
        df = df[df["COD_SUBC"].astype(str)==subcuenca]

    if microcuenca:
        if microcuenca != '':
            df = df[(df["COD_MICROC"].astype(str) == microcuenca)]
    #     df = df[(df["MICROCU"]==microcuenca) & (df["TEMPORADA"]==temporada)]
    
    df = df.dropna(subset=["TDS"])
    
    # iniciamos el grafico
    f, ax = plt.subplots(figsize=(18, 15))
    plt.title("Temporada de {}".format(temporada))
    ttl = ax.title
    ttl.set_position([.5, 1.05])
    ttl.set_size(24)
    
    # graficamos los puntos
    for index, row in df.iterrows():
        coordenada(row['Na_MEQL'], row['K_MEQL'], row['Ca_MEQL'], row['ION_Cl_MEQL'], row["HCO3_MEQL"], row["TDS"],
                   row["TIP_FUENTE"], row["FAC_HIDRO"], ion)
    
    # Agregamos lineas entrecortadas
    plt.plot([0, 0.9], [900, 60000], linestyle='--', color='#6e914b', linewidth=3)
    plt.plot([0, 0.9], [100, 2], linestyle='--', color='#6e914b', linewidth=3)
    
    plt.plot([0.48, 0.9], [300, 3000], linestyle='--', color='#6e914b', linewidth=3)
    plt.plot([0.48, 0.9], [300, 20], linestyle='--', color='#6e914b', linewidth=3)
    
    # Agregamos textos
    font = {'family': 'serif',
            'color': '#6e914b',
            'weight': 'normal',
            'size': 25,
            'style': 'italic'
            }
    plt.text(0.1, 600, 'Rock\nDominance', fontdict=font)
    plt.text(0.54, 3500, 'Evaporation\nDominance', fontdict=font, rotation=25)
    plt.text(0.54, 11, 'Precipitation\nDominance', fontdict=font, rotation=335)
    
    font_ejes = {'family': 'serif',
                 'color': '0',
                 'weight': 'normal',
                 'size': 24,
                 }
    
    texto_x = '(Na + K) / (Na + K + Ca)'
    if ion == 'Cl':
        texto_x = 'CL / (CL + HCO3)'
    
    texto_y = msg._MHQ_TEXTO_Y
    
    plt.xlabel(texto_x, fontdict=font_ejes, labelpad=10)
    plt.ylabel(texto_y, fontdict=font_ejes, labelpad=10)
    
    # plt.axis('square')
    plt.grid()
    plt.xlim(0, 1.2)
    plt.ylim(1, 10 ** 5)
    
    plt.yscale("log")
    # plt.gca().set_aspect(1/100, adjustable='datalim')
    # plt.tick_params(direction='out', length=6, width=2, colors='r',
    #                grid_color='r', grid_alpha=0.5)
    
    # linea para dejar de usar la notacion cientifica
    ax.yaxis.set_major_formatter(ScalarFormatter())
    
    ax.tick_params(which='both', direction='inout')
    ax.tick_params(which='major', width=2, length=7, labelsize=16)
    # ax.xaxis.set_minor_locator(MultipleLocator(0.04))
    
    
    ax.xaxis.set_minor_locator(AutoMinorLocator(5))
    ax.tick_params(which='minor', length=7)

    if os.path.exists(path_fig):
        os.remove(path_fig)

    plt.savefig(path_fig, bbox_inches='tight', pad_inches=0.3)
    return path_fig
    
    plt.show()

if __name__ == '__main__':
    # path_csv = r"D:\JYUPANQUI\PROYECTOS\dgar\piper\prueba_export_dom2.csv"
    # path_csv = r"d:\Users\JORGELUISYH\Documents\ArcGIS\scratch\datos_graf.csv"
    path_csv = arcpy.GetParameterAsText(0)
    # path_csv = r"D:\jyupanqui\proyectos\dgar\datos_graf.csv"
    # temporada = arcpy.GetParameterAsText(1)
    # ion = arcpy.GetParameterAsText(2)
    cuenca = arcpy.GetParameterAsText(1)
    subcuenca = arcpy.GetParameterAsText(2)
    microcuenca = arcpy.GetParameterAsText(3)


    try:
        # Insertar procesos 
        # img_file = gibbs(path_csv, temporada, ion, cuenca, subcuenca, microcuenca)

        img_gibbs1 = gibbs(path_csv, 'Estiaje', u'Cl', cuenca, subcuenca, microcuenca)
        img_gibbs2 = gibbs(path_csv, 'Avenida', u'Cl', cuenca, subcuenca, microcuenca)

        img_gibbs3 = gibbs(path_csv, 'Estiaje', u'Na', cuenca, subcuenca, microcuenca)
        img_gibbs4 = gibbs(path_csv, 'Avenida', u'Na', cuenca, subcuenca, microcuenca)

        img_files =[img_gibbs1, img_gibbs2, img_gibbs3, img_gibbs4]

        mxd = arcpy.mapping.MapDocument("current")
        elm_gibbs_1 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_estiaje_cl*')[0]
        elm_gibbs_2 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_avenida_cl*')[0]
        elm_gibbs_3 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_estiaje_na*')[0]
        elm_gibbs_4 = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT",'*gibbs_avenida_na*')[0]
        elm_gibbs_1.sourceImage = img_gibbs1
        elm_gibbs_2.sourceImage = img_gibbs2
        elm_gibbs_3.sourceImage = img_gibbs3
        elm_gibbs_4.sourceImage = img_gibbs4
        arcpy.RefreshActiveView()
        mxd.save()


        response["response"] = ','.join(img_files)
        arcpy.AddMessage("Hello World!")
    except Exception as e:
        response['status'] = 0
        # response['message'] = e.message 
        response['message'] = traceback.format_exc()
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(4, response)