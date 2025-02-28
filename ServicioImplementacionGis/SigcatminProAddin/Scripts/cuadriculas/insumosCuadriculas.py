# -*- coding: utf-8 -*-
"""
Script que prepara los insumos (regiones, cuadrículas, mar, fronteras, etc.)
para el posterior proceso de 'cálculo de límites regionales' en ArcGIS Pro (Python 3).
"""
import os
import uuid
import arcpy
from shutil import rmtree
import json
import traceback

# Se asume que 'model.py' y otros módulos están en el mismo entorno
from model import (regiones, zonas_geograficas, FeatureDatasets, cuadriculas_17,
                   cuadriculas_18, cuadriculas_19)

from config import (Suplies, EPSG_WGS, EPSG_PSAD, FOLDER_WGS, EXTENTION_SHP)
arcpy.env.parallelProcessingFactor = "60%"
arcpy.env.overwriteOutput = True
response = dict()

class GeneradorInsumos(object):

    # Lee parámetros desde la herramienta en ArcGIS Pro:
    datums = arcpy.GetParameterAsText(0).split(';')  # Ej: "wgs84;psad56"
    # new_folder = True if arcpy.GetParameterAsText(1).lower() == 'true' else False
    # Si la herramienta indica 'nueva carpeta', entonces actualiza nombre de directorio (función del model)
    # if new_folder:
    #     update_name_dir()

    # Se definen variables de clase iniciales
    _input_dir = arcpy.GetParameterAsText(1)                   # Ruta a la carpeta de insumos
    _model_region = regiones()
    _path_region = None
    _path_region_fc = None
    _model_zona_geografica = zonas_geograficas()
    _path_zona_geografica = None
    _dataset = FeatureDatasets()
    _work_dir = ""
    _datum_dir = ""
    _zones = range(17, 20)                          # Zonas 17,18,19
    _cod_mar = "99"
    _cod_frontera = "99"
    _name_mar = "MAR"
    _name_frontera = "FUERA DEL PERU"
    _insumos = Suplies()

    def agrupar_regiones_por_zona(self, datum):
        """
        Exporta el grupo de regiones que comparten la misma zona.
        Crea un Feature Class por cada zona, guardándolas en la
        File Geodatabase (_dataset.path) con nombre = region + datum + zona.
        """
        assert os.path.exists(self._path_zona_geografica), (
            f"No se encuentra el archivo de zona geográfica: {self._path_zona_geografica}"
        )

        # Evitar regiones que tengan el nombre de frontera o mar
        _query = f"{self._model_region.nm_depa.name} NOT IN ('{self._name_frontera}', '{self._name_mar}')"

        feature_layer_region = arcpy.MakeFeatureLayer_management(
            self._path_region,
            self._model_region.name,
            _query
        )

        # Recorre las zonas en el shapefile/feature class de zona geográfica
        with arcpy.da.SearchCursor(
                                        self._path_zona_geografica,
                                        ["SHAPE@", self._model_zona_geografica.zona.name]
                                    ) as array:
            for shp_zona, val_zona in array:
                self._dataset.zone = val_zona
                self._dataset.datum = datum

                # Construye la ruta al FC en la FGDB: path + "regiones" + datum + zona
                path = os.path.join(
                                        self._input_dir,#self._dataset.path,
                                        f"{self._model_region.name}{datum}{val_zona}"
                                    )

                # Filtra las regiones que solapan con la zona
                oid = []
                with arcpy.da.SearchCursor(feature_layer_region, [self._model_region.cd_depa.name, 'SHAPE@']) as cur2:
                    for reg_cd_depa, reg_shape in cur2:
                        if reg_shape.overlaps(shp_zona) or shp_zona.contains(reg_shape):
                            oid.append(str(reg_cd_depa))

                if oid:
                    joinOid ="', '".join(oid)
                    sql = f"{self._model_region.cd_depa.name} IN ('{joinOid}')"
                else:
                    # Si no hay regiones
                    continue

                flayer_filter = arcpy.MakeFeatureLayer_management(
                                                                    feature_layer_region,
                                                                    uuid.uuid4().hex[:8],
                                                                    sql
                                                                )
                arcpy.CopyFeatures_management(flayer_filter, path)

    def crear_directorio_trabajo(self, datum):
        """
        Elimina archivos existentes en el directorio temporal, con el fin
        de evitar cruces de información. Luego crea la carpeta para un
        datum específico.
        """
        self._work_dir = os.path.join(self._input_dir, "insumos")#arcpy.GetParameterAsText(2)
        response["folder_insumos"] = self._work_dir
        if not os.path.exists(self._work_dir):
            os.mkdir(self._work_dir)

        self._datum_dir = os.path.join(self._work_dir, datum)

        if os.path.exists(self._datum_dir):
            rmtree(self._datum_dir)

        os.mkdir(self._datum_dir)

        arcpy.AddMessage(f"Directorio de trabajo: {self._work_dir}")
        arcpy.AddMessage("Se eliminaron temporales.")

    def contruir_directorio(self, name, zone):
        """
        Construye un directorio de trabajo para cada región en función
        a su nombre y la zona geográfica.
        """
        path = os.path.join(self._datum_dir, f"{name}{zone}")
        os.makedirs(path)
        return path

    def generar_feature_layer_region(self, name):
        """
        Retorna un FeatureLayer para la región indicada por 'name' (string).
        Filtra la capa principal _path_region_fc con el campo NM_DEPA.
        """
        query = f"{self._model_region.nm_depa.name} = '{name}'"
        name_sin_espacio = name.replace(" ", "")
        res = arcpy.MakeFeatureLayer_management(self._path_region_fc, name_sin_espacio, query)
        return res

    def generar_feature_layer_cuadrantes(self, zone, datum):
        """
        Genera un FeatureLayer de las cuadrículas según la zona geográfica.
        Se busca en la carpeta de input, en la subcarpeta 'datum',
        la clase/tabla: cuadriculas_17, 18 o 19.
        """
        if int(zone) == 17:
            cuadrante = cuadriculas_17()
        elif int(zone) == 18:
            cuadrante = cuadriculas_18()
        elif int(zone) == 19:
            cuadrante = cuadriculas_19()
        else:
            raise ValueError(f"Zona {zone} no válida.")

        path_cuadrante = os.path.join(self._input_dir, datum, cuadrante.name + EXTENTION_SHP)
        res = arcpy.MakeFeatureLayer_management(path_cuadrante, cuadrante.name)
        return res

    def get_frontera_en_memoria(self, namefront):
        """
        Crea un FeatureLayer en memoria para la frontera o mar indicados.
        """
        sql = f"{self._model_region.nm_depa.name} = '{namefront}'"
        name_lyr = f"m{uuid.uuid4().hex}"
        response = arcpy.MakeFeatureLayer_management(self._path_region, name_lyr, sql)
        return response

    def get_frontera(self, namefront, flayer, directorio):
        """
        Copia el feature layer (frontera o mar) a un shapefile en 'directorio'.
        """
        # Determina el nombre de shapefile a exportar
        if namefront == self._name_mar:
            name = self._insumos.sea
        else:
            name = self._insumos.countries

        path_shp = os.path.join(directorio, name)
        arcpy.CopyFeatures_management(flayer, path_shp)
        return path_shp

    @staticmethod
    def get_src(datum, zona):
        """
        Retorna el EPSG o código de proyección en base a datum y la zona.
        """
        src = EPSG_WGS if datum.lower() == FOLDER_WGS else EPSG_PSAD
        src = src + zona
        return src

    @staticmethod
    def clear_seleccion(flayer):
        """
        Limpia la selección de cualquier FeatureLayer.
        """
        arcpy.SelectLayerByAttribute_management(flayer, "CLEAR_SELECTION")

    def generar_insumos(self):
        """
        Genera los insumos necesarios para el procesamiento de límites regionales:
        1. Agrupa regiones por zona geográfica.
        2. Crea un directorio temporal para cada datum.
        3. Para cada zona (17, 18, 19), filtra las regiones y cuadrantes
           y genera archivos shapefile de mar, frontera, cuadrículas, region y regions.
        """
        for datum in self.datums:
            # Rutas a shapefiles de 'region' y 'zona_geografica'
            self._path_region = os.path.join(self._input_dir, datum.lower(), self._model_region.name + EXTENTION_SHP)
            self._path_zona_geografica = os.path.join(
                self._input_dir, datum.lower(),
                self._model_zona_geografica.name + EXTENTION_SHP
            )

            # Genera un FeatureLayer con la frontera y el mar
            frontera_lyr = self.get_frontera_en_memoria(self._name_frontera)
            mar_lyr = self.get_frontera_en_memoria(self._name_mar)

            arcpy.AddMessage(f"\nDATUM: {datum}")

            # 1) Agrupa regiones por zona en la FGDB
            self.agrupar_regiones_por_zona(datum)

            # 2) Crea directorio temporal para este datum
            self.crear_directorio_trabajo(datum)

            # 3) Itera sobre las zonas geográficas
            for zone in self._zones:
                self._dataset.zone = zone
                # Ruta a la Feature Class que contiene las regiones para (datum, zone)
                self._path_region_fc = os.path.join(
                                                        self._input_dir,#self._dataset.path,
                                                        f"{self._model_region.name}{datum}{zone}"
                                                    )

                # Genera FL de cuadrantes
                cuadrante_lyr = self.generar_feature_layer_cuadrantes(zone, datum)

                # Recorre cada región en la Feature Class por zona
                with arcpy.da.SearchCursor(self._path_region_fc, [self._model_region.nm_depa.name]) as sc:
                    for (nm_depa,) in sc:
                        arcpy.AddMessage(f"\tGenerando insumos de {nm_depa: <20}{zone}")
                        namedir = nm_depa.replace(" ", '')

                        # Crea directorio para cada región
                        path_region_dir = self.contruir_directorio(namedir, zone)

                        feature_layer_region = self.generar_feature_layer_region(nm_depa)

                        # Selecciona cuadrantes que intersecten la región actual
                        arcpy.SelectLayerByLocation_management(
                                                                    cuadrante_lyr, "INTERSECT", feature_layer_region,
                                                                    None, "NEW_SELECTION"
                                                                )

                        # Fija la proyección de salida al sistema UTM apropiado
                        arcpy.env.outputCoordinateSystem = arcpy.SpatialReference(self.get_src(datum, zone))

                        # Copia el mar a shapefile
                        self.get_frontera(self._name_mar, mar_lyr, path_region_dir)

                        # Copia la frontera (paises) a shapefile
                        self.get_frontera(self._name_frontera, frontera_lyr, path_region_dir)

                        # Crea el shapefile de cuadrantes (erase la frontera)
                        path_cuadrantes_shp = os.path.join(path_region_dir, self._insumos.quads)
                        arcpy.Erase_analysis(cuadrante_lyr, frontera_lyr, path_cuadrantes_shp)

                        # Limpia la selección en los cuadrantes
                        self.clear_seleccion(cuadrante_lyr)

                        # Copia la región actual a shapefile
                        path_region_shp = os.path.join(path_region_dir, self._insumos.region)
                        arcpy.CopyFeatures_management(feature_layer_region, path_region_shp)

                        # Copia TODAS las regiones de la zona actual
                        path_regions_shp = os.path.join(path_region_dir, self._insumos.regions)
                        arcpy.CopyFeatures_management(self._path_region_fc, path_regions_shp)

    def main(self):
        """Punto de entrada: Genera todos los insumos."""
        self.generar_insumos()


# Bloque principal, si se ejecuta este archivo directamente
if __name__ == '__main__':
    try:
        poo = GeneradorInsumos()
        poo.main()
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        response["Error"] = traceback.format_exc()
    finally:
        response = json.dumps(response)
        arcpy.SetParameterAsText(2, response)
