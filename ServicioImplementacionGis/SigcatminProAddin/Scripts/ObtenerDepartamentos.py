import arcpy
import os
import json

response = dict()
def obtenerDepartamentos(codigo):
    """
    Return a list of dictionarys of intersections between certain feture of lyr_catastro and lyr_departamentos
    properties of returned object: nombre, area de interseccion, porcentaje

    Args:
        codigo (_type_): _description_
    """
    listado = []
    lyr_catastro_name = "Catastro"
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    map_view = aprx.activeMap
    layer_catastro = map_view.listLayers(f'{lyr_catastro_name}')[0]

    lyr_departamento_name = "Departamento"
    busqueda = map_view.listLayers(f'{lyr_departamento_name}')
    if not busqueda:
        raise Exception(f"Capa {lyr_departamento_name} no encontrada")
    
    lyr_departamentos = busqueda[0]

    # Select features from lyr_catastro based on codigo
    catastro_fields = ["CODIGOU", "HECTAGIS","SHAPE@"]
    arcpy.management.SelectLayerByAttribute(layer_catastro, "NEW_SELECTION", f"CODIGOU = '{codigo}'")
    arcpy.SelectLayerByLocation_management(lyr_departamentos, "INTERSECT", layer_catastro)
    row_dm = [x for x in arcpy.da.SearchCursor(layer_catastro, catastro_fields)][0]
    area_dm = row_dm[1]
    geom_dm = row_dm[2]



    # Calculate area of intersection and percentage
    with arcpy.da.SearchCursor(lyr_departamentos, ["SHAPE@", "NM_DEPA"]) as cursor:
        for row in cursor:
            intersection_geom = geom_dm.intersect(row[0],4) 
            intersection_area = round(intersection_geom.area/10000 ,2)
            porcentaje = (intersection_area / area_dm) * 100
            listado.append({
                "nombre": row[1],
                "area": intersection_area,
                "porcentaje": porcentaje
            })

    # Clean Selection
    arcpy.management.SelectLayerByAttribute(lyr_departamentos, "CLEAR_SELECTION")

    removeLayerfromMapbyName(lyr_departamentos)

    return listado


    
    


def removeLayerfromMapbyName(layer):
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    map_view = aprx.activeMap
    map_view.removeLayer(layer)




if __name__ == '__main__':
    try:
        codigo = arcpy.GetParameterAsText(0)
        response = obtenerDepartamentos(codigo)

        arcpy.AddMessage("Satisfactorio")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        out_geom = None
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(1, response)