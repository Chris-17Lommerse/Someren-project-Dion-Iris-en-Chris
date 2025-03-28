namespace Someren_Applicatie.Models
{
    public class DormitoryStudentsViewModel
    {
        public int KamerNummer { get; set; }
        public List<Student> StudentsWithARoom { get; set; }
        public List<Student> StudentsWithoutARoom { get; set; }

        public DormitoryStudentsViewModel()
        {
            KamerNummer = 0;
            StudentsWithARoom = new List<Student>();
            StudentsWithoutARoom = new List<Student>();
        }

        public DormitoryStudentsViewModel(int kamerNummer, List<Student> studentsWithARoom, List<Student> studentsWithoutARoom)
        {
            KamerNummer = kamerNummer;
            StudentsWithARoom = studentsWithARoom;
            StudentsWithoutARoom = studentsWithoutARoom;
        }
    }
}
