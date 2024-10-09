using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();  
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
            
        }
    }
}
