import arcpy
import json
import traceback

# Variable global donde almacenaremos los resultados a devolver
response = dict()

def procesarZonas(CARAM_WGS):
    """
    Esta función realiza los siguientes procesos geoespaciales:
      1) Select (filtro de entidades)
      2) Dissolve
      3) Intersect
      4) CalculateAreas

    Retorna:
        dict: con información o mensajes de progreso que se requieran.
    """

    # Diccionario local para ir guardando información de proceso, si hiciera falta
    resultado_local = {}
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    mapa_activo = aprx.activeMap
    # # Definimos variables locales (rutas o conexiones a SDE, shapefiles, etc.)

    # --------------------------------------------------------------------------
    # 1) Proceso: Select
    # --------------------------------------------------------------------------
    arcpy.Select_analysis(
                            in_features=CARAM_WGS,
                            out_feature_class = rd_shp,
                            where_clause= "TP_AREA = 'RED VIAL NACIONAL'"
                        )

    # --------------------------------------------------------------------------
    
    # Agregar un campo nuevo para almacenar el área
    arcpy.AddField_management(
                                in_table= rd_shp, 
                                field_name="F_AREA", 
                                field_type="DOUBLE"
                            )

    # 2) Calcular el área en ese campo
    arcpy.CalculateField_management(
                                        in_table= rd_shp,
                                        field="F_AREA",
                                        expression="!SHAPE.area@SQUAREMETERS!",  # <--- Calcula el área en m²
                                        expression_type="PYTHON3"
                                    )

    # Si quieres devolver alguna información en particular
    resultado_local["mensaje"] = "Procesos finalizados correctamente."
    resultado_local["ruta_salida"] = rd_shp

    if mapa_activo:
        # Agregamos las capas resultantes
        mapa_activo.addDataFromPath(rd_shp)
    
    return resultado_local


if __name__ == '__main__':
    try:
        # Llamamos a la función principal
        # Definimos variables locales (rutas o conexiones a SDE, shapefiles, etc.)
        DATA_GIS_GPO_CAR_CARAM_W_18_T = arcpy.GetParameterAsText(0) #r"Database Connections\Connection to 10.102.0.66.sde\DATA_GIS.DS_CATASTRO_MINERO_WGS84_18\DATA_GIS.GPO_CAR_CARAM_WGS_18"
        rd_shp = "C:\\bdgeocatmin\\Temporal\\rd.shp"
        response = procesarZonas(DATA_GIS_GPO_CAR_CARAM_W_18_T)

        arcpy.AddMessage("Proceso Exitoso")

    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        # Opcionalmente podrías capturar más detalles del error en el diccionario
        response["Error"] = traceback.format_exc()

    finally:
        # Convertir diccionario a JSON (siempre y cuando se utilice la salida en un script tool)
        response_json = json.dumps(response)

        # Si establecer el parámetro de salida:
        arcpy.SetParameterAsText(1, response_json)

        # Si puedes imprimirlo o guardarlo en un archivo
        arcpy.AddMessage(response_json)
