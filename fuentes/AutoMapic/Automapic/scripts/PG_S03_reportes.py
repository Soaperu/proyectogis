# Importar librerias
import arcpy
import pandas as pd
import json
import packages_aut_ora as pkgo
import os
import settings_aut as st
import tempfile
import uuid


def remove_string_duplicates(data):
    if data:
        data = filter(lambda i: i, data.split(','))
        data = map(lambda i: i.strip(), data)
        data = list(set(data))
        response = ', '.join(data)
        return response
    return data

if __name__ == '__main__':
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    report_general_current_year = arcpy.GetParameter(0)
    report_general_current_year_year = arcpy.GetParameter(1)
    report_general_by_date = arcpy.GetParameter(2)
    start_date = arcpy.GetParameter(3)
    end_date = arcpy.GetParameter(4)
    report_month = arcpy.GetParameter(5)
    report_month_year = arcpy.GetParameter(6)
    report_years = arcpy.GetParameter(7)
    report_years_year = arcpy.GetParameter(8)
    report_all_years = arcpy.GetParameter(9)

    # try:
    # Insertar procesos 
    temp_folder = st._TEMP_FOLDER if st._TEMP_FOLDER else tempfile.gettempdir()
    response['response'] = os.path.join(temp_folder, 'reporte_{}.xls'.format(uuid.uuid4().hex))

    writer = pd.ExcelWriter(response['response'], encoding='utf-8')

    if report_general_current_year:
        df = pkgo.report_dgar_pg_data_general(getdataframe=True)
        df['DEPARTAMENTO'] = df['DEPARTAMENTO'].apply(lambda i: remove_string_duplicates(i))
        df['PROVINCIA'] = df['PROVINCIA'].apply(lambda i: remove_string_duplicates(i))
        df['DISTRITO'] = df['DISTRITO'].apply(lambda i: remove_string_duplicates(i))
        df.to_excel(writer, index=False, sheet_name='general_{}'.format(report_general_current_year_year))
    
    if report_general_by_date:
        df = pkgo.report_dgar_pg_data_general_by_dates(start_date, end_date, getdataframe=True)
        df['DEPARTAMENTO'] = df['DEPARTAMENTO'].apply(lambda i: remove_string_duplicates(i))
        df['PROVINCIA'] = df['PROVINCIA'].apply(lambda i: remove_string_duplicates(i))
        df['DISTRITO'] = df['DISTRITO'].apply(lambda i: remove_string_duplicates(i))
        start_date = start_date.replace('/', '_')
        end_date = end_date.replace('/', '_')
        df.to_excel(writer, index=False, sheet_name='general_{}_{}'.format(start_date, end_date))
    
    if report_month:
        df = pkgo.report_dgar_pg_data_departament_month(report_month_year, getdataframe=True)
        df = df.fillna(0)
        df.to_excel(writer, index=False, sheet_name='departamentos_mes_{}'.format(report_month_year))
    
    if report_years:
        df = pkgo.report_dgar_pg_data_departament(report_years_year, getdataframe=True)
        df = df.fillna(0)
        df.to_excel(writer, index=False, sheet_name='departamentos_vs_anio_{}'.format(report_years_year))
    
    if report_all_years:
        df = pkgo.report_dgar_pg_data_departament_years(getdataframe=True)
        df_dep = pkgo.get_departaments(getdataframe=True)
        df_nac = pd.DataFrame([{'CD_DEPA': '99', 'NM_DEPA': 'PERU'}])
        df_dep = df_dep.append(df_nac)
        df = df.pivot_table(index='CD_DEPA', values='ATENCIONES', columns='ANIO').reset_index()
        df_r = pd.merge(df_dep, df, how="left", on=["CD_DEPA", "CD_DEPA"])
        df_r = df_r.rename(columns={'NM_DEPA':'DEPARTAMENTO'})
        columns = filter(lambda i: i != 'CD_DEPA', df_r.columns)
        df_r = df_r[columns]
        df_r = df_r.fillna(0)
        df_r.to_excel(writer, index=False, sheet_name='departamentos_vs_anios')
    
    writer.save()


    # except Exception as e:
    #     response['status'] = 0
    #     response['message'] = e.message
    # finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    print(response)
    arcpy.SetParameterAsText(10, response)
