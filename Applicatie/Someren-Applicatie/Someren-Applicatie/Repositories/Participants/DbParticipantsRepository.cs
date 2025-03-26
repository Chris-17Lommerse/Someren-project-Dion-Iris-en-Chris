using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using System.Runtime.InteropServices;

namespace Someren_Applicatie.Repositories.Participants
{
    public class DbParticipantsRepository : BaseRepository, IParticipantsRepository
    {
        public DbParticipantsRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(int studentnr, int activityid)
        {
            if (DoesParticipantExist(studentnr, activityid))
                throw new Exception("Deze student neemt al deel aan deze activiteit");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO dbo.DEELNEMER (studentennr, activiteitid) " +
                                $"VALUES (@studentennr, @activiteitid);";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentennr", studentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                studentnr = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int studentnr, int activityid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM dbo.DEELNEMER WHERE studentennr = @studentnr AND activiteitid = @activiteitid;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentnr", studentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                int nRowsAffected = command.ExecuteNonQuery();
                if (nRowsAffected == 0) throw new Exception("Geen deelnemers verwijderd");
            }
        }

        public bool DoesParticipantExist(int studentnr, int activityid)
        {
            bool doesParticipantExist = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * " +
                                "FROM dbo.DEELNEMER " +
                                "WHERE activiteitid = @activiteitid AND studentennr = @studentennr;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentennr", studentnr);
                command.Parameters.AddWithValue("@activiteitid", activityid);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                doesParticipantExist = reader.Read();
                reader.Close();
            }
            return doesParticipantExist;
        }

        public string? GetActivityName(int? id)
        {
            string? activityName = null;

            using(SqlConnection connection = new SqlConnection(_connectionString))
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

        public List<Student> GetByActivityId(int? id)
        {
            List<Student> participants = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT S.studentennr, S.voornaam, S.achternaam " +
                                "FROM dbo.DEELNEMER AS D " +
                                "JOIN dbo.STUDENT AS S ON S.studentennr = D.studentennr " +
                                "JOIN dbo.ACTIVITEIT AS A ON A.activiteitid = D.activiteitid " +
                                "WHERE A.activiteitid = @activiteitid;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@activiteitid", id);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student participant = ReadParticipant(reader);
                    participants.Add(participant);
                }
                reader.Close();
            }

            return participants;
        }

        private Student ReadParticipant(SqlDataReader reader)
        {
            int studentnr = (int)reader["studentennr"];
            string voornaam = (string)reader["voornaam"];
            string achternaam = (string)reader["achternaam"];

            return new Student(studentnr, voornaam, achternaam, "", "", "");
        }
    }
}
