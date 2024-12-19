namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiExtensions(this IServiceCollection services)
        {
            return services;
        }

        public static WebApplication UseApiExtensions(this WebApplication webApplication)
        {
            return webApplication;
        }
    }
}