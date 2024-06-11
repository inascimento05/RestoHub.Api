using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Bootstrap.Filters;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Bootstrap.Botstrapers
{
    [ExcludeFromCodeCoverage]
    public static class FiltersBootstrap
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<DeserializeHeadersFilter>();

            return services;
        }
    }
}
