using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketByUserNameQuery :IRequest<ShoppingCartResponse>
    {
        public string Username { get; set; }
        public GetBasketByUserNameQuery(string username)
        {
            Username = username;

        }
    }
}
