using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next();

            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            List<ValidationFailure> failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}