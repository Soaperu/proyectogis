using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var categories = Categories.Where(x => x.Name == categoryName);
            if (categories.Any())
            {
                throw new InvalidOperationException("no se encontro ninguna categoria con este nombre");
            }

            var module = Categories.SelectMany(x => x.Modules)
                .FirstOrDefault(x => x.Name == moduleName);

            return (Type)Activator.CreateInstance(module.Module);
        }

        public void RegisterModule(string categoryName, ModuleModel module)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            if (Categories.Any(x => x.Name == categoryName))
            {
               var categoria = Categories.FirstOrDefault(x => x.Name == categoryName);

                categoria.Modules.Add(module);
            
            }

            var modules = new List<ModuleModel>() { module };

            Categories.Add(new CategoryModuleModel {
                Name = categoryName,
                Modules =  modules 
            });
        }
    }
}
