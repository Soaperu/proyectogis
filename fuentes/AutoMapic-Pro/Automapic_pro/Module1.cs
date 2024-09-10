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
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Automapic_pro.GlobalVariables;

namespace Automapic_pro
{

    internal class Module1 : Module
    {
        private static Module1 _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("Automapic_prototipo_Module");

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

        protected override bool Initialize()
        {
            Task.Run(() => InstalarLibreriasPython());
            return base.Initialize();
        }

        private void InstalarLibreriasPython()
        {
            //MessageBox.Show(Process.GetCurrentProcess().MainModule.FileName);
            // Ruta al intérprete de Python de ArcGIS Pro
            //string pythonExePath = @"C:\Program Files\ArcGIS\Pro\bin\Python\envs\arcgispro-py3\python3.exe";
            // Ruta al script de Python que instala las librerías
            string scriptPath = System.IO.Path.Combine(currentPath, scriptLibrerisPath);

            ProcessStartInfo startInfo = new ProcessStartInfo(pythonExePath, scriptPath)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    // Aquí podrías hacer algo con la salida, si es necesario
                    Debug.WriteLine(output);
                }
            }
            catch (Exception ex)
            {
                // Maneja aquí cualquier excepción
                Debug.WriteLine("Error al ejecutar el script de Python: " + ex.Message);
            }
        }
    }
}
