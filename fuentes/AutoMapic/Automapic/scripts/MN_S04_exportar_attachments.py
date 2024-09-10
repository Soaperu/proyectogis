# Importar librerias
import arcpy
import json
import os
import settings_aut as st
import uuid

def export_attachments(output_dir, feature, query=None):
    fields = [st._ATTACHMENTID_FIELD, st._REL_GLOBALID_FIELD, st._ATT_NAME_FIELD, st._DATA_FIELD]
    cursor = arcpy.da.SearchCursor(feature, fields, query)
    for row in cursor:
        attachment = row[-1]
        filename = 'ATT{}_{}'.format(row[0], row[2])
        open(os.path.join(output_dir, filename), 'wb').write(attachment.tobytes())
        del row
        del filename
        del attachment     
    del cursor   

if __name__ == '__main__':
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    cd_depa = arcpy.GetParameterAsText(0)
    # cd_depa = '02'

    try:
        # Insertar procesos

        # Creando ruta del directorio donde se almacenaran los attachments
        response['response'] = os.path.join(st._TEMP_FOLDER, uuid.uuid4().hex)

        # Creado directorio de attachments
        os.mkdir(response['response'])

        # Filtrando las muestras en la region especificada
        query = "{} = '{}'".format(st._REGION_FIELD, cd_depa)
        globalids = map(lambda i: i[0], arcpy.da.SearchCursor(st._GPT_NEO_MUESTRA, [st._GLOBALID_FIELD], query))
        
        # Filtrando los attachments de las muestras 
        query_parentglobalids = "{} in ('{}')".format(st._PARENTGLOBALID_FIELD, "','".join(globalids))
        globalids_att = map(lambda i: i[0], arcpy.da.SearchCursor(st._TB_NEO_FOTOGRAFIA, [st._GLOBALID_FIELD], query_parentglobalids))

        # Extrayendo los attachments
        query_relglobalids = "{} in ('{}')".format(st._REL_GLOBALID_FIELD, "','".join(globalids_att))
        export_attachments(response['response'], st._TB_NEO_FOTOGRAFIA__ATTACH, query_relglobalids)
        
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
