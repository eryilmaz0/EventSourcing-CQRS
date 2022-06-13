using Command.Domain.Exception;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Command.Domain.Exception.ValidationException;

namespace Command.Infrastructure.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResult.SelectMany(x => x.Errors).Where(x => x != null).ToList();

            if (failures.Count > 0)
                ThrowValidationError(failures);
        }

        return await next();
    }


    private void ThrowValidationError(List<ValidationFailure> errors)
    {
        var validationErrors = errors.Select(x => new ValidationError()
        {
            Field = x.PropertyName,
            ErrorMessage = x.ErrorMessage
        });

        throw new ValidationException(validationErrors.ToList());
    }
}