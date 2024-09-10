import messages_aut as msg
from CPM_S02_model import *
import arcpy
import settings_aut as st
import packages_aut as pkg

arcpy.env.overwriteOutput = True


class Sustancias(object):
    def __init__(self, *args):
        self.ws = args[0]
        self.fc = args[1]
        self.sust = args[2]
        # self.grado = args[3]
        # self.valor = args[4]
        self.tb_sust = rmi_tb_sustancias(self.ws)
        self.fc_sust = rmi_gpt_sustancias(self.ws)
        self.fields = {self.fc_sust.sustancia: self.sust}
        # self.fields = {self.fc_sust.grado: self.grado, self.fc_sust.valor: self.valor}

    # def get_domain(self):
    #     self.domain = [x[0] for x in arcpy.da.SearchCursor(
    #         self.tb_sust.path, [self.tb_sust.sustancia]
    #     )]

    def check_geodatabase(self):
        desc = arcpy.Describe(self.ws)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def check_exist_feature(self):
        if not arcpy.Exists(self.fc_sust.path):
            raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST.format(self.fc_sust.name))

    def check_info(self):
        cursor_sc_domain = arcpy.da.SearchCursor(self.tb_sust.path, [self.tb_sust.sustancia])
        self.domain = map(lambda i: i[0], cursor_sc_domain)

        cursor_sc_info = arcpy.da.SearchCursor(self.fc, ['OID@', self.sust])
        errors = filter(lambda i: i[1].lower() not in self.domain, cursor_sc_info)

        if len(errors) > 0:
            for x in errors:
                arcpy.AddWarning('\t\tError: FID: {}  |  {} '.format(x[0], x[1]))
            raise RuntimeError(msg._CPM_ERROR_DETECTEC)

    def load_data(self):
        copy = arcpy.CopyFeatures_management(self.fc, "in_memory\\sustancias")
        arcpy.CalculateField_management(copy, self.sust, '!{}!.lower()'.format(self.sust), 'PYTHON_9.3', '#')

        arcpy.DeleteRows_management(self.fc_sust.path)

        for k, v in self.fields.items():
            arcpy.AlterField_management(copy, v, k)

        arcpy.Append_management(copy, self.fc_sust.path, "NO_TEST")

    def main(self):
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        # self.get_domain()
        arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
        self.check_geodatabase()
        self.check_exist_feature()
        arcpy.AddMessage(msg._CPM_CHECK_DATA)
        self.check_info()
        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        self.load_data()
        arcpy.AddMessage(msg._CPM_END_PROCESS)

if __name__ == '__main__':
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        fc = arcpy.GetParameterAsText(0)
        sust = arcpy.GetParameterAsText(1)
        # grado = arcpy.GetParameterAsText(2)
        # valor = arcpy.GetParameterAsText(3)

        poo = Sustancias(geodatabase, fc, sust)
        poo.main()
        arcpy.SetParameterAsText(2, poo.fc_sust.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)