namespace Someren_Applicatie.Models
{
    public class Lecturer
    {
        public int DocentNr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string TelefoonNr { get; set; }
        public int Leeftijd { get; set; }
        public string KamerNr { get; set; }


        public Lecturer() 
        {
            DocentNr = 0;
            Voornaam = "";
            Achternaam = "";
            TelefoonNr = "";
            Leeftijd = 0;
            KamerNr = "";
        }
        public Lecturer(int docentNr, string voornaam, string achternaam, string telefoonNr, int leeftijd, string kamerNr)
        {
            DocentNr = docentNr;
            Voornaam = voornaam;
            Achternaam = achternaam;
            TelefoonNr = telefoonNr;
            Leeftijd = leeftijd;
            KamerNr = kamerNr;
        }
        
    }
}
