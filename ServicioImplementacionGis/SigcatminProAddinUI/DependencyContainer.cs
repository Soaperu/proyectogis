using Microsoft.Extensions.DependencyInjection;
using SigcatminProAddinUI.Handlers;
using SigcatminProAddinUI.Services.Implements;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<IUIStateService, UIStateService>();
            services.AddSingleton<IModuleFactory, ModuleFactory>();
            services.AddScoped<INotifyComboBoxService, NotifyComboBoxService>();
            services.AddSingleton<ErrorHandler>();

            return services;
        }
    }
}
