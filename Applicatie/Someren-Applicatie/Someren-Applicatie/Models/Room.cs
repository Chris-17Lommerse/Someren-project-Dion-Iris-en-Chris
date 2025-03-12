using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Models
{
    public class Room
    {
        public string KamerNummer { get; set; }
        public int Aantal_Slaaplekken { get; set; }
        public TypeKamer TypeKamer { get; set;}
        public Room()
        {
            KamerNummer = "";
            Aantal_Slaaplekken = 0;
            TypeKamer = TypeKamer;
        }

        public Room(string kamerNummer, int aantal_Slaaplekken, TypeKamer typeKamer)
        {
            KamerNummer = kamerNummer;
            Aantal_Slaaplekken = aantal_Slaaplekken;
            TypeKamer = typeKamer;
        }
    }
}
