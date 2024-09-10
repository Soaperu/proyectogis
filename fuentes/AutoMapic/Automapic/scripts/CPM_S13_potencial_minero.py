# -*- coding: utf-8 -*-
from CPM_S02_model import *
import messages_aut as msg
import packages_aut as pkg
import arcpy
import settings_aut as st

arcpy.env.overwriteOutput = True

class PotencialMinero(object):
    def __init__(self, lambda_1, lambda_2, nivel):
        
        self.lambda_1 = lambda_1
        self.lambda_2 = lambda_2
        self.nivel = nivel
        self.ws = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        # pm_metalico = arcpy.GetParameterAsText(1)
        # pm_no_metalico = arcpy.GetParameterAsText(2)

        self.config = tb_config(self.ws)

        # pm_factor = tb_factor(ws)
        self.r_potencial_no_metalico = ras_potencial_minero_no_metalico(self.ws)
        self.r_potencial_metalico = ras_potencial_minero_metalico(self.ws)
        self.r_potencial_minero = ras_potencial_minero(self.ws)

    def mining_potential(self):
        arcpy.AddMessage(msg._CPM_EVALUATE_PM)
        arcpy.CheckOutExtension("spatial")

        # Determinando areas con mayor potencial minero metalico
        r_metalico = arcpy.sa.Con(arcpy.sa.Raster(self.r_potencial_metalico.path), 1, 0, "VALUE >= {}".format(self.nivel))
        r_metalico_path = os.path.join(arcpy.env.scratchGDB, 'metalico')
        r_metalico.save(r_metalico_path)

        arcpy.BuildRasterAttributeTable_management(r_metalico_path, "Overwrite")
        raster_info = list()
        # values_metalico = dict()
        with arcpy.da.SearchCursor(r_metalico_path, ['VALUE', 'COUNT']) as rows:
            for row in rows:
                if row[0] == 1:
                    raster_info.append(row[1])
                # values_metalico[row[0]] = row[1]
        # arcpy.AddMessage(values_metalico)
        if raster_info == []:
            raster_info.append(0)

        # Determinando areas con mayor potencial minero no metalico
        r_no_metalico = arcpy.sa.Con(arcpy.sa.Raster(self.r_potencial_no_metalico.path), 1, 0, "VALUE >= {}".format(self.nivel))
        r_no_metalico_path = os.path.join(arcpy.env.scratchGDB, 'no_metalico')
        r_no_metalico.save(r_no_metalico_path)

        arcpy.BuildRasterAttributeTable_management(r_no_metalico_path, "Overwrite")
        # values_no_metalico = dict()
        with arcpy.da.SearchCursor(r_no_metalico_path, ['VALUE', 'COUNT']) as rows:
            for row in rows:
                if row[0] == 1:
                    raster_info.append(row[1])
        # arcpy.AddMessage(values_no_metalico)
        if len(raster_info) == 1:
            raster_info.append(0)
        

        # arcpy.AddMessage(raster_info)

        # Determinar area de predominancia metalica o no metalica
        arcpy.AddMessage(msg._CPM_CHECK_PREDOMINANT_AREA)
        if sum(raster_info) == 0:
            raise RuntimeError(msg._CPM_ERROR_LEVEL_IS_VERY_SMALL)
        
        idx = raster_info.index(max(raster_info))

        factor_metalico = self.lambda_1 if idx == 0 else self.lambda_2
        factor_no_metalico = self.lambda_1 if idx == 1 else self.lambda_2

        message_predom = msg._CPM_PREDOMINANT_METALIC if idx == 0 else msg._CPM_PREDOMINANT_NO_METALIC
        arcpy.AddMessage(message_predom)
    
        product_raster = (arcpy.sa.Raster(self.r_potencial_metalico.path) * factor_metalico) + (arcpy.sa.Raster(self.r_potencial_no_metalico.path) * factor_no_metalico)
        product_raster.save(self.r_potencial_minero.path)
        arcpy.CheckInExtension("spatial")

    def main(self):
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        self.mining_potential()
        arcpy.AddMessage(msg._CPM_END_PROCESS)
        arcpy.SetParameterAsText(3, self.r_potencial_minero.path)

if __name__ == "__main__":

    try:
        lambda_1 = arcpy.GetParameter(0)
        lambda_2 = arcpy.GetParameter(1)
        nivel = arcpy.GetParameter(2)
        poo = PotencialMinero(lambda_1, lambda_2, nivel)
        poo.main()
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
