import arcpy
import json
import os
import traceback

# path to log %AppData%\Roaming\Esri\ArcGISPro\ArcToolbox\History.

SCRIPTS_PATH = os.path.dirname(__file__)
arcpy.SetLogHistory(True)

arcpy.env.overwriteOutput = True

in_layer = arcpy.GetParameterAsText(0)
in_codigo = arcpy.GetParameterAsText(1)
in_datum = arcpy.GetParameterAsText(2).zfill(2)
in_zona = arcpy.GetParameterAsText(3)
temp_folder = r"c:/bdgeocatmin/temporal"

out_geom= object()
response = None
# Anterior o Prioritario
# _ANTERIOR = "AN"
_ANTERIOR = 'PR'

_POSTERIOR = 'PO'
_EXTINGUIDO = 'EX'
_EVALUADO = 'EV'
_SIMULTANEO = 'SI'
_REDENUNCIO = 'RD'
SDE_ = os.path.join(SCRIPTS_PATH, 'data_cat.sde')

listado_objs_evaluacion = []



def act_geom_info(lyrpath, codigo):
    """
    Actualiza informacion de la capa temporal de cmi, a partir de sus propios datos
    """
    # campos_agregar = [["CONTADOR", "SHORT", "#"], ["PRIORI", "TEXT", 2], ["AREAINT", "DOUBLE", "#"],["TOTALSINO", "TEXT", 2]]
    campos_agregar = [["CONTADOR", "SHORT", "#"], ["TOTALSINO", "TEXT", 2]]

    for campo in campos_agregar:
        try:
            arcpy.AddField_management(lyrpath, campo[0], campo[1], "#", "#", campo[2])
        except:
            pass
    
    campos_dm_consultado = ["shape@", "FEC_DENU", "HOR_DENU", "ESTADO", "D_ESTADO", "IDENTI", "CONCESION", "DATUM", "TIPO_EX" ]
    query = "CODIGOU = '{}'".format(codigo)
    valores = [x for x in arcpy.da.SearchCursor(lyrpath, campos_dm_consultado, query)][0]
    geom_dm = valores[0]
    fecdenu_dm = valores[1]
    hordenu_dm = valores[2]
    estado_dm = valores[3]
    vestado_dm = valores[4]
    identi_dm = valores[5]
    conce_dm = valores[6]
    datum_dm = valores[7]
    tipo_ex_dm = valores[8]

    oid_fieldname = arcpy.Describe(lyrpath).OIDFieldName
    campos = [oid_fieldname, "CODIGOU", "SHAPE@", "CONTADOR", "EVAL", "AREA_INT", "FEC_DENU", "HOR_DENU", "ESTADO", "D_ESTADO", "CONCESION", "TIPO_EX", "IDENTI", "DE_IDEN", "FEC_LIB", "DATUM", "SITUACION", "TOTALSINO" ]
    query = "CODIGOU <> '{}'".format(codigo)
    query = "1=1"
    with arcpy.da.UpdateCursor(lyrpath, campos, query) as cursor:
        for i in cursor:
            # definimos variables para iterar
            codigo_x = i[1]
            geom_x = i[2]
            fec_denu_x = i[6]
            hor_denu_x = i[7]
            estado_x = i[8]
            vestado_x = i[9]
            concesion_x = i[10]
            tipo_ex_x = i[11]
            identi_x = i[12]
            de_iden_x = i[13] # incorpor
            fec_lib_x = i[14]
            datum_x = i[15]
            situex_x = i[16]
            #### Variables a calcular segun criterios
            # i[4] PRIORI
            # i[5] AREAINT
            # i[3] CONTADOR
            priori = "i4"
            area = 0
            totalsino =''


            i[3] = i[0]+1


            # Los registros que no intersectan ni tocan el dm consultado
            if geom_x.disjoint(geom_dm):
                priori = "VE"
            # Los registros que intersectan el dm consultado
            else:
                iscgeom = geom_dm.intersect(geom_x, 4)
                area = iscgeom.area/10000.0

                # Determina si la interseccion es total
                if round(iscgeom.area,4) == round(geom_dm.area,4):
                    totalsino = 'SI'
                # Colindantes
                if area < 0.001:
                    priori = "CO"
                else:
                    # Derechos que intersectan con el derecho consultado
                    i[5] = round(area,4)
                    # Comenzamos a evaluar 

                    # Si el estado es P
                    if estado_dm == 'P':                        
                        ## Evaluacion por datum
                        # 1er caso, dm es psad56 y dato iterado es wgs84(mas reciente)
                        if int(datum_dm) < int(datum_x):
                            if situex_x =='V' :
                                priori = _POSTERIOR 
                            else :
                                priori = _EXTINGUIDO
                            
                        # 2do caso, dm es wgs84 y dato iterado es psad5(mas antiguo)
                        elif int(datum_dm) > int(datum_x):
                            if situex_x =='V' :
                                priori = _ANTERIOR 
                            else :
                                priori = _EXTINGUIDO
                        
                        # los datums son iguales
                        else:
                            
                            if estado_x == 'D':
                                if tipo_ex_dm == 'PE':
                                    priori = _ANTERIOR
                                elif tipo_ex_dm == 'RD':
                                    priori = _POSTERIOR
                            
                            elif estado_x == 'F':
                                if tipo_ex_dm == 'PE':
                                    priori = _ANTERIOR
                                elif tipo_ex_dm == 'RD':
                                    if fecdenu_dm < fec_lib_x:
                                        priori = _ANTERIOR
                                    else:
                                        priori = _EXTINGUIDO

                            elif estado_x == 'X':
                                if tipo_ex_x in ('PE', 'RD'):
                                    priori = _EXTINGUIDO

                            elif estado_x == 'Y':
                                if fecdenu_dm < fec_lib_x:
                                    priori = _ANTERIOR
                                else:
                                    priori = _EXTINGUIDO
                            
                            elif estado_x in ('L', 'J', 'H'):
                                if tipo_ex_dm == 'RD':
                                    if tipo_ex_x == 'PE':
                                        priori = _EXTINGUIDO 
                                    else:
                                        priori = _ANTERIOR
                                elif tipo_ex_dm == 'PE':
                                    priori = _ANTERIOR
                            
                            elif estado_x in ('B', 'M', 'G', 'A', 'S', 'R'):
                                priori = _POSTERIOR
                            
                            else:
                                # Caso 1
                                # Verificando D.M. Evaluado vs sistema de cuadriculas y Sistema antiguo (Redenuncios)
                                if identi_dm == identi_x:
                                    if estado_x != 'P':
                                        if tipo_ex_dm == 'PE':
                                            if estado_x in ('K', 'Q', 'C', 'N', 'E', 'T'):
                                                if tipo_ex_x in ('DN', 'AC', 'PE'):
                                                    priori = _ANTERIOR
                                            
                                            if estado_x == 'X':
                                                if de_iden_x == 'I':
                                                    if tipo_ex_x in ('DN', 'AC'):
                                                        priori = _ANTERIOR
                                        
                                        elif tipo_ex_dm == 'RD':
                                            if estado_x in ('T', 'X', 'C'):
                                                if tipo_ex_x == "RD":
                                                    priori = _ANTERIOR
                                    
                                    elif estado_x == 'P':
                                        if fecdenu_dm != '' and fec_denu_x != '':
                                            if fec_denu_x < fecdenu_dm:
                                                priori = _ANTERIOR
                                            elif fec_denu_x > fecdenu_dm:
                                                priori = _POSTERIOR
                                            elif fec_denu_x == fecdenu_dm:
                                                if hor_denu_x < hordenu_dm:
                                                    priori = _ANTERIOR
                                                elif hor_denu_x > hordenu_dm:
                                                    priori = _POSTERIOR
                                                elif hordenu_dm == hor_denu_x:
                                                    priori = _SIMULTANEO
                                        else:
                                            if float(vestado_x) < 2:
                                                priori = _ANTERIOR
                                
                                # Caso 2
                                # Verificando D.M. (Redenuncio) Vs Sistemas de Cuadriculas
                                if identi_dm == "01-10" and identi_x != "01-10":
                                    if estado_x in ('Q', 'C', 'N', 'E'):
                                        if tipo_ex_x in ('DN', 'AC', 'PE'):
                                            priori = _ANTERIOR
                                    if estado_x  == 'X':
                                        if de_iden_x == 'I':
                                            if tipo_ex_x in ('DN', 'AC'):
                                                priori = _ANTERIOR
                                    
                                    if estado_x in ('P', 'T', 'K'):
                                        priori = _POSTERIOR
                                
                                # Caso 3
                                # Evaluando DM Evaluado Petitorio vs redenuncio
                                if identi_dm != "01-10" and identi_x == "01-10":
                                    if estado_x in ('P', 'T', 'C', 'X'):
                                        if tipo_ex_x == 'RD':
                                            priori = _ANTERIOR
                    
                    # Verificando si el D.M. Evaluado es diferente de petitorio
                    # estado_dm !=P
                    else: 
                        # 1er caso, dm es psad56 y dato iterado es wgs84(mas reciente)
                        if int(datum_dm) < int(datum_x):
                            if situex_x =='V' :
                                priori = _POSTERIOR 
                            else :
                                priori = _EXTINGUIDO
                            
                        # 2do caso, dm es wgs84 y dato iterado es psad5(mas antiguo)
                        elif int(datum_dm) > int(datum_x):
                            if situex_x =='V' :
                                priori = _ANTERIOR 
                            else :
                                priori = _EXTINGUIDO
                        
                        # los datums son iguales
                        else:
                            # Caso 1
                            if identi_dm == '01-10' and identi_x != "01-10":
                                if fecdenu_dm != '' and fec_denu_x != '':
                                    if fec_denu_x < fecdenu_dm:
                                        if estado_x in ('E', 'N'):
                                            priori = _ANTERIOR
                                        else:
                                            priori = _POSTERIOR
                                    
                                    elif fec_denu_x > fecdenu_dm:
                                        priori = _POSTERIOR
                                    
                                    elif fecdenu_dm == fec_denu_x:
                                        if hor_denu_x < hordenu_dm:
                                            priori = _ANTERIOR
                                        elif hor_denu_x > hordenu_dm:
                                            priori = _POSTERIOR
                                        elif hor_denu_x == hordenu_dm:
                                            priori = _SIMULTANEO
                                else:
                                    if float(vestado_x) < 2:
                                        priori = _ANTERIOR
                                    else:
                                        priori = _POSTERIOR
                            
                            # Caso 2 
                            if identi_dm != '01-10' and identi_x == "01-10":
                                if fecdenu_dm != '':
                                    if fec_denu_x != '':
                                        if fec_denu_x < fecdenu_dm:
                                            priori = _ANTERIOR
                                        
                                        elif fec_denu_x > fecdenu_dm:
                                            if estado_dm in ('E', 'N'):
                                                priori = _POSTERIOR
                                            else:
                                                priori = _ANTERIOR
                                        
                                        elif fecdenu_dm == fec_denu_x:
                                            if hor_denu_x < hordenu_dm:
                                                priori = _ANTERIOR
                                            elif hor_denu_x > hordenu_dm:
                                                priori = _POSTERIOR
                                            elif hor_denu_x == hordenu_dm:
                                                priori = _SIMULTANEO
                                    else:
                                        if float(vestado_dm) < 2:
                                            priori = _ANTERIOR
                                        else:
                                            priori = _POSTERIOR
                                else:
                                    if float(vestado_dm) < 2:
                                        priori = _POSTERIOR
                                    else:
                                        priori = _ANTERIOR
                            
                            # Caso 3 y 4
                            if identi_dm == identi_x:
                                if fecdenu_dm != '' and fec_denu_x != '':
                                    if fec_denu_x < fecdenu_dm:
                                        priori = _ANTERIOR
                                    elif fec_denu_x > fecdenu_dm:
                                        priori = _POSTERIOR
                                    elif fec_denu_x == fecdenu_dm:
                                        if hor_denu_x < hordenu_dm:
                                            priori = _ANTERIOR
                                        elif hor_denu_x > hordenu_dm:
                                            priori = _POSTERIOR
                                        elif hor_denu_x == hordenu_dm:
                                            priori = _SIMULTANEO
                                else:
                                    if float(vestado_x) < float(vestado_dm):
                                        priori = _ANTERIOR
                                    elif float(vestado_x) > float(vestado_dm):
                                        priori = _POSTERIOR
                                    elif float(vestado_x) == float(vestado_dm):
                                        corre1 = codigo[2:8]
                                        if not corre1.isdigit():
                                            corre1 = codigo[2:7]
                                        corre2 = codigo_x[2:8]
                                        if not corre2.isdigt():
                                            corre2 = codigo[2:7]
                                        letra_dm = codigo[8]
                                        letra_x = codigo_x[8]
                                        
                                        if (letra_dm == 'X' and letra_x == 'Y') or (letra_dm == 'Z' and letra_x == 'Y') or (letra_dm == 'Z' and letra_x == 'X'):
                                            priori = _POSTERIOR
                                        elif (letra_dm == 'Y' and letra_x == 'X') or (letra_dm == 'Y' and letra_x == 'Z') or (letra_dm == 'X' and letra_x == 'Z'):
                                            priori = _ANTERIOR
                                        elif letra_dm == letra_x :
                                            if corre1 < corre2:
                                                priori = _ANTERIOR
                                            else:
                                                priori = _POSTERIOR
            if codigo == codigo_x:
                priori = _EVALUADO
            if estado_x == "F":
                priori = _REDENUNCIO
            i[4] = priori
            i[17] = totalsino            
            cursor.updateRow(i)
    return geom_dm


def eval_capasvsdm( shapegeom, codigo, datum, zona):
    """
    Evalua el codigo DM y su relacion con las capas de area rese, urba, cata forestal para registrar en la tabla evaltecnica
    """
    # Define los sufijos para consultar los feature class apuntando a su datum tyzona correspondiente
    datumname = '_WGS' if datum =='02' else ''
    sufijo = "{0}_{1}".format(datumname, zona)
    capa_rese = 'DATA_GIS.GPO_ARE_AREA_RESERVADA{}'.format(sufijo)
    capa_urba = 'DATA_GIS.GPO_ZUR_ZONA_URBANA{}'.format(sufijo)

    datumname = 'W' if datum =='02' else 'P'
    sufijo = "_{0}{1}".format(datumname, zona)
    # capa_fore = 'DATA_GIS.GPO_CFO_CATASTRO_FORESTAL{}'.format(sufijo)
    capa_fore = 'DATA_GIS.GPO_SERFOR_CONCESIONES{}'.format(sufijo)

    sentencia = "select * from {}"

    # Comparando con area reservada
    arcpy.AddWarning(SDE_)
    mfl_caparese = arcpy.MakeQueryLayer_management(SDE_,'lyrx',sentencia.format(capa_rese), "OBJECTID" ).getOutput(0)
    arcpy.SelectLayerByLocation_management(mfl_caparese, "INTERSECT", shapegeom, "", "NEW_SELECTION")
    contador_rese =0
    last_codigo_rese=0
    with arcpy.da.SearchCursor(mfl_caparese,["shape@", "NM_RESE", "CODIGO", "CLASE"]) as cursor:
        
        for row in cursor:
            geom_rese = row[0]
            nm_rese = row[1]
            codigo_rese = row[2]
            clase_rese = row[3]
            geom_isc = object()
            area_geom = round(shapegeom.area/10000.0, 4)
            area_isc = 0
            variable = nm_rese
            cod_enum = codigo_rese
            print(nm_rese)
            if not codigo_rese.startswith('RV'):
                geom_isc = geom_rese.intersect(shapegeom, 4)
                area_isc = round(geom_isc.area/10000.0, 4)

            if nm_rese.startswith('PMA'):
                variable = codigo_rese + nm_rese[3:] 
            
            variable_ld = variable
            if area_geom == area_isc:
                " Indica que la interseccion es total"
                variable_ld = "TOTAL "+ variable_ld 

            if codigo_rese == last_codigo_rese:
                contador_rese += 1
                cod_enum = codigo_rese+"_"+ str(contador_rese)
            else:
                contador_rese = 0
            last_codigo_rese = codigo_rese
            
            obj = {"codigoDM": codigo,
                    "codigoU": cod_enum,
                    "eval": "AP",
                    "hectarea": area_isc,
                    "concesion": variable_ld,
                    "clase" : clase_rese,
                    "tipoEx" : "",
                    "estado" : "",
                    "contador": ""}
            listado_objs_evaluacion.append(obj) 
            
    
    del mfl_caparese

    # Comparando con zona urbana
    mfl_capaurba = arcpy.MakeQueryLayer_management(SDE_,'lyrx',sentencia.format(capa_urba), "OBJECTID" ).getOutput(0)
    arcpy.SelectLayerByLocation_management(mfl_capaurba, "INTERSECT", shapegeom, "", "NEW_SELECTION")

    with arcpy.da.SearchCursor(mfl_capaurba,["shape@", "NM_URBA", "CODIGO"]) as cursor:
        for row in cursor:
            geom_urba = row[0]
            nm_urba = row[1]
            codigo_urba = row[2]
            geom_isc = geom_urba.intersect(shapegeom, 4)
            area_geom = round(shapegeom.area/10000.0, 4)
            area_isc = round(geom_isc.area/10000.0, 4)
            variable = nm_urba
            variable_ld =  variable
            if area_geom == area_isc:
                " Indica que la interseccion es total"
                variable_ld = "TOTAL "+ variable_ld 
            
            obj = {"codigoDM": codigo,
                    "codigoU": codigo_urba,
                    "eval": "ZU",
                    "hectarea": area_isc,
                    "concesion": variable_ld,
                    "clase" : "",
                    "tipoEx" : "",
                    "estado" : "",
                    "contador": ""}
            listado_objs_evaluacion.append(obj) 
    
    del mfl_capaurba

    # Comparando con capa forestal
    mfl_capafore = arcpy.MakeQueryLayer_management(SDE_,'lyrx',sentencia.format(capa_fore), "OBJECTID" ).getOutput(0)
    arcpy.SelectLayerByLocation_management(mfl_capafore, "INTERSECT", shapegeom, "", "NEW_SELECTION")

    with arcpy.da.SearchCursor(mfl_capafore,["shape@", "CD_CONCE", "TP_CONCE"]) as cursor:
        for row in cursor:
            geom_conce = row[0]
            codigo_conce = row[1]
            tp_conce = row[2]
            geom_isc = geom_conce.intersect(shapegeom, 4)
            area_isc = round(geom_isc.area/10000.0, 4)
            variable = codigo_conce
            
            pkg.p_ins_evaltecnica_ld(None, codigo, codigo_conce, 'SF', _sesion, area_isc, tp_conce, '', 'IT', _user)
            obj = {"codigoDM": codigo,
                    "codigoU": codigo_conce,
                    "eval": "SF",
                    "hectarea": area_isc,
                    "concesion": tp_conce,
                    "clase" : "",
                    "tipoEx" : "",
                    "estado" : "",
                    "contador": ""}
            listado_objs_evaluacion.append(obj) 
    
    del mfl_capafore

def get_criterios(lyrpath, codigo ):
    oid_fieldname = arcpy.Describe(lyrpath).OIDFieldName
    campos = [oid_fieldname, "CODIGOU", "SHAPE@", "CONTADOR", "EVAL",  "CONCESION", "TIPO_EX", "ESTADO" ]
    query = "CODIGOU <> '{}'".format(codigo)
    query = "EVAL IN ('PR', 'RD', 'PO', 'SI', 'EX')"
    with arcpy.da.SearchCursor(lyrpath, campos, query) as cursor:
        for i in cursor:
            obj = {"codigoDM": codigo,
                    "codigoU": i[1],
                    "eval": i[4],
                    "hectarea": "",
                    "concesion": i[5],
                    "clase" : "",
                    "tipoEx" : i[6],
                    "estado" : i[7],
                    "contador": i[3]}
            listado_objs_evaluacion.append(obj)           


def obtener_area_disponible(lyrpath, geom_ini):
    campos = ["shape@", "CODIGOU", "EVAL" ]
    query = "EVAL NOT IN ('VE', 'CO', 'EV')"
    geometria = geom_ini
    codigo_ad ='AD'

    geometria_ad_ld = geom_ini    
    geometria_po_ld = geom_ini
    po_regional = 'INGEMMET'

    area_ld_ad = round((geometria.area /10000.0) ,4)
    with arcpy.da.SearchCursor(lyrpath,campos,query) as cursor:
        for i in cursor:
            if i[2] in ('AN', 'SI'):
                geometria_ad_ld = geometria_ad_ld.difference(i[0])
                codigo_ad = i[1]

    areadisponible_ld = round((geometria_ad_ld.area /10000.0) ,4)

    obj = {"codigoDm": in_codigo,
            "codigoU": in_codigo,
            "eval": "AD",
            "hectarea": areadisponible_ld,
            "concesion": "AREA DISPONIBLE",
            "clase" : "",
            "tipoEx":"",
            "estado": "",
            "contador": ""}
    listado_objs_evaluacion.append(obj)
    return areadisponible_ld

if __name__ == '__main__':
    try:
        lyr_path = os.path.join(temp_folder, in_layer)
        out_geom = act_geom_info(lyr_path, in_codigo)
        get_criterios(lyr_path, in_codigo)
        ad = obtener_area_disponible(lyr_path, out_geom)
        eval_capasvsdm(out_geom, in_codigo, in_datum, in_zona)
        response = listado_objs_evaluacion
        arcpy.AddMessage("Satisfactorio")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        out_geom = None
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(4, response)
        
    