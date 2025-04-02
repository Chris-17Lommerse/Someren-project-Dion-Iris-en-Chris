using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Drinks
{
    public class DbDrinksRepository : BaseRepository, IDrinksRepository
    {
        // Base query
        const string BaseSelectQuery = "SELECT drankid, dranknaam, aantal, alcoholisch, prijs FROM DRANKJE";

        public DbDrinksRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void Add(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to add a drink to the database
                string query = $"INSERT INTO DRANKJE (dranknaam, aantal, alcoholisch, prijs)" +
                                   "VALUES (@drankNaam, @aantal, @alcoholisch, @prijs); " +
                                   "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                // Parameters will be combined with the activity 
                command.Parameters.AddWithValue("@drankNaam", drink.DrankNaam);
                command.Parameters.AddWithValue("@aantal", drink.Aantal);
                command.Parameters.AddWithValue("@alcoholisch", drink.IsAlcoholisch);
                command.Parameters.AddWithValue("@prijs", drink.Prijs);

                // Query will be exexcuted
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to delete a drink
                string query = $"DELETE FROM DRANKJE WHERE drankid = @drankId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@drankId", drink.DrankId);

                command.Connection.Open();
                // Query will be executed
                int nrOfAffectedRows = command.ExecuteNonQuery();
                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("Drankje kan niet verwijderd worden");
                }
            }
        }

        public List<Drink> GetAll()
        {
            List<Drink> drinks = new List<Drink>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} ORDER BY dranknaam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Drink drink = ReadDrink(reader);
                    drinks.Add(drink);
                }
                reader.Close();
            }
            return drinks;
        }

        // Read the data via SqlDataReader
        private Drink ReadDrink(SqlDataReader reader)
        {
            // retrieve data from fields
            int drankId = (int)reader["drankid"];
            string drankNaam = (string)reader["dranknaam"];
            int aantal = (int)reader["aantal"];
            bool typeDrankjeValue = (bool)reader["alcoholisch"];
            TypeDrankje typeDrankje = typeDrankjeValue ? TypeDrankje.Nonalcoholisch :
                TypeDrankje.Alcoholisch;
            decimal prijs = (decimal)reader["prijs"];

            return new Drink(drankId, drankNaam, aantal, typeDrankje, prijs);
        }

        // Get Drink by id via query
        public Drink? GetById(int drankId)
        {
            Drink? drink = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE drankid = @drankId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@drankId", drankId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new Exception("Geen activiteit gevonden met die naam");
                }
                drink = ReadDrink(reader);
                reader.Close();
            }
            return drink;
        }
        public void Update(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to update an activity
                string query = "UPDATE DRANKJE SET dranknaam = @drankNaam, aantal = @aantal, alcoholisch = @alcoholisch, prijs = @prijs " +
                           "WHERE drankid = @drankId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@drankId", drink.DrankId);
                command.Parameters.AddWithValue("@drankNaam", drink.DrankNaam);
                command.Parameters.AddWithValue("@aantal", drink.Aantal);
                command.Parameters.AddWithValue("@alcoholisch", drink.IsAlcoholisch);
                command.Parameters.AddWithValue("@prijs", drink.Prijs);

                command.Connection.Open();
                // Query will be executed
                int nrOfAffectedRows = command.ExecuteNonQuery();

                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("No records updated");
                }
            }
        }
    }
}
