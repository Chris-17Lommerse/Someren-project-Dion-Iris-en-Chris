namespace Someren_Applicatie.Models
{
    public class Drink
    {
        public int DrankId { get; set; }
        public string DrankNaam { get; set; }
        public int Aantal {  get; set; }
        public bool IsAlcoholisch { get; set; }
        public decimal Prijs { get; set; }

        public Drink()
        {
            DrankId = 0;
            DrankNaam = "";
            Aantal = 0;
            IsAlcoholisch = false;
            Prijs = 0;
        }

        public Drink(int drankId, string drankNaam, int aantal, bool isAlcoholisch, decimal prijs)
        {
            DrankId = drankId;
            DrankNaam = drankNaam;
            Aantal = aantal;
            IsAlcoholisch = isAlcoholisch;
            Prijs = prijs;
        }
    }
}
