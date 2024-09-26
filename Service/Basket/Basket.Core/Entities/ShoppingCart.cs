namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart(string username)
        {
            UserName = username;

        }
        public ShoppingCart(string username, List<ShoppingCartItem> items)
        {
            Items = items;
            UserName = username;

        }
    }
}
