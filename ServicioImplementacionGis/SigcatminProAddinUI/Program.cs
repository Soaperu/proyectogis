﻿using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SigcatminProAddinUI.Helpers;
using System;
using System.IO;
using Sigcatmin.pro.IoC;

namespace SigcatminProAddinUI
{
    internal class Program : Module
    {
        private static Program _this = null;
        private static IServiceProvider _serviceProvider;
        //private static IConfiguration _configuration;
        public Program()
        {
            var services = new ServiceCollection();

            string pathAsembly = PathAssembly.GetExecutingAssembly();
            // Construir la ruta completa al archivo appsettings.json
            string pathSettings = Path.Combine(pathAsembly, "appsettings.json");

            if (!File.Exists(pathSettings))
            {
                throw new FileNotFoundException($"No se encontró el archivo de configuración en la ruta: {pathSettings}");
            }

            services.AddIoC(filpathSettingsePath);
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
