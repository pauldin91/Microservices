using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries;

namespace Catalog.Api.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Response);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginatedRequest request, ISender sender) =>
            {
            
                var result = await sender.Send(new GetOrdersQuery(request));
    
                var response = result.Adapt<GetOrdersResponse>();
            
                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
    }
}