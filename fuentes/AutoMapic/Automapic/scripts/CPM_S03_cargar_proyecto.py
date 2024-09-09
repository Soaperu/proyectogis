# Importar librerias
import arcpy
import json
from CPM_S02_model import *
import messages_aut as msg
import packages_aut as pkg


_ENV_GDB_PATH_CPM = 'GDB_PATH_CPM'


def validation_geodatabase(path_geodatabase):
    # Existencia
    if not arcpy.Exists(path_geodatabase):
        return 0
    return 1

def validation_feature(path_feature, val_lenght_data=True):
    # Existencia
    if not arcpy.Exists(path_feature):
        return 0
    # Registros
    if val_lenght_data:
        n_rows = arcpy.GetCount_management(path_feature)
        if int(n_rows.getOutput(0)) == 0:
            return 0
    return 1

if __name__ == '__main__':
    try:
        response = dict()
        response['status'] = 1
        response['message'] = 'success'

        workspace = arcpy.GetParameterAsText(0)
        geodatabase = arcpy.GetParameterAsText(1)
        only_validation = arcpy.GetParameter(2)

        if only_validation:
            geodatabase_path = pkg.get_config_param_value(_ENV_GDB_PATH_CPM, one=True)
        else:
            geodatabase_path = os.path.join(workspace, geodatabase)

        features = list()
        response_validation = list()

        val_geodatabase = validation_geodatabase(geodatabase_path)
        if val_geodatabase == 0:
            raise RuntimeError(msg._CPM_GEODATABASE_NOT_EXIST)

        config_tb = tb_config(geodatabase_path)
        r = validation_feature(config_tb.path)
        if r == 0:
            raise RuntimeError(msg._CPM_TB_CONFIG_NOT_EXIST)
        
        scursor = arcpy.da.SearchCursor(config_tb.path, [config_tb.cd_depa, config_tb.region, config_tb.zona])
        data = [{'cd_depa': i[0], 'nm_depa': i[1], 'zona': i[2]}  for i in scursor]


        # features.append(pm_region(geodatabase_path))
        features.append(pmm_gpo_ugeol(geodatabase_path))
        # features.append(pmm_tb_ugeol_condicion(geodatabase_path))
        # features.append(pmm_var_ugeol(geodatabase_path))
        # features.append(pmm_ras_ugeol(geodatabase_path))

        features.append(pmm_gpl_fallageol(geodatabase_path))
        # features.append(pmm_tb_fallageol(geodatabase_path))
        # features.append(pmm_tb_fallageol_grado(geodatabase_path))
        # features.append(pmm_var_fallageol(geodatabase_path))
        # features.append(pmm_ras_fallageol(geodatabase_path))

        features.append(pmm_gpo_depmineral(geodatabase_path))
        # features.append(pmm_var_depmineral(geodatabase_path))
        # features.append(pmm_ras_depmineral(geodatabase_path))

        # features.append(pmm_gpo_concmin(geodatabase_path))
        # features.append(pmm_tb_concmin_grado(geodatabase_path))
        # features.append(pmm_var_concmin(geodatabase_path))
        # features.append(pmm_ras_concmin(geodatabase_path))

        features.append(pmm_ras_geoquimica(geodatabase_path))

        features.append(pmm_gpo_sensores(geodatabase_path))
        # features.append(pmm_tb_sensores_grado(geodatabase_path))
        # features.append(pmm_var_sensores(geodatabase_path))
        # features.append(pmm_ras_sensores(geodatabase_path))

        features.append(rmi_gpo_litologia(geodatabase_path))
        # features.append(rmi_tb_litologia_condicion(geodatabase_path))
        # features.append(rmi_var_litologia(geodatabase_path))
        # features.append(rmi_ras_litologia(geodatabase_path))

        # features.append(rmi_gpo_concmin(geodatabase_path))
        # features.append(rmi_tb_concmin_grado(geodatabase_path))
        # features.append(rmi_var_concmin(geodatabase_path))
        # features.append(rmi_ras_concmin(geodatabase_path))

        features.append(rmi_gpl_accesos(geodatabase_path))
        # features.append(rmi_tb_accesos(geodatabase_path))
        # features.append(rmi_var_accesos(geodatabase_path))
        # features.append(rmi_ras_accesos(geodatabase_path))

        features.append(rmi_gpt_sustancias(geodatabase_path))
        # features.append(rmi_tb_sustancias(geodatabase_path))
        # features.append(rmi_var_sustancias(geodatabase_path))
        # features.append(rmi_ras_sustancias(geodatabase_path))

        features.append(rmi_gpo_sensores(geodatabase_path))
        # features.append(rmi_tb_sensor_grado(geodatabase_path))
        # features.append(rmi_var_sensores(geodatabase_path))
        # features.append(rmi_ras_sensores(geodatabase_path))


        # features.append(tb_nivel(geodatabase_path))
        # features.append(pmm_tb_factor(geodatabase_path))

        features.append(ras_potencial_minero_metalico(geodatabase_path))
        # features.append(rmi_tb_factor(geodatabase_path))
        features.append(ras_potencial_minero_no_metalico(geodatabase_path))

        for i in features:
            r = validation_feature(i.path, val_lenght_data= i.dataType != 'rasterdataset')
            response_validation.append([i.name, r])
        
        if not only_validation:
            pkg.set_config_param(9, geodatabase_path, iscommit=True)

        # Insertar procesos 
        response["response"] = response_validation
        response['data'] = data[0]
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(3, response)
