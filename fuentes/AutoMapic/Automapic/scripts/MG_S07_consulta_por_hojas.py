# Importar librerias
import arcpy
import json
import settings_aut as st
import packages_aut as pkg
import os
import automapic as aut

response = dict()
response['status'] = 1
response['message'] = 'success'

hoja = arcpy.GetParameterAsText(0)
features = arcpy.GetParameterAsText(1)
name_layer = "GPO_DG_HOJAS_50K"

# try:
# Insertar procesos
mxd = arcpy.mapping.MapDocument("CURRENT")
layers = arcpy.mapping.ListLayers(mxd, "*{}*".format(name_layer))
if not len(layers):
    raise RuntimeError("La capa de Hojas a escala 50,000 ({}), no se encuentra en la tabla de contenidos".format(name_layer))
layer = layers[0]
# arcpy.AddMessage(layer.dataSource)
# desc = arcpy.Describe(layer.dataSource)
# id_selected = desc.FIDset
rowcount = arcpy.GetCount_management(layer).getOutput(0)
if not int(rowcount):
    raise RuntimeError("Debe seleccionar por lo menos una hoja")
# id_selected = id_selected.replace(";", ",")
cursor = [i for i in arcpy.da.SearchCursor(layer, [st._CODHOJA_FIELD, st._ZONA_FIELD])] #, "{} IN ({})".format("OBJECTID", id_selected))]
hojas = map(lambda i: i[0], cursor)
zonas = set(map(lambda i: i[1], cursor))
if hoja not in hojas:
    hojas.append(hoja)
query = "{} IN ('{}')".format(st._CODHOJA_FIELD, "','".join(hojas))

for zona in zonas:
    paths = list()
    symbols = list()
    query_features = "{} IN ({})".format(st._ID, features)
    df = pkg.get_layers_selected(query_features, as_dataframe=True)
    for i, r in df.iterrows():
        feature = os.path.join(r['datasource'], r['feature'].format(zona, zona))
        paths.append(feature)
        lyr = os.path.join(st._LAYERS_DIR, r['layer']) if r['layer'] else None
        symbols.append(lyr)
    arcpy.AddMessage(paths)
    aut.check_layer_inside_data_frame(paths, symbols, query=query)

arcpy.SelectLayerByAttribute_management(layer, "CLEAR_SELECTION")
# except Exception as e:
#     response['status'] = 0
#     response['message'] = e.message
# finally:
response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
arcpy.SetParameterAsText(2, response)
