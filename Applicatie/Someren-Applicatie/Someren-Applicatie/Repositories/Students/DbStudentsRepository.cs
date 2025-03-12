using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Students
{
    public class DbStudentsRepository : IStudentsRepository
    {
        // EDIT AND DELETE NEED TO BE ADDED, DONT USE CREATE IF NO ROOMS ARE MADE

        private readonly string? _connectionString;

        public DbStudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }

        public void Add(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO dbo.STUDENT (voornaam, achternaam, telefoonnr, klas, kamernr) " +
                                $"VALUES (@voornaam,@achternaam,@telefoonnr,@klas,@kamernr); " +
                                "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@voornaam", student.Voornaam);
                command.Parameters.AddWithValue("@achternaam", student.Achternaam);
                command.Parameters.AddWithValue("@telefoonnr", student.TelefoonNr);
                command.Parameters.AddWithValue("@klas", student.Klas);
                command.Parameters.AddWithValue("@kamernr", student.KamerNr);

                command.Connection.Open();
                student.StudentNr = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT studentennr, voornaam, achternaam, telefoonnr, klas, kamernr FROM dbo.STUDENT ORDER BY achternaam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = ReadStudent(reader);
                    students.Add(student);
                }
                reader.Close();
            }

            return students;
        }

        public Student? GetById(int studentNr)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT studentennr, voornaam, achternaam, telefoonnr, klas, kamernr FROM dbo.STUDENT WHERE studentennr = @studentnr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                command.Parameters.AddWithValue("@studentnr", studentNr);

                while (reader.Read())
                {
                    student = ReadStudent(reader);
                }
                reader.Close();
            }

            return student;
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }

        private Student ReadStudent(SqlDataReader reader)
        {
            int studentnr = (int)reader["studentennr"];
            string voornaam = (string)reader["voornaam"];
            string achternaam = (string)reader["achternaam"];
            string telefoonnr = (string)reader["telefoonnr"];
            string klas = (string)reader["klas"];
            int kamernr = (int)reader["kamernr"];

            return new Student(studentnr, voornaam, achternaam, telefoonnr, klas, kamernr);
        }
    }
}
