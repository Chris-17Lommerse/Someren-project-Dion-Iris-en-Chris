using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public class DbOrdersRepository : BaseRepository, IOrdersRepository
    {

        public DbOrdersRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public void Add(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order? GetById(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
