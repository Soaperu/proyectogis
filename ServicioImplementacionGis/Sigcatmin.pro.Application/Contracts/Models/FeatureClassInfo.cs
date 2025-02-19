using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Contracts.Models
{
    public class FeatureClassInfo
    {
        public string FeatureClassName { get; set; }
        public string LayerName { get; set; }
        public string VariableName { get; set; }
        public Func<string, string>? FeatureClassNameGenerator { get; set; } // Función para generar el nombre dinámicamente
        public Func<string, string>? LayerNameGenerator { get; set; }
    }
}
