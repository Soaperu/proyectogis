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
    # Insertar procesos 
    mxd = arcpy.mapping.MapDocument("CURRENT")

    list_tables_views = arcpy.mapping.ListTableViews(mxd, "*{}*".format(tb_leyenda.name), mxd.activeDataFrame)

    tb_view = arcpy.mapping.TableView(tb_leyenda.path)
    if len(list_tables_views):
        tb_view = list_tables_views[0]
    
    if codhoja:
        if len(codhoja.split(","))> 1:
            query = "LOWER({}) IN ('{}') and {} = 1".format(tb_leyenda.codhoja, "','".join(codhoja.split(",")).lower(), tb_leyenda.estado)
        else: query = "LOWER({}) = '{}' and {} = 1".format(tb_leyenda.codhoja, codhoja.lower(), tb_leyenda.estado)
        tb_view.definitionQuery = query
    
    if len(list_tables_views) == 0:
        arcpy.mapping.AddTableView(mxd.activeDataFrame, tb_view)
    arcpy.RefreshActiveView()

    response["response"]="success"
except Exception as e:
    response['status'] = 0
    response['message'] = e.message
finally:
    response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
    arcpy.SetParameterAsText(1, response)
