using ArcGIS.Desktop.Framework.Contracts;
using SigcatminProAddinUI.Views.WPF.Views.Layout;
using SigcatminProAddinUI.Views.WPF.Views.Modulos;

namespace SigcatminProAddinUI.Views.ArgisPro.Views.Buttons
{
    internal class GraficarExcelButton : Button
    {
        protected override void OnClick()
        {
            MainView mainView = new MainView();
            mainView.frameContainer.Navigate(new EvaluacionDMView());
            mainView.Show();  
        }
    }
}
