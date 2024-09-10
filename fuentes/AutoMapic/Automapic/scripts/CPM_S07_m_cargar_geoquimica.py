from CPM_S02_model import *
import messages_aut as msg
import arcpy
import packages_aut as pkg
import settings_aut as st

arcpy.env.overwriteOutput = True


class Geoquimica(object):
    """
    Clase que contiene el procesamiento para el tratamiento
    de la variable geoquimica
    """
    def __init__(self, geodatabase, raster):
        self.geodatabase = geodatabase
        self.raster = raster
        self.raster_dataset = pmm_ras_geoquimica(geodatabase)

    def check_geodatabase(self):
        desc = arcpy.Describe(self.geodatabase)
        if not desc.datatype.lower() == 'workspace':
            raise RuntimeError(msg._CPM_ERROR_GDB_TYPE)

    def process(self):
        """
        Enviando el raster ingresado al File Geodatabase
        :return:
        """
        arcpy.AddMessage(msg._CPM_CHECK_SA_EXTENSION_AVAILABLE)
        license = arcpy.CheckExtension('spatial')
        if license.lower() != 'available':
            raise RuntimeError('\tError: %s' % license)

        arcpy.AddMessage(msg._CPM_SEND_DATA_TO_GEODATABASE)
        arcpy.CheckOutExtension('spatial')
        geoquimica = arcpy.sa.Fill(self.raster)
        geoquimica.save(self.raster_dataset.path)
        arcpy.CheckInExtension('spatial')

    def main(self):
        """
        Funcion principal del proceso
        :return:
        """
        try:
            arcpy.AddMessage(msg._CPM_INIT_PROCESS)
            arcpy.AddMessage(msg._CPM_CHECK_GEODATABASE)
            self.check_geodatabase()
            self.process()
            arcpy.AddMessage(msg._CPM_END_PROCESS)
        except Exception as e:
            arcpy.AddError(e.message)


if __name__ == "__main__":
    try:
        geodatabase = pkg.get_config_param_value(st._ENV_GDB_PATH_CPM, one=True)
        raster = arcpy.GetParameterAsText(0)
        poo = Geoquimica(geodatabase, raster)
        poo.main()
        arcpy.SetParameterAsText(1, poo.raster_dataset.path)
    except Exception as e:
        arcpy.AddError('\n\t%s\n' % e.message)
