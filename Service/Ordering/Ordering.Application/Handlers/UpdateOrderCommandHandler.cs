using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositaries;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepositary _orderRepositary;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepositary orderRepositary, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepositary = orderRepositary;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepositary.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await _orderRepositary.UpdateAsync(orderToUpdate);
            _logger.LogInformation($"The order {request.Id} is been updated");
            return Unit.Value;
        }
    }
}
