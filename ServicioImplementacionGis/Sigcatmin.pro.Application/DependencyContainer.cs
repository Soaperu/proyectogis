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

            services.AddScoped<LoginUseCase>();
            services.AddScoped<GetDerechoMineroUseCase>();
            services.AddScoped<CountRowsGISUseCase>();
            services.AddScoped<GetCoordenadasDMUseCase>();
            services.AddScoped<GraficarDerechoMineroUseCase>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
