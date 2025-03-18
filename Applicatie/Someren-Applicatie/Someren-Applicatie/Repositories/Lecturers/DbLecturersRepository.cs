using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Repositories.Lecturers
{
    public class DbLecturersRepository : ILecturersRepository
    {
        private readonly string? _connectionString;

        public DbLecturersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }
        public void Add(Lecturer lecturer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO DOCENT (docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr)" +
                               "VALUES (@docentnr, @voornaam, @achternaam, @telefoonnr, @leeftijd, @kamernr); " +
                               "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@docentnr", lecturer.DocentNr);
                command.Parameters.AddWithValue("@voornaam", lecturer.Voornaam);
                command.Parameters.AddWithValue("@achternaam", lecturer.Achternaam);
                command.Parameters.AddWithValue("@telefoonnr", lecturer.TelefoonNr);
                command.Parameters.AddWithValue("@leeftijd", lecturer.Leeftijd);
                command.Parameters.AddWithValue("@kamernr", lecturer.KamerNr);

                command.Connection.Open();
                lecturer.DocentNr = Convert.ToInt32(command.ExecuteScalar());
                command.ExecuteNonQuery();
            }
        }

        //Get All
        public List<Lecturer> GetAll()
        {
            List<Lecturer> lecturers = new List<Lecturer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr FROM DOCENT ORDER BY achternaam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lecturer lecturer = ReadLecturer(reader);
                    lecturers.Add(lecturer);
                }
                reader.Close();
            }
            return lecturers;
        }

        //Delete
        public void Delete(Lecturer lecturer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM DOCENT WHERE docentnr = @docentnr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@docentnr", lecturer.DocentNr);

                command.Connection.Open();
                int nrOfAffectedRows = command.ExecuteNonQuery();
                if (nrOfAffectedRows == 0)
                {
                    throw new Exception("No records deleted");
                }
            }
        }

        //Read Lecturer
        private Lecturer ReadLecturer(SqlDataReader reader)
        {
            int docentnr = (int)reader["docentnr"];
            string voornaam = (string)reader["voornaam"];
            string achternaam = (string)reader["achternaam"];
            string telefoonnr = (string)reader["telefoonnr"];
            int leeftijd = (int)reader["leeftijd"];
            string kamernr = (string)reader["kamernr"];

            return new Lecturer(docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr);
        }

       //Get Id
        public Lecturer? GetById(int docentNr)
        {
            Lecturer lecturer = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr FROM DOCENT " +
                    "WHERE docentnr = @DocentNr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DocentNr", docentNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lecturer = ReadLecturer(reader);
                }
                reader.Close();
            }
            return lecturer;
        }

        public void Update(Lecturer lecturer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE dbo.DOCENT SET voornaam = @voornaam, achternaam = @achternaam, telefoonnr = @telefoonnr, leeftijd = @leeftijd, kamernr = @kamernr " +
                                "WHERE docentnr = @docentnr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@docentnr", lecturer.DocentNr);
                command.Parameters.AddWithValue("@voornaam", lecturer.Voornaam);
                command.Parameters.AddWithValue("@achternaam", lecturer.Achternaam);
                command.Parameters.AddWithValue("@telefoonnr", lecturer.TelefoonNr);
                command.Parameters.AddWithValue("@leeftijd", lecturer.Leeftijd);
                command.Parameters.AddWithValue("@kamernr", lecturer.KamerNr);

                command.Connection.Open();
                int nRowsAffected = command.ExecuteNonQuery();
                if (nRowsAffected == 0)
                {
                    throw new Exception("No records updated.");
                }
            }
        }
    }
}

