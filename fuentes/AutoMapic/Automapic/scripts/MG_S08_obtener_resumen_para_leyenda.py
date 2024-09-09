# Importar librerias
import arcpy
import MG_S00_model as model
import json

response = dict()
response['status'] = 1
response['message'] = 'success'

if __name__ == '__main__':
    try:
        arcpy.env.overwriteOutput = True
        codhoja = arcpy.GetParameterAsText(0)
        # codhoja = '29r4'
        fc_hojas = model.gpo_dg_hojas_50()
        tb_leyenda = model.tb_dgr_leyenda()

        query = "LOWER({}) = '{}'".format(fc_hojas.codhoja, codhoja.lower())

        # Ocultar los registros anteriores
        # tb_leyenda_view = arcpy.MakeTableView_management(tb_leyenda.path, 'tv_leyenda', query)
        # count_data = int(arcpy.GetCount_management(tb_leyenda_view).getOutput(0))

        # if count_data:
        #     arcpy.CalculateField_management(tb_leyenda, tb_leyenda.estado, "0")

        # Obtener zonas geografica de la hoja seleccionada
        cursor_sc = arcpy.da.SearchCursor(fc_hojas.path, [fc_hojas.codhoja, fc_hojas.zona], query)
        zona = map(lambda i: i[1], cursor_sc)[0]
        fc_ulito = model.gpo_dgr_ulito(str(zona))

        tb_leyenda_fields = tb_leyenda.get_fields()
        fc_ulito_fields = fc_ulito.get_fields()
        fields_match = list(set(tb_leyenda_fields) & set(fc_ulito_fields))

        fields_match.remove(tb_leyenda.etiqueta)
        fields_match_equiv = {'MIN_{}'.format(i): i for i in fields_match}

        statistics_fields = ';'.join(map(lambda i: '{} MIN'.format(i), fields_match))

        mf_ulito = arcpy.Select_analysis(fc_ulito.path, 'in_memory\\fl_ulito', query)
        #mf_ulito = arcpy.MakeFeatureLayer_management(fc_ulito.path, 'fl_ulito', query)
        count_data = arcpy.GetCount_management(mf_ulito).getOutput(0)
        if int(count_data) == 0:
            raise RuntimeError("No se encontraron registros para la hoja seleccionada")

        diss = arcpy.Dissolve_management(mf_ulito, 'in_memory\\dissolve', fc_ulito.etiqueta, statistics_fields)

        dissToTable = arcpy.TableToTable_conversion(diss, arcpy.env.scratchGDB, 'dissToTable')
        
        for k, v in fields_match_equiv.items():
            arcpy.AlterField_management(dissToTable, k, v)
        
        arcpy.AddField_management(dissToTable, tb_leyenda.estado, "SHORT")
        arcpy.CalculateField_management(dissToTable, tb_leyenda.estado, "1")
        
        tv_leyenda = arcpy.MakeTableView_management(tb_leyenda.path, 'tv_leyenda', query)
        tb_leyenda_count = arcpy.GetCount_management(tv_leyenda).getOutput(0)
        if int(tb_leyenda_count) > 0:
            # arcpy.DeleteRows_management(tv_leyenda)
            arcpy.CalculateField_management(tv_leyenda, tb_leyenda.estado, "0")
        del mf_ulito, diss
        arcpy.Append_management(dissToTable, tb_leyenda.path, "NO_TEST")

    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(1, response)
