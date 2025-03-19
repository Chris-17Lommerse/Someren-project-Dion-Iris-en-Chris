using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Students
{
    public class DbStudentsRepository : IStudentsRepository
    {
        // DONT USE CREATE IF NO ROOMS ARE MADE

        private readonly string? _connectionString;

        public DbStudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SomerenDatabase");
        }

        public void Add(Student student)
        {
            bool IsRoomForStudent = IsBedAvailableInRoom(student.KamerNr);
            if (!IsRoomForStudent)
                throw new Exception($"Room {student.KamerNr} is full");
            Student? checkStudent = GetByName(student.Voornaam, student.Achternaam);
            if (checkStudent != null)
                throw new Exception("Student already exists!");

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

        public bool IsBedAvailableInRoom(string roomNr)
        {
            int numberOfStudents = 0;
            int numberOfBeds = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT SK.aantal_slaapplekken, COUNT(ST.studentennr) AS numberOfStudentsInRoom " +
                                "FROM dbo.STUDENT AS ST " +
                                "JOIN dbo.SLAAPKAMER AS SK ON ST.kamernr = SK.kamernr " +
                                "WHERE ST.kamernr = @kamernr " +
                                "GROUP BY SK.aantal_slaapplekken;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", roomNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    numberOfStudents = (int)reader["numberOfStudentsInRoom"];
                    numberOfBeds = (int)reader["aantal_slaapplekken"];
                }
                reader.Close();
            }

            return numberOfStudents < numberOfBeds;
        }

        public void Delete(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM dbo.STUDENT WHERE studentennr = @studentnr;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentnr", student.StudentNr);

                command.Connection.Open();
                int nRowsAffected = command.ExecuteNonQuery();
                if (nRowsAffected == 0)
                {
                    throw new Exception("Geen studenten gedelete");
                }
            }
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

                command.Parameters.AddWithValue("@studentnr", studentNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                    student = ReadStudent(reader);
                reader.Close();
            }

            return student;
        }

        public Student? GetByName(string firstName, string lastName)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT studentennr, voornaam, achternaam, telefoonnr, klas, kamernr FROM dbo.STUDENT WHERE voornaam = @voornaam AND achternaam = @achternaam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@voornaam", firstName);
                command.Parameters.AddWithValue("@achternaam", lastName);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                    student = ReadStudent(reader);
                reader.Close();
            }

            return student;
        }

        public void Update(Student student)
        {
            Student previousStudent = GetById(student.StudentNr);
            Student? checkStudent = GetByName(student.Voornaam, student.Achternaam);
            if (((student.Voornaam != previousStudent.Voornaam) && (student.Achternaam != previousStudent.Achternaam)) && ((student.Voornaam == checkStudent.Voornaam) && (student.Achternaam == checkStudent.Achternaam)))
                throw new Exception("Student already exists!");
            if (previousStudent.KamerNr != student.KamerNr)
            {
                bool IsRoomForStudent = IsBedAvailableInRoom(student.KamerNr);
                if (!IsRoomForStudent)
                    throw new Exception($"Room {student.KamerNr} is full");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE dbo.STUDENT SET voornaam = @voornaam, achternaam = @achternaam, telefoonnr = @telefoonnr, klas = @klas, kamernr = @kamernr " +
                                "WHERE studentennr = @studentennr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentennr", student.StudentNr);
                command.Parameters.AddWithValue("@voornaam", student.Voornaam);
                command.Parameters.AddWithValue("@achternaam", student.Achternaam);
                command.Parameters.AddWithValue("@telefoonnr", student.TelefoonNr);
                command.Parameters.AddWithValue("@klas", student.Klas);
                command.Parameters.AddWithValue("@kamernr", student.KamerNr);

                command.Connection.Open();
                int nRowsAffected = command.ExecuteNonQuery();
                if (nRowsAffected == 0)
                {
                    throw new Exception("No records updated.");
                }
            }
        }

        private Student ReadStudent(SqlDataReader reader)
        {
            int studentnr = (int)reader["studentennr"];
            string voornaam = (string)reader["voornaam"];
            string achternaam = (string)reader["achternaam"];
            string telefoonnr = (string)reader["telefoonnr"];
            string klas = (string)reader["klas"];
            string kamernr = (string)reader["kamernr"];

            return new Student(studentnr, voornaam, achternaam, telefoonnr, klas, kamernr);
        }
    }
}
