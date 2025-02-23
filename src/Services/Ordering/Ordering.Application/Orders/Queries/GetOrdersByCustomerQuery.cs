using BuildingBlocks.Cqrs;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;


public record GetOrdersByCustomerResult(IEnumerable<OrderDto> OrderDtos);