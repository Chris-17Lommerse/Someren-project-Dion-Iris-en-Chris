using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories.Lecturers;

namespace Someren_Applicatie.Repositories.Supervisors
{
    public class DbSupervisorsRepository : BaseRepository, ISupervisorsRepository
    {
        public DbSupervisorsRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(int docentnr, int activityid)
        {
            if (DoesSupervisorExist(docentnr, activityid))
                throw new Exception("Deze student neemt al deel aan deze activiteit");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO dbo.BEGELEIDER (docentnr, activiteitid) " +
                                $"VALUES (@docentnr, @activiteitid);";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@docentnr", docentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                docentnr = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public bool DoesSupervisorExist(int docentnr, int activityid)
        {
            bool doesSupervisorExist = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * " +
                                "FROM dbo.BEGELEIDER " +
                                "WHERE activiteitid = @activiteitid AND docentnr = @docentnr;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@docentnr", docentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                doesSupervisorExist = reader.Read();
                reader.Close();
            }
            return doesSupervisorExist;
        }


        private Lecturer ReadSupervisor(SqlDataReader reader)
        {
            int docentnr = (int)reader["docentnr"];
            string voornaam = (string)reader["voornaam"];
            string achternaam = (string)reader["achternaam"];

            return new Lecturer(docentnr, voornaam, achternaam, "", 0, "");
        }

        public List<Lecturer> GetByActivityId(int? id)
        {
            List<Lecturer> supervisors = new List<Lecturer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT D.docentnr, D.voornaam, D.achternaam " +
                                "FROM dbo.BEGELEIDER AS B " +
                                "JOIN dbo.DOCENT AS D ON D.docentnr = B.docentnr " +
                                "JOIN dbo.ACTIVITEIT AS A ON A.activiteitid = B.activiteitid " +
                                "WHERE A.activiteitid = @activiteitid;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitid", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lecturer supervisor = ReadSupervisor(reader);
                    supervisors.Add(supervisor);
                }
                reader.Close();
            }

            return supervisors;
        }

        public void Delete(int docentnr, int activityid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM dbo.BEGELEIDER WHERE docentnr = @docentnr AND activiteitid = @activiteitid;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@docentnr", docentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted.");
                }
            }
        }

        public string? GetActivityName(int? id)
        {
            string? activityName = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT naam " +
                                "FROM dbo.ACTIVITEIT " +
                                "WHERE activiteitid = @activiteitid;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@activiteitid", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    activityName = (string)reader["naam"];
                }
                reader.Close();
            }

            return activityName;
        }

    }
}
