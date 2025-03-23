using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public class DbOrdersRepository : IOrdersRepository
    {
        private readonly string? _connectionString;

        public DbOrdersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
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
