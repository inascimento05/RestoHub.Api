using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.UsersModule.Data.Repositories;
using RestoHub.Api.Modules.UsersModule.Domain.Interfaces;

namespace RestoHub.Api.Modules.UsersModule.Infrastructure.Bootstrapers
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
            services.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}
