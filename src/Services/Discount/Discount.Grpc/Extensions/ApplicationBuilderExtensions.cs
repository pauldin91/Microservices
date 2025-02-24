using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UseApplicationBuilderExtensions(this IApplicationBuilder builder) {

            using var scope = builder.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            await dbContext.Database.MigrateAsync();

            return builder;
        }
    }
}
