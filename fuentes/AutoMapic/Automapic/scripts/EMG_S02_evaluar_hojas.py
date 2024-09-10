# Importar librerias
import shutil
import arcpy
from datetime import datetime
import json
import os
import MG_S00_model as model
import messages_aut as msg
import pandas as pd
import settings_aut as st
import sys
from sys import stdout


_ROUND_DIGITS = 2
_GROUP_NAME_POG = 'Distribucion de Puntos de Observacion'
_GROUP_NAME_EL = 'Elementos lineales'
_GROUP_NAME_LE = 'Leyenda'
_GROUP_NAME_FM = 'Ubicacion de muestras con distintos estudios'
_GROUP_NAME_SG = 'Seccion estructural'


def get_data_indicators(feature_object, group_name, geodatabase, query):
    feature_object_path = os.path.join(geodatabase, feature_object.dataset, feature_object.name)
    if not arcpy.Exists(feature_object_path):
        return {'GRUPO': group_name, 'REGISTROS': 0, 'CAPA': feature_object.name, 'obs': 'La capa ' + feature_object.name + ' no se encuentra en la Geodatabase'}
    else:
        cursor_sc = arcpy.da.SearchCursor(feature_object_path, [feature_object.codhoja], query)
        number_records = len(list(map(lambda i: i[0], cursor_sc)))
        return {'GRUPO': group_name, 'REGISTROS': number_records, 'CAPA': feature_object.name}

def evaluation_pog(geodatabase, zona, query):
    global _GROUP_NAME_POG
    features = list()
    response = list()
    fc_pog = model.gpt_dgr_pog(zona)
    features.append(fc_pog)

    for feature in features:
        row = get_data_indicators(feature, _GROUP_NAME_POG, geodatabase, query)
        response.append(row)
    return response



def evaluation_lineal_elements(geodatabase, zona, query):
    global _GROUP_NAME_EL
    features = list()
    response = list()
    fc_contac = model.gpl_dgr_contac(zona)
    features.append(fc_contac)
    fc_dique = model.gpl_dgr_dique(zona)
    features.append(fc_dique)
    fc_esvolc = model.gpl_dgr_esvolc(zona)
    features.append(fc_esvolc)
    fc_falla = model.gpl_dgr_falla(zona)
    features.append(fc_falla)
    fc_geofor = model.gpl_dgr_geofor(zona)
    features.append(fc_geofor)
    fc_plieg = model.gpl_dgr_plieg(zona)
    features.append(fc_plieg)
    
    for feature in features:
        row = get_data_indicators(feature, _GROUP_NAME_EL, geodatabase, query)
        response.append(row)
    return response

def evaluation_legend(geodatabase, query):
    global _GROUP_NAME_LE
    features = list()
    response = list()
    response_le = list()
    fc_form = model.gpo_mg_form()
    features.append(fc_form)
    fc_celd = model.gpl_mg_celd()
    features.append(fc_celd)
    fc_label = model.gpt_mg_label()
    features.append(fc_label)

    for feature in features:
        row = get_data_indicators(feature, _GROUP_NAME_LE, geodatabase, query)
        response.append(row)
    return response

def evaluation_fossil_samples(geodatabase, zona, query):
    global _GROUP_NAME_FM
    features = list()
    response = list()
    fc_fosil = model.gpt_dgr_fosil(zona)
    features.append(fc_fosil)
    fc_muestr = model.gpt_dgr_muestr(zona)
    features.append(fc_muestr)

    for feature in features:
        row = get_data_indicators(feature, _GROUP_NAME_FM, geodatabase, query)
        response.append(row)
    return response

def evaluation_geological_section(geodatabase, query):
    global _GROUP_NAME_SG
    features = list()
    response = list()
    gpl_perfil = model.gpl_mg_perfil()
    features.append(gpl_perfil)
    gpo_perfil = model.gpo_mg_perfil()
    features.append(gpo_perfil)
    gpt_perfil = model.gpt_mg_perfil()
    features.append(gpt_perfil)

    for feature in features:
        row = get_data_indicators(feature, _GROUP_NAME_SG, geodatabase, query)
        response.append(row)
    return response


def generate_report(dataframe, codhoja, evaluador, total, out_dir):
    from reportlab.platypus import SimpleDocTemplate, Paragraph, Table, Image, TableStyle, Spacer
    from reportlab.lib import colors
    from reportlab.lib.styles import ParagraphStyle
    from reportlab.lib.enums import TA_LEFT, TA_CENTER
    from reportlab.lib.pagesizes import A4
    from reportlab.lib.units import cm
    global _PATH_REPORT, _DATE_NOW

    _TITLE_REPORT = 'EVALUACION DE HOJA {}<br/>Evaluador: {}'.format(codhoja.upper(), evaluador)
    # print(dir(datetime))
    _DATE_NOW = datetime.today().strftime('%d/%m/%Y')

    _NAME_REPORT = 'reporte_{}.pdf'.format(codhoja)
    _PATH_REPORT = os.path.join(out_dir, _NAME_REPORT)
    _PATH_REPORT_SRV = os.path.join(st._SRVFS_BDGEOCIENTIFICA_EVAL_MG, _NAME_REPORT)
    _PATH_REPORT_URL = st._URL_BDGEOCIENTIFICA_EVAL_MG + '/{}'.format(_NAME_REPORT)
    
    _KWARGS = {
        'pagesize': A4,
        'rightMargin': 65,
        'leftMargin': 65,
        'topMargin': 0.5 * cm,
        'bottomMargin': 0.5 * cm,
    }
    elm = list()
    # Creando la cabecera del documento
    titleStyle = ParagraphStyle(
        name='Heading1',
        fontSize=12, leading=14,
        alignment=TA_CENTER
    )
    headStyle = ParagraphStyle(
        name='Heading1',
        fontSize=8,
        leading=10,
        alignment=TA_CENTER
    )
    rowStyle = ParagraphStyle(
        name='Heading1',
        fontSize=8,
        leading=10,
        alignment=TA_CENTER
    )
    rowStyle2 =  ParagraphStyle(
        name='Heading1',
        fontSize=8,
        leading=10,
        alignment=TA_LEFT
    )
    rowStyle3 =  ParagraphStyle(
        name='Heading1',
        fontSize=12,
        leading=10,
        alignment=TA_CENTER
    )
    
    tableStyle = TableStyle([
        ('GRID', (0, 0), (-1, -1), 0.1, colors.black),
        ('VALIGN', (0, 0), (-1, -1), 'MIDDLE'),
    ])
    tableGroupStyle = TableStyle([
        ('GRID', (0, 0), (-1, -1), 0.1, colors.black),
        ('SPAN', (0, 2), (0, 7)),
        ('SPAN', (0, 8), (0, 10)),
        ('SPAN', (0, 11), (0, 12)),
        ('SPAN', (0, 13), (0, 15)),
        ('SPAN', (3, 2), (3, 7)),
        ('SPAN', (3, 8), (3, 10)),
        ('SPAN', (3, 11), (3, 12)),
        ('SPAN', (3, 13), (3, 15)),
        ('SPAN', (0, -1), (-2, -1)),
        ('VALIGN', (0, 0), (-1, -1), 'MIDDLE'),
        ('BACKGROUND', (0, 0), (-1, 0), colors.Color(220.0 / 255, 220.0 / 255, 220.0 / 255)),
        ('BACKGROUND', (0, -1), (-2, -1), colors.Color(220.0 / 255, 220.0 / 255, 220.0 / 255))
    ])
    colWidths = [6 * cm, 6 * cm, 4 * cm, 4 * cm]
    titlePg = Paragraph(_TITLE_REPORT, titleStyle)
    datePg = Paragraph(_DATE_NOW, titleStyle)
    logo = Image(st._IMG_LOGO_INGEMMET_COMPLETE, width=70, height=35)
    header = [[logo, titlePg, datePg]]
    tableHead = Table(header, colWidths=[3 * cm, 14 * cm, 3 * cm])
    tableHead.setStyle(tableStyle)
    elm.append(tableHead)
    elm.append(Spacer(0, 20))
    
    # Creando el cuerpo del documento
    ## Generando cabecera de la tabla del documento
    columns = []
    columns = [Paragraph('<b>{}</b>'.format(i), headStyle) for i in list(dataframe.columns)]
    # columns.insert(3, Paragraph('<b>PUNTAJE</b>', headStyle))
    headBody = [columns]
    rowStyleArray = [rowStyle, rowStyle2, rowStyle, rowStyle]

    ## Agregando registros a la tabla del documento
    for i, r in dataframe.iterrows():
        # area += i[-1]
        row = list(map(lambda m: Paragraph(str(r[df.columns[m]]), rowStyleArray[m]), range(len(df.columns))))
        # row.insert(-1, Paragraph(str(i + 1), headStyle))rr
        headBody.append(row)
    rowFooter = [Paragraph('<b>TOTAL</b>', headStyle), '', '', Paragraph(str(total), rowStyle)]
    headBody.append(rowFooter)
    rowHeights = [1 * cm] + [None] * len(df) + [1 * cm]
    tableHeadBody = Table(headBody, colWidths=colWidths, rowHeights=rowHeights, repeatRows=1)
    tableHeadBody.setStyle(tableGroupStyle)
    elm.append(tableHeadBody)

    # Observaciones
    elm.append(Spacer(0, 20))

    ptext = u'<br/><b>OBSERVACIONES:</b><br/><br/>\u2022 Ninguna<br/><br/>'

    if 'obs' in list(dataframe.columns):
        obs = list(set(dataframe['obs'].tolist()))
        obs = '<br/>\u2022 '.join(obs)
        ptext = u'<br/><b>OBSERVACIONES:</b><br/><br/>\u2022 {}<br/><br/>'.format(obs)

    obs_text = [[Paragraph(ptext, rowStyle2)]]
    tableObs = Table(obs_text, colWidths=[20 * cm])
    tableObs.setStyle(tableStyle)
    elm.append(tableObs)

    # Generando reporte
    response = SimpleDocTemplate(_PATH_REPORT, **_KWARGS)
    response.build(elm)

    shutil.copy2(_PATH_REPORT, _PATH_REPORT_SRV)
    return _PATH_REPORT_URL

def register_evaluation(query, *args):
    fc_eval = model.po_dgr_evaluacion_mg_50()
    # mtv = arcpy.Make(fc_eval.path, 'fc_eval', query)
    # arcpy.DeleteRows_management(mtv)
    fields = [
        fc_eval.score_pog,
        fc_eval.score_el,
        fc_eval.score_le,
        fc_eval.score_fm,
        fc_eval.score_se,
        fc_eval.score_tot,
        fc_eval.evaluador,
        fc_eval.fecha,
        fc_eval.url,
        fc_eval.anio,
        fc_eval.estado
    ]

    with arcpy.da.UpdateCursor(fc_eval.path, fields, query) as cursor_uc:
        for row in cursor_uc:
            row[0] = args[0]
            row[1] = args[1]
            row[2] = args[2]
            row[3] = args[3]
            row[4] = args[4]
            row[5] = args[5]
            row[6] = args[6]
            row[7] = args[7]
            row[8] = args[8]
            row[9] = args[9]
            row[10] = args[10]
            cursor_uc.updateRow(row)
        del cursor_uc


if __name__ == '__main__':
    try:
        arcpy.env.overwriteOutput = True
        response = dict()
        response['status'] = 1
        response['message'] = 'success'

        geodatabase = sys.argv[1]
        hojas = sys.argv[2]
        evaluador = sys.argv[3]

        if not arcpy.Exists(geodatabase):
            raise RuntimeError(msg._CPM_GEODATABASE_NOT_EXIST)
        
        if not hojas:
            raise RuntimeError(msg._ERROR_EMG_HOJA_NO_SELECCIONADA)
        
        cod_hojas = hojas.split(',')

        tb_hojas = model.gpo_dg_hojas_50()
        tb_hojas_path = os.path.join(geodatabase, tb_hojas.dataset, tb_hojas.name)
        if not arcpy.Exists(tb_hojas_path):
            raise RuntimeError(msg._ERROR_FEATURE_CUADRICULAS_MG)
        
        query = "{} in ('{}')".format(tb_hojas.codhoja, "','".join(cod_hojas))
        cursor_sc = arcpy.da.SearchCursor(tb_hojas_path, [tb_hojas.codhoja, tb_hojas.zona], query)
        dict_hojas_zona = {i[0]: i[1] for i in cursor_sc}

        # from datetime import datetime
        _DATETIME_NOW = datetime.now().strftime("%Y%m%d_%H%M%S")
        # arcpy.AddMessage(dir(datetime))
        # arcpy.AddMessage('_________{}_________'.format(_DATETIME_NOW))
        _FOLDER_PROCESS = os.path.join(arcpy.env.scratchFolder, 'evaluacion_{}'.format(_DATETIME_NOW))
        # _FOLDER_PROCESS = os.path.join(st._TEMP_FOLDER, 'evaluacion_{}'.format(_DATETIME_NOW))
        os.mkdir(_FOLDER_PROCESS)
        
        for hoja in cod_hojas:
            # writer = pd.ExcelWriter(r'D:\2022\temp\proc\prueba_{}.xls'.format(hoja), encoding='utf-8')
            df_data = list()
            query_cod_hoja = "{} = '{}'".format(tb_hojas.codhoja, hoja)
            
            # Score POG
            evl_pog = evaluation_pog(geodatabase, dict_hojas_zona[hoja], query_cod_hoja)
            df_data.extend(evl_pog)
            evl_lineal_elements = evaluation_lineal_elements(geodatabase, dict_hojas_zona[hoja], query_cod_hoja)
            df_data.extend(evl_lineal_elements)
            evl_legend = evaluation_legend(geodatabase, query_cod_hoja)
            df_data.extend(evl_legend)
            evl_fossil_samples = evaluation_fossil_samples(geodatabase, dict_hojas_zona[hoja], query_cod_hoja)
            df_data.extend(evl_fossil_samples)
            evl_geological_section = evaluation_geological_section(geodatabase, query_cod_hoja)
            df_data.extend(evl_geological_section)

            df = pd.DataFrame(df_data)

            scores = []
            total = 0
            # Score POG
            df_pog = df[df['GRUPO'] == _GROUP_NAME_POG]
            rows_pog = sum(df_pog['REGISTROS'].tolist())
            if rows_pog > 600:
                rows_pog = 600
            score_pog =  round((rows_pog/600.0)*10.0, _ROUND_DIGITS)
            total += score_pog
            scores.append(score_pog)

            # Score Elementos lineales
            df_el = df[df['GRUPO'] == _GROUP_NAME_EL]
            rows_el = sum(df_el['REGISTROS'].tolist())
            score_el = 10 if rows_el > 0 else 0
            total += score_el
            scores.extend([score_el]*len(df_el))

            # Score Leyenda
            df_le = df[df['GRUPO'] == _GROUP_NAME_LE]
            rows_le = df_le['REGISTROS'].tolist()
            score_le = 10 if 0 not in rows_le else 0
            total += score_le
            scores.extend([score_le]*len(df_le))

            # Score Fosiles Muestras
            df_fm = df[df['GRUPO'] == _GROUP_NAME_FM]
            rows_fm = sum(df_fm['REGISTROS'].tolist())
            score_fm = 10 if rows_fm > 0 else 0
            total += score_fm
            scores.extend([score_fm]*len(df_fm))

            # Score Seccion geologica
            df_sg = df[df['GRUPO'] == _GROUP_NAME_SG]
            rows_sg = df_sg['REGISTROS'].tolist()
            score_sg = 10 if 0 not in rows_sg else 0
            total += score_sg
            scores.extend([score_sg]*len(df_sg))

            df['PUNTAJE'] = scores

            df = df[['GRUPO', 'CAPA', 'REGISTROS', 'PUNTAJE']]

            url = generate_report(df, hoja, evaluador, total, _FOLDER_PROCESS)
            register_evaluation(
                query_cod_hoja, 
                score_pog, 
                score_el, 
                score_le, 
                score_fm, 
                score_sg, 
                total, 
                evaluador, 
                datetime.today(), 
                url,
                datetime.now().year,
                1
            )

        response['response'] = _FOLDER_PROCESS
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        stdout.write(response)
        stdout.flush()
        # arcpy.SetParameterAsText(3, response)
