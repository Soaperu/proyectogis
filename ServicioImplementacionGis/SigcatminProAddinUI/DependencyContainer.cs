using Microsoft.Extensions.DependencyInjection;
using SigcatminProAddinUI.Services.Implements;
using SigcatminProAddinUI.Services.Interfaces;
using SigcatminProAddinUI.Views.ArgisPro.Views.ComboBoxs;

namespace SigcatminProAddinUI
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {

            services.AddScoped<IUIStateService, UIStateService>();
            services.AddSingleton<IModuleFactory, ModuleFactory>();
            services.AddScoped<INotifyComboBoxService, NotifyComboBoxService>();


            return services;
        }
    }
}
