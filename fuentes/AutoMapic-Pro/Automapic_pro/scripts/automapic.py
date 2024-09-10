import arcpy
import os
import json
import sys

format_datasource_oracle =  """
                            Server=bdgeocat,Instance=bdgeocat,Database Platform=Oracle,User=publ_gis,Version=SDE.DEFAULT (Traditional),
                            Authentication Type=Database Authentication,Feature Dataset={},
                            Dataset={}
                            """

def dataSourcetodict(textSource):
    """
    Crea diccionario a partir de los elementos del data Source de una Feature Layer
    """
    pares = textSource.split(',')
    # Crear un diccionario a partir de los pares clave-valor
    diccionario = {}
    for par in pares:
        clave, valor = par.split('=')
        diccionario[clave] = valor
    return diccionario


def new_datasource(layer, name_feature):
    """
    Add a layer to the map document with a new data source.
    :param layer: path layer to add.
    :param name_feature: name of the feature class.
    """
    if layer.supports("DATASOURCE"):
        # Verificar si la capa tiene fuente de datos
        # Extraer la información del usuario y la fuente de datos
        try:
            source = layer.dataSource
            #print(source)
            if len(source.split(','))>1:
                properties= dataSourcetodict(source)
                connp = layer.connectionProperties
                connp['dataset'] = name_feature
                layer.updateConnectionProperties(layer.connectionProperties, connp, validate=True)
            else:
                print("pendiente de desarrollar cambio de fuente de datos para featureClass en File GDBs")
            # Añadir la información al listado
            
        except Exception as e:
            print(e)
            # user= properties['Dataset'].split('.')[0]
                        