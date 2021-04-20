using Application.Common.Models;
using Application.Common.Validation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : CQRSResponse, new()
    {
        private readonly IValidationHandler<TRequest> _validationHandler;

        public ValidationBehavior() { }

        public ValidationBehavior(IValidationHandler<TRequest> validationHandler)
        {
            _validationHandler = validationHandler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validationHandler == null) return await next();

            var result = await _validationHandler.Validate(request);

            if (result.IsSuccessful == false)
            {
                return new TResponse 
                { 
                    IsSuccessful = false, 
                    ErrorMessage = $"Validation error: {result.Error}" 
                };
            }

            return await next();
        }
    }
}
