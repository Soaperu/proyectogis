﻿using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.KnowledgeGraph;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using SigcatminProAddin.Models.Constants;
using SigcatminProAddin.View.Botones;
using CommonUtilities;
using SigcatminProAddin.View.Modulos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SigcatminProAddin
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("SigcatminProAddin_Module");

        protected override bool Initialize()
        {
            if (LoginButton.IsActiveSessionStatic())
            {
                // Activar el estado si la sesión es válida
                StatesUtil.ActivateState(UIState.IsLogged);
            }
            return base.Initialize();
        }

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

    public class StatesUtil
    {
        #region Toggle State
        /// <summary>
        /// Activate or Deactivate the specified state. State is identified via
        /// its name. Listen for state changes via the DAML <b>condition</b> attribute
        /// </summary>
        /// <param name="stateID"></param>
        public static void ToggleState(string stateID)
        {
            if (FrameworkApplication.State.Contains(stateID))
            {
                FrameworkApplication.State.Deactivate(stateID);
            }
            else
            {
                FrameworkApplication.State.Activate(stateID);
            }
        }

        public static void ActivateState(string stateID)
        {
            FrameworkApplication.State.Activate(stateID);
        }

        public static void DeactivateState(string stateID)
        {
            FrameworkApplication.State.Deactivate(stateID);
        }

        #endregion Toggle State
    }
}
