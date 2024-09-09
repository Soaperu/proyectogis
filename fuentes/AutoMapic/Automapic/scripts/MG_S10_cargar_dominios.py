# Importar librerias
import arcpy
import json
import MG_S00_model as model

try:
    response = dict()
    response['status'] = 1
    response['message'] = 'success'

    tb_leyenda = model.tb_dgr_leyenda()
    codhoja = arcpy.GetParameterAsText(0)
    tipo_leyenda = arcpy.GetParameterAsText(1)

    if len(codhoja.split(","))>1:
        query = "({} IN ('{}')) and ({} = 1) and ({} = '{}')".format(tb_leyenda.codhoja, "','".join(codhoja.split(",")), tb_leyenda.estado, tb_leyenda.tipo, tipo_leyenda)
    else: query = "({} = '{}') and ({} = 1) and ({} = '{}')".format(tb_leyenda.codhoja, codhoja, tb_leyenda.estado, tb_leyenda.tipo, tipo_leyenda)
    cursor = arcpy.da.SearchCursor(tb_leyenda.path, [tb_leyenda.dominio], query)
    domains = list(set(map(lambda i: i[0], cursor)))
    domains = filter(lambda i: i, domains)
    domains.sort()
    response['response'] = domains
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response)#, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(2, response)
