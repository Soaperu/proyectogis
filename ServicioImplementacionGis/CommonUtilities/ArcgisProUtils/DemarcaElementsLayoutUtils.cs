using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.ArcgisProUtils
{
    public class DemarcaElementsLayoutUtils
    {
        // Constantes para la longitud máxima del texto y el tamaño de fuente por defecto
        private const int MaxTextLength = 40;
        private const double DefaultFontSize = 7.0;
        private string sele_reporte = "Demarca";
        private string v_nombre_dm = GlobalVariables.CurrentNameDm;
        private string v_codigo = GlobalVariables.CurrentCodeDm;
        private string v_zona_dm= GlobalVariables.CurrentZoneDm;
        private string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        Layout? _layout;
        // Estructura para almacenar las propiedades de cada contatexto

        /// <summary>
        /// Agrega elementos de texto al layout basado en el reporte "Demarca".
        /// </summary>
        /// <param name="sele_reporte">Tipo de reporte.</param>
        /// <param name="v_nombre_dm">Nombre de la demarcación.</param>
        /// <param name="v_codigo">Código asociado.</param>
        /// <param name="lista_dist_dema">Lista de dist demarcación política.</param>
        /// <param name="lista_dist">Lista de dist demarcación normal.</param>
        /// <param name="caso_consulta1">Tipo de consulta.</param>
        /// <param name="lista_prov_dema">Lista de provincias demarcación política.</param>
        /// <param name="lista_prov">Lista de provincias normal.</param>
        /// <param name="lista_depa_dema">Lista de departamentos demarcación política.</param>
        /// <param name="lista_depa">Lista de departamentos normal.</param>
        /// <param name="fecha">Fecha actual.</param>
        /// <param name="v_zona_dm">Zona de demarcación.</param>
        /// <param name="layout">Layout activo donde se agregarán los elementos de texto.</param>
        public async Task AddDemarcaTextAsync(
            //string sele_reporte,
            //string v_nombre_dm,
            //string v_codigo,
            string lista_dist_dema,
            string lista_dist,
            string caso_consulta1,
            string lista_prov_dema,
            string lista_prov,
            string lista_depa_dema,
            string lista_depa,
            //string fecha,
            LayoutProjectItem layoutItem)
        {

            // Definir el mapeo para cada contatexto
            var textMappings = new Dictionary<int, TextProperties>
        {
            { 1, new TextProperties
                {
                    RequiresConditionalText = false,
                    Text = v_nombre_dm,
                    XStart = 21.7,
                    YStart = 9.9
                }
            },
            { 2, new TextProperties
                {
                    RequiresConditionalText = false,
                    Text = v_codigo,
                    XStart = 21.7,
                    YStart = 9.2
                }
            },
            { 3, new TextProperties
                {
                    RequiresConditionalText = true,
                    ConditionalTextFunc = (text) => caso_consulta1 == "TIPO DEMARCACION POLITICA" ? lista_dist_dema : lista_dist,
                    XStart = 21.7,
                    YStart = 8.4
                }
            },
            { 4, new TextProperties
                {
                    RequiresConditionalText = true,
                    ConditionalTextFunc = (text) => caso_consulta1 == "TIPO DEMARCACION POLITICA" ? lista_prov_dema : lista_prov,
                    XStart = 21.7,
                    YStart = 7.6
                }
            },
            { 5, new TextProperties
                {
                    RequiresConditionalText = true,
                    ConditionalTextFunc = (text) => caso_consulta1 == "TIPO DEMARCACION POLITICA" ? lista_depa_dema : lista_depa,
                    XStart = 21.7,
                    YStart = 6.7
                }
            },
            { 6, new TextProperties
                {
                    RequiresConditionalText = false,
                    Text = fecha,
                    XStart = 19.8,
                    YStart = 2.1
                }
            },
            { 7, new TextProperties
                {
                    RequiresConditionalText = false,
                    Text = string.Empty, // No se proporciona texto en el código original
                    XStart = 22.1,
                    YStart = 2.1
                }
            },
            { 8, new TextProperties
                {
                    RequiresConditionalText = false,
                    Text = v_zona_dm,
                    XStart = 9.6,
                    YStart = 1.4
                }
            }
        };

            // Definir el color del texto (puedes modificarlo según tus necesidades)
            var textColor = ColorFactory.Instance.BlackRGB; 

            // Nombre de la fuente
            string fontName = "Tahoma";

            // Ejecutar las operaciones en el hilo principal de ArcGIS Pro
            await QueuedTask.Run(async () =>
            {
                _layout = layoutItem.GetLayout();
                foreach (var kvp in textMappings)
                {
                    int contatexto = kvp.Key;
                    TextProperties props = kvp.Value;

                    string textToAdd = props.Text;

                    // Manejar texto condicional
                    if (props.RequiresConditionalText && props.ConditionalTextFunc != null)
                    {
                        textToAdd = props.ConditionalTextFunc.Invoke(props.Text);
                    }

                    // Dividir el texto si es necesario
                    string formattedText = SplitText(textToAdd, MaxTextLength);

                    // Saltar la creación del elemento si el texto está vacío
                    if (string.IsNullOrEmpty(formattedText))
                        continue;

                    // Crear el símbolo de texto usando SymbolFactory
                    var textSymbol = SymbolFactory.Instance.ConstructTextSymbol(
                        textColor,         // Color del texto
                        DefaultFontSize,   // Tamaño de la fuente
                        fontName,          // Nombre de la fuente
                        "Regular"   // Estilo de la fuente (Normal, Cursiva, etc.)
                    );

                    // Crear un LayoutPoint para la posición del texto
                    // Asegúrate de que las unidades coincidan con las del layout (por ejemplo, pulgadas, centímetros)
                    Coordinate2D coord = new Coordinate2D(props.XStart, props.YStart);
                    var mapPoint = MapPointBuilderEx.CreateMapPoint(coord);

                    // Crear el elemento de texto gráfico usando ElementFactory
                    var textGraphicElement = ElementFactory.Instance.CreateTextGraphicElement(
                        _layout,
                        TextType.PointText,
                        mapPoint,
                        textSymbol,
                        formattedText
                    );

                    // Opcional: Ajustar propiedades adicionales del elemento de texto si es necesario
                    // Por ejemplo, rotación, alineación, etc.
                }

            });
        }

        /// <summary>
        /// Divide el texto de entrada en dos líneas si excede la longitud máxima.
        /// </summary>
        /// <param name="input">Texto de entrada.</param>
        /// <param name="maxLength">Longitud máxima permitida.</param>
        /// <returns>Texto formateado con salto de línea si es necesario.</returns>
        private static string SplitText(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length > maxLength)
            {
                string firstPart = input.Substring(0, maxLength);
                string secondPart = input.Substring(maxLength);
                return $"{firstPart}\n{secondPart}";
            }
            else
            {
                return input;
            }
        }
    }
    public class TextProperties
    {
        public string Text { get; set; }
        public double XStart { get; set; }
        public double YStart { get; set; }
        public bool RequiresConditionalText { get; set; }
        public Func<string, string> ConditionalTextFunc { get; set; }
    }
}
