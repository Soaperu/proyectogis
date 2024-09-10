# import sys
# reload(sys)
# sys.setdefaultencoding('ISO-8859-1')

import arcpy
import json
import settings_aut as st
import messages_aut as msg
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
from matplotlib.offsetbox import TextArea, DrawingArea, OffsetImage, AnnotationBbox
import PG_S01_mapa_peligros_geologicos as mpg
# from datetime import datetime
import uuid
import os


tamanio = arcpy.GetParameterAsText(0)
autores = arcpy.GetParameterAsText(1)
titulo1 = arcpy.GetParameterAsText(2)
titulo2 = arcpy.GetParameterAsText(3)
titulo2 = titulo2.upper()
numero = arcpy.GetParameter(4)
numero = int(numero) if numero.is_integer() else numero
# year = datetime.today().year

mxd = arcpy.mapping.MapDocument("CURRENT")
escala = mxd.activeDataFrame.scale
wkid = str(mxd.activeDataFrame.spatialReference.factoryCode)

if wkid == '4326':
    datum = 'WGS 84 GCS'
elif wkid == '4248':
    datum = 'PSAD 56 GCS'
elif '327' in str(wkid):
    datum = 'WGS 84 Zona {}Sur'.format(wkid[3:])
elif '248' in str(wkid):
    datum = 'PSAD 56 Zona 1{}Sur'.format(wkid[-1])

cm = 1/2.54

# nombre_dgar = msg._NAME_DGAR
# nombre_ingemmet = 'INSTITUTO GEOL\xc3\x93GICO MINERO Y METAL\xc3\x9aRGICO'
# nombre_sector = 'SECTOR ENERG\xc3\x8dA Y MINAS'

border_color = '#000000'
font_TimesNewRoman = 'Times New Roman'
font_Arial = 'Arial'

response = dict()
response['status'] = 1
response['message'] = 'success'

try:
    if tamanio == '1':
        fontsize_titulo1 = 6
        fontsize_titulo2 = 7
        fontisze_numero = 24
        fontsize_figura = 6.2
        fontsize_detalle = 6.4
        fontsize_nombre_dgar = 6
        fontsize_nombre_ingemmet = 3
        fontsize_nombre_sector = 3

        # :Dimensiones de rotulo
        w, h = 7 * cm, 3.5* cm
        w_numero, h_numero = 1.5 * cm, 1.6 * cm
        w_titulo1, h_titulo1 = w, 0.8 * cm
        w_detalle, h_detalle = w - w_numero, 0.8 * cm
        
        # :PlotObject
        fig, ax = plt.subplots(figsize=(w, h))

        # :Limites de dibujo
        ax.set_xlim(0, w)
        ax.set_ylim(0, h)

        # :Bordes
        ax.vlines([0, w], ymin=0, ymax=h, color=border_color, linewidth=1)                             # Bordes verticales
        ax.hlines([0, h], xmin=0, xmax=w, color=border_color, linewidth=1)                             # Bordes horizontales
        ax.vlines([w - w_numero], ymin=0, ymax=h_numero, color=border_color, linewidth=0.5)            # Borde vertical de la seccion "numero de mapa"
        ax.hlines([h_numero, h_numero + h_titulo1], xmin=0, xmax=w, color=border_color, linewidth=0.5) # Bordes horizontales de la seccion "titulo 1"
        ax.hlines([h_detalle], xmin=0, xmax=w_detalle, color=border_color, linewidth=0.5)              # Borde horizontal de la seccion "detalle"

        # :Texto
        titulo1 = mpg.set_detalle(titulo1, 35, first_line=True)
        plt.annotate(titulo1, (3.5*cm, 2.0*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_titulo1)
        titulo2 = mpg.set_detalle(titulo2, 20, first_line=True)
        plt.annotate(titulo2, (2.75*cm, 1.2*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_titulo2)
        plt.annotate(msg._NAME_DGAR, (3.5*cm, 2.55*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_nombre_dgar)
        plt.annotate(msg._NAME_INGEMMET, (2.3*cm, 2.77*cm), fontname=font_Arial, weight='bold', fontsize=fontsize_nombre_ingemmet)
        plt.annotate(msg._NAME_SECTOR, (2.3*cm, 3.3*cm), fontname=font_Arial, weight='bold', fontsize=fontsize_nombre_sector)
        
        arr_logo = mpimg.imread(st._IMG_LOGO_INGEMMET)
        imagebox = OffsetImage(arr_logo, zoom=0.15)

        ab = AnnotationBbox(imagebox, (3.5*cm, 3*cm), frameon=False)

        ax.add_artist(ab)


        plt.annotate(numero, (6.25*cm, 0.35*cm), ha='center', fontname=font_Arial, weight='regular', fontsize=fontisze_numero)
        plt.annotate('FIGURA', (6.25*cm, 1.25*cm), ha='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_figura)

        detalle = u'Escala:  1/ {} Datum: {}\n{}'.format(escala, datum, msg._DETALLE_ROTULO)

        plt.annotate(detalle, (2.75*cm, 0.4*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='regular', fontsize=6.4)

    else:
        fontsize_titulo1 = 12
        fontsize_titulo2 = 20
        fontisze_numero = 48
        fontsize_figura = 12.4
        fontsize_escala = 12
        fontsize_autor = 12
        fontsize_autor_value = 11
        fontsize_datum = 12
        fontsize_detalle = 12
        fontsize_nombre_dgar = 12
        fontsize_nombre_ingemmet = 6
        fontsize_nombre_sector = 6

        # :Dimensiones de rotulo
        w, h = 14 * cm, 9* cm
        w_numero, h_numero = 3 * cm, 3.2 * cm
        w_titulo1, h_titulo1 = w, 1.5 * cm
        w_titulo2, h_titulo2 = w, 1.6 * cm
        w_escala, h_escala = 5.6 * cm, 1.4 * cm
        w_autor, h_autor = w - w_numero - w_escala, 1.4 * cm
        w_datum, h_datum = w - w_numero, 0.9 * cm
        w_detalle, h_detalle = w - w_numero, 0.9 * cm
        
        # :PlotObject
        fig, ax = plt.subplots(figsize=(w, h))

        # :Limites de dibujo
        ax.set_xlim(0, w)
        ax.set_ylim(0, h)

        # :Bordes
        ax.vlines([0, w], ymin=0, ymax=h, color=border_color, linewidth=1)
        ax.hlines([0, h], xmin=0, xmax=w, color=border_color, linewidth=1)
        ax.hlines([h - 2.7 * cm], xmin=0, xmax=w, color=border_color, linewidth=0.5)
        ax.vlines([w - w_numero], ymin=0, ymax=h_numero, color=border_color, linewidth=0.5)
        ax.vlines([w_escala], ymin=h_datum + h_detalle, ymax=h_datum + h_detalle + h_escala, color=border_color, linewidth=0.5)
        ax.hlines([h_numero, h_numero + h_titulo2], xmin=0, xmax=w, color=border_color, linewidth=0.5)
        ax.hlines([h_detalle, h_detalle + h_datum], xmin=0, xmax=w_detalle, color=border_color, linewidth=0.5)

        titulo1 = mpg.set_detalle(titulo1, 35, first_line=True)
        plt.annotate(titulo1, (w/2, 5.5*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_titulo1)
        titulo2 = mpg.set_detalle(titulo2, 20, first_line=True)
        plt.annotate(titulo2, (w/2, 3.95*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_titulo2)
        plt.annotate(msg._NAME_DGAR, (w/2, 6.8*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_nombre_dgar)
        plt.annotate(msg._NAME_INGEMMET, (4.8*cm, 7.2*cm), fontname=font_Arial, weight='bold', fontsize=fontsize_nombre_ingemmet)
        plt.annotate(msg._NAME_SECTOR, (4.8*cm, 8.4*cm), fontname=font_Arial, weight='bold', fontsize=fontsize_nombre_sector)

        arr_logo = mpimg.imread(st._IMG_LOGO_INGEMMET)
        imagebox = OffsetImage(arr_logo, zoom=0.31)

        ab = AnnotationBbox(imagebox, (0.1 + w/2, 7.7*cm), frameon=False)

        ax.add_artist(ab)

        plt.annotate(numero, (w - (w_numero/2), 0.6*cm), ha='center', fontname=font_Arial, weight='regular', fontsize=fontisze_numero)
        plt.annotate('Mapa:', (w - (w_numero/2), 2.3*cm), ha='center', fontname=font_TimesNewRoman, weight='bold', fontsize=fontsize_figura) 

        plt.annotate('Escala:', (w_escala/2, 2.8*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='regular', fontsize=fontsize_escala)
        plt.annotate('1: {}'.format(escala), (w_escala/2, 2.2*cm), ha='center', va='center', fontname=font_Arial, weight='regular', fontsize=fontsize_escala)
        plt.annotate('Elaborado por:', (w_escala + (w_autor/2.0), 2.8*cm), ha='center', va='center', fontname=font_TimesNewRoman, weight='regular', fontsize=fontsize_autor)
        plt.annotate(autores.upper(), (w_escala + (w_autor/2.0), 2.2*cm), ha='center', va='center', fontname=font_Arial, weight='regular', fontsize=fontsize_autor_value)
        plt.annotate('Datum: {}'.format(datum), (w_datum/2.0, h_detalle + h_datum/2.0), ha='center', va='center', fontname=font_TimesNewRoman, weight='regular', fontsize=fontsize_datum)
        plt.annotate(msg._DETALLE_ROTULO, (w_datum/2.0, h_detalle/2.0), ha='center', va='center', fontname=font_TimesNewRoman, weight='regular', fontsize=fontsize_detalle)

    plt.subplots_adjust(top=1, bottom=0, right=1, left=0, hspace=0, wspace=0)
    plt.margins(0,0)
    plt.gca().xaxis.set_major_locator(plt.NullLocator())
    plt.gca().yaxis.set_major_locator(plt.NullLocator())
    plt.axis('off')
    extent = ax.get_window_extent().transformed(fig.dpi_scale_trans.inverted())
    rotulo_path = os.path.join(st._TEMP_FOLDER, 'rotulo_{}.png'.format(uuid.uuid4().hex))
    plt.savefig(rotulo_path, dpi=600, bbox_inches='tight', pad_inches=0)

    response["response"] = rotulo_path
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(5, response)
