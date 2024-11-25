using BuildingBlocks.Cqrs;
using Carter;
using Mapster;
using MediatR;
using System.Net.WebSockets;

namespace Catalog.Api.Products.CreateProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", 
                async (Guid id, ISender sender) =>
            {
                _ = await sender.Send(new DeleteProductCommand(id));
                
                return Results.NoContent();
            })
                .WithName("DeleteProduct")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete a product")
                .WithDescription("Delete a product");
        }
    }
}