using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure
{
    public static class ModuleBootstrap
    {
        public static IServiceCollection ConfigureRestaurantesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHealthCheck(configuration);

            services.ConfigureContextDb(configuration);
            services.ConfigureClientsServices(configuration);

            services.ConfigureMediators();
            services.ConfigureRepositories();
            services.ConfigureServices();

            return services;
        }

        public static IApplicationBuilder ConfigureRestaurantesModule(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureHealthCheck();

            app.MigrateDatabaseOnStartup();

            return app;
        }
    }
}
