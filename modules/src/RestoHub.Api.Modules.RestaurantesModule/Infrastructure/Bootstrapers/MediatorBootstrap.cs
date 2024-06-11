using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Create;
using RestoHub.Api.Modules.RestaurantesModule.Application.Mediators.RestaurantesOperations.Dtos;
using RestoHub.Api.Modules.Shared.Application.Notifications;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers
{
    public static class MediatorBootstrap
    {
        public static IServiceCollection ConfigureMediators(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateRestauranteRequest, DataResult<RestauranteDto>>, CreateRestauranteHandler>();

            return services;
        }
    }
}
