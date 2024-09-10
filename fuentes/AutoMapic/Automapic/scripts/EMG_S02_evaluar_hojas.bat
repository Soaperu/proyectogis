@echo off
REM Segun la logica planteada para el proceso de este archivo batch, es necesario
REM que el archivo python y batch tengan el mismo nombre, en caso contrario tendria
REM que editar la variable %script%
REM Si se respeta la primera indicacion no deberia realizar alguna modificacion
REM sobre esta seccion

set base_dir="%~dp0%\.."
set name_current_batch="%~n0"
set scripts="%base_dir%\scripts"
set script="%name_current_batch%.py"

REM En esta seccion debera configurar las variables a utilizar
set python_exe="%1"
set geodatabase="%2"
set hojas="%3"
set evaluador="%~4"

REM En esta seccion, debera insertar sus variables separados por espacios
%python_exe% %scripts%\\%script% %geodatabase% %user% %hojas% %evaluador%
@echo on