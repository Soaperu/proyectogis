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

#temp_folder = r"c:/bdgeocatmin/temporal"

def verificar_mapa_activo():
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    mapa_activo = aprx.activeMap
    
    # Verificar nombre del mapa
    if mapa_activo.name.lower() != "catastro minero":
        # Crear la ventana raíz de Tk (aunque no se muestre)
        root = tkinter.Tk()
        root.withdraw()  # Oculta la ventana principal
        
        # Mostrar el cuadro de diálogo
        messagebox.showwarning("Advertencia", 
            "El mapa activo no se llama 'catastro minero'. Se abortará el proceso.")
        
        # Destruir la ventana raíz para evitar que quede abierta
        root.destroy()
        
        # Terminar sin seguir
        return False
    
    # Si todo bien, continuar
    return True

def calculo_area_superpuestas_y_disponibles(
        capa_entrada,
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
    capa_temp = "capa_temp"
    arcpy.management.MakeFeatureLayer(capa_entrada, capa_temp)

    # 3. Seleccionar y exportar polígonos "EV"
    #where_ev = "EVAL = 'EV'"
    #arcpy.management.SelectLayerByAttribute(capa_temp, "NEW_SELECTION", where_ev)
    ev_salida = os.path.join(carpeta_salida, nombre_ev_salida + fechaArchi)
    #arcpy.management.CopyFeatures(capa_temp, ev_salida)

    # 4. Seleccionar y exportar polígonos "PR"
    where_pr = "EVAL = 'PR'"
    arcpy.management.SelectLayerByAttribute(capa_temp, "NEW_SELECTION", where_pr)
    pr_salida = os.path.join(carpeta_salida, nombre_pr_salida + fechaArchi)
    arcpy.management.CopyFeatures(capa_temp, pr_salida)

    # 5. Limpiar selección
    arcpy.management.SelectLayerByAttribute(capa_temp, "CLEAR_SELECTION")

    # 6. Intersección entre las capas EV y PR
    intersect_salida = os.path.join(carpeta_salida, nombre_intersect + fechaArchi)
    intersect_salida_temp = os.path.join(carpeta_salida, nombre_intersect + fechaArchi + "_temp")#f"in_memory\{nombre_intersect}{fechaArchi}" # + nombre_intersect + fechaArchi
    arcpy.AddMessage(f"generando: {intersect_salida_temp}")
    arcpy.analysis.Intersect([ev_salida, pr_salida],
                             intersect_salida_temp,
                             "ALL")
    arcpy.AddMessage(f"generado: {intersect_salida_temp}")
    # 7. Obtener la geometría que NO se intersecta
    #    Para ello, hacemos "Erase" de cada capa usando la intersección como "erase_features".
    # ev_no_intersect = os.path.join(carpeta_salida, "EV_no_intersect")
    # pr_no_intersect = os.path.join(carpeta_salida, "PR_no_intersect")
    # arcpy.analysis.Erase(ev_salida, intersect_salida, ev_no_intersect)
    # arcpy.analysis.Erase(pr_salida, intersect_salida, pr_no_intersect)
    no_intersect_salida= os.path.join(carpeta_salida, nombre_no_intersect + fechaArchi)
    arcpy.analysis.Erase(ev_salida, pr_salida, no_intersect_salida)


    # 8. Combinar la parte no intersectada (EV + PR) en un solo feature class
    # no_intersect_salida = os.path.join(carpeta_salida, nombre_no_intersect + archi)
    # arcpy.management.Merge([ev_no_intersect, pr_no_intersect],
    #                        no_intersect_salida)

    # 9. Calcular el área total de la capa intersectada y de la no intersectada
    #    Suponiendo que los datos tienen proyección adecuada para medir áreas en m2 (o la unidad deseada).
    area_intersectada = 0.0
    with arcpy.da.SearchCursor(intersect_salida_temp, ["SHAPE@AREA"]) as cursor:
        for row in cursor:
            area_intersectada += row[0]

    area_no_intersectada = 0.0
    with arcpy.da.SearchCursor(no_intersect_salida, ["SHAPE@AREA"]) as cursor:
        for row in cursor:
            area_no_intersectada += row[0]

    # 10. Crear el diccionario a retornar
    resultado_areas = {
        "area_superpuesta": round(area_intersectada, 4),
        "area_disponible": round(area_no_intersectada, 4)
    }

    capas_resultantes = [no_intersect_salida + ".shp"]
    campos_a_conservar = ["CODIGOU",
                            "FEC_DENU",
                            "CODIGOU_1",
                            "CONCESIO_1"]
    
    for capa in capas_resultantes:
            # Obtener todos los campos
            lista_campos = arcpy.ListFields(capa)
            # Armar la lista de campos a eliminar
            campos_a_eliminar = [
                f.name for f in lista_campos
                if f.name not in campos_a_conservar
            ]
            # Si hay campos por eliminar, los borramos
            if campos_a_eliminar:
                for field in campos_a_eliminar:
                    try:
                        arcpy.management.DeleteField(capa, [field])
                    except:
                        arcpy.AddMessage("No se eliminaron los campos:" + str(field))
                        pass

    arcpy.analysis.PairwiseDissolve(intersect_salida_temp, intersect_salida, campos_a_conservar, "", 'SINGLE_PART')
    arcpy.management.AddField(intersect_salida, "AREA_FINAL", "DOUBLE", field_scale=4, field_precision=18)
    with arcpy.da.UpdateCursor(intersect_salida, ["SHAPE@AREA", "AREA_FINAL"]) as cursor_f:
        for row in cursor_f:
            row[1] = row[0]
            cursor_f.updateRow(row)
            
    # Mensajes informativos
    #arcpy.AddMessage("Proceso finalizado correctamente.")
    arcpy.AddMessage(f"Capas generadas en: {carpeta_salida}")
    arcpy.AddMessage(f" - Polígonos EV: {ev_salida}")
    arcpy.AddMessage(f" - Polígonos PR: {pr_salida}")
    arcpy.AddMessage(f" - Intersección EV-PR: {intersect_salida}")
    arcpy.AddMessage(f" - Polígonos sin intersección: {no_intersect_salida}")
    arcpy.AddMessage("Áreas calculadas:")
    arcpy.AddMessage(f" - Área superpuesta total: {area_intersectada}")
    arcpy.AddMessage(f" - Área disponible total: {area_no_intersectada}")

    # (Opcional) Agregar las capas resultantes al mapa activo de ArcGIS Pro
    if anadir_al_mapa:
        # Agregamos las capas resultantes
        mapa_activo.addDataFromPath(intersect_salida + ".shp")
        mapa_activo.addDataFromPath(no_intersect_salida + ".shp")
    
    resultado_areas["nombreSuperpuesta"] = nombre_intersect + fechaArchi
    resultado_areas["nombreDisponible"] = nombre_no_intersect + fechaArchi
    return resultado_areas

if __name__ == '__main__':
    try:
        in_layer = arcpy.GetParameterAsText(0)
        in_folder = arcpy.GetParameterAsText(1)
        in_id_eva = arcpy.GetParameterAsText(2)
        in_bool_add_layer = arcpy.GetParameterAsText(3)
        response = calculo_area_superpuestas_y_disponibles(in_layer, in_folder, in_id_eva, in_bool_add_layer)

        arcpy.AddMessage("Calculo Existoso")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        response["Error"] = traceback.format_exc()
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(4, response)