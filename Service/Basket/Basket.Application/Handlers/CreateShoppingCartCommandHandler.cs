using Basket.Application.Commads;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositary;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepositary _basketRepositary;

        public CreateShoppingCartCommandHandler(IBasketRepositary basketRepositary)
        {
            _basketRepositary = basketRepositary;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _basketRepositary.UpdateShoppingCartAsyn(new ShoppingCart(request.UserName, request.Items));
            if (cart == null)
            {
                return null;
            }
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
