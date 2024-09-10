import messages_aut as msg
from CPM_S02_model import *
import arcpy
import packages_aut as pkg
import settings_aut as st

arcpy.env.overwriteOutput = True


class FallGeo(object):
    def __init__(self, *args):
        self.ws = args[0]
        self.fc = args[1]
        self.codi = args[2]
        self.desc = args[3]
        self.dist = args[4]
        self.tb_fallgeo = pmm_tb_fallageol(self.ws)
        self.fc_fallgeo = pmm_gpl_fallageol(self.ws)
        self.fields = {
            self.fc_fallgeo.codi: self.codi,
            self.fc_fallgeo.descripcion: self.desc,
            self.fc_fallgeo.distancia: self.dist
        }

    def get_domain(self):
        cursor_sc = arcpy.da.SearchCursor(self.tb_fallgeo.path, [self.tb_fallgeo.nombre_falla])
        self.domain = map(lambda i: i[0].lower(), cursor_sc)

    def check_geodatabase(self):
        desc = arcpy.Describe(self.ws)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def check_exist_feature(self):
        if not arcpy.Exists(self.fc_fallgeo.path):
            raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST.format(self.fc_fallgeo.name))
    
    def check_geometry_type(self):
        desc = arcpy.Describe(self.fc)
        if desc.shapeType.lower() != self.fc_fallgeo.shapeType:
            raise RuntimeError(msg._CPM_ERROR_SHAPETYPE_FG)

    def check_info(self):
        self.fc_fallgeo_copy = arcpy.CopyFeatures_management(self.fc, "in_memory\\fallasGeologicas")
        if not self.dist:
            arcpy.AddField_management(self.fc_fallgeo_copy, self.fc_fallgeo.distancia, "DOUBLE")
            arcpy.CalculateField_management(self.fc_fallgeo_copy, self.fc_fallgeo.distancia, "!shape.length@meters!", "PYTHON_9.3")
            self.fields[self.fc_fallgeo.distancia] = self.fc_fallgeo.distancia
        
        errors = list()

        cursor_sc = arcpy.da.SearchCursor(self.fc_fallgeo_copy, ['OID@', self.desc, self.fields[self.fc_fallgeo.distancia]])
        for row in cursor_sc:
            if row[1].lower() not in self.domain or row[-1] in (None, False, '', ' '):
                error_msg = '\t\tError: FID: {}  |  {}  |  {}'.format(row[0], row[1], row[-1])
                errors.append(error_msg)
        if len(errors):
            for err in errors:
                arcpy.AddWarning(err)
            arcpy.RuntimeError(msg._CPM_ERROR_DETECTEC)

    def load_data(self):
        with arcpy.da.UpdateCursor(self.fc_fallgeo_copy, [self.desc]) as cursorUC:
            for row in cursorUC:
                row[0] = row[0].lower()
                cursorUC.updateRow(row)
        del cursorUC

        arcpy.DeleteRows_management(self.fc_fallgeo.path)



        for k, v in self.fields.items():
            arcpy.AlterField_management(self.fc_fallgeo_copy, v, k)

        arcpy.Append_management(self.fc_fallgeo_copy, self.fc_fallgeo.path, "NO_TEST")

    def main(self):
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        self.get_domain()
        arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
        self.check_geodatabase()
        self.check_exist_feature()
        self.check_geometry_type()
        arcpy.AddMessage(msg._CPM_CHECK_DATA)
        self.check_info()
        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        self.load_data()
        arcpy.AddMessage(msg._CPM_END_PROCESS)


if __name__ == '__main__':
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        fc = arcpy.GetParameterAsText(0)
        codi = arcpy.GetParameterAsText(1)
        desc = arcpy.GetParameterAsText(2)
        dist = arcpy.GetParameterAsText(3)

        poo = FallGeo(geodatabase, fc, codi, desc, dist)
        poo.main()
        arcpy.SetParameterAsText(4, poo.fc_fallgeo.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
