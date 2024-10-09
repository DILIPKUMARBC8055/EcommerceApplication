using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositaries;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositaries
{
    public class OrderRepositary : RepositaryBase<Order>, IOrderRepositary
    {
        public OrderRepositary(OrderContext orderContext) : base(orderContext) { }

        public async Task<IEnumerable<Order>> GetOrdersByName(string userName)
        {
            var orders = await _dbContext.Orders.Where(p => p.UserName.ToLower() == userName.ToLower()).ToListAsync();
            return orders;
        }
    }
}
