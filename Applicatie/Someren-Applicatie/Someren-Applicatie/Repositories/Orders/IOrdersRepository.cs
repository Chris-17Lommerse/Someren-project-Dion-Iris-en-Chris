using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public interface IOrdersRepository
    {
        List<Order> GetAll();
        Order? GetById(int orderId);
        void Add(Order order);
    }
}
