import messages_aut as msg
from CPM_S02_model import *
import arcpy
import packages_aut as pkg

arcpy.env.overwriteOutput = True

class UnidGeo(object):
    def __init__(self, *args):
        self.tipo_pot = args[9]

        self.ws = args[0]
        self.fc = args[1]
        self.codi = args[2]
        self.name = args[3]
        self.desc = args[4]
        self.unid = args[5]
        self.grade = args[6]
        self.value = args[7]
        self.condition = args[8]
        self.fc_unidgeo =  pmm_gpo_ugeol(self.ws) if self.tipo_pot == 'metalico' else rmi_gpo_litologia(self.ws)       # Feature class de la gdb local que es el contenedor de la data de UG ingresada
        self.tb_grade = tb_nivel(self.ws)               # Tabla maestra que tiene la referencia del codigo y grad
        self.tb_cond = tb_ugeol_condicion(self.ws)  # tabla que contiene la descripcion y codigo de las unidades geologicas para metalico y no metalico
        self.fields = {
            self.fc_unidgeo.codi: self.codi,
            self.fc_unidgeo.nombre: self.name,
            self.fc_unidgeo.descripcion: self.desc,
            self.fc_unidgeo.unidad: self.unid,
            self.fc_unidgeo.grado: self.grade,
            self.fc_unidgeo.valor: self.value,
            self.fc_unidgeo.condicion: self.condition
        }

    def check_geodatabase(self):
        """
        Verifica si es una geodatabase
        """
        desc = arcpy.Describe(self.ws)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def check_exist_feature(self):
        if not arcpy.Exists(self.fc_unidgeo.path):
            raise RuntimeError(msg._CPM_FEATURE_NOT_EXIST.format(self.fc_unidgeo.name))

    def check_geometry_type(self):
        desc = arcpy.Describe(self.fc)
        if desc.shapeType.lower() != self.fc_unidgeo.shapeType:
            raise RuntimeError(msg._CPM_ERROR_SHAPETYPE_UG)
    
    def check_grade(self):
        # Obtiene el grado (muy bajo, bajo, medio, alto, muy alto)
        domain = [x[0] for x in arcpy.da.SearchCursor(self.tb_grade.path, [self.tb_grade.grado])]

        # Obtiene los valores de grado y condicion del featurelayer ingresado
        self.info = [x for x in arcpy.da.SearchCursor(self.fc, ['OID@', self.grade, self.condition])]

        # Revisa si el campo grado hace match con el el grado de la tabla maestra, verifica que el campo grado no sea nulo, falso o una cadena vacia
        errors = [x for x in self.info if x[1].lower() not in domain or x[1] in [None, False, '', ' ']]

        #  Si existen errores el proceso se interrumpe
        if len(errors) > 0:
            for x in errors:
                arcpy.AddWarning('\t\tError: %s  |  %s' % (x[0], x[1]))
            raise RuntimeError(msg._CPM_ERROR_DETECTEC)

    def check_condition(self):
        # query a la tabla maestra en funcion al tipo de potencial minero a calcular
        query = "{} = '{}'".format(self.tb_cond.tipo, self.tipo_pot)

        # Se obtiene todos los valores (metalotecto, no metalotecto (PMM) | litotecto, no litotecto (PMNM))
        domain = [x[0] for x in arcpy.da.SearchCursor(self.tb_cond.path, [self.tb_cond.descrip], query)]

        # Revisa si el campo grado hace match con el el condicion de la tabla maestra, verifica que el campo condicion no sea nulo, falso o una cadena vacia
        errors = [x for x in self.info if x[-1].lower() not in domain or x[-1] in [None, False, '', ' ']]
        
        #  Si existen errores el proceso se interrumpe
        if len(errors) > 0:
            for x in errors:
                arcpy.AddWarning('\t\tError: FID %s | %s' % (x[0], x[-1]))
            raise RuntimeError(msg._CPM_ERROR_DETECTEC)

    def load_data(self):
        copy = arcpy.CopyFeatures_management(self.fc, "in_memory\\unidadesGeologicas")

        region = pm_region(self.ws)
        copy = arcpy.Clip_analysis(copy, region.path, 'in_memory\\unidades_geologicas_region')

        # Transforma el grado y la condicion del featurelayer de entrada a minusculas
        with arcpy.da.UpdateCursor(copy, [self.grade, self.condition]) as cursorUC:
            for row in cursorUC:
                row[0], row[1] = row[0].lower(), row[1].lower()
                cursorUC.updateRow(row)
        del cursorUC

        # Elimina todos los registros de la capa GDB local
        arcpy.DeleteRows_management(self.fc_unidgeo.path)

        # Elimina y crea los campos correctos
        for k, v in self.fields.items():
            if v:
                arcpy.AlterField_management(copy, v, k)
        
        # Realiza el append al featureclas de la GDB local
        arcpy.Append_management(copy, self.fc_unidgeo.path, "NO_TEST")

    def main(self):
        arcpy.AddMessage(msg._CPM_INIT_PROCESS)
        arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
        self.check_geodatabase()
        self.check_exist_feature()
        arcpy.AddMessage(msg._CPM_CHECK_DATA)
        self.check_grade()
        self.check_condition()
        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        self.load_data()
        arcpy.AddMessage(msg._CPM_END_PROCESS)


if __name__ == '__main__':
    try:

        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        # ws = arcpy.GetParameterAsText(0)
        fc = arcpy.GetParameterAsText(0)
        codi = arcpy.GetParameterAsText(1)
        name = arcpy.GetParameterAsText(2)
        desc = arcpy.GetParameterAsText(3)
        unid = arcpy.GetParameterAsText(4)
        grade = arcpy.GetParameterAsText(5)
        value = arcpy.GetParameterAsText(6)
        condition = arcpy.GetParameterAsText(7)
        tipo = arcpy.GetParameterAsText(8)

        poo = UnidGeo(geodatabase, fc, codi, name, desc, unid, grade, value, condition, tipo)
        poo.main()
        arcpy.SetParameterAsText(9, poo.fc_unidgeo.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
