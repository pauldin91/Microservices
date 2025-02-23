using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries;

namespace Catalog.Api.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> OrderDtos);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{customerId}", async (Guid customerId, ISender sender) =>
            {
            
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
    
                var response = result.Adapt<GetOrdersByCustomerResponse>();
            
                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }
}