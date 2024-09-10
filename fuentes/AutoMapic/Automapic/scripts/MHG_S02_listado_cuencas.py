import arcpy
import json
import settings_aut as st
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'


try:
    feature = st._PL_01_CUENCAS_HIDROGRAFICAS_PATH
    if not arcpy.Exists(feature):
        raise RuntimeError(msg._ERROR_FEATURE_CUENCAS_HG)
    cursor = arcpy.da.SearchCursor(feature, [st._CD_CUENCA, st._NM_CUENCA, st._ORDEN], sql_clause = (None, "ORDER BY {}".format(st._ORDEN)))
    response["response"] = list(map(lambda i: [i[0], i[1] + " - " +  str(i[0])], cursor))
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(0, response)
