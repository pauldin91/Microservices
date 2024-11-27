﻿using BuildingBlocks.Cqrs;
using Catalog.Api.Models;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    public class GetProductsHandler(ILogger<GetProductsHandler> logger, IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.PageNumber,request.PageSize,cancellationToken);
            return new GetProductsResult(products);
        }
    }
}