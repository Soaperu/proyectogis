using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.UsesCases;

namespace Sigcatmin.pro.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<LoginUseCase>();
            return services;
        }

    }
}
