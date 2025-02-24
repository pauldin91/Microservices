using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.Infrastructure.Data;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString(nameof(OrderingDbContext))!);

            return services;
        }

        public static WebApplication UseApiExtensions(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}