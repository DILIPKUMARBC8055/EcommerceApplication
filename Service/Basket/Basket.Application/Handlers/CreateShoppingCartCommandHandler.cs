using Basket.Application.Commads;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositary;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepositary _basketRepositary;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly ILogger<CreateShoppingCartCommandHandler> _logger;

        public CreateShoppingCartCommandHandler(IBasketRepositary basketRepositary, DiscountGrpcService discountGrpcService, ILogger<CreateShoppingCartCommandHandler> logger)
        {
            _basketRepositary = basketRepositary;
            _discountGrpcService = discountGrpcService;
            _logger = logger;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                _logger.LogInformation($"The discount for the {item.ProductName} coupon is triggered");
                var coupon = await _discountGrpcService.getDiscount(item.ProductName);
                if (coupon != null)
                {
                    _logger.LogInformation($"The discount for the {item.ProductName} coupon is applied succefully");
                    item.Price -= coupon.Amount;
                }
            }


            var cart = await _basketRepositary.CreateShoppingCartAsyn(new ShoppingCart
            {
                Items = request.Items,
                UserName = request.UserName
            });
            if (cart == null)
            {
                return null;
            }
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
