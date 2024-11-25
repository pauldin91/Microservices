using BuildingBlocks.Cqrs;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);


public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>   
{
    
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
       var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

       if (product is null)
           throw new ProductNotFoundException();
       
       return new GetProductByIdResult(product);
    }
}