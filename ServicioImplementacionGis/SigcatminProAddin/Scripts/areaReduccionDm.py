# -*- coding: utf-8 -*-
"""
Script en Python que separa polígonos según su atributo 'EVAL',
los intersecta y calcula las áreas intersectadas y no intersectadas.
Utiliza la librería arcpy para ArcGIS Pro.
"""
import arcpy
import json
import os
import tkinter
from tkinter import messagebox
import traceback
import string


# path to log %AppData%\Roaming\Esri\ArcGISPro\ArcToolbox\History.
arcpy.SetLogHistory(True)

arcpy.env.overwriteOutput = True

response = dict()

# campos = ["CODIGOU", "CODIGOU_1", "CODIGO","NM_AREA", "ESTILO", "ETIQUETA"]
campos = ["CODIGOU", "ETIQUETA"]

def mapeo_campos(capas_entrada, nombre_campos_ok):

    # 1. Crear objeto FieldMappings vacío
    field_mappings = arcpy.FieldMappings()

    # 2. Agregar o combinar campos desde cada Feature o capa
    #    (los mismos que vas a usar en el "in_features")
    # Ejemplo: supongamos que tenemos 3 entradas (layer1, layer2, layer3)
    for capa in capas_entrada:
        # Crear un FieldMappings temporal para cada capa
        temp_fm = arcpy.FieldMappings()
        temp_fm.addTable(capa)
        
        # Seleccionar solo algunos campos que interesan
        # o eliminar los que no quieras.

        # Por ejemplo, si solo quiero conservar el campo 'CODIGO' y 'NOMBRE'
        # y descartar el resto, puedo iterar sobre todos los FieldMap y
        # remover los que no coincidan con mis deseados.
        for field_map in temp_fm.fieldMappings:
            nombre_campo = field_map.outputField.name
            if nombre_campo not in nombre_campos_ok:
                temp_fm.removeFieldMap(temp_fm.findFieldMapIndex(nombre_campo))

        # Combinar (addFieldMap) al field_mappings general
        for fm_index in range(temp_fm.fieldCount):
            sub_fm = temp_fm.getFieldMap(fm_index)
            field_mappings.addFieldMap(sub_fm)

    return field_mappings

def generar_etiqueta_indice(idx):
    """
    Genera una etiqueta de tipo A, B, ..., Z, AA, AB, etc.
    a partir de un índice numérico (0, 1, 2...).
    Si no deseas pasar de 'Z', podrías limitarte a 26 y lanzar error.
    """
    letras = string.ascii_uppercase  # 'A'..'Z'
    resultado = ""
    while True:
        # División y módulo para base 26
        idx, r = divmod(idx, 26)
        resultado = letras[r] + resultado
        if idx == 0:
            break
        # Restar 1 para que sea una "base 26" pura
        idx -= 1
    return resultado

def etiquetado_cuadrangulos(in_feature):
    # ----------------------------------------------------------------
    # Parámetros de entrada
    # ----------------------------------------------------------------
    #in_feature Capa de entrada
    campo_etiqueta = "ETIQUETA"  # Nombre del campo donde almacenar letras
    # Si no existe, lo creamos. Si ya existe, sobrescribe su valor.
    
    arcpy.AddMessage("Preparando la capa para asignar etiquetas...")

    # # Comprobar si existe el campo. Si no, crearlo:
    # field_list = [f.name for f in arcpy.ListFields(in_feature)]
    # if campo_etiqueta not in field_list:
    #     arcpy.AddMessage(f"Creando el campo {campo_etiqueta} (TEXT)...")
    #     arcpy.management.AddField(in_feature, campo_etiqueta, "TEXT", field_length=10)

    # ----------------------------------------------------------------
    # Obtener el OID y las coordenadas de centro para cada polígono
    # ----------------------------------------------------------------
    lista_poligonos = []
    with arcpy.da.SearchCursor(in_feature, ["OID@", "SHAPE@XY"]) as cursor:
        for oid, (xcent, ycent) in cursor:
            # Guardar en lista: (oid, x, y)
            lista_poligonos.append((oid, xcent, ycent))

    # ----------------------------------------------------------------
    # Ordenar la lista
    #  En este ejemplo, interpretamos:
    #   - "fila superior" => mayor Y
    #   - "columna más a la izquierda" => menor X
    #  Por lo tanto, orden descendente en Y, ascendente en X
    # ----------------------------------------------------------------
    lista_poligonos.sort(key=lambda x: (-x[2], x[1]))
    # x[2] = ycent => multiplicamos por -1 para orden descendente
    # x[1] = xcent => orden ascendente normal

    # ----------------------------------------------------------------
    # Asignar letras (A, B, C, ... Z, AA, AB, ...)
    # ----------------------------------------------------------------
    # Si son muchos polígonos, usaremos la función generar_etiqueta_indice
    # que pasa de índice numérico a "A, B, ... Z, AA, ..." en base 26.
    dict_oid_etiqueta = {}
    for i, (oid, xcent, ycent) in enumerate(lista_poligonos):
        etiqueta = generar_etiqueta_indice(i)
        dict_oid_etiqueta[oid] = etiqueta

    # ----------------------------------------------------------------
    # Actualizar el campo ETIQUETA con la letra correspondiente
    # ----------------------------------------------------------------
    with arcpy.da.UpdateCursor(in_feature, ["OID@", campo_etiqueta]) as up_cursor:
        for row in up_cursor:
            oid_registro = row[0]
            if oid_registro in dict_oid_etiqueta:
                row[1] = dict_oid_etiqueta[oid_registro]
                up_cursor.updateRow(row)

def calculo_area_a_reducir(
                            capa_entrada,
                            capa_cuadriculas,
                            carpeta_salida,
                            fechaArchi,
                            anadir_al_mapa = 0,
                            ):
    """
    Separa polígonos 'EV' y 'PR' de 'capa_entrada', los intersecta y
    produce tanto la geometría intersectada como la no intersectada.
    Además, calcula las áreas totales de ambas capas y las devuelve
    en un diccionario.

    Parámetros:
    -----------
    capa_entrada : str
        Ruta de la capa (o feature class) de polígonos original.
    carpeta_salida : str
        Carpeta o geodatabase donde se guardarán los resultados.
    nombre_ev_salida : str
        Nombre de la capa de salida que contendrá los polígonos EV.

    Retorno:
    --------
    dict
        {
            "area_intersectada":  <float>,
            "area_no_intersectada": <float>
        }
    """
    # Variables Fijas
    nombre_ev_salida="DM"
    nombre_dm_cua_salida = "DM_Cua_"
    nombre_pr_salida="Prioritarios_"
    nombre_intersect="Area_Inter_Reduc_"
 
    
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    mapa_activo = aprx.activeMap
    # Verificar nombre del mapa
    if mapa_activo.name not in ["CATASTRO MINERO", "CATASTRO MINERO - WGS-84", "CATASTRO MINERO - PSAD-56"]:
        # Crear la ventana raíz de Tk (aunque no se muestre)
        root = tkinter.Tk()
        root.withdraw()  # Oculta la ventana principal
        message="No se encuentra en el Mapa [CATASTRO MINERO]. Se abortará el proceso."
        # Mostrar el cuadro de diálogo
        messagebox.showwarning("Advertencia", message)
        
        # Destruir la ventana raíz para evitar que quede abierta
        root.destroy()

        response["Advertencia"]= message
        return


    # 1. Configurar entorno
    arcpy.env.overwriteOutput = True
    # Opcional: establecer el workspace
    arcpy.env.workspace = carpeta_salida

    # 2. Crear una Feature Layer temporal a partir de la capa de entrada
    capa_cat = "capa_temp"
    capa_dm = "capa_dm"
    capa_caram = "Caram"
    capa_catastro_forestal = "CatastroForestal"
    salida_limitaciones = "Limitaciones"
    codigo_dm = ""
    ev_salida = os.path.join(carpeta_salida, nombre_ev_salida + fechaArchi)
    arcpy.management.MakeFeatureLayer(capa_entrada, capa_cat)
    arcpy.management.MakeFeatureLayer(ev_salida, capa_dm)
    arcpy.management.SelectLayerByLocation(capa_cuadriculas,'INTERSECT', capa_dm, "-1 Meters", 'NEW_SELECTION')
    
    with arcpy.da.SearchCursor(ev_salida,["CODIGOU"]) as cursorDm:
        for i in cursorDm:
            codigo_dm = i[0]
    
    dm_cua_salida = os.path.join(carpeta_salida, nombre_dm_cua_salida + fechaArchi)
    arcpy.management.CopyFeatures(capa_cuadriculas, dm_cua_salida)
    arcpy.management.AddField(dm_cua_salida, "ETIQUETA", "TEXT", field_length = 3)
    arcpy.management.AddField(dm_cua_salida, "RESULTADO", "TEXT", field_length = 25)
    etiquetado_cuadrangulos(dm_cua_salida)
    arcpy.management.RepairGeometry(dm_cua_salida)
    with arcpy.da.UpdateCursor(dm_cua_salida,["CODIGOU"]) as cursorDmCua:
        for i in cursorDmCua:
            i[0] = codigo_dm
            cursorDmCua.updateRow(i)
    # 3. Seleccionar y exportar polígonos "EV"
    capas_a_unir = []

    # 4. Seleccionar y exportar polígonos "PR" - Prioritarios, si existen
    where_pr = "EVAL = 'PR'"
    arcpy.management.SelectLayerByAttribute(capa_cat, "NEW_SELECTION", where_pr)
    pr_salida = os.path.join(carpeta_salida, nombre_pr_salida + fechaArchi)
    count_pr = arcpy.management.GetCount(capa_cat)
    if int(count_pr.getOutput(0)) > 0:
        arcpy.management.CopyFeatures(capa_cat, pr_salida)
        capas_a_unir.append(pr_salida + ".shp")
        campos.append("CODIGOU_1")

    # 5. Limpiar selección
    arcpy.management.SelectLayerByAttribute(capa_cat, "CLEAR_SELECTION")
    arcpy.management.SelectLayerByAttribute(capa_cuadriculas, "CLEAR_SELECTION")

    # Agregamos a la lista de capas a unir Caram, si existen
    ruta_caram = os.path.join(carpeta_salida, capa_caram + fechaArchi + ".shp")
    if os.path.isfile(ruta_caram):
        capas_a_unir.append(ruta_caram)
        campos.append("CODIGO")
        campos.append("NM_AREA")
        campos.append("ESTILO")

    # Agregamos a la lista de capas a unir Catastro Forestal, si existen
    ruta_cat_forestal = os.path.join(carpeta_salida, capa_catastro_forestal + fechaArchi + ".shp")
    if os.path.isfile(ruta_cat_forestal):
        capas_a_unir.append(ruta_cat_forestal)
        campos.append("TP_CONCE")
        campos.append("CD_CONCE")

    #arcpy.management.SelectLayerByLocation("DM_Cua_638725549858548162", "COMPLETELY WITHIN", 'Caram',"", 'NEW_SELECTION')

    salida_limitaciones = os.path.join(carpeta_salida, salida_limitaciones + fechaArchi)
    if len(capas_a_unir)==0:
        return
    arcpy.management.Merge(capas_a_unir, salida_limitaciones)
    # 6. Intersección entre las capas EV y PR
    intersect_salida = os.path.join(carpeta_salida, nombre_intersect + fechaArchi)
    in_memory= "in_memory\intersect_All_Field"
    arcpy.analysis.PairwiseIntersect([dm_cua_salida, salida_limitaciones], in_memory, "ALL")
    field_mappings = mapeo_campos([in_memory], campos)
    arcpy.conversion.FeatureClassToFeatureClass(in_memory, carpeta_salida, nombre_intersect + fechaArchi, field_mapping = field_mappings)
    field_area = "AREA_INT"
    arcpy.management.AddField(intersect_salida, field_area, "DOUBLE", field_scale = 4, field_precision = 8)
    resultados = dict()
    with arcpy.da.UpdateCursor(intersect_salida, [field_area, "SHAPE@AREA", "ETIQUETA"]) as cursorIntDmCua:
        for i in cursorIntDmCua:
            i[0] = i[1]
            area= i[1]
            etiqueta = i[2]
            # Establecer la condición en función del área
            condicion = "SUPERPUESTO" if area > 990000 else "REDUCIR"
            
            # Si el IDENT ya existe en el diccionario, comparar el área
            if etiqueta in resultados:
                max_area, _ = resultados[etiqueta]
                
                # Si el área actual es mayor que la anterior, actualizamos el diccionario
                if area > max_area:
                    resultados[etiqueta] = (area, condicion)
            else:
                # Si no existe, simplemente agregarlo al diccionario
                resultados[etiqueta] = (area, condicion)

            cursorIntDmCua.updateRow(i)

    with arcpy.da.UpdateCursor(dm_cua_salida, ["ETIQUETA","RESULTADO"]) as finalcursor:
        for i in finalcursor:
            etiqueta = i[0]
            resultado = i[1]
            if etiqueta in resultados:
                i[1] = resultados[etiqueta][1]
            else: i[1] = "LIBRE"
            arcpy.AddMessage("OBS: "+ resultado)
            finalcursor.updateRow(i)

    resultado_reducir = os.path.join(carpeta_salida, "Resultado_reducir" + fechaArchi)
    arcpy.analysis.PairwiseDissolve(dm_cua_salida, resultado_reducir, ["CODIGOU","RESULTADO"], "ETIQUETA CONCATENATE", "SINGLE_PART", ", ")
    arcpy.edit.Generalize(resultado_reducir)
    resultado_areas = dict()
    # Mensajes informativos
    arcpy.AddMessage("Proceso finalizado correctamente.")
    arcpy.AddMessage(f"Capas generadas en: {carpeta_salida}")
    arcpy.AddMessage(f" - Polígonos EV: {ev_salida}")
    arcpy.AddMessage(f" - Polígonos PR: {pr_salida}")
    arcpy.AddMessage(f" - Intersección EV-PR: {intersect_salida}")
    # arcpy.AddMessage(f" - Polígonos sin intersección: {no_intersect_salida}")
    # arcpy.AddMessage("Áreas calculadas:")
    # arcpy.AddMessage(f" - Área superpuesta total: {area_intersectada}")
    # arcpy.AddMessage(f" - Área disponible total: {area_no_intersectada}")

    # (Opcional) Agregar las capas resultantes al mapa activo de ArcGIS Pro
    if anadir_al_mapa:
        # Agregamos las capas resultantes
        mapa_activo.addDataFromPath(intersect_salida + ".shp")
        mapa_activo.addDataFromPath(dm_cua_salida + ".shp")
        mapa_activo.addDataFromPath(resultado_reducir + ".shp")
    
    resultado_areas["nombreDmCuadricula"] = nombre_dm_cua_salida + fechaArchi
    resultado_areas["nombreInterReducir"] = nombre_intersect + fechaArchi
    resultado_areas["nombreResultadoReducir"] = "Resultado_reducir" + fechaArchi
    resultado_areas["Ruta_Interseccion"] = intersect_salida + ".shp"
    return resultado_areas

if __name__ == '__main__':
    try:
        in_layer1 = arcpy.GetParameterAsText(0) # Capa Catastro
        in_layer2 = arcpy.GetParameterAsText(1) # Capa Cuadriculas
        in_folder = arcpy.GetParameterAsText(2)
        in_id_eva = arcpy.GetParameterAsText(3)
        in_bool_add_layer = arcpy.GetParameterAsText(4)
        response = calculo_area_a_reducir(in_layer1, in_layer2, in_folder, in_id_eva, in_bool_add_layer)

        arcpy.AddMessage("Calculo Existoso")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        response["Error"] = traceback.format_exc()
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(5, response)