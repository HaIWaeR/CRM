using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviors
{
    /// <summary>
    /// Шаг конвейера MediatR, который перед вызовом обработчика прогоняет запрос
    /// через все зарегистрированные для него валидаторы FluentValidation.
    /// </summary>
    /// <remarks>
    /// Подключается в <c>Program.cs</c> через <c>AddOpenBehavior</c> и срабатывает для каждого запроса.
    /// Если для запроса нет валидаторов (например, для <see cref="Behavior.Client.GetClientByIdQuery"/>),
    /// запрос сразу передаётся обработчику.
    /// </remarks>
    /// <typeparam name="TRequest">Тип команды или запроса.</typeparam>
    /// <typeparam name="TResponse">Тип результата обработчика.</typeparam>
    /// <param name="validators">Все валидаторы для <typeparamref name="TRequest"/>, найденные в DI.</param>
    public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Запускает все валидаторы параллельно и собирает ошибки.
        /// Если ошибок нет — вызывает следующий шаг конвейера.
        /// </summary>
        /// <param name="request">Проверяемая команда или запрос.</param>
        /// <param name="next">Следующий шаг конвейера (как правило, сам обработчик).</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат обработчика.</returns>
        /// <exception cref="ValidationException">
        /// Хотя бы один валидатор нашёл ошибку; исключение содержит список всех ошибок.
        /// </exception>
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