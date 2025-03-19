using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Lecturers
{
    public interface ILecturersRepository
    {
        List<Lecturer> GetAll();
        Lecturer? GetById(int docentNr);
        void Add(Lecturer lecturer);
        void Update(Lecturer lecturer);
        void Delete(Lecturer lecturer);

        // Add this method for searching by last name
        List<Lecturer> GetByLastName(string lastName);
    }
}