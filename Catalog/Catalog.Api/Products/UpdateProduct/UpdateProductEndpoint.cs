using BuildingBlocks.Cqrs;
using Carter;
using Mapster;
using MediatR;
using System.Net.WebSockets;

namespace Catalog.Api.Products.CreateProduct
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", 
                async (UpdateProductRequest request, ISender sender) =>
                {
                var command = request.Adapt<UpdateProductCommand>();

                _ = await sender.Send(command);

                return Results.NoContent();
            })
                .WithName("UpdateProduct")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update a product")
                .WithDescription("Update a product");
        }
    }
}