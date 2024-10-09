using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositaries;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepositary _orderRepositary;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepositary orderRepositary, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepositary = orderRepositary;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _orderRepositary.GetByIdAsync(request.Id);
            if (orderEntity == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }
            await _orderRepositary.DeleteAsync(orderEntity);
            return Unit.Value;


        }
    }
}
