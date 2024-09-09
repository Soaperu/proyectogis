# Importar librerias
import arcpy
import json
import settings_aut as st
import messages_aut as msg
import packages_aut as pkg
import uuid
import os
import arcobjects as arc
import matplotlib.pyplot as plt
from matplotlib.offsetbox import TextArea, DrawingArea, OffsetImage, AnnotationBbox
# import automapic as aut
from MHG_S07_cortar_features_por_cuencas import split_data_by_polygon
# import pythonaddins

arcpy.env.overwriteOutput = True


# Parametros
response = dict()
response['status'] = 1
response['message'] = 'success'

cd_depa = arcpy.GetParameterAsText(0)       # Codigo de la departamento (string requerido)
orientacion = arcpy.GetParameter(1)   # 1: Horizontal, 0: Vertical (integer requerido)
zona = arcpy.GetParameter(2)          # Zona geografica 17, 18, 19 (integer requerido)
escala = arcpy.GetParameter(3)        # 25000, 50000, 100000... (integer requerido)
autores = arcpy.GetParameterAsText(4)       # "Juanito, Perez, Lui, ... " (string opcional)
# attachments = arcpy.GetParameterAsText(5)   # identificadores de attchments
# outmxd = arcpy.GetParameterAsText(5)        # Ruta de salida del mapa (string opcional)


_DFMAPAPRINCIPAL = "DFMAPAPRINCIPAL"
_DFMAUBICACION = "DFMAPAUBICACION"
_ELM_TITLE = "ELM_TITLE"
_ELM_TITLE_ORIG = "ELM_TITLE_ORIG"
_ELM_AUTOR = "ELM_AUTOR"
_ELM_AUTOR_ORIG = "ELM_AUTOR_ORIG"
_ELM_DATUM = "ELM_DATUM"
_ELM_DATUM_ORIG = "ELM_DATUM_ORIG"
_ELM_TABLA_FALLAS = "ELM_TABLA_FALLAS"
_ELM_IMG = "ELM_IMG_"


# def split_data_by_polygon(row, polygon, df_name, mxd):
#     response = os.path.join(arcpy.env.scratchGDB, row['layer_name'])
#     feature = os.path.join(row['datasource'], row['feature'])
#     if arcpy.Exists(feature):
#         arcpy.AddMessage('{} SI existe'.format(feature))
#     else:
#         arcpy.AddMessage('{} NO existe'.format(feature))

#     # feature = feature.replace('\\\\', '\\')
#     name_feature= os.path.basename(feature).split('.')[1]
#     try:
#         arcpy.Clip_analysis(feature, polygon, response)
#         aut.add_layer_with_new_datasource(row['layer_name'], name_feature, arcpy.env.scratchGDB, "FILEGDB_WORKSPACE", df_name=df_name, mxd=mxd)
#     except Exception as e:
#         pythonaddins.MessageBox("Ocurrio un error con el feature {}\n{}".format(row['feature'], e.message), st.__title__)
#         return


def generate_table_fallas(fallas_lyr, departamento_lyr):
    cm = 1/2.54
    border_color = '#000000'
    font_Arial = 'Arial'
    font_size_rows = 11
    font_size_title = 12
    # arcpy.SelectLayerByLocation_management(fallas_lyr, "INTERSECT", departamento_lyr, None, "NEW_SELECTION")
    fields = [st._COD_GEOCAT_FIELD, st._NOMBRE_FIELD, st._EDAD_ULT_M_FIELD]
    freq = arcpy.Frequency_analysis(fallas_lyr, os.path.join(st._TEMP_FOLDER, 'resumen.dbf'), fields)
    
    
    # Tamanio de la tabla
    w_colum_1 = 2.6 * cm
    w_colum_2 = 3.2 * cm
    w_colum_3 = 7.5 * cm
    w_colum_4 = 6 * cm

    h_row = 0.6 * cm

    w_columns = [w_colum_1, w_colum_2, w_colum_3, w_colum_4]
    
    rows = map(lambda i: i, arcpy.da.SearchCursor(freq, fields))
    nrows = len(rows)
        
    rows.sort(key=lambda x: x[0])

    arcpy.SelectLayerByAttribute_management(fallas_lyr, "CLEAR_SELECTION")
    
    h, w = (0.8 + ((nrows + 1) * 0.6)) * cm, sum(w_columns)
    h_table = h - (0.8 * cm)
    fig, ax = plt.subplots(figsize=(w, h))

    # Contruir header
    # :Limites de dibujo
    ax.set_xlim(0, w)
    ax.set_ylim(0, h)

    # Titulo de tabla
    plt.annotate(msg._MAIN_TITLE_MN, (sum(w_columns) * 0.5, h - (0.8 * 0.25 * cm)), ha='center', va='center', fontname=font_Arial, weight='bold', fontsize=font_size_title)

    # :Bordes exteriores
    ax.vlines([0, w_colum_1, sum(w_columns[:2]), sum(w_columns[:3]), w], ymin=0, ymax=h_table, color=border_color, linewidth=1)                             # Bordes verticales
    ax.hlines([0, h_table - h_row, h_table], xmin=0, xmax=w, color=border_color, linewidth=1)                             # Bordes horizontales
    plt.annotate(msg._NRO, (w_colum_1 * 0.5, h_table - (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, weight='bold', fontsize=font_size_rows)
    plt.annotate(msg._CODIGO, (w_colum_1 + (w_colum_2 * 0.5), h_table - (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, weight='bold', fontsize=font_size_rows)
    plt.annotate(msg._NOMBRE, (sum(w_columns[:2]) + (w_colum_3 * 0.5), h_table - (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, weight='bold', fontsize=font_size_rows)
    plt.annotate(msg._ULTIMA_REACTIVACION, (sum(w_columns) - (w_colum_4 * 0.5), h_table - (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, weight='bold', fontsize=font_size_rows)
    
    # Fondo blanco
    h_table = h_table - h_row

    # Construir body
    for i, r in enumerate(rows, 1):
        h_table = h_table - h_row
        ax.hlines([h_table], xmin=0, xmax=w, color=border_color, linewidth=1)
        plt.annotate(i, (w_colum_1 * 0.5, h_table + (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, fontsize=font_size_rows)
        plt.annotate(r[0], (w_colum_1 + (w_colum_2 * 0.5), h_table + (h_row * 0.5)), ha='center', va='center', fontname=font_Arial, fontsize=font_size_rows)
        plt.annotate(r[1], (w_colum_1 + w_colum_2 + 0.1 * cm, h_table + (h_row * 0.5)), ha='left', va='center', fontname=font_Arial, fontsize=font_size_rows)
        plt.annotate(r[2], (w_colum_1 + w_colum_2 + w_colum_3 + 0.1 * cm, h_table + (h_row * 0.5)), ha='left', va='center', fontname=font_Arial, fontsize=font_size_rows)

    plt.subplots_adjust(top=1, bottom=0, right=1, left=0, hspace=0, wspace=0)
    plt.margins(0, 0)
    plt.axis('off')
    extent = ax.get_window_extent().transformed(fig.dpi_scale_trans.inverted())
    
    table_path = os.path.join(st._TEMP_FOLDER, 'table_{}.png'.format(uuid.uuid4().hex))
    plt.savefig(table_path, dpi=600, bbox_inches=extent, pad_inches=0, transparent=True)

    return table_path



# try:
# Localizar el archivo mxd
mxd_path = st._MXD_A0_H if orientacion == 1 else st._MXD_A0_V

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

# arcpy.AddMessage(query_departamento)
# arcpy.AddMessage(arcpy.GetCount_management(departamento))

arcpy.RefreshActiveView()

# Nombre del departamento
nm_depa = map(lambda i: i[0], arcpy.da.SearchCursor(st._TB_REGION_CONFIG, [st._NM_DEPA_FIELD], query_departamento))[0]

# Departamentos del dataframe ubicacion
departamento_ubicacion = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_DEPARTAMENTO), df_mapa_ubicacion)[0]
departamento_ubicacion.definitionQuery = query_departamento

elms = arcpy.mapping.ListLayoutElements(mxd, "TEXT_ELEMENT")


# arcpy.AddMessage([i.name for i in arcpy.mapping.ListLayers(mxd)])
# Configurar datasource de capas filtrables
fallas = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPL_NEO_NEOTECTONICO), df_mapa_principal)[0]
desidad_poblacional = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_NEO_DESIDAD_POBLACIONAL), df_mapa_principal)[0]
centrales_hidroelectricas_proy = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_PROY), df_mapa_principal)[0]
centrales_hidroelectricas_ejec = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_EJEC), df_mapa_principal)[0]
gaseoducto_construccion = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPL_NEO_GASEODUCTO_CONSTRUCCION), df_mapa_principal)[0]
telecomunicaciones = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_NEO_TELECOMUNICACIONES), df_mapa_principal)[0]
mecanismos_focales = arcpy.mapping.ListLayers(mxd, '*{}*'.format(st._LAYER_GPT_NEO_MECANISMOS_FOCALES), df_mapa_principal)[0]

# fallas.replaceDataSource(st._GDB_PATH_NT, "SDE_WORKSPACE", os.path.basename(st._GPL_NEO_NEOTECTONICO_PATH), False)
desidad_poblacional.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPT_NEO_DESIDAD_POBLACIONAL, False)
centrales_hidroelectricas_proy.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_PROY, False)
centrales_hidroelectricas_ejec.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_EJEC, False)
gaseoducto_construccion.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPL_NEO_GASEODUCTO_CONSTRUCCION, False)
telecomunicaciones.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPT_NEO_TELECOMUNICACIONES, False)
mecanismos_focales.replaceDataSource(st._GDB_PATH_NT, "FILEGDB_WORKSPACE", st._LAYER_GPT_NEO_MECANISMOS_FOCALES, False)

fallas.definitionQuery = query_departamento
desidad_poblacional.definitionQuery = query_departamento
centrales_hidroelectricas_proy.definitionQuery = query_departamento
centrales_hidroelectricas_ejec.definitionQuery = query_departamento
gaseoducto_construccion.definitionQuery = query_departamento
telecomunicaciones.definitionQuery = query_departamento
mecanismos_focales.definitionQuery = query_departamento


arcpy.RefreshActiveView()
arcpy.RefreshTOC()


df_layers = pkg.get_layers_by_parent(5, as_dataframe=True)
for i, r in df_layers.iterrows():
    split_data_by_polygon(r, departamento, mxd.activeDataFrame.name, mxd=mxd)

arcpy.RefreshActiveView()
arcpy.RefreshTOC()


# Configurar rotulos
for elm in elms:
    if elm.name in (_ELM_TITLE, _ELM_TITLE_ORIG):
        # pass
        elm.text = elm.text.format(nm_depa)
    if autores and elm.name in (_ELM_AUTOR, _ELM_AUTOR_ORIG):
        elm.text = autores
    if elm.name in (_ELM_DATUM, _ELM_DATUM_ORIG):
        elm.text = elm.text.format(zona)

# Generar tabla de fallas
path_table = generate_table_fallas(fallas, departamento)
picture_box = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT", _ELM_TABLA_FALLAS)[0]
picture_box.sourceImage = path_table

# Configurando la escala
df_mapa_principal.extent = departamento.getExtent()
df_mapa_principal.scale = escala

# Configurando las imagenes de los attachaments
# attachments = attachments.split(",")
# attachments_ids = map(lambda i: os.path.basename(i)[3], attachments)
# picture_att = arcpy.mapping.ListLayoutElements(mxd, "PICTURE_ELEMENT", "{}*".format(_ELM_IMG))
# picture_att.sort(key=lambda i: i.name)

# for i, elm in enumerate(picture_att):
#     idx = i + 1
#     if idx <= len(attachments):
#         elm.sourceImage = attachments[i]
#         continue
#     elm._arc_object.delete()

# arcpy.RefreshActiveView()
# arcpy.RefreshTOC()


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
#     # En caso de error, se retorna un json con la descripcion del error
#     response['status'] = 0
#     response['message'] = e.message
# finally:
    # Se guarda el json de respuesta
response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
arcpy.SetParameterAsText(5, response)
