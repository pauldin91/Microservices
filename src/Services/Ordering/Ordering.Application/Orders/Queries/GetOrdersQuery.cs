using BuildingBlocks.Cqrs;
using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersQuery(PaginatedRequest Request) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> OrderDtos);


    
