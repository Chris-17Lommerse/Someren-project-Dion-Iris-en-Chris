namespace Someren_Applicatie.Models
{
    public class Order
    {

        public int StudentNr { get; set; }
        public string StudentNaam { get; set; }
        public int DrankId { get; set; }
        public string DrankNaam { get; set; }
        public int Aantal {  get; set; }
        public Order()
        {
            StudentNr = 0;
            StudentNaam = "";
            DrankId = 0;
            DrankNaam = "";
            Aantal = 0;
        }

        public Order(int studentNr, string studentNaam, int drankId, string drankNaam, int aantal)
        {
            StudentNr = studentNr;
            StudentNaam = studentNaam;
            DrankId = drankId;
            DrankNaam = drankNaam;
            Aantal = aantal;
        }
    }
}
