using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework.Contracts;

namespace SigcatminProAddinUI.Models
{
    public class CategoryModuleModel
    {
        public string Name { get; set; }
        public List<ModuleModel> Modules { get; set; }
    }
}
