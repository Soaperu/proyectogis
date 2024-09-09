--
-- File generated with SQLiteStudio v3.3.3 on mi�. Oct. 6 09:28:17 2021
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: tb_access
CREATE TABLE tb_access (id_user NUMERIC CONSTRAINT id_user_constraint REFERENCES tb_user (id_user), id_modulo NUMERIC CONSTRAINT id_modulo_constraint REFERENCES tb_modulo (id_modulo), id_perfil NUMERIC CONSTRAINT id_perfil_constraint REFERENCES tb_perfil (id_perfil));
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 2, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 1, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (4, 1, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (4, 2, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 2, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (2, 1, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (1, 2, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 3, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 4, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 5, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 6, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 6, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 5, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 4, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (6, 4, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (7, 4, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (8, 7, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (5, 3, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (9, 6, 2);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 7, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (10, 8, 3);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (3, 8, 4);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (11, 5, 2);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (12, 5, 2);
INSERT INTO tb_access (id_user, id_modulo, id_perfil) VALUES (13, 5, 2);

-- Table: tb_config
CREATE TABLE tb_config (id INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL, name VARCHAR (100) NOT NULL, value VARCHAR (250), description VARCHAR (250), state INTEGER, id_modulo NUMERIC CONSTRAINT id_modulo_constraint REFERENCES tb_modulo (id_modulo));
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (1, 'TEMP_FOLDER', NULL, 'Directorio donde se almacenan archivos temporales', 1, 0);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (2, 'GDB_PATH_PT', NULL, 'Ubicaci�n de la base de datos espacial para el proyecto de planos topograficos 25000', 1, 1);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (3, 'SDE_CONN', NULL, 'Archivo de conexi�n a BDGEOCAT', 1, 0);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (4, 'GDB_PATH_HG', '\\Srvfs01\bdgeocientifica$\Addins_Geoprocesos\esriaddin\dgar\geodatabase\gdb_hidrogeologia_template.gdb', 'Ubicaci�n de la base de datos espacial para el proyecto de hidrogeolog�a', 1, 4);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (5, 'GDB_PATH_MG', NULL, 'Ubicaci�n de la base de datos espacial para el proyecto de geolog�a', 1, 3);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (6, 'GDB_PATH_NT', '\\Srvfs01\bdgeocientifica$\Addins_Geoprocesos\esriaddin\dgar\geodatabase\gdb_neotectonica_template.gdb', 'Ubicaci�n de la base de datos espacial para el proyecto de neotectonica', 1, 7);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (7, 'GDB_PATH_GP', '\\Srvfs01\bdgeocientifica$\Addins_Geoprocesos\esriaddin\dgar\geodatabase\gdb_geopatrimonio_template.gdb', 'Ubicaci�n de la base de datos espacial para el proyecto de Geopatrimonio', 1, 8);
INSERT INTO tb_config (id, name, value, description, state, id_modulo) VALUES (8, 'GDB_PATH_MHQ', '\\Srvfs01\bdgeocientifica$\Addins_Geoprocesos\esriaddin\dgar\geodatabase\gdb_hidrogeoquimico_template.gdb', 'Ubicaci�n de la base de datos espacial para el proyecto de mapa hidrogeoqu�mico', 1, 5);

-- Table: tb_layers
CREATE TABLE tb_layers (id BIGINT PRIMARY KEY, description VARCHAR (200), datasource VARCHAR (300), feature VARCHAR (200), parent VARCHAR (200), category INTEGER, state INTEGER, settable INTEGER, layer VARCHAR (200), source VARCHAR (100), "query" VARCHAR (1), typedatasource VARCHAR (50), layer_name VARCHAR (50), withzone INTEGER);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (2, 'Base Ingemmet', NULL, NULL, '999', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (306, 'Secciones', NULL, 'DS_01_hidrogeologia_{0}s\PL_01_seccion_hidrogeologica_{0}s', '3', 1, 0, 1, 'PL_01_seccion_hidrogeologica.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'PL_01_seccion_hidrogeologica', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (204, 'Vias Vecinal (Geocatmin  - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_VECINAL', '2', 1, 0, 9, 'GLI_MTC_VIA_VECINAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_VECINAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (202, 'Vias Nacional (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_NACIONAL', '2', 1, 0, 9, 'GLI_MTC_VIA_NACIONAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_NACIONAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (301, 'Formaciones', NULL, 'DS_01_hidrogeologia_{0}s\PO_01_formacion_hidrogeologica_{1}s', '3', 1, 0, 1, 'PO_01_formacion_hidrogeologica.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'PO_01_formacion_hidrogeologica', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (3, 'Base Hidrogeolog�a', NULL, NULL, '999', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (302, 'Aforos', NULL, 'DS_01_hidrogeologia_{0}s\PT_01_aforos_{1}s', '3', 1, 0, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'PT_01_aforos_{0}s', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (304, 'Ensayo de permeabilidad', NULL, 'DS_01_hidrogeologia_{0}s\PT_01_ensayo_permeabilidad_{1}s', '3', 1, 0, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'PT_01_ensayo_permeabilidad_{0}s', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (305, 'Muestra de rocas', NULL, 'DS_01_hidrogeologia_{0}s\PT_01_muestra_rocas_{1}s', '3', 1, 0, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'PT_01_muestra_rocas_{0}s', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (208, 'Masas de agua :: Hidrograf�a (Geocatmin - IGN)', NULL, 'DATA_GIS.DS_IGN_CARTA_NACIONAL_2011\DATA_GIS.GPO_IGN_HIDROGRAFIA_MASA_AGUA', '2', 1, 0, 9, 'GPO_IGN_HIDROGRAFIA_MASA_AGUA.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_IGN_HIDROGRAFIA_MASA_AGUA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (207, 'Islas :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_IGN_CARTA_NACIONAL_2011\DATA_GIS.GPO_IGN_HIDROGRAFIA_ISLAS', '2', 1, 0, 9, 'GPO_IGN_HIDROGRAFIA_ISLAS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_IGN_HIDROGRAFIA_ISLAS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (206, 'Lagos :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_IGN_CARTA_NACIONAL_2011\DATA_GIS.GPO_IGN_HIDROGRAFIA_LAGOS', '2', 1, 0, 9, 'GPO_IGN_HIDROGRAFIA_LAGOS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_IGN_HIDROGRAFIA_LAGOS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (205, 'R�os :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_IGN_CARTA_NACIONAL_2011\DATA_GIS.GLI_IGN_HIDROGRAFIA_RIOS', '2', 1, 0, 9, 'GLI_IGN_HIDROGRAFIA_RIOS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_IGN_HIDROGRAFIA_RIOS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (203, 'Vias Departamental (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_DEPARTAMENTAL', '2', 1, 0, 9, 'GLI_MTC_VIA_DEPARTAMENTAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_DEPARTAMENTAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (210, 'L�mites provinciales (Geocatmin  - INEI)', NULL, 'DATA_GIS.DS_BASE_CATASTRAL_GEOWGS84\DATA_GIS.GPO_DEP_PROVINCIA', '2', 1, 0, 9, 'GPO_DEP_PROVINCIA.lyr', NULL, '9', 'SDE_WORKSPACE', 'GPO_DEP_PROVINCIA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (211, 'L�mites distritales (Geocatmin  - INEI)', NULL, 'DATA_GIS.DS_BASE_CATASTRAL_GEOWGS84\DATA_GIS.GPO_DEP_DISTRITO', '2', 1, 0, 9, 'GPO_DEP_DISTRITO.lyr', NULL, '9', 'SDE_WORKSPACE', 'GPO_DEP_DISTRITO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (209, 'L�mites departamentales (Geocatmin  - INEI)', NULL, 'DATA_GIS.DS_BASE_CATASTRAL_GEOWGS84\DATA_GIS.GPO_DEP_DEPARTAMENTO', '2', 1, 0, 9, 'GPO_DEP_DEPARTAMENTO.lyr', NULL, '9', 'SDE_WORKSPACE', 'GPO_DEP_DEPARTAMENTO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (213, 'Transporte :: V�as (IGN - DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPL_IGN_TRANSPORTE_VIAS_50K', '2', 1, 0, 9, 'GPL_IGN_TRANSPORTE_VIAS_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPL_IGN_TRANSPORTE_VIAS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (214, 'Curvas de nivel :: Fisiograf�a (IGN - DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPL_IGN_FISIOGRAFIA_CURVAS_50K', '2', 1, 0, 9, 'GPL_IGN_FISIOGRAFIA_CURVAS_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPL_IGN_FISIOGRAFIA_CURVAS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (215, 'Cultural Urbano (DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPO_IGN_CULTURAL_URBANO_50K', '2', 1, 0, 9, 'GPO_IGN_CULTURAL_URBANO_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_IGN_CULTURAL_URBANO_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (216, 'Toponimia cerros (DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPT_IGN_TOPONIMIA_CERROS_50K', '2', 1, 0, 9, 'GPT_IGN_TOPONIMIA_CERROS_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_IGN_TOPONIMIA_CERROS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (217, 'Toponimia pueblos (DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPT_IGN_TOPONIMIA_PUEBLOS_50K', '2', 1, 0, 9, 'GPT_IGN_TOPONIMIA_PUEBLOS_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_IGN_TOPONIMIA_PUEBLOS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (218, 'Hojas 50k (DGR)', NULL, 'DATA_GIS.DS_IGN_GRILLA_WGS84\DATA_GIS.GRID_IGN_50k', '2', 1, 0, 9, 'GPO_DG_HOJAS_50K.lyr', NULL, '9', 'SDE_WORKSPACE', 'GPO_DG_HOJAS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (219, 'Geolog�a 100k (Geocatmin - DGR)', NULL, 'DATA_GIS.DS_GEOLOGIA_INTEGRADA_100K\DATA_GIS.PO_0101003_GEOLOGIA', '2', 1, 0, 9, NULL, NULL, '0', 'SDE_WORKSPACE', 'PO_0101003_GEOLOGIA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (307, 'Inventario de fuentes subterraneas', NULL, 'DS_01_hidrogeologia_{0}s\PT_01_inventario_fuentes_{1}s', '3', 1, 0, 1, 'PT_01_inventario_fuentes.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'PT_01_inventario_fuentes', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (407, 'Puntos de Observaci�n Geol�gica - POG', NULL, 'DS06_GEOLOGIA_{0}S\GPT_DGR_POG_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPT_DGR_POG_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (406, 'F�siles', NULL, 'DS06_GEOLOGIA_{0}S\GPT_DGR_FOSIL_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPT_DGR_FOSIL_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (405, 'Unidades Litol�gicas (geolog�a)', NULL, 'DS06_GEOLOGIA_{0}S\GPO_DGR_ULITO_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPO_DGR_ULITO_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (404, 'L�nea de Secci�n', NULL, 'DS06_GEOLOGIA_{0}S\GPL_DGR_SECCION_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPL_DGR_SECCION_{0}', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (403, 'Pliegue', NULL, 'DS06_GEOLOGIA_{0}S\GPL_DGR_PLIEG_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPL_DGR_PLIEG_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (402, 'Falla Geol�gica', NULL, 'DS06_GEOLOGIA_{0}S\GPL_DGR_FALLA_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPL_DGR_FALLA_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (401, 'Contacto geol�gico', NULL, 'DS06_GEOLOGIA_{0}S\GPL_DGR_CONTAC_{1}S', '4', 2, 1, 1, NULL, NULL, '1', 'FILEGDB_WORKSPACE', 'GPL_DGR_CONTAC_{0}S', 1);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (4, 'Base DGR', NULL, NULL, '999', 2, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (5, 'Base Neotect�nica', NULL, NULL, '999', 3, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (501, 'Capital de distrito', NULL, 'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_CUL_CAP_DIST', '5', 3, 0, 9, 'IGN_CUL_CAP_DIST.lyr', NULL, '0', 'SDE_WORKSPACE', 'IGN_CUL_CAP_DIST', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (503, 'Vias Nacional (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_NACIONAL', '5', 3, 0, 9, 'GLI_MTC_VIA_NACIONAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_NACIONAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (502, 'Vias Vecinal (Geocatmin  - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_VECINAL', '5', 3, 0, 9, 'GLI_MTC_VIA_VECINAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_VECINAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (504, 'Vias Departamental (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_DEPARTAMENTAL', '5', 3, 0, 9, 'GLI_MTC_VIA_DEPARTAMENTAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_DEPARTAMENTAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (505, 'Lagos :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_HIDROGRAFIA\DATA_GIS.GPO_LAG_LAGUNAS', '5', 3, 0, 9, 'GPO_LAG_LAGUNAS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_LAG_LAGUNAS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (506, 'R�os :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_HIDROGRAFIA\DATA_GIS.GLI_RIO_RIOS_LINEA', '5', 3, 0, 9, 'GLI_RIO_RIOS_LINEA.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_RIO_RIOS_LINEA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (507, 'Aeropuertos', NULL, 'DATA_GIS.DS_BASE_CATASTRAL_COMPLEMENTARIO_GEOPSAD56\DATA_GIS.GPT_AER_AEROPUERTO', '5', 3, 0, 9, 'GPT_AER_AEROPUERTO.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_AER_AEROPUERTO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (508, 'Puertos', NULL, 'DATA_GIS.DS_BASE_CATASTRAL_COMPLEMENTARIO_GEOPSAD56\DATA_GIS.GPT_PUE_PUERTOS', '5', 3, 0, 9, 'GPT_PUE_PUERTOS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_PUE_PUERTOS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (509, 'Actividad minera', NULL, 'DATA_GIS.DS_GEOLOGIA_DRME\DATA_GIS.GPT_PYO_PROYECTO_OPERACION', '5', 3, 0, 9, 'GPT_PYO_PROYECTO_OPERACION.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_PYO_PROYECTO_OPERACION', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (510, 'Puentes (Geocatmin  - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GPT_MTC_PUENTE', '5', 3, 0, 9, 'GPT_MTC_PUENTE.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPT_MTC_PUENTE', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (6, 'Base Geopatrimonio', NULL, NULL, '999', 4, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (601, 'Capital de distrito', NULL, 'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_CUL_CAP_DIST', '6', 4, 0, 9, 'IGN_CUL_CAP_DIST.lyr', NULL, '0', 'SDE_WORKSPACE', 'IGN_CUL_CAP_DIST', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (602, 'Capital de provincia', NULL, 'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_CUL_CAP_PROV', '6', 4, 0, 9, 'IGN_CUL_CAP_PROV.lyr', NULL, '0', 'SDE_WORKSPACE', 'IGN_CUL_CAP_PROV', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (603, 'Capital de departamento', NULL, 'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_CUL_CAPITAL_DPTO', '6', 4, 0, 9, 'IGN_CUL_CAPITAL_DPTO.lyr', NULL, '0', 'SDE_WORKSPACE', 'IGN_CUL_CAPITAL_DPTO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (604, 'Curvas de nivel :: Fisiograf�a (IGN - DGR)', NULL, 'DATA_GIS.DS01_BASE_GEOGRAFICA_DGA\DATA_GIS.GPL_IGN_FISIOGRAFIA_CURVAS_50K', '6', 4, 0, 9, 'GPL_IGN_FISIOGRAFIA_CURVAS_50K.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPL_IGN_FISIOGRAFIA_CURVAS_50K', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (605, 'Nevados', NULL, 'DATA_GIS.DS_HIDROGRAFIA\DATA_GIS.GPO_NEV_NEVADOS', '6', 4, 0, 9, 'GPO_NEV_NEVADOS.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_NEV_NEVADOS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (606, 'Vias Vecinal (Geocatmin  - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_VECINAL', '6', 4, 0, 9, 'GLI_MTC_VIA_VECINAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_VECINAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (607, 'Vias Departamental (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_DEPARTAMENTAL', '6', 4, 0, 9, 'GLI_MTC_VIA_DEPARTAMENTAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_DEPARTAMENTAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (608, 'Vias Nacional (Geocatmin - MTC)', NULL, 'DATA_GIS.DS_OTRAS_FUENTES\DATA_GIS.GLI_MTC_VIA_NACIONAL', '6', 4, 0, 9, 'GLI_MTC_VIA_NACIONAL.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_MTC_VIA_NACIONAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (609, 'R�os :: Hidrograf�a (Geocatmin  - IGN)', NULL, 'DATA_GIS.DS_HIDROGRAFIA\DATA_GIS.GLI_RIO_RIOS_LINEA', '6', 4, 0, 9, 'GLI_RIO_RIOS_LINEA.lyr', NULL, '0', 'SDE_WORKSPACE', 'GLI_RIO_RIOS_LINEA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (610, 'Zona urbana', NULL, 'DATA_GIS.DS_CATASTRO_MINERO_GEOWGS84\DATA_GIS.GPO_ZUR_ZONA_URBANA_G84', '6', 4, 0, 9, 'GPO_ZUR_ZONA_URBANA_G84.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_ZUR_ZONA_URBANA_G84', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (611, 'Area reservada', NULL, 'DATA_GIS.DS_CATASTRO_MINERO_GEOWGS84\DATA_GIS.GPO_ARE_AREA_RESERVADA_G84', '6', 4, 0, 9, 'GPO_ARE_AREA_RESERVADA_G84.lyr', NULL, '0', 'SDE_WORKSPACE', 'GPO_ARE_AREA_RESERVADA_G84', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (612, 'Lagos y lagunas', NULL, 'DATA_GIS.DS_IGN_BASE_PERU_500000\DATA_GIS.IGN_HID_LAGO_LAGUNA', '6', 4, 0, 9, 'IGN_HID_LAGO_LAGUNA.lyr', NULL, '0', 'SDE_WORKSPACE', 'IGN_HID_LAGO_LAGUNA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (613, 'dem', NULL, 'DATA_GIS.RS_SRTM_PERU', '6', 4, 0, 9, 'RS_SRTM_PERU.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'RS_SRTM_PERU', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (614, 'hillshade', NULL, 'DATA_GIS.RS_IMG_HILLSHADE_PERU', '6', 4, 0, 9, 'RS_IMG_HILLSHADE_PERU.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'RS_IMG_HILLSHADE_PERU', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (511, 'hillshade', NULL, 'DATA_GIS.RS_IMG_HILLSHADE_PERU', '5', 3, 0, 9, 'RS_IMG_HILLSHADE_PERU.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'RS_IMG_HILLSHADE_PERU', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (7, 'Mapa Hidrogeoqu�mico', NULL, NULL, '999', 7, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (701, 'PT_CAPITAL_DISTRITAL', NULL, 'DS_CAPAS_GENERALES\PT_CAPITAL_DISTRITAL', '7', 7, 0, 1, 'PT_CAPITAL_DISTRITAL.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PT_CAPITAL_DISTRITAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (702, 'PT_01_YACIMIENTOS_MINEROS', NULL, 'DS_CAPAS_GENERALES\PT_01_YACIMIENTOS_MINEROS', '7', 7, 0, 1, 'PT_01_YACIMIENTOS_MINEROS.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PT_01_YACIMIENTOS_MINEROS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (703, 'PT_02_PASIVOS_AMBIENTALES_MINEROS', NULL, 'DS_CAPAS_GENERALES\PT_02_PASIVOS_AMBIENTALES_MINEROS', '7', 7, 0, 1, 'PT_02_PASIVOS_AMBIENTALES_MINEROS.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PT_02_PASIVOS_AMBIENTALES_MINEROS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (704, 'ESTACIONES', NULL, 'BASE_EXCEL_LAB_FC', '7', 7, 0, 1, 'ESTACIONES.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'ESTACIONES', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (705, 'HIDROTIPOS', NULL, 'BASE_EXCEL_LAB_FC', '7', 7, 0, 1, 'HIDROTIPOS.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'HIDROTIPOS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (706, 'drenaje', NULL, 'DATA_GIS.DS_IGN_CARTA_NACIONAL_2011\DATA_GIS.GLI_IGN_HIDROGRAFIA_RIOS', '7', 7, 0, 9, 'drenaje.lyr', NULL, '0', 'SDE_WORKSPACE', 'drenaje', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (707, 'Vias_PL_03_VIAS_VECINALES', NULL, 'DS_CAPAS_GENERALES\Vias_PL_03_VIAS_VECINALES', '7', 7, 0, 1, 'Vias_PL_03_VIAS_VECINALES.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'Vias_PL_03_VIAS_VECINALES', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (708, 'Vias_PL_04_VIAS_DISTRITALES', NULL, 'DS_CAPAS_GENERALES\Vias_PL_04_VIAS_DISTRITALES', '7', 7, 0, 1, 'Vias_PL_04_VIAS_DISTRITALES.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'Vias_PL_04_VIAS_DISTRITALES', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (709, 'Vias_PL_05_VIAS_NACIONALES', NULL, 'DS_CAPAS_GENERALES\Vias_PL_05_VIAS_NACIONALES', '7', 7, 0, 1, 'Vias_PL_05_VIAS_NACIONALES.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'Vias_PL_05_VIAS_NACIONALES', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (710, 'PO_LAGUNA', NULL, 'DS_CAPAS_GENERALES\PO_LAGUNA', '7', 7, 0, 1, 'PO_LAGUNA.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PO_LAGUNA', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (711, 'PO_BOFEDAL', NULL, 'DS_CAPAS_GENERALES\PO_BOFEDAL', '7', 7, 0, 1, 'PO_BOFEDAL.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PO_BOFEDAL', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (712, 'PO_CENTRO_URBANO', NULL, 'DS_CAPAS_GENERALES\PO_CENTRO_URBANO', '7', 7, 0, 1, 'PO_CENTRO_URBANO.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PO_CENTRO_URBANO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (713, 'PO_MICROCUENCAS', NULL, 'DS_CAPAS_GENERALES\PO_MICROCUENCAS', '7', 7, 0, 1, 'PO_MICROCUENCAS.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'PO_MICROCUENCAS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (714, 'PO_ZONA_ESTUDIO', NULL, 'DS_CAPAS_GENERALES\PO_SUBCUENCAS', '7', 7, 0, 1, 'PO_ZONA_ESTUDIO.lyr', NULL, '1', 'FILEGDB_WORKSPACE', 'PO_ZONA_ESTUDIO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (715, 'PL_curvas_nivel_47C', NULL, 'DS_CAPAS_GENERALES\PL_curvas_nivel_47C', '7', 7, 0, 1, 'PL_curvas_nivel_47C.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PL_curvas_nivel_47C', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (716, 'PL_BUZAMIENTO', NULL, 'DS_CAPAS_GENERALES\PL_BUZAMIENTO', '7', 7, 0, 1, 'PL_BUZAMIENTO.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PL_BUZAMIENTO', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (717, 'PL_PLIEGUES', NULL, 'DS_CAPAS_GENERALES\PL_PLIEGUES', '7', 7, 0, 1, 'PL_PLIEGUES.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PL_PLIEGUES', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (718, 'PL_FALLAS', NULL, 'DS_CAPAS_GENERALES\PL_FALLAS', '7', 7, 0, 1, 'PL_FALLAS.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PL_FALLAS', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (719, 'PO_GEOLOGIA_47C', NULL, 'DS_CAPAS_GENERALES\PO_GEOLOGIA_47C', '7', 7, 0, 1, 'PO_GEOLOGIA_47C.lyr', NULL, '0', 'FILEGDB_WORKSPACE', 'PO_GEOLOGIA_47C', NULL);
INSERT INTO tb_layers (id, description, datasource, feature, parent, category, state, settable, layer, source, "query", typedatasource, layer_name, withzone) VALUES (720, 'RS_IMG_HILLSHADE_PERU', NULL, 'DATA_GIS.RS_IMG_HILLSHADE_PERU', '7', 7, 0, 9, 'RS_IMG_HILLSHADE_PERU.lyr', NULL, '0', 'SDE_WORKSPACE', 'RS_IMG_HILLSHADE_PERU', NULL);

-- Table: tb_mhq_facies_rgb
CREATE TABLE tb_mhq_facies_rgb (ID TEXT (2) PRIMARY KEY, R DOUBLE, G DOUBLE, B DOUBLE);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('11', 0.0, 176.0, 240.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('13', 0.0, 32.0, 80.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('12', 50.0, 74.0, 178.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('21', 255.0, 255.0, 0.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('22', 255.0, 102.0, 0.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('23', 154.0, 105.0, 0.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('33', 0.0, 128.0, 0.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('32', 0.0, 157.0, 113.0);
INSERT INTO tb_mhq_facies_rgb (ID, R, G, B) VALUES ('31', 86.0, 130.0, 0.0);

-- Table: tb_mhq_gdb_dominios
CREATE TABLE "tb_mhq_gdb_dominios" (
"DOMINIO" TEXT,
  "KEY" TEXT,
  "VALUE" TEXT
);
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_POBL', 'POBL_URB', 'urbana');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_POBL', 'POBL_RUR', 'rural');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ESTANDAR', '1', 'tipo i');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ESTANDAR', '2', 'tipo ii');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ZONA', '19', '19');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ZONA', '18', '18');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ZONA', '17', '17');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_TIP_FUENTE', 'TIP_FT_EFL', 'efluente');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_TIP_FUENTE', 'TIP_FT_SUB', 'subterraneo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_TIP_FUENTE', 'TIP_FT_SUP', 'superficial');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_016', 'geisser');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_008', 'manantial');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_009', 'manantial captado');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_006', 'represa');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_007', 'humedal');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_004', 'laguna');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_005', 'lago');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_002', 'quebrada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_003', 'canal');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_013', 'labor minera abandonada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_001', 'rio');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_010', 'fuente termal');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_015', 'bofedal');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_011', 'pozos');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_014', 'galeria filtrante');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_012', 'sondeos');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_CLAS_FUENT', 'CLS_FT_017', 'punto de control');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'COLINA', 'colina');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'MONTANA', 'monta�a');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'PIS_VALL', 'piso de valle');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'PIE_MONT', 'pie de monte');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'PLANICIE', 'planicie');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'CANON', 'ca��n');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'ALTIP', 'altiplanicie');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'LAD_ESCAR', 'ladera escarpada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'LITORAL', 'litoral');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'LADERA', 'ladera');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GEOMORF', 'TERRAZA', 'terraza');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_BLANCO', '1', 'blanco de campo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_BLANCO', '2', 'blanco viajero');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_BOOLEAN', '1', 'si');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_BOOLEAN', '0', 'no');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_PENDIENTE', 'PEND_004', 'muy fuerte');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_PENDIENTE', 'PEND_002', 'media');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_PENDIENTE', 'PEND_003', 'fuerte');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_PENDIENTE', 'PEND_001', 'baja');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '11', 'bicarbonatada c�lcica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '13', 'bicarbonatada s�dica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '12', 'bicarbonatada magn�sica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '21', 'sulfatada c�lcica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '22', 'sulfatada magn�sica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '23', 'sulfatada s�dica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '33', 'clorurada s�dica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '32', 'clorurada magn�sica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '31', 'clorurada c�lcica');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '1', 'bicarbonatada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '3', 'clorurada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_FAC_HIDRO', '2', 'sulfatada');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_001', 'qu�mico');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_003', 'radioactivo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_002', 'isot�pico');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_005', 'qu�mico/radioactivo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_004', 'qu�mico/isot�pico');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_007', 'qu�mico/isot�pico/radioactivo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_ANALI', 'ANALIS_006', 'isot�pico/radioactivo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GRAD_FRAC', 'ALTO', 'alto');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GRAD_FRAC', 'BAJO', 'bajo');
INSERT INTO tb_mhq_gdb_dominios (DOMINIO, "KEY", VALUE) VALUES ('DOM_GRAD_FRAC', 'MEDIO', 'medio');

-- Table: tb_mhq_gdb_relacion_campos
CREATE TABLE "tb_mhq_gdb_relacion_campos" (
"campo_lab" TEXT,
  "campo_lab_2" TEXT,
  "campo_fc" TEXT,
  "tipo" TEXT,
  "largo" REAL,
  "dominio" TEXT
);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ficha', 'Ficha', 'N_FICHA', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('C�digo', 'C�digo', 'CODIGO', 'TEXT', 18.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('C�digo_Corto', 'Proyecto', 'COD_PROY', 'TEXT', 18.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Nombre', 'Nombre', 'Nombre_corto', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Nombre_completo', 'Nombre completo', 'NOMBRE', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Fecha', 'Fecha', 'FECHA', 'DATE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Hora', 'Hora', 'HORA', 'DATE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Norte', 'Norte', 'NORTE', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Este', 'Este', 'ESTE', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cota', 'Cota', 'COTA', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Zona', 'Zona', 'ZONA', 'TEXT', 2.0, 'DOM_ZONA');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Lugar', 'Lugar', 'LOCALIDAD', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Distrito ', 'Distrito', 'NM_DIST', 'TEXT', 50.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Provincia', 'Provincia', 'NM_PROV', 'TEXT', 50.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Vertiente', 'Vertiente', 'REG_HIDR', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cuenca', 'Cuenca', 'CU_IN_HIDR', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Subcuenca', 'Subcuenca', 'SUBCUENCA', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Microcuenca  ', 'Microcuenca', 'MICROCU', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Tipo_fuente', 'Tipo de fuente', 'TIP_FUENTE', 'TEXT', 10.0, 'DOM_TIP_FUENTE');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Clase_fuente', 'Clase de fuente', 'CLAS_FUENT', 'TEXT', 10.0, 'DOM_CLAS_FUENT');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Uso', 'Uso de la fuente', 'USO_FUENTE', 'TEXT', 250.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Toma_par�metros', 'Medida de par�metros f�sico-qu�micos', 'PARAMETROS', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Tipo_an�lisis', 'Tipo de an�lisis', 'ANALISIS', 'TEXT', 10.0, 'DOM_ANALI');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Muestreo', 'Muestreo', 'MUESTREO', 'TEXT', 50.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Monitoreo', 'Monitoreo', 'MONITOREO', 'TEXT', 50.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Blanco', 'Blanco', 'BLANCO', 'TEXT', 50.0, 'DOM_BLANCO');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Estandar', 'Estandar', 'ESTANDAR', 'TEXT', 50.0, 'DOM_ESTANDAR');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Duplicado', 'Duplicado', 'DUPLICADO', 'TEXT', 50.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Aspecto_Geol�gico', 'Aspecto Geol�gico', 'GEOLOGIA', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Descripci�n_Litol�gica ', 'Descripci�n Litol�gica', 'LITOLOGIA', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Morfolog�a', 'Morfolog�a', 'GEOMORFO', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Pendiente', 'Pendiente', 'PENDIENTE', 'TEXT', 8.0, 'DOM_PENDIENTE');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Caudal(Q) ', 'Caudal(Q)', 'CAUDAL_LS', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Color', 'Color', 'COLOR', 'TEXT', 100.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Olor', 'Olor', 'OLOR', 'TEXT', 100.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('T_fuente', 'Temp. de la Fuente', 'TEMP_FUEN', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('T_ambiente ', 'Temp. Ambiente', 'TEMP_AMBI', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('pH', 'pH', 'PH', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('pH_mV', 'pH (mV)', 'PH_MV', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Eh', 'Eh', 'EH', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('ORP_mv', 'ORP_mv', 'ORP', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('CE_uS_cm', 'CE_uS/cm', 'CE', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('TDS_mg_L', 'TDS_mg/L', 'TDS', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Salinidad_PSU', 'Salinidad_PSU', 'SALINIDAD', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Resistividad_Kohm_cm', 'Resistividad_Kohm-cm', 'RESIST', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('OD_mgL ', 'OD_mgL', 'RDO', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('OD_%Sat. ', 'OD_%Sat.', 'OD', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Precipitados', 'Precipitados', 'PRECIPIT', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Algas_o_plantas', 'Presencia de Algas o plantas', 'PRES_AL_PL', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Presencia_basurales', 'Presencia de basurales', 'PRES_BASUR', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Presencia_animales', 'Presencia de animales', 'PRES_ANI', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Letrinas_silos', 'Letrinas/S�tios', 'LETR_SILO', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Poblaci�n', 'Poblaci�n', 'POBLACION', 'TEXT', 8.0, 'DOM_POBL');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Pasivos_Ambientales', 'Pasivos Ambientales', 'PASIV_AMB', 'TEXT', 1.0, 'DOM_BOOLEAN');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Actividad_antr�pica', 'Actividad antr�pica', 'ACT_ANTRO', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Alteraci�n_geol�gica', 'Zona de alteraci�n geol�gica natural', 'ALT_GEO', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Eventos_metereologicos', 'Eventos metereologicos considerables antes del muestreo', 'METEOROLOG', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Viento', 'Viento durante el muestreo', 'VIENTO', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Archico Fotogr�fico', 'Archico Fotogr�fico', 'FOTO', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Observaciones ', 'Observaciones', 'OBS', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Realizado por', 'Realizado por', 'REALIZ_POR', 'TEXT', 500.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Alcalinidad_mgCaCO3_l', 'Alcalinidad  (mgCaCO3/L)', 'Alcalinidad_carbonatos', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('CO3= (mg/L)', 'CO3= (mg/L)', 'CO3', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('HCO3-', 'HCO3-', 'HCO3', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('F-', 'F-', 'F_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cl-', 'Cl-', 'ION_Cl', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('NO3-', 'NO3-', 'NO3', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('SO4=', 'SO4=', 'SO4', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('NO2-', 'NO2-', 'NO2', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Na_dis', 'Na_dis', 'Na_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mg_dis        ', 'Mg_dis', 'Mg_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('K_dis', 'K_dis', 'K_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ca_dis', 'Ca_dis', 'Ca_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sr_dis        ', 'Sr_dis', 'Sr_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Li_dis', 'Li_dis', 'Li_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('SiO2 _dis       ', 'SiO2 _dis', 'SiO2_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ag_dis', 'Ag_dis', 'Ag_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Al_dis', 'Al_dis', 'Al_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('As_dis', 'As_dis', 'As_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('B_dis', 'B_dis', 'B_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ba_dis', 'Ba_dis', 'Ba_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Be_dis', 'Be_dis', 'Be_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Bi_dis', 'Bi_dis', 'Bi_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cd_dis', 'Cd_dis', 'Cd_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ce_dis', 'Ce_dis', 'Ce_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Co_dis', 'Co_dis', 'Co_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cr_dis', 'Cr_dis', 'Cr_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cu_dis', 'Cu_dis', 'Cu_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Fe_dis', 'Fe_dis', 'Fe_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Hg_dis', 'Hg_dis', 'Hg_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('La_dis', 'La_dis', 'La_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mn_dis', 'Mn_dis', 'Mn_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mo_dis', 'Mo_dis', 'Mo_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ni_dis', 'Ni_dis', 'Ni_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Pb_dis', 'Pb_dis', 'Pb_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('S_dis', 'S_dis', 'S_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sb_dis', 'Sb_dis', 'Sb_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Se_dis', 'Se_dis', 'Se_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sn_dis', 'Sn_dis', 'Sn_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Th_dis', 'Th_dis', 'Th_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ti_dis', 'Ti_dis', 'Ti_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Tl_dis', 'Tl_dis', 'Tl_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('U_dis', 'U_dis', 'U_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('V_dis', 'V_dis', 'V_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('W_dis', 'W_dis', 'W_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Y_dis', 'Y_dis', 'Y_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Zn_dis', 'Zn_dis', 'Zn_DIS', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Na_tot', 'Na_tot', 'Na__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mg_tot         ', 'Mg_tot', 'Mg__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('K_tot', 'K_tot', 'K__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ca_tot', 'Ca_tot', 'Ca__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sr_tot        ', 'Sr_tot', 'Sr__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Li_tot', 'Li_tot', 'Li__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('SiO2_tot       ', 'SiO2_tot', 'SiO2__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ag_tot ', 'Ag_tot', 'Ag_TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Al_tot', 'Al_tot', 'Al_TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('As_tot', 'As_tot', 'As__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('B_tot', 'B_tot', 'B__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ba_tot', 'Ba_tot', 'Ba__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Be_tot', 'Be_tot', 'Be__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Bi_tot', 'Bi_tot', 'Bi__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cd_tot', 'Cd_tot', 'Cd__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ce_tot', 'Ce_tot', 'Ce__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Co_tot', 'Co_tot', 'Co__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cr_tot', 'Cr_tot', 'Cr__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Cu_tot', 'Cu_tot', 'Cu__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Fe_tot', 'Fe_tot', 'Fe__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Hg_tot', 'Hg_tot', 'Hg__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('La_tot', 'La_tot', 'La__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mn_tot', 'Mn_tot', 'Mn__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Mo_tot', 'Mo_tot', 'Mo__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ni_tot', 'Ni_tot', 'Ni__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Pb_tot', 'Pb_tot', 'Pb__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('S_tot', 'S_tot', 'S__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sb_tot', 'Sb_tot', 'Sb__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Se_tot', 'Se_tot', 'Se__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Sn_tot', 'Sn_tot', 'Sn__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Th_tot', 'Th_tot', 'Th__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Ti_tot', 'Ti_tot', 'Ti__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Tl_tot', 'Tl_tot', 'Tl__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('U_tot', 'U_tot', 'U__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('V_tot', 'V_tot', 'V__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('W_tot', 'W_tot', 'W__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Y_tot', 'Y_tot', 'Y__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('Zn_tot', 'Zn_tot', 'Zn__TOT', 'TEXT', 9.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES ('HIDROTIPO', 'HIDROTIPO', 'FAC_HIDRO', 'TEXT', 2.0, 'DOM_FAC_HIDRO');
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, NULL, 'TEMPORADA', 'TEXT', 7.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, NULL, 'LATITUD', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, NULL, 'LONGITUD', 'DOUBLE', NULL, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Ca_meq/l', 'Ca_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Mg_meq/l', 'Mg_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Na_meq/l', 'Na_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'K_meq/l', 'K_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Suma_meq/l_cat', 'SUMA_MEQL_CAT', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'HCO3_meq/l', 'HCO3_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'CO3_meq/l', 'CO3_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'SO4_meq/l', 'SO4_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Cl_meq/l', 'ION_Cl_MEQL', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Suma_meq/l_ani', 'SUMA_MEQL_ANI', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'BI_%', 'BI_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Ca_%', 'Ca_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Mg_%', 'Mg_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Na+K_%', 'Na_K_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'HCO3+CO3_%', 'HCO3_CO3_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'SO4_%', 'SO4_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Cl_%', 'Cl_MEQP', 'TEXT', 20.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Cod_Cuen', 'COD_CUENCA', 'TEXT', 12.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, 'Cod_Subc', 'COD_SUBC', 'TEXT', 15.0, NULL);
INSERT INTO tb_mhq_gdb_relacion_campos (campo_lab, campo_lab_2, campo_fc, tipo, largo, dominio) VALUES (NULL, NULL, 'COD_MICROC', 'TEXT', 20.0, NULL);

-- Table: tb_modulo
CREATE TABLE tb_modulo (id_modulo NUMERIC PRIMARY KEY UNIQUE NOT NULL, nombre VARCHAR (50) UNIQUE NOT NULL, descrip VARCHAR (200), orden INTEGER NOT NULL, estado INTEGER NOT NULL);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (2, 'Peligros Geol�gicos', NULL, 2, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (1, 'Plano Topogr�fico 25000', NULL, 1, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (0, 'main', NULL, 0, 0);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (3, 'Mapa Geol�gico 50000', NULL, 3, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (6, 'Sincronizaci�n Geodatabase', NULL, 99, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (5, 'Mapa Hidrogeoqu�mico', NULL, 5, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (4, 'Mapa Hidrogeol�gico', NULL, 4, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (7, 'Mapa Neotect�nico', NULL, 6, 1);
INSERT INTO tb_modulo (id_modulo, nombre, descrip, orden, estado) VALUES (8, 'Geopatrimonio', NULL, 7, 1);

-- Table: tb_oficina
CREATE TABLE tb_oficina (id_oficina NUMERIC PRIMARY KEY UNIQUE NOT NULL, nombre VARCHAR (20) UNIQUE NOT NULL, acronimo VARCHAR (20), parent NUMERIC);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (2, 'Oficina de Sistemas de Informaci�n', 'OSI', NULL);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (1, 'Direcci�n de Geolog�a Ambiental y Riesgo Geol�gico', 'DGAR', NULL);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (3, 'Cartograf�a', NULL, 2);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (5, 'Hidrogeolog�a', NULL, 1);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (4, 'Peligros Geol�gicos', NULL, 1);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (6, 'Direcci�n de Geolog�a Regional', 'DGR', NULL);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (7, 'Neotect�nica', NULL, 1);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (8, 'Geopatrimonio', NULL, 1);
INSERT INTO tb_oficina (id_oficina, nombre, acronimo, parent) VALUES (9, 'L�nea Base Geoambiental', NULL, 1);

-- Table: tb_opcion
CREATE TABLE tb_opcion (id_opcion NUMERIC PRIMARY KEY NOT NULL UNIQUE, "key" INTEGER NOT NULL, value VARCHAR (50) NOT NULL, "group" INT NOT NULL);

-- Table: tb_perfil
CREATE TABLE tb_perfil (id_perfil NUMERIC PRIMARY KEY NOT NULL UNIQUE, nombre VARCHAR (20) UNIQUE NOT NULL);
INSERT INTO tb_perfil (id_perfil, nombre) VALUES (4, 'developer');
INSERT INTO tb_perfil (id_perfil, nombre) VALUES (3, 'admin');
INSERT INTO tb_perfil (id_perfil, nombre) VALUES (2, 'editor');
INSERT INTO tb_perfil (id_perfil, nombre) VALUES (1, 'view');

-- Table: tb_topology
CREATE TABLE tb_topology (id BIGINT PRIMARY KEY, name VARCHAR (50), id_modulo CONSTRAINT id_modulo_constraint REFERENCES tb_modulo (id_modulo));
INSERT INTO tb_topology (id, name, id_modulo) VALUES (2, '�reas sin datos (holes)', 3);
INSERT INTO tb_topology (id, name, id_modulo) VALUES (1, '�reas superpuestas (overlaps)', 3);

-- Table: tb_user
CREATE TABLE tb_user (id_user NUMERIC PRIMARY KEY UNIQUE NOT NULL, user VARCHAR (20) NOT NULL, password VARCHAR (50), id_oficina NUMERIC CONSTRAINT id_oficina_constraint REFERENCES tb_oficina (id_oficina), estado NUMERIC, login INTEGER);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (5, 'jyupanqui', NULL, 2, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (4, 'jsalcedo', NULL, 2, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (3, 'autonomoosi02', NULL, 2, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (2, 'slu', NULL, 3, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (1, 'gluque', NULL, 4, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (6, 'bquispe', NULL, 5, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (7, 'bcastillo', NULL, 5, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (8, 'apalomino', NULL, 1, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (9, 'hcastro', NULL, 2, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (10, 'iastete', NULL, 8, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (11, 'jortiz', NULL, 9, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (12, 'mcarrasco', NULL, 9, 1, 0);
INSERT INTO tb_user (id_user, user, password, id_oficina, estado, login) VALUES (13, 'autonomodgar02', NULL, 9, 1, 0);

-- View: vw_access
CREATE VIEW vw_access AS select A.id_user, B.user, A.id_modulo, C.nombre modulo, A.id_perfil, D.nombre perfil from tb_access A left join tb_user B on A.id_user = B.id_user
left join tb_modulo C on A.id_modulo = C.id_modulo left join tb_perfil D on A.id_perfil = D.id_perfil where B.estado = 1 and C.estado = 1 order by C.orden;

-- View: vw_colores_facies
CREATE VIEW vw_colores_facies AS SELECT A.KEY, A.VALUE, B.R, B.G, B.B FROM tb_mhq_gdb_dominios A LEFT JOIN tb_mhq_facies_rgb B ON A.KEY=B.ID WHERE A.DOMINIO='DOM_FAC_HIDRO' AND LENGTH(A.KEY)>1;

-- View: vw_user_config
CREATE VIEW vw_user_config AS select * from (
select A.user, B.id config, B.name from vw_access A left join tb_config B on A.id_modulo = B.id_modulo where B.id_modulo <> 0
union all
select A.user, B.id config, B.name from tb_user A, tb_config B where B.id_modulo = 0 and B.id not in (3)
union all
select distinct A.user, C.id config, C.name from tb_user A,
(select distinct id_user from tb_access where id_perfil = 4) B, tb_config C where A.id_user = B.id_user and C.id = 3)
order by user, config;

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
