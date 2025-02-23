using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IFeatureManager featureManager, IPublishEndpoint publishEndpoint) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCreatedEvent = notification.Order.ToOrderDto();

            await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
        }
    }
}