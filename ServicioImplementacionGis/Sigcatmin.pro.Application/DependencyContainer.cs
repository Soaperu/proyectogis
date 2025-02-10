using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.Mappers;
using Sigcatmin.pro.Application.UsesCases;

namespace Sigcatmin.pro.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.OrderBy(a => a.FullName))
            {
                Console.WriteLine($"Cargado: {assembly.FullName} | Ubicación: {assembly.Location}");
            }

            services.AddScoped<LoginUseCase>();
            services.AddScoped<GetDerechoMineroUseCase>();
            services.AddScoped<CountRowsGISUseCase>();
            services.AddScoped<GetCoordenadasDMUseCase>();
            services.AddScoped<GraficarDerechoMineroUseCase>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
