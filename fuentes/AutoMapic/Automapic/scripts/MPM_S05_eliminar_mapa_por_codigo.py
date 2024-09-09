# Importar librerias
import arcpy
import json
import packages_aut_arc as pkga
from MPM_S03_mapa_potencial_minero import _DELETE_STATE

# Permite eliminar un mapa que esta registrado 
# en la base de datos coorporativa

if __name__ == '__main__':
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    cd_mapa = arcpy.GetParameterAsText(0)
    detalle = arcpy.GetParameterAsText(1)

    detalle = detalle if detalle else ''

    try:
        pkga.update_state_row(cd_mapa, _DELETE_STATE, detalle, iscommit=True)
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(2, response)
