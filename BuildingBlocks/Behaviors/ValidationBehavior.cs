using BuildingBlocks.Cqrs;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var ctx = new ValidationContext<TRequest>(request);
        
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(ctx, cancellationToken)));
        
        
        var failures = validationResults
            .Where(result => result.Errors.Any())   
            .SelectMany(result => result.Errors)
            .ToList();
        
        if(failures.Any())
            throw new ValidationException(failures);
        
        return await next();    
    }
}