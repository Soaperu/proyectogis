using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Geometry;

namespace CommonUtilities.ArcgisProUtils
{
    public class CartaIgnElementsLayoutUtils
    {
        private const int MaxTextLength = 40;
        private const double DefaultFontSize = 7.0;
        private string sele_reporte = "Demarca";
        private string v_nombre_dm = GlobalVariables.CurrentNameDm;
        private string v_codigo = GlobalVariables.CurrentCodeDm;
        private string v_zona_dm = GlobalVariables.CurrentZoneDm;
        private string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        Layout? _layout;
        public async Task AddCartaIgnTextAsync(LayoutProjectItem layoutItem,
                                                string lista_nmhojas_ign,
                                                string lista_cd_cartas,
                                                string lista_dist,
                                                string lista_dist_carta,
                                                string lista_prov,
                                                string lista_prov_carta,
                                                string lista_depa,
                                                string lista_depa_carta,
                                                string caso_consulta1)
            {

                await QueuedTask.Run(() =>
                {

                    _layout = layoutItem.GetLayout();

                    double posiX = 19.1;
                    double posiY1 = 12.03;
                    double decremento = 0.3;

                    // Función para agregar un elemento de texto
                    void AgregarTexto(string texto, double x, double y, double fontSize = 6, bool isBlue = false)
                    {
                        CIMColor color;
                        if (isBlue == true)
                        {
                            color = new CIMRGBColor { R = 0, G = 0, B = 255, Alpha = 100 };
                        }
                        else { color = new CIMRGBColor { R = 0, G = 0, B = 0, Alpha = 100 }; }
                        // Crear el símbolo de texto
                        CIMTextSymbol textSymbol = SymbolFactory.Instance.ConstructTextSymbol(color, fontSize, "Tahoma", "Regular");
                        // Crear el punto de ubicación
                        Coordinate2D coord = new Coordinate2D(x, y);
                        var mapPoint = MapPointBuilderEx.CreateMapPoint(coord);
                        // Crear el elemento de texto
                        var textElement = ElementFactory.Instance.CreateTextGraphicElement(_layout, TextType.PointText, mapPoint, textSymbol, texto);

                        

                    }

                    // Lista de textos a agregar con sus configuraciones
                    var textos = new System.Collections.Generic.List<(int contatexto, string texto, double x, double y, double tamaño, bool esAzul, double drecrementoV)>
                        {
                            // contatexto = 1
                            (1, FormatearTexto("DERECHO MINERO:  ", v_nombre_dm), posiX, posiY1, 8, true, 0.4),

                            // contatexto = 2
                            (2, FormatearTexto("Nombre de la Carta :   ", lista_nmhojas_ign), posiX, posiY1, 6, false, decremento),

                            // contatexto = 3
                            (3, FormatearTexto("Número de la Carta :   ", lista_cd_cartas), posiX, posiY1, 6, false, decremento),

                            // contatexto = 4
                            (4, "Escala                  :    1/100 000", posiX, posiY1, 6, false, decremento),

                            // contatexto = 5
                            (5, $"Zona                    :   {v_zona_dm}", posiX, posiY1, 6, false, 0.6),

                            // contatexto = 6
                            (6, "UBICACION", posiX, posiY1, 9, true, 0.4),

                            // contatexto = 7
                            (7, ObtenerDistritos(caso_consulta1, lista_dist, lista_dist_carta), posiX, posiY1, 6, false,decremento),

                            // contatexto = 8
                            (8, ObtenerProvincias(caso_consulta1, lista_prov, lista_prov_carta), posiX, posiY1, 6, false, decremento),

                            // contatexto = 9
                            (9, ObtenerDepartamentos(caso_consulta1, lista_depa, lista_depa_carta), posiX, posiY1, 6, false, 0.6),

                            // contatexto = 10
                            (10, "OBSERVACIONES", posiX, posiY1, 9, true, 0.4),

                            // contatexto = 11
                            (11, "No existen Observaciones", posiX, posiY1, 5.5, false, decremento),

                            // contatexto = 12
                            (12, fecha, 19.8, 1.97, 5.5, false, 0.0),

                            // contatexto = 13
                            //(13, "", 19.4, 2.9, 5.5, false,decremento) // Asumiendo que el texto se establece en alguna parte
                        };

                    foreach (var item in textos)
                    {
                        // Determinar si el texto debe ser azul
                        bool esAzul = item.contatexto == 1 || item.contatexto == 6 || item.contatexto == 10;

                        // Agregar el texto al layout
                        AgregarTexto(item.texto, item.x, posiY1, item.tamaño, esAzul);
                        posiY1 -= item.drecrementoV;
                    }

                    // Funciones auxiliares

                    string FormatearTexto(string prefix, string contenido)
                    {
                        if (contenido.Length > 60)
                        {
                            string posi_x = contenido.Substring(0, 60);
                            string posi_x1 = contenido.Length > 60 ? contenido.Substring(60) : string.Empty;
                            return $"{prefix}{posi_x}\n{posi_x1}";
                        }
                        else
                        {
                            return $"{prefix}{contenido}";
                        }
                    }

                    string ObtenerDistritos(string tipoConsulta, string listaDist, string listaDistCarta)
                    {
                        string prefix = "DISTRITOS (S)       :   ";
                        string contenido = tipoConsulta == "TIPO CARTA IGN" ? listaDistCarta : listaDist;
                        if (contenido.Length > 60)
                        {
                            string posi_x = contenido.Substring(0, 60);
                            string posi_x1 = contenido.Length > 60 ? contenido.Substring(60) : string.Empty;
                            // Ajustar posiciones Y si se divide el texto
                            //posiY1 -= 0.3;
                            posiY1 -= 0.3;
                            return $"{prefix}{posi_x}\n{posi_x1}";
                        }
                        else
                        {
                            return $"{prefix}{contenido}";
                        }
                    }

                    string ObtenerProvincias(string tipoConsulta, string listaProv, string listaProvCarta)
                    {
                        string prefix = "PROVINCIA (S)       :   ";
                        string contenido = tipoConsulta == "TIPO CARTA IGN" ? listaProvCarta : listaProv;
                        if (contenido.Length > 60)
                        {
                            string posi_x = contenido.Substring(0, 60);
                            string posi_x1 = contenido.Length > 60 ? contenido.Substring(60) : string.Empty;
                            return $"{prefix}{posi_x}\n{posi_x1}";
                        }
                        else
                        {
                            return $"{prefix}{contenido}";
                        }
                    }

                    string ObtenerDepartamentos(string tipoConsulta, string listaDepa, string listaDepaCarta)
                    {
                        string prefix = "DEPARTAMENTO (S) :   ";
                        string contenido = tipoConsulta == "TIPO CARTA IGN" ? listaDepaCarta : listaDepa;
                        if (contenido.Length > 60)
                        {
                            string posi_x = contenido.Substring(0, 60);
                            string posi_x1 = contenido.Length > 60 ? contenido.Substring(60) : string.Empty;
                            return $"{prefix}{posi_x}\n{posi_x1}";
                        }
                        else
                        {
                            return $"{prefix}{contenido}";
                        }
                    }
                });
            }

        }
}
