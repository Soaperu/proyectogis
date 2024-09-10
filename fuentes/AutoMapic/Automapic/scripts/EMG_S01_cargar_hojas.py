# Importar librerias
import arcpy
import json
import os
import MG_S00_model as model
import messages_aut as msg

if __name__ == '__main__':
    try:
        response = dict()
        response['status'] = 1
        response['message'] = 'success'

        geodatabase = arcpy.GetParameterAsText(0)
        if not arcpy.Exists(geodatabase):
            raise RuntimeError(msg._CPM_GEODATABASE_NOT_EXIST)
        tb_hojas = model.gpo_dg_hojas_50()
        tb_hojas_path = os.path.join(geodatabase, tb_hojas.dataset, tb_hojas.name)
        if not arcpy.Exists(tb_hojas_path):
            raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
        response['response'] = list(set(map(lambda i: i[0], arcpy.da.SearchCursor(tb_hojas_path, [tb_hojas.codhoja]))))
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
