import os
from datetime import datetime
import arcpy

def agregar_estilos(kml):
    """
    Escribe estilos predefinidos en el archivo KML.
    """
    kml.write('	<Style id="sn_noicon">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<Icon>\n')
    kml.write('			</Icon>\n')
    kml.write('		</IconStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sh_grn-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<StyleMap id="msn_ltblu-pushpin">\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>normal</key>\n')
    kml.write('			<styleUrl>#sn_ltblu-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>highlight</key>\n')
    kml.write('			<styleUrl>#sn_ltblu-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('	</StyleMap>\n')

    kml.write('	<StyleMap id="msn_wht-pushpin">\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>normal</key>\n')
    kml.write('			<styleUrl>#sn_wht-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>highlight</key>\n')
    kml.write('			<styleUrl>#sh_wht-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('	</StyleMap>\n')

    kml.write('	<StyleMap id="msn_blue-pushpin_copy1">\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>normal</key>\n')
    kml.write('			<styleUrl>#sn_blue-pushpin_copy1</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>highlight</key>\n')
    kml.write('			<styleUrl>#sh_blue-pushpin_copy1</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('	</StyleMap>\n')

    kml.write('	<Style id="yline">\n')
    kml.write('		<LineStyle>\n')
    kml.write('			<color>ff00ffff</color>\n')
    kml.write('		</LineStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sh_ltblu-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sh_blue-pushpin_copy1">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sn_wht-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/wht-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sn_ltblu-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<StyleMap id="msn_grn-pushpin">\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>normal</key>\n')
    kml.write('			<styleUrl>#sn_grn-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('		<Pair>\n')
    kml.write('			<key>highlight</key>\n')
    kml.write('			<styleUrl>#sh_grn-pushpin</styleUrl>\n')
    kml.write('		</Pair>\n')
    kml.write('	</StyleMap>\n')

    kml.write('	<Style id="sn_blue-pushpin_copy1">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>\n')

    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')

    kml.write('	<Style id="sn_grn-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')


    kml.write('	<Style id="sn_red-pushpin">\n')
    kml.write('		<IconStyle>\n')
    kml.write('			<scale>0.7</scale>\n')
    kml.write('			<Icon>\n')
    kml.write('				<href>http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png</href>\n')
    kml.write('			</Icon>\n')
    kml.write('			<hotSpot x="20" y="2" xunits="pixels" yunits="pixels"/>\n')
    kml.write('		</IconStyle>\n')
    kml.write('		<ListStyle>\n')
    kml.write('		</ListStyle>\n')
    kml.write('	</Style>\n')




def generar_etiquetas(kml, layer):
    """
    Escribe las geometrías y atributos de la capa en el archivo KML.
    
    :param kml: StreamWriter para escribir el archivo KML.
    :param layer: FeatureLayer de ArcGIS Pro.
    :param zona: Zona UTM para la proyección.
    :param nombre_capa: Nombre de la capa a procesar.
    """
    icono_mapping = {
    "G1": "msn_blue-pushpin_copy1",
    "G2": "sn_grn-pushpin",
    "G3": "sn_red-pushpin",
    "G4": "sn_ltblu-pushpin",
    "G5": "sn_wht-pushpin"}

    from arcpy import Describe

    # Obtener la descripción de la capa
    desc = Describe(layer)
    spatial_reference = desc.spatialReference
    wgs84_sr = arcpy.SpatialReference(4326)

    kml.write(' <Folder>\n')
    kml.write(' <name>Detalle Catastro</name>\n')

    # Iterar sobre las entidades
    with arcpy.da.SearchCursor(layer, ["SHAPE@", "CODIGOU","LEYENDA"]) as cursor:
        for row in cursor:
            geometry = row[0]  # Geometría
            geometry = geometry.projectAs(wgs84_sr)
            centroid = geometry.centroid
            field_value = row[1]  # Valor del campo
            estilo = icono_mapping.get(row[2], None) 
            
            kml.write('  <Placemark>\n')
            kml.write(f'    <name>{field_value}</name>\n')
            kml.write('    <description>Detalle</description>\n')
            kml.write(f'    <styleUrl>#{estilo}</styleUrl>\n')
            arcpy.AddMessage(geometry.type)
            arcpy.AddMessage(centroid.X)
            arcpy.AddMessage(centroid.Y)

            ###
            
            kml.write('    <Point>\n')
            kml.write('      <coordinates>\n')
            kml.write(f'{centroid.X},{centroid.Y},0\n')
            kml.write('      </coordinates>\n')
            kml.write('    </Point>\n')
            kml.write('  </Placemark>\n')
            ###

    kml.write(' </Folder>\n')
def generar_poligonos(kml, layer):
    """
    Escribe las geometrías y atributos de la capa en el archivo KML.
    
    :param kml: StreamWriter para escribir el archivo KML.
    :param layer: FeatureLayer de ArcGIS Pro.
    :param zona: Zona UTM para la proyección.
    :param nombre_capa: Nombre de la capa a procesar.
    """
    from arcpy import Describe

    # Obtener la descripción de la capa
    desc = Describe(layer)
    spatial_reference = desc.spatialReference
    wgs84_sr = arcpy.SpatialReference(4326)

    kml.write(' <Folder>\n')
    kml.write(' <name>Detalle Catastro</name>\n')

    contador=1

    # Iterar sobre las entidades
    with arcpy.da.SearchCursor(layer, ["SHAPE@", "CODIGOU"]) as cursor:
        for row in cursor:
            geometry = row[0]  # Geometría
            geometry = geometry.projectAs(wgs84_sr)
            centroid = geometry.centroid
            field_value = row[1]  # Valor del campo
            
            kml.write('  <Placemark>\n')
            kml.write(f'    <name>{contador}</name>\n')
            kml.write('    <description>Detalle</description>\n')
            kml.write('    <styleUrl>#sn_wht-pushpin</styleUrl>\n')
            kml.write('    <LineString>\n')
            kml.write('    <tessellate>1</tessellate>\n')
            kml.write('      <coordinates>\n')

            for part in geometry:
                for point in part:
                    kml.write(f'{point.X},{point.Y}\n')

            kml.write('          </coordinates>\n')
            kml.write('        </LineString>\n')
            kml.write('          <Style>\n')
            kml.write('            <LineStyle>\n')
            kml.write('              <color>ffff0000</color>\n')
            kml.write('              <width>2</width>\n')
            kml.write('            </LineStyle>\n')
            kml.write('          </Style>\n')        
        
            kml.write('  </Placemark>\n')
            contador+=1

    kml.write(' </Folder>\n')


def genera_kml_google(layer, output_folder, nombre_capa, zona):
    """
    Genera un archivo KML con datos de un FeatureLayer, replicando la funcionalidad de VB.NET.
    
    :param layer: FeatureLayer de ArcGIS Pro.
    :param output_folder: Carpeta de salida para el archivo KML.
    :param nombre_capa: Nombre de la capa a procesar.
    :param zona: Zona UTM para la proyección.
    """
    # Ruta del archivo de salida
    # timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    output_kml = os.path.join(output_folder, f"{nombre_capa}.kmz")

    # Crear el archivo KML
    with open(output_kml, "w", encoding="utf-8") as kml:
        # Escribir encabezados del KML
        kml.write('<?xml version="1.0" encoding="UTF-8"?>\n')
        kml.write('<kml xmlns="http://earth.google.com/kml/2.0">\n')
        kml.write('<Document>\n')
        kml.write(f'  <name>{nombre_capa}</name>\n')

        # Estilos predeterminados
        agregar_estilos(kml)

        # Agregar los datos de la capa
        generar_etiquetas(kml, layer)
        generar_poligonos(kml, layer)

        # Cerrar etiquetas del documento
        kml.write('</Document>\n')
        kml.write('</kml>\n')

    print(f"KML generado en: {output_kml}")
    os.startfile(output_kml)  # Abrir el archivo KML generado automáticamente

if __name__ == '__main__':
    try:
        aprx = arcpy.mp.ArcGISProject("CURRENT")
        map_view = aprx.activeMap
        layerPrefix = arcpy.GetParameterAsText(0)

        layer = map_view.listLayers(f'{layerPrefix}*')[0]  # Reemplaza "NombreDeTuCapa"

        genera_kml_google(layer, r"D:\temp", layer.name, 18)
        arcpy.AddMessage("Satisfactorio")
    except Exception as e:
        arcpy.AddError("Error: " + str(e))
        out_geom = None