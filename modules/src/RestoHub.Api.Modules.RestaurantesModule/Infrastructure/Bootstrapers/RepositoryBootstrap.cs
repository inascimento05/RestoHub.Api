using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Data.Repositories;
using RestoHub.Api.Modules.RestaurantesModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers
{
    public static class RepositoryBootstrap
    {
        public static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            ConfigureModuleRepositories(services);

            return services;
        }

        private static void ConfigureModuleRepositories(IServiceCollection services)
        {
            services.AddTransient<IRestaurantesRepository, RestaurantesRepository>();
        }
    }
}
