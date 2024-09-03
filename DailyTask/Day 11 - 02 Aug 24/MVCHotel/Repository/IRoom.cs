using MVCHotel.Models;

namespace MVCHotel.Repository
{
    public interface IRoom
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoom(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);

    }
}
