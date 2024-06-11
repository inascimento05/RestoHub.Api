using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;

namespace RestoHub.Api.Modules.RestaurantesModule.Infrastructure.Bootstrapers
{
    public static class HealthCheckBootstrap
    {
        public static IServiceCollection ConfigureHealthCheck(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString,
                    name: "RestaurantesModule"
                );

            return services;
        }

        public static IApplicationBuilder ConfigureHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/Restaurantes/ping");

            app.UseHealthChecks(
                "/Restaurantes/pong",
                new HealthCheckOptions() { ResponseWriter = WritePongResultAsync });

            return app;
        }

        private static async Task WritePongResultAsync(
            HttpContext context,
            HealthReport report)
        {
            var pongResult = report.Entries
                  .OrderBy(x => x.Value.Status)
                  .Select(x => new
                  {
                      Resource = x.Key,
                      Status = x.Value.Status.ToString(),
                      x.Value.Tags
                  });

            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    pongResult,
                    options: new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }
                )
            );
        }
    }
}
