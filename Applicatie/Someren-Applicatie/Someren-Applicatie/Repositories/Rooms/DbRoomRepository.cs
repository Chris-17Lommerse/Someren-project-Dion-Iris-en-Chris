using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Rooms
{
    public class DbRoomRepository : IRoomRepository
    {
        private readonly string? _connectionString;

        // Connection to the database will be set 
        public DbRoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        public void Add(Room room)
        {
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception("Cannot add room");
            }
        }
        public void Delete(Room room)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Room cannot be deleted.");
            }
        }
        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT kamernr, aantal_slaapplekken, type_kamer FROM SLAAPKAMER ORDER BY kamernr";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        Room room = ReadRoom(reader);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot load rooms from the database");
            }
            return rooms;
        }

        private Room ReadRoom(SqlDataReader reader)
        {
            try
            {
                // retrieve data from fields
                string kamerNummer = (string)reader["kamernr"];
                int aantalSlaapplekken = (int)reader["aantal_slaapplekken"];
                bool typeKamerValue = (bool)reader["type_kamer"];
                TypeKamer typeKamer = typeKamerValue ? TypeKamer.Lecturer :
                    TypeKamer.Student;

                return new Room(kamerNummer, aantalSlaapplekken, typeKamer);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }
        public Room? GetById(string roomId)
        {
            Room room = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT kamernr, aantal_slaapplekken, type_kamer FROM SLAAPKAMER " +
                        "WHERE kamernr = @KamerNr";
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
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot load the room chosen");
            }
            return room;
        }
        public List<Room> GetByRoomSize(string aantalSlaapplekken)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT kamernr, aantal_slaapplekken, type_kamer " +
                               "FROM SLAAPKAMER WHERE aantal_slaapplekken LIKE @aantal_slaapplekken ORDER BY aantal_slaapplekken";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@aantal_slaapplekken", aantalSlaapplekken);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Room room = ReadRoom(reader);
                    rooms.Add(room);
                }
                reader.Close();
            }
            return rooms;
        }
        public void Update(Room room)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Cannot update room");
            }
        }
    }
}
