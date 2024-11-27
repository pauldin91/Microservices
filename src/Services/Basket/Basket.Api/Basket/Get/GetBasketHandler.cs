using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Cqrs;
using Marten;

namespace Basket.Api.Basket.Get
{
    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.Username);
            return new GetBasketResult(basket);
        }
    }
}
