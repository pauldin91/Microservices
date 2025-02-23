using BuildingBlocks.Cqrs;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

namespace Ordering.Application.Orders;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x=>x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.");
    }
}