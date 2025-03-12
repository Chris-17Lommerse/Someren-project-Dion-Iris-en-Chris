using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Students
{
    public interface IStudentsRepository
    {
        List<Student> GetAll();
        Student? GetById(int StudentNr);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
}
