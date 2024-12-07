using BuildingBlocks.Cqrs;
using Discount.Grpc;

namespace Basket.Api.Basket.Store
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string Username);

    public class StoreCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreCommandValidator()
        {
            RuleFor(s => s.Cart).NotNull().WithMessage("Cart empty");
            RuleFor(s => s.Cart.Username).NotEmpty().WithMessage("Username empty");
        }
    }

    public class StoreBasketHandler(ICachedBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscount(request.Cart, cancellationToken);
            await basketRepository.StoreBasket(request.Cart,cancellationToken);

            return new StoreBasketResult(request.Cart.Username);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var discount = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= discount.Amount;
            }
        }
    }
}