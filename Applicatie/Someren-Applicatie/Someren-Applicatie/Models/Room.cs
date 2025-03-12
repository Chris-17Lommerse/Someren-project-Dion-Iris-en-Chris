namespace Someren_Applicatie.Models
{
    public class Room
    {
        public string KamerNummer { get; set; }
        public int Aantal_Slaaplekken { get; set; }

        public Room()
        {
            KamerNummer = "";
            Aantal_Slaaplekken = 0;
        }
        public Room(string kamerNummer, int aantal_Slaaplekken)
        {
            KamerNummer = kamerNummer;
            Aantal_Slaaplekken = aantal_Slaaplekken;
        }
    }
}
