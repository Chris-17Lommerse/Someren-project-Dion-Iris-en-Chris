namespace Someren_Applicatie.Models
{
    public class Student
    {
        public int StudentNr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string TelefoonNr { get; set; }
        public int KamerNr { get; set; }

        public Student()
        {
            StudentNr = 0;
            Voornaam = "";
            Achternaam = "";
            TelefoonNr = "";
            KamerNr = 0;
        }

        public Student(int studentNr, string voornaam, string achternaam, string telefoonNr, int kamerNr)
        {
            StudentNr = studentNr;
            Voornaam = voornaam;
            Achternaam = achternaam;
            TelefoonNr = telefoonNr;
            KamerNr = kamerNr;
        }
    }
}
