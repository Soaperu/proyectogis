import arcpy
import json

in_codigo = arcpy.GetParameterAsText(0)
in_user = arcpy.GetParameterAsText(1)

url_servicio = "https://geocatmin.ingemmet.gob.pe/arcgis/rest/services;GEOPROCESO/DatosLibreDenunciabilidad/"

def calcular_y_actualizar_prioritarios(codigo, sesion='123', usuario='user'):
    """
    Calcular prioritarios y actualizar evaltecnica_ld
    """
    arcpy.ImportToolbox(url_servicio)
    param = '{"codigo":"%s", "sesion":"%s", "user":"%s"}'%(codigo, sesion, usuario)
    respuesta = arcpy.DatosLibreDenunciabilidad(param)

    # param = json.loads(param)
    # value = service_datos_libredenu(**param)

    while respuesta.status < 4:
        time.sleep(0.2)
    return


def main():
    calcular_y_actualizar_prioritarios(in_codigo, sesion='123', usuario='in_user')

if __name__ == '__main__':
    try:
        main()
        response = {"success":True}
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        response = {"success":False}
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(2, response)