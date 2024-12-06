using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace SigcatminProAddinUI
{
    internal class Program : Module
    {
        private static Program _this = null;
        private static IServiceProvider _serviceProvider;
        //private static IConfiguration _configuration;
        public Program()
        {
            // Configurar el contenedor de dependencias
            var services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())  // Establecer la base para la búsqueda del archivo
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  // Cargar el archivo JSON
                 .Build();

            // Puedes acceder a la configuración desde aquí
            Console.WriteLine(configuration);
            var someSetting = configuration["SomeSettingKey"];
            var con = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Valor de la configuración: {someSetting}");

            // Registrar tus dependencias
            //services.AddSingleton<IUser, User>();

            // Construir el proveedor de servicios
            _serviceProvider = services.BuildServiceProvider();

        }
        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        protected override bool Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static Program Current => _this ??= (Program)FrameworkApplication.FindModule("SigcatminProAddinUI_Module");

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

        #endregion Overrides

    }
}
