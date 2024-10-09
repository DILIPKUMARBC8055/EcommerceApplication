using AutoMapper;
using MediatR;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositaries;

namespace Ordering.Application.Handlers
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepositary _orderRepositary;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepositary orderRepositary, IMapper mapper)
        {
            _orderRepositary = orderRepositary;
            _mapper = mapper;
        }
        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepositary.GetOrdersByName(request.UserName);
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
