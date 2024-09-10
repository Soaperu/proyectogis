# Importar librerias
import arcpy
import json
import packages_aut_arc as pkga

# Permite obtener los datos de un mapa que esta registrado 
# en la base de datos coorporativa

if __name__ == '__main__':
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    cd_mapa = arcpy.GetParameterAsText(0)

    try:
        data = pkga.get_map_by_code(cd_mapa, getcursor=True)
        response["response"] = data
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
