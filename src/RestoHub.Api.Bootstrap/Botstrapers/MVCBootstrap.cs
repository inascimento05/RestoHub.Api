using Microsoft.Extensions.DependencyInjection;
using RestoHub.Api.Bootstrap.Filters;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Bootstrap.Botstrapers
{
    [ExcludeFromCodeCoverage]
    public static class MvcBootstrap
    {
        public static IServiceCollection ConfigureMVC(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.AddService<DeserializeHeadersFilter>();
                options.Filters.AddService<LogActionFilter>();
            });

            services.AddScoped<LogActionFilter>();

            return services;
        }
    }
}
