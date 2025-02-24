using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext discountDbContext) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountDbContext
                .Coupons
                .FirstOrDefaultAsync(s => s.ProductName == request.ProductName);
            
            if (coupon is null)
                coupon = new Coupon { ProductName="No discount",Amount=0,Description="Stingy"};

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid argument"));

            discountDbContext.Coupons .Add(coupon);
            await discountDbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument"));

            discountDbContext.Coupons.Update(coupon);
            await discountDbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountDbContext
                .Coupons
                .FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Discount not found"));

            discountDbContext.Coupons.Remove(coupon);
            await discountDbContext.SaveChangesAsync();

            return new DeleteDiscountResponse {Success=true };
        }
    }
}
