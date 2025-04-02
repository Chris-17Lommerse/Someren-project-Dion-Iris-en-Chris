using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Supervisors
{
    public interface ISupervisorsRepository
    {
        List<Lecturer> GetByActivityId(int? id);
        string? GetActivityName(int? id);
        void Add(int docentnr, int activityid);
        void Delete(int docentnr, int activityid);
        bool DoesSupervisorExist(int docentnr, int activityid);
    }
}
