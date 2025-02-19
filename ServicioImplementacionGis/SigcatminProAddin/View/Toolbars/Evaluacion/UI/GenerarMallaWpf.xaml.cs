using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using CommonUtilities.ArcgisProUtils;
using CommonUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para GenerarMallaWpf.xaml
    /// </summary>
    public partial class GenerarMallaWpf : Window
    {
        public GenerarMallaWpf()
        {
            InitializeComponent();
        }


        private void CbxCuadriculaHa_Loaded(object sender, RoutedEventArgs e)
        {
            CbxCuadriculaHa.Items.Add("100 Ha.");
            CbxCuadriculaHa.Items.Add("10 Ha.");
            CbxCuadriculaHa.SelectedIndex = 0;
        }

        private async void btnGraficar_Click(object sender, RoutedEventArgs e)
        {

            if (GlobalVariables.CurrentTipoEx == "PE")
            {
                
                var vertices = ObtenerVertices100(GlobalVariables.currentExtentDM);

                if (CbxCuadriculaHa.SelectedIndex == 1)
                {
                    vertices = ObtenerVertices10(GlobalVariables.currentExtentDM);
                    Graficarcuadriculas10(vertices);
                }
                else
                {
                    Graficarcuadriculas100(vertices);
                }


            }
            else
            {
                MessageBox.Show("No se puede generar malla de cuadriculas para este tipo de expediente", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private DataTable ObtenerVertices100(ExtentModel extent)
        {
            DataTable lodtbDatos = new DataTable();
            int k = 0;
            int step = 1000;
            int numVer = 1;
            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"));

            for (int i = (int)extent.xmin; i <= (int)extent.xmax - 1; i += step)
            {


                for (int j = (int)extent.ymin; j <= (int)extent.ymax - 1; j += step)
                {
                    k = k + 1;

                    // Primera fila
                    DataRow dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer;
                    lodtbDatos.Rows.Add(dRow);

                    // Segunda fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer + 1;
                    lodtbDatos.Rows.Add(dRow);

                    // Tercera fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step;
                    dRow["CD_CORNOR"] = j + step;
                    dRow["CD_NUMVER"] = numVer + 2;
                    lodtbDatos.Rows.Add(dRow);

                    // Cuarta fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j + step;
                    dRow["CD_NUMVER"] = numVer + 3;
                    lodtbDatos.Rows.Add(dRow);
                }
            }
            return lodtbDatos;
        }

        private DataTable ObtenerVertices10(ExtentModel extent)
        {
            DataTable lodtbDatos = new DataTable();
            int k = 0;
            int step200 = 200;
            int step500 = 500;
            int numVer = 1;
            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"));

            for (int i = (int)extent.xmin; i <= (int)extent.xmax - 1; i += step200)
            {


                for (int j = (int)extent.ymin; j <= (int)extent.ymax - 1; j += step500)
                {
                    k = k + 1;

                    // Primera fila
                    DataRow dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer;
                    lodtbDatos.Rows.Add(dRow);

                    // Segunda fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step200;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer + 1;
                    lodtbDatos.Rows.Add(dRow);

                    // Tercera fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step200;
                    dRow["CD_CORNOR"] = j + step500;
                    dRow["CD_NUMVER"] = numVer + 2;
                    lodtbDatos.Rows.Add(dRow);

                    // Cuarta fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j + step500;
                    dRow["CD_NUMVER"] = numVer + 3;
                    lodtbDatos.Rows.Add(dRow);
                }
            }
            return lodtbDatos;
        }
        private async void Graficarcuadriculas10(DataTable lodtbDatos)
        {
            

            await QueuedTask.Run(async() =>
            {
                // Crear o verificar la existencia de la capa de destino (líneas)
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                FeatureLayer mallasLayer = uTMGridGenerator.GetOrCreateLayerwithNoRows("Recta", GlobalVariables.CurrentZoneDm);
                if (mallasLayer == null) throw new Exception("No se pudo crear o acceder a la capa.");

                // Recorre la tabla
                for (int i = 0; i < lodtbDatos.Rows.Count; i+=4)
                { 
                    string codigo = lodtbDatos.Rows[i]["CG_CODIGO"].ToString();
                    string idpolygon = ((i / 4) + 1).ToString();

                    var  polygon = PolygonBuilder.CreatePolygon(new[]
                        {
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+1]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+1]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+2]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+2]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+3]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+3]["CD_CORNOR"].ToString())),
                    });

                    AddFeatureToLayer(mallasLayer, polygon, codigo, "PE", idpolygon , "10");

                }
                string newNameMalla = "Cuadriculas_10HA";
                mallasLayer.SetName(newNameMalla);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(mallasLayer);
            });
        }

        private async void Graficarcuadriculas100(DataTable lodtbDatos)
        {
            string[] letrasMayusculas = new string[]
                {
                    "A","B","C","D","E","F","G",
                    "H","I","J","K","L","M","N",
                    "O","P","Q","R","S","T","U",
                    "V","W","X","Y","Z"
                };


            await QueuedTask.Run(async() =>
            {
                // Crear o verificar la existencia de la capa de destino (líneas)
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                FeatureLayer mallasLayer = uTMGridGenerator.GetOrCreateLayerwithNoRows("Recta", GlobalVariables.CurrentZoneDm);
                if (mallasLayer == null) throw new Exception("No se pudo crear o acceder a la capa.");

                // Recorre la tabla
                for (int i = 0; i < lodtbDatos.Rows.Count; i += 4)
                {
                    string codigo = lodtbDatos.Rows[i]["CG_CODIGO"].ToString();
                    int idpolygon = ((i / 4) + 1);

                    var polygon = PolygonBuilder.CreatePolygon(new[]
                        {
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+1]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+1]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+2]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+2]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+3]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+3]["CD_CORNOR"].ToString())),
                    });

                    AddFeatureToLayer(mallasLayer, polygon, letrasMayusculas[idpolygon - 1], "PE", idpolygon.ToString(), "100");
                }
                string newNameMalla = "Cuadriculas_100HA";
                mallasLayer.SetName(newNameMalla);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(mallasLayer);
            });

        }

        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Comprueba que el botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }

        private void AddFeatureToLayer(FeatureLayer layer, ArcGIS.Core.Geometry.Polygon line, string codigo, string tipo, string idpol, string area)
        {
            using (var featureClass = layer.GetFeatureClass())
            using (var rowBuffer = featureClass.CreateRowBuffer())
            using (var featureCursor = featureClass.CreateInsertCursor())
            {
                rowBuffer["SHAPE"] = line;
                rowBuffer["CODIGOU"] = codigo;
                rowBuffer["TIPO"] = tipo;
                rowBuffer["POLIGONO"] = idpol;
                rowBuffer["AREA"] = area;
                featureCursor.Insert(rowBuffer);
            }
        }
    }
}
