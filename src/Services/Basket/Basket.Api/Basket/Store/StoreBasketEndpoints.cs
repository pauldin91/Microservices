using Basket.Api.Basket.Store;
using Basket.Api.Models;
using Mapster;
using Marten.Linq.CreatedAt;

namespace Basket.Api.Basket.Get
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);

    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.Username}",response);
            })
                .WithName("StoreBasket")
                .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Store basket")
                .WithDescription("Store basket");
        }
    }
}