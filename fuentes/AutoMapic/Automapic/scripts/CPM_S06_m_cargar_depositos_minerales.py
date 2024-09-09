from CPM_S02_model import *
# from function import *
import arcpy
import messages_aut as msg
import settings_aut as st
import packages_aut as pkg

arcpy.env.overwriteOutput = True


class DepoMin(object):
    def __init__(self, *args):
        self.ws = args[0]
        self.fc = args[1]
        self.unid = args[2]
        self.dimension = args[3]
        self.grade = args[4]
        self.value = args[5]
        self.fc_depomin = pmm_gpo_depmineral(self.ws)
        self.tb_grade = tb_nivel(self.ws)
        self.fields = {
            self.fc_depomin.grado: self.grade,
            self.fc_depomin.unidad: self.unid,
            self.fc_depomin.dimension: self.dimension,
            self.fc_depomin.valor: self.value
        }

    def check_geodatabase(self):
        desc = arcpy.Describe(self.ws)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def check_exist_feature(self):
        if not arcpy.Exists(self.fc_depomin.path):
            raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST.format(self.fc_depomin.name))
    
    def check_geometry_type(self):
        desc = arcpy.Describe(self.fc)
        if desc.shapeType.lower() != self.fc_depomin.shapeType:
            raise RuntimeError(msg._CPM_ERROR_SHAPETYPE_DM)

    def check_grade(self):
        domain = [x[0] for x in arcpy.da.SearchCursor(self.tb_grade.path, [self.tb_grade.grado])]
        info = [x for x in arcpy.da.SearchCursor(self.fc, ['OID@', self.grade])]
        errors = [x for x in info if x[1].lower() not in domain]
        if len(errors) > 0:
            for x in errors:
                arcpy.AddWarning('\t\tError: %s  |  %s' % (x[0], x[1]))
            raise RuntimeError(msg._CPM_ERROR_DETECTEC)

    def load_data(self):
        copy = arcpy.CopyFeatures_management(self.fc, "in_memory\\depositosMinerales")
        fields_copy = map(lambda i: i.name, arcpy.ListFields(copy))
        with arcpy.da.UpdateCursor(copy, [self.grade]) as cursorUC:
            for row in cursorUC:
                row[0] = row[0].lower()
                cursorUC.updateRow(row)
        del cursorUC
        arcpy.DeleteRows_management(self.fc_depomin.path)
        for k, v in self.fields.items():
            if v:
                arcpy.AlterField_management(copy, v, k)
                continue
            if k in fields_copy:
                arcpy.DeleteField_management(copy, k)
        arcpy.Append_management(copy, self.fc_depomin.path, "NO_TEST")

    def main(self):
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
        self.check_geodatabase()
        self.check_exist_feature()
        self.check_geometry_type()
        arcpy.AddMessage(msg._CPM_CHECK_DATA)
        self.check_grade()
        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        self.load_data()
        arcpy.AddMessage(msg._CPM_END_PROCESS)


if __name__ == '__main__':
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        fc = arcpy.GetParameterAsText(0)
        unid = arcpy.GetParameterAsText(1)
        dimension = arcpy.GetParameterAsText(2)
        grade = arcpy.GetParameterAsText(3)
        value = arcpy.GetParameterAsText(4)

        poo = DepoMin(geodatabase, fc, unid, dimension, grade, value)
        poo.main()
        arcpy.SetParameterAsText(5, poo.fc_depomin.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
