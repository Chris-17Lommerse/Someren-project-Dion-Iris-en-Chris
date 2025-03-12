using System.Net.Mail;

namespace Someren_Applicatie.Models
{
    public class Lecturer
    {
        public int Docentnr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
        public int Leeftijd { get; set; }
        public int Kamernr { get; set; }

        public Lecturer(int docentnr, string voornaam, string achternaam, string telefoonnummer, int leeftijd, int kamernr)
        {
            Docentnr = docentnr;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Telefoonnummer = telefoonnummer;
            Leeftijd = leeftijd;
            Kamernr = kamernr;
        }
    }
}
