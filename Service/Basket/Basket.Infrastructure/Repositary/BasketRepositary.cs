using Basket.Core.Entities;
using Basket.Core.Repositary;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositary
{
    public class BasketRepositary : IBasketRepositary
    {
        private readonly IDistributedCache _redis;

        public BasketRepositary(IDistributedCache redis)
        {
            _redis = redis;

        }
        public async Task<ShoppingCart> CreateShoppingCartAsyn(ShoppingCart shoppingCart)
        {
            await _redis.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart.Items));
            return shoppingCart;
        }


        public async Task<ShoppingCart> GetShoppingCartAsync(string username)
        {
            var basket = await _redis.GetStringAsync(username);
            if (basket == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }

        public async Task<ShoppingCart> UpdateShoppingCartAsyn(ShoppingCart shoppingCart)
        {
            await _redis.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart.Items));
            return await GetShoppingCartAsync(shoppingCart.UserName);

        }
        public async Task<bool> DeleteShoppingCartAsyn(string username)
        {
            try
            {
                await _redis.RemoveAsync(username);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
