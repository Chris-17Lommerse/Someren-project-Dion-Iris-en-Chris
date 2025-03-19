using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO ACTIVITEIT (naam, starttijd, eindtijd)" +
                               "VALUES (@naam, @starttijd, @eindtijd); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@naam", activiteit.Naam);
                command.Parameters.AddWithValue("@starttijd", activiteit.StartTijd);
                command.Parameters.AddWithValue("@eindtijd", activiteit.EindTijd);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Activiteit activiteit)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM ACTIVITEIT WHERE activiteitid = @activiteitId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitId", activiteit.ActiviteitId);

                command.Connection.Open();
                int nrOfAffectedRows = command.ExecuteNonQuery();
                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("No records deleted");
                }
            }
        }

        public List<Activiteit> GetAll()
        {
            List<Activiteit> activiteiten = new List<Activiteit>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT activiteitid, naam, starttijd, eindtijd FROM ACTIVITEIT ORDER BY starttijd";
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
                string query = "SELECT activiteitid, naam, starttijd, eindtijd FROM ACTIVITEIT " +
                    "WHERE activiteitid = @activiteitId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitId", activiteitId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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
                string query = "UPDATE ACTIVITEIT SET naam = @naam, starttijd = @startTijd, eindtijd = @eindTijd " +
                           "WHERE activiteitid = @activiteitId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitId", activiteit.ActiviteitId);
                command.Parameters.AddWithValue("@naam", activiteit.Naam);
                command.Parameters.AddWithValue("@startTijd", activiteit.StartTijd);
                command.Parameters.AddWithValue("@eindTijd", activiteit.EindTijd);

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
