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
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = $"INSERT INTO DOCENT (voornaam, achternaam, telefoonnr, leeftijd, kamernr)" +
                                   "VALUES (@voornaam, @achternaam, @telefoonnr, @leeftijd, @kamernr); " +
                                   "SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);


                    command.Parameters.AddWithValue("@voornaam", lecturer.Voornaam);
                    command.Parameters.AddWithValue("@achternaam", lecturer.Achternaam);
                    command.Parameters.AddWithValue("@telefoonnr", lecturer.TelefoonNr);
                    command.Parameters.AddWithValue("@leeftijd", lecturer.Leeftijd);
                    command.Parameters.AddWithValue("@kamernr", lecturer.KamerNr);

                    command.Connection.Open();
                    lecturer.DocentNr = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kan activiteit niet toevoegen");
            }
        }

        //Get All
        public List<Lecturer> GetAll()
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            try
            {
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
            }
            catch (Exception ex)
            {
                throw new Exception("Kan docent niet laden");
            }
            return lecturers;
        }

        //Delete
        public void Delete(Lecturer lecturer)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Activititeit kan niet worden verwijderd");
            }
        }

        //Read Lecturer
        private Lecturer ReadLecturer(SqlDataReader reader)
        {
            try
            {
                int docentnr = (int)reader["docentnr"];
                string voornaam = (string)reader["voornaam"];
                string achternaam = (string)reader["achternaam"];
                string telefoonnr = (string)reader["telefoonnr"];
                int leeftijd = (int)reader["leeftijd"];
                string kamernr = (string)reader["kamernr"];

                return new Lecturer(docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr);
            }
            catch (Exception ex)
            {
                throw new Exception("Kan activiteiten niet lezen.");
            }
        }

        //Get Id
        public Lecturer? GetById(int docentNr)
        {
            Lecturer lecturer = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr FROM DOCENT " +
                        "WHERE docentnr = @DocentNr";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@DocentNr", docentNr);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        lecturer = ReadLecturer(reader);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kan de activiteit niet laden.");
            }
            return lecturer;
        }

        public void Update(Lecturer lecturer)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Docent kan niet worden aangepast");
            }
        }

        //search
        public List<Lecturer> GetByLastName(string lastName)
        {
            
                List<Lecturer> lecturers = new List<Lecturer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT docentnr, voornaam, achternaam, telefoonnr, leeftijd, kamernr " +
                                   "FROM DOCENT WHERE achternaam LIKE @LastName ORDER BY achternaam";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LastName", "%" + lastName + "%");

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
            catch (Exception ex)
            {
                throw new Exception("Docent kan niet gevonden worden");
            }
        }
    }
}

