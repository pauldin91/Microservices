
using BuildingBlocks.Cqrs;



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

    public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async  Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            var cart = await basketRepository.StoreBasket(request.Cart);

            return new StoreBasketResult(cart.Username);
        }
    }
}