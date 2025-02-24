using Basket.Api.Dtos;
using BuildingBlocks.Cqrs;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto must not be empty");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username required");
        }
    }

    public class CheckoutBasketHanlder(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}