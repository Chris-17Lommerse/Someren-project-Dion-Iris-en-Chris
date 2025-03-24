namespace Someren_Applicatie.Repositories
{
    public class BaseRepository
    {
        protected readonly string? _connectionString;

        // Connection to the database will be defined in the constructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
    }
}
