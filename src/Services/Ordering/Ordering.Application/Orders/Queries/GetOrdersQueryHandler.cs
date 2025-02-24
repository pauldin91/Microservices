using BuildingBlocks.Cqrs;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries;

public class GetOrdersQueryHandler(IApplicationDbContext applicationDbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await applicationDbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(request.Request.PageSize * request.Request.PageIndex)
            .Take(request.Request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await applicationDbContext.Orders.CountAsync(cancellationToken);
        
        return new GetOrdersResult(new PaginatedResult<OrderDto>(request.Request.PageIndex, request.Request.PageSize,count,orders.ToOrderDtoList()));
    }
}