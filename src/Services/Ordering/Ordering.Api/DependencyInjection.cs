using Carter;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiExtensions(this IServiceCollection services)
        {
            services.AddCarter();
            return services;
        }

        public static WebApplication UseApiExtensions(this WebApplication webApplication)
        {
            webApplication.MapCarter();
            return webApplication;
        }
    }
}