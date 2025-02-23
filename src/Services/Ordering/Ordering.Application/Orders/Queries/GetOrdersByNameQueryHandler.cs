using System.Collections.Immutable;
using BuildingBlocks.Cqrs;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries;

public class GetOrdersByNameQueryHandler(IApplicationDbContext applicationDbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        
        var orders = await applicationDbContext.Orders
            .Include(o=>o.OrderItems)
            .AsNoTracking()
            .Where(o=>o.OrderName.Value.Contains(request.Name))
            .OrderBy(o=>o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
        
    }
    
}