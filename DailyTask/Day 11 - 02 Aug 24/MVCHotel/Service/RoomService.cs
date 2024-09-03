using Microsoft.EntityFrameworkCore;
using MVCHotel.Models;
using MVCHotel.Repository;

namespace MVCHotel.Service
{
    public class RoomService : IRoom
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms.Include(r => r.Hotel).ToList();
        }

        public Room GetRoom(int id)
        {
            return _context.Rooms.Include(c => c.Hotel).FirstOrDefault(o => o.RoomId == id) ?? new Room();
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }


        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}
