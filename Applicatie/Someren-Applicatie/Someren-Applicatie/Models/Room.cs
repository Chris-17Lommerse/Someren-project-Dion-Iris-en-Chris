namespace Someren_Applicatie.Models
{
    public class Room
    {
        public int KamerNummer { get; set; }
        public int Aantal_Slaaplekken { get; set; }

        public Room()
        {
            KamerNummer = 0;
            Aantal_Slaaplekken = 0;
        }
        public Room(int kamerNummer, int aantal_Slaaplekken)
        {
            KamerNummer = kamerNummer;
            Aantal_Slaaplekken = aantal_Slaaplekken;
        }
    }
}
