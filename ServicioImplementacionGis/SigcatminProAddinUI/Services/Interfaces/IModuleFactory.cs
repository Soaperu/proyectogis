using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Extensions;
using DevExpress.Mvvm.ModuleInjection;
using SigcatminProAddinUI.Models;

namespace SigcatminProAddinUI.Services.Interfaces
{
    public interface IModuleFactory
    {
        List<CategoryModuleModel> Categories { get; set; }

        void RegisterModule(string categoryName, ModuleModel module);

        public Type CreateModule(string categoryName, string moduleName);

    }
}
