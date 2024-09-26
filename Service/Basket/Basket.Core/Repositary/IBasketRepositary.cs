using Basket.Core.Entities;

namespace Basket.Core.Repositary
{
    public interface IBasketRepositary
    {
        Task<ShoppingCart> GetShoppingCartAsync(string username);
        Task<ShoppingCart> CreateShoppingCartAsyn(ShoppingCart shoppingCart);
        Task<ShoppingCart> UpdateShoppingCartAsyn(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCartAsyn(string username);
    }
}
