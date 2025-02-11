# -*- coding: utf-8 -*-
"""
Código de configuración de rutas y variables adaptado a Python 3 (ArcGIS Pro).
"""

import sys
from uuid import getnode as get_mac
import os
import arcpy


# -------------------------------------------------------------------------
# RUTAS Y VARIABLES BÁSICAS
# -------------------------------------------------------------------------

# Directorio base, ascendiendo dos niveles desde el archivo actual
BASE_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), '..', '..'))

# Variable de entorno (posiblemente usada en tu sistema)
SET_ENV_DIRS = 'SET_ENVIROMENT_DIRS_CADEMTER'

# MAC Address
MAC_ADDRESS = get_mac()

# ¿Está habilitado este entorno?
ENABLED = True

# Rutas a python.exe y pip.exe dentro del mismo env. de ArcGIS Pro
PIP_EXE = os.path.join(sys.exec_prefix, 'Scripts', 'pip.exe')
PYTHON_EXE = os.path.join(sys.exec_prefix, 'python.exe')

# Base de datos SQLite
SQLITE_DB = os.path.join(BASE_DIR, 'scripts', 'cademter.sqlite')

# -------------------------------------------------------------------------
# OTRAS RUTAS Y ARCHIVOS
# -------------------------------------------------------------------------

STATIC_DIR = os.path.join(BASE_DIR, 'statics')
ABOUT_FORM = os.path.join(STATIC_DIR, 'forms', 'presentacion_cademter.exe')
LOADER_FORM = os.path.join(STATIC_DIR, 'forms', 'loader_cademter.exe')
REPORT_CHECK_STRUCTURE_HTML = os.path.join(STATIC_DIR, 'reports', 'report_check_structure.html')
STRUCTURE_DIR = os.path.join(STATIC_DIR, 'structure')
FILE_GDB = os.path.join(STATIC_DIR, 'insumos.gdb')
REQUIREMENTS_DIR = os.path.join(STATIC_DIR, 'requirements')

# Carpeta temporal de ArcPy
TEMP_DIR = arcpy.env.scratchFolder

# Extensiones usadas
EXTENTION_SHP = '.shp'
EXTENTION_TXT = '.txt'
EXTENTION_HTML = '.html'
EXTENTION_LOG = '.log'
EXTENTION_CSV = '.csv'
EXTENTION_PNG = '.png'

# Directorios/etiquetas para datum
FOLDER_PSAD = 'psad56'
FOLDER_WGS = 'wgs84'

# Códigos EPSG
EPSG_PSAD = 24860  # Ajusta si necesitas otro ID
EPSG_WGS = 32700  # Ajusta si necesitas otro ID base

# -------------------------------------------------------------------------
# CLASES AUXILIARES
# -------------------------------------------------------------------------
class Suplies:
    """
    Clase que define los nombres de shapefiles o feature classes
    para mar, países, región, regiones y cuadrantes.
    """
    sea = 'mar'
    countries = 'paises'
    region = 'region'
    regions = 'regiones'
    quads = 'cuadrantes'
