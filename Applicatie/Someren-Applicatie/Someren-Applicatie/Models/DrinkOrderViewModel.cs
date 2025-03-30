namespace Someren_Applicatie.Models
{
    public class DrinkOrderViewModel
    {
        public List<Student> Students { get; set; }
        public List<Drink> Drinks { get; set; }
        public int SelectedStudentNr { get; set; }
        public int SelectedDrankId { get; set; }
        public int Aantal {  get; set; }
        public DrinkOrderViewModel()
        {
            Students = new List<Student>();
            Drinks = new List<Drink>();
        }

        public DrinkOrderViewModel(List<Student> students, List<Drink> drinks)
        {
            Students = students;
            Drinks = drinks;
        }
    }
}
