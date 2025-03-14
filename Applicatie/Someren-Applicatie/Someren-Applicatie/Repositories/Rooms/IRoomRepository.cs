using Someren_Applicatie.Models;

namespace Someren_Applicatie.Repositories.Rooms
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        Room? GetById(string roomId);
        void Add(Room room);
        void Update(Room room);
        void Delete(Room room);
    }
}
