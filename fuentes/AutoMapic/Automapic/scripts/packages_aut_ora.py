#!/usr/bin/env python
# -*- coding: utf-8 -*-
# from settings_aut import _CONNSTRING_BDGEOCAT
import cx_Oracle as ora
import pandas as pd


_CONNSTRING_BDGEOCAT = "publ_gis/publ_gis@bdgeocat"
connora =  ora.connect(_CONNSTRING_BDGEOCAT)
cursor = connora.cursor()

def oraPackageDecore(func):
    def decorator(*args, **kwargs):
        global connora    
        package = func(*args, **kwargs)
        if kwargs.get('getdataframe'):
            df = pd.read_sql(package, connora)
            return df
        else:
            cursor.execute(package)
            return cursor.fetchall()

    return decorator


@oraPackageDecore
def report_roc_min_ind(cd_mapa, getdataframe=True):
    query = '''
        SELECT 
        rownum "N°",
        B.NOMBRE "Nombre",
        B.CD_MTRA "Código de muestra",
        B.ESTE "Este",
        B.NORTE "Norte",
        B.SUSTANCIA "Sustancia",
        '' "Unidad Estratigráfica",
        A.CD_MAPA
    FROM UGEO1749.PT_DRME_REGISTRO_MP A, DATA_EDIT.GPT_RMI_Roc_Min_Ind B
    WHERE A.CD_MAPA = '{}' AND sde.st_intersects(A.SHAPE, B.SHAPE) = 1'''.format(cd_mapa)
    return query

@oraPackageDecore
def report_ocu_mineral_metalico(cd_mapa, getdataframe=True):
    query = '''
        SELECT 
            rownum "N°",
            B.NOMBRE "Cantera",
            B.TIPO_DEPOS "Tipo de Yacimientos",
            REPLACE(B.ELEMENTO, ' -', ',') "Elementos",
            '' "Minerales",
            '' "Roca caja",
            B.FORMACION "Formación",
            A.CD_MAPA
        FROM UGEO1749.PT_DRME_REGISTRO_MP A, GPT_OMF_OCURRENCIA_MINERAL_FR B
        WHERE A.CD_MAPA = '{}' AND B.TIPO LIKE 'Met%'
        AND sde.st_intersects(A.SHAPE, B.SHAPE) = 1'''.format(cd_mapa)
    return query

@oraPackageDecore
def report_ocu_mineral_no_metalico(cd_mapa, getdataframe=True):
    query = '''
        SELECT 
            rownum "N°",
            B.NOMBRE "Cantera",
            REPLACE(B.ELEMENTO, ' -', ',') "Sustancia",
            B.TIPO_DEPOS "Tipo de depósito",
            '' "Roca caja",
            B.FORMACION "Unidad Estratigráfica",
            B.EDAD "Edad Geológica",
            A.CD_MAPA
        FROM UGEO1749.PT_DRME_REGISTRO_MP A, GPT_OMF_OCURRENCIA_MINERAL_FR B
        WHERE A.CD_MAPA = '{}' AND B.TIPO LIKE 'No Met%'
        AND sde.st_intersects(A.SHAPE, B.SHAPE) = 1'''.format(cd_mapa)
    return query

@oraPackageDecore
def report_geoquimica_sedimentos(cd_mapa, getdataframe=True):
    query = '''
        SELECT rownum   "N°",
            B.CODIGO "Código",
            UTM_E    "Este",
            UTM_N    "Norte",
            ZONA     "Zona",
            AU_PPB   "Au (ppb)",
            AG_PPM   "Ag (ppm)",
            CU_PPM   "Cu (ppm)",
            PB_PPM   "Pb (ppm)",
            ZN_PPM   "Zn (ppm)",
            AS_PPM   "As (ppm)"
        FROM UGEO1749.PT_DRME_REGISTRO_MP A, DATA_GIS.GPT_GEO_GeoqSedimento B
        WHERE A.CD_MAPA = '{}'
        AND sde.st_intersects(A.SHAPE, B.SHAPE) = 1'''.format(cd_mapa)
    return query

@oraPackageDecore
def report_drme_pm_data_general(getdataframe=True):
    query = '''
        SELECT A.CD_MAPA,
            DECODE(A.TIPO, 1, 1, 2, 0, NULL) TIPO,
            A.DOCUMENTO,
            A.TITULO,
            A.AUTOR,
            A.REVISOR,
            A.ESCALA,
            A.ORIENTACION,
            A.HOJA,
            SUBSTR(A.SRC, 4, 2) ZONA,
            TO_CHAR(A.FECHA, 'DD/MM/YYYY') FECHA,
            A.FORMATO,
            A.CD_DEPA,
            B.NM_DEPA
        FROM UGEO1749.TB_DRME_REGISTRO_MP A
        LEFT JOIN DATA_GIS.GPO_DEP_DEPARTAMENTO B
            ON A.CD_DEPA = B.CD_DEPA
        WHERE ESTADO = 1
    '''
    return query

@oraPackageDecore
def report_dgar_pg_data_general(getdataframe=True):
    query = '''
        SELECT 
            P.CD_DEPA,
            DECODE(P.CD_DEPA, '99', 'PERU', R.NM_DEPA) "DEPARTAMENTO PRINCIPAL",
            Q.DEPARTAMENTO,
            Q.PROVINCIA,
            Q.DISTRITO,
            P.SECTOR,
            P.ZONA,
            P.X ESTE,
            P.Y NORTE,
            TO_CHAR(P.FEC_SOLICITUD, 'YYYY') "AÑO",
            P.SOLICITANTE,
            P.EMISOR,
            P.DOCUMENTO,
            P.CD_CORREL CORRELATIVO,
            TO_CHAR(P.FEC_SOLICITUD, 'dd/mm/yyyy') FECHA,
            P.ASUNTO,
            P.TIPO_INFO "TIPO DE INFORMACIÓN",
            P.RESPONSABLES "RESPONSABLE DE ATENCIÓN",
            TO_CHAR(P.FEC_ASIGNACION, 'dd/mm/yyyy') "FECHA DE NOTIFICACIÓN",
            P.ESTADO,
            TO_CHAR(P.FEC_ATENCION, 'dd/mm/yyyy') "FECHA DE ENTREGA",
            P.OBSERVACION "OBSERVACIONES"
        FROM UGEO1749.TB_DGAR_REGISTRO_PG P
        LEFT JOIN (SELECT PSID,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_DEPA || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS DEPARTAMENTO,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_PROV || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS PROVINCIA,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_DIST || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS DISTRITO
                    FROM (SELECT A.PSID, B.NM_DEPA, B.NM_PROV, B.NM_DIST
                            FROM UGEO1749.TB_DGAR_REGISTRO_PG_DIST A
                            LEFT JOIN DATA_GIS.GPO_DIS_DISTRITO_G84 B
                                ON A.CD_DIST = B.CD_DIST)
                    GROUP BY PSID) Q
            ON P.PSID = Q.PSID
        LEFT JOIN (SELECT * FROM DATA_GIS.GPO_DEP_DEPARTAMENTO_G84 WHERE CD_DEPA <> '99') R
            ON P.CD_DEPA = R.CD_DEPA WHERE TO_CHAR(P.FEC_ATENCION, 'YYYY') = TO_CHAR(sysdate, 'YYYY')
             order by P.FEC_ATENCION DESC
    '''
    return query

@oraPackageDecore
def report_dgar_pg_data_general_by_dates(start_date, end_date, getdataframe=True):
    query = '''
        SELECT 
            P.CD_DEPA,
            DECODE(P.CD_DEPA, '99', 'PERU', R.NM_DEPA) "DEPARTAMENTO PRINCIPAL",
            Q.DEPARTAMENTO,
            Q.PROVINCIA,
            Q.DISTRITO,
            P.SECTOR,
            P.ZONA,
            P.X ESTE,
            P.Y NORTE,
            TO_CHAR(P.FEC_SOLICITUD, 'YYYY') "AÑO",
            P.SOLICITANTE,
            P.EMISOR,
            P.DOCUMENTO,
            P.CD_CORREL CORRELATIVO,
            TO_CHAR(P.FEC_SOLICITUD, 'dd/mm/yyyy') FECHA,
            P.ASUNTO,
            P.TIPO_INFO "TIPO DE INFORMACIÓN",
            P.RESPONSABLES "RESPONSABLE DE ATENCIÓN",
            TO_CHAR(P.FEC_ASIGNACION, 'dd/mm/yyyy') "FECHA DE NOTIFICACIÓN",
            P.ESTADO,
            TO_CHAR(P.FEC_ATENCION, 'dd/mm/yyyy') "FECHA DE ENTREGA",
            P.OBSERVACION "OBSERVACIONES"
        FROM UGEO1749.TB_DGAR_REGISTRO_PG P
        LEFT JOIN (SELECT PSID,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_DEPA || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS DEPARTAMENTO,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_PROV || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS PROVINCIA,
                            REGEXP_REPLACE(rtrim(xmlagg(xmlelement(e, NM_DIST || ', ')).extract('//text()'),
                                                ' '),
                                        '([^ ]+)( [ ]*\1)+',
                                        '\1') AS DISTRITO
                    FROM (SELECT A.PSID, B.NM_DEPA, B.NM_PROV, B.NM_DIST
                            FROM UGEO1749.TB_DGAR_REGISTRO_PG_DIST A
                            LEFT JOIN DATA_GIS.GPO_DIS_DISTRITO_G84 B
                                ON A.CD_DIST = B.CD_DIST)
                    GROUP BY PSID) Q
            ON P.PSID = Q.PSID
        LEFT JOIN (SELECT * FROM DATA_GIS.GPO_DEP_DEPARTAMENTO_G84 WHERE CD_DEPA <> '99') R
            ON P.CD_DEPA = R.CD_DEPA WHERE P.FEC_ATENCION BETWEEN TO_DATE('{}', 'DD/MM/YYYY') AND TO_DATE('{}', 'DD/MM/YYYY')
             order by P.FEC_ATENCION DESC
    '''.format(start_date, end_date)
    return query


@oraPackageDecore
def report_dgar_pg_data_departament(anio, getdataframe=True):
    query = '''
        SELECT NVL(A.NM_DEPA, 'PERU') DEPARTAMENTO, B.TOTAL
        FROM (SELECT CD_DEPA, NM_DEPA
                FROM DATA_GIS.GPO_DEP_DEPARTAMENTO_G84
                WHERE CD_DEPA <> '99'
                ORDER BY CD_DEPA) A
        FULL OUTER JOIN (SELECT CD_DEPA, COUNT(*) TOTAL
                            FROM UGEO1749.TB_DGAR_REGISTRO_PG
                            WHERE TO_CHAR(FEC_ATENCION, 'YYYY') = '{}'
                            GROUP BY CD_DEPA) B
            ON A.CD_DEPA = B.CD_DEPA
    '''.format(anio)
    return query


@oraPackageDecore
def report_dgar_pg_data_departament_month(anio, getdataframe=True):
    query = '''
        SELECT 
            NVL(A.NM_DEPA, 'PERU') DEPARTAMENTO,
            B.ENERO,
            B.FEBRERO,
            B.MARZO,
            B.ABRIL,
            B.MAYO,
            B.JUNIO,
            B.JULIO,
            B.AGOSTO,
            B.SETIEMBRE,
            B.OCTUBRE,
            B.NOVIEMBRE,
            B.DICIEMBRE
        FROM (SELECT CD_DEPA, NM_DEPA
                FROM DATA_GIS.GPO_DEP_DEPARTAMENTO_G84
                WHERE CD_DEPA <> '99'
                ORDER BY CD_DEPA) A
        FULL OUTER JOIN (
        select CD_DEPA,
            sum(case when to_char(FEC_ATENCION, 'MM') = '01' then 1 end) as Enero,
            sum(case when to_char(FEC_ATENCION, 'MM') = '02' then 1 end) as Febrero,
            sum(case when to_char(FEC_ATENCION, 'MM') = '03' then 1 end) as Marzo,
            sum(case when to_char(FEC_ATENCION, 'MM') = '04' then 1 end) as Abril,
            sum(case when to_char(FEC_ATENCION, 'MM') = '05' then 1 end) as Mayo,
            sum(case when to_char(FEC_ATENCION, 'MM') = '06' then 1 end) as Junio,
            sum(case when to_char(FEC_ATENCION, 'MM') = '07' then 1 end) as Julio,
            sum(case when to_char(FEC_ATENCION, 'MM') = '08' then 1 end) as Agosto,
            sum(case when to_char(FEC_ATENCION, 'MM') = '09' then 1 end) as Setiembre,
            sum(case when to_char(FEC_ATENCION, 'MM') = '10' then 1 end) as Octubre,
            sum(case when to_char(FEC_ATENCION, 'MM') = '11' then 1 end) as Noviembre,
            sum(case when to_char(FEC_ATENCION, 'MM') = '12' then 1 end) as Diciembre
        from UGEO1749.TB_DGAR_REGISTRO_PG p WHERE TO_CHAR(FEC_ATENCION, 'YYYY') = '{}'
        group by CD_DEPA) B ON A.CD_DEPA = B.CD_DEPA
    '''.format(anio)
    return query


@oraPackageDecore
def report_dgar_pg_data_departament_years(getdataframe=True):
    query = '''
    SELECT CD_DEPA, TO_CHAR(FEC_ATENCION, 'YYYY') ANIO, COUNT(*) ATENCIONES
    FROM UGEO1749.TB_DGAR_REGISTRO_PG
    GROUP BY CD_DEPA, TO_CHAR(FEC_ATENCION, 'YYYY')

    '''
    return query


@oraPackageDecore
def get_departaments(getdataframe=True):
    query = '''
        select CD_DEPA, NM_DEPA from DATA_GIS.GPO_DEP_DEPARTAMENTO_G84 WHERE CD_DEPA <> '99'
    '''
    return query

@oraPackageDecore
def get_modules_by_user(usuario, getdataframe=True):
    query = u'''
        SELECT 
            ID_MODULO 
        FROM ugeo1749.vw_osi_aut_access 
        WHERE USUARIO = '{0}' AND ID_PERFIL <> '0' 
        union all
        select 
            0 ID_MODULO 
        from dual '''.format(usuario)
    return query

@oraPackageDecore
def get_users_from_active_directory(getdataframe=True):
    query = u'''
    select samaccountname id_usuario, name nombre 
    from userhdesk.adtemporal@bdgeocat_huawei
    where deleted = 0 order by samaccountname'''
    return query

@oraPackageDecore
def get_report_users_automapic(getdataframe=True):
    query = """
        SELECT 
            id_usuario, 
            usuario, 
            id_modulo, 
            modulo, 
            id_perfil, 
            perfil 
        FROM ugeo1749.vw_osi_aut_access 
        where id_perfil <> 0 
        order by id_usuario, id_modulo"""
    return query

@oraPackageDecore
def getfeaturesfromsde(getdataframe=True):
    query = """select physicalname from sde.gdb_items where  SUBSTR(physicalname, 1, 8) IN ('DATA_GIS')"""
    return query
    
# if __name__ == '__main__':
#     x = get_users_from_active_directory(getdataframe=False)
#     for i in x:
#         print i
