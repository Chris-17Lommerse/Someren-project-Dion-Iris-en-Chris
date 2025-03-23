using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Drinks
{
    public class DbDrinksRepository : IDrinksRepository
    {
        private readonly string? _connectionString;

        public DbDrinksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }

        public void Add(Drink drink)
        {
            throw new NotImplementedException();
        }

        public void Delete(Drink drink)
        {
            throw new NotImplementedException();
        }

        public List<Drink> GetAll()
        {
            throw new NotImplementedException();
        }

        public Drink? GetById(int drankId)
        {
            throw new NotImplementedException();
        }

        public void Update(Drink drink)
        {
            throw new NotImplementedException();
        }
    }
}
