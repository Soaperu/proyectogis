# -*- coding: utf-8 -*-
"""
Archivo model.py adaptado a Python 3.
Define clases y modelos para la configuración del proyecto.
"""

import os
# Si connectionsqlite.py maneja ya import os, revisa si no es redundante.
# Pero para seguridad lo incluimos aquí:
# from connectionsqlite import *  # <-- Se asume que provee ciertas funciones o variables globales.


class column:
    def __init__(self, **kwargs):
        self.name = kwargs.get('name')
        self.type = kwargs.get('type')

    @property
    def to_dict(self):
        return self.__dict__


class FeatureDatasets:
    def __init__(self, zone=None, datum=None):
        self.zone = zone
        self.datum = datum

    @property
    def name(self):
        # Ejemplo: "insumos_17_wgs84"
        return f"insumos_{self.zone}_{self.datum}"

    @property
    def path(self):
        # Se asume que FILE_GDB está definido en connectionsqlite.py o similar.
        return os.path.join("FILE_GDB", self.name)


class tb_cademter_control:
    """
    Tabla de control con información básica de insumos/paths.
    """
    def __init__(self):
        self.usuario = column(name='USUARIO')
        self.mac = column(name='MAC')
        self.path = column(name='PATH')
        self.path_input = column(name='PATH_INPUT')
        self.path_output = column(name='PATH_OUTPUT')
        self.path_tmp = column(name='PATH_TMP')
        self.name_dir = column(name='NAME_DIR')
        self.cod_mar = column(name='COD_MAR')
        self.name_mar = column(name='NAME_MAR')
        self.cod_front = column(name='COD_FRONT')
        self.name_front = column(name='NAME_FRONT')

    @property
    def name(self):
        # Ajusta si necesitas un nombre distinto para la tabla real
        return 'DAGU1883.TB_CADEMTER_CONTROL'

    def __str__(self):
        return self.name


class regiones:
    """
    Modelo para tabla/FeatureClass de regiones (departamentos).
    """
    def __init__(self):
        self.cd_depa = column(name='CD_DEPA', type='String')
        self.nm_depa = column(name='NM_DEPA', type='String')
        self.cap_depa = column(name='CAP_DEPA', type='String')

    @property
    def name(self):
        # Retorna el nombre de la clase como string
        return self.__class__.__name__

    def __str__(self):
        return self.name


class zonas_geograficas:
    """
    Modelo para tabla/FeatureClass de zonas geográficas (ZONA 17, 18, 19, etc.).
    """
    def __init__(self):
        self.zona = column(name='ZONA', type='Integer')

    @property
    def name(self):
        return self.__class__.__name__

    def __str__(self):
        return self.name


class cuadriculas_17:
    """
    Modelo para la tabla/FeatureClass de cuadriculas en zona 17.
    """
    def __init__(self):
        self.cd_cuad = column(name='CD_CUAD', type='String')
        self.zona = column(name='ZONA', type='String')
        self.norte_min = column(name='NORTE_MIN', type='Double')
        self.norte_max = column(name='NORTE_MAX', type='Double')
        self.este_min = column(name='ESTE_MIN', type='Double')
        self.este_max = column(name='ESTE_MAX', type='Double')
        self.has = column(name='HAS', type='Double')

    @property
    def name(self):
        return self.__class__.__name__

    def __str__(self):
        return self.name


class cuadriculas_18(cuadriculas_17):
    """
    Modelo para cuadriculas en zona 18, hereda de cuadriculas_17.
    """
    def __init__(self):
        super().__init__()


class cuadriculas_19(cuadriculas_17):
    """
    Modelo para cuadriculas en zona 19, hereda de cuadriculas_17.
    """
    def __init__(self):
        super().__init__()


class distritos:
    """
    Modelo para tabla/FeatureClass de distritos.
    """
    def __init__(self):
        self.cd_dist = column(name='CD_DIST', type='String')
        self.zona = int()
        self.datum = str()

    @property
    def name(self):
        # Retorna algo como 'dist17c' si zona=17, por ejemplo
        return f"dist{self.zona}c"

    @property
    def path(self):
        # Se asume get_path_input y EXTENTION_SHP existen en connectionsqlite
        return ""#os.path.join(get_path_input(), self.datum, self.name + EXTENTION_SHP)
