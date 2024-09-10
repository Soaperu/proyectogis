import settings_aut as st
import os

# Departamento
class gpo_region(object):
    """
    FEATURE CLASS DE DEPARTAMENTOS EN LA GEODATABASECOORPORATIVA
    """
    def __init__(self):
        self.nm_depa = "NM_DEPA"
        self.cd_depa = "CD_DEPA"
    @property
    def dataset(self):
        return 'DATA_GIS.DS_BASE_CATASTRAL_GEOWGS84'
    @property
    def name(self):
        return 'DATA_GIS.GPO_DEP_DEPARTAMENTO'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_SDE, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'



class pm_region(object):
    """
    FEATURE CLASS DE DEPARTAMENTOS EN EL FILE GEODATABASE
    """
    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'GPO_Region'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

# Catastro minero
class gpo_catastro_minero(object):
    """
    FEATURE CLASS DE CATASTRO MINERO EN LA GEODATABASE COORPORATIVA
    """
    def __init__(self):
        self.id         = "OBJECTID"
        self.shape      = "SHAPE"
        self.leyenda    = "LEYENDA"
        self.naturaleza = "NATURALEZA"
    @property
    def dataset(self):
        return 'DATA_GIS.DS_CATASTRO_MINERO_GEOWGS84'
    @property
    def name(self):
        return 'DATA_GIS.GPO_CMI_CATASTRO_MINERO_G84'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_SDE, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

class gpo_sensores_remotos(object):
    def __init__(self):
        self.tipo = 'tipo'
    @property
    def dataset(self):
        return 'DATA_GIS.DS_GEOLOGIA_DRME'
    @property
    def name(self):
        return 'DATA_GIS.PO_0201001_ANOMALIA_ESPECTRAL'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_SDE, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'   

########################################################################
# Potencial Minero Metalico

# Unidad geologica
class pmm_gpo_ugeol(object):
    """
    FEATURE CLASS DE UNIDADES GEOLOGICAS EN EL FILE GEODATABASE
    """
    codi = "CODI"
    nombre = "NOMBRE"
    descripcion = "DESCRIPCION"
    unidad = "UNIDAD"
    grado = "GRADO"
    valor = "VALOR"
    condicion = "CONDICION"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'M_01_GPO_Litologia'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

class tb_ugeol_condicion(object):
    """
    TABLA QUE CONTIENE LA CONDICION METALOTECTO O NO METALOTECTO PARA EL
    FEATURE CLASS DE UNIDADES GEOLOGICAS
    """
    code = "CODE"
    descrip = "DESCRIP"
    tipo = "TIPO"

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_UG_Condicion'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_var_ugeol(object):
    def __init__(self, ws):
        self.ws = ws
        self.grado = 'GRADO'
        self.valor = 'VALOR'
    @property
    def name(self):
        return 'M_VAR_GPO_Litologia'
    @property
    def dataset(self):
        return 'DS_02_Variables'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class pmm_ras_ugeol(object):
    def __init__(self, ws):
        self.ws = ws

    @property
    def name(self):
        return 'M_VAR_RAS_UnidadGeologica'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'rasterdataset'

# Falla geologica
class pmm_gpl_fallageol(object):
    """
    FEATURE CLASS DE FALLAS GEOLOGICAS EN EL FILE GEODATABASE
    """
    codi        = 'CODI'
    descripcion = 'DESCRIPCION'
    distancia   = 'DISTANCIA'

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'M_02_GPL_FallaGeologica'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polyline'

class pmm_tb_fallageol(object):
    """
    FEATURE CLASS DE FALLAS GEOLOGICAS EN EL FILE GEODATABASE
    """
    nombre_falla = 'NOMBRE_FALLA'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_M_FG_Descripcion'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_tb_fallageol_grado(object):
    dist_min = 'DIST_MIN'
    dist_max = 'DIST_MAX'
    influencia = 'INFLUENCIA'
    dominio = 'DOMINIO'
    grado = 'GRADO'
    valor = 'VALOR'

    def __init__(self, ws):
        self.ws = ws

    @property
    def name(self):
        return 'TB_M_FG_Grado'

    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_var_fallageol(pmm_var_ugeol):
    influencia = 'INFLUENCIA'
    grado      = 'GRADO'
    valor      = 'VALOR'

    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_GPO_FallaGeologica'

class pmm_ras_fallageol(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_RAS_FallaGeologica'

# Deposito mineral
class pmm_gpo_depmineral(object):
    """
    FEATURE CLASS DE DEPOSITOS MINERALES EN EL FILE GEODATABASE
    """
    dimension = "BUFFER"
    unidad    = "UNIDAD_MIN"
    grado     = "GRADO"
    valor     = "VALOR"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'M_03_GPO_DepositoMineral'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

class pmm_var_depmineral(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_GPO_DepositoMineral'

class pmm_ras_depmineral(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_RAS_DepositoMineral'


class pm_dominio_estructural(object):
    """
    FEATURE CLASS DE DEPARTAMENTOS EN EL FILE GEODATABASE
    """
    def __init__(self, ws):
        self.dominio = "DOMINIO"
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'GPO_Dominio_Estructural'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'


# Concesion minera
class pmm_gpo_concmin(object):
    """
    FEATURE CLASS DE CATASTRO MINERO EN EL FILE GEODATABASE
    """
    leyenda    = "LEYENDA"
    naturaleza = "NATURALEZA"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'M_04_GPO_CatastroMinero'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'


class pmm_tb_concmin_grado(object):
    leyenda = 'LEYENDA'
    condicion = 'CONDICION'
    dominio = 'DOMINIO'
    grado = 'GRADO'
    valor = 'VALOR'
    tipo = 'TIPO'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_M_CM_Grado'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_var_concmin(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_GPO_ConcesionMinera'

class pmm_ras_concmin(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_VAR_RAS_ConcesionMinera'

# Geoquimica
class pmm_ras_geoquimica(pmm_ras_ugeol):
    """
    RASTER DE LA VARIABLE GEOQUIMICA EN EL FILE GEODATABASE
    """
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        """
        Nombre de la capa
        :return:
        """
        return 'M_VAR_RAS_Geoquimica'

# Sensores remotos
class pmm_gpo_sensores(object):
    """
    FEATURE CLASS DE LA VARIABLE SENSORES REMOTOS
    """
    tipo_arc = "TIPO_ARC"
    tipo_oxi = "TIPO_OXI"
    tipo     = "TIPO"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'M_05_GPO_SensorRemoto'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

class pmm_tb_sensores_grado(object):
    condicion = 'CONDICION'
    dominio = 'DOMINIO'
    grado = 'GRADO'
    valor = 'VALOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_M_SR_Grado'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_var_sensores(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)

    @property
    def name(self):
        return 'M_VAR_GPO_SensorRemoto'

class pmm_ras_sensores(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)

    @property
    def name(self):
        return 'M_VAR_RAS_SensorRemoto'

########################################################################
# Potencial Minero No Metalico

# Litologia
class rmi_gpo_litologia(object):
    """
    FEATURE CLASS DE UNIDADES GEOLOGICAS EN EL FILE GEODATABASE
    """
    codi = "CODI"
    nombre = "NOMBRE"
    descripcion = "DESCRIPCION"
    unidad = "UNIDAD"
    grado = "GRADO"
    valor = "VALOR"
    condicion = "CONDICION"

    def __init__(self, ws):
        self.ws = ws

    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'RMI_06_GPO_Litologia'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

# class rmi_tb_litologia_condicion(object):
#     """
#     TABLA QUE CONTIENE LA CONDICION LITOCTETO O NO LITOCTETO PARA EL
#     FEATURE CLASS DE LITOLOGIA
#     """
#     code = "CODE"
#     descrip = "DESCRIP"
#     tipo = "TIPO"

#     def __init__(self, ws):
#         self.ws = ws
#     @property
#     def name(self):
#         return 'TB_UG_Condicion'
#     @property
#     def path(self):
#         return os.path.join(self.ws, self.name)
#     @property
#     def dataType(self):
#         return 'table'

class rmi_var_litologia(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_GPO_Litologia'
    @property
    def dataset(self):
        return 'DS_02_Variables'

class rmi_ras_litologia(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_RAS_Litologia'

# Concesion minera
class rmi_gpo_concmin(object):
    """
    FEATURE CLASS DE CATASTRO MINERO EN EL FILE GEODATABASE
    """
    leyenda    = "LEYENDA"
    naturaleza = "NATURALEZA"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'RMI_08_GPO_CatastroMinero'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_tb_concmin_grado(object):
    leyenda = 'LEYENDA'
    condicion = 'CONDICION'
    dominio = 'DOMINIO'
    grado = 'GRADO'
    valor = 'VALOR'
    tipo = 'TIPO'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_RMI_CM_Grado'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class rmi_var_concmin(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_GPO_ConcesionMinera'

class rmi_ras_concmin(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_RAS_ConcesionMinera'

# Accesos
class rmi_accesos(object):
    """
    FEATURE CLASS DE VIAS EN LA GEODATABASE COORPORATIVA
    """

    def __init__(self):
        self.id     = "OBJECTID"
        self.rasgo_secu   = "RASGO_SECU"
        self.rasgo_prin = "RASGO_PRIN"
    @property
    def dataset(self):
        return 'DATA_GIS.DS_IGN_BASE_PERU_500000'
    @property
    def name(self):
        return 'DATA_GIS.IGN_TRA_VIAS'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_SDE, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_gpl_accesos(object):
    """
    FEATURE CLASS DE ACCESOS EN EL FILE GEODATABASE
    """
    nombre = 'NOMBRE'
    rasgo_secu = 'RASGO_SECU'
    tipo = 'TIPO'
    influencia = 'INFLUENCIA'

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'RMI_10_GPL_Accesos'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_tb_accesos(object):
    """
    TABLA DE ACCESOS EN EL FILE GEODATABASE
    """
    tipo = "TIPO"
    grado = "GRADO"
    influencia = "INFLUENCIA"
    valor = "VALOR"

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_RMI_Accesos'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class rmi_var_accesos(object):
    influencia = 'INFLUENCIA'
    grado = 'GRADO'
    valor = 'VALOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'RMI_VAR_GPO_Accesos'
    @property
    def dataset(self):
        return 'DS_02_Variables'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_ras_accesos(object):
    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'RMI_VAR_RAS_Accesos'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'featureclass'

# Sustancias
class rmi_gpt_sustancias(object):
    """
    FEATURE CLASS DE SUSTANCIAS EN EL FILE GEODATABASE
    """
    def __init__(self, ws):
        self.ws = ws
        self.sustancia = 'SUSTANCIA'
        self.grado = 'GRADO'
        self.valor = 'VALOR'

    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'RMI_07_GPT_Sustancias'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_sustancias(object):
    """
    FEATURE CLASS DE SUSTANCIAS EN LA GEODATABASE COORPORATIVA
    """
    def __init__(self):
        self.id = "OBJECTID"
        self.shape = "SHAPE"
        self.sustancia = "SUSTANCIA"
    @property
    def dataset(self):
        return 'DATA_EDIT.DS_GEOCATMIN'
    @property
    def name(self):
        return 'DATA_EDIT.GPT_RMI_Roc_Min_Ind'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_SDE, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_tb_sustancias(object):
    """
    TABLA DE SUSTANCIAS EN EL FILE GEODATABASE
    """
    sustancia = 'SUSTANCIA'
    dominio = 'DOMINIO'
    grado = 'GRADO'
    valor = 'VALOR'
    influencia = 'INFLUENCIA'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_RMI_Sustancias'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class rmi_var_sustancias(object):
    sustancia = 'SUSTANCIA'
    influencia = 'INFLUENCIA'
    grado      = 'GRADO'
    valor      = 'VALOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'RMI_VAR_GPO_Sustancias'
    @property
    def dataset(self):
        return 'DS_02_Variables'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'

class rmi_ras_sustancias(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_RAS_Sustancias'

# Sensores remotos
class rmi_gpo_sensores(object):
    """
    FEATURE CLASS DE LA VARIABLE SENSORES REMOTOS
    """
    tipo_arc = "TIPO_ARC"
    tipo_oxi = "TIPO_OXI"
    tipo     = "TIPO"

    def __init__(self, ws):
        self.ws = ws
    @property
    def dataset(self):
        return 'DS_01_Insumos'
    @property
    def name(self):
        return 'RMI_09_GPO_SensorRemoto'
    @property
    def path(self):
        return os.path.join(self.ws, self.dataset, self.name)
    @property
    def dataType(self):
        return 'featureclass'
    @property
    def shapeType(self):
        return 'polygon'

class rmi_tb_sensor_grado(object):
    condicion = 'CONDICION'
    grado = 'GRADO'
    valor = 'VALOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_SR_Grado'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class rmi_var_sensores(pmm_var_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_GPO_SensorRemoto'

class rmi_ras_sensores(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'RMI_VAR_RAS_SensorRemoto'

########################################################################
# Potencial Minero Metalico
class tb_config(object):
    region = 'NM_DEPA'
    zona = 'ZONA'
    ubicacion = 'UBICACION'
    cd_depa = 'CD_DEPA'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'tb_config'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class tb_nivel(object):
    """
    TABLA QUE CONTIENE ELS NIVELES DE POTENCIAL PARA CADA VARIABLE
    EN EL FILE GEODATABASE
    """
    code = "CODE"
    grado = "GRADO"
    vmin = "VMIN"
    vmax = "VMAX"

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_Grado'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class pmm_tb_factor(object):
    nom_ras = 'NOM_RAS'
    factor = 'FACTOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_M_Factor'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class ras_potencial_minero_metalico(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_RAS_PotencialMineroMetalico'

# Potencial Minero No Metalico
class rmi_tb_factor(object):
    nom_ras = 'NOM_RAS'
    factor = 'FACTOR'

    def __init__(self, ws):
        self.ws = ws
    @property
    def name(self):
        return 'TB_RMI_Factor'
    @property
    def path(self):
        return os.path.join(self.ws, self.name)
    @property
    def dataType(self):
        return 'table'

class ras_potencial_minero_no_metalico(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_RAS_PotencialMineroNoMetalico'

class ras_potencial_minero(pmm_ras_ugeol):
    def __init__(self, ws):
        super(self.__class__, self).__init__(ws)
    @property
    def name(self):
        return 'M_RAS_PotencialMinero'