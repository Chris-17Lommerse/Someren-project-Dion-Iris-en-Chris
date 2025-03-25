using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public class DbOrdersRepository : BaseRepository, IOrdersRepository
    {
        const string BaseSelectQuery = "SELECT studentennr, drankid, aantal FROM BESTELLING";
        public DbOrdersRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public void Add(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Query to add a room to the database
                string query = $"INSERT INTO BESTELLING (studentennr, drankid, aantal)" +
                               "VALUES (@studentennr, @drankid, @aantal); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentennr", order.StudentNr);
                command.Parameters.AddWithValue("@drankid", order.DrankId);
                command.Parameters.AddWithValue("@aantal", order.Aantal);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
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
            Student studentNr = (Student)reader["studentennr"];
            Drink drankId = (Drink)reader["drankid"];
            int aantal = (int)reader["aantal"];

            return new Order(studentNr, drankId, aantal);
        }
        public Order? GetById(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
