# Importar librerias
import arcpy
import json
import packages_aut_arc as pkga
import uuid


# try:
response = dict()
response['status'] = 1
response['message'] = 'success'

cd_correl = arcpy.GetParameterAsText(0)
sector = arcpy.GetParameterAsText(1)
documento = arcpy.GetParameterAsText(2)
fec_solicitud = arcpy.GetParameterAsText(3)
solicitante = arcpy.GetParameterAsText(4)
emisor = arcpy.GetParameterAsText(5)
asunto = arcpy.GetParameterAsText(6)
tipo_info = arcpy.GetParameterAsText(7)
responsables = arcpy.GetParameterAsText(8)
fec_asignacion = arcpy.GetParameterAsText(9)
estado = arcpy.GetParameterAsText(10)
fec_atencion = arcpy.GetParameterAsText(11)
observacion = arcpy.GetParameterAsText(12)
departamento = arcpy.GetParameterAsText(13)
x = arcpy.GetParameterAsText(14)
y = arcpy.GetParameterAsText(15)
zona = arcpy.GetParameterAsText(16)
distritos = arcpy.GetParameterAsText(17)

psid = str(uuid.uuid4().hex)

pkga.insert_row_pg_dgar(
    cd_correl,
    sector,
    documento,
    fec_solicitud,
    solicitante,
    emisor,
    asunto,
    tipo_info,
    responsables,
    fec_asignacion,
    estado,
    fec_atencion,
    observacion,
    departamento,
    psid,
    x,
    y,
    zona,
    iscommit=True
)

if departamento != "99":
    if distritos:
        distritos = distritos.split(",")
        for distrito in distritos:
            pkga.insert_row_pg_dgar_distritos(psid, distrito, iscommit=True)


# except Exception as e:
#     response['status'] = 0
#     response['message'] = e.message
# finally:
response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
arcpy.SetParameterAsText(18, response)
