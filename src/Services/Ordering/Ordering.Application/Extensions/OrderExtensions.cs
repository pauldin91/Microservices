using Microsoft.AspNetCore.Routing;
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
                    ShippingAddress: new AddressDto(
                        s.ShippingAddress.FirstName,
                        s.ShippingAddress.LastName,
                        s.ShippingAddress.EmailAddress,
                        s.ShippingAddress.AddressLine,
                        s.ShippingAddress.Country,
                        s.ShippingAddress.State,
                        s.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(
                        s.BillingAddress.FirstName,
                        s.BillingAddress.LastName,
                        s.BillingAddress.EmailAddress,
                        s.BillingAddress.AddressLine,
                        s.BillingAddress.Country,
                        s.BillingAddress.State,
                        s.BillingAddress.ZipCode
                    ),
                    Payment: new PaymentDto(
                        s.Payment.CardName,
                        s.Payment.CardNumber,
                        s.Payment.Expiration,
                        s.Payment.CVV,
                        s.Payment.PaymentMethod),
                    Status: s.Status,
                    OrderItems: s.OrderItems.Select(o=>new OrderItemDto(o.OrderId.Value,o.ProductId.Value,o.Quantity,o.Price)).ToList()
                
                )
            ).ToList();
    }
}