# -*-coding: utf-8-*-

from datetime import datetime
import arcpy

_ERROR_MXD_PATH_NOT_EXISTS = 'La ubicacion del archivo mxd especificado no existe'
_ERROR_CODE_NOT_EXISTS = 'El codigo de cuadricula especificado no existe'
_ERROR_NOT_LAYER_CUADRICULAS = 'El layer de cuadriculas no exista o no se encuentra presente en el mapa'
_ERROR_NOT_DATAFRAMES = 'No se encontramos data frames en el mapa'

_OPEN_DIALOG_TITLE = 'Seleccione un directorio'
_OPEN_DIALOG_BUTTON_TITLE = 'Seleccionar'

_ERROR_NOT_PROJECTION = 'El archivo especificado no tiene una proyeccion cartografica valida'
_ERROR_PROJECTION_NO_VALID = u'El archivo especificado tiene una proyección no valida. Proyecciones validas WGS 17, 18, 19S'

_ERROR_UNREGISTERED_USER = 'El usuario ingresado no esta registrado'
_ERROR_INCORRECT_PASSWORD = u'La contraseña ingresada no es correcta'
_ERROR_NO_MODULES_ASSIGNED = u'El usuario no tiene módulos asignados'
_ERROR_ADD_USER = 'Por favor ingrese un nombre de usuario'
_ERROR_ADD_PASSWORD = 'Por favor ingrese una contraseña'

_SET_CONFIG_TEMP_FOLDER = u'Configuración del directorio de archivos temporales'
_SET_CONFIG_GDB_PT = u'Configuración de la geodatabase de archivos para el módulo de Planos Topográficos 25000'
_SET_CONFIG_MXD_PT_17 = u'Configuración del documento de mapa en zona 17 para el módulo de Planos Topográficos 25000'
_SET_CONFIG_MXD_PT_18 = u'Configuración del documento de mapa en zona 18 para el módulo de Planos Topográficos 25000'
_SET_CONFIG_MXD_PT_19 = u'Configuración del documento de mapa en zona 19 para el módulo de Planos Topográficos 25000'
_SET_CONFIG_MXD_PG_17 = u'Configuración del documento de mapa en zona 17 para el módulo de Peligros Geológicos'
_SET_CONFIG_MXD_PG_18 = u'Configuración del documento de mapa en zona 18 para el módulo de Peligros Geológicos'
_SET_CONFIG_MXD_PG_19 = u'Configuración del documento de mapa en zona 19 para el módulo de Peligros Geológicos'
_SET_GDB_MHIDROGEO = u'Configuración de la geodatabase de archivos para el módulo de Mapas Hidrogeológicos'
_PROCESS_FINISHED = 'Proceso finalizado correctamente'
_SET_CONFIG_GDB_PG = 'Configuración de la geodatabase de archivos para el módulo de Peligros Geológicos'

_ERROR_FEATURE_CUADRICULAS_MG = 'El feature class de hojas a escala 500000 no se encuentra en la geodatabase'
_ERROR_FEATURE_CUENCAS_HG = u'El feature class de Cuencas no existe, o no se declaró la geodatabase en la sección de configuración'
_ERROR_FEATURE_CUENCAS_MHQ = u'No se declaró la geodatabase en la sección de configuración o la capa BASE_EXCEL_LAB_FC aún no ha sido creada'
_ERROR_GDB_CONFIG_MHQ = u'No se declaró la geodatabase en la sección de configuración'
_ERROR_FEATURE_AUTORES_HG = u'El feature table de Autores no existe, o no se declaró la geodatabase en la sección de configuración'
_ERROR_FEATURE_FHIDROGEO_HG = u'El feature class de Formaciones hidrogeológicas no existe, o no se declaró la geodatabase en la sección de configuración'
_ERROR_FEATURE_UHIDROGEO_HG = u'El feature class de Unidades hidrogeológicas no existe, o no se declaró la geodatabase en la sección de configuración'
_ERROR_NO_SUCH_DATAFRAME = u'El data frame ingresado no existe'
_ERROR_NO_CODHOJAS = u'No se ingresaron códigos de hojas válidas'
_ERROR_WKID_DEM_PROFILE_GEOLOGY = u'El DEM ingresado debe ser proyectado en coordenadas WGS84 UTM (17, 18 o 19)'

_NAME_DGAR = u'Dirección de Geología Ambiental y Riesgo Geológico'
_NAME_INGEMMET = u'INSTITUTO GEOLÓGICO MINERO Y METALÚRGICO'
_NAME_SECTOR = u'SECTOR ENERGÍA Y MINAS'

year = datetime.today().year
_DETALLE_ROTULO = u'Versión digital: Año {} Lima - Perú'.format(year)

_ERROR_EMPTY_MXD = 'El MXD Actual no contiene capas'
_ERROR_DIALOG = u'¡ERROR!'


_NRO = 'NRO'
_CODIGO = u'CÓDIGO'
_NOMBRE = 'NOMBRE'
_ULTIMA_REACTIVACION = u'ÚLTIMA REACTIVACIÓN'
_MAIN_TITLE_MN = u'RELACIÓN DE FALLAS ACTIVAS Y CUATERNARIAS'
_WARNING_FORMATO_FILTRO = 'Formato de filtro no reconocido'
_WARNING_DIALOG = u'ADVERTENCIA'


_MAIN_TITLE_MGP = u'SITIOS DE INTERÉS GEOLÓGICO Y MINERO - REGIÓN {}'
_CODIGO_MGP = u'CÓDIGO'
_SITIO_INTERES_MGP = u'DENOMINACIÓN O SITIO DE\nINTERÉS'
_INTERES_PRINCIPAL_MGP = u'INTERÉS GEOLÓGICO\nPRINCIPAL'
_VC_MGP = 'VC'
_VD_MGP = 'VD'
_VT_MGP = 'VT'
# ---------------------------------------------------------------------------------------------------
# :Mapa hidrogeoquimico
# :PATHS
_ERROR_FEATURE_SUBCUENCA_MHQ = 'La subcuenca seleccionada no se encuentra en la base, por favor ingrese un shape asociado a esta subcuenca'
_ERROR_FEATURE_MICROCUENCA_MHQ = 'La microcuenca seleccionada no se encuentra en la base, por favor ingrese un shape asociado a esta microcuenca'

_ERROR_CUENCA_DUPLICADA  = u'Existen valores erróneos en la correspondecia de CodCuenca y Cuenca en el excel, por favor corregir'
_ERROR_SUBCUENCA_DUPLICADA  = u'Existen valores erróneos en la correspondecia de CodSubc y Subcuenca en el excel, por favor corregir'


_CODIGO_MHQ = u'Código'
_TEMPORADA = 'Temporada'
_MHQ_TEXTO_Y = u'SÓLIDOS TOTALES DISUELTOS'
_IONES ={'cation' : [u'cálcica', u'magnésica', u'sódica'],
'anion' : ['Bicarbonatada', 'Sulfatada', 'Clorurada']}
_TIP_FUENTES_MHQ={
    'TIP_FT_SUB':u'Subterránea',
    'TIP_FT_SUP':u'Superficial'
}


# Potencial minero
_MPM_FEATURE_NOT_EXIST = 'El archivo shapefile ingresado no existe'
_MPM_SRC_NOT_EXIST = 'El archivo shapefile ingresado no cuenta con Sistema de Referencia Espacial (SRC); por favor asigne el SRC antes de continuar'
_MPM_ERROR_OUT_PERU = u'El archivo shapefile ingresado está fuera del territorio nacional'
_MPM_FEATURE_NOT_POLYGON = u'El archivo shapefile ingresado no es un polígono'



# CALCULO DE POTENCIAL MINERO
_CPM_GEODATABASE_NOT_EXIST = 'La geodatabase seleccionada no existe'
_CPM_TB_CONFIG_NOT_EXIST = u'La tabla de configuración no existe o no contiene los datos necesarios para seguir con el proceso'
_CPM_FEATURE_NOT_EXIST = u'El FeatureClass {} no se encuentra en la Geodatabase ingresada'
_CPM_INIT_PROCESS = u'\n\tIniciando procesamiento'
_CPM_END_PROCESS = u'\n\tProceso finalizado con éxito\n'
_CPM_CHECK_GEODATABASE = u'\t1. Comprobando la existencia de la geodatabase ingresada'
_CPM_ERROR_GDB_TYPE = u'El tipo de espacio de trabajo agregado no es correcto'
_CPM_CHECK_DATA = u'\t2. Verificando datos erroneos'
_CPM_SEND_DATA_TO_GEODATABASE = u'\t   - Enviando información a geodatabase'
_CPM_ERROR_DETECTEC = 'Se detectaron registros incorrectos'
_CPM_ERROR_SHAPETYPE_DM = 'La capa de depositos mineros debe ser de tipo Polígono'
_CPM_ERROR_SHAPETYPE_FG = 'La capa de Fallas Geológicas debe ser de tipo línea o polilínea'
_CPM_ERROR_SHAPETYPE_UG = 'La capa de Litología debe ser de tipo Polígono'
_CPM_CHECK_SA_EXTENSION_AVAILABLE = u'\t1. Verificando disponibilidad de licencia SPATIAL ANALYST'
_CPM_ERROR_SA_EXTENSION_AVAILABLE = u'\n\tLa licencia SPATIAL ANALYST no está disponible\n'
_CPM_FEATURE_EMPTY_ROWS = u'\n\tLa variable {} no contiene registros\n'

_CPM_VARIABLE_UNIDADES_GEOLOGICAS = u'\n\t1. Evaluación de variable de Unidades Geológicas'
_CPM_VARIABLE_UNIDADES_GEOLOGICAS_RMI = u'\n\t1. Evaluación de variable Litología'
_CPM_VARIABLE_CONCESIONES_MINERAS = u'\n\t3. Evaluación de variable de Concesiones Mineras'
_CPM_VARIABLE_FALLAS_GEOLOGICAS = u'\n\t2. Evaluación de variable de Fallas Geológicas'
_CPM_VARIABLE_DEPOSITOS_MINERALES = u'\n\t4. Evaluación de variable de Depositos Minerales'
_CPM_VARIABLE_GEOQUIMICA = u'\n\t5. Evaluación de variable de Geoquímica'
_CPM_VARIABLE_SENSORES_REMOTOS = u'\n\t6. Evaluación de variable Sensores Remotos'

_CPM_STORING_FEATURECLASS_INTO_GEODATABASE = u'\t   - Almacenando resultado como Feature Class en el File Geodatabase'
_CPM_STORING_RASTERDATASET_INTO_GEODATABASE = u'\t   - Almacenando resultado como Raster Dataset en el File Geodatabase'

_CPM_UNION_CONCESIONES_MINERAS = u'\t   - Superposición entre el insumo de Catastro Minero y Unidades Geológicas'
_CPM_CALCULATE_GRADE_VALUE = u'\t   - Estimando el grado y valor'

_CPM_BUFFER_FALLA_GEOLOGICA = u'\t   - Estimando el radio de influencia para cada falla geológica'
_CPM_ERROR_DISTANCE_VALUE_NOT_NUMBER = u"No se ha especificado un valor de distancia de tipo numérico"
_CPM_ERROR_OPERATOR_NOT_EXIST = "El operador ingresado no existe"
_CPM_ERROR_VALUE_INCORRECT = "Valores erroneos en la tabla {}"
_CPM_CALCULATE_BUFFER_FALLAS_GEOLOGICAS = u'\t   - Calculando las zonas de influencia'
_CPM_CONFIG_LIMITS_BY_REGION = u'\t   - Configurando límites en base a la región'
_CPM_CHECK_GEOQUIMICA_RASTER_EXIST = u'\t   - Se comprobó existencia del dataset raster de Geoquímica'
_CPM_ERROR_RASTER_GEOQUIMICA_NOT_EXIST = u'\tEl Raster Dataset de Geoquimica no existe o no fue cargado'


_CPM_EVAL_POTENCIAL_MINERO_METALICO = u'\n\t7. Evaluación el Potencial Minero Metálico'
_CPM_EVAL_POTENCIAL_MINERO_NO_METALICO = u'\n\t7. Evaluación el Potencial Minero no Metálico'
_CPM_ALGEBRA_MAPS = u'\t   - Algebra de mapas entre las variables establecidas'

_CPM_VARIABLE_ACCESOS = u'\n\t5. Evaluación de variable de Accesos'
_CPM_BUFFER_ACCESOS = u'\t   - Estimando el radio de influencia para cada vía'
_CPM_CALCULATE_BUFFER_ACCESOS = u'\t   - Calculando las zonas de influencia'


_CPM_VARIABLE_SUSTANCIAS = u'\n\t2. Evaluación de variable de Sustancias'
_CPM_BUFFER_SUSTANCIAS = u'\t   - Estimando el radio de influencia para cada sustancia'
_CPM_CALCULATE_BUFFER_SUSTANCIAS = u'\t   - Calculando las zonas de influencia'


_CPM_EVALUATE_PM = u'\t  - Evaluación de parámetros del Potencial Minero'
_CPM_ERROR_LEVEL_IS_VERY_SMALL = u"El nivel especificado para el análisis es muy alto"
_CPM_CHECK_PREDOMINANT_AREA = u"\t  - Determinando area de predominancia metálica o no metálica"
_CPM_PREDOMINANT_METALIC = u"\t  - Se determinó una mayor predominancia metalica"
_CPM_PREDOMINANT_NO_METALIC = u"\t  - Se determinó una mayor predominancia no metalica"


# Mapa geologico 50000
_ERROR_MG_NOT_DATA_ULITO = u"La hoja seleccionada no cuenta con datos de Unidades Litólogicas"


# Evaluacion Mapa geologico 50000
_ERROR_EMG_HOJA_NO_SELECCIONADA = u"Debe seleccionar al menos una hoja"
_ERROR_FEATURE_POG_MG = u'El feature class de Puntos de Observación Geológica (POG) no se encuentra en la geodatabase'

# Mapa geomorfologico
_ERROR_MGEOM_NOT_DATA = u"El archivo seleccionado no existe"