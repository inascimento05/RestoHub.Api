using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Bootstrap.Botstrapers
{
    [ExcludeFromCodeCoverage]
    public static class ModulesBootstrap
    {
        public static IServiceCollection ConfigureModules(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureRestaurantesModule(configuration);

            return services;
        }

        public static IApplicationBuilder ConfigureModules(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.ConfigureRestaurantesModule(env);

            return app;
        }
    }
}
