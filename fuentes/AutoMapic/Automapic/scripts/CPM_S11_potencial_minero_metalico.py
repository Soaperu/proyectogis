# -*- coding: utf-8 -*-

from CPM_S02_model import *
import messages_aut as msg
import arcpy
import packages_aut as pkg
import settings_aut as st
import operator
import re

arcpy.env.overwriteOutput = True


class PotencialMineroMetalico(object):
    def __init__(self, geodatabase, pixel):
        self.tipo_pot = 'metalico'

        # GDB local descargada
        self.ws = geodatabase
        # Valor del pixel a utilizar para el procesamiento
        self.pixel = pixel

        # Insumos
        self.config = tb_config(self.ws)
        self.limite = pm_region(self.ws)
        self.unidad_geologica = pmm_gpo_ugeol(self.ws)
        self.catastro_minero = pmm_gpo_concmin(self.ws)
        self.falla_geologica = pmm_gpl_fallageol(self.ws)
        self.deposito_mineral = pmm_gpo_depmineral(self.ws)
        self.sensor_remoto = pmm_gpo_sensores(self.ws)
        self.geoquimica = pmm_ras_geoquimica(self.ws)

        # Variables valoradas
        self.v_unidad_geologica = pmm_var_ugeol(self.ws)
        self.v_falla_geologica = pmm_var_fallageol(self.ws)
        self.v_catastro_minero = pmm_var_concmin(self.ws)
        self.v_deposito_mineral = pmm_var_depmineral(self.ws)
        self.v_sensor_remoto = pmm_var_sensores(self.ws)

        # Variables como raster
        self.r_unidad_geologica = pmm_ras_ugeol(self.ws)
        self.r_falla_geologica = pmm_ras_fallageol(self.ws)
        self.r_catastro_minero = pmm_ras_concmin(self.ws)
        self.r_deposito_mineral = pmm_ras_depmineral(self.ws)
        self.r_sensor_remoto = pmm_ras_sensores(self.ws)

        # Tabla de ponderacion
        self.pm_factor = pmm_tb_factor(self.ws)

        # Potencial minero metalico
        self.r_potencial_minero = ras_potencial_minero_metalico(self.ws)

        # Dominio estructural
        self.dominio_estructural = pm_dominio_estructural(self.ws)

        # Sistema de referencia
        self.src = [i[0] for i in arcpy.da.SearchCursor(self.config.path, [self.config.zona])][0]

        self.operations = {
            '<': operator.lt,
            '>': operator.gt,
            '<=': operator.le,
            '>=': operator.ge
        }

    def value_resources(self):
        license = arcpy.CheckExtension('spatial')
        if license != 'Available':
            raise RuntimeError(msg._CPM_ERROR_SA_EXTENSION_AVAILABLE)
    
    def pre_processing(self):
        self.dominio_estructural_fc = arcpy.Clip_analysis(self.dominio_estructural.path, self.limite.path, 'in_memory\\dominio_estructural')

    def pre_treatment(self, insumo, feature, raster):
        name = os.path.basename(insumo)

        if arcpy.GetCount_management(insumo).__str__() == '0':
            raise RuntimeError(msg._CPM_FEATURE_EMPTY_ROWS.format(name))

        arcpy.DeleteRows_management(feature)

        if arcpy.Exists(raster):
            arcpy.Delete_management(raster)
    
    def method_unidades_geologicas(self):
        """

        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_UNIDADES_GEOLOGICAS)
        self.pre_treatment(self.unidad_geologica.path, self.v_unidad_geologica.path, self.r_unidad_geologica.path)

        arcpy.AddMessage(msg._CPM_STORING_FEATURECLASS_INTO_GEODATABASE)
        arcpy.Append_management(self.unidad_geologica.path, self.v_unidad_geologica.path, 'NO_TEST')

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.unidad_geologica.path, self.unidad_geologica.valor, self.r_unidad_geologica.path, "CELL_CENTER", self.unidad_geologica.valor, self.pixel)

    def method_concesiones_mineras(self):
        """

        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_CONCESIONES_MINERAS)

        self.pre_treatment(self.catastro_minero.path, self.v_catastro_minero.path, self.r_catastro_minero.path)

        fields = [self.catastro_minero.leyenda, self.catastro_minero.naturaleza]

        dissolve = arcpy.Dissolve_management(self.catastro_minero.path, 'in_memory\\feature', fields, '#', 'MULTI_PART', 'DISSOLVE_LINES')

        unidad_geologica = arcpy.CopyFeatures_management(self.unidad_geologica.path, 'in_memory\\unidades_geologicas')

        # Eliminar los campos de grado y valor en la variable de litologia para evitar 
        # el cruce con los campos de grado y valor de la varibale concesiones mineras
        # arcpy.DeleteField_management(unidad_geologica, self.unidad_geologica.valor)
        # arcpy.DeleteField_management(unidad_geologica, self.unidad_geologica.grado)


        input_feature = [dissolve, unidad_geologica, self.dominio_estructural_fc]

        arcpy.AddMessage(msg._CPM_UNION_CONCESIONES_MINERAS)
        union = arcpy.Union_analysis(input_feature, 'in_memory\\union', 'ALL', '#', 'GAPS')
        arcpy.CalculateField_management(union, self.v_catastro_minero.valor, "0")
        arcpy.CalculateField_management(union, self.v_catastro_minero.grado, "0")

        fields = [self.catastro_minero.leyenda, self.unidad_geologica.condicion, self.dominio_estructural.dominio, self.v_catastro_minero.valor, self.v_catastro_minero.grado]

        cm_grado = pmm_tb_concmin_grado(self.ws)

        # query = "{} = '{}'".format(cm_grado.tipo, self.tipo_pot)
        grade = [i for i in arcpy.da.SearchCursor(cm_grado.path, fields)]

        arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)

        with arcpy.da.UpdateCursor(union, fields) as cursor:
            for i in cursor:
                leyenda = None if i[0] in (None, '', ' ') else i[0].lower()
                condicion = None if i[1] in (None, '', ' ') else i[1].lower()
                dominio = None if i[2] in (None, '', ' ') else i[2].lower()
                for r in grade:
                    if leyenda == r[0] and condicion == r[1] and dominio == r[2]:
                        i[3] = r[3]
                        i[4] = r[4]
                        break
                    else:
                        i[3] = 0
                        i[4] = 0
                cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        arcpy.Append_management(union, self.v_catastro_minero.path, 'NO_TEST')

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_catastro_minero.path, self.unidad_geologica.valor, self.r_catastro_minero.path, "CELL_CENTER", self.unidad_geologica.valor, self.pixel)


    def method_fallas_geologicas(self):
        arcpy.AddMessage(msg._CPM_VARIABLE_FALLAS_GEOLOGICAS)

        # Validaciones de existencia y cantidad de registros
        self.pre_treatment(self.falla_geologica.path, self.v_falla_geologica.path, self.r_falla_geologica.path)

        # Copia de featureClass para procesamiento
        feature = arcpy.CopyFeatures_management(self.falla_geologica.path, 'in_memory\\feature')

        # Se agrega el campo influencia a la capa de fallas
        arcpy.AddField_management(feature, self.v_falla_geologica.influencia, 'DOUBLE')

        fg_grado = pmm_tb_fallageol_grado(self.ws)
        fg_grado_fields = [fg_grado.dist_min, fg_grado.dist_max, fg_grado.influencia, fg_grado.dominio, fg_grado.grado, fg_grado.valor]

        cursor_sc_dominio = arcpy.da.SearchCursor(fg_grado.path, fg_grado_fields, "{} is not null".format(fg_grado.dominio))
        valoration_by_dominio = {i[3]: [i[4], i[5]] for i in cursor_sc_dominio}

        # Tratamiento de dominio estructural
        # Copia a dominio estructural
        dominio_estructural_fc_copy = arcpy.CopyFeatures_management(self.dominio_estructural_fc, os.path.join(arcpy.env.scratchGDB, 'dominio_estructural_fc_copy'))
        # Agregamos campo grado y valor a la copia del dominio estructural
        arcpy.AddField_management(dominio_estructural_fc_copy, self.v_falla_geologica.grado, 'TEXT')
        arcpy.AddField_management(dominio_estructural_fc_copy, self.v_falla_geologica.valor, 'DOUBLE')
        # Calculamos los campos grado y valor
        dominio_estructural_fc_copy_fields = [self.dominio_estructural.dominio, self.v_falla_geologica.grado, self.v_falla_geologica.valor]
        with arcpy.da.UpdateCursor(dominio_estructural_fc_copy, dominio_estructural_fc_copy_fields) as cursor_uc:
            for row in cursor_uc:
                if valoration_by_dominio.get(row[0]):
                    row[1] = valoration_by_dominio[row[0]][0]
                    row[2] = valoration_by_dominio[row[0]][1]
                    cursor_uc.updateRow(row)
            del cursor_uc
        del cursor_sc_dominio
        
        # Tratamiento de fallas geologicas
        cursor_sc_not_dominio = arcpy.da.SearchCursor(fg_grado.path, fg_grado_fields, "{} is null".format(fg_grado.dominio))
        cursor_sc_not_dominio = map(lambda i: i, cursor_sc_not_dominio)

        valoration_without_dominio = list()
        for i in cursor_sc_not_dominio:
            row = list()
            if i[0]:
                dist_min_string = i[0].replace(" ", '')
                dist_min = re.findall(r'\d+', dist_min_string)
                if not dist_min:
                    raise RuntimeError(msg._CPM_ERROR_DISTANCE_VALUE_NOT_NUMBER)
                dist_min_string = dist_min_string.replace(dist_min[0], '')
                dist_min = float(dist_min[0]) * 1000
                operator_sel_min = self.operations.get(dist_min_string)
                if operator_sel_min is None:
                    raise RuntimeError(msg._CPM_ERROR_OPERATOR_NOT_EXIST)
                row.extend([operator_sel_min, dist_min])
            if i[1]:
                dist_max_string = i[1].replace(" ", '')
                dist_max = re.findall(r'\d+', dist_max_string)
                if not dist_max:
                    raise RuntimeError(msg._CPM_ERROR_DISTANCE_VALUE_NOT_NUMBER)
                dist_max_string = dist_max_string.replace(dist_max[0], '')
                dist_max = float(dist_max[0]) * 1000
                operator_sel_max = self.operations.get(dist_max_string)
                if operator_sel_max is None:
                    raise RuntimeError(msg._CPM_ERROR_OPERATOR_NOT_EXIST)
                row.extend([operator_sel_max, dist_max])
            if not row:
                raise RuntimeError(msg._CPM_ERROR_VALUE_INCORRECT.format(fg_grado.name))
            row.extend([i[2]])
            valoration_without_dominio.append(row)

        fields = [self.falla_geologica.distancia, self.v_falla_geologica.influencia]
        with arcpy.da.UpdateCursor(feature, fields) as cursor_uc:
            for row in cursor_uc:
                for i in valoration_without_dominio:
                    if len(i) == 5:
                        op_min, v_min, op_max, v_max, inf = i
                        if op_min(row[0], v_min) and op_max(row[0], v_max): 
                            row[1] = inf * 1000
                    else:
                        op_min, v_min, inf = i
                        if op_min(row[0], v_min): 
                            row[1] = inf * 1000
                cursor_uc.updateRow(row)
            del cursor_uc

        dissolve = arcpy.Dissolve_management(feature, 'in_memory\\dissolve', self.v_falla_geologica.influencia, "#", 'MULTI_PART', 'DISSOLVE_LINES')

        arcpy.AddMessage(msg._CPM_CALCULATE_BUFFER_FALLAS_GEOLOGICAS)
        influencia = arcpy.Buffer_analysis(dissolve, 'in_memory\\buffer', self.v_falla_geologica.influencia)

        arcpy.Append_management(influencia, self.v_falla_geologica.path, 'NO_TEST')

        erase = arcpy.Erase_analysis(dominio_estructural_fc_copy, self.v_falla_geologica.path, 'in_memory\\erase')

        arcpy.AddMessage(msg._CPM_CONFIG_LIMITS_BY_REGION)
        arcpy.Append_management(erase, self.v_falla_geologica.path, 'NO_TEST')

        del fields[0]

        fields.append(self.v_falla_geologica.grado)
        fields.append(self.v_falla_geologica.valor)

        arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)
        with arcpy.da.UpdateCursor(self.v_falla_geologica.path, fields) as cursor:
            for i in cursor:
                if not i[1]:
                    for x in cursor_sc_not_dominio:
                        if i[0] == x[2] * 1000:
                            i[1], i[2] = x[4], x[5]
                            break
                    cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_falla_geologica.path, self.v_falla_geologica.valor,
                                         self.r_falla_geologica.path, "CELL_CENTER", self.v_falla_geologica.valor,
                                         self.pixel)

    def method_depositos_minerales(self):
        """

        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_DEPOSITOS_MINERALES)
        self.pre_treatment(self.deposito_mineral.path, self.v_deposito_mineral.path, self.r_deposito_mineral.path)
        arcpy.AddMessage(msg._CPM_STORING_FEATURECLASS_INTO_GEODATABASE)
        arcpy.Append_management(self.deposito_mineral.path, self.v_deposito_mineral.path, 'NO_TEST')
        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.deposito_mineral.path, self.unidad_geologica.valor,
                                         self.r_deposito_mineral.path, "CELL_CENTER", self.unidad_geologica.valor,
                                         self.pixel)

    def method_geo_quimica(self):
        """

        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_GEOQUIMICA)
        if arcpy.Exists(self.geoquimica.path):
            arcpy.AddMessage(msg._CPM_CHECK_GEOQUIMICA_RASTER_EXIST)
        else:
            raise RuntimeError(msg._CPM_ERROR_RASTER_GEOQUIMICA_NOT_EXIST)

    def method_sensores_remotos(self):
        arcpy.AddMessage(msg._CPM_VARIABLE_SENSORES_REMOTOS)
        self.pre_treatment(self.sensor_remoto.path, self.v_sensor_remoto.path, self.r_sensor_remoto.path)

        input_feature = [self.sensor_remoto.path, self.dominio_estructural_fc]

        arcpy.AddMessage(msg._CPM_UNION_CONCESIONES_MINERAS)
        union = arcpy.Union_analysis(input_feature, 'in_memory\\union', 'ALL', '#', 'GAPS')

        arcpy.AddField_management(union, self.v_sensor_remoto.grado, 'TEXT')
        arcpy.AddField_management(union, self.v_sensor_remoto.valor, 'DOUBLE')
        # arcpy.CalculateField_management(union, self.v_catastro_minero.valor, "0")
        # arcpy.CalculateField_management(union, self.v_catastro_minero.grado, "0")

        
        sr_grado = pmm_tb_sensores_grado(self.ws)

        fields = [sr_grado.condicion, sr_grado.grado, sr_grado.valor, sr_grado.dominio]
        grade = [i for i in arcpy.da.SearchCursor(sr_grado.path, fields)]

        del fields[0]
        fields.insert(0, self.sensor_remoto.tipo)

        arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)
        with arcpy.da.UpdateCursor(union, fields) as cursor:
            for i in cursor:
                cond = i[0]
                if i[0] in (None, '', ' '):
                    cond = None
                if cond:
                    cond = i[0].lower()
                for x in grade:
                    if cond == x[0] and i[-1] == x[-1]:
                        i[1], i[2] = x[1], x[2]
                        break
                cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_STORING_FEATURECLASS_INTO_GEODATABASE)
        arcpy.Append_management(union, self.v_sensor_remoto.path, 'NO_TEST')

        # erase = arcpy.Erase_analysis(self.limite.path, self.v_sensor_remoto.path, 'in_memory\\erase')

        # arcpy.AddMessage(msg._CPM_CONFIG_LIMITS_BY_REGION)
        # arcpy.Append_management(erase, self.v_sensor_remoto.path, 'NO_TEST')

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_sensor_remoto.path, sr_grado.valor, self.r_sensor_remoto.path,
                                         "CELL_CENTER", sr_grado.valor, self.pixel)

    def get_metalic_potential(self):
        arcpy.AddMessage(msg._CPM_EVAL_POTENCIAL_MINERO_METALICO)

        if arcpy.Exists(self.r_potencial_minero.path):
            arcpy.Delete_management(self.r_potencial_minero.path)

        arcpy.env.outputCoordinateSystem = 4326
        fields = [self.pm_factor.nom_ras, self.pm_factor.factor]

        arcpy.AddMessage(msg._CPM_ALGEBRA_MAPS)
        arcpy.CheckOutExtension("spatial")

        arcpy.AddMessage('\t   - Ponderacion:')
        values = list()
        for i in arcpy.da.SearchCursor(self.pm_factor.path, fields):
            if arcpy.Exists(os.path.join(self.ws, i[0])):
                values.append(arcpy.sa.Raster(os.path.join(self.ws, i[0])) * i[1])
                arcpy.AddMessage('\t     * {:<30} {:>10}'.format(i[0], i[1]))

        pm = sum(values)

        
        # for i in arcpy.da.SearchCursor(self.pm_factor.path, fields):
        #     if arcpy.Exists(os.path.join(self.ws, i[0])):
        #         arcpy.AddMessage('\t     * {:<30} {:>10}'.format(i[0], i[1]))

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        pm.save(self.r_potencial_minero.path)
        arcpy.CheckInExtension("spatial")

        arcpy.env.outputCoordinateSystem = self.src

        arcpy.SetParameterAsText(1, self.r_potencial_minero.path)

    def main(self):
        """

        :return:
        """
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        # try:
        self.value_resources()              # Revisa si existe una licencia disponible para ejecutar analisis espacial
        self.pre_processing()
        self.method_unidades_geologicas()   # 
        self.method_fallas_geologicas()
        self.method_concesiones_mineras()
        self.method_depositos_minerales()
        self.method_geo_quimica()
        self.method_sensores_remotos()
        self.get_metalic_potential()
        arcpy.AddMessage(msg._CPM_END_PROCESS)
        # except Exception as e:
        # arcpy.AddError('\n\t' + e.message + '\t')


if __name__ == '__main__':
    geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
    pixel = arcpy.GetParameterAsText(0)
    
    poo = PotencialMineroMetalico(geodatabase, pixel)
    arcpy.env.outputCoordinateSystem = poo.src
    poo.main()
