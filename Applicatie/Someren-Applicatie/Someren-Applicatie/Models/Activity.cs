namespace Someren_Applicatie.Models
{
    public class Activity
    {
        public int Activiteitid { get; set; }
        public string Naam { get; set; }
        public DateTime Datum { get; set; }

        public Activity(int activiteitid, string naam, DateTime datum)
        {
            Activiteitid = activiteitid;
            Naam = naam;
            Datum = datum;
        }
    }
}
