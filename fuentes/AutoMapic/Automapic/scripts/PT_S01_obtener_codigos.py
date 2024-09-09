# -*-coding:utf-8-*-

import arcpy
import settings_aut as st
import traceback

fila = arcpy.GetParameterAsText(0)
columna = arcpy.GetParameterAsText(1)
cuadrante = arcpy.GetParameterAsText(2)
rumbo = arcpy.GetParameterAsText(3)


state, message, response = 0, str(), str()

try:
    query = list()
    field = [st._FILA_FIELD]

    if fila:
        query.append("({} = '{}')".format(st._FILA_FIELD, fila))
        field = [st._COLUMNA_FIELD]
    if columna:
        query.append("({} = '{}')".format(st._COLUMNA_FIELD, columna))
        field = [st._CUADRANTE_FIELD]
    if cuadrante:
        query.append("({} = '{}')".format(st._CUADRANTE_FIELD, cuadrante))
        field = [st._RUMBO_FIELD]

    query_string = ' AND '.join(query)

    cursor = arcpy.da.SearchCursor(st._CUADRICULAS_PATH, field, query_string)
    response = list(set(list(map(lambda i: i[0], cursor))))
    response.sort()


    state, message, response = 1, 'success', ','.join(response)
    
except Exception as e:
    message = traceback.print_exc().__str__()
finally:
    arcpy.SetParameterAsText(4, state)
    arcpy.SetParameterAsText(5, message)
    arcpy.SetParameterAsText(6, response)
