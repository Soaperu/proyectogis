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
    
    fields = arcpy.ListFields(tb_leyenda.path)
    field = filter(lambda i: i.name.lower() == tb_leyenda.tipo.lower(), fields)[0]
    domain = field.domain
    domains = arcpy.da.ListDomains(tb_leyenda.workspace)
    domain_obj = filter(lambda i: i.name == domain, domains)[0]
    if len(codhoja.split(","))>1:
        query = "({} IN ('{}')) and ({} = 1)".format(tb_leyenda.codhoja, "','".join(codhoja.split(",")), tb_leyenda.estado)
    else: query = "({} = '{}') and ({} = 1)".format(tb_leyenda.codhoja, codhoja, tb_leyenda.estado)
    cursor = arcpy.da.SearchCursor(tb_leyenda.path, [tb_leyenda.dominio], query)
    rock_type = list(set(map(lambda i: i[0], cursor)))
    rock_type = filter(lambda i: i, rock_type)
    rock_type.sort()
    response['response'] = [{'key': rock, 'value': domain_obj.codedValues[str(rock)]} for rock in rock_type]
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)