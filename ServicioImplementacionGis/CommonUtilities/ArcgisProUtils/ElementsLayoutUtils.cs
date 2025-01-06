using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Mapping.CommonControls.Ribbon;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilities.ArcgisProUtils.Models;

namespace CommonUtilities.ArcgisProUtils
{
    public class ElementsLayoutUtils
    {
        private readonly string? layoutName;
        private Layout? _layout;
        private Dictionary<string, int> dictCriterios = new Dictionary<string, int>();
        // Variables de ejemplo (estas deberían ser proporcionadas externamente)
        private string v_carta_dm = GlobalVariables.CurrentCodeDm;
        private string v_codigo = GlobalVariables.CurrentCodeDm;
        private string v_area_eval = GlobalVariables.CurrentAreaDm;
        private string v_nombre_dm = GlobalVariables.CurrentNameDm;
        private string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        private string v_tipo_exp = "";
        private int? Cuenta_rd = null;

        //public ElementsLayoutUtils(string layoutName)
        //{
        //    this.layoutName = layoutName;
        //}

        /// <summary>
        /// Carga el layout y ejecuta la operación en el contexto de ArcGIS Pro (QueuedTask).
        /// </summary>
        public async Task<double> AgregarTextosLayoutAsync(string seleReporte, LayoutProjectItem layoutItem, double y)
        {
            double yPre = y;
            return await QueuedTask.Run(() =>
            {
                _layout = layoutItem.GetLayout();
                if (_layout == null)
                    throw new Exception("No se pudo obtener el layout.");
                
                // Supongamos que sele_reporte = "Evaluacion"
                if (seleReporte == "Evaluacion")
                {
                    // Aquí podríamos usar una lista de definiciones:
                    var textos = GetTextDefinitionsForEvaluation(y);

                    // Crear un símbolo de texto básico
                    //CIMTextSymbol textSymbol = CrearSimboloTexto(ColorFromRGB(0, 0, 0), 10, "Arial");

                    // Agregar cada elemento
                    foreach (var item in textos)
                    {
                        CIMTextSymbol textSymbol = CrearSimboloTexto(item.color, item.fontSize, "Tahoma");
                        CrearTextElement(item.Texto, item.X, item.Y, textSymbol);
                        yPre = item.Y;
                    }
                }
                return yPre;
            });
            
        }
        /// <summary>
        /// Crea un elemento de texto en el layout.
        /// </summary>
        public void CrearTextElement(string texto, double x, double y, CIMTextSymbol textSymbol)
        {
            // Crear el punto de ubicación
            Coordinate2D coord = new Coordinate2D(x, y);
            var mapPoint = MapPointBuilderEx.CreateMapPoint(coord);
            // Crear el elemento de texto
            var textElement = ElementFactory.Instance.CreateTextGraphicElement(_layout, TextType.PointText, mapPoint, textSymbol, texto);

            // Opcional: Ajustar propiedades adicionales del elemento si es necesario
            // Por ejemplo, textElement.SetName("NombreDeElemento");
        }

        /// <summary>
        /// Crea un símbolo de texto sencillo.
        /// </summary>
        public CIMTextSymbol CrearSimboloTexto(CIMColor color, double tamañoFuente, string fuente, string estilo = "Regular")
        {
            return SymbolFactory.Instance.ConstructTextSymbol(color, tamañoFuente, fuente, estilo);
        }

        /// <summary>
        /// Crea un color CIM a partir de valores RGB.
        /// </summary>
        private CIMColor ColorFromRGB(byte r, byte g, byte b)
        {
            return new CIMRGBColor { R = r, G = g, B = b, Alpha = 100 };
        }

        private string FormatearTextoResultado(ResultadoEval res)
        {
            // Lógica de formateo de texto
            return $"{res.Contador.PadRight(3)} {res.Concesion.PadRight(25)} {res.CodigoU.PadRight(20)} {res.TipoEx.PadRight(10)} {res.Eval.PadRight(10)} {res.Estado}";
        }
        private string FormatearTexto(string v_campo1, string v_campo2, string v_campo3, string v_campo4, string v_campo5, string v_campo6, string v_campo7, string v_campo8, string v_campo9)
        {
            // Basado en la lógica original, ajusta el espaciado según la longitud de v_campo1, etc.
            // Por simplicidad, se presenta una versión simplificada:
            string cadenatexto3 = v_campo3.PadRight(13); // Asegurar ancho para el codigo
            string cadenatexto2 = v_campo2.PadRight(30, ' ');

            string cadenatexto4 = (v_campo8.Length == 2) ? v_campo8 + "  " : v_campo8 + "   ";

            // Ajustar espacios según la longitud de v_campo1
            string cadenatexto = "";
            int lenContador = v_campo1.Length;
            if (lenContador == 1)
            {
                cadenatexto = v_campo1 + "    " + cadenatexto2 + " " + cadenatexto3 + "  " + v_campo5 + "   " + v_campo6 + "   " + cadenatexto4 + "  " + v_campo9;
            }
            else if (lenContador == 2)
            {
                cadenatexto = v_campo1 + "   " + cadenatexto2 + " " + cadenatexto3 + "  " + v_campo5 + "   " + v_campo6 + "   " + cadenatexto4 + "  " + v_campo9;
            }
            else if (lenContador == 3)
            {
                // Este caso en el código original era más complejo, aquí simplificamos:
                cadenatexto = v_campo1 + "  " + cadenatexto2 + " " + cadenatexto3 + v_campo4 + "   " + v_campo5 + "   " + v_campo6 + "   " + v_campo7 + "   " + cadenatexto4 + "  " + v_campo9;
            }
            else
            {
                // Por si no se cumple ninguno, usar un fallback:
                cadenatexto = v_campo1 + " " + cadenatexto2 + " " + cadenatexto3 + v_campo5 + " " + v_campo6 + " " + cadenatexto4 + " " + v_campo9;
            }

            return cadenatexto;
        }

        /// <summary>
        /// Devuelve una lista con la definición de cada texto a colocar en el layout para el caso "Evaluacion".
        /// Esto reemplaza la larga cadena de If/Else del código original.
        /// </summary>
        private (string Texto, double X, double Y, CIMColor color, int fontSize)[] GetTextDefinitionsForEvaluation(double posY)
        {
            // Aquí simplificamos. Ajusta las coordenadas según el codigo anterior.
            CIMColor colorMagenta = ColorFromRGB(197, 0, 255);
            int fontSizeMagenta = 8; 
            CIMColor colorRed = ColorFromRGB(230, 0, 0);
            int fontSizeRedBlue = 7;
            CIMColor colorBlue = ColorFromRGB(71, 61, 255);
            CIMColor colorBlack = ColorFromRGB(0, 0, 0);

            var textList = new List<(string Texto, double X, double Y, CIMColor color, int fontSize)>()
            {
            ("Carta: " + v_carta_dm, 9.2, 17.8, colorBlue, fontSizeRedBlue),
            ("Fecha: " + fecha, 14.8, 17.8, colorBlue, fontSizeRedBlue),
            ("CÓDIGO DEL DM: " + v_codigo, 18.2, 17.3, colorMagenta, fontSizeMagenta),
            ("NOMBRE DEL DM: " + v_nombre_dm, 18.2, 16.7, colorMagenta, fontSizeMagenta),
            ("HECTÁREA: " + Math.Round(double.Parse(v_area_eval),4) + " Ha.", 18.2, 16.1, colorMagenta, fontSizeMagenta),
                // Agregar aquí el resto de textos con sus posiciones,
                // tomando como referencia lo que hacía el código original.
                // Por ejemplo:
                // ("DERECHOS PRIORITARIOS: (X)", 18.2, 15.5),
                // ...
            };

            // contatexto=6: DERECHOS PRIORITARIOS
            string derechosPriText = (dictCriterios["PR"] == 0)
                ? "DERECHOS PRIORITARIOS : No Presenta DM Prioritarios"
                : "DERECHOS PRIORITARIOS : (" + dictCriterios["PR"] + ")";
            textList.Add((derechosPriText, 18.2, 15.7, colorBlue, fontSizeRedBlue));
            posY -= 0.4;

            // contatexto=7: Petitorio RD
            string petitorioTexto;
            if (v_tipo_exp == "RD")
            {
                if (Cuenta_rd == 0)
                    petitorioTexto = "Petitorio formulado al amparo del Art.12 de la Ley 26615, sobre el área del derecho extinguido y publicado de libre denunciabilidad";
                else
                    petitorioTexto = "Petitorio formulado al amparo del Art.12 de la Ley 26615, sobre el área del derecho extinguido y publicado de libre denunciabilidad (" + Cuenta_rd + ")";
                textList.Add((petitorioTexto, 18.2, posY, colorBlue, fontSizeRedBlue));
                posY -= 0.4;
            }
            else
            {
                //textList.Add(("Petitorio sin RD - coords fijas", 18.2, posY));
                //posY -= 0.4;
            }

            // contatexto=8: DERECHOS POSTERIORES/RESPETAR
            string derechosPostText;
            if (dictCriterios["PO"] == 0)
            {
                if (v_tipo_exp == "RD")
                    derechosPostText = "DERECHOS QUE DEBEN RESPETAR EL ÁREA PETICIONADA : No Presenta";
                else
                    derechosPostText = "DERECHOS POSTERIORES : No Presenta DM Posteriores";
            }
            else
            {
                if (v_tipo_exp == "RD")
                    derechosPostText = "DERECHOS QUE DEBEN RESPETAR EL ÁREA PETICIONADA: (" + dictCriterios["PO"] + ")";
                else
                    derechosPostText = "DERECHOS POSTERIORES : (" + dictCriterios["PO"] + ")";
            }
            textList.Add((derechosPostText, 18.2, posY, colorBlue, fontSizeRedBlue));
            posY -= 0.4;

            // contatexto=9: DERECHOS SIMULTÁNEOS
            string derechosSimText = (dictCriterios["SI"] == 0)
                ? "DERECHOS SIMULTÁNEOS : No Presenta DM Simultáneos"
                : "DERECHOS SIMULTÁNEOS : (" + dictCriterios["SI"] + ")";
            textList.Add((derechosSimText, 18.2, posY, colorBlue, fontSizeRedBlue));
            posY -= 0.4;

            // contatexto=10: DERECHOS EXTINGUIDOS
            string derechosExText = (dictCriterios["EX"] == 0)
                ? "DERECHOS EXTINGUIDOS : No Presenta DM Extinguidos"
                : "DERECHOS EXTINGUIDOS : (" + dictCriterios["EX"] + ")";
            textList.Add((derechosExText, 18.2, posY, colorBlue, fontSizeRedBlue));
            posY -= 0.4;

            // contatexto=11: CATASTRO NO MINERO
            textList.Add(("CATASTRO NO MINERO", 18.2, posY, colorRed, fontSizeRedBlue));
            posY -= 0.4;
            string listaCaramur = GlobalVariables.resultadoEvaluacion.listaCaramUrbana;
            string listaCaramre = GlobalVariables.resultadoEvaluacion.listaCaramReservada;
            string listaCforestal = GlobalVariables.resultadoEvaluacion.listaCatastroforestal;
            // contatexto=12: Zonas Urbanas
            string zonasUrbanasText;
            if (string.IsNullOrEmpty(GlobalVariables.resultadoEvaluacion.listaCaramUrbana))
            {
                zonasUrbanasText = "Zonas Urbanas : No se encuentra superpuesto a un Área urbana";
            }
            else
            {
                if (GlobalVariables.resultadoEvaluacion.listaCaramUrbana.Length > 65)
                {
                    string posi_x = listaCaramur.Substring(0, 65);
                    string posi_x1 = listaCaramur.Substring(65);
                    zonasUrbanasText = "Zonas Urbanas : " + posi_x + "\n" + posi_x1;
                }
                else
                {
                    zonasUrbanasText = "Zonas Urbanas : " + listaCaramur;
                }
            }
            textList.Add((zonasUrbanasText, 18.2, posY, colorBlack, 6));
            posY -= 0.4;

            // contatexto=13: Zonas Reservadas
            string zonasReservadasText;
            if (string.IsNullOrEmpty(listaCaramre))
            {
                zonasReservadasText = "Zonas Reservadas : No se encuentra superpuesto a un Área de Reserva";
            }
            else
            {
                if (listaCaramre.Length > 65)
                {
                    string posi_x = listaCaramre.Substring(0, 65);
                    string posi_x1 = listaCaramre.Substring(65);
                    zonasReservadasText = "Zonas Reservadas : " + posi_x + "\n" + posi_x1;
                }
                else
                {
                    zonasReservadasText = "Zonas Reservadas : " + listaCaramre;
                }
            }
            textList.Add((zonasReservadasText, 18.2, posY, colorBlack, 6));
            posY -= 0.4;

            // contatexto=14: Cobertura temática SERFOR
            string serforText;
            if (string.IsNullOrEmpty(listaCforestal))
            {
                serforText = "Cobertura temática SERFOR : NO PRESENTA COBERTURAS TEMÁTICAS";
            }
            else
            {
                if (listaCforestal.Length > 65)
                {
                    string posi_x = listaCforestal.Substring(0, 65);
                    string posi_x1 = listaCforestal.Substring(65);
                    serforText = "Cobertura temática SERFOR : " + posi_x + "\n" + posi_x1;
                }
                else
                {
                    serforText = "Cobertura temática SERFOR : " + listaCforestal.ToLowerInvariant();
                }
            }
            textList.Add((serforText, 18.2, posY, colorBlack, 6));
            posY -= 0.4;

            // contatexto=15: Límites fronterizos
            var distanciaFront = GlobalVariables.DistBorder;
            string limitesFronText = "Límites fronterizos (Fuente IGN): Distancia de la línea de frontera de " + distanciaFront + " (Km.)";
            textList.Add((limitesFronText, 18.2, posY, colorBlack, 6));
            posY -= 0.4;

            // contatexto=16: LISTADO DE DERECHOS MINEROS
            string listadoDMText = "                                      LISTADO DE DERECHOS MINEROS";
            textList.Add((listadoDMText, 18.2, posY, colorBlack, 6));
            posY -= 0.4;

            // contatexto=17: Encabezado de columnas
            string encabezadoDM = "Nº      NOMBRE                                    CÓDIGO              TE    TP   INCOR  SUST";
            textList.Add((encabezadoDM, 18.2, posY, colorBlue, fontSizeRedBlue));
            posY -= 0.4;
            return textList.ToArray();
        }

        public async Task<(double finalPosX, double finalPosY)> TextElementsEvalAsync(LayoutProjectItem layoutItem)
        {
            //var layoutItem = Project.Current.GetItems<LayoutProjectItem>()
            //    .FirstOrDefault(l => l.Name.Equals(layoutName, StringComparison.OrdinalIgnoreCase));
            //dictCriterios = new Dictionary<string, int>();
            if (layoutItem == null)
                throw new Exception($"No se encontró el layout {layoutName}");

            // Variables de posición definidas aquí para que sean accesibles después del QueuedTask
            double posX = 18.2; // Posición inicial X (ejemplo)
            double posY = 15.2; //14.6; // Posición inicial Y (ejemplo)

            await QueuedTask.Run(() =>
            {
                _layout = layoutItem.GetLayout();
                if (_layout == null)
                    throw new Exception("No se pudo obtener el layout.");

                // Crear símbolo de texto
                CIMTextSymbol textSymbol = CrearSimboloTexto(ColorFromRGB(0, 0, 0), 6.0, "Courier New");

                // Criterios a procesar
                var criterios = new string[] {"PR", "AR", "PO", "SI", "EX"};
                //int contador = 0;
                foreach (var criterio in criterios)
                {
                    
                    // Obtener resultados para el criterio actual
                    //var resultados = ObtenerResultadosEval(criterio).Result;
                    var resultados = GlobalVariables.resultadoEvaluacion.ResultadosCriterio[criterio];

                    if (resultados.Count > 0)
                    {
                        //contador += 1;
                        // Procesar resultados, colocar textos
                        foreach (var res in resultados)
                        {
                            string texto = FormatearTextoResultado(res);
                            CrearTextElement(texto, posX, posY, textSymbol);
                            posY -= 0.35; // Ajustar la posición vertical después de cada texto
                        }
                    }
                    else
                    {
                        // Ajustar posición si no hubo resultados, según la lógica original
                        //posY -= 0.4;
                    }
                    dictCriterios[criterio] = resultados.Count;
                    // Aquí se podrían hacer ajustes adicionales a posX y posY según el criterio,
                    // basándose en la lógica original.
                    // Por ahora asumimos que posX no cambia, y sólo posY se ajusta.
                }

                // Opcionalmente puedes modificar posX y posY acá si lo requieres
                // antes de salir del QueuedTask.

            }); // fin del QueuedTask.Run

            // Devuelve las coordenadas finales después de recorrer todos los criterios
            return (posX, posY);
        }

        public async Task<List<ResultadoEval>> ObtenerResultadosEval(string criterio)
        {
            // Aquí se implementaría la lógica para consultar la FeatureClass
            // y obtener la lista de resultados según el criterio.
            List<ResultadoEval> resultados = new List<ResultadoEval>();

            // Ejecutar en el hilo adecuado de ArcGIS Pro
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            await QueuedTask.Run(async() =>
            {
                
                Map mapCatastro = await MapUtils.FindMapByNameAsync(GlobalVariables.mapNameCatastro);
                
                var zoomNameLayer = mapCatastro.GetLayersAsFlattenedList().OfType<Layer>().FirstOrDefault(l => l.Name == GlobalVariables.CurrentShpName);
                var catastroLayer = (FeatureLayer)zoomNameLayer;
                
                // Obtener la FeatureClass asociada
                FeatureClass featureClass = catastroLayer.GetFeatureClass();
                if (featureClass == null)
                    throw new Exception("No se pudo obtener la FeatureClass de la capa 'catastro'.");

                // Crear el QueryFilter con la cláusula WHERE
                QueryFilter queryFilter = new QueryFilter
                {
                    WhereClause = $"EVAL = '{criterio}'"
                };

                // Buscar los campos necesarios
                // Suponemos que existen todos los campos: "CONTADOR", "CONCESION", "TIPO_EX", "CODIGOU", "ESTADO", "EVAL"
                int idxContador = featureClass.GetDefinition().FindField("CONTADOR");
                int idxConcesion = featureClass.GetDefinition().FindField("CONCESION");
                int idxTipoEx = featureClass.GetDefinition().FindField("TIPO_EX");
                int idxCodigoU = featureClass.GetDefinition().FindField("CODIGOU");
                int idxEstado = featureClass.GetDefinition().FindField("ESTADO");
                int idxEval = featureClass.GetDefinition().FindField("EVAL");

                if (idxContador < 0 || idxConcesion < 0 || idxTipoEx < 0 || idxCodigoU < 0 || idxEstado < 0 || idxEval < 0)
                    throw new Exception("No se encontraron todos los campos requeridos en la FeatureClass 'catastro'.");

                // Ejecutar la consulta
                using (RowCursor cursor = featureClass.Search(queryFilter, false))
                {
                    while (cursor.MoveNext())
                    {
                        using (Feature feature = (Feature)cursor.Current)
                        {
                            // Leer los valores de los campos
                            string contador = feature[idxContador]?.ToString() ?? string.Empty;
                            string concesion = feature[idxConcesion]?.ToString() ?? string.Empty;
                            string tipoEx = feature[idxTipoEx]?.ToString() ?? string.Empty;
                            string codigoU = feature[idxCodigoU]?.ToString() ?? string.Empty;
                            string estado = feature[idxEstado]?.ToString() ?? string.Empty;
                            string eval = feature[idxEval]?.ToString() ?? string.Empty;

                            // Crear un objeto ResultadoEval y agregarlo a la lista
                            var resultado = new ResultadoEval
                            {
                                Contador = contador,
                                Concesion = concesion,
                                TipoEx = tipoEx,
                                CodigoU = codigoU,
                                Estado = estado,
                                Eval = eval
                            };
                            resultados.Add(resultado);
                        }
                    }
                }
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma

            return resultados;
            //return new List<ResultadoEval>();
        }

        public async Task GeneralistaDmPlanoEvaAsync(double posi_y1_list)
        {
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            await QueuedTask.Run(async() =>
            {
                //// Obtener el layout
                
                MapFrame mfrm = _layout.FindElement(GlobalVariables.mapNameCatastro + " Map Frame") as MapFrame;
                Map mapCatastro = await MapUtils.FindMapByNameAsync(GlobalVariables.mapNameCatastro);

                var zoomNameLayer = mapCatastro.GetLayersAsFlattenedList().OfType<Layer>().FirstOrDefault(l => l.Name == GlobalVariables.CurrentShpName);
                var catastroLayer = (FeatureLayer)zoomNameLayer;

                FeatureClass featureClass = catastroLayer.GetFeatureClass();
                if (featureClass == null)
                    throw new Exception("No se pudo obtener la FeatureClass de 'Catastro'.");

                // Indices de campos
                int idxContador = featureClass.GetDefinition().FindField("CONTADOR");
                int idxConcesion = featureClass.GetDefinition().FindField("CONCESION");
                int idxCodigou = featureClass.GetDefinition().FindField("CODIGOU");
                int idxZona = featureClass.GetDefinition().FindField("ZONA");
                int idxTipoEx = featureClass.GetDefinition().FindField("TIPO_EX");
                int idxEstado = featureClass.GetDefinition().FindField("ESTADO");
                int idxDepubl = featureClass.GetDefinition().FindField("DE_PUBL");
                int idxDeiden = featureClass.GetDefinition().FindField("DE_IDEN");
                int idxNatura = featureClass.GetDefinition().FindField("NATURALEZA");

                // Crear un Search sin filtro (Nothing,False en VB era sin filtro)
                QueryFilter qf = new QueryFilter();
                // Crear el símbolo de texto
                
                List<(string texto, double x, double y, CIMTextSymbol symbol)> texts = new List<(string, double, double, CIMTextSymbol)>();

                double posi_y1 = posi_y1_list - 0.3;
                double posi_y = posi_y1_list - 0.3;

                
                
                using (RowCursor cursor = featureClass.Search(qf, false))
                {
                    int conta1 = 0;
                    while (cursor.MoveNext())
                    {
                        CIMTextSymbol textSymbol = CrearSimboloTexto(ColorFromRGB(0, 0, 0), 6.0, "Courier New");
                        using (Feature feature = (Feature)cursor.Current)
                        {
                            conta1++;
                            string v_campo1 = feature[idxContador]?.ToString() ?? "";
                            string v_campo2 = feature[idxConcesion]?.ToString().Trim() ?? "";
                            string v_campo3 = feature[idxCodigou]?.ToString().Trim() ?? "";
                            string v_campo4 = feature[idxZona]?.ToString().Trim() ?? "";
                            string v_campo5 = feature[idxTipoEx]?.ToString().Trim() ?? "";
                            string v_campo6 = feature[idxEstado]?.ToString().Trim() ?? "";
                            string v_campo7 = feature[idxDepubl]?.ToString().Trim();
                            if (string.IsNullOrEmpty(v_campo7)) v_campo7 = "NP";

                            string v_campo8 = feature[idxDeiden]?.ToString().Trim();
                            if (string.IsNullOrEmpty(v_campo8)) v_campo8 = "NI";

                            string v_campo9 = feature[idxNatura]?.ToString().Trim() ?? "";

                            // Comprobar si hay espacio en el layout
                            if (posi_y < 1.2 || posi_y1 < 1.2)
                                break; // Sale si no hay más espacio

                            // Ajustar color si es el DM evaluado
                            //CIMTextSymbol currentSymbol = textSymbol;
                            if (v_campo3 == v_codigo)
                            {
                                textSymbol = CrearSimboloTexto(ColorFromRGB(169, 0, 230), 6.0, "Courier New");
                            }

                            string cadenatexto = FormatearTexto(v_campo1, v_campo2, v_campo3, v_campo4, v_campo5, v_campo6, v_campo7, v_campo8, v_campo9);

                            // Agregar el texto a la lista
                            // En el código original se usaba pPoint.X = 17.2 y luego pPoint.X = 18.2
                            // Asumimos que finalmente se usa (18.2, posi_y)
                            double x = 18.2;
                            double y = posi_y;

                            texts.Add((cadenatexto, x, y, textSymbol));

                            // Actualizar posi_y
                            posi_y1 -= 0.3;
                            posi_y -= 0.3;
                        }
                    }
                }

                // Crear los elementos de texto en el layout
                foreach (var (texto, x, y, textoSymbol) in texts)
                {
                    CrearTextElement(texto, x, y, textoSymbol);
                    //var coord = new ArcGIS.Core.Geometry.Coordinate2D(x, y);
                    //ElementFactory.Instance.CrearTextElement(layout, textSymbol, coord, texto);
                }

                // Si se requiere, actualizar lodtbReporte o realizar otras acciones.
                // El código original no mostraba lodtbReporte en este método, pero se podría agregar.

            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }


        }

    
}
