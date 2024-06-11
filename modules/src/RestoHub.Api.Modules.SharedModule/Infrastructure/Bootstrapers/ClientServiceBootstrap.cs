using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Modules.Shared.Infrastructure.Bootstrapers
{
    [ExcludeFromCodeCoverage]
    public static class ClientServiceBootstrap
    {
        public static IServiceCollection ConfigureClientsServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Example of Refit implementation.
            //services.AddRefitClient<ISampleExternalApiService>().ConfigureHttpClient(c =>
            //{
            //    c.BaseAddress = new Uri(configuration.GetSection("Shared:ApiServices:Sample:Url").Value);
            //    c.Timeout = TimeSpan.FromSeconds(15);
            //});

            return services;
        }
    }
}
