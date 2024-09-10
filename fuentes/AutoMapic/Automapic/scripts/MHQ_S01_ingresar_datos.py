#!/usr/bin/env python
# -*- coding: utf-8 -*-
# Importar librerias
import arcpy
import os
import json
import pandas as pd
import difflib as diff
import traceback
import settings_aut as st
import numpy as np
import packages_aut as pkg
import messages_aut as msg

response = dict()
response['status'] = 1
response['message'] = 'success'

gdb_base = arcpy.GetParameterAsText(0)
xls_file = arcpy.GetParameterAsText(1)
# gdb_base = r"D:\jyupanqui\pruebas\GDB_LBG.gdb"
# xls_file = r"D:\jyupanqui\proyectos\dgar\BD_GA47C_AREQUIPA_ves4.xls"

## Definimos las variables estaticas
_scratch = arcpy.env.scratchFolder
_scratchgdb = arcpy.env.scratchGDB
_nombre_tabla = r'MHQ_BASE_EXCEL_LAB_TABLA'
_nombre_tabla_corregida = r'MHQ_BASE_EXCEL_LAB_CORREGIDA'
_nombre_fc = r'MHQ_BASE_EXCEL_LAB_FC'
_tabla_relacion_campos = r'MHQ_SIS_RELACION_CAMPOS'
_tabla_dominios = r'MHQ_SIS_DOMAINS'
_nombre_fc_prod = r'BASE_EXCEL_LAB_FC'
_cvs_temporal = os.path.join(_scratch, r'datos_temp.csv')

campo_dominio = list
lista_campos = list
lista_dominios = list
# creamos el diccionario de equivalencias
dic_equiv = dict()
bd_relacion_campos = list
bd_tabla_equivalencias = list
tabla_equivalencias = _tabla_relacion_campos

def validamos_excel_valores_unicos(excel_file):
    # campos
    cod_cuen = "Cod_Cuen"
    cuenca = "Cuenca"
    cod_subc = "Cod_Subc"
    subcuenca = "Subcuenca"

    # validamos que los valores de las columnas sean unicos
    df = pd.read_excel(excel_file)
    dfcuencas = df.groupby([cod_cuen, cuenca]).size().reset_index(name='counts')
    dfsubcuencas = df.groupby([cod_subc, subcuenca]).size().reset_index(name='counts')

    df_n = dfcuencas.drop_duplicates(subset=[cod_cuen]).drop_duplicates(subset=[cuenca])
    df_n2 = dfsubcuencas.drop_duplicates(subset=[cod_subc]).drop_duplicates(subset=[subcuenca])
    if df_n.shape[0] < dfcuencas.shape[0]:
        raise RuntimeError(msg._ERROR_CUENCA_DUPLICADA)
    elif  df_n2.shape[0] < dfsubcuencas.shape[0]:
        raise RuntimeError(msg._ERROR_SUBCUENCA_DUPLICADA)
    else:
        return True

def obtenemos_variables_globales():
    global campo_dominio
    global lista_campos
    global lista_dominios
    global dic_equiv
    global bd_relacion_campos
    global bd_tabla_equivalencias

    bd_relacion_campos = pkg.get_relacion_campos()
    bd_tabla_equivalencias = pkg.get_tabla_equivalencias()
    # campo_dominio = [[x[0], x[3]] for x in
    #                 arcpy.da.SearchCursor(_tabla_relacion_campos, ["campo_fc", "tipo", "largo", "dominio"])]
                
    campo_dominio = [[x[0], x[3]] for x in bd_relacion_campos ]
    lista_campos = [x[0] for x in campo_dominio]
    lista_dominios = [x[1] for x in campo_dominio]

    # with arcpy.da.SearchCursor(_tabla_relacion_campos, ['campo_lab_2', 'campo_fc']) as cursor:
    #     for row in cursor:
    #         dic_equiv[row[0]] = row[1]
    for row in bd_tabla_equivalencias:
        dic_equiv[row[0]] = row[1]


# definimos funciones
def crear_tablas(gdb):
    if not arcpy.Exists(_nombre_tabla):
        arcpy.CreateTable_management(_scratchgdb, _nombre_tabla)

    if not arcpy.Exists(_nombre_tabla_corregida):
        arcpy.CreateTable_management(_scratchgdb, _nombre_tabla_corregida)

    if not arcpy.Exists(_nombre_fc):
        # creamos feature class para pasar los valores de tabla base, creando coordenadas y respetando dominios
        arcpy.CreateFeatureclass_management(_scratchgdb, _nombre_fc, "POINT", spatial_reference=4326)
    
    arcpy.env.workspace = gdb

    if not arcpy.Exists(_nombre_fc_prod):
        # creamos feature class para pasar los valores de tabla base, creando coordenadas y respetando dominios
        arcpy.CreateFeatureclass_management(gdb, _nombre_fc_prod, "POINT", spatial_reference=4326)



# Agregamos los campos a cada una de las tablas
def agregarcampos(gdb, capa):
    arcpy.env.workspace = gdb
    try:
        # with arcpy.da.SearchCursor(_tabla_relacion_campos, ["campo_fc", "tipo", "largo", "dominio"]) as cursor:
        cursor = bd_relacion_campos
        for row in cursor:
            if 'TABLA' in capa:
                largo = row[2]
                if row[3]:
                    largo = 250
                arcpy.AddField_management(in_table=capa, field_name=row[0], field_type=row[1], field_length=largo)
            else:
                arcpy.AddField_management(in_table=capa, field_name=row[0], field_type=row[1], field_length=row[2],
                                            field_domain=row[3])
    except:
        print("ya existen los campos para el objeto {0}".format(capa))


def crear_capas_auxiliares(gdb):
    arcpy.env.workspace = _scratchgdb
    crear_tablas(gdb)
    agregarcampos(_scratchgdb ,_nombre_tabla)
    agregarcampos(_scratchgdb,_nombre_fc)
    agregarcampos(_scratchgdb, _nombre_tabla_corregida)
    agregarcampos(gdb, _nombre_fc_prod)



### funciones para ingreso de datos de laboratorio
def getequivalentfc(column_name):
    response = column_name
    if dic_equiv.get(column_name):
        response = dic_equiv.get(column_name)
    return response

def sum_equiv(a,b):
    suma = a+b
    resta = a-b
    return (resta/suma)*100

def get_max_ion(a,b,c, ion):
    lista_valores = [a,b,c]    
    max_value = max(lista_valores)
    idx = lista_valores.index(max_value)
    texto = msg._IONES[ion][idx]
    return texto

def str_to_num(val):
	try:
		if '.'in val:
			num = float(val)
		else:
			num = int(val)
	except:
		num = val
	return num

# @msg.addTimeMessage
def preparar_datos_csv(excel_ingreso,salida=None):
    xls = excel_ingreso
    # df=pd.read_excel(xls,'Avenida')
    x = pd.ExcelFile(xls)
    sheets = x.sheet_names
    sheets.sort()

    df = pd.read_excel(xls, sheets[0])
    columnas_xls = df.columns
    columnas_xls = [x.strip() for x in columnas_xls]
    df.columns = columnas_xls

    # strip para todos los valores dentro del xls
    df=df.applymap(lambda x: x.strip() if isinstance(x, (str, unicode)) else x)

    # eliminamos todos los na
    df = df.dropna(subset = [msg._CODIGO_MHQ])


    # Actualizamos el codigo con el sufijo de temporada
    df[msg._CODIGO_MHQ] = df[msg._CODIGO_MHQ] + "_" + df[msg._TEMPORADA].str[0]

    # Calculamos valores para cationes
    df["Ca_meq/l"] = df["Ca_dis"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["Ca_meq/l"] = df["Ca_meq/l"].apply(lambda x: float(x)*2/40 if x != "-" else "-")

    df["Mg_meq/l"] = df["Mg_dis"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["Mg_meq/l"] = df["Mg_meq/l"].apply(lambda x: float(x)*2/24.3 if x != "-" else "-")

    df["Na_meq/l"] = df["Na_dis"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["Na_meq/l"] = df["Na_meq/l"].apply(lambda x: float(x)/23 if x != "-" else "-")

    df["K_meq/l"] = df["K_dis"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["K_meq/l"] = df["K_meq/l"].apply(lambda x: float(x)/39.1 if x != "-" else "-")

    df["Suma_meq/l_cat"] = df["Ca_meq/l"] + df["Mg_meq/l"] + df["Na_meq/l"] + df["K_meq/l"]
    df["Suma_meq/l_cat"] = df["Suma_meq/l_cat"].apply(lambda x: x if type(x) in (int,float) else '-' )

    # Calculamos valores para aniones
    df["HCO3_meq/l"] = df["HCO3-"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["HCO3_meq/l"] = df["HCO3_meq/l"].apply(lambda x: float(x)/61 if x != "-" else "-")

    df["CO3_meq/l"] = df["CO3= (mg/L)"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["CO3_meq/l"] = df["CO3_meq/l"].apply(lambda x: float(x)*2/60 if x != "-" else "-")

    df["SO4_meq/l"] = df["SO4="].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["SO4_meq/l"] = df["SO4_meq/l"].apply(lambda x: float(x)*2/96 if x != "-" else "-")

    df["Cl_meq/l"] = df["Cl-"].apply(lambda x: x if str(x)[0] != "<" else 0)
    df["Cl_meq/l"] = df["Cl_meq/l"].apply(lambda x: float(x)/35.45 if x != "-" else "-")

    df["Suma_meq/l_ani"] = df["HCO3_meq/l"] + df["CO3_meq/l"] + df["SO4_meq/l"] + df["Cl_meq/l"]
    df["Suma_meq/l_ani"] = df["Suma_meq/l_ani"].apply(lambda x: x if type(x) in (int,float) else '-' )

    # Calculos
    df["BI_%"] = df.apply(lambda x: sum_equiv(x["Suma_meq/l_cat"], x["Suma_meq/l_ani"]) if x["Suma_meq/l_cat"]!='-' else '-', axis=1)
    # Resumen cationes
    df["Ca_%"] = df.apply(lambda x: 100 * x["Ca_meq/l"]/x["Suma_meq/l_cat"] if x["Suma_meq/l_cat"]!='-' else '-', axis=1)
    df["Mg_%"] = df.apply(lambda x: 100 * x["Mg_meq/l"]/x["Suma_meq/l_cat"] if x["Suma_meq/l_cat"]!='-' else '-', axis=1)
    df["Na+K_%"] = df.apply(lambda x: 100 * (x["Na_meq/l"] + x["K_meq/l"])/x["Suma_meq/l_cat"] if x["Suma_meq/l_cat"]!='-' else '-', axis=1)

    # Resumen aniones
    df["HCO3+CO3_%"] = df.apply(lambda x: 100 * (x["HCO3_meq/l"] + x["CO3_meq/l"])/x["Suma_meq/l_ani"] if x["Suma_meq/l_ani"]!='-' else '-', axis=1)
    df["SO4_%"] = df.apply(lambda x: 100 * x["SO4_meq/l"]/x["Suma_meq/l_ani"] if x["Suma_meq/l_ani"]!='-' else '-', axis=1)
    df["Cl_%"] = df.apply(lambda x: 100 * x["Cl_meq/l"] /x["Suma_meq/l_ani"] if x["Suma_meq/l_ani"]!='-' else '-', axis=1)

    # Calculo de facie
    df["HIDROTIPO"] = df.apply(lambda x: get_max_ion(x["HCO3+CO3_%"], x["SO4_%"], x["Cl_%"], 'anion') + " "+ get_max_ion(x["Ca_%"], x["Mg_%"], x["Na+K_%"], 'cation') if x["Cl_%"]!='-' else '-', axis=1 )
    
    dataframe =df.copy()
    if not salida:
        return dataframe
    else:
        dataframe.to_csv(salida, index=False, encoding='utf-8-sig', sep =';', escapechar="\n")
        return salida

def datos_a_temporal(dataframe):
    # Eliminamos espacios vacios de las cabeceras
    columnas_xls = df.columns
    columnas_xls = [x.strip() for x in columnas_xls]
    df.columns = columnas_xls

    # reemplazamos los nombres de cabeceras por el equivalente en el fc
    columnas_xls = dataframe.columns
    columnasfc = [getequivalentfc(x) for x in columnas_xls]
    dataframe.columns = columnasfc
    # Reemplazamos los guiones por vacios
    dataframe = dataframe.replace('-', '')

    dataframe.to_csv(_cvs_temporal, index=False, encoding='utf-8-sig', sep =';', escapechar="\n")




####
def limpiar_tablas():
    arcpy.env.workspace = _scratchgdb
    arcpy.TruncateTable_management(_nombre_tabla)
    arcpy.TruncateTable_management(_nombre_tabla_corregida)
    arcpy.TruncateTable_management(_nombre_fc)


def csvtemp_to_tabla_base(csv, target):
    arcpy.Append_management(csv, target, "NO_TEST")


###
### funciones para ingresar datos del csv a la gdb
def getvalues_dominio(nom_dominio):
    diccionario_dominio = dict()
    # with arcpy.da.SearchCursor(_tabla_dominios, ["DOMINIO", "KEY", "VALUE"],
    #                            where_clause="DOMINIO ='{}'".format(nom_dominio)) as cursor:
    #     for row in cursor:
    #         diccionario_dominio[row[2]] = row[1]
    
    cursor = pkg.get_dic_dominio(nom_dominio)
    for row in cursor:
        diccionario_dominio[row[2]] = row[1]
    del cursor
    return diccionario_dominio


def get_valor_equiv(valor, diccionario):
    # obtiene el valor equivalente de dominio

    coincidencia = diff.get_close_matches(valor, [key for key in diccionario], n=1, cutoff=0.6)
    coincidencia = coincidencia[0] if len(coincidencia) > 0 else None
    return diccionario.get(coincidencia)


def creargeometria(coor_x, coor_y, zona):
    zona = int(zona)
    sr_wgs = arcpy.SpatialReference(4326)
    references = {17: 32717, 18: 32718, 19: 32719}
    reference = references[zona]

    point = arcpy.Point()
    point.X = coor_x
    point.Y = coor_y
    srf = arcpy.SpatialReference(reference)

    pointGeometry = arcpy.PointGeometry(point, srf)
    pointGeometry_wgs = pointGeometry.projectAs(sr_wgs)
    return pointGeometry_wgs


def insertar_fc(capa):
    # insertamos registros de la tabla base a la tabla corregida y al fc
    capa_intermedia = os.path.join(_scratchgdb, capa)
    icursor = arcpy.da.InsertCursor(capa_intermedia, lista_campos)

    with arcpy.da.SearchCursor(_nombre_tabla, lista_campos) as cursor:
        for row in cursor:
            fila = []
            for columna in range(len(row)):
                dominio_nombre = lista_dominios[columna]

                if dominio_nombre:

                    dic_dom = getvalues_dominio(dominio_nombre)

                    if row[columna]:
                        valor = row[columna].lower()

                        if valor == u'sí':
                            valor = 'si'
                        valor_equiv = get_valor_equiv(valor, dic_dom)

                        if valor_equiv:
                            valor_columna = valor_equiv
                        else:
                            valor_columna = None
                    else:
                        valor_columna = None

                else:
                    valor_columna = str_to_num(row[columna])
                del dominio_nombre
                fila.append(valor_columna)
            fila_as_tupla = tuple(fila)
            # print fila
            icursor.insertRow(fila_as_tupla)

    del icursor


def act_geometria(fc):
    path = os.path.join(_scratchgdb, fc)
    with arcpy.da.UpdateCursor(path, ["ESTE", "NORTE", "ZONA", "SHAPE@", "LONGITUD", "LATITUD"]) as ucursor:
        for row in ucursor:
            geometria = creargeometria(row[0], row[1], row[2])
            row[3] = geometria
            row[4] = geometria.centroid.Y
            row[5] = geometria.centroid.X
            ucursor.updateRow(row)

def act_campo_microcuenca(fc):
    path = os.path.join(gdb_base, fc)
    sql = '''{} =\'{}\''''
    lista_subcuencas = [x[0] for x in arcpy.da.SearchCursor(fc,["COD_SUBC"])]
    lista_subcuencas = list(set(lista_subcuencas))
    lista_campos = ["COD_SUBC","MICROCU","COD_MICROC"]
    field_subc = "COD_SUBC"

    for subcuenca in lista_subcuencas:
        sql_subc = sql.format(field_subc, subcuenca)
        with arcpy.da.UpdateCursor(path, lista_campos, sql_subc,sql_clause=(None,"ORDER BY COD_SUBC ASC, MICROCU ASC")) as cursoru:
            cont = 0
            microc_ac = ""
            for row in cursoru:
                if row[1] !=microc_ac:
                    microc_ac = row[1]
                    cont+=1  

                row[2] = str(row[0])+'_'+str(cont).zfill(2)
                cursoru.updateRow(row)

    pass
def insertar_de_base_a_capas_intermedias():
    insertar_fc(_nombre_fc)
    insertar_fc(_nombre_tabla_corregida)
    act_geometria(_nombre_fc)
    # act_campo_microcuenca(_nombre_fc)
    # act_campo_microcuenca(_nombre_tabla_corregida)

def limpieza_nuevos_datos(capa, codes):
    # Codigo inicial usando cursores licencia advanced
    # num_registros = int(arcpy.GetCount_management(capa)[0])
    # if num_registros>0:
    #     with arcpy.da.UpdateCursor(capa, ["CODIGO"]) as cursoru:
    #         for row in cursoru:
    #             if row[0] in codes:
    #                 cursoru.deleteRow()
    
    # Codigo pruebas para licencia basic
    query_values = "('"+"','".join(codes)+"')"
    query = "CODIGO IN "+query_values

    if capa.startswith("TB"):
        lyr = arcpy.MakeTableView_management(capa, capa+"_lyr", query)[0]
    else:
        lyr = arcpy.MakeFeatureLayer_management(capa, capa+"_lyr", query)[0]
    
    num_registros = int(arcpy.GetCount_management(lyr)[0])
    if num_registros>0: 
        arcpy.DeleteRows_management(lyr)
    
    # arcpy.Append_management(capain, capafin, "NO_TEST")

def insertar_capas_produccion(gdb):
    arcpy.env.workspace = gdb
    # edit = arcpy.da.Editor(gdb)
    # edit.startEditing(False, True)
    # edit.startOperation()

    capaingreso = os.path.join(_scratchgdb, _nombre_fc)
    tablaingreso = os.path.join(_scratchgdb, _nombre_tabla_corregida)

    codes = [x[0] for x in arcpy.da.SearchCursor(capaingreso,["CODIGO"])]
    limpieza_nuevos_datos( r'TB_ANIONS', codes)
    limpieza_nuevos_datos( r'TB_ANIONS_MEQ_L', codes)
    limpieza_nuevos_datos( r'TB_ANIONS_MEQ_L_PORC', codes)
    limpieza_nuevos_datos( r'TB_ASPECT_GEOL_LIT', codes)
    limpieza_nuevos_datos( r'TB_CAT_MEQ_L', codes)
    limpieza_nuevos_datos( r'TB_CAT_MEQ_L_PORC', codes)
    # limpieza_nuevos_datos( r'TB_CAT_TOT', codes)
    limpieza_nuevos_datos( r'TB_INDICES_CALC', codes)
    limpieza_nuevos_datos( r'TB_LOCAL', codes)
    limpieza_nuevos_datos( r'TB_PARAM_FISQUIM', codes)
    limpieza_nuevos_datos( r'TB_USO_FUENTE', codes)
    limpieza_nuevos_datos( r'DS_LINEABASEGEOAMBIENTAL\GPT_LINEA_BASE_GEOAMBIENTAL_HIDROQUIMICA', codes)

    limpieza_nuevos_datos( r'DS_LINEABASEGEOAMBIENTAL\GPT_LINEA_BASE_GEOAMBIENTAL', codes)
    limpieza_nuevos_datos( _nombre_fc_prod, codes)


    # edit.stopOperation()
    # edit.stopEditing(True)

    arcpy.Append_management(tablaingreso, r'TB_ANIONS', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_ANIONS_MEQ_L', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_ANIONS_MEQ_L_PORC', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_ASPECT_GEOL_LIT', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_CAT_MEQ_L', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_CAT_MEQ_L_PORC', "NO_TEST")
    # arcpy.Append_management(tablaingreso, r'TB_CAT_TOT', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_INDICES_CALC', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_LOCAL', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_PARAM_FISQUIM', "NO_TEST")
    arcpy.Append_management(tablaingreso, r'TB_USO_FUENTE', "NO_TEST")
    arcpy.Append_management(capaingreso, r'DS_LINEABASEGEOAMBIENTAL\GPT_LINEA_BASE_GEOAMBIENTAL_HIDROQUIMICA', "NO_TEST")
    arcpy.Append_management(capaingreso, r'DS_LINEABASEGEOAMBIENTAL\GPT_LINEA_BASE_GEOAMBIENTAL', "NO_TEST")
    arcpy.Append_management(capaingreso, _nombre_fc_prod, "NO_TEST")



if __name__ == '__main__':
    try:
        obtenemos_variables_globales()
        validamos_excel_valores_unicos(xls_file)
        crear_capas_auxiliares(gdb_base)
        df = preparar_datos_csv(xls_file)
        datos_a_temporal(df)
        limpiar_tablas()
        csvtemp_to_tabla_base(_cvs_temporal, _nombre_tabla)
        insertar_de_base_a_capas_intermedias()
        insertar_capas_produccion(gdb_base)
        act_campo_microcuenca(_nombre_fc_prod)
        response["response"] = "response"
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(2, response)
