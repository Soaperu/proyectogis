using System;
using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Services.Interfaces;
using SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs;
using SigcatminProAddinUI.Views.WPF.Views.Layout;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.Buttons
{
    internal class ConfirmModuleButton : Button
    {
        private readonly IModuleFactory _moduleFactory;
        public ConfirmModuleButton()
        {
            _moduleFactory = Program.GetService<IModuleFactory>();
        }
        protected override void OnClick()
        {
            string categorName = CategoryComboBox.Intance.SelectedItem.ToString();
            string ModuleName = ModuleComboBox.Intance.SelectedItem.ToString();

            var moduleType = _moduleFactory.CreateModule(categorName, ModuleName);

            MainView mainView = new MainView();
            mainView.frameContainer.Navigate(Activator.CreateInstance(moduleType));
            mainView.Show();


        }
    }
}
