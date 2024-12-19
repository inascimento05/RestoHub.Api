using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.UsersModule.Infrastructure.Bootstrapers;

namespace RestoHub.Api.Modules.UsersModule.Infrastructure
{
    public static class ModuleBootstrap
    {
        public static IServiceCollection ConfigureUsersModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHealthCheck(configuration);

            services.ConfigureContextDb(configuration);
            services.ConfigureClientsServices(configuration);

            services.ConfigureMediators();
            services.ConfigureRepositories();
            services.ConfigureServices();

            return services;
        }

        public static IApplicationBuilder ConfigureUsersModule(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureHealthCheck();

            //app.MigrateDatabaseOnStartup();

            return app;
        }
    }
}
