using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Rooms
{
    public class DbRoomRepository : IRoomRepository
    {
        private readonly string? _connectionString;

        public DbRoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        public void Add(Room room)
        {
            throw new NotImplementedException();
        }
        public void Delete(Room room)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT kamernr, aantal_slaapplekken, type_kamer FROM SLAAPKAMER";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Room room = ReadRoom(reader);
                    rooms.Add(room);
                }
                reader.Close();
            }
            return rooms;
        }

        private Room ReadRoom(SqlDataReader reader)
        {
            // retrieve data from fields
            string kamerNummer = (string)reader["kamernr"];
            int aantalSlaapplekken = (int)reader["aantal_slaapplekken"];
            

            return new Room(kamerNummer, aantalSlaapplekken);
        }
        public Room? GetById(int roomId)
        {
            throw new NotImplementedException();
        }

        public void Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
