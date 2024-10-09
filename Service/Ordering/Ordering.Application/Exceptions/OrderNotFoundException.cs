namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException(string name, int id):base($"The order {name} - {id} not found")
        {
            
        }
    }
}
