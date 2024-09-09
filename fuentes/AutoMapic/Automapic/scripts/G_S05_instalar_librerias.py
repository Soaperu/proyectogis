import os
from subprocess import call
import sys
import settings_aut as st
# import glob


name_packages = [
    'functools32-3.2.3-2.tar.gz',
    'six-1.16.0-py2.py3-none-any.whl',
    'cycler-0.10.0-py2.py3-none-any.whl',
    'matplotlib-2.1.0-cp27-cp27m-win32.whl',
    'comtypes-1.1.7-py2-none-any.whl',
    'cx_Oracle-7.0.0-cp27-cp27m-win32.whl',
    'et_xmlfile-1.0.1.tar.gz',
    'jdcal-1.4.1-py2.py3-none-any.whl',
    'openpyxl-2.5.0.tar.gz',
    'GDAL-2.2.4-cp27-cp27m-win32.whl'
]

# Install packages
def decore_subprocess(func):
    """
    Decora funciones que devuelvan una sentencia ejecutable del consola(cmd)
    :param func: Funcion a decorar
    :return: Nueva funcion
    """

    def decorator(*args):
        command = func(*args)
        p = call('{}\python.exe {}'.format(sys.exec_prefix, command), shell=True)

    return decorator


@decore_subprocess
def install_pip():
    """
    Funcion decorada con decore_subprocess()
    :return: sentencia para la instalacion de pip desde consola
    """
    return '{}\get-pip.py'.format(st._REQUIREMENTS_DIR)


@decore_subprocess
def upgrade_pip():
    """
    Funcion decorada con decore_subprocess()
    :return: sentencia para la actualizacion de pip desde consola
    """
    return '-m easy_install pip==20.2.1'


@decore_subprocess
def install_package(package):
    """
    Funcion decorada con decore_subprocess()
    :param package: Modulo o *whl a instalar
    :return: sentencia para la actualizacion de cualquier paquete desde consola
    """
    return '-m pip install --user {}'.format(package)


if __name__ == '__main__':
    try:
        import pip
        if pip.__version__ != '20.2.1':
            upgrade_pip()
    except:
        install_pip()

    try:
        import comtypes
        import matplotlib
        import cx_Oracle
        import openpyxl
        import osgeo
        if matplotlib.__version__ != '2.1.0':
            raise RuntimeError('Version incorrecta de matplotlib')

        # import pandas as pd
        # arcpy.ImportToolbox(st._BASE_DIR, "T00_automapic")
        # arcpy.ImportToolbox(st._BASE_DIR, "T01_plano_topografico_25k")
        # arcpy.ImportToolbox(st._BASE_DIR, "T02_mapa_peligros_geologicos")
        # arcpy.ImportToolbox(st._BASE_DIR, "T03_mapa_geologico_50k")
    except:
        packages = map(lambda i: os.path.join(st._REQUIREMENTS_DIR, i), name_packages)
        # packages = glob.glob('{}/*.whl'.format(st._REQUIREMENTS_DIR))
        map(install_package, packages)
