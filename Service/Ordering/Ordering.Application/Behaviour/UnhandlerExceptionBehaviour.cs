using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviour
{
    public class UnhandlerExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandlerExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(requestName, $"There was unhandle exception for the request {requestName} - {request}");
                throw;
            }
        }
    }
}
