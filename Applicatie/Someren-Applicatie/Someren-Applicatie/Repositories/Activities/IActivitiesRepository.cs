using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Activities
{
    public interface IActivitiesRepository
    {
        List<Activiteit> GetAll();
        Activiteit? GetById(int activiteitId);
        void Add(Activiteit activiteit);
        void Update(Activiteit activiteit);
        void Delete(Activiteit activiteit);
    }
}
