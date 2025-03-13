using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Activities
{
    public class DbActivitiesRepository : IActivitiesRepository
    {
        private readonly string? _connectionString;

        public DbActivitiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        public void Add(Activiteit activiteit)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activiteit activiteit)
        {
            throw new NotImplementedException();
        }

        public List<Activiteit> GetAll()
        {
            throw new NotImplementedException();
        }

        public Activiteit? GetById(int activiteitId)
        {
            throw new NotImplementedException();
        }

        public void Update(Activiteit activiteit)
        {
            throw new NotImplementedException();
        }
    }
}
