﻿using ArcGIS.Core.Data;
using ArcGIS.Core.Data.DDL;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldDescription = ArcGIS.Core.Data.DDL.FieldDescription;

namespace CommonUtilities.ArcgisProUtils
{
    public class FeatureClassCreatorUtils
    {
        static Geodatabase geodatabase;
        static Datastore? datastore;
        static FeatureLayer fLyr;
        static Layer lyr;

        public async static Task<FeatureLayer> CreatePolygonInNewGdbAsync(string gdbFolderPath, string nameNewGdb, string featureClassName, List<MapPoint> vertices, string zone)
        {
            // Validar entradas
            if (string.IsNullOrWhiteSpace(gdbFolderPath))
                return null;//throw new ArgumentException("La ruta de la carpeta no puede estar vacía.", nameof(gdbFolderPath));


            if (string.IsNullOrWhiteSpace(featureClassName))
                return null;//throw new ArgumentException("El nombre del shapefile no puede estar vacío.", nameof(featureClassName));

            if (vertices == null)
                return null;//throw new ArgumentNullException(nameof(vertices), "La lista de vértices no puede ser nula.");

            if (vertices.Count < 4)
                return null;

            // Verificar que todos los vértices sean únicos
            if (vertices.Distinct(new MapPointEqualityComparer()).Count() != vertices.Count)
                return null;//throw new ArgumentException("La lista de vértices contiene puntos duplicados.", nameof(vertices));
            //string fullShapefilePath = System.IO.Path.Combine(shapefileFolderPath, $"{shapefileName}.geodatabase");
            string fullPathPolygonGdb = System.IO.Path.Combine(gdbFolderPath, $"{nameNewGdb}.gdb");
            // Ejecutar la creación del shapefile en el hilo adecuado
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            return await QueuedTask.Run(() =>
            {
                // Verificar si la carpeta existe
                if (!Directory.Exists(gdbFolderPath))
                    throw new DirectoryNotFoundException($"La carpeta '{gdbFolderPath}' no existe.");

                if (geodatabase == null || !geodatabase.GetPath().ToString().Contains(fullPathPolygonGdb.Replace("\\", "/"), StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        geodatabase = SchemaBuilder.CreateGeodatabase(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                    catch
                    {
                        geodatabase = new(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                }
                FieldDescription countPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CONTADOR", FieldType.String)
                {
                    AliasName = "Contador",
                    Length = 10
                };
                FieldDescription codePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CODIGO", FieldType.String)
                {
                    AliasName = "Codigo",
                    Length = 25
                };
                FieldDescription namePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("DESCRIPCION", FieldType.String)
                {
                    AliasName = "Descripcion",
                    Length = 30
                };
                List<FieldDescription> fieldDescriptions = new List<FieldDescription>() { countPolygonFieldDescription,
                                                                                          codePolygonFieldDescription,
                                                                                          namePolygonFieldDescription};

                // Create a ShapeDescription object
                string stringWkt = $"327{zone}";
                int wkt = int.Parse(stringWkt);
                SpatialReference spatialReference = SpatialReferenceBuilder.CreateSpatialReference(wkt);
                ShapeDescription shapeDescription = new ShapeDescription(GeometryType.Polygon, spatialReference);

                // Create a FeatureClassDescription object to describe the feature class to create
                FeatureClassDescription featureClassDescription = new FeatureClassDescription(featureClassName, fieldDescriptions, shapeDescription);

                // Create a SchemaBuilder object
                SchemaBuilder schemaBuilder = new SchemaBuilder(geodatabase);
                // Add the creation of PoleInspection to our list of DDL tasks
                schemaBuilder.Create(featureClassDescription);

                // Execute the DDL
                bool success = schemaBuilder.Build();
                // Ruta completa del shapefile

                // Abrir el feature class recién creado
                FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
                FeatureClassDefinition fcDefinition = featureClass.GetDefinition();
                string shapeFieldName = fcDefinition.GetShapeField();
                var polygon = PolygonBuilder.CreatePolygon(vertices, fcDefinition.GetSpatialReference());

                using (RowBuffer rowBuffer = featureClass.CreateRowBuffer())
                {
                    rowBuffer[shapeFieldName] = polygon;
                    // Asignar valor al campo "Contador"
                    rowBuffer["CONTADOR"] = "1";
                    rowBuffer["CODIGO"] = "1";
                    rowBuffer["DESCRIPCION"] = featureClassName;

                    using (Feature feature = featureClass.CreateRow(rowBuffer))
                    {
                        // Nada más que hacer aquí
                    }
                }

                // Agregar el shapefile al mapa activo
                Map map = MapView.Active?.Map;
                if (map == null)
                    throw new InvalidOperationException("No hay un mapa activo para agregar el shapefile.");

                // Verificar si el shapefile ya está agregado al mapa
                bool layerExists = map.Layers.FirstOrDefault(l => string.Equals(l.Name, featureClassName, StringComparison.OrdinalIgnoreCase)) != null;

                if (!layerExists)
                {
                    lyr = LayerFactory.Instance.CreateLayer(new Uri(Path.Combine(fullPathPolygonGdb, featureClassName)), map);
                    _ = MapView.Active.ZoomTo(lyr);
                }
                else
                {
                    // Opcional: Manejar el caso donde la capa ya existe
                    System.Diagnostics.Debug.WriteLine($"La capa '{featureClassName}' ya existe en el mapa.");
                }
                return lyr as FeatureLayer;
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        public async static Task<FeatureLayer> CreatePolygonInNewPoGdbAsync(string gdbFolderPath, string nameNewGdb, string featureClassName, List<MapPoint> vertices, string zone)
        {
            // Validar entradas
            if (string.IsNullOrWhiteSpace(gdbFolderPath))
                return null;//throw new ArgumentException("La ruta de la carpeta no puede estar vacía.", nameof(gdbFolderPath));


            if (string.IsNullOrWhiteSpace(featureClassName))
                return null;//throw new ArgumentException("El nombre del shapefile no puede estar vacío.", nameof(featureClassName));

            if (vertices == null)
                return null;//throw new ArgumentNullException(nameof(vertices), "La lista de vértices no puede ser nula.");

            if (vertices.Count < 4)
                return null;

            // Verificar que todos los vértices sean únicos
            if (vertices.Distinct(new MapPointEqualityComparer()).Count() != vertices.Count)
                return null;//throw new ArgumentException("La lista de vértices contiene puntos duplicados.", nameof(vertices));
            //string fullShapefilePath = System.IO.Path.Combine(shapefileFolderPath, $"{shapefileName}.geodatabase");
            string fullPathPolygonGdb = System.IO.Path.Combine(gdbFolderPath, $"{nameNewGdb}.gdb");
            // Ejecutar la creación del shapefile en el hilo adecuado
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            return await QueuedTask.Run(() =>
            {
                // Verificar si la carpeta existe
                if (!Directory.Exists(gdbFolderPath))
                    throw new DirectoryNotFoundException($"La carpeta '{gdbFolderPath}' no existe.");

                if (geodatabase == null || !geodatabase.GetPath().ToString().Contains(fullPathPolygonGdb.Replace("\\", "/"), StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        geodatabase = SchemaBuilder.CreateGeodatabase(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                    catch
                    {
                        geodatabase = new(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                }
                FieldDescription countPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CONTADOR", FieldType.String)
                {
                    AliasName = "Contador",
                    Length = 10
                };
                FieldDescription codePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CODIGOU", FieldType.String)
                {
                    AliasName = "Codigou",
                    Length = 25
                };
                FieldDescription namePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CONCESION", FieldType.String)
                {
                    AliasName = "Concesion",
                    Length = 30
                };
                FieldDescription estaPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("ESTADO", FieldType.String)
                {
                    AliasName = "Estado",
                    Length = 25
                };
                FieldDescription cartPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CARTA", FieldType.String)
                {
                    AliasName = "Carta",
                    Length = 24
                };
                FieldDescription ZonaPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("ZONA", FieldType.String)
                {
                    AliasName = "Zona",
                    Length = 2
                };
                FieldDescription TipoPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("TIPO_EX", FieldType.String)
                {
                    AliasName = "Tipo_ex",
                    Length = 12
                };

                FieldDescription LeyePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("LEYENDA", FieldType.String)
                {
                    AliasName = "Leyenda",
                    Length = 10
                };
                FieldDescription EvalPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("EVAL", FieldType.String)
                {
                    AliasName = "Eval",
                    Length = 5
                };
                FieldDescription FechPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("FEC_DENU", FieldType.Date)
                {
                    AliasName = "Fec_denu"
                    //Length = 5
                };
                FieldDescription HoraPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("HOR_DENU", FieldType.String)
                {
                    AliasName = "Hor_denu",
                    Length = 5
                };
                FieldDescription DatumPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("DATUM", FieldType.String)
                {
                    AliasName = "Datum",
                    Length = 2
                };
                FieldDescription ObservPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("OBSERV", FieldType.String)
                {
                    AliasName = "Observ",
                    Length = 20
                };

                List<FieldDescription> fieldDescriptions = new List<FieldDescription>() { countPolygonFieldDescription,
                                                                                          codePolygonFieldDescription,
                                                                                          namePolygonFieldDescription,
                                                                                          estaPolygonFieldDescription,
                                                                                          cartPolygonFieldDescription,
                                                                                          ZonaPolygonFieldDescription,
                                                                                          TipoPolygonFieldDescription,
                                                                                          LeyePolygonFieldDescription,
                                                                                          EvalPolygonFieldDescription,
                                                                                          FechPolygonFieldDescription,
                                                                                          HoraPolygonFieldDescription,
                                                                                          DatumPolygonFieldDescription,
                                                                                          ObservPolygonFieldDescription};

                // Create a ShapeDescription object
                string stringWkt = $"327{zone}";
                int wkt = int.Parse(stringWkt);
                SpatialReference spatialReference = SpatialReferenceBuilder.CreateSpatialReference(wkt);
                ShapeDescription shapeDescription = new ShapeDescription(GeometryType.Polygon, spatialReference);

                // Create a FeatureClassDescription object to describe the feature class to create
                FeatureClassDescription featureClassDescription = new FeatureClassDescription(featureClassName, fieldDescriptions, shapeDescription);

                // Create a SchemaBuilder object
                SchemaBuilder schemaBuilder = new SchemaBuilder(geodatabase);
                // Add the creation of PoleInspection to our list of DDL tasks
                schemaBuilder.Create(featureClassDescription);

                // Execute the DDL
                bool success = schemaBuilder.Build();
                // Ruta completa del shapefile

                // Abrir el feature class recién creado
                FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
                FeatureClassDefinition fcDefinition = featureClass.GetDefinition();
                string shapeFieldName = fcDefinition.GetShapeField();
                var polygon = PolygonBuilder.CreatePolygon(vertices, fcDefinition.GetSpatialReference());
                DateTime fechaHoy = DateTime.Now;


                using (RowBuffer rowBuffer = featureClass.CreateRowBuffer())
                {
                    rowBuffer[shapeFieldName] = polygon;
                    // Asignar valor al campo "Contador"
                    rowBuffer["CONTADOR"] = "1";
                    rowBuffer["CODIGOU"] = "000000001";
                    rowBuffer["CONCESION"] = "DM Simulado"; //featureClassName;
                    rowBuffer["ESTADO"] = "P";
                    rowBuffer["CARTA"] = "Carta";
                    rowBuffer["ZONA"] = $"{zone}";
                    rowBuffer["TIPO_EX"] = "PE";
                    rowBuffer["LEYENDA"] = "G6";
                    rowBuffer["EVAL"] = "EV";
                    rowBuffer["FEC_DENU"] = fechaHoy.ToShortDateString();
                    rowBuffer["HOR_DENU"] = fechaHoy.ToShortTimeString();
                    rowBuffer["DATUM"] = "02";
                    rowBuffer["OBSERV"] = "RETENIDA";

                    using (Feature feature = featureClass.CreateRow(rowBuffer))
                    {
                        // Nada más que hacer aquí
                    }
                }

                // Agregar el shapefile al mapa activo
                Map map = MapView.Active?.Map;
                if (map == null)
                    throw new InvalidOperationException("No hay un mapa activo para agregar el shapefile.");

                // Verificar si el shapefile ya está agregado al mapa
                bool layerExists = map.Layers.FirstOrDefault(l => string.Equals(l.Name, featureClassName, StringComparison.OrdinalIgnoreCase)) != null;

                if (!layerExists)
                {
                    lyr = LayerFactory.Instance.CreateLayer(new Uri(Path.Combine(fullPathPolygonGdb, featureClassName)), map);
                    _ = MapView.Active.ZoomTo(lyr);
                }
                else
                {
                    // Opcional: Manejar el caso donde la capa ya existe
                    System.Diagnostics.Debug.WriteLine($"La capa '{featureClassName}' ya existe en el mapa.");
                }
                return lyr as FeatureLayer;
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        public async static Task<FeatureLayer> CreatePolygonInNewPoGdbAsync(string gdbFolderPath, string nameNewGdb, string featureClassName, List<MapPoint> vertices, string zone, string codigo)
        {
            // Validar entradas
            if (string.IsNullOrWhiteSpace(gdbFolderPath))
                return null;//throw new ArgumentException("La ruta de la carpeta no puede estar vacía.", nameof(gdbFolderPath));


            if (string.IsNullOrWhiteSpace(featureClassName))
                return null;//throw new ArgumentException("El nombre del shapefile no puede estar vacío.", nameof(featureClassName));

            if (vertices == null)
                return null;//throw new ArgumentNullException(nameof(vertices), "La lista de vértices no puede ser nula.");

            if (vertices.Count < 4)
                return null;

            // Verificar que todos los vértices sean únicos
            if (vertices.Distinct(new MapPointEqualityComparer()).Count() != vertices.Count)
                return null;//throw new ArgumentException("La lista de vértices contiene puntos duplicados.", nameof(vertices));
            //string fullShapefilePath = System.IO.Path.Combine(shapefileFolderPath, $"{shapefileName}.geodatabase");
            string fullPathPolygonGdb = System.IO.Path.Combine(gdbFolderPath, $"{nameNewGdb}.gdb");
            // Ejecutar la creación del shapefile en el hilo adecuado
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            return await QueuedTask.Run(() =>
            {
                // Verificar si la carpeta existe
                if (!Directory.Exists(gdbFolderPath))
                    throw new DirectoryNotFoundException($"La carpeta '{gdbFolderPath}' no existe.");

                if (geodatabase == null || !geodatabase.GetPath().ToString().Contains(fullPathPolygonGdb.Replace("\\", "/"), StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        geodatabase = SchemaBuilder.CreateGeodatabase(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                    catch
                    {
                        geodatabase = new(new FileGeodatabaseConnectionPath(new Uri(fullPathPolygonGdb)));
                    }
                }
                FieldDescription countPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CONTADOR", FieldType.String)
                {
                    AliasName = "Contador",
                    Length = 10
                };
                FieldDescription codePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CODIGOU", FieldType.String)
                {
                    AliasName = "Codigou",
                    Length = 25
                };
                FieldDescription namePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CONCESION", FieldType.String)
                {
                    AliasName = "Concesion",
                    Length = 30
                };
                FieldDescription estaPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("ESTADO", FieldType.String)
                {
                    AliasName = "Estado",
                    Length = 25
                };
                FieldDescription cartPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("CARTA", FieldType.String)
                {
                    AliasName = "Carta",
                    Length = 24
                };
                FieldDescription ZonaPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("ZONA", FieldType.String)
                {
                    AliasName = "Zona",
                    Length = 2
                };
                FieldDescription TipoPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("TIPO_EX", FieldType.String)
                {
                    AliasName = "Tipo_ex",
                    Length = 12
                };

                FieldDescription LeyePolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("LEYENDA", FieldType.String)
                {
                    AliasName = "Leyenda",
                    Length = 10
                };
                FieldDescription EvalPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("EVAL", FieldType.String)
                {
                    AliasName = "Eval",
                    Length = 5
                };
                FieldDescription FechPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("FEC_DENU", FieldType.Date)
                {
                    AliasName = "Fec_denu"
                    //Length = 5
                };
                FieldDescription HoraPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("HOR_DENU", FieldType.String)
                {
                    AliasName = "Hor_denu",
                    Length = 5
                };
                FieldDescription DatumPolygonFieldDescription = new ArcGIS.Core.Data.DDL.FieldDescription("DATUM", FieldType.String)
                {
                    AliasName = "Datum",
                    Length = 2
                };

                List<FieldDescription> fieldDescriptions = new List<FieldDescription>() { countPolygonFieldDescription,
                                                                                          codePolygonFieldDescription,
                                                                                          namePolygonFieldDescription,
                                                                                          estaPolygonFieldDescription,
                                                                                          cartPolygonFieldDescription,
                                                                                          ZonaPolygonFieldDescription,
                                                                                          TipoPolygonFieldDescription,
                                                                                          LeyePolygonFieldDescription,
                                                                                          EvalPolygonFieldDescription,
                                                                                          FechPolygonFieldDescription,
                                                                                          HoraPolygonFieldDescription,
                                                                                          DatumPolygonFieldDescription};

                // Create a ShapeDescription object
                string stringWkt = $"327{zone}";
                int wkt = int.Parse(stringWkt);
                SpatialReference spatialReference = SpatialReferenceBuilder.CreateSpatialReference(wkt);
                ShapeDescription shapeDescription = new ShapeDescription(GeometryType.Polygon, spatialReference);

                // Create a FeatureClassDescription object to describe the feature class to create
                FeatureClassDescription featureClassDescription = new FeatureClassDescription(featureClassName, fieldDescriptions, shapeDescription);

                // Create a SchemaBuilder object
                SchemaBuilder schemaBuilder = new SchemaBuilder(geodatabase);
                // Add the creation of PoleInspection to our list of DDL tasks
                schemaBuilder.Create(featureClassDescription);

                // Execute the DDL
                bool success = schemaBuilder.Build();
                // Ruta completa del shapefile

                // Abrir el feature class recién creado
                FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
                FeatureClassDefinition fcDefinition = featureClass.GetDefinition();
                string shapeFieldName = fcDefinition.GetShapeField();
                var polygon = PolygonBuilder.CreatePolygon(vertices, fcDefinition.GetSpatialReference());
                DateTime fechaHoy = DateTime.Now;


                using (RowBuffer rowBuffer = featureClass.CreateRowBuffer())
                {
                    rowBuffer[shapeFieldName] = polygon;
                    // Asignar valor al campo "Contador"
                    rowBuffer["CONTADOR"] = "1";
                    rowBuffer["CODIGOU"] = $"{codigo}"; ;
                    rowBuffer["CONCESION"] = "DM Simulado"; //featureClassName;
                    rowBuffer["ESTADO"] = "P";
                    rowBuffer["CARTA"] = "Carta";
                    rowBuffer["ZONA"] = $"{zone}";
                    rowBuffer["TIPO_EX"] = "PE";
                    rowBuffer["LEYENDA"] = "G6";
                    rowBuffer["EVAL"] = "EV";
                    rowBuffer["FEC_DENU"] = fechaHoy.ToShortDateString();
                    rowBuffer["HOR_DENU"] = fechaHoy.ToShortTimeString();
                    rowBuffer["DATUM"] = "02";

                    using (Feature feature = featureClass.CreateRow(rowBuffer))
                    {
                        // Nada más que hacer aquí
                    }
                }

                // Agregar el shapefile al mapa activo
                Map map = MapView.Active?.Map;
                if (map == null)
                    throw new InvalidOperationException("No hay un mapa activo para agregar el shapefile.");

                // Verificar si el shapefile ya está agregado al mapa
                bool layerExists = map.Layers.FirstOrDefault(l => string.Equals(l.Name, featureClassName, StringComparison.OrdinalIgnoreCase)) != null;

                if (!layerExists)
                {
                    lyr = LayerFactory.Instance.CreateLayer(new Uri(Path.Combine(fullPathPolygonGdb, featureClassName)), map);
                    _ = MapView.Active.ZoomTo(lyr);
                }
                else
                {
                    // Opcional: Manejar el caso donde la capa ya existe
                    System.Diagnostics.Debug.WriteLine($"La capa '{featureClassName}' ya existe en el mapa.");
                }
                return lyr as FeatureLayer;
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        private class MapPointEqualityComparer : IEqualityComparer<MapPoint>
        {
            private const double Tolerance = 1e-6;

            public bool Equals(MapPoint p1, MapPoint p2)
            {
                if (p1 == null && p2 == null)
                    return true;
                if (p1 == null || p2 == null)
                    return false;
                return Math.Abs(p1.X - p2.X) < Tolerance && Math.Abs(p1.Y - p2.Y) < Tolerance;
            }

            public int GetHashCode(MapPoint p)
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + p.X.GetHashCode();
                    hash = hash * 23 + p.Y.GetHashCode();
                    return hash;
                }
            }
        }

        /// <summary>
        /// Crea una lista de MapPoint a partir de los ítems de un ListBox.
        /// Cada ítem del ListBox debe tener el formato: "Punto n: X; Y"
        /// </summary>
        /// <param name="listBox">El control ListBox que contiene las coordenadas.</param>
        /// <returns>Una lista de MapPoint con las coordenadas extraídas.</returns>
        /// <exception cref="ArgumentNullException">Si el ListBox es nulo.</exception>
        /// <exception cref="FormatException">Si alguna línea no tiene el formato esperado.</exception>
        public static List<MapPoint> GetVerticesFromListBoxItems(IEnumerable<string> linesString, SpatialReference spatialReference = null)
        {
            if (linesString == null)
                throw new ArgumentNullException(nameof(linesString), "El ListBox no tiene vertices");

            List<MapPoint> vertices = new List<MapPoint>();

            // Definir el sistema de referencia espacial UTM Zona 18S, WGS84
            //SpatialReference spatialReference = SpatialReferenceBuilder.CreateSpatialReference(32718);

            foreach (var item in linesString)
            {
                if (item is string line)
                {
                    // Ejemplo de línea: "Punto 1: 500000; 8900000"
                    try
                    {
                        // Dividir la línea en dos partes separadas por ':'
                        string[] parts = line.Split(':');
                        if (parts.Length != 2)
                            throw new FormatException($"La línea '{line}' no contiene ':' para separar el nombre y las coordenadas.");

                        // La segunda parte contiene las coordenadas " X; Y"
                        string coordinatesPart = parts[1].Trim();

                        // Dividir las coordenadas por ';'
                        string[] coordinates = coordinatesPart.Split(';');
                        if (coordinates.Length != 2)
                            throw new FormatException($"La línea '{line}' no contiene ';' para separar X e Y.");

                        // Parsear X e Y a double, considerando la cultura invariante para manejar el punto decimal
                        if (!double.TryParse(coordinates[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double x))
                            throw new FormatException($"La coordenada X '{coordinates[0]}' en la línea '{line}' no es válida.");

                        if (!double.TryParse(coordinates[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
                            throw new FormatException($"La coordenada Y '{coordinates[1]}' en la línea '{line}' no es válida.");

                        // Crear el MapPoint y agregarlo a la lista
                        MapPoint point = MapPointBuilder.CreateMapPoint(x, y, spatialReference);
                        vertices.Add(point);
                    }
                    catch (FormatException ex)
                    {
                        // Manejar el error según tus necesidades
                        // Por ejemplo, puedes registrar el error y continuar con la siguiente línea
                        // O lanzar la excepción para que el llamador la maneje
                        // Aquí optamos por lanzar la excepción
                        throw new FormatException($"Error al procesar la línea '{line}': {ex.Message}", ex);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Todos los ítems del ListBox deben ser de tipo string.");
                }
            }

            return vertices;
        }


    }
}
