﻿using System;
using System.IO;
using System.Linq;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.IoC;
using SigcatminProAddinUI.Handlers;
using SigcatminProAddinUI.Resourecs.Helpers;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI
{
    internal class Program : Module
    {
        private static Program _this = null;
        public static Program Current => _this ??= (Program)FrameworkApplication.FindModule("SigcatminProAddinUI_Module");

        private static IServiceProvider _serviceProvider;
        private readonly IUIStateService _UIStateService;
        private readonly ErrorHandler _errorHandler;
        //private static IConfiguration _configuration;
        public Program()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.OrderBy(a => a.FullName))
            {
                Console.WriteLine($"Cargado: {assembly.FullName} | Ubicación: {assembly.Location}");
            }

            //var assembly = System.Reflection.Assembly.Load("Microsoft.Extensions.Options");

            var services = new ServiceCollection();

            string pathAsembly = PathAssembly.GetExecutingAssembly();
            // Construir la ruta completa al archivo appsettings.json
            string pathSettings = Path.Combine(pathAsembly, "appsettings.json");

            if (!File.Exists(pathSettings))
            {
                throw new FileNotFoundException($"No se encontró el archivo de configuración en la ruta: {pathSettings}");
            }

            services.AddIoC(pathSettings);
            services.AddPresentation();
            // Construir el proveedor de servicios
            _serviceProvider = services.BuildServiceProvider();

            _UIStateService = GetService<IUIStateService>();
            _errorHandler = GetService<ErrorHandler>();

        }
        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        protected override bool Initialize()
        {
            _UIStateService.InitializeStates();
            _errorHandler.RegisterGlobalException();

            return base.Initialize();
        }

        protected override void Uninitialize()
        {
            _errorHandler.ClearGlobalException();
            base.Uninitialize();
        }

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>


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
