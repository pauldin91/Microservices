using Ordering.Application.Dtos;
using Ordering.Application.Orders;

namespace Catalog.Api.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async(Guid id, ISender sender) =>
            {
            
                var result = await sender.Send(new DeleteOrderCommand(id));
    
                var response = result.Adapt<DeleteOrderResponse>();
            
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete an order")
            .WithDescription("Delete an order");
    }
}