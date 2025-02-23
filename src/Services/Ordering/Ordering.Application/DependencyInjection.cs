using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BuildingBlocks.Behaviors;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }
    }
}