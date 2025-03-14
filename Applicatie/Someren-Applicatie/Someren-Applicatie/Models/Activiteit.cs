namespace Someren_Applicatie.Models
{
    public class Activiteit
    {
        public int ActiviteitId { get; set; }
        public string Naam { get; set; }
        public DateTime StartTijd { get; set; }
        public DateTime EindTijd { get; set;}

        public Activiteit(int activiteitId, string naam, DateTime startTijd, DateTime eindTijd)
        {
            ActiviteitId = activiteitId;
            Naam = naam;
            StartTijd = startTijd;
            EindTijd = eindTijd;
        }
    }
}
