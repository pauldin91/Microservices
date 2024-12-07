using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Hakuna Mattata", Description = "Hakuna Discount", Amount = 1 },
                new Coupon { Id = 2, ProductName = "Mattata", Description = "Mattata Discount", Amount = 2 }
                );
        }

    }
}
