# Importar librerias
import arcpy
import json
import os
import settings_aut as st
import uuid
import matplotlib.pyplot as plt
import arcobjects as arc
from matplotlib.offsetbox import TextArea, DrawingArea, OffsetImage, AnnotationBbox
import messages_aut as msg
from PG_S01_mapa_peligros_geologicos import set_detalle
import uuid
import packages_aut as pkg
from MHG_S07_cortar_features_por_cuencas import split_data_by_polygon


arcpy.env.overwriteOutput = True

response = dict()
response['status'] = 1
response['message'] = 'success'

cd_depa = arcpy.GetParameterAsText(0)       # Codigo de la departamento (string requerido)
orientacion = arcpy.GetParameter(1)   # 1: Horizontal, 0: Vertical (integer requerido)
zona = arcpy.GetParameter(2)          # Zona geografica 17, 18, 19 (integer requerido)
escala = arcpy.GetParameter(3)        # 25000, 50000, 100000... (integer requerido)
autores = arcpy.GetParameterAsText(4)       # "Juanito, Perez, Lui, ... " (string opcional)
numero_registros = int(arcpy.GetParameterAsText(5))  # Numero de registros por cuadro de interes geologico (integer requerido)

# cd_depa = '21'       # Codigo de la departamento (string requerido)
# orientacion = 2   # 1: Horizontal, 0: Vertical (integer requerido)
# zona = 19        # Zona geografica 17, 18, 19 (integer requerido)
# escala = 500000       # 25000, 50000, 100000... (integer requerido)
# autores = 'Daniel A., Jorge Y.'      # "Juanito, Perez, Lui, ... " (string opcional)
# numero_registros = 150  # Numero de registros por cuadro de interes geologico (integer requerido)

_DFMAPAPRINCIPAL = "DFMAPAPRINCIPAL"
_DFMAUBICACION = "DFMAPAUBICACION"
_ELM_TITLE = "ELM_TITLE"
_ELM_TITLE_ORIG = "ELM_TITLE_ORIG"
_ELM_AUTOR = "ELM_AUTOR"
# _ELM_AUTOR_ORIG = "ELM_AUTOR_ORIG"
_ELM_DATUM = "ELM_DATUM"
# _ELM_DATUM_ORIG = "ELM_DATUM_ORIG"
_ELM_TABLA = "ELM_TABLA_{}"
_ELM_IMG = "ELM_IMG_"


def generate_table_interes_geologico(data, nm_depa):
    cm = 1/2.54
    border_color = '#000000'
    font_times_new_roman = 'Times New Roman'
    font_size_rows = 9
    font_size_title = 12
    max_character_by_line = 34
    
    # Dimensiones de la tabla
    # Ancho de la tabla por columnas
    w_colum_1 = 1.5 * cm    # codigo
    w_colum_2 = 5 * cm    # denominacion de sitio de interes
    w_colum_3 = 4 * cm    # interes geologico principal
    w_colum_4 = 1 * cm      # valoracion cientifica (VC)
    w_colum_5 = 1 * cm      # valoracion didactica (VD)
    w_colum_6 = 1 * cm      # valoracion turistica (VT)
    w_columns = [w_colum_1, w_colum_2, w_colum_3, w_colum_4, w_colum_5, w_colum_6]

    # :Altura total del cuadro 
    h, w = int(), int()

    # Altura de una fila cuando el texto es de una linea
    h_row = 0.5 * cm

    # Altura de la fila donde se situa el titulo
    h_row_title = 0.6 * cm

    # Altura de los nombres de columna
    h_row_header = 1 * cm

    # Altura por registros
    rows = list()
    for i in data:
        txt = set_detalle(i[1], max_character_by_line)
        h_row_det = len(txt.split('\n')) * h_row
        row = i + [txt, h_row_det]
        rows.append(row)
        h = h + h_row_det
    
    # Dimensiones totales
    h, w = h + h_row_title + h_row_header, sum(w_columns)

    # Configurando las dimensiones del grafico
    fig, ax = plt.subplots(figsize=(w, h))

    # Contruir header
    # :Limites de dibujo
    ax.set_xlim(0, w)
    ax.set_ylim(0, h)

    # Ejes verticales y horizontales
    ax.vlines([0, w], ymin=0, ymax=h, color=border_color, linewidth=1)                                                  # Bordes verticales
    ax.vlines([sum(w_columns[:i+1]) for i in range(len(w_columns) - 1)], ymin=0, ymax=h - h_row_title, color=border_color, linewidth=0.5)                                    # Bordes verticales columnas
    ax.hlines([0, h, h - h_row_title, h - h_row_title - h_row_header], xmin=0, xmax=w, color=border_color, linewidth=0.5) # Bordes horizontales

    # Titulo de tabla
    plt.annotate(msg._MAIN_TITLE_MGP.format(nm_depa), (w * 0.5, h - (h_row_title * 0.5)), ha='center', va='center', fontname=font_times_new_roman, weight='bold', fontsize=font_size_title)

    # h_current es la altura actual de la tabla
    h_current = h - h_row_title

    # Header: nombre de columnas
    h_header = h_current - (h_row_header * 0.5)
    w_header = lambda i: sum(w_columns[:i]) + (w_columns[i] * 0.5)
    kwargs_header = {'ha': 'center', 'va': 'center', 'fontname': font_times_new_roman, 'weight': 'bold', 'fontsize': font_size_rows}
    kwargs_header_left = {'ha': 'left', 'va': 'center', 'fontname': font_times_new_roman, 'weight': 'bold', 'fontsize': font_size_rows}

    plt.annotate(msg._CODIGO_MGP, (w_header(0), h_header), **kwargs_header)
    plt.annotate(msg._SITIO_INTERES_MGP, (sum(w_columns[:1]) + (0.1 * cm), h_header), **kwargs_header_left)
    plt.annotate(msg._INTERES_PRINCIPAL_MGP, (sum(w_columns[:2]) + (0.1 * cm), h_header), **kwargs_header_left)
    plt.annotate(msg._VC_MGP, (w_header(3), h_header), **kwargs_header)
    plt.annotate(msg._VD_MGP, (w_header(4), h_header), **kwargs_header)
    plt.annotate(msg._VT_MGP, (w_header(5), h_header), **kwargs_header)

    h_current = h_current - h_row_header

    # Registros
    for r in rows:
        h_current = h_current - r[-1]
        h_row_current =  h_current + (r[-1] * 0.5)
        ax.hlines([h_current], xmin=0, xmax=w, color=border_color, linewidth=0.5)

        plt.annotate(r[0], (w_header(0), h_row_current), ha='center', va='center', fontname=font_times_new_roman, fontsize=font_size_rows)
        plt.annotate(r[6], (sum(w_columns[:1]) + (0.1 * cm), h_row_current), ha='left', va='center', fontname=font_times_new_roman, fontsize=font_size_rows)
        plt.annotate(r[2], (w_header(2), h_row_current), ha='center', va='center', fontname=font_times_new_roman, fontsize=font_size_rows)
        plt.annotate(r[3] if r[3] else "", (w_header(3), h_row_current), ha='center', va='center', weight='bold', fontname=font_times_new_roman, fontsize=font_size_rows)
        plt.annotate(r[4] if r[4] else "", (w_header(4), h_row_current), ha='center', va='center', weight='bold', fontname=font_times_new_roman, fontsize=font_size_rows)
        plt.annotate(r[5] if r[5] else "", (w_header(5), h_row_current), ha='center', va='center', weight='bold', fontname=font_times_new_roman, fontsize=font_size_rows)


    plt.subplots_adjust(top=1, bottom=0, right=1, left=0, hspace=0, wspace=0)
    plt.margins(0, 0)
    plt.axis('off')
    extent = ax.get_window_extent().transformed(fig.dpi_scale_trans.inverted())
    
    table_path = os.path.join(st._TEMP_FOLDER, 'table_{}.png'.format(uuid.uuid4().hex))
    plt.savefig(table_path, dpi=300, bbox_inches=extent, pad_inches=0, transparent=True)
    plt.draw()
    plt.clf()
    plt.close("all")

    # print table_path
    return table_path


# try:
# Insertar procesos 
# Localizar archivo mxd
mxd_path = st._MXD_A0_H_MGP if orientacion == 1 else st._MXD_A0_V_MGP
if not os.path.exists(mxd_path):
    raise RuntimeError("El mapa aun esta en fase de desarrollo")

# Cargar el mxd
mxd = arcpy.mapping.MapDocument(mxd_path)

# DataFrame activo _DFMAPAPRINCIPAL
df_mapa_principal = mxd.activeDataFrame

# Dataframe del mapa de ubicacion
df_mapa_ubicacion = arcpy.mapping.ListDataFrames(mxd, '*{}*'.format(_DFMAUBICACION))[0] 

# Query departamentos
query_departamentos = "{} not in ('{}', '99')".format(st._CD_DEPA_FIELD, cd_depa)

# Query departamento
query_departamento = "{} = '{}'".format(st._CD_DEPA_FIELD, cd_depa)

# Departamentos del dataframe principal
departamentos = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_DEPARTAMENTOS), df_mapa_principal)[0]
departamentos.definitionQuery = query_departamentos

departamento = arcpy.mapping.ListLayers(mxd, '*{}'.format(st._LAYER_DEPARTAMENTO), df_mapa_principal)[0]
departamento.definitionQuery = query_departamento

# Nombre del departamento
nm_depa = map(lambda i: i[0], arcpy.da.SearchCursor(st._TB_REGION_CONFIG_MGP, [st._NM_DEPA_FIELD], query_departamento))[0]

# Departamentos del dataframe ubicacion
departamento_ubicacion = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_DEPARTAMENTO), df_mapa_ubicacion)[0]
departamento_ubicacion.definitionQuery = query_departamento

elms = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")

arcpy.RefreshActiveView()

# Configurando la capa de geositios
geositios_lyr = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_GEO_GEOSITIOS), df_mapa_principal)[0]
# geositios_lyr.replaceDataSource(st._GDB_PATH_MGP, "SDE_WORKSPACE", os.path.basename(st._GPT_GEO_GEOSITIOS_PATH), False)
geositios_lyr.definitionQuery = query_departamento

arcpy.RefreshActiveView()
arcpy.RefreshTOC()


df_layers = pkg.get_layers_by_parent(6, as_dataframe=True)
for i, r in df_layers.iterrows():
    split_data_by_polygon(r, departamento, mxd.activeDataFrame.name, mxd=mxd)

arcpy.RefreshActiveView()
arcpy.RefreshTOC()



# Configurar rotulos
for elm in elms:
    if elm.name in (_ELM_TITLE, _ELM_TITLE_ORIG):
        # pass
        elm.text = elm.text.format(nm_depa)
    if autores and (elm.name == _ELM_AUTOR):
        elm.text = autores
    if elm.name == _ELM_DATUM:
        elm.text = elm.text.format(zona)

# Generar tabla de interes geologico

fields = [
    st._NUM_GEOSITIO_FIELD,
    st._DENOMINACION_FIELD,
    st._IGEOL_PRINCIPAL_FIELD,
    st._V_CIENTIFICA_FIELD,
    st._V_DIDACTICA_FIELD,
    st._V_TURISTICA_FIELD
]

rows = arcpy.da.SearchCursor(st._GPT_GEO_GEOSITIOS_PATH, fields, query_departamento)
rows = map(lambda i: list(i), rows)
rows.sort(key=lambda i: i[0])
rows_split = [rows[i:i + numero_registros] for i in range(0, len(rows), numero_registros)]


for i in range(4):
    picture_box = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT", _ELM_TABLA.format(i + 1))[0]
    if i + 1 <= len(rows_split):
        path_table = generate_table_interes_geologico(rows_split[i], nm_depa)
        picture_box.sourceImage = path_table
        continue
    picture_box._arc_object.delete()

arcpy.RefreshActiveView()


# Configurando la escala
df_mapa_principal.extent = departamento.getExtent()
df_mapa_principal.scale = escala

# Habilitando la visualizacion de capas
for layer in arcpy.mapping.ListLayers(mxd, '*'):  
    if layer.supports("VISIBLE"):
        if not layer.visible:
            layer.visible = True

arcpy.RefreshActiveView()
arcpy.RefreshTOC()

# Devolver la ubicacion del mapa
response['response'] = os.path.join(st._TEMP_FOLDER, 'response_{}.mxd'.format(uuid.uuid4().hex))
response['response'] = response['response'].replace('\\\\', '\\')
mxd.saveACopy(response['response'])
del mxd

# Seleccionar la grilla del mapa principal
arc.select_grid_by_name(response['response'], '327{}'.format(zona), exclude_grids=['4326'])

# except Exception as e:
#     response['status'] = 0
#     response['message'] = e.message
# finally:
response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
arcpy.SetParameterAsText(6, response)