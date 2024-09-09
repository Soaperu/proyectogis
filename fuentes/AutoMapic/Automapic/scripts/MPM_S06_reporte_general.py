# Importar librerias
import arcpy
import json
import packages_aut_ora as pkgo
import pandas as pd
import os
from settings_aut import _TEMP_FOLDER
import tempfile

if __name__ == '__main__':
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    try:
        temp_folder = _TEMP_FOLDER if _TEMP_FOLDER else tempfile.gettempdir()
        response['response'] = os.path.join(temp_folder, 'reporte_general.xls')
        df_principal= pkgo.report_drme_pm_data_general(getdataframe=True)
        writer = pd.ExcelWriter(response['response'], encoding='utf-8')
        df_principal.to_excel(writer, index=False, sheet_name='datos_generales')
        writer.save()
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(0, response)
