import arcpy
import json
import settings_aut as st
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'

cuenca = arcpy.GetParameterAsText(0)
subcuenca = arcpy.GetParameterAsText(1)
def get_valores_cuencas(cuenca, subcuenca):
    """
    funcion para obtener valores de cuencas, subcuencas y microcuencas para los combobox
    """
    CD_CUENCA = "COD_CUENCA"
    CD_SUBC = "COD_SUBC"
    CD_MICROC = "COD_MICROC"
    NM_CUENCA = "CU_IN_HIDR"
    NM_SUBC = "SUBCUENCA"
    NM_MICROC = "MICROCU"

    feature = st._BASE_EXCEL_LAB_FC
    # feature = r'D:\jyupanqui\proyectos\dgar\NUEVA_GDB\GDB_LBG.gdb\BASE_EXCEL_LAB_FC'
    if not arcpy.Exists(feature):
        raise RuntimeError(msg._ERROR_FEATURE_CUENCAS_MHQ)


    if cuenca == "0":
        return [["0",""]]
    if subcuenca == "0":
        return [["0",""]]

    
    campos = [CD_CUENCA, NM_CUENCA, CD_SUBC, NM_SUBC, CD_MICROC, NM_MICROC]
    
    cursor = arcpy.da.SearchCursor(feature,campos , sql_clause = (None, "ORDER BY {} ASC , {} ASC , {} ASC".format(CD_CUENCA, CD_SUBC, CD_MICROC)))

    if not cuenca or cuenca == '':
        lista_unica = sorted(list(set([ (x[0], x[1]) for x in cursor])))
        respuesta = list(map(lambda i: [i[0], i[1] + " - " +  str(i[0])], lista_unica))
    else:
        if not subcuenca or subcuenca == '':
            lista_unica = sorted(list(set([ (x[2], x[3]) for x in cursor if x[0]==cuenca])))
            respuesta = list(map(lambda i: [i[0], i[1] + " - " +  str(i[0])], lista_unica))

        else:
            lista_unica = sorted(list(set([ (x[4], x[5]) for x in cursor if x[2]==subcuenca])))
            respuesta = list(map(lambda i: [i[0], i[1] + " - " +  str(i[0])], lista_unica))
    
    respuesta.insert(0, ["0",""])
    return respuesta
            



try:
    response["gdb"] = st._GDB_PATH_MHQ
    response["response"] = get_valores_cuencas(cuenca,subcuenca)
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(2, response)
