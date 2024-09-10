# -*- coding: utf-8 -*-

import arcpy
from CPM_S02_model import *
import messages_aut as msg
import settings_aut as st
import packages_aut as pkg
import operator
arcpy.env.overwriteOutput = True


class PotencialMineroNoMetalico(object):
    def __init__(self, geodatabase, pixel):
        self.tipo_pot = 'no metalico'

        # GDB local descargada
        self.ws = geodatabase
        # Valor del pixel a utilizar para el procesamiento
        self.pixel = pixel

        # Insumos
        self.config = tb_config(self.ws)
        self.limite = pm_region(self.ws)
        self.litologia = rmi_gpo_litologia(self.ws)
        self.accesos = rmi_gpl_accesos(self.ws)
        self.catastro_minero = rmi_gpo_concmin(self.ws)
        self.sensor_remoto = rmi_gpo_sensores(self.ws)
        self.sustancia = rmi_gpt_sustancias(self.ws)

        # Variables valoradas
        self.v_litologia = rmi_var_litologia(self.ws)
        self.v_accesos = rmi_var_accesos(self.ws)
        self.v_catastro_minero = rmi_var_concmin(self.ws)
        self.v_sensor_remoto = rmi_var_sensores(self.ws)
        self.v_sustancia = rmi_var_sustancias(self.ws)

        # Variables como raster
        self.r_accesos = rmi_ras_accesos(self.ws)
        self.r_litologia = rmi_ras_litologia(self.ws)
        self.r_catastro_minero = rmi_ras_concmin(self.ws)
        self.r_sensor_remoto = rmi_ras_sensores(self.ws)
        self.r_sustancia = rmi_ras_sustancias(self.ws)

        # Tabla de ponderacion
        self.pm_factor = rmi_tb_factor(self.ws)

        # Potencial minero no metalico
        self.r_potencial_minero = ras_potencial_minero_no_metalico(self.ws)

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
            raise RuntimeError(u'\n\tError de licencia Spatial Analyst\n')
    
    def pre_processing(self):
        self.dominio_estructural_fc = arcpy.Clip_analysis(self.dominio_estructural.path, self.limite.path, 'in_memory\\dominio_estructural')

    def pre_treatment(self, insumo, feature, raster):
        name = os.path.basename(insumo)

        if arcpy.GetCount_management(insumo).__str__() == '0':
            raise RuntimeError(msg._CPM_FEATURE_EMPTY_ROWS.format(name))

        arcpy.DeleteRows_management(feature)

        if arcpy.Exists(raster):
            arcpy.Delete_management(raster)

    def method_litologia(self):
        """
        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_UNIDADES_GEOLOGICAS_RMI)

        self.pre_treatment(self.litologia.path, self.v_litologia.path, self.r_litologia.path)

        arcpy.AddMessage(msg._CPM_STORING_FEATURECLASS_INTO_GEODATABASE)
        arcpy.Append_management(self.litologia.path, self.v_litologia.path, 'NO_TEST')

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.litologia.path, self.litologia.valor,
                                         self.r_litologia.path, "CELL_CENTER", self.litologia.valor,
                                         self.pixel)

    def method_concesiones_mineras(self):
        """
        :return:
        """
        arcpy.AddMessage(msg._CPM_VARIABLE_CONCESIONES_MINERAS)

        self.pre_treatment(self.catastro_minero.path, self.v_catastro_minero.path, self.r_catastro_minero.path)

        fields = [self.catastro_minero.leyenda, self.catastro_minero.naturaleza]

        dissolve = arcpy.Dissolve_management(self.catastro_minero.path, 'in_memory\\feature', fields, '#', 'MULTI_PART', 'DISSOLVE_LINES')

        input_feature = [dissolve, self.litologia.path, self.dominio_estructural_fc]

        arcpy.AddMessage(msg._CPM_UNION_CONCESIONES_MINERAS)
        union = arcpy.Union_analysis(input_feature, 'in_memory\\union', 'ALL', '#', 'GAPS')
        arcpy.CalculateField_management(union, self.v_catastro_minero.valor, "0")
        arcpy.CalculateField_management(union, self.v_catastro_minero.grado, "0")

        fields = [self.catastro_minero.leyenda, self.litologia.condicion, self.dominio_estructural.dominio, self.litologia.valor, self.litologia.grado, ]

        cm_grado = rmi_tb_concmin_grado(self.ws)

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
        arcpy.PolygonToRaster_conversion(self.v_catastro_minero.path, self.litologia.valor, self.r_catastro_minero.path, "CELL_CENTER", self.litologia.valor, self.pixel)


    def method_accesos(self):
        arcpy.AddMessage(msg._CPM_VARIABLE_ACCESOS)

        self.pre_treatment(self.accesos.path, self.v_accesos.path, self.r_accesos.path)

        feature = arcpy.CopyFeatures_management(self.accesos.path, 'in_memory\\feature')

        arcpy.AddField_management(feature, self.accesos.influencia, 'DOUBLE')

        fields = [self.accesos.tipo, self.accesos.influencia]

        tb_vias = rmi_tb_accesos(self.ws)

        fields_vias_tipo = [tb_vias.tipo, tb_vias.grado, tb_vias.influencia, tb_vias.valor]

        grade = [i for i in arcpy.da.SearchCursor(tb_vias.path, fields_vias_tipo)]

        arcpy.AddMessage(msg._CPM_BUFFER_ACCESOS)

        with arcpy.da.UpdateCursor(feature, fields) as cursor:
            for i in cursor:
                for x in grade:
                    if x[0] == i[0]:
                        i[1] = x[2]
                        break
                cursor.updateRow(i)
            del cursor

        dissolve = arcpy.Dissolve_management(feature, 'in_memory\\dissolve', self.v_accesos.influencia, "#",
                                             'MULTI_PART', 'DISSOLVE_LINES')

        arcpy.AddMessage(msg._CPM_CALCULATE_BUFFER_ACCESOS)
        influencia = arcpy.Buffer_analysis(dissolve, 'in_memory\\buffer', self.v_accesos.influencia)

        arcpy.Append_management(influencia, self.v_accesos.path, 'NO_TEST')

        erase = arcpy.Erase_analysis(self.limite.path, self.v_accesos.path, 'in_memory\\erase')

        arcpy.AddMessage(msg._CPM_CONFIG_LIMITS_BY_REGION)
        arcpy.Append_management(erase, self.v_accesos.path, 'NO_TEST')

        del fields[0]

        fields.append(self.v_accesos.grado)
        fields.append(self.v_accesos.valor)

        arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)
        valores = [x[0] for x in arcpy.da.SearchCursor(tb_vias.path, [tb_vias.valor])]
        with arcpy.da.UpdateCursor(self.v_accesos.path, fields) as cursor:
            for i in cursor:
                for x in grade:
                    if i[0] == x[2]:
                        i[1], i[2] = x[1], x[3]
                        break
                if i[0] == None:
                    i[2] = min(valores)
                cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_accesos.path, self.v_accesos.valor,
                                         self.r_accesos.path, "CELL_CENTER", self.v_accesos.valor,
                                         self.pixel)

    def method_sensores_remotos(self):
        arcpy.AddMessage(msg._CPM_VARIABLE_SENSORES_REMOTOS)
        self.pre_treatment(self.sensor_remoto.path, self.v_sensor_remoto.path, self.r_sensor_remoto.path)

        arcpy.AddMessage(msg._CPM_STORING_FEATURECLASS_INTO_GEODATABASE)
        arcpy.Append_management(self.sensor_remoto.path, self.v_sensor_remoto.path, 'NO_TEST')

        erase = arcpy.Erase_analysis(self.limite.path, self.v_sensor_remoto.path, 'in_memory\\erase')

        arcpy.AddMessage(msg._CPM_CONFIG_LIMITS_BY_REGION)
        arcpy.Append_management(erase, self.v_sensor_remoto.path, 'NO_TEST')

        sr_grado = pmm_tb_sensores_grado(self.ws)

        fields = [sr_grado.condicion, sr_grado.grado, sr_grado.valor]
        grade = [i for i in arcpy.da.SearchCursor(sr_grado.path, fields)]

        del fields[0]
        fields.insert(0, self.sensor_remoto.tipo)

        arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)
        with arcpy.da.UpdateCursor(self.v_sensor_remoto.path, fields) as cursor:
            for i in cursor:
                cond = i[0]
                if i[0] in (None, '', ' '):
                    cond = None
                if cond:
                    cond = i[0].lower()
                for x in grade:
                    if cond == x[0]:
                        i[1], i[2] = x[1], x[2]
                        break
                cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_sensor_remoto.path, sr_grado.valor, self.r_sensor_remoto.path,
                                         "CELL_CENTER", sr_grado.valor, self.pixel)

    def method_sustancias(self):
        arcpy.AddMessage(msg._CPM_VARIABLE_SUSTANCIAS)

        self.pre_treatment(self.sustancia.path, self.v_sustancia.path, self.r_sustancia.path)

        sust_grado = rmi_tb_sustancias(self.ws)
        fields_grado = [sust_grado.sustancia, sust_grado.influencia, sust_grado.dominio , sust_grado.grado, sust_grado.valor]

        cursor_sc_dominio = arcpy.da.SearchCursor(sust_grado.path, fields_grado, "{} is not null".format(sust_grado.dominio))
        valoration_by_dominio = {i[2]: [i[3], i[4]] for i in cursor_sc_dominio}

        # Tratamiento de dominio estructural
        # Copia a dominio estructural
        dominio_estructural_fc_copy = arcpy.CopyFeatures_management(self.dominio_estructural_fc, os.path.join(arcpy.env.scratchGDB, 'dominio_estructural_fc_copy'))
        # Agregamos campo grado y valor a la copia del dominio estructural
        arcpy.AddField_management(dominio_estructural_fc_copy, self.v_sustancia.grado, 'TEXT')
        arcpy.AddField_management(dominio_estructural_fc_copy, self.v_sustancia.valor, 'DOUBLE')
        # Calculamos los campos grado y valor
        dominio_estructural_fc_copy_fields = [self.dominio_estructural.dominio, self.v_sustancia.grado, self.v_sustancia.valor]
        with arcpy.da.UpdateCursor(dominio_estructural_fc_copy, dominio_estructural_fc_copy_fields) as cursor_uc:
            for row in cursor_uc:
                if valoration_by_dominio.get(row[0]):
                    row[1] = valoration_by_dominio[row[0]][0]
                    row[2] = valoration_by_dominio[row[0]][1]
                    cursor_uc.updateRow(row)
            del cursor_uc
        del cursor_sc_dominio



        feature = arcpy.CopyFeatures_management(self.sustancia.path, 'in_memory\\feature')
        arcpy.AddField_management(feature, self.v_sustancia.influencia, 'DOUBLE')
        arcpy.AddField_management(feature, self.v_sustancia.grado, 'TEXT')
        arcpy.AddField_management(feature, self.v_sustancia.valor, 'DOUBLE')

        grade = [i for i in arcpy.da.SearchCursor(sust_grado.path, fields_grado, "{} is null".format(sust_grado.dominio))]

        fields = [self.v_sustancia.sustancia, self.v_sustancia.influencia, self.v_sustancia.valor, self.v_sustancia.grado]

        arcpy.AddMessage(msg._CPM_BUFFER_SUSTANCIAS)

        with arcpy.da.UpdateCursor(feature, fields) as cursor:
            for i in cursor:
                for x in grade:
                    if i[0] == x[0]:
                        i[1] = x[1]
                        i[2] = x[4]
                        i[3] = x[3]
                cursor.updateRow(i)
            del cursor

        arcpy.AddMessage(msg._CPM_CALCULATE_BUFFER_SUSTANCIAS)
        influencia = arcpy.Buffer_analysis(feature, 'in_memory\\buffer', self.v_sustancia.influencia)

        arcpy.Append_management(influencia, self.v_sustancia.path, 'NO_TEST')

        erase = arcpy.Erase_analysis(dominio_estructural_fc_copy, self.v_sustancia.path, 'in_memory\\erase')

        arcpy.AddMessage(msg._CPM_CONFIG_LIMITS_BY_REGION)
        arcpy.Append_management(erase, self.v_sustancia.path, 'NO_TEST')

        # arcpy.AddMessage(msg._CPM_CALCULATE_GRADE_VALUE)
        # with arcpy.da.UpdateCursor(self.v_sustancia.path, fields) as cursor:
        #     for i in cursor:
        #         for x in grade:
        #             if i[0] == x[0]:
        #                 i[2] = x[2]
        #                 i[3] = x[3]
        #         if i[0] == None:
        #             i[2] = 1
        #         cursor.updateRow(i)
        #     del cursor

        arcpy.AddMessage(msg._CPM_STORING_RASTERDATASET_INTO_GEODATABASE)
        arcpy.PolygonToRaster_conversion(self.v_sustancia.path, self.v_sustancia.valor,
                                         self.r_sustancia.path, "CELL_CENTER", self.v_sustancia.valor,
                                         self.pixel)

    def get_no_metalic_potential(self):
        arcpy.AddMessage(msg._CPM_EVAL_POTENCIAL_MINERO_NO_METALICO)
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

        # arcpy.AddMessage('\t   - Ponderacion:')
        # for i in arcpy.da.SearchCursor(self.pm_factor.path, fields):
            # if arcpy.Exists(os.path.join(self.ws, i[0])):
                # arcpy.AddMessage('\t     * {:<30} {:>10}'.format(i[0], i[1]))

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
        self.value_resources()
        self.pre_processing()
        self.method_litologia()
        self.method_sustancias()
        self.method_concesiones_mineras()
        self.method_accesos()
        self.method_sensores_remotos()
        self.get_no_metalic_potential()
        arcpy.AddMessage(msg._CPM_END_PROCESS)
        # except Exception as e:
        #     arcpy.AddError('\n\t' + e.message + '\t')

if __name__ == '__main__':
    geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
    pixel = arcpy.GetParameterAsText(0)

    poo = PotencialMineroNoMetalico(geodatabase, pixel)
    arcpy.env.outputCoordinateSystem = poo.src
    poo.main()
