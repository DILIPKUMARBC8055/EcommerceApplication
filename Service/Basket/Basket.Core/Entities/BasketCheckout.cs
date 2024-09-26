using System.Reflection.Metadata.Ecma335;

namespace Basket.Core.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string State { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string Expiry { get; set; }

        public int PaymentMethod { get; set; }
    }
}
