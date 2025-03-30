namespace Someren_Applicatie.Models
{
    public class DormitoryStudentsViewModel
    {
        public Room? Room { get; set; }
        public List<Student> StudentsWithARoom { get; set; }


        public DormitoryStudentsViewModel()
        {
            Room = new Room();
            StudentsWithARoom = new List<Student>();
        }

        public DormitoryStudentsViewModel(Room? room, List<Student> studentsWithARoom)
        {
            Room = room;
            StudentsWithARoom = studentsWithARoom;
        }
    }
}
