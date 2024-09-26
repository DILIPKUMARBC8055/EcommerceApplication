using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commads
{
    public class UpdateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public UpdateShoppingCartCommand(string username, List<ShoppingCartItem> items)
        {
            UserName = username;
            Items = items;

        }
    }
}
