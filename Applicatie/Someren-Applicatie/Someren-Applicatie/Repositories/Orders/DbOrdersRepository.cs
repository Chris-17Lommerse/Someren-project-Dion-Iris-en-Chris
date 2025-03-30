using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public class DbOrdersRepository : BaseRepository, IOrdersRepository
    {
        const string BaseSelectQuery = "SELECT B.bestellingid, B.studentennr, S.voornaam AS studentnaam, B.drankid, D.dranknaam AS dranknaam, B.aantal " +
                                       "FROM BESTELLING AS B " +
                                       "INNER JOIN STUDENT AS S ON B.studentennr = S.studentennr " +
                                       "INNER JOIN DRANKJE AS D ON B.drankid = D.drankid";
        public DbOrdersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void Add(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                // Query to add an order to the database
                string insertQuery = $"INSERT INTO BESTELLING (studentennr, drankid, aantal)" +
                               "VALUES (@studentNr, @drankId, @aantal); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction);

                insertCommand.Parameters.AddWithValue("@studentNr", order.StudentNr);
                insertCommand.Parameters.AddWithValue("@drankId", order.DrankId);
                insertCommand.Parameters.AddWithValue("@aantal", order.Aantal);

                insertCommand.ExecuteNonQuery();

                // Query om het aantal drankkjes te verminderen
                string updateQuery = $"UPDATE DRANKJE SET aantal = aantal - @aantal WHERE drankid = @drankId AND aantal >= @aantal";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);

                updateCommand.Parameters.AddWithValue("@drankId", order.DrankId);
                updateCommand.Parameters.AddWithValue("@aantal", order.Aantal);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if(rowsAffected == 0)
                {
                    throw new Exception("Onvoldoende voorraad van de drankjes of drankje niet gevobden");
                }

                transaction.Commit();
                connection.Close();
            }
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery}";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = ReadOrder(reader);
                    orders.Add(order);
                }
                reader.Close();
            }
            return orders;
        }


        private Order ReadOrder(SqlDataReader reader)
        {
            int bestellingId = (int)reader["bestellingid"];
            int studentNr = (int)reader["studentennr"];
            string studentNaam = (string)reader["studentnaam"];
            int drankId = (int)reader["drankid"];
            string drankNaam = (string)reader["dranknaam"];
            int aantal = (int)reader["aantal"];

            return new Order(bestellingId, studentNr, studentNaam, drankId, drankNaam, aantal);
        }
        public Order? GetById(int bestellingId)
        {
            Order? order = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE bestellingid = @bestellingId";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@bestellingId", bestellingId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    order = ReadOrder(reader);
                }
                reader.Close();
            }
            return order;
        }
    }
}
