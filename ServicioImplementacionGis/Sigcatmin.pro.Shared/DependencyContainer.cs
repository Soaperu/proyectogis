using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Shared.Interfaces;
using Sigcatmin.pro.Shared.Services;
using Sigcatmin.prop.Domain.Interfaces.Services;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Shared
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddShared(this IServiceCollection services, string pathSettings)
        {
            services.AddSingleton<IOptions<JWTSettings>>(x => new Options<JWTSettings>(pathSettings, nameof(JWTSettings)));
            services.AddSingleton<IOptions<DdConnectionSettings>>(x => new Options<DdConnectionSettings>(pathSettings, "DdConnection"));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserSessionService, UserSessionService>();

            return services;
        }
    }
}
