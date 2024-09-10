import settings_aut as st
import os

# Departamento
class gpo_dgr_ulito(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
        self.etiqueta = "ETIQUETA"
        self.unidad = "UNIDAD"
        self.grosor_m = "GROSOR_M"
        self.grosor_i = "GROSOR_I"
        self.descripcion = "DESCRIP"
        self.codhoja = "CODHOJA"
        self.codi = "CODI"
        self.codform = "CODIUNIHOJA"
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPO_DGR_ULITO_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    # @property
    # def zona(self):
    #     return self.__zona
    # @zona.setter
    # def zona(self, value):
    #     self.__zona = value
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class gpt_dgr_pog(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.pog = 'POG'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
        # self.etiqueta = "ETIQUETA"
        # self.unidad = "UNIDAD"
        # self.grosor_m = "GROSOR_M"
        # self.grosor_i = "GROSOR_I"
        # self.descripcion = "DESCRIP"
        self.codhoja = "CODHOJA"
        # self.codi = "CODI"
        # self.codform = "CODIUNIHOJA"
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPT_DGR_POG_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    # @property
    # def zona(self):
    #     return self.__zona
    # @zona.setter
    # def zona(self, value):
    #     self.__zona = value
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class tb_dgr_leyenda(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self):
        self.codi = "CODI"
        self.codform = "CODIUNIHOJA"
        self.etiqueta = "ETIQUETA"
        self.unidad = "UNIDAD"
        self.descripcion = "DESCRIP"
        self.serie = "SERIE"
        self.tipo = "TIPO"
        self.edad = "EDAD"
        self.orden = "ORDEN"
        self.grosor_m = "GROSOR_M"
        self.grosor_i = "GROSOR_I"
        self.grosor_u = "GROSOR_U"
        self.separador = "SEP"
        self.codhoja = "CODHOJA"
        # self.sep = "SEP"
        self.dominio = "DOMINIO"
        self.estado = "ESTADO"
    @property
    def name(self):
        return 'TB_MG_LEYENDA'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.name)
    @property
    def workspace(self):
        return st._GDB_PATH_MG
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class tb_edades(object):
    def __init__(self):
        self.id_edad = 'ID_EDAD'
        self.id_padre = 'ID_PADRE'
        self.nombre = 'NOMBRE'
        self.edad_ini = 'EDAD_INI'
        self.ei_aprox = 'EI_APROX'
        self.edad_fin = 'EDAD_FIN'
        self.ef_aprox = 'EF_APROX'
        self.tipo = 'tipo'
    @property
    def name(self):
        return 'TB_MG_EDADES_N'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.name)



class gpo_dg_hojas_50(object):
    def __init__(self):
        self.zona = 'ZONA'
        self.codhoja = 'CODHOJA'
    @property
    def name(self):
        return 'GPO_DG_HOJAS_50K'
    @property
    def dataset(self):
        return 'DS01_DATO_GEOGRAFICO'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)  


class gpo_mg_form(object):
    """
    Feature clas de diagramas para leyenda geologica
    """
    def __init__(self):
        self.codi = "CODI"
        # self.codform = "CODIUNIHOJA"
        self.etiqueta = "ETIQUETA"
        self.unidad = "UNIDAD"
        self.descripcion = "DESCRIP"
        # self.serie = "SERIE"
        self.tipo = "TIPO"
        # self.edad = "EDAD"
        # self.orden = "ORDEN"
        # self.grosor_m = "GROSOR_M"
        # self.grosor_i = "GROSOR_I"
        # self.grosor_u = "GROSOR_U"
        # self.separador = "SEP"
        self.codhoja = "CODHOJA"
        # self.sep = "SEP"
        self.dominio = "DOMINIO"
        self.estado = "ESTADO"
    @property
    def name(self):
        return 'GPO_MG_FORM'
    @property
    def dataset(self):
        return 'DS11_LEYENDA'
    @property
    def workspace(self):
        return st._GDB_PATH_MG
    @property
    def path(self):
        return os.path.join(self.workspace, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class gpl_mg_celd(object):
    """
    Feature class de lineas auxiliares para leyenda geologica
    """
    def __init__(self):
        self.codi = "CODI"
        self.tipo = "TIPO"
        self.dominio = "DOMINIO"
        self.hoja = "HOJA"
        self.cuadrante = "CUADRANTE"
        self.codhoja = "CODHOJA"
    @property
    def name(self):
        return 'GPL_MG_CELD'
    @property
    def dataset(self):
        return 'DS11_LEYENDA'
    @property
    def workspace(self):
        return st._GDB_PATH_MG
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class gpt_mg_label(object):
    """
    Feature class de etiquetas para leyenda geologica
    """
    def __init__(self):
        self.etiqueta = "ETIQUETA"
        self.estilo = "ESTILO"
        self.angulo = "ANGULO"
        self.alineacion = "ALINEACION"
        self.tipo = "TIPO"
        self.dominio = "DOMINIO"
        self.estado = "ESTADO"
        self.hoja = "HOJA"
        self.cuadrante = "CUADRANTE"
        self.codhoja = "CODHOJA"
    @property
    def name(self):
        return 'GPT_MG_LABEL'
    @property
    def dataset(self):
        return 'DS11_LEYENDA'
    @property
    def workspace(self):
        return st._GDB_PATH_MG
    @property
    def path(self):
        return os.path.join(self.workspace, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]



class gpl_dgr_contac(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_CONTAC_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class gpl_dgr_dique(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_DIQUE_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class gpl_dgr_esvolc(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_ESVOLC_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]

class gpl_dgr_falla(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_FALLA_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class gpl_dgr_geofor(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_GEOFOR_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class gpl_dgr_plieg(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPL_DGR_PLIEG_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]
    

class gpt_dgr_fosil(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPT_DGR_FOSIL_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class gpt_dgr_muestr(object):
    """
    Feature ce unidades litologicas
    """
    def __init__(self, zona):
        self.pog = 'POG'
        self.codhoja = 'CODHOJA'
        self._zona = zona
        self._indicator = str(int(zona) - 12).zfill(2)
    @property
    def dataset(self):
        return 'DS{}_GEOLOGIA_{}S'.format(self._indicator, self._zona)
    @property
    def name(self):
        return 'GPT_DGR_MUESTR_{}S'.format(self._zona)
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]
class gpl_mg_perfil(object):
    def __init__(self):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
    @property
    def dataset(self):
        return 'DS12_PERFIL'
    @property
    def name(self):
        return 'GPL_MG_PERFIL'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]
class gpo_mg_perfil(object):
    def __init__(self):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
    @property
    def dataset(self):
        return 'DS12_PERFIL'
    @property
    def name(self):
        return 'GPO_MG_PERFIL'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]
class gpt_mg_perfil(object):
    def __init__(self):
        self.codi = 'CODI'
        self.codhoja = 'CODHOJA'
    @property
    def dataset(self):
        return 'DS12_PERFIL'
    @property
    def name(self):
        return 'GPT_MG_PERFIL'
    @property
    def path(self):
        return os.path.join(st._GDB_PATH_MG, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]


class po_dgr_evaluacion_mg_50(object):
    def __init__(self):
        self.score_pog = "SCORE_POG"
        self.score_el = "SCORE_EL"
        self.score_le = "SCORE_LE"
        self.score_fm = "SCORE_FM"
        self.score_se = "SCORE_SE"
        self.score_tot = "SCORE_TOT"
        self.evaluador = "EVALUADOR"
        self.fecha = "FECHA"
        self.codhoja = "CODHOJA"
        self.url = "URL"
        self.anio = "ANIO"
        self.estado = "ESTADO"
    @property
    def dataset(self):
        return 'DATA_EDIT.DS_DGR'
    @property
    def name(self):
        return 'DATA_EDIT.PO_DGR_EVALUACION_MG_50'
    @property
    def path(self):
        return os.path.join(st._BDGEOCAT_HUAWEI_SDE, self.dataset, self.name)
    def get_fields(self):
        return [v for k, v in vars(self).items() if not k.startswith('_')]
