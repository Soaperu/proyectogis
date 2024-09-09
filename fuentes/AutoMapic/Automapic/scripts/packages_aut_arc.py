#!/usr/bin/env python
# -*- coding: utf-8 -*-
import settings_aut as st
import arcpy

arcpy.env.workspace = st._BASE_DIR
connarc = arcpy.ArcSDESQLExecute(st._BDGEOCAT_NAME)

def arcPackageDecore(func):
    def decorator(*args, **kwargs):
        global connarc    
        package = func(*args, **kwargs)
        # cursor = connarc.execute(package)
        # return cursor
        if kwargs.get('iscommit'):
            connarc.execute(package)
            connarc.commitTransaction()
            return
        elif kwargs.get('getcursor'):
            cursor = connarc.execute(package)
            return cursor

    return decorator

@arcPackageDecore
def get_all_regions(getcursor=True):
    query = '''
        SELECT 
            CD_DEPA, 
            CAST(CD_DEPA || ' - ' || NM_DEPA as varchar(100)) 
        FROM DATA_GIS.GPO_DEP_DEPARTAMENTO 
        WHERE CD_DEPA <> '99' 
        order by CD_DEPA
    '''
    return query

@arcPackageDecore
def get_last_code(getcursor=True):
    # query = '''
    #     SELECT 
    #         decode(count(*), 0, 'null', MIN(CD_MAPA)) 
    #     FROM (
    #         SELECT 
    #             CD_MAPA 
    #         FROM UGEO1749.TB_DRME_REGISTRO_MP 
    #         WHERE TIPO IN (1, 2) 
    #         ORDER BY FECHA DESC
    #     ) WHERE ROWNUM  = 1
    # '''
    query = '''
        SELECT decode(count(*), 0, 'null', MIN(CD_MAPA))
        FROM (SELECT substr(CD_MAPA, 1, 15) CD_MAPA
                FROM UGEO1749.TB_DRME_REGISTRO_MP
                WHERE TIPO IN (1, 2)
                order by substr(CD_MAPA, 1, 15) DESC)
        WHERE ROWNUM = 1
    '''
    return query

@arcPackageDecore
def insert_code(*args, **kwargs):
    query = '''
        INSERT INTO UGEO1749.TB_DRME_REGISTRO_MP 
            (
                OBJECTID, CD_MAPA, TIPO, TITULO, 
                AUTOR, REVISOR, ESCALA, ORIENTACION, HOJA, 
                SRC, FECHA, ESTADO, DETALLE, FORMATO, CD_DEPA, DOCUMENTO)
            VALUES
            (
                SDE.GDB_UTIL.next_rowid('UGEO1749', 'TB_DRME_REGISTRO_MP'), 
                '{}', '{}', '{}', '{}', '{}', {}, '{}','{}', {}, sysdate, 9, '', '{}', '{}', '{}')
    '''.format(*args)
    return query

@arcPackageDecore
def update_state_row(cd_mapa, state, detail='', iscommit=True):
    query = '''
        UPDATE 
            UGEO1749.TB_DRME_REGISTRO_MP 
            SET ESTADO = {}, DETALLE = '{}' 
            WHERE CD_MAPA = '{}'
    '''.format(state, detail, cd_mapa)
    return query


@arcPackageDecore
def get_map_by_code(cd_mapa, getcursor=True):
    query = '''
        SELECT 
            'CÓDIGO: '||CD_MAPA || CHR(10) || 
            'TITULO: ' || TITULO || CHR(10) || 
            'AUTOR: '|| AUTOR || CHR(10) || 
            'FECHA: '|| FECHA
        FROM UGEO1749.TB_DRME_REGISTRO_MP
        WHERE ESTADO = 1 AND CD_MAPA = '{0}'
        UNION ALL
        SELECT NULL FROM DUAL
        WHERE NOT EXISTS (
        SELECT * 
        FROM UGEO1749.TB_DRME_REGISTRO_MP 
        WHERE ESTADO = 1 AND CD_MAPA = '{0}')'''.format(cd_mapa)
    return query

@arcPackageDecore
def insert_row_pg_dgar(*args, **kwargs):
    query = u'''
        INSERT INTO UGEO1749.TB_DGAR_REGISTRO_PG
        (
            OBJECTID, CD_CORREL, SECTOR, DOCUMENTO, FEC_SOLICITUD, SOLICITANTE,
            EMISOR, ASUNTO, TIPO_INFO, RESPONSABLES, FEC_ASIGNACION, ESTADO, FEC_ATENCION,
            OBSERVACION, CD_DEPA, PSID, X, Y, ZONA
        )
        VALUES
        (
            SDE.GDB_UTIL.next_rowid('UGEO1749', 'TB_DGAR_REGISTRO_PG'),
            '{}', '{}', '{}', TO_DATE('{}', 'dd/mm/yyyy'), '{}', '{}', '{}', '{}', '{}',
            TO_DATE('{}', 'dd/mm/yyyy'), '{}', TO_DATE('{}', 'dd/mm/yyyy'), '{}', '{}', '{}', {}, {}, {})
    '''.format(*args)
    return query


@arcPackageDecore
def insert_row_pg_dgar_distritos(*args, **kwargs):
    query = u'''
        INSERT INTO UGEO1749.TB_DGAR_REGISTRO_PG_DIST
        (
            OBJECTID, PSID, CD_DIST
        )
        VALUES
        (SDE.GDB_UTIL.next_rowid('UGEO1749', 'TB_DGAR_REGISTRO_PG_DIST'), '{}', '{}')
    '''.format(*args)
    return query

@arcPackageDecore
def getusersdatagrid(getcursor=True):
    query = u'''
        SELECT a.id_usuario id, a.usuario, a.nombres, a.apepat, b.nombre oficina 
        FROM ugeo1749.tb_osi_aut_usuarios a, ugeo1749.tb_osi_aut_oficina b 
        where a.id_oficina = b.id_oficina order by a.id_usuario'''
    return query

@arcPackageDecore
def get_users_list_tb(getcursor=True):
    query = u'''
        SELECT id_usuario, usuario, id_oficina, NVL(nombres,' '), NVL(apepat,' '),NVL(apemat,' ')
        FROM ugeo1749.tb_osi_aut_usuarios order by id_usuario'''
    return query

@arcPackageDecore
def get_max_id(getcursor=True):
    query = u'''
        SELECT MAX(id_usuario)+1 FROM ugeo1749.tb_osi_aut_usuarios'''
    return query

@arcPackageDecore
def get_oficinas_names(getcursor=True):
    query = u'''
        SELECT id_oficina, nombre FROM ugeo1749.tb_osi_aut_oficina order by nombre'''
    return query

@arcPackageDecore
def get_perfiles(getcursor=True):
    query = u'''
        SELECT id_perfil, nombre FROM ugeo1749.tb_osi_aut_perfil order by id_perfil'''
    return query

@arcPackageDecore
def insert_row_tb_osi_usuarios(*args, **kwargs):
    query = u'''
    insert into ugeo1749.tb_osi_aut_usuarios
    (
        objectid, id_usuario, usuario, id_oficina, estado, login, nombres, apepat,apemat
        )
    values
    ( SDE.GDB_UTIL.next_rowid('UGEO1749', 'tb_osi_aut_usuarios'),
        {},'{}',{},1,0,'{}','{}','{}')
    '''.format(*args)
    return query

@arcPackageDecore
def edit_row_tb_osi_usuarios(*args, **kwargs):
    query = u'''
    update ugeo1749.tb_osi_aut_usuarios
    set usuario = '{1}', id_oficina = {2},  nombres = '{3}', apepat = '{4}', apemat = '{5}'
    where id_usuario = {0}
    '''.format(*args)
    return query

@arcPackageDecore
def delete_row_tb_osi_usuarios(*args, **kwargs):
    query = u'''
    delete from ugeo1749.tb_osi_aut_usuarios
    where id_usuario = {}
    '''.format(*args)
    return query

@arcPackageDecore
def delete_row_tb_osi_access(*args, **kwargs):
    query = u'''
    delete from ugeo1749.tb_osi_aut_access
    where id_usuario = {}
    '''.format(*args)
    return query

@arcPackageDecore
def get_access_information_by_id(id_user, getcursor=True):
    query = u'''
    select 
       b.id_modulo, 
       b.nombre, 
       decode(nvl(a.id_perfil,0),0,0,1) booleano, 
       nvl(a.id_perfil, 0) perfil
    from (select * from
       ugeo1749.tb_osi_aut_access where id_usuario={}) a right join
       ugeo1749.tb_osi_aut_modulo b on a.id_modulo = b.id_modulo
       where b.id_modulo not IN (0)  
       order by booleano desc, b.id_modulo
    '''.format(id_user)
    return query

@arcPackageDecore
def insert_row_tb_osi_access(*args, **kwargs):
    query = u'''
    DECLARE
	    existe number;
    BEGIN
    select count(*) into existe from ugeo1749.tb_osi_aut_access where id_usuario = {0} and id_modulo = {1};
    if( existe = 1)
        then
        update ugeo1749.tb_osi_aut_access
        set id_perfil = {2}
        where id_usuario = {0} and id_modulo = {1};
    else
        insert into ugeo1749.tb_osi_aut_access(objectid, id_usuario, id_modulo, id_perfil)
        values (SDE.GDB_UTIL.next_rowid('UGEO1749', 'tb_osi_aut_access'), {0}, {1}, {2});
    end if;
    END; 
    '''.format(*args)
    return query

