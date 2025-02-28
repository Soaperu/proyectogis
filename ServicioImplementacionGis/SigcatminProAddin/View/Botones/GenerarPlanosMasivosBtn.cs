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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using SigcatminProAddin.View.Botones.UI;

namespace SigcatminProAddin.View.Botones
{
    internal class GenerarPlanosMasivosBtn : Button
    {
        GenerarPlanosMasivosWpf generarPlanosMasivosWpf;
        protected override void OnClick()
        {
            generarPlanosMasivosWpf = new GenerarPlanosMasivosWpf();
            generarPlanosMasivosWpf.Closed += (s, e) => generarPlanosMasivosWpf = null;
            generarPlanosMasivosWpf.Show();
        }
    }
}
