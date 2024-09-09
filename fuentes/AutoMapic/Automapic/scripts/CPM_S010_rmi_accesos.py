import messages_aut as msg
from CPM_S02_model import *
import arcpy
import packages_aut as pkg
import settings_aut as st

arcpy.env.overwriteOutput = True

class Accesos(object):
    def __init__(self, *args):
        self.ws = args[0]
        self.fc = args[1]
        self.tipo = args[2]
        self.tb_acceso = rmi_tb_accesos(self.ws)
        self.fc_acceso = rmi_gpl_accesos(self.ws)
        self.fields = {self.fc_acceso.tipo: self.tipo}

    def check_geodatabase(self):
        desc = arcpy.Describe(self.ws)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def check_exist_feature(self):
        if not arcpy.Exists(self.fc_acceso.path):
            raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST)

    def check_info(self):
        cursor_sc_domain = arcpy.da.SearchCursor(self.tb_acceso.path, [self.tb_acceso.tipo])
        self.domain = map(lambda i: i[0], cursor_sc_domain)
        cursor_sc_info = arcpy.da.SearchCursor(self.fc, ['OID@', self.tipo])

        errors = list()
        for row in cursor_sc_info:
            if row[1]:
                if row[1].lower() not in self.domain or x[-1] in ('', ' '):
                    errors.append(row)
                errors.append(row)
        
        if len(errors) > 0:
            for err in errors:
                arcpy.AddWarning('\t\tError: FID: %s  |  %s  |  %s' % (err[0], err[1], err[-1]))
            raise RuntimeError(msg.error_info)


    def load_data(self):
        copy = arcpy.CopyFeatures_management(self.fc, "in_memory\\accesos")

        arcpy.CalculateField_management(copy, self.tipo, '!{}!.lower()'.format(self.tipo), 'PYTHON_9.3', '#')

        arcpy.DeleteRows_management(self.fc_acceso.path)

        for k, v in self.fields.items():
            arcpy.AlterField_management(copy, v, k)

        arcpy.Append_management(copy, self.fc_acceso.path, "NO_TEST")

    def main(self):
        arcpy.AddMessage(msg.init_process)
        # self.get_domain()
        arcpy.AddMessage(msg.check_gdb)
        self.check_geodatabase()
        self.check_exist_feature()
        arcpy.AddMessage(msg.check_info)
        self.check_info()
        arcpy.AddMessage(msg.send_database)
        self.load_data()
        arcpy.AddMessage(msg.end_process)


if __name__ == '__main__':
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        fc = arcpy.GetParameterAsText(0)
        tipo = arcpy.GetParameterAsText(1)

        poo = Accesos(geodatabase, fc, tipo)
        poo.main()
        arcpy.SetParameterAsText(2, poo.fc_acceso.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
