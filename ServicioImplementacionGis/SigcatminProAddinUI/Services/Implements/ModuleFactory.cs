using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ArcGIS.Desktop.Internal.KnowledgeGraph.FFP;
using SigcatminProAddinUI.Models;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Services.Implements
{
    public class ModuleFactory : IModuleFactory
    {
        public List<CategoryModuleModel> Categories { get; set; } = new();

        public Type CreateModule(string categoryName, string moduleName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            if (string.IsNullOrEmpty(moduleName))
            {
                throw new ArgumentNullException(nameof(moduleName));
            }

            var category = Categories.FirstOrDefault(x => x.Name == categoryName);
            if (category is null)
            {
                throw new InvalidOperationException("no se encontro ninguna categoria con este nombre");
            }

            var module = category.Modules.FirstOrDefault(x => x.Name == moduleName);

            return module.Module;
        }

        public void RegisterModule(string categoryName, ModuleModel module = null)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            var category = Categories.FirstOrDefault(x => x.Name == categoryName);

            if (category != null)
            {
                category.Modules.Add(module);
                return;
            
            }
            var modules = new List<ModuleModel>() { module };

            Categories.Add(new CategoryModuleModel {
                Name = categoryName,
                Modules =  modules 
            });
        }
    }
}
