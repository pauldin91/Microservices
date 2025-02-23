using BuildingBlocks.Cqrs;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries;

public class GetOrdersByCustomerQueryHandler(IApplicationDbContext applicationDbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await applicationDbContext.Orders
            .Include(o=>o.OrderItems)
            .AsNoTracking()
            .Where(o=>o.CustomerId.Value==request.CustomerId)
            .OrderBy(o=>o.OrderName.Value)
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}