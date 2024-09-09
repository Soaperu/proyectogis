#!/usr/bin/env python
# -*- coding: utf-8 -*-
import os
import packages_aut as pkg

"""
This file contains the settings for the automapic module.
"""


__author__ = 'Daniel Fernando Aguado Huaccharaqui'
__copyright__ = 'INGEMMET 2021'
__credits__ = ['Daniel Aguado H.', 'Jorge Yupanqui H.', 'Julio Cruz F.']
__version__ = '1.0.1'
__maintainer__ = 'Daniel Aguado H.'
__mail__ = 'proyectososi10@ingemmet.gob.pe'
__title__ = 'Automapic'
# __status__ = 'Development'
__status__ = 'Production'

_CONNSTRING_BDGEOCAT = "publ_gis/publ_gis@bdgeocat"
_BASE_DIR = os.path.dirname(__file__)
_TEMP_FOLDER = pkg.get_config_param_value('TEMP_FOLDER', one=True)
# _TEMP_FOLDER = r'D:\daguado\neotectonica\temp'
_REQUIREMENTS_DIR = os.path.join(_BASE_DIR, 'require')

_IMG_DIR = os.path.join(_BASE_DIR, 'img')
_IMG_LOGO_INGEMMET = os.path.join(_IMG_DIR, 'logo_ingemmet.png')
_IMG_LOGO_INGEMMET_COMPLETE = os.path.join(_IMG_DIR, 'logoIngemmet.png')

_SRVFS_BDGEOCIENTIFICA_EVAL_MG = r'\\10.102.0.143\Aplicaciones\bdgeocientifica\dgr_evaluacion_mg'
_URL_BDGEOCIENTIFICA_EVAL_MG = 'https://geocatminapp.ingemmet.gob.pe/bdgeocientifica/dgr_evaluacion_mg'

_BDGEOCAT_NAME = 'bdgeocat_dev.sde' if __status__ == 'Development' else 'bdgeocat.sde'
_BDGEOCAT_HUAWEI_NAME = 'bdgeocat_huawei.sde'
_BDGEOCAT_SDE = os.path.join(_BASE_DIR, _BDGEOCAT_NAME)
_BDGEOCAT_HUAWEI_SDE = os.path.join(_BASE_DIR, _BDGEOCAT_HUAWEI_NAME)
# _BDGEOCAT_SDE_DEV = os.path.join(_BASE_DIR, 'bdgeocat_dev.sde')

_ZONAS_GEOGRAFICAS = [17, 18, 19]
_EPSG_W17S = 32717
_EPSG_W18S = 32718
_EPSG_W19S = 32719

_LAYERS_DIR = os.path.join(_BASE_DIR, 'layers')
_EXT_LAYER = '.lyr'     # Extension de archivos Layer

# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Plano topografico 25000

_GDB_DIR = pkg.get_config_param_value('GDB_PATH_PT', one=True)
_CUADRICULAS_PATH = os.path.join(_GDB_DIR if _GDB_DIR else '', 'PO_00_cuadriculas')
_MXD_DIR = os.path.join(_BASE_DIR, 'mxd')
_MXD_17 = os.path.join(_MXD_DIR, 'T0117.mxd')
_MXD_18 = os.path.join(_MXD_DIR, 'T0218.mxd')
_MXD_19 = os.path.join(_MXD_DIR, 'T0319.mxd')


# GENERAL FIELDS
_CODE_FIELD = 'code'
_ZONA_FIELD = 'zona'
_NOMBRE_FIELD = 'nombre'
_FILA_FIELD = 'fila'
_COLUMNA_FIELD = 'columna'
_CUADRANTE_FIELD = 'cuadrante'
_RUMBO_FIELD = 'rumbo'

_SCALE_MAPA_TOPOGRAFICO_25K = 25000

# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Mapas DGAR

# MXD
_MXD_PG_17 = os.path.join(_MXD_DIR, 'T01PG17.mxd')
_MXD_PG_18 = os.path.join(_MXD_DIR, 'T01PG18.mxd')
_MXD_PG_19 = os.path.join(_MXD_DIR, 'T01PG19.mxd')

# GENERAL FIELDS
_ID_AREA = 'id_area'

# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
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

# import arcpy
# arcpy.AddMessage('______________________________')
# arcpy.AddMessage(pkg.get_config_param_value('GDB_PATH_MG', one=True))
# arcpy.AddMessage('______________________________')
_GDB_PATH_MG = pkg.get_config_param_value('GDB_PATH_MG', one=True)
# _GDB_PATH_MG = r'D:\2022\OS_MAYO\E02_XXXXXXXX\mapa_geologico\data\MASTER_DGR_50K_MPAS INTEGRADOS_2021.gdb'

_CUADRICULAS_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS01_DATO_GEOGRAFICO\GPO_DG_HOJAS_50K')
_ULITO_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS06_GEOLOGIA_{}S\GPO_DGR_ULITO_{}S')
_POG_MG_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS06_GEOLOGIA_{}S\GPT_DGR_POG_{}S')
_POG_MG_PERFIL_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS12_PERFIL\GPT_MG_PERFIL')
_GPL_MG_PERFIL_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'DS12_PERFIL\GPL_MG_PERFIL')
_TB_MG_BUZAMIENTO_APARENTE_PATH = os.path.join(_GDB_PATH_MG if _GDB_PATH_MG else '', 'TB_MG_BUZAMIENTO_APARENTE')
_IGN_TOP_CERRO = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CERRO')
_IGN_TOP_CORDILLERA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CORDILLERA')
_IGN_TOP_LOMA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_LOMA')
_IGN_TOP_NEVADO = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_TOP_CERRO')
_IGN_HID_RIO_QUEBRADA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_HID_RIO_QUEBRADA')
_DGR_ULITO_50K_ = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_INTEGRADA_50K\DATA_GIS.GPO_DGR_ULITO')
_DGR_POG_50K = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_INTEGRADA_50K\DATA_GIS.GPT_DGR_POG')

#Layers
_LAYER_AREA_INTERES = 'gpo_area_interes'

# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# :Mapa HidroGeologico
# :PATHS
_GDB_PATH_HG = pkg.get_config_param_value('GDB_PATH_HG', one=True)
_PL_01_CUENCAS_HIDROGRAFICAS_PATH = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_DGA\DATA_GIS.GPO_HID_UNIDADES_HIDROGRAFICAS')
_TB_01_AUTOR_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'TB_01_autor')
_PO_01_FORMACION_HIDROGEOLOGICA_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'DS_01_hidrogeologia_{}s\PO_01_formacion_hidrogeologica_{}s')
_TB_01_UNIDAD_HIDROGEOLOGICA_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'TB_01_unidad_hidrogeologica')
_TB_01_LEYENDA_AUX_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'TB_01_leyenda_aux')
_PO_01_LEYENDA_DIVISIONES_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'PO_01_leyenda')
_PT_01_LEYENDA_ETIQUETAS_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', 'PT_01_leyenda_etiquetas')
_TB_01_LEYENDA_PATH = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', "TB_01_leyenda")
_TB_01_CLASIFICACION_DESCRIPCION = os.path.join(_GDB_PATH_HG if _GDB_PATH_HG else '', "TB_01_clasificacion_descripcion")

# :FIELDS
_CD_CUENCA = 'cd_cuenca'
_NM_CUENCA = 'nm_cuenca'

_ID_AUTOR = "id_autor"
_ABREV = "abrev"

_ID_FHIDROG = "id_fhidrog"
_N_FHIDROG = "n_fhidrog"
_D_FHIDROG = "d_fhidrog"
_LITOLOGIA_G = "litologia_g"
_ORDEN = "orden"
_UHIDROG = "uhidrog"
_CODI_G = "codi_g"
_ESTADO = "estado"

_DESCRIP = "descrip"

_ID = "id"
_GREEN = "green"
_RED = "red"
_BLUE = "blue"

# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# :Mapa hidrogeoquimico
# :PATHS
_GDB_PATH_MHQ = pkg.get_config_param_value('GDB_PATH_MHQ', one=True)
_BASE_EXCEL_LAB_FC = os.path.join(_GDB_PATH_MHQ if _GDB_PATH_MHQ else '', 'BASE_EXCEL_LAB_FC')
_DATASET_MHQ_GENERALES = os.path.join(_GDB_PATH_MHQ if _GDB_PATH_MHQ else '', 'DS_CAPAS_GENERALES')
_PO_SUBCUENCAS = os.path.join(_DATASET_MHQ_GENERALES if _DATASET_MHQ_GENERALES else '', 'PO_SUBCUENCAS')
_PO_MICROCUENCAS = os.path.join(_DATASET_MHQ_GENERALES if _DATASET_MHQ_GENERALES else '', 'PO_MICROCUENCAS')
_IMG_THIN_PIPER = os.path.join(_IMG_DIR, 'PiperCompleto_thinborder.png')
_IMG_THICK_PIPER = os.path.join(_IMG_DIR, 'PiperCompleto_thickborder.png')

_CSV_GRAFICOS = 'datos_graf.csv'
_MXD_MHQ = 'T01MHQ{}_g{}k.mxd'
_TB_AUTOR_MHQ = os.path.join(_GDB_PATH_MHQ if _GDB_PATH_MHQ else '', 'TB_MHQ_AUTOR')

# _MXD_MHQ_18 = os.path.join(_MXD_DIR, 'T01MHQ{}.mxd')
# _MXD_MHQ_19 = os.path.join(_MXD_DIR, 'T01MHQ{}.mxd')

# Layers 
_LAYER_PT_CAPITAL_DISTRITAL = 'PT_CAPITAL_DISTRITAL'
_LAYER_PT_01_YACIMIENTOS_MINEROS = 'PT_01_YACIMIENTOS_MINEROS'
_LAYER_PT_02_PASIVOS_AMBIENTALES_MINEROS = 'PT_02_PASIVOS_AMBIENTALES_MINEROS'
_LAYER_Vias_PL_03_VIAS_VECINALES ='Vias_PL_03_VIAS_VECINALES'
_LAYER_Vias_PL_04_VIAS_DISTRITALES = 'Vias_PL_04_VIAS_DISTRITALES'
_LAYER_Vias_PL_05_VIAS_NACIONALES = 'Vias_PL_05_VIAS_NACIONALES'
_LAYER_PO_LAGUNA = 'PO_LAGUNA'
_LAYER_PO_BOFEDAL = 'PO_BOFEDAL'
_LAYER_PO_CENTRO_URBANO = 'PO_CENTRO_URBANO'
_LAYER_PO_ZONA_ESTUDIO = 'PO_ZONA_ESTUDIO'
_LAYER_PO_MICROCUENCAS = 'PO_MICROCUENCAS'
_LAYER_PO_SUBCUENCAS = 'PO_SUBCUENCAS'
_LAYER_PL_curvas_nivel_47C = 'PL_curvas_nivel_47C'
_LAYER_PL_BUZAMIENTO = 'PL_BUZAMIENTO'
_LAYER_PL_PLIEGUES = 'PL_PLIEGUES'
_LAYER_PL_FALLAS = 'PL_FALLAS'
_LAYER_PO_GEOLOGIA_47C = 'PO_GEOLOGIA_47C'
_LAYER_ESTACIONES = 'ESTACIONES'
_LAYER_HIDROTIPOS = 'HIDROTIPOS'




# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# :Sincronizacion geodatabase
# :Tipos de Filtrado para lista de capas
_ORIGEN = "Todas las capas de la fuente"
_EXTFILE = "Archivo CSV"
_MXD_ACTUAL = "Capas del MXD Actual"
_MXD_EXTERNO = "Archivo MXD Externo"

# Escritorio del usuario
_DESKTOP_PATH = u'c:/users/%userprofile%/desktop'
_CL_HIDROG = "cl_hidrog"
_SCL_HIDROG = "scl_hidrog"
_ID_MAPA = "id_mapa"
_ID_PADRE = "id_padre"



# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Mapa Neotectonica
# PATHS
_GDB_PATH_NT = pkg.get_config_param_value('GDB_PATH_NT', one=True)
# _GDB_PATH_NT = r'D:\daguado\neotectonica\gdb\Neotectonicav2.gdb'
_TB_REGION_CONFIG = os.path.join(_GDB_PATH_NT if _GDB_PATH_NT else '', 'tb_neo_region_config')
_GPT_NEO_MUESTRA = os.path.join(_GDB_PATH_NT if _GDB_PATH_NT else '', 'gpt_neo_muestra')
_TB_NEO_FOTOGRAFIA = os.path.join(_GDB_PATH_NT if _GDB_PATH_NT else '', 'tb_neo_fotografia')
_TB_NEO_FOTOGRAFIA__ATTACH = os.path.join(_GDB_PATH_NT if _GDB_PATH_NT else '', 'tb_neo_fotografia__ATTACH')
_CD_DEPA_FIELD = 'cd_depa'
_NM_DEPA_FIELD = 'nm_depa'
_ESCALA_FIELD = 'escala'
_HOJA_FIELD = 'hoja'
_ORIENTACION_FIELD = 'orientacion'
_ZONA_FIELD = 'zona'

_TB_AUTOR = os.path.join(_GDB_PATH_NT if _GDB_PATH_NT else '', 'tb_neo_autor')
_ID_AUTOR_MN = 'id_autor'
_AB_AUTOR_MN = 'abrev'
_GLOBALID_FIELD = 'globalid'
_PARENTGLOBALID_FIELD = 'parentglobalid'
_REL_GLOBALID_FIELD = 'rel_globalid'
_ATTACHMENTID_FIELD = 'attachmentid'
_REGION_FIELD = 'region'
_ATT_NAME_FIELD = 'ATT_NAME'
_DATA_FIELD = 'DATA'


_MXD_A0_H = os.path.join(_MXD_DIR, 'T01MN_A0_H.mxd')
_MXD_A0_V = os.path.join(_MXD_DIR, 'T01MN_A0_V.mxd')

_LAYER_DEPARTAMENTOS = 'GPO_DEP_DEPARTAMENTOS'
_LAYER_DEPARTAMENTO = 'GPO_DEP_DEPARTAMENTO'

# Layers que permiten filtro de region
_LAYER_GPL_NEO_NEOTECTONICO = 'gpl_neo_neotectonico'
_GPL_NEO_NEOTECTONICO_PATH = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_DGA\DATA_GIS.GPL_NEO_NEOTECTONICO')
_LAYER_GPT_NEO_DESIDAD_POBLACIONAL= 'gpt_neo_densidad_poblacional'
_LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_PROY = 'gpt_neo_centrales_hidroelectricas_proy'
_LAYER_GPT_NEO_CENTRALES_HIDROELECTRICAS_EJEC = 'gpt_neo_centrales_hidroelectricas_ejec'
_LAYER_GPL_NEO_GASEODUCTO_CONSTRUCCION = 'gpl_neo_gaseoducto_construccion'
_LAYER_GPT_NEO_TELECOMUNICACIONES = 'gpt_neo_telecomunicaciones'
_LAYER_GPT_NEO_MECANISMOS_FOCALES = 'gpt_neo_mecanismos_focales'


_COD_GEOCAT_FIELD = 'COD_GEOCAT'
_NOMBRE_FIELD = 'NOMBRE'
_EDAD_ULT_M_FIELD = 'EDAD_ULT_M'


# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Mapa Geopatrimonio
# PATHS
_GDB_PATH_MGP = pkg.get_config_param_value('GDB_PATH_GP', one=True)
# _GDB_PATH_MGP = r'D:\daguado\geopatrimonio\gdb\gdb_geopatrimonio_template.gdb'
_TB_REGION_CONFIG_MGP = os.path.join(_GDB_PATH_MGP if _GDB_PATH_MGP else '', 'tb_geo_region_config')
_GPT_GEO_GEOSITIOS_PATH = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_DGA\DATA_GIS.GPT_GEO_GEOSITIO')
_TB_AUTOR_MGP = os.path.join(_GDB_PATH_MGP if _GDB_PATH_MGP else '', 'tb_geo_autor')

_MXD_A0_H_MGP = os.path.join(_MXD_DIR, 'T01MGP_A0_H.mxd')
_MXD_A0_V_MGP = os.path.join(_MXD_DIR, 'T01MGP_A0_V.mxd')

_LAYER_GPT_GEO_GEOSITIOS = 'GPT_GEO_GEOSITIOS'


_NUM_GEOSITIO_FIELD = 'num_geositio'
_DENOMINACION_FIELD = 'denominacion'
_IGEOL_PRINCIPAL_FIELD = 'igeol_principal'
_V_CIENTIFICA_FIELD = 'v_cientifica'
_V_DIDACTICA_FIELD = 'v_didactica'
_V_TURISTICA_FIELD = 'v_turistica'


# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Mapa Potencial Minero
# PATHS
_GPO_ZUT_ZONAS_UTM_PATH = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GPO_ZUT_ZONAS_UTM')
_PT_DGAR_REGISTRO_MP_TEMP = os.path.join(_BDGEOCAT_SDE, 'UGEO1749.PT_DRME_REGISTRO_MP')



# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Calculo del Potencial Minero
# PATHS
_GDB_CPM_DIR = pkg.get_config_param_value('FOLDER_PATH_CPM', one=True)
_CSV_REGION_CONFIG = os.path.join(_GDB_CPM_DIR, 'region_config.csv')
_ENV_GDB_PATH_CPM = 'GDB_PATH_CPM'




# ---------------------------------------------------------------------------------------------------
# ---------------------------------------------------------------------------------------------------
# Mapa Geomorfologico
# PATHS
_GPO_GEOMORFOLOGIA = os.path.join(_BDGEOCAT_SDE, r'DATA_GIS.DS_GEOLOGIA_DGA\DATA_GIS.GPO_GEO_GEOMORFOLOGIA_PERU')