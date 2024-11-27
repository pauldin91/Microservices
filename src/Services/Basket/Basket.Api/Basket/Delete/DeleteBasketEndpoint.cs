namespace Basket.Api.Basket.Delete
{
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(username));

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.NoContent();
            })
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete basket")
                .WithDescription("Delete basket");
        }
    }
}