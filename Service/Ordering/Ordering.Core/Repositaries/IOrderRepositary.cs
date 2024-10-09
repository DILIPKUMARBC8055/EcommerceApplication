using Ordering.Core.Entities;

namespace Ordering.Core.Repositaries
{
    public interface IOrderRepositary : IAsyncRepositary<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByName(string userName);
    }
}
