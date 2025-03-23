namespace Someren_Applicatie.Models
{
    public class Order
    {
        public int StudentNr { get; set; }
        public int DrankId { get; set; }
        public int Aantal {  get; set; }

        public Order()
        {
            StudentNr = 0;
            DrankId = 0;
            Aantal = 0;
        }
        public Order(int studentNr, int drankId, int aantal)
        {
            StudentNr = studentNr;
            DrankId = drankId;
            Aantal = aantal;
        }
    }
}
