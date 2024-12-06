using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application;
using Sigcatmin.pro.Shared;

namespace Sigcatmin.pro.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, string pathSettings)
        {

            services.AddApplication()
            .AddShared(pathSettings);
            return services;
        }
    }
}
