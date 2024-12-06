using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Shared.Interfaces;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Shared
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddShared(this IServiceCollection services, string pathSettings)
        {
            services.AddSingleton<IOptions<JWTSettings>>(x => new Options<JWTSettings>(pathSettings, nameof(JWTSettings)));

            return services;
        }
    }
}
