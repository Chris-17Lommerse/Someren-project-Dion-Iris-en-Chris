using Someren_Applicatie.Models.Enums;

namespace Someren_Applicatie.Models
{
    public class Drink
    {
        public int DrankId { get; set; }
        public string DrankNaam { get; set; }
        public int Aantal {  get; set; }
        public TypeDrankje IsAlcoholisch { get; set; }
        public decimal Prijs { get; set; }

        public Drink()
        {
            DrankId = 0;
            DrankNaam = "";
            Aantal = 0;
            IsAlcoholisch = TypeDrankje.Nonalcoholisch;
            Prijs = 0;
        }

        public Drink(int drankId, string drankNaam, int aantal, TypeDrankje isAlcoholisch, decimal prijs)
        {
            DrankId = drankId;
            DrankNaam = drankNaam;
            Aantal = aantal;
            IsAlcoholisch = isAlcoholisch;
            Prijs = prijs;
        }
    }
}
