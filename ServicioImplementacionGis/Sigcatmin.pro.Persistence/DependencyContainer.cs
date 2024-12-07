using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Persistence.Configuration;
using Sigcatmin.pro.Persistence.Repositories;
using Sigcatmin.prop.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Persistence
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, DbContext>();
            //services.AddScoped<IDbManager, DbManager>();
            services.AddScoped<IDbManagerFactory, DbManagerFactory>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            
            return services;

        }
    }
}
