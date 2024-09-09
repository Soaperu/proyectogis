#!/usr/bin/env python
# -*- coding: utf-8 -*-

import sys
import arcpy
import os
import traceback
import json
import pandas as pd
import datetime
import getpass
import sqlite3
import cx_Oracle as oracle

arcpy.env.overwriteOutput= True

# _DATABASE_PATH = os.path.join(os.path.dirname(__file__), 'automapic.db')
_DATABASE_PATH = os.path.expandvars(r'%localappdata%\ESRI\Desktop10.4\assemblycache\{7652433A-59B4-4E33-A60F-8C9DC6BF0358}\scripts\automapic.db')

conn = sqlite3.connect(_DATABASE_PATH, isolation_level=None, check_same_thread=False)
conn_oracle =  oracle.connect("data_cat", "data_cat","bdgeocat",encoding="UTF-8")
cursor_oracle = conn_oracle.cursor()

_ORIGEN = "Todas las capas de la fuente"
_EXTFILE = "Archivo CSV"
_MXD_ACTUAL = "Capas del MXD Actual"
_MXD_EXTERNO = "Archivo MXD Externo"
_ERROR_EMPTY_MXD = 'El MXD Actual no contiene capas'
_ERROR_DIALOG = u'¡ERROR!'
_WARNING_FORMATO_FILTRO = 'Formato de filtro no reconocido'
_WARNING_DIALOG = u'ADVERTENCIA'

response = dict()
response['status'] = 1
response['message'] = 'success'


def oracleDecore(func):
    def decorator(*args, **kwargs):
        global conn_oracle, cursor_oracle    
        package = func(*args, **kwargs)
        if kwargs.get('iscommit'):
            cursor_oracle.execute(package)
            return
        elif kwargs.get('one'):
            cursor_oracle.execute(package)
            return cursor_oracle.fetchone()[0]
        elif kwargs.get('as_dataframe'):
            import pandas as pd
            return pd.read_sql(package, conn_oracle)
        else:
            cursor_oracle.execute(package)
            return cursor_oracle.fetchall()

    return decorator
@oracleDecore
def getparametros(key):
    sql ="""SELECT CODIGO, ADICIONAL GDB_INI, ADICIONAL2 GDB_FIN, ADICIONAL3 FILTRO, ADICIONALVV DATASET FROM 
    ged_m_lista WHERE TIPO = 'SINCRONIZACION_GDB' AND CODIGO = '{}'""".format(key)
    return sql

@oracleDecore
def getfeaturesfromsde():
    sql = """select physicalname from sde.gdb_items where  SUBSTR(physicalname, 1, 8) IN ('DATA_GIS')"""
    return sql

def tableize(df, col_len):

    """
    funcion para imprimir un dataframe como tabla en el terminal
    """
    if not isinstance(df, pd.DataFrame):
        return
    df_columns = df.columns.tolist() 
    max_len_in_lst = lambda lst: col_len
    align_center = lambda st, sz: "{0}{1}{0}".format(" "*(1+(sz-len(st))//2), st)[:sz] if len(st) < sz else st
    align_right = lambda st, sz: "{0}{1} ".format(" "*(sz-len(st)-1), st) if len(st) < sz else st
    ajustado = lambda txt : "%s..."%txt[0:col_len-3]if len(txt)>col_len else txt
    max_col_len = max_len_in_lst(df_columns)
    max_val_len_for_col = dict([(col, max_len_in_lst(df.iloc[:,idx].astype('str'))) for idx, col in enumerate(df_columns)])
    col_sizes = dict([(col, 2 + max(max_val_len_for_col.get(col, 0), max_col_len)) for col in df_columns])
    build_hline = lambda row: '+'.join(['-' * col_sizes[col] for col in row]).join(['+', '+'])
    build_data = lambda row, align: "|".join([align(ajustado(str(val)), col_sizes[df_columns[idx]]) for idx, val in enumerate(row)]).join(['|', '|'])
    hline = build_hline(df_columns)
    out = [hline, build_data(df_columns, align_center), hline]
    for _, row in df.iterrows():
        out.append(build_data(row.tolist(), align_right))
    out.append(hline)
    return "\n".join(out)


def getcapas(gdb, ds=None):
    """
    Obtiene el listado de FeatureClass y Tablas de una gdb
    params:
    gdb: gdb en la cual se buscarán los elementos a listar
    ds: si no es nulo se lista dataset/fc, de ser nulo solo se muestra el nombre de la capa
    """

    # Se agrega ese filtro para el uso de la sde de oracle
    if '.sde' in gdb:
        listacapas = [x[0] for x in getfeaturesfromsde()]
    
    else :        
        arcpy.env.workspace = gdb
        listacapas = []
        capasraiz = arcpy.ListFeatureClasses()
        listatablas = arcpy.ListTables()
        listacapas = listacapas + capasraiz + listatablas
        lds = arcpy.ListDatasets()
        if len(lds) > 0:
            for dataset in lds:
                arcpy.env.workspace = os.path.join(gdb, dataset)
                ltemp = arcpy.ListFeatureClasses()
                if ds is not None:
                    ltemp = list(map(lambda x:os.path.join(dataset,x), ltemp))
                listacapas = listacapas + ltemp
    return listacapas


def get_filtrados(valor, lista, gdb):
    """
    Busca que el texto del valor ingresado se encuentre en el elemento de lista
    Si el valor existe, devuelve el elemento de la lista que lo contiene y el indicador 1
    Si el valor no existe, devuelve el nombre del valor y el indicador 0
    """
    for i in lista:
        # if valor[0] in i:
        if i.endswith(valor[0]):

            return [1, i, os.path.join(gdb,i)]

    return [0, valor[0],valor[1]]


def get_nombre_destino(valor, ds):
    """
    devuelve el nombre de la capa con o sin dataset especificado segun filtro
    """
    if '.' in valor:
        valor = valor.split('.')[1]
    if ds:
        return valor
    else:
        return os.path.basename(valor)


def existe_destino(valor, gdb):
    """
    Valida si el valor existe dentro de la gdb, retorna 1 si existe, 0 si no
    """
    if '.' in valor:
        valor = valor.split('.')[1]
    ruta = os.path.join(gdb, valor)
    if arcpy.Exists(ruta):
        return 1
    else:
        return 0

def getnum_ini(source):
    """
    retorna el numero de registros que contiene la capa inicial en el source especificado
    """
    num=0
    if str(source)=="0":
        pass
    else:
        num = int(arcpy.GetCount_management(source).getOutput(0))
    return num

def getnum_fin(capa, gdb_final):
    """
    retorna el numero de registro que contiene una capa perteneciente a un gdb_final
    """
    arcpy.env.workspace = gdb_final
    ds = None
    fc= None
    lista_splitcapa = os.path.split(capa)
    if len(lista_splitcapa)>1:
        ds,fc = lista_splitcapa
    else:
        fc = capa
    num=0
    if ds:
        if arcpy.Exists(ds):
            arcpy.env.workspace=ds
            capa_path = os.path.join(gdb_final, ds, fc)
            if arcpy.Exists(capa_path):
                num = int(arcpy.GetCount_management(capa_path).getOutput(0))
    else :
        arcpy.env.workspace = gdb_final
        capa_path = os.path.join(gdb_final, fc)
        if arcpy.Exists(capa_path):
            num = int(arcpy.GetCount_management(capa_path).getOutput(0))
    # return gdb_final+capa+"-"+str(num)
    return num


def getboolean(numero):
    if numero<=0:
        return 0
    else:
        return 1
        

def get_tabla(gdbini, gdbfin, filtro=None, ds='si'):
    print("Obtenemos listado de capas de la gdb fuente")
    lista_ini = getcapas(gdbini, ds='si')
    # lista_fin = getcapas(ini, ds)
    lista_filtro = []
    print("Aplicamos los filtros para obtener solo capas de interes")
    if filtro == _ORIGEN:
        pass
    elif filtro == _MXD_ACTUAL or filtro.endswith('.mxd'):
        if filtro == _MXD_ACTUAL:
            mxd = arcpy.mapping.MapDocument("current")
        else:
            mxd = arcpy.mapping.MapDocument(filtro)
        listalayers = arcpy.mapping.ListLayers(mxd)
        listatablas = arcpy.mapping.ListTableViews(mxd)
        if len(listalayers) >0:
            for capa in listalayers:
                if capa.isFeatureLayer:
                    source = capa.dataSource
                    if not capa.isBroken:
                        if '.gdb' in source:
                            name = source.split('.gdb')[1][1:]
                        elif '.sde' in source:
                            name = capa.datasetName
                            source =0
                            name = name.split('.')[-1]
                        else:
                            name =os.path.basename(source)
                        lista_filtro.append([name, source])
        if len(listatablas)>0:
            for tabla in listatablas:
                source = tabla.dataSource
                if not tabla.isBroken:
                    if '.gdb' in source:
                        name = source.split('.gdb')[1][1:]
                    elif '.sde' in source:
                        name = source.split('.sde')[1][1:]
                    else:
                        name =os.path.basename(source)
                    lista_filtro.append([name,source])
        
        if len(listalayers)==0 and len(listatablas)==0:
            print(_ERROR_DIALOG)
            print(_ERROR_EMPTY_MXD)


    elif filtro.endswith('.csv') or filtro.endswith('.xls') or filtro.endswith('.xlsx') :
        if filtro.endswith('.csv'):
            dataf = pd.read_csv(filtro)
        elif filtro.endswith('.xls') or filtro.endswith('.xlsx'):
            dataf = pd.read_excel(filtro)
        else:
            return 0
        firstcolumn = dataf.iloc[:, 0]
        listafiltro = firstcolumn.tolist()
        lista_filtro = [[x.strip(),0] for x in listafiltro]
    
    else:
        print(_WARNING_DIALOG)
        print(_WARNING_FORMATO_FILTRO)
    if len(lista_filtro) == 0:
        # Agregamos nuevo elemnto para source
        lista_ini_ = [[1,x, os.path.join(gdbini,x)] for x in lista_ini] 
    else:
        lista_ini_ = list(
            map(lambda x: get_filtrados(x, lista_ini, gdbini), lista_filtro))
    


    dff = pd.DataFrame(lista_ini_)
    dff.columns = ["existe_origen", "origen","source"]    
    dff["existe_destino"] = dff.apply(
        lambda x: existe_destino(x["origen"], gdbfin), axis=1)
    dff["nombre_destino"] = dff.apply(
        lambda x: get_nombre_destino(x["origen"], ds), axis=1)
    dff["num_origen"] = dff.apply(
        lambda x: getnum_ini(x["source"]), axis=1)
    dff["num_destino"] = dff.apply(
        lambda x: getnum_fin(x["nombre_destino"], gdbfin), axis=1)
    
    dff["enviar"] = dff["existe_origen"] +dff["num_origen"] -dff["existe_destino"] - dff["num_destino"]
    dff["enviar"] = dff.apply(lambda x :getboolean(x["enviar"]), axis=1)
    catastro= "GPO_CMI_CATA"
    dff["enviar"] = dff.apply(lambda x : 1 if catastro in x["nombre_destino"] and x["num_origen"] != x["num_destino"] else x["enviar"] , axis=1)

    resultado = dff
    # pathcsv = "D:\JYUPANQUI\csvpruebita_num.csv"
    # dff.to_csv(pathcsv)

    return resultado

def enviardatos(gdbini, gdbfin, df, usuario):
    df = df
    contador = 0
    for index, row in df.iterrows():
        if row["enviar"]== True:
            fc_inicial = row["source"]
            fc_destino = os.path.join(gdbfin, row["nombre_destino"].split('.')[0])

            try:
                desc = arcpy.Describe(fc_inicial)
                if desc.dataType in [u'FeatureClass', u'ShapeFile']:
                    arcpy.CopyFeatures_management(fc_inicial,fc_destino)
                elif desc.dataType == u'Table':
                    arcpy.CopyRows_management(fc_inicial,fc_destino)
                elif desc.dataType in [u'TextFile',u'File']:
                    path, file = os.path.split(fc_destino)
                    arcpy.TableToTable_conversion(fc_inicial, path, file)
                else:
                    pass
                
                contador +=1
            except:
                desc = arcpy.Describe(fc_inicial)
                if desc.dataType in [u'FeatureClass', u'ShapeFile']:
                    arcpy.TruncateTable_management(fc_destino)
                    arcpy.Append_management(fc_inicial,fc_destino,"NO_TEST")
                elif desc.dataType == u'Table':
                    arcpy.TruncateTable_management(fc_destino)
                    arcpy.Append_management(fc_inicial,fc_destino,"NO_TEST")
                else:
                    pass
                
                contador +=1
    
    df2 = df[df["enviar"]==True].copy()
    df2 = df2.drop(df2.columns[-1], axis=1)

    df2["tipo"] = "UPDATE"
    df2.loc[(df.existe_destino == 0) | (df2.existe_origen == 0), "tipo"] = "INSERT"
    df2["usuario"] = usuario
    df2["fecha"] = datetime.datetime.now()
    df2.to_sql('tb_sinc_gdb', con=conn, if_exists='append', index=False)

    return contador


if __name__ == '__main__':
    # gdb_origen = sys.argv[1]
    # gdb_destino = sys.argv[2]
    # filtro = sys.argv[3] if sys.argv[3:] else _ORIGEN
    # dataset = sys.argv[4] if sys.argv[4:] else 'si'

    archivo = sys.argv[1]
    # archivo = "NUBE"
    fetchparams = getparametros(archivo)[0]
    params = list(filter(lambda x: x, fetchparams))
    print("Obtenemos parametros de ejecucion")
    print(params)
    gdb_origen = params[1]
    gdb_destino = params[2]
    filtro = params[3] if params[3:] else _ORIGEN
    dataset = params[4] if params[4:] else 'si'
    usuario = "windows_{}".format(getpass.getuser())

    try:
        print("Obtenemos tabla de resultados")        
        dataframeAsJson = get_tabla(gdb_origen, gdb_destino, filtro, dataset)
        print(tableize(dataframeAsJson,15))
        num_capas_actualizadas = enviardatos(gdb_origen, gdb_destino, dataframeAsJson, usuario)
        print("Se actualizaron satisfactoriamente {} capas".format(num_capas_actualizadas))
        response['response'] = num_capas_actualizadas
        
    except Exception as e:
        response['status'] = 0
        # response['message'] = e.message
        response['message'] = traceback.format_exc()
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(4, response)
        print(response)
