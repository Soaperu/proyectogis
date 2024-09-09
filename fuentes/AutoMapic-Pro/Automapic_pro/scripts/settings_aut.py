import os
#import packages_aut as pkg

"""
This file contains the settings for the automapic module.
"""


__author__ = 'Julio Christian Cruz Fernandez'
__copyright__ = 'INGEMMET 2024'
__credits__ = ['Julio Cruz F.', 'Jorge Yupanqui H.']
__version__ = '1.0.1'
__maintainer__ = 'Julio Cruz F.'
__mail__ = 'proyectososi10@ingemmet.gob.pe'
__title__ = 'AutomapicPro'
# __status__ = 'Development'
__status__ = 'Production'


_BASE_DIR = os.path.dirname(__file__)
_BDGEOCAT_NAME = 'bdgeocat.sde'
_BDGEOCAT_HUAWEI_NAME = 'bdgeocat_huawei.sde'
_BDGEOCAT_SDE = os.path.join(_BASE_DIR, _BDGEOCAT_NAME)
_BDGEOCAT_HUAWEI_SDE = os.path.join(_BASE_DIR, _BDGEOCAT_HUAWEI_NAME)

#File Servers
_SERVER_LAYER_PRO = r'\\srvfs01\bdgeocientifica$\LAYER_PRO'


# GENERAL FIELDS

_CODE_FIELD = 'code'
_ZONA_FIELD = 'zona'
_NOMBRE_FIELD = 'nombre'
_FILA_FIELD = 'fila'
_COLUMNA_FIELD = 'columna'
_CUADRANTE_FIELD = 'cuadrante'
_RUMBO_FIELD = 'rumbo'

# ZONAS GEOGRAFICAS
_ZONAS_GEOGRAFICAS = [17, 18, 19]
_EPSG_W17S = 32717
_EPSG_W18S = 32718
_EPSG_W19S = 32719


# Mapa Geologico
# PATHS

_CODHOJA_FIELD = 'CODHOJA'
_POG_FIELD = 'POG'
_AZIMUT_FIELD = 'AZIMUT'
_CODI_FIELD = 'CODI'
_BUZAMIENTO_FIELD = 'BUZAMIENTO'
_BZ_REAL_FIELD = 'BZ_REAL'
_A_BZ_SECC_FIELD = "A_BZ_SECC"
_BZ_SEC_FIELD = "BZ_SEC" 
_P_SECC_FIELD = "P_SECC"
_BZ_APAR_DD_FIELD = "BZ_APAR_DD"
_BZ_APAR_FIELD = "BZ_APAR"
_ETIQUETA_FIELD = "ETIQUETA"
_ETIQUETA_FUNCTION_MARK_ALTITUD1 = """def FindLabel ( [ETIQUETA] ):
                                        lbl= int([ETIQUETA])
                                        if lbl % 1000 {} 0 or lbl== {} or lbl== {}:
                                            return lbl
                                        else:
                                            return"""""
_ETIQUETA_FUNCTION_MARK_ALTITUD2= """def FindLabel ([ETIQUETA]):
                                        return [ETIQUETA]"""
_RAS_PRINCI= "RASGO_PRIN"
_NOMBRE= "NOMBRE"

_GDB_PATH_MG = r"\\srvfs01\bdgeocientifica$\Addins_Geoprocesos\esriaddin\dgr\geodatabase\BASE_DGR_50K_V02.gdb" #pkg.get_config_param_value('GDB_PATH_MG', one=True)


_CUADRICULAS_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS01_DATO_GEOGRAFICO\GPO_DG_HOJAS_50K')
_ULITO_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS06_GEOLOGIA_{}S\GPO_DGR_ULITO_{}S')
_POG_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS06_GEOLOGIA_{}S\GPT_DGR_POG_{}S')
_POG_MG_PERFIL_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS12_PERFIL\GPT_MG_PERFIL')
_GPL_MG_PERFIL_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS12_PERFIL\GPL_MG_PERFIL')
_TB_MG_BUZAMIENTO_APARENTE_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'TB_MG_BUZAMIENTO_APARENTE')
# _IGN_TOP_CERRO = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CERRO')
# _IGN_TOP_CORDILLERA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CORDILLERA')
# _IGN_TOP_LOMA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_LOMA')
# _IGN_TOP_NEVADO = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CERRO')
# _IGN_HID_RIO_QUEBRADA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_HID_RIO_QUEBRADA')
# _DGR_ULITO_50K_ = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_INTEGRADA_50K\DATA_GIS.GPO_DGR_ULITO')
# _DGR_POG_50K = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_INTEGRADA_50K\DATA_GIS.GPT_DGR_POG')