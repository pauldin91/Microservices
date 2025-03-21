using BuildingBlocks.Cqrs;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProductById;


public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Product);

internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>   
{

    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(request.Category))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}