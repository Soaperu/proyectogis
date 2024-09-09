# -*-coding:utf-8-*-

import arcpy
import settings_aut as st
import messages_aut as msg
import pandas as pd
import json


cuencas = arcpy.GetParameterAsText(0)
zona = arcpy.GetParameterAsText(1)

response = dict()
response['status'] = 1
response['message'] = 'success'
data = dict()

def get_name_component(code):
    query = "{} = '{}'".format(st._ID, code)
    cursor = arcpy.da.SearchCursor(st._TB_01_UNIDAD_HIDROGEOLOGICA_PATH, [st._NOMBRE_FIELD], query)
    data = map(lambda i: i[0], cursor)
    response = data[0] if len(data) else code
    return response

try:
    feature = st._PO_01_FORMACION_HIDROGEOLOGICA_PATH.format(zona, zona)
    if not arcpy.Exists(feature):
        raise RuntimeError(msg._ERROR_FEATURE_FHIDROGEO_HG)

    table = st._TB_01_UNIDAD_HIDROGEOLOGICA_PATH
    if not arcpy.Exists(table):
        raise RuntimeError(msg._ERROR_FEATURE_UHIDROGEO_HG)

    cuencas = cuencas.replace("'", '').replace(' ', '')
    cuencas = "('{}')".format("', '".join(cuencas.split(',')))

    fields = [st._ID_FHIDROG, st._N_FHIDROG, st._D_FHIDROG, st._LITOLOGIA_G, st._UHIDROG, st._CL_HIDROG]
    query = '{} in {} and {} NOT IN (3, 193, 194, 462, 2024, 2600)'.format(st._CD_CUENCA, cuencas, st._CODI_FIELD)
    n_arr = arcpy.da.FeatureClassToNumPyArray(feature, fields, query, null_value=999)
    df_ini = pd.DataFrame(n_arr)
    
    if len(df_ini) > 0:
    	df = df_ini.groupby(by=[st._ID_FHIDROG]).max().reset_index()
    	df['id_m'] = df[st._ID_FHIDROG].str[:3]

    	fields_uh = [st._ID, st._GREEN, st._RED, st._BLUE, st._NOMBRE_FIELD, st._ID_PADRE]
    	n_arr_uh = arcpy.da.FeatureClassToNumPyArray(table, fields_uh, null_value=999)
    	df_uh = pd.DataFrame(n_arr_uh)

        uhidrog = list(df_ini[st._UHIDROG].unique())
        chidrog = list(df_ini[st._CL_HIDROG].unique())
        chidrog = df_uh.loc[df_uh[st._ID].isin(uhidrog) | df_uh[st._ID].isin(chidrog)]
        chidrog = chidrog[[st._ID, st._ID_PADRE, st._NOMBRE_FIELD]]

    	df_uh[st._ID] = df_uh[st._ID].str.ljust(3, '0')
    	df_uh = df_uh.fillna(0)

    	df_fhidrog = pd.merge(df, df_uh, how='left', left_on=['id_m'], right_on= [st._ID])

        data["fhidrog"] = df_fhidrog.to_dict('records')
        data["chidrog"] = chidrog.to_dict('records')
        # data["chidrog"] = chidrog

    	# data = df_response.to_dict('records')
        
    response["response"] = data
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(2, response)
