using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using System.Threading.Tasks;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Data;
using ArcGIS.Desktop.Core;
using QueryFilter = ArcGIS.Core.Data.QueryFilter;
using System.Windows;

namespace CommonUtilities.ArcgisProUtils
{

    public class SymbologyUtils
    {
        private string arch_cata = "tu_valor"; // Asigna el valor correspondiente
        private string v_activar = "tu_valor"; // Asigna el valor correspondiente
        private string v_morado = "tu_valor"; // Asigna el valor correspondiente
        private string val_opcion_plano_Ar = "tu_valor"; // Asigna el valor correspondiente
        private string loStrShapefile_dm = "tu_valor"; // Asigna el valor correspondiente
        //private static List<string> fields;

        /// <summary>
        /// Aplica una simbología simple a una capa de puntos.
        /// </summary>
        /// <param name="layer">Capa a la que se le aplicará la simbología.</param>
        /// <param name="color">Color en formato hexadecimal (ejemplo: "#FF0000" para rojo).</param>
        public static async Task ApplySimplePointSymbologyAsync(FeatureLayer layer, CIMColor  color)
        {
            await QueuedTask.Run(() =>
            {
                var renderer = layer.GetRenderer() as CIMSimpleRenderer;
                if (renderer == null)
                {
                    renderer = new CIMSimpleRenderer();
                }
                var symbol = SymbolFactory.Instance.ConstructPointSymbol(color, 5, SimpleMarkerStyle.Circle);
                renderer.Symbol = symbol.MakeSymbolReference();
                layer.SetRenderer(renderer);
            });
        }

        /// <summary>
        /// Aplica una simbología categorizada a una capa.
        /// </summary>
        /// <param name="layer">Capa a la que se le aplicará la simbología.</param>
        /// <param name="fieldName">Nombre del campo para categorizar.</param>
        public static async Task ApplyUniqueValueRendererAsync(FeatureLayer layer, string fieldName)
        {
            await QueuedTask.Run(() =>
            {
                //fields = new List<string> { fieldName };
                var uvRendererDefinition = new UniqueValueRendererDefinition()
                {
                    ValueFields = new List<string> { fieldName },
                };
                var renderer = layer.CreateRenderer(uvRendererDefinition);
                layer.SetRenderer(renderer);
            });
        }
        public static async void CustomLinePolygonLayer(FeatureLayer layer, SimpleLineStyle style, CIMColor colorFill, CIMColor colorLine1, CIMColor colorLine2 = null)
        {
            await QueuedTask.Run(() =>
            {
                var trans = 75.0;//semi transparent
                // Linea de brode negra con grosor 2 y de estilo punteado
                CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(colorLine1, 1.5, style);
                
                var symbol = layer.GetRenderer() as CIMSimpleRenderer;
                // Crear una nueva simbología con relleno de color transparente
                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                    colorFill, SimpleFillStyle.Solid, InLine);
                if (colorLine2 != null)
                {
                    CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(colorLine2, 2.5, SimpleLineStyle.Dash);
                    newFillSymbol.SymbolLayers = newFillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();
                }
                symbol.Symbol = newFillSymbol.MakeSymbolReference();
                // Actualiza la simbologia 
                layer.SetRenderer(symbol);
            });
        }

        public static async void CustomLinePolygonGraphic(ArcGIS.Core.Geometry.Geometry geomPolygon, CIMColor colorFill, CIMColor colorLine1, CIMColor colorLine2)
        {
            await QueuedTask.Run(() =>
            {
                Map activeMap = MapView.Active.Map;
                var trans = 75.0;//semi transparent
                // Linea de brode negra con grosor 2 y de estilo punteado
                CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(colorLine1, 1.5, SimpleLineStyle.Dash);
                CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(colorLine2, 2.5, SimpleLineStyle.Dash);
                
                // Crear una nueva simbología con relleno de color transparente
                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                    colorFill, SimpleFillStyle.Solid, InLine);
                newFillSymbol.SymbolLayers = newFillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();
                // Crear y añadir el gráfico
                var cimSymbol = newFillSymbol.MakeSymbolReference();
                var cimGraphicElement = new CIMPolygonGraphic
                {
                    Polygon = geomPolygon as ArcGIS.Core.Geometry.Polygon,
                    Symbol = cimSymbol,
                };
                MapView.Active.AddOverlay(cimGraphicElement);
            });
        }

        public static async Task ApplySymbologyFromStyleAsync(string layerName, string styleFilePath, string fieldName, string codeValue)
        {
            await QueuedTask.Run(() =>
            {
                //Obtener el mapa y la capa
                Map map = MapView.Active?.Map;
                if (map == null)
                {
                    System.Windows.MessageBox.Show("No active map found.");
                    return;
                }

                // Encuentre la capa de entidades por nombre
                FeatureLayer featureLayer = map.Layers.OfType<FeatureLayer>().FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase));
                if (featureLayer == null)
                {
                    System.Windows.MessageBox.Show($"Layer '{layerName}' not found.");
                    return;
                }
                StyleHelper.AddStyle(Project.Current, styleFilePath);
                // Abrir el archivo .stylx
                StyleProjectItem styleItem = Project.Current.GetItems<StyleProjectItem>().FirstOrDefault(s => s.Path.Equals(styleFilePath, StringComparison.OrdinalIgnoreCase));
                if (styleItem == null)
                {
                    // Si el estilo no está en el proyecto, agréguelo
                    styleItem = ItemFactory.Instance.Create(styleFilePath) as StyleProjectItem;
                    if (styleItem != null)
                    {
                        Project.Current.AddItem(styleItem);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show($"Style file '{styleFilePath}' not found or could not be added to the project.");
                        return;
                    }
                }

                // Obtenga valores únicos del campo
                List<string> uniqueValues = GetUniqueFieldValues(featureLayer, fieldName);

                // Crear un UniqueValueRenderer
                CIMUniqueValueRenderer uniqueValueRenderer = new CIMUniqueValueRenderer
                {
                    Fields = new string[] { fieldName },
                    UseDefaultSymbol = false,
                    DefaultLabel = "Other",
                    DefaultSymbol = null,
                    Groups = new CIMUniqueValueGroup[0]
                };

                List<CIMUniqueValueClass> uniqueValueClasses = new List<CIMUniqueValueClass>();

                // Recorrer valores únicos y asignar símbolos
                foreach (string value in uniqueValues)
                {
                    // Obtenga el símbolo del estilo según el valor
                    var symbolLookupName = value; // Suponiendo que los nombres de los símbolos en el estilo coinciden con los valores del campo
                    SymbolStyleItem symbolItem = styleItem.SearchSymbols(StyleItemType.PolygonSymbol, symbolLookupName).FirstOrDefault();

                    if (symbolItem == null)
                    {
                        // Manejar caso donde no se encuentra el símbolo
                        System.Diagnostics.Debug.WriteLine($"Symbol '{symbolLookupName}' not found in style.");
                        continue;
                    }

                    // Obtener el CIMSymbol
                    CIMSymbol symbol = symbolItem.Symbol;

                    // Crear un unique value class
                    CIMUniqueValue uniqueValue = new CIMUniqueValue
                    {
                        FieldValues = new string[] { value }
                    };
                    string labelValue = GetUniqueValueLabel(value, codeValue);
                    CIMUniqueValueClass uniqueValueClass = new CIMUniqueValueClass
                    {
                        Values = new CIMUniqueValue[] { uniqueValue },
                        Label = labelValue, // Use the field value as the label
                        Symbol = symbol.MakeSymbolReference()
                    };

                    uniqueValueClasses.Add(uniqueValueClass);
                }

                // Asignar las clases de valores únicos al renderizador
                uniqueValueRenderer.Groups = new CIMUniqueValueGroup[]
                {
                new CIMUniqueValueGroup
                {
                    Classes = uniqueValueClasses.ToArray(),
                    Heading = "Categorias"
                }
                };

                // Aplicar el renderizador a la capa
                featureLayer.SetRenderer(uniqueValueRenderer);

            });
        }

        /// <summary>
        /// Gets unique values from a field in a feature layer.
        /// </summary>
        /// <param name="featureLayer">The feature layer.</param>
        /// <param name="fieldName">The field name to get unique values from.</param>
        /// <returns>A list of unique string values.</returns>
        private static List<string> GetUniqueFieldValues(FeatureLayer featureLayer, string fieldName)
        {
            List<string> uniqueValues = new List<string>();

            using (RowCursor cursor = featureLayer.Search(new QueryFilter { SubFields = fieldName }))
            {
                while (cursor.MoveNext())
                {
                    using (Row row = cursor.Current)
                    {
                        object fieldValue = row[fieldName];
                        if (fieldValue != null)
                        {
                            string value = fieldValue.ToString();
                            if (!uniqueValues.Contains(value))
                            {
                                uniqueValues.Add(value);
                            }
                        }
                    }
                }
            }
            return uniqueValues;
        }

        public static string GetUniqueValueLabel(string styleItemName, string codeValue, string catastroH="", string estadoHDm="")
        {
            string value = string.Empty;  // Variable para almacenar el valor de la etiqueta

            switch (styleItemName)
            {
                case "G1":
                    value = "D.M. en Trámite"; // Color Tramite
                    break;
                case "G2":
                    value = "D.M. en Trámite D.L. 109"; // Color Tramite 109
                    break;
                case "G3":
                    value = "D.M. Titulados"; // Color Titulado
                    break;
                case "G4":
                    if (catastroH == "1" && estadoHDm == " ")
                    {
                        value = "D.M."; // Color Extinguido
                    }
                    else
                    {
                        value = "D.M. Extinguido"; // Color Extinguido
                    }
                    break;
                case "G5":
                    value = "D.M. - Otros"; // Color Planta, deposito
                    break;
                case "G6":
                    value = "DM_" + codeValue;
                    break;
                case "G7":
                    value = "DM_ Anap"; // Activo
                    break;
                case "G8":
                    value = "D.M. con Res. Ext. sin Consentir"; // Activo
                    break;
                // Puedes agregar más casos según sea necesario
                default:
                    value = ""; // Valor predeterminado si no se encuentra el caso
                    break;
            }

            return value;
        }

        /// <summary>
        /// Método para asignar simbología a una capa de polígonos basada en condiciones.
        /// </summary>
        /// <param name="layerName">Nombre de la capa a simbologizar.</param>
        public static async Task ColorPolygonSimple(FeatureLayer featureLayer)//string layerName)
        {
            await QueuedTask.Run(() =>
            {
                string layerName = featureLayer.Name;
                try
                {
                    //// Obtener el mapa activo
                    //Map map = MapView.Active.Map;

                    //// Buscar la capa por nombre
                    //FeatureLayer featureLayer = map.Layers.FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase)) as FeatureLayer;

                    //if (featureLayer == null)
                    //{
                    //    MessageBox.Show($"La capa '{layerName}' no existe o no es una FeatureLayer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //    return;
                    //}
                    
                    // Crear el símbolo de relleno simple
                    CIMPolygonSymbol simpleFillSymbol = new CIMPolygonSymbol();

                    // Crear el símbolo de línea simple
                    CIMStroke simpleLineSymbol = null;

                    // Asignar estilo, color y ancho basados en condiciones
                    AssignSymbolProperties(layerName, ref simpleFillSymbol, ref simpleLineSymbol);

                    // Crear el renderizador simple
                    CIMSimpleRenderer simpleRenderer = new CIMSimpleRenderer
                    {
                        Symbol = simpleFillSymbol.MakeSymbolReference(),
                    };
                                        
                    // Asignar el renderizador a la capa
                    featureLayer.SetRenderer(simpleRenderer);

                    // Refrescar la vista para aplicar los cambios
                    //MapView.Active.Redraw(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al asignar simbología a la capa '{layerName}': {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        /// <summary>
        /// Asigna propiedades al símbolo de relleno y línea basado en condiciones.
        /// </summary>
        /// <param name="layerName">Nombre de la capa.</param>
        /// <param name="fillSymbol">Referencia al símbolo de relleno.</param>
        /// <param name="lineSymbol">Referencia al símbolo de línea.</param>
        private static void AssignSymbolProperties(string layerName, ref CIMPolygonSymbol fillSymbol, ref CIMStroke lineSymbol)
        {
            // Definir colores según la función GetRGBColor
            Func<int, int, int, CIMRGBColor> GetRGBColor = (r, g, b) => new CIMRGBColor
            {
                R = r,
                G = g,
                B = b,
                Alpha = 0 // Opacidad completa
            };

            // Obtener el diccionario de configuraciones
            var symbolConfigurations = GetSymbolConfigurations();

            // Asignar simbología basada en el diccionario
            if (symbolConfigurations.TryGetValue(layerName, out var config))
            {
                lineSymbol = CreateLineSymbol(config.LineStyle, config.LineColor, config.LineWidth);
                fillSymbol = CreatePolygonSymbol(config.FillStyle, config.FillColor, lineSymbol);
            }
            else
            {
                // Manejar configuraciones especiales o predeterminadas
                if (layerName.Equals("Provincia", StringComparison.OrdinalIgnoreCase))
                {
                    if ("v_opcion_modulo".Equals("OP_25", StringComparison.OrdinalIgnoreCase))
                    {
                        lineSymbol = CreateLineSymbol(SimpleLineStyle.Solid, GetRGBColor(225, 225, 225), 1);
                        fillSymbol = CreatePolygonSymbol(SimpleFillStyle.Horizontal, GetRGBColor(225, 225, 225), lineSymbol);
                    }
                    else
                    {
                        lineSymbol = CreateLineSymbol(SimpleLineStyle.Solid, GetRGBColor(0, 168, 132), 1);
                        fillSymbol = CreatePolygonSymbol(SimpleFillStyle.Cross, GetRGBColor(0, 168, 132), lineSymbol);
                    }
                }
                else
                {
                    // Asignar una simbología predeterminada si la capa no está en el diccionario
                    lineSymbol = CreateLineSymbol(SimpleLineStyle.DashDotDot, GetRGBColor(76, 230, 0), 0.2);
                    fillSymbol = CreatePolygonSymbol(SimpleFillStyle.DiagonalCross, GetRGBColor(76, 230, 0), lineSymbol);
                }
            }
        }
        private static Dictionary<string, SymbolConfiguration> GetSymbolConfigurations()
        {
            Func<int, int, int, CIMRGBColor> GetRGBColor = (r, g, b) => new CIMRGBColor
            {
                R = r,
                G = g,
                B = b,
            };

            var symbolConfigurations = new Dictionary<string, SymbolConfiguration>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "union",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(230, 76, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 76, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Intersección",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(56, 168, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(56, 168, 0),
                        LineWidth = 1
                    }
                },
                {
                    "traslape",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Cross,
                        FillColor = GetRGBColor(255, 255, 0),
                        LineStyle = SimpleLineStyle.Dot,
                        LineColor = GetRGBColor(230, 230, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Zona Urbana",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(255, 127, 127),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(255, 127, 127),
                        LineWidth = 1
                    }
                },
                {
                    "Catastro Forestal",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(255, 255, 115),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(168, 112, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Zona Reservada",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(76, 230, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(76, 230, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Caram",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.ForwardDiagonal,
                        FillColor = GetRGBColor(255, 100, 100),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(255, 100, 100),
                        LineWidth = 1
                    }
                },
                 {
                    "Cuadri_Suptot",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.BackwardDiagonal,
                        FillColor = GetRGBColor(76, 230, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(76, 230, 0),
                        LineWidth = 0.2
                    }
                },
                {
                    "Poligono",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Division",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1.2
                    }
                },
                {
                    "Renuncia",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1.0
                    }
                },
                {
                    "Acumulacion",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(187, 0, 255),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(187, 0, 255),
                        LineWidth = 1.2
                    }
                },
                {
                    "Acumulacion_revisado",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1.2
                    }
                },
                {
                    "Integra_Acumulacion",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(0, 0, 230),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(0, 0, 230),
                        LineWidth = 1.2
                    }
                },
                {
                    "Afectacion",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(0, 0, 230),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(0, 0, 230),
                        LineWidth = 1.2
                    }
                },
                {
                    "Beneficio",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(0, 0, 230),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(0, 0, 230),
                        LineWidth = 1.2
                    }
                },
                {
                    "Areas Urbanas",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(255, 170, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(255, 170, 0),
                        LineWidth = 3
                    }
                },
                {
                    "Areas Restringidas",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(0, 115, 76),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(0, 115, 76),
                        LineWidth = 3
                    }
                },
                {
                    "Cuadriculas_10HAS",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1
                    }
                },
                {
                    "Cuadriculas_100HAS",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 2
                    }
                },
                {
                    "Cuadricula Regional",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 2
                    }
                },
                {
                    "AreaReserva_100Ha",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 2
                    }
                },
                {
                    "ZonaUrbana_10Ha",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 2
                    }
                },
                {
                    "Departamento",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(168, 112, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(168, 112, 0),
                        LineWidth = 1 // Este valor puede ser ajustado según el valor de val_opcion_plano_Ar
                    }
                },
                {
                    "Provincia",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(0, 168, 132),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(0, 168, 132),
                        LineWidth = 1 // Este valor puede ser ajustado según el valor de val_opcion_plano_Ar
                    }
                },
                {
                    "Distrito",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1 // Este valor puede ser ajustado según el valor de val_opcion_plano_Ar
                    }
                },
                {
                    "Rectangulo",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(230, 0, 0),
                        LineWidth = 1 // Ajustar según val_opcion_plano_Ar
                    }
                },
                {
                    "Catastro Minero",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null,
                        FillColor = GetRGBColor(169, 0, 230),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(169, 0, 230),
                        LineWidth = 2
                    }
                },
                {
                    "DM_Uso_Minero",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Solid,
                        FillColor = GetRGBColor(115, 223, 255),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(115, 223, 255),
                        LineWidth = 1
                    }
                },
                {
                    "DM_Actividad_Minera",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Solid,
                        FillColor = GetRGBColor(255, 255, 115),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(255, 255, 115),
                        LineWidth = 1
                    }
                },
                {
                    "Hoja",
                    new SymbolConfiguration
                    {
                        FillStyle = SimpleFillStyle.Null, // Asumiendo que el estilo es Hollow según VB.NET
                        FillColor = GetRGBColor(230, 0, 0),
                        LineStyle = SimpleLineStyle.Solid,
                        LineColor = GetRGBColor(76, 230, 0),
                        LineWidth = 1
                    }
                }
                // Agrega más configuraciones según sea necesario...

                // Configuraciones compartidas por múltiples capas
            };

            // Configuraciones compartidas para capas renurets
            var renurets = new List<string> { "Renuret1", "Renuren1", "Renuret2", "Renuren2", "Renuret3", "Renuren3", "Renuret4", "Renuren4" };
            var renuretConfig = new SymbolConfiguration
            {
                FillStyle = SimpleFillStyle.Horizontal,
                FillColor = GetRGBColor(0, 0, 230),
                LineStyle = SimpleLineStyle.Solid,
                LineColor = GetRGBColor(0, 0, 230),
                LineWidth = 1.0
            };
            foreach (var layer in renurets)
            {
                symbolConfigurations.Add(layer, renuretConfig);
            }

            // Agregar más configuraciones compartidas según sea necesario

            return symbolConfigurations;
        }

        /// <summary>
        /// Crea un símbolo de relleno de polígono simple.
        /// </summary>
        /// <param name="style">Estilo de relleno.</param>
        /// <param name="color">Color de relleno.</param>
        /// <returns>Objeto CIMPolygonSymbol configurado.</returns>
        private static CIMPolygonSymbol CreatePolygonSymbol(SimpleFillStyle style, CIMRGBColor color, CIMStroke lineSymbol)
        {
            return SymbolFactory.Instance.ConstructPolygonSymbol(
                        color, style, lineSymbol); ;
        }

        /// <summary>
        /// Crea un símbolo de línea simple.
        /// </summary>
        /// <param name="style">Estilo de línea.</param>
        /// <param name="color">Color de línea.</param>
        /// <param name="width">Ancho de línea.</param>
        /// <returns>Objeto CIMLineSymbol configurado.</returns>
        private static CIMStroke CreateLineSymbol(SimpleLineStyle style, CIMRGBColor color, double width)
        {
            return SymbolFactory.Instance.ConstructStroke(color, width, style);
        }

        /// <summary>
        /// Helper para crear un objeto CIMRGBColor.
        /// </summary>
        /// <param name="r">Rojo (0-255).</param>
        /// <param name="g">Verde (0-255).</param>
        /// <param name="b">Azul (0-255).</param>
        /// <returns>Objeto CIMRGBColor.</returns>
        private CIMRGBColor GetRGBColor(int r, int g, int b)
        {
            return new CIMRGBColor
            {
                R = r,
                G = g,
                B = b,
                Alpha = 0 // Opacidad completa
            };
        }
    }
    public class SymbolConfiguration
    {
        public SimpleFillStyle FillStyle { get; set; }
        public CIMRGBColor FillColor { get; set; }
        public SimpleLineStyle LineStyle { get; set; }
        public CIMRGBColor LineColor { get; set; }
        public double LineWidth { get; set; }
    }
}
