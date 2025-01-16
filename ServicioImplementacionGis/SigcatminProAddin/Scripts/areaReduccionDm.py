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


# path to log %AppData%\Roaming\Esri\ArcGISPro\ArcToolbox\History.
arcpy.SetLogHistory(True)

arcpy.env.overwriteOutput = True

response = dict()

campos = ["CODIGOU", "CODIGOU_1", "CODIGO","NM_AREA", "ESTILO"]

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

def calculo_area_superpuestas_y_disponibles(
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
    nombre_pr_salida : str
        Nombre de la capa de salida que contendrá los polígonos PR.
    nombre_intersect : str
        Nombre de la capa de salida con la intersección de EV y PR.
    nombre_no_intersect : str
        Nombre de la capa de salida con la geometría no intersectada
        (EV + PR sin solapamientos).

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
    nombre_intersect="Areainter_"
    nombre_no_intersect="Areadispo_"
    
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
    arcpy.management.AddField(dm_cua_salida, "INDICE", "TEXT", field_length = 2)
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

    # 5. Limpiar selección
    arcpy.management.SelectLayerByAttribute(capa_cat, "CLEAR_SELECTION")
    arcpy.management.SelectLayerByAttribute(capa_cuadriculas, "CLEAR_SELECTION")

    # Agregamos a la lista de capas a unir Caram, si existen
    ruta_caram = os.path.join(carpeta_salida, capa_caram + fechaArchi + ".shp")
    if os.path.isfile(ruta_caram):
        capas_a_unir.append(ruta_caram)

    # Agregamos a la lista de capas a unir Catastro Forestal, si existen
    ruta_cat_forestal = os.path.join(carpeta_salida, capa_catastro_forestal + fechaArchi + ".shp")
    if os.path.isfile(ruta_cat_forestal):
        capas_a_unir.append(ruta_cat_forestal)

    #arcpy.management.SelectLayerByLocation("DM_Cua_638725549858548162", "COMPLETELY WITHIN", 'Caram',"", 'NEW_SELECTION')

    salida_limitaciones = os.path.join(carpeta_salida, salida_limitaciones + fechaArchi)
    if len(capas_a_unir)==0:
        return
    arcpy.management.Merge(capas_a_unir, salida_limitaciones)
    # 6. Intersección entre las capas EV y PR
    intersect_salida = os.path.join(carpeta_salida, nombre_intersect + fechaArchi)
    in_memory= "in_memory\intersect_All_Field"
    arcpy.analysis.PairwiseIntersect([dm_cua_salida, salida_limitaciones],
                                        in_memory,
                                        "ALL")
    field_mappings = mapeo_campos([in_memory], campos)
    arcpy.conversion.FeatureClassToFeatureClass(in_memory, carpeta_salida, nombre_intersect + fechaArchi, field_mapping = field_mappings)
    arcpy.management.AddField(intersect_salida, "RESULTADO", "TEXT", field_length = 50)
    # # 7. Obtener la geometría que NO se intersecta
    # #    Para ello, hacemos "Erase" de cada capa usando la intersección como "erase_features".
    # no_intersect_salida= os.path.join(carpeta_salida, nombre_no_intersect + fechaArchi)
    # arcpy.analysis.Erase(ev_salida, pr_salida, no_intersect_salida)


    # # 8. Combinar la parte no intersectada (EV + PR) en un solo feature class
    # # no_intersect_salida = os.path.join(carpeta_salida, nombre_no_intersect + archi)
    # # arcpy.management.Merge([ev_no_intersect, pr_no_intersect],
    # #                        no_intersect_salida)

    # # 9. Calcular el área total de la capa intersectada y de la no intersectada
    # #    Suponiendo que los datos tienen proyección adecuada para medir áreas en m2 (o la unidad deseada).
    # area_intersectada = 0.0
    # with arcpy.da.SearchCursor(intersect_salida, ["SHAPE@AREA"]) as cursor:
    #     for row in cursor:
    #         area_intersectada += row[0]

    # area_no_intersectada = 0.0
    # with arcpy.da.SearchCursor(no_intersect_salida, ["SHAPE@AREA"]) as cursor:
    #     for row in cursor:
    #         area_no_intersectada += row[0]

    # # 10. Crear el diccionario a retornar


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
        # mapa_activo.addDataFromPath(no_intersect_salida + ".shp")
    
    # resultado_areas["nombreSuperpuesta"] = nombre_intersect + fechaArchi
    # resultado_areas["nombreDisponible"] = nombre_no_intersect + fechaArchi
    resultado_areas ={"Ruta_Interseccion": intersect_salida + ".shp"}
    return resultado_areas

if __name__ == '__main__':
    try:
        in_layer1 = arcpy.GetParameterAsText(0) # Capa Catastro
        in_layer2 = arcpy.GetParameterAsText(1) # Capa Cuadriculas
        in_folder = arcpy.GetParameterAsText(2)
        in_id_eva = arcpy.GetParameterAsText(3)
        in_bool_add_layer = arcpy.GetParameterAsText(4)
        response = calculo_area_superpuestas_y_disponibles(in_layer1, in_layer2, in_folder, in_id_eva, in_bool_add_layer)

        arcpy.AddMessage("Calculo Existoso")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        response["Error"] = traceback.format_exc()
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(5, response)