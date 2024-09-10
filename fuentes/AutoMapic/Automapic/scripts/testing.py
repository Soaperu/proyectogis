# # Importar librerias
# import arcpy
# import json
# import os
# from settings_aut import _CD_DEPA_FIELD
# import settings_aut as st
# import messages_aut as msg
# import arcobjects as arc
# import PG_S01_mapa_peligros_geologicos as mpg
# import uuid
# import tempfile
# import packages_aut_arc as pkga
# import packages_aut_ora as pkgo
# import packages_aut as pkg
# from MHG_S07_cortar_features_por_cuencas import split_data_by_polygon
# import pandas as pd

# _ERROR_STATE = 0    # Cuando el proceso falla
# _SUCCESS_STATE = 1  # Cuando el proceso se ejecuta sin fallos
# _DELETE_STATE = 99  # Cuando se elimina un registro de la tabla de mapas de potencial minero
# _CREATION_STATE = 9  # Cuando se crea un nuevo codigo pero aun no se genera el mapa

# _TYPE_INFO = 1  # El mapa tiene informacion de interes
# _TYPE_NO_INFO = 2   # El area no tiene informacion de interes


# map_code = 'MAF-SIGE-22-044'
# output_report_dir = r'D:\2022\temp'

# # Generando reportes
# # df_principal= pkgo.report_drme_pm_data_general(getdataframe=True)
# df_ocumin_m = pkgo.report_ocu_mineral_metalico(map_code, getdataframe=True)
# df_ocumin_nm = pkgo.report_ocu_mineral_no_metalico(map_code, getdataframe=True)
# df_rocmin = pkgo.report_roc_min_ind(map_code, getdataframe=True)
# df_geoquimica = pkgo.report_geoquimica_sedimentos(map_code, getdataframe=True)
# response = os.path.join(output_report_dir, 'reporte_{}.xls'.format(map_code)) 
# writer = pd.ExcelWriter(response, encoding='utf-8')
# # df_principal.to_excel(writer, index=False, sheet_name='datos_generales')
# df_geoquimica.to_excel(writer, index=False, sheet_name='geoquimica_sedimentos')
# df_ocumin_m.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_metalico')
# df_ocumin_nm.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_no_metalico')
# df_rocmin.to_excel(writer, index=False, sheet_name='rocas_minerales_industriales')
# writer.save()