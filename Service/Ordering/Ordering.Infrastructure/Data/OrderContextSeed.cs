using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public  class OrderContextSeed
    {
        public static async Task SeedAsyn(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(getOrders());
                await context.SaveChangesAsync();
                logger.LogInformation($"The seed of data is done");

            }
        }

        private static IEnumerable<Order> getOrders()
        {
            return new List<Order>
            {
                new Order
                {
                      UserName = "dilip",
                    FirstName = "DILIP",
                    LastName = "KUMAR",
                    EmailAddress = "dilip@eCommerce.net",
                    AddressLine = "Bangalore",
                    Country = "India",
                    TotalPrice = 750,
                    State = "KA",
                    ZipCode = "560001",

                    CardName = "Visa",
                    CardNumber = "1234567890123456",
                    CreatedBy = "Dilip Kumar B C",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Dilip",
                    LastModifiedDate = new DateTime(),
                },
            };
        }
    }
}
