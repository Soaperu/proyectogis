using Dapper.FluentMap;
using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using Sigcatmin.pro.Persistence.Configuration;
using Sigcatmin.pro.Persistence.Mappings;
using Sigcatmin.pro.Persistence.Repositories;
using Sigcatmin.prop.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Persistence
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IDbManager, DbManager>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEvaluacionGISRepository, EvaluacionGISRepository>();
            services.AddScoped<IGeodatabaseRepository, GeodatabaseRepository>();
            services.AddScoped<IDerechoMineroRepository, DerechoMineroRepository>();



            FluentMapper.Initialize(config =>
            {
                config.AddMap(new DerechoMineroDtoMap());
                config.AddMap(new CoordenadaDtoMap());
            });

            return services;

        }
    }
}
