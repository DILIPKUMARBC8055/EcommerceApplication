using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositary;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepositary _basketRepositary;

        public GetBasketByUserNameQueryHandler(IBasketRepositary basketRepositary)
        {
            _basketRepositary = basketRepositary;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepositary.GetShoppingCartAsync(request.Username);
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
