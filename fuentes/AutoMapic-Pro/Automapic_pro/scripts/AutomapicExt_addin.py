import os
#from subprocess import call
import sys
import subprocess


_BASE_DIR = os.path.dirname(__file__)
_REQUIREMENTS_DIR = os.path.join(_BASE_DIR, 'require')
_SHELL = True


whl_pip_file = os.path.join(_REQUIREMENTS_DIR, 'pip-20.2.1-py2.py3-none-any.whl')
name_packages = [
    'reportlab-4.0.0-py3-none-any.whl',
    'shapely-2.0.0-cp39-cp39-win32.whl',
    'cx_Oracle-8.3.0-cp39-cp39-win32.whl'
]

# Install packages

def run_command(command):
    """
    Ejecuta un comando en una manera segura y captura la salida.
    """
    try:
        result = subprocess.run([sys.executable] + command, check=True, text=True, capture_output=True)
        return result.stdout
    except subprocess.CalledProcessError as e:
        print("Error al ejecutar el comando:", e)
        return None

def install_package(package):
    """
    Instala un paquete utilizando pip.
    """
    whl_file_path = os.path.join(_REQUIREMENTS_DIR, package)
    return run_command(["-m", "pip", "install", whl_file_path])

def check_and_install_packages():
    """
    Verifica si los paquetes necesarios están instalados y los instala si es necesario.
    """
    # packages_to_install = ['reportlab-4.0.0-py3-none-any.whl']  # Agregar otros paquetes según sea necesario
    try:
        import numpy
        import reportlab
        import shapely
        import cx_Oracle
        # if numpy.__version__ != '1.15.4':
        #     print("Instalando numpy...")
        #     install_package("numpy-1.15.4-py3-none-any.whl")
        # if reportlab.Version != '4.0.0':
        #     print("Instalando reportlab...")
        #     install_package('reportlab-4.0.0-py3-none-any.whl')
        # Agregar otras verificaciones de paquetes aquí
    except ImportError as e:
        print("Paquete no encontrado, instalando:", e.name)
        for package in name_packages:
            install_package(package)

if __name__ == "__main__":
    check_and_install_packages()