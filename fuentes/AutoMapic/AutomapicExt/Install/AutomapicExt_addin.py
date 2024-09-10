import os
from subprocess import call
import sys


_BASE_DIR = os.path.dirname(__file__)
_REQUIREMENTS_DIR = os.path.join(_BASE_DIR, 'require')
_SHELL = True


whl_pip_file = os.path.join(_REQUIREMENTS_DIR, 'pip-20.2.1-py2.py3-none-any.whl')
name_packages = [
    'numpy-1.15.4-cp27-none-win32.whl',
    'functools32-3.2.3-2.tar.gz',
    'six-1.16.0-py2.py3-none-any.whl',
    'cycler-0.10.0-py2.py3-none-any.whl',
    'matplotlib-2.1.0-cp27-cp27m-win32.whl',
    'comtypes-1.1.7-py2-none-any.whl',
    'cx_Oracle-7.0.0-cp27-cp27m-win32.whl',
    'et_xmlfile-1.0.1.tar.gz',
    'jdcal-1.4.1-py2.py3-none-any.whl',
    'openpyxl-2.5.0.tar.gz',
    'GDAL-2.2.4-cp27-cp27m-win32.whl',
    'Shapely-1.6.4.post2-cp27-cp27m-win32.whl',
    'Pillow-6.2.2-cp27-cp27m-win32.whl',
    'reportlab-3.5.59-cp27-cp27m-win32.whl',
    'xmltodict-0.12.0-py2.py3-none-any.whl',
    'mplstereonet-0.5.tar.gz'
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
        p = call('{}\python.exe {}'.format(sys.exec_prefix, command), shell=_SHELL)

    return decorator


@decore_subprocess
def install_pip():
    """
    Funcion decorada con decore_subprocess()
    :return: sentencia para la instalacion de pip desde consola
    """
    return '{}\get-pip.py'.format(_REQUIREMENTS_DIR)


@decore_subprocess
def upgrade_pip():
    """
    Funcion decorada con decore_subprocess()
    :return: sentencia para la actualizacion de pip desde consola
    """
    return '-m pip install --trusted-host=pypi.org --trusted-host=files.pythonhosted.org --user {}'.format(whl_pip_file)


@decore_subprocess
def install_package(package):
    """
    Funcion decorada con decore_subprocess()
    :param package: Modulo o *whl a instalar
    :return: sentencia para la actualizacion de cualquier paquete desde consola
    """
    return '-m pip install --trusted-host=pypi.org --trusted-host=files.pythonhosted.org --user {}'.format(package)


class AutomapicExtClass(object):
    """Implementation for AutomapicExt_addin.AutomapicExtID (Extension)"""
    def __init__(self):
        # For performance considerations, please remove all unused methods in this class.
        self.enabled = True
    def startup(self):
        try:
            import pip
            if pip.__version__ != '20.2.1':
                upgrade_pip()
        except:
            install_pip()

        try:
            import comtypes
            # import matplotlib
            import cx_Oracle
            import openpyxl
            import osgeo
            import reportlab
            import numpy
            if numpy.__version__ != '1.15.4':
                raise RuntimeError('Version incorrecta de numpy')
            # if matplotlib.__version__ != '2.1.0':
            #     raise RuntimeError('Version incorrecta de matplotlib')
            import shapely
            import xmltodict
            import mplstereonet
        except:
            packages = map(lambda i: os.path.join(_REQUIREMENTS_DIR, i), name_packages)
            map(install_package, packages)
    def closeDocument(self):
        pass