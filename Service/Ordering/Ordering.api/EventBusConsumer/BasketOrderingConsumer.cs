using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketOrderingConsumer> _logger;
        private readonly IMapper _mapper;

        public BasketOrderingConsumer(IMediator mediator,ILogger<BasketOrderingConsumer> logger,IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = _logger.BeginScope("The even of basket checkout is consuming {corelationId}", context.Message.CorelationId);
            var cmd = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result= await _mediator.Send(cmd);
            _logger.LogInformation("Basket checkout executed successfully of id"+context.Message.CorelationId);
        }
    }
}
