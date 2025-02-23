using BuildingBlocks.Cqrs;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders;

public class DeleteOrderCommandHandler(IApplicationDbContext applicationDbContext) : ICommandHandler<DeleteOrderCommand,DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderId); 
        var order = await applicationDbContext
            .Orders
            .FindAsync(orderId) ?? throw new OrderNotFoundException(request.OrderId);
        
        applicationDbContext.Orders.Remove(order);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);

    }
}