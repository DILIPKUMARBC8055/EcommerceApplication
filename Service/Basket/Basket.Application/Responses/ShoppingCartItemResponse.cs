namespace Basket.Application.Responses
{
    public class ShoppingCartItemResponse
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string ProductImage { get; set; }

        public decimal TotalPriceOfItem
        {
            get
            {
                return Quantity * Price;
            }
        }

    }
}
