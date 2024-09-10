using ArcGIS.Desktop.Core.Geoprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using static Automapic_pro.toolBox;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using System.Drawing.Text;

namespace Automapic_pro.datos_campo_drme
{
    /// <summary>
    /// Lógica de interacción para datos_campos_drme.xaml
    /// </summary>
    public partial class datos_campos_drme : Page
    {
        public datos_campos_drme()
        {
            InitializeComponent();
            _ = PopulateTreeView();
        }

        private async Task PopulateTreeView()
        {
            try { 
                var Params = Geoprocessing.MakeValueArray("0,10");
                var response = await ExecuteGPAsync(_toolBoxPath_datos_campo, _tool_gettreeLayers, Params);
                var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
                var responseString = responseJson["response"].ToString();
                // Convertir el JArray a una lista de objetos LayerItem
                var items = JsonConvert.DeserializeObject<List<LayerItem>>(responseString);
                // Ordenamos de forma ascendente
                var orderedItems = items.OrderBy(item => int.Parse(item.CODIGO)).ToList();
                //var orderedItems = items.OrderBy(item => (item.ADICIONAL3)).ToList();

                var itemsTree = new ObservableCollection<NodeItem>();

                // Filtrar la lista basada en los criterios proporcionados
                var ParentsItems = items
                    .Where(item => item.ADICIONAL3 is null)
                    .ToList();

                foreach (var item in ParentsItems)
                {
                    var parentItem = new NodeItem { Title = item.DESCRIPCION, Children = new List<NodeItem>() };
                    var ChildrenItems = items
                        .Where(itemchild => itemchild.ADICIONAL3 == item.CODIGO)
                        .ToList();
                    foreach (var childItem in ChildrenItems)
                    {
                        parentItem.Children.Add(new NodeItem { Title = childItem.DESCRIPCION,
                                                               IsChild=true,
                                                               Tag= childItem.ADICIONAL,
                                                               fileLyrxName= childItem.ADICIONAL2,
                        });
                    }
                    itemsTree.Add(parentItem);
                }


                // Nos Aseguramos de ejecutar la actualización del UI en el hilo de la UI
                Application.Current.Dispatcher.Invoke(() =>
                {
                    tvc_campodrme_layers.ItemsSource = itemsTree;
                });

            }
            catch (Exception ex) {; }
        }
    }

    public class ToolBoxProcessing
    {
        public async Task AddFeatureToMap(string pathFeatureClass, string fileLyrxName, string lyrName)
        {
            var Params = Geoprocessing.MakeValueArray(pathFeatureClass, fileLyrxName, lyrName);
            var response = await ExecuteGPAsync(_toolBoxPath_datos_campo, _tool_addFeatureToMap, Params);
            //var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
        }

        public async Task removeFeatureToMap(string lyrName)
        {
            var Params = Geoprocessing.MakeValueArray(lyrName);
            var response = await ExecuteGPAsync(_toolBoxPath_datos_campo, _tool_removeFeatureOfTOC, Params);
            //var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
        }
    }

    public class NodeItem : INotifyPropertyChanged
    {
        //private Action GeoProcessingMethod;
        ToolBoxProcessing instance = new ToolBoxProcessing();
        public string Title { get; set; }
        public string Tag { get; set; }
        public string fileLyrxName { get; set; }
        public bool IsChild { get; set; }
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked == value) return;
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));

                // Cambia la ruta de la imagen cuando se marca/desmarca el CheckBox
                ImagePath = _isChecked ? "/Automapic_pro;component/Images/layer_on.png" : "/Automapic_pro;component/Images/layer_off.png";
                //MessageBox.Show($"Seleccionado: {Tag}");
                _ = IsChecked ? instance.AddFeatureToMap(Tag,fileLyrxName,Title): instance.removeFeatureToMap(Title);
            }
        }

        private string _imagePath = "/Automapic_pro;component/Images/layer_off.png"; // ruta de la imagen predeterminada
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath == value) return;
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
        public List<NodeItem> Children { get; set; }

        public NodeItem()
        {
            Children = new List<NodeItem>();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Clase que recibe la estructura de datos de la tabla ged_m_lista
    public class LayerItem
    {
        public string TIPO { get; set; } // valor: AUTOMAPICPRO_LAYERS
        public string CODIGO { get; set; } // ID de la capa y grupos de capa
        public string DESCRIPCION { get; set; } // Nombre de la capa 
        public string INDICADOR_ACTIVO { get; set; } // Indicador de vigencia
        public int ORDEN { get; set; } // Categoria de la capa (agrupa una serie de capas)
        public string DETALLE { get; set; } // Nombre de la capa en el servidor
        public string ADICIONAL { get; set; } // Ruta de la capa en el servidor 
        public string ADICIONAL2 { get; set; } // Nombre del archivo lyrx
        public string ADICIONAL3 { get; set; } // Id del grupo de capa 
        public string ADICIONALVV { get; set; } // no asignado
        public string ADICIONAL4 { get; set; } // no asignado
    }

}
