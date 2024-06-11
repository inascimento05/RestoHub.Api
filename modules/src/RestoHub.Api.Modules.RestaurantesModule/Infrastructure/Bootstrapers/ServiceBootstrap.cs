using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Services;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers
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
            services.AddTransient<IRestaurantesService, RestaurantesService>();
        }
    }
}
