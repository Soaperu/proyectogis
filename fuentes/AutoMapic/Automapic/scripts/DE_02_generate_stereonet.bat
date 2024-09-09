@echo off
set scripts= "%~dp0\DE_02_generate_stereonet.py"
rem set scripts= "C:\app_trigger\scripts\run.py"
rem set venv= "%~dp0\venv\Scripts"
rem set venv= "C:\app_trigger\venv\Scripts"
set param="%1"
set python= C:\Python27\ArcGIS10.4\python.exe
rem call %venv%\activate.bat
rem %venv%\python.exe %scripts% %param%
rem call %venv%\deactivate.bat
python %scripts% %param%
@echo on
