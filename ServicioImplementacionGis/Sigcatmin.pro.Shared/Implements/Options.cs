using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sigcatmin.pro.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Shared.Implements
{
    public class Options<T> : IOptions<T>
    {
        public T Value { get; }
        public Options(string filePath, string sectionName)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"El archivo de configuración no se encuentra en la ruta: {filePath}");
            }

            string jsonContent = File.ReadAllText(filePath);

            try
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonContent);
                var section = jsonObject[sectionName];

                if (section == null)
                {
                    throw new ArgumentException($"La sección '{sectionName}' no existe en el archivo de configuración.");
                }

                Value = JsonConvert.DeserializeObject<T>(section.ToString());
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Error al deserializar el archivo de configuración: {ex.Message}", ex);
            }
        }

    }
}
