using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Orders
{
    public class DbOrdersRepository : BaseRepository, IOrdersRepository
    {
        const string BaseSelectQuery = "SELECT B.studentennr, S.voornaam AS studentnaam, B.drankid, D.dranknaam AS dranknaam, B.aantal " +
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
                // Query to add a room to the database
                string query = $"INSERT INTO BESTELLING (studentennr, drankid, aantal)" +
                               "VALUES (@studentNr, @drankId, @aantal); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentNr", order.StudentNr);
                command.Parameters.AddWithValue("@drankId", order.DrankId);
                command.Parameters.AddWithValue("@aantal", order.Aantal);

                command.Connection.Open();
                command.ExecuteNonQuery();
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
            int studentNr = (int)reader["studentennr"];
            string studentNaam = (string)reader["studentnaam"];
            int drankId = (int)reader["drankid"];
            string drankNaam = (string)reader["dranknaam"];
            int aantal = (int)reader["aantal"];

            return new Order(studentNr, studentNaam, drankId, drankNaam, aantal);
        }
        public Order? GetById(int studentNr, int drankId)
        {
            Order? order = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"{BaseSelectQuery} WHERE studentennr = @studentnr AND drankid = @drankid";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentnr", studentNr);
                command.Parameters.AddWithValue("@drankid", drankId);

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
