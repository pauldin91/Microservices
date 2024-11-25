using BuildingBlocks.Cqrs;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Products.CreateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var toBeUpdated = await session.LoadAsync<Product>(command.Id);
            
            if (toBeUpdated == null)
                throw new ProductNotFoundException();
            
            var product = new Product
            {
                Id = toBeUpdated.Id,
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);

        }
    }
}