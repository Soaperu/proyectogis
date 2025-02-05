using ArcGIS.Core.CIM;
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
    public class AreaRestElementsLayout
    {
        private readonly string? layoutName;
        private Layout? _layout;
        private Dictionary<string, int> dictCriterios = new Dictionary<string, int>();
        // Variables de ejemplo (estas deberían ser proporcionadas externamente)
        private string v_carta_dm = GlobalVariables.CurrentPagesDm;
        private string v_codigo_rest = GlobalVariables.CurrentCodeDm;
        private string v_area_rest = GlobalVariables.CurrentAreaDm;
        private string v_nombre_dm = GlobalVariables.CurrentNameDm;
        private string fecha = DateTime.Now.ToString("dd/MM/yyyy");
        private string v_zona_utm = GlobalVariables.CurrentZoneDm;
        private string v_datum = GlobalVariables.CurrentDatumStrDm;
        private string v_user = GlobalVariables.currentUser;

        /// <summary>
        /// Agrega textos al layout y devuelve la coordenada "Y" final.
        /// </summary>
        public async Task<double> AgregarTextosLayoutAsync(LayoutProjectItem layoutItem, double y)
        {
            double yPre = y;
            return await QueuedTask.Run(() =>
            {
                _layout = layoutItem.GetLayout();
                if (_layout == null)
                    throw new Exception("No se pudo obtener el layout.");

                // Aquí podríamos usar una lista de definiciones:
                var textos = GetTextDefinitionsForARes(y);

                // Agregar cada elemento
                foreach (var item in textos)
                {
                    CIMTextSymbol textSymbol = CrearSimboloTexto(item.color, item.fontSize, "Tahoma");
                    CrearTextElement(item.Texto, item.X, item.Y, textSymbol);
                    yPre = item.Y;
                }
                
                return yPre;
            });

        }
        private (string Texto, double X, double Y, CIMColor color, double fontSize)[] GetTextDefinitionsForARes(double posY)
        {
            // Aquí simplificamos. Ajusta las coordenadas según el codigo anterior.
            double fontSizeBlackS = 3.75;
            double fontSizeBlackM = 6.75;
            double fontSizeBlackH = 9.0;

            CIMColor colorBlack = ColorUtils.ColorFromRGB(0, 0, 0);;
            var textList = new List<(string Texto, double X, double Y, CIMColor color, double fontSize)>()
            {
            (fecha , 24.5 , 2.04 , colorBlack , fontSizeBlackS), // FECHA
            (v_nombre_dm, 20.5, 3.70, colorBlack, fontSizeBlackM), // NOMBRE DEL AR MEMBRETE
            (v_nombre_dm, 21.42, 7.12, colorBlack, fontSizeBlackH), // "NOMBRE DEL AR LEYENDA
            (v_datum, 21.6, 3.07, colorBlack, fontSizeBlackS), // Sitema Datum
            (v_user, 21.5, 2.06, colorBlack, fontSizeBlackS), // Usuario
            (v_zona_utm, 21.6, 2.44, colorBlack, fontSizeBlackS), // Zona UTM
            };

            
            string listaDistritos = GlobalVariables.listadoLimitesAdministrativos.listaDistritos;
            string listaProvincias = GlobalVariables.listadoLimitesAdministrativos.listaProvincias;
            string listaDepartamentos = GlobalVariables.listadoLimitesAdministrativos.listaDepartamentos;

            // contatexto: departamentos
            string labelDepartamentos;
            if (string.IsNullOrEmpty(listaDepartamentos))
            {
                labelDepartamentos = "No se encuentra en area continental";
            }
            else
            {
                if (listaDepartamentos.Length > 60)
                {
                    string posi_x = listaDepartamentos.Substring(0, 60);
                    string posi_x1 = listaDepartamentos.Substring(60);
                    labelDepartamentos = "DEPARTAMENTO(S) : " + posi_x + "\n" + posi_x1;
                    posY -= 0.25;
                }
                else
                {
                    labelDepartamentos = "DEPARTAMENTO(S) : " + listaDepartamentos;
                }
            }
            textList.Add((labelDepartamentos, 19.0, posY, colorBlack, fontSizeBlackM));
            posY -= 0.4;
            
            // contatexto: provincias
            string labelProvincias;
            if (string.IsNullOrEmpty(listaProvincias))
            {
                labelProvincias = "No se encuentra en area continental";
            }
            else
            {
                if (listaProvincias.Length > 60)
                {
                    string posi_x = listaProvincias.Substring(0, 60);
                    string posi_x1 = listaProvincias.Substring(60);
                    labelProvincias = "PROVINCIA(S) : " + posi_x + "\n" + posi_x1;
                    posY -= 0.25;
                }
                else
                {
                    labelProvincias = "PROVINCIA(S) : " + listaProvincias;
                }
            }
            textList.Add((labelProvincias, 19.0, posY, colorBlack, fontSizeBlackM));
            posY -= 0.4;
            
            // contatexto: distritos
            string labelDistritos;
            if (string.IsNullOrEmpty(listaDistritos))
            {
                labelDistritos = "No se encuentra en area continental";
            }
            else
            {
                if (listaDistritos.Length > 60)
                {
                    string posi_x = listaDistritos.Substring(0, 60);
                    string posi_x1 = listaDistritos.Substring(60);
                    labelDistritos = "DISTRITO(S) : " + posi_x + "\n" + posi_x1;
                    posY -= 0.25;
                }
                else
                {
                    labelDistritos = "DISTRITO(S) : " + listaDistritos;
                }
            }
            textList.Add((labelDistritos, 19.0, posY, colorBlack, fontSizeBlackM));
            posY -= 0.4;

            // Retornamos la configuracion de elementos textos
            return textList.ToArray();
        }

        public CIMTextSymbol CrearSimboloTexto(CIMColor color, double tamañoFuente, string fuente, string estilo = "Regular")
        {
            return SymbolFactory.Instance.ConstructTextSymbol(color, tamañoFuente, fuente, estilo);
        }
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
    }
}
