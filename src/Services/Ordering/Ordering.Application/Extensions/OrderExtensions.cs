using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders
            .Select(s =>
                new OrderDto(
                    Id: s.Id.Value,
                    CustomerId: s.CustomerId.Value,
                    OrderName: s.OrderName.Value,
                    ShippingAddress: new AddressDto(s.ShippingAddress.FirstName, s.ShippingAddress.LastName, s.ShippingAddress.EmailAddress, s.ShippingAddress.AddressLine, s.ShippingAddress.Country, s.ShippingAddress.State, s.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(s.BillingAddress.FirstName, s.BillingAddress.LastName, s.BillingAddress.EmailAddress, s.BillingAddress.AddressLine, s.BillingAddress.Country, s.BillingAddress.State, s.BillingAddress.ZipCode),
                    Payment: new PaymentDto(s.Payment.CardName, s.Payment.CardNumber, s.Payment.Expiration, s.Payment.CVV, s.Payment.PaymentMethod),
                    Status: s.Status, OrderItems: s.OrderItems.Select(o => new OrderItemDto(o.OrderId.Value, o.ProductId.Value, o.Quantity, o.Price)).ToList()

                )
            ).ToList();
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
    }
}