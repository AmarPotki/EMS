using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Irvine.SeedWork.Domain;

namespace BuildingBlocks.Infrastructure.Services.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;
        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var errorBuilder = new StringBuilder();
                errorBuilder.AppendLine($"Command Validation Errors for type {typeof(TRequest).Name}");
                failures.ForEach(failure =>
                {
                    errorBuilder.AppendLine(failure.ErrorMessage);
                });
                throw new DomainException(
                    errorBuilder.ToString(), new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}