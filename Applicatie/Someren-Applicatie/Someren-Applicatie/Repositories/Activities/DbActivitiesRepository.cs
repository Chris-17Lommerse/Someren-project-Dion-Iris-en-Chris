using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Activities
{
    public class DbActivitiesRepository : IActivitiesRepository
    {
        private readonly string? _connectionString;

        // Connection to the database will be defined in the constructor
        public DbActivitiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        // Add activity functionality via query
        public void Add(Activiteit activiteit)
        {
            Activiteit? checkActiviteit = GetByActivityName(activiteit.Naam);
            if (checkActiviteit != null)
            {
                throw new Exception($"Activiteit {activiteit.Naam} bestaat al");
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Query to add an activity to the database
                    string query = $"INSERT INTO ACTIVITEIT (naam, starttijd, eindtijd)" +
                                   "VALUES (@naam, @starttijd, @eindtijd); " +
                                   "SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Parameters will be combined with the activity 
                    command.Parameters.AddWithValue("@naam", activiteit.Naam);
                    command.Parameters.AddWithValue("@starttijd", activiteit.StartTijd);
                    command.Parameters.AddWithValue("@eindtijd", activiteit.EindTijd);

                    // Query will be exexcuted
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Kan activiteit niet toevoegen");
            }
        }
        // Delete functionality via query
        public void Delete(Activiteit activiteit)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Query to delete an activity
                    string query = $"DELETE FROM ACTIVITEIT WHERE activiteitid = @activiteitId";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@activiteitId", activiteit.ActiviteitId);

                    command.Connection.Open();
                    // Query will be executed
                    int nrOfAffectedRows = command.ExecuteNonQuery();
                    if (nrOfAffectedRows == 0)
                    {
                        throw new Exception("No records deleted");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Activititeit kan niet worden verwijderd");
            }
        }
        // Get all rooms via query
        public List<Activiteit> GetAll()
        {
            List<Activiteit> activiteiten = new List<Activiteit>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"{BaseQuery()} ORDER BY starttijd";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        Activiteit activiteit = ReadActivity(reader);
                        activiteiten.Add(activiteit);
                    }
                    reader.Close();
                }
            } 
            catch (Exception)
            {
                throw new Exception("Activiteiten kunnen niet worden geladen");
            }
            return activiteiten;
        }

        // Read the data via SqlDataReader
        private Activiteit ReadActivity(SqlDataReader reader)
        {
            // retrieve data from fields
            int activiteitId = (int)reader["activiteitid"];
            string naam = (string)reader["naam"];
            DateTime startTijd = (DateTime)reader["starttijd"];
            DateTime eindTijd = (DateTime)reader["eindtijd"];

            return new Activiteit(activiteitId, naam, startTijd, eindTijd);
        }
        // Base query
        private string BaseQuery()
        {
            string query = $"SELECT activiteitid, naam, starttijd, eindtijd FROM ACTIVITEIT";
            return query;
        }

        // Get Activity by id via query
        public Activiteit? GetById(int activiteitId)
        {
            Activiteit? activiteit = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"{BaseQuery()} WHERE activiteitid = @activiteitId";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@activiteitId", activiteitId);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        activiteit = ReadActivity(reader);
                    }
                    reader.Close();
                }
            } catch (Exception)
            {
                throw new Exception("Kan de activiteit niet laden.");
            }
            return activiteit;
        }
        // Filter activities by activity name via query
        public Activiteit? GetByActivityName(string activityName)
        {
            Activiteit? activiteit = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"{BaseQuery()} WHERE naam = @naam";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@naam", activityName);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        activiteit = ReadActivity(reader);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Kan activiteit niet laden");
            }
            return activiteit;
        }
        // Update activity via query
        public void Update(Activiteit activiteit)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Query to update an activity
                    string query = "UPDATE ACTIVITEIT SET naam = @naam, starttijd = @startTijd, eindtijd = @eindTijd " +
                               "WHERE activiteitid = @activiteitId";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@activiteitId", activiteit.ActiviteitId);
                    command.Parameters.AddWithValue("@naam", activiteit.Naam);
                    command.Parameters.AddWithValue("@startTijd", activiteit.StartTijd);
                    command.Parameters.AddWithValue("@eindTijd", activiteit.EindTijd);

                    command.Connection.Open();
                    // Query will be executed
                    int nrOfAffectedRows = command.ExecuteNonQuery();

                    if (nrOfAffectedRows == 0)
                    {
                        throw new Exception("No records updated");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Activiteit kan niet worden aangepast");
            }
        }
        // Get activity by name via query
        public List<Activiteit> GetByName(string Naam)
        {
            List<Activiteit> activiteit = new List<Activiteit>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT activiteitid, naam, starttijd, eindtijd " +
                               "FROM ACTIVITEIT WHERE naam LIKE @naam ORDER BY naam";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@naam", "%" + Naam + "%");

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Activiteit activity = ReadActivity(reader);
                    activiteit.Add(activity);
                }
                reader.Close();
            }
            return activiteit;
        }
    }
}


