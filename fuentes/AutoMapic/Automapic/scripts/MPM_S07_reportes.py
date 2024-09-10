import packages_aut_ora as pkgo
import os
import pandas as pd

map_code = 'MAF-SIGE-22-079'
output_report_dir = r'D:\temp'

df_ocumin_m = pkgo.report_ocu_mineral_metalico(map_code, getdataframe=True)
df_ocumin_nm = pkgo.report_ocu_mineral_no_metalico(map_code, getdataframe=True)
df_rocmin = pkgo.report_roc_min_ind(map_code, getdataframe=True)
df_geoquimica = pkgo.report_geoquimica_sedimentos(map_code, getdataframe=True)
report = os.path.join(output_report_dir, 'reporte_{}.xls'.format(map_code)) 
writer = pd.ExcelWriter(report, encoding='utf-8')
df_geoquimica.to_excel(writer, index=False, sheet_name='geoquimica_sedimentos')
df_ocumin_m.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_metalico')
df_ocumin_nm.to_excel(writer, index=False, sheet_name='ocurrencia_mineral_no_metalico')
df_rocmin.to_excel(writer, index=False, sheet_name='rocas_minerales_industriales')
writer.save()