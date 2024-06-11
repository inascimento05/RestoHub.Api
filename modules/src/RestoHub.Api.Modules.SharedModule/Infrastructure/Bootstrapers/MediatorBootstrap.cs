using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Modules.Shared.Application.Mediators;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Modules.Shared.Infrastructure.Bootstrapers
{
    [ExcludeFromCodeCoverage]
    public static class MediatorBootstrap
    {
        public static IServiceCollection ConfigureMediators(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IBaseHandler<,>).Assembly);

            return services;
        }
    }
}
