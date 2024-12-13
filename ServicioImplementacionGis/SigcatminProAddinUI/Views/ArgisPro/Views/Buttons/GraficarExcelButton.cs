using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.KnowledgeGraph;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using SigcatminProAddinUI.Views.WPF.Views.Layout;
using SigcatminProAddinUI.Views.WPF.Views.Modulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.Buttons
{
    internal class GraficarExcelButton : Button
    {
        protected override void OnClick()
        {
            MainView mainView = new MainView();
            mainView.frameContainer.Navigate(new CartaNacionalView());
            mainView.Show();  
        }
    }
}
