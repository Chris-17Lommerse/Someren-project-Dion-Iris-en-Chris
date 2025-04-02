using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Rooms
{
    public class DbRoomRepository : BaseRepository, IRoomRepository
    {
        const string BaseSelectQuery = "SELECT kamernr, aantal_slaapplekken, type_kamer FROM SLAAPKAMER";


        // Connection to the database will be set 
        public DbRoomRepository(IConfiguration configuration) : base(configuration)
        {

        }
        // Add room functionality via query 
        public void Add(Room room)
        {
            Room? checkRoom = GetById(room.KamerNummer);
            if (checkRoom != null)
            {
                throw new Exception($"Kamer {room.KamerNummer} bestaat al");
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to add a room to the database
                string query = $"INSERT INTO SLAAPKAMER (kamernr, aantal_slaapplekken, type_kamer)" +
                               "VALUES (@kamernr, @aantal_slaapplekken, @type_kamer); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", room.KamerNummer);
                command.Parameters.AddWithValue("@aantal_slaapplekken", room.AantalSlaaplekken);
                command.Parameters.AddWithValue("@type_kamer", room.TypeKamer);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Room room)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM SLAAPKAMER WHERE kamernr = @kamernr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", room.KamerNummer);

                command.Connection.Open();
                int nrOfAffectedRows = command.ExecuteNonQuery();
                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("No records deleted");
                }
            }
        }
        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} ORDER BY kamernr";
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
            bool typeKamerValue = (bool)reader["type_kamer"];
            TypeKamer typeKamer = typeKamerValue ? TypeKamer.Lecturer :
                TypeKamer.Student;

            return new Room(kamerNummer, aantalSlaapplekken, typeKamer);
        }
        public Room? GetById(string roomId)
        {
            Room? room = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE kamernr = @KamerNr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@KamerNr", roomId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    room = ReadRoom(reader);
                }
                reader.Close();
            }
            return room;
        }
        // filter query functionality
        public List<Room> GetByRoomSize(string aantalSlaapplekken)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE aantal_slaapplekken LIKE @aantal_slaapplekken ORDER BY aantal_slaapplekken";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@aantal_slaapplekken", aantalSlaapplekken);

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
        // Update room functionality via query
        public void Update(Room room)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE SLAAPKAMER SET aantal_slaapplekken = @aantal_slaapplekken, " +
                          "type_kamer = @type_kamer WHERE kamernr = @kamernr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", room.KamerNummer);
                command.Parameters.AddWithValue("@aantal_slaapplekken", room.AantalSlaaplekken);
                command.Parameters.AddWithValue("@type_kamer", room.TypeKamer);

                command.Connection.Open();
                int nrOfAffectedRows = command.ExecuteNonQuery();
                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("No records updated");
                }
            }
        }
    }
}
