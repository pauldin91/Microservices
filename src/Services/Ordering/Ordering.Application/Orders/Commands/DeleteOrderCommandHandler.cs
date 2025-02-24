using BuildingBlocks.Cqrs;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.Application.Orders.Commands;

public class DeleteOrderCommandHandler(IApplicationDbContext applicationDbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderId);
        var order = await applicationDbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }
        applicationDbContext.Orders.Remove(order);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}