using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO SLAAPKAMER (kamernr, aantal_slaapplekken, type_kamer" +
                               "VALUES (@kamernr, @aantal_slaapplekken, @ type_kamer); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", room.KamerNummer);
                command.Parameters.AddWithValue("@aantal_slaaplekken", room.Aantal_Slaaplekken);
                command.Parameters.AddWithValue("@type_kamer", room.TypeKamer);

                command.Connection.Open();
            }
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
            TypeKamer typeKamer = (TypeKamer)reader["type_kamer"];

            return new Room(kamerNummer, aantalSlaapplekken, typeKamer);
        }
        public Room? GetById(int roomId)
        {
            Room room = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT kamernr, aantal_slaapplekken, type_kamer FROM SLAAPKAMER " +
                    "WHERE kanmernr = @KamerNr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@KamerNr", roomId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    room = ReadRoom(reader);
                }
                reader.Close();
            }
            return room;
        }

        public void Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
