﻿using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Shared.Implements;
using Sigcatmin.pro.Shared.DomainServices;
using Sigcatmin.prop.Domain.Interfaces.Services;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Shared
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddShared(this IServiceCollection services, string pathSettings)
        {

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            //.WriteTo.Console()
            .WriteTo.RollingFile(@"c:\temp\Log-{Date}.txt")
            .CreateLogger();

            services.AddSingleton<IOptions<JWTSettings>>(x => new Options<JWTSettings>(pathSettings, nameof(JWTSettings)));
            services.AddSingleton<IOptions<DdConnectionSettings>>(x => new Options<DdConnectionSettings>(pathSettings, "DdConnection"));

            services.AddSingleton<IAuthService, AuthService>();

            services.AddTransient<ILoggerService, LoggerService>();

            services.AddTransient<IUserSessionService, UserSessionService>();
            services.AddTransient<IMapService, MapService>();
            

            return services;
        }
    }
}
