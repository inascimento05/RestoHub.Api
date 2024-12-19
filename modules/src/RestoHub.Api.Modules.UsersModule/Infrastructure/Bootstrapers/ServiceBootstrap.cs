using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;
using RestoHub.Api.Modules.UsersModule.Domain.Services;

namespace RestoHub.Api.Modules.UsersModule.Infrastructure.Bootstrapers
{
    public static class ServiceBootstrap
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            ConfigureModuleServices(services);

            return services;
        }

        private static void ConfigureModuleServices(IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
        }
    }
}
