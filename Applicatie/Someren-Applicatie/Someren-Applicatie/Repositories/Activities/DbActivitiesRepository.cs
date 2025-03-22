using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Activities
{
    public class DbActivitiesRepository : IActivitiesRepository
    {
        const string BaseSelectQuery = "SELECT activiteitid, naam, starttijd, eindtijd FROM ACTIVITEIT";
        private readonly string? _connectionString;

        // Connection to the database will be defined in the constructor
        public DbActivitiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        public void Add(Activiteit activiteit)
        {
            Activiteit? checkActiviteit = GetByActivityName(activiteit.Naam);
            if (checkActiviteit != null)
            {
                throw new Exception($"Activiteit {activiteit.Naam} bestaat al");
            }
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

        public void Delete(Activiteit activiteit)
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
                    throw new Exception("Activiteit kan niet verwijderd worden");
                }
            }
        }

        public List<Activiteit> GetAll()
        {
            List<Activiteit> activiteiten = new List<Activiteit>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} ORDER BY starttijd";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Activiteit activiteit = ReadActivity(reader);
                    activiteiten.Add(activiteit);
                }
                reader.Close();
            }
            return activiteiten;
        }

        private Activiteit ReadActivity(SqlDataReader reader)
        {
                // retrieve data from fields
                int activiteitId = (int)reader["activiteitid"];
                string naam = (string)reader["naam"];
                DateTime startTijd = (DateTime)reader["starttijd"];
                DateTime eindTijd = (DateTime)reader["eindtijd"];

                return new Activiteit(activiteitId, naam, startTijd, eindTijd);
        }

        public Activiteit? GetById(int activiteitId)
        {
            Activiteit? activiteit = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE activiteitid = @activiteitId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitId", activiteitId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    activiteit = ReadActivity(reader);
                }
                reader.Close();
            }
            return activiteit;
        }

        public Activiteit? GetByActivityName(string activityName)
        {
            Activiteit? activiteit = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE naam = @naam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@naam", activityName);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    activiteit = ReadActivity(reader);
                }
                reader.Close();
            }
            return activiteit;
        }
        public void Update(Activiteit activiteit)
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

        public List<Activiteit> GetByName(string Naam)
        {
            List<Activiteit> activiteit = new List<Activiteit>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = BaseSelectQuery + " WHERE naam LIKE @naam ORDER BY naam";
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


