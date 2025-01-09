using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.Mappings;
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

            services.AddAutoMapper(typeof(CoordinateMappingProfile));
            //services.AddAutoMapper(typeof(CordinatePsad56MappingProfile));
            //services.AddAutoMapper(typeof(CordinateWgs84MappingProfile));

            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<CoordinateMappingProfile>();
            //    //config.AddProfile<CordinatePsad56MappingProfile>();
            //    //config.AddProfile<CordinateWgs84MappingProfile>();
            //}); 
            return services;
        }

    }
}
