namespace Someren_Applicatie.Models
{
    public class Order
    {
        public Student StudentNr { get; set; }
        public Drink DrankId { get; set; }
        public int Aantal {  get; set; }
        public Order()
        {
            StudentNr = new Student();
            DrankId = new Drink();
            Aantal = 0;
        }
        public Order(Student studentNr, Drink drankId, int aantal)
        {
            StudentNr = studentNr;
            DrankId = drankId;
            Aantal = aantal;
        }
    }
}
