namespace Someren_Applicatie.Models
{
    public class DrinkOrderViewModel
    {
        public List<Drink> Drinks = new List<Drink>();
        public List<Student> Students = new List<Student>();
        public int Aantal {  get; set; }

        public DrinkOrderViewModel()
        {
            Drinks = new List<Drink>();
            Students = new List<Student>();
            Aantal = 0;
        }

        public DrinkOrderViewModel(List<Drink> drinks, List<Student> students, int aantal)
        {
            Drinks = drinks;
            Students = students;
            Aantal = aantal;
        }
    }
}
