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
        public void Add(Activiteit activiteit)
        {
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
            catch (Exception ex)
            {
                throw new Exception("Cannot add activity");
            }
        }

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
            catch (Exception ex)
            {
                throw new Exception("Activity cannot be deleted");
            }
        }

        public List<Activiteit> GetAll()
        {
            List<Activiteit> activiteiten = new List<Activiteit>();
            try
            {
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
            } 
            catch (Exception ex)
            {
                throw new Exception("Cannot load activities");
            }
            return activiteiten;
        }

        private Activiteit ReadActivity(SqlDataReader reader)
        {
            try
            {
                // retrieve data from fields
                int activiteitId = (int)reader["activiteitid"];
                string naam = (string)reader["naam"];
                DateTime startTijd = (DateTime)reader["starttijd"];
                DateTime eindTijd = (DateTime)reader["eindtijd"];

                return new Activiteit(activiteitId, naam, startTijd, eindTijd);
            } catch (Exception ex)
            {
                throw new Exception("Cannot read activities");
            } 
            
        }

        public Activiteit? GetById(int activiteitId)
        {
            Activiteit? activiteit = null;
            try
            {
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
            } catch (Exception ex)
            {
                throw new Exception("Cannot load the activity chosen");
            }
            return activiteit;
        }
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
            catch (Exception ex)
            {
                throw new Exception("Cannot update activity");
            }
        }

       
    }
}
