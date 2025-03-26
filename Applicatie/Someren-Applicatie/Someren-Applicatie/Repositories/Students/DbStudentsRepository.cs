using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Students
{
    public class DbStudentsRepository : BaseRepository, IStudentsRepository
    {
        // DONT USE CREATE IF NO ROOMS ARE MADE
        const string BaseSelectQuery = "SELECT studentennr, voornaam, achternaam, telefoonnr, klas, kamernr FROM dbo.STUDENT";

        public DbStudentsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public void Add(Student student)
        {
            //Error handling (eigenlijk moet het in nog een andere layer maar dat hebben wij nog niet geleerd)
            //bool IsRoomForStudent = IsBedAvailableInRoom(student.KamerNr);
            //if (!IsRoomForStudent) //als er geen ruimte is voor studenten dan geef een error
                //throw new Exception($"Room {student.KamerNr} is full");
            Student? checkStudent = GetByName(student.Voornaam, student.Achternaam);
            if (checkStudent != null) //als er al een student is met die naam dan geef een error (IN ZO'N KLEINE SCOPE IS HET NULLABLE ALS ER IEMAND IS MET DEZELFDE VOOR EN ACHTERNAAM IN EEN ANDER DEEL VAN NEDERLAND) {implementatie is justified}
                throw new Exception($"Student {student.Voornaam} {student.Achternaam} already exists!");

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

        /*public bool IsBedAvailableInRoom(string roomNr)
        {
            //Check methode om de kijken of er ruimte is in een kamer of niet
            int numberOfStudents = 0;
            int numberOfBeds = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT SK.aantal_slaapplekken, COUNT(ST.studentennr) AS numberOfStudentsInRoom " +
                                "FROM dbo.STUDENT AS ST " +
                                "JOIN dbo.SLAAPKAMER AS SK ON ST.kamernr = SK.kamernr " +
                                "WHERE ST.kamernr = @kamernr " +
                                "GROUP BY SK.aantal_slaapplekken;";
                //deze lange query geeft het aantal slaapplekken en de aantal studenten in die slaapkamer
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@kamernr", roomNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    numberOfStudents = (int)reader["numberOfStudentsInRoom"];
                    Console.WriteLine(numberOfStudents);
                    numberOfBeds = (int)reader["aantal_slaapplekken"];
                    Console.WriteLine(numberOfBeds);
                }
                reader.Close();
            }

            return numberOfStudents < numberOfBeds;
        }*/

        public void Delete(Student student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM dbo.STUDENT WHERE studentennr = @studentnr;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentnr", student.StudentNr);

                command.Connection.Open();
                int nRowsAffected = command.ExecuteNonQuery();
                if (nRowsAffected == 0) throw new Exception("Geen studenten gedelete");
            }
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //Niet makkelijk om een unified GET methode te maken want als je een filter er op wil zetten zoals GetId of GetLastName
                //dan moet je een parameter gebruiken maar je kan niet de parameter customizen want het is niet een string die je kan aanpassen ofzo
                //het is een apparte regel met code en als je bijvoorbeeld 2 parameters wilt doorgeven dan moet je ook 2 input parameter op de methode hebben
                //en dat kan oneindig doorgaan, wat ook moeilijk wordt is dat GetAll een while gebruikt bij read en GetById een if gebruikt en dan moet je gaan switchen tussen if en while
                //EN dat allemaal zit in 1 using.
                //Het is te doen maar het is de moeite niet waard.
                string query = BaseSelectQuery + " ORDER BY achternaam";
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
            Student? student = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = BaseSelectQuery + " WHERE studentennr = @studentnr";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@studentnr", studentNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) student = ReadStudent(reader);
                reader.Close();
            }

            return student;
        }

        public Student? GetByName(string firstName, string lastName)
        {
            Student? student = null;

            //Met GetByName bedoel ik voor- en achternaam

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = BaseSelectQuery + " WHERE voornaam = @voornaam AND achternaam = @achternaam";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@voornaam", firstName);
                command.Parameters.AddWithValue("@achternaam", lastName);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) student = ReadStudent(reader);
                reader.Close();
            }

            return student;
        }

        public void Update(Student student)
        {
            Student? previousStudent = GetById(student.StudentNr); //Info dat er origineel in stond
            Student? checkStudent = GetByName(student.Voornaam, student.Achternaam); //Student met dezelfde naam (als hij bestaat)
            if (checkStudent != null)
            {
                //deze lange [if] checkt eerst of de naam die je wilt hetzelfde is als een naam in de database EN als de naam die je wilt niet hetzelfde is als de vorige naam,
                //ALS dat allemaal waar is dan geeft hij een fout.
                if (((student.Voornaam + student.Achternaam) == (checkStudent.Voornaam + checkStudent.Achternaam)) && (student.Voornaam + student.Achternaam) != (previousStudent?.Voornaam + previousStudent?.Achternaam))
                    throw new Exception($"Student {student.Voornaam} {student.Achternaam} already exists!");
            }
            //check of vorige kamernummer niet hetzelfde is als het nieuwe kamernummer
            /*if (previousStudent?.KamerNr != student.KamerNr)
            {
                bool IsRoomForStudent = IsBedAvailableInRoom(student.KamerNr);
                if (!IsRoomForStudent)
                    throw new Exception($"Room {student.KamerNr} is full");
            }*/

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
                if (nRowsAffected == 0) throw new Exception("Geen studenten aangepast.");
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

        public List<Student> GetByLastName(string lastName)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = BaseSelectQuery + " WHERE achternaam = @achternaam;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@achternaam", lastName);

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
    }
}
