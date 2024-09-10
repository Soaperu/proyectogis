from datetime import datetime
from CPM_S02_model import *
import settings_aut as st
import arcpy
import os
import pandas as pd
from datetime import datetime
import json
import packages_aut as pkg
import messages_aut as msg

arcpy.env.overwriteOutput = True

def path_database_input(zone):
    name_gdb = 'DRME_PM_{}.gdb'.format(zone)
    path = os.path.join(st._GDB_CPM_DIR, name_gdb)
    return path

def get_props_by_cd_depa(cd_depa):
    df = pd.read_csv(st._CSV_REGION_CONFIG, sep=';', converters={st._CD_DEPA_FIELD: lambda x: str(x)})
    df2 = df[df[st._CD_DEPA_FIELD] == cd_depa]
    return df2

def create_name_geodatabase(region, zona):
    datetime_now = datetime.now()
    datetime_string = datetime_now.strftime("%m%d%Y_%H%M%S")
    name = 'DRME_PM_{}_{}_{}.gdb'.format(region, zona, datetime_string)
    return name

def update_fields_accesos(via):
    with arcpy.da.UpdateCursor(via.path, [via.nombre, via.rasgo_secu, via.tipo]) as cursor:
        for x in cursor:
            nombre = x[0].replace(' ', '')
            rasgo_secu = x[1].lower()
            if len(nombre):
                x[2] = "asfaltada nacional"
            else:
                if rasgo_secu == "carretera asfaltada":
                    x[2] = "asfaltada local"
                elif rasgo_secu == "carretera afirmada":
                    x[2] = "afirmada"
                else:
                    x[2] = "trocha"
            cursor.updateRow(x)

class MakeGdb(object):
    global pm_region
    def __init__(self, path, cd_depa):
        self.path = path
        self.props_depa = get_props_by_cd_depa(cd_depa)
        self.nm_depa = self.props_depa[st._NM_DEPA_FIELD].item()
        self.zone = self.props_depa[st._ZONA_FIELD].item()
        self.cd_depa = cd_depa # Codigo de departamento
        self.path_inp_geodatabase = path_database_input(self.zone) 
        self.main_folder = str()
        self.name_geodatabase = create_name_geodatabase(self.nm_depa, self.zone)
        self.path_out_geodatabase = os.path.join(self.path, self.name_geodatabase)
        self.scratchGDB = arcpy.env.scratchGDB
        self.features = list()

    def need_features(self):
        self.dep = pm_region(self.path_out_geodatabase)
        self.via = rmi_gpl_accesos(self.path_out_geodatabase)
        self.sustancia = rmi_gpt_sustancias(self.path_out_geodatabase)
        self.sensores = pmm_gpo_sensores(self.path_out_geodatabase)
        self.sensores_rm = rmi_gpo_sensores(self.path_out_geodatabase)
        self.cat_pmm = pmm_gpo_concmin(self.path_out_geodatabase)
        self.cat_rmi = rmi_gpo_concmin(self.path_out_geodatabase)

    def limit_region(self):
        dep = gpo_region()
        if not arcpy.Exists(dep.path):
            raise RuntimeError()
        
        # Si el departamento seleccionado es LIMA (15) este debe incluir CALLAO
        query = "{} = '{}'".format(dep.cd_depa, self.cd_depa)
        if self.cd_depa == '15':
            query = "{} in ('{}', '07')".format(dep.cd_depa, self.cd_depa)

        lim_reg_mfl = arcpy.MakeFeatureLayer_management(dep.path, "mfl_dep", query)

        if self.cd_depa == '15':
            lim_reg_mfl = arcpy.Dissolve_management(lim_reg_mfl, 'in_memory\\region', '#', '{} MAX;{} MAX'.format(dep.cd_depa, dep.nm_depa), 'MULTI_PART', 'DISSOLVE_LINES')
            arcpy.AlterField_management(lim_reg_mfl, 'MAX_{}'.format(dep.cd_depa), dep.cd_depa)
            arcpy.AlterField_management(lim_reg_mfl, 'MAX_{}'.format(dep.nm_depa), dep.nm_depa)
        arcpy.Append_management(lim_reg_mfl, self.dep.path, "NO_TEST")
        self.features.append(self.dep.path)

    def catastro_minero(self, type_catastro):
        cat = gpo_catastro_minero()
        if not arcpy.Exists(cat.path):
            raise RuntimeError()

        if type_catastro == "metalico":
            query = "%s = 'TITULADO' AND (%s = 'M')" % (cat.leyenda, cat.naturaleza)
        elif type_catastro == "no metalico":
            query = "%s = 'TITULADO' AND (%s = 'N')" % (cat.leyenda, cat.naturaleza)

        catastro_tmp = arcpy.MakeFeatureLayer_management(cat.path, 'mfl_cat_min', query)
        arcpy.SelectLayerByLocation_management(catastro_tmp, "INTERSECT", self.dep.path, "#", "NEW_SELECTION", "NOT_INVERT")
        fc_download = arcpy.CopyFeatures_management(catastro_tmp, os.path.join(self.scratchGDB, 'catastro_copy'))
        cmi_clip_for_region = arcpy.Clip_analysis(fc_download, self.dep.path, os.path.join(self.scratchGDB, 'catastro_clip'))

        if type_catastro == "metalico":
            arcpy.Append_management(cmi_clip_for_region, self.cat_pmm.path, "NO_TEST")
            self.features.append(self.cat_pmm.path)
        elif type_catastro == "no metalico":
            arcpy.Append_management(cmi_clip_for_region, self.cat_rmi.path, "NO_TEST")
            self.features.append(self.cat_rmi.path)
        

    def vias(self):
        vias = rmi_accesos()
        if not arcpy.Exists(vias.path):
            raise RuntimeError()
        vias_tmp = arcpy.MakeFeatureLayer_management(vias.path, 'mfl_vias')

        arcpy.SelectLayerByLocation_management(vias_tmp, "INTERSECT", self.dep.path, "#", "NEW_SELECTION", "NOT_INVERT")
        fc_download = arcpy.CopyFeatures_management(vias_tmp, os.path.join(self.scratchGDB, 'vias_copy'))
        vias_clip_for_region = arcpy.Clip_analysis(fc_download, self.dep.path, os.path.join(self.scratchGDB, 'vias_clip'))
        arcpy.Append_management(vias_clip_for_region, self.via.path, "NO_TEST")
        self.features.append(self.via.path)
        try:
            update_fields_accesos(self.via)
        except:
            pass

    def sustancias(self):
        sust = rmi_sustancias()
        if not arcpy.Exists(sust.path):
            raise RuntimeError()

        sust_tmp = arcpy.MakeFeatureLayer_management(sust.path, 'mfl_sust')

        arcpy.SelectLayerByLocation_management(sust_tmp, "INTERSECT", self.dep.path, "#", "NEW_SELECTION", "NOT_INVERT")
        fc_download = arcpy.CopyFeatures_management(sust_tmp, os.path.join(self.scratchGDB, 'sust_copy'))
        sust_clip_for_region = arcpy.Clip_analysis(fc_download, self.dep.path, os.path.join(self.scratchGDB, 'sust_clip'))

        arcpy.CalculateField_management(sust_clip_for_region, self.sustancia.sustancia, '!{}!.lower()'.format(self.sustancia.sustancia), 'PYTHON_9.3', '#')

        arcpy.Append_management(sust_clip_for_region, self.sustancia.path, "NO_TEST")
        self.features.append(self.sustancia.path)
    
    def sensores_remotos(self):
        sensores_remotos = gpo_sensores_remotos()
        sensores_remotos_mfl = arcpy.MakeFeatureLayer_management(sensores_remotos.path, 'mfl_sr')
        arcpy.SelectLayerByLocation_management(sensores_remotos_mfl, "INTERSECT", self.dep.path, "#", "NEW_SELECTION", "NOT_INVERT")
        fc_download = arcpy.CopyFeatures_management(sensores_remotos_mfl, os.path.join(self.scratchGDB, 'sr_copy'))
        sr_clip_for_region = arcpy.Clip_analysis(fc_download, self.dep.path, os.path.join(self.scratchGDB, 'sr_clip'))
        with arcpy.da.UpdateCursor(sr_clip_for_region, [sensores_remotos.tipo]) as cursor_uc:
            for i in cursor_uc:
                arcpy.AddMessage(i[0])
                if i[0] == 'Arcillas':
                    i[0] = i[0].lower()
                elif 'xidos y Arcillas' in i[0]:
                    i[0] = 'arcillas - oxidos'
                else:
                    i[0] = 'oxidos'
                cursor_uc.updateRow(i)
            del cursor_uc
        arcpy.Append_management(sr_clip_for_region, self.sensores.path, "NO_TEST")
        arcpy.Append_management(sr_clip_for_region, self.sensores_rm.path, "NO_TEST")
        self.features.append(self.sensores.path)
        
    def update_config(self):
        config = tb_config(self.path_out_geodatabase)
        cursor = arcpy.da.InsertCursor(config.path, [config.region, config.zona, config.ubicacion])
        cursor.insertRow((self.nm_depa, int('327' + str(self.zone)), config.path))
        del cursor
    
    def add_feature_to_toc(self):
        mxd = arcpy.mapping.MapDocument("CURRENT")
        df = mxd.activeDataFrame
        for i in self.features:
            lyr = arcpy.mapping.Layer(i)
            arcpy.mapping.AddLayer(df, lyr)

    def main(self):
        arcpy.Copy_management(self.path_inp_geodatabase, self.path_out_geodatabase)
        pkg.set_config_param(9, self.path_out_geodatabase, iscommit=True)
        self.need_features()    # pmm_gpo_concmin == rmi_gpo_concmin (solo debe existir una sola capa)
        self.limit_region()     # hace una copia del sde a la base de datos local
        self.catastro_minero('metalico')    # Obtiene el catastro minero de la EGDB y la envia a la base local
        self.catastro_minero('no metalico')    # Obtiene el catastro minero de la EGDB y la envia a la base local
        self.vias()             # Obtiene la informacion de vias de la EGDB, realiza un pre tratamiento y la envia la base local
        self.sustancias()       # Obtiene la informacion de sustancias de la EGDB, realiza un pre tratamiento y la envia la base local
        self.sensores_remotos()
        self.update_config()    # Actualiza la tabla config
        self.add_feature_to_toc() # Agrega todos los layers al mapa
        self.data = {'cd_depa': self.cd_depa, 'nm_depa': self.nm_depa, 'zona': self.zone}


if __name__ == "__main__":
    response = dict()
    response['status'] = 1
    response['message'] = 'success'
    try:
        path = arcpy.GetParameterAsText(0)
        cd_depa = arcpy.GetParameterAsText(1)
        poo = MakeGdb(path, cd_depa)
        poo.main()
        response['data'] = poo.data
    except Exception as e:
        response['status'] = 0
        response['message'] = e.message
    finally:
        response = json.dumps(response, encoding='windows-1252', ensure_ascii=False)
        arcpy.SetParameterAsText(2, response)