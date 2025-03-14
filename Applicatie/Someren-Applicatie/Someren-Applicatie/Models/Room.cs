using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Models
{
    public class Room
    {
        public string KamerNummer { get; set; }
        public int AantalSlaaplekken { get; set; }
        public TypeKamer TypeKamer { get; set;}
        public Room()
        {
            KamerNummer = "";
            AantalSlaaplekken = 0;
            TypeKamer = TypeKamer.Student;
        }

        public Room(string kamerNummer, int aantal_Slaaplekken, TypeKamer typeKamer)
        {
            KamerNummer = kamerNummer;
            AantalSlaaplekken = aantal_Slaaplekken;
            TypeKamer = typeKamer;
        }
    }
}
