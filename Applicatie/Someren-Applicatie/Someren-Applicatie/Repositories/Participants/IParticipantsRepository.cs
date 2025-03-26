using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Participants
{
    public interface IParticipantsRepository
    {
        List<Student> GetByActivityId(int? id);
        string? GetActivityName(int? id);
        void Add(int studentnr, int activityid);
        void Delete(int studentnr, int activityid);
    }
}
