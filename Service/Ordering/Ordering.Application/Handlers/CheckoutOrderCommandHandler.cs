using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositaries;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepositary _orderRepositary;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepositary orderRepositary, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepositary = orderRepositary;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var createdEntity = await _orderRepositary.AddAsync(orderEntity);
            _logger.LogInformation($"The order with order ID {createdEntity.Id} created");
            return createdEntity.Id;
        }
    }
}
