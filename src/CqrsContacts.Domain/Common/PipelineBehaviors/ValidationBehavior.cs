namespace CqrsContacts.Domain.Common.PipelineBehaviors
{
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context)));

            var errors = validationResult.Where(x => !x.IsValid).SelectMany(x => x.Errors).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            var response = await next();
            return response;
        }
    }
}
