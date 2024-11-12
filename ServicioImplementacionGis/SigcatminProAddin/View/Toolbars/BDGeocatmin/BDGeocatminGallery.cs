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
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin
{
    internal class BDGeocatminGallery : Gallery
    {
        private bool _isInitialized;

        //protected override void OnDropDownOpened()
        //{
        //    Initialize();
        //    this.AlwaysFireOnClick = true;
        //}
        public BDGeocatminGallery() 
        {
            Initialize();
            this.AlwaysFireOnClick = true;
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            //Add 6 items to the gallery
            //for (int i = 0; i < 6; i++)
            //{
            //    string name = string.Format("Item {0}", i);
            //    Add(new GalleryItem(name, this.LargeImage != null ? ((ImageSource)this.LargeImage).Clone() : null, name));
            //}
            
            foreach (var component in Categories.GetComponentElements("BDGeocatminTools"))
            {
                try
                {
                    var content = component.GetContent();
                    //This will throw an exception if the attribute is not there
                    var version = content.Attribute("version").Value;
                    //This flavor (off component) returns empty string
                    //if the attribute is not there
                    var group = component.ReadAttribute("group") ?? "";

                    //check we get a plugin
                    var plugin = FrameworkApplication.GetPlugInWrapper(component.ID);
                    if (plugin != null)
                    {
                        Add(new CustomGalleryItem(component.ID, group, plugin));
                    }
                }
                catch (Exception e)
                {
                    string x = e.Message;
                }
            }
        }

        protected override void OnClick(object item)
        {
            //TODO - insert your code to manipulate the clicked gallery item here
            //System.Diagnostics.Debug.WriteLine("Remove this line after adding your custom behavior.");
            //base.OnClick(item);
            var customItem = item as CustomGalleryItem;
            customItem.Execute();
        }
    }
}
