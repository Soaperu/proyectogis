﻿using ArcGIS.Core.CIM;
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
using ArcGIS.Desktop.KnowledgeGraph;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using CommonUtilities.LoginUtil;
using DatabaseConnector;
using SigcatminProAddin.Models.Constants;
using SigcatminProAddin.View.Contenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddin.View.Botones
{
    internal class LogoutButton : Button
    {
        protected override void OnClick()
        {
            var contenedor = MainContainer.Instance;
            contenedor.ClearModules();
            SessionManager.EndSession();
            StatesUtil.DeactivateState(UIState.IsLogged);

            AppConfig.userName = "";
            AppConfig.password = "";

        }
    }
}
