using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Students
{
    public class DbStudentsRepository : IStudentsRepository
    {
        private readonly string? _connectionString;

        public DbStudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }

        public void Add(Student student)
        {
            throw new NotImplementedException();
        }

        public void Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student? GetById(int StudentNr)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
