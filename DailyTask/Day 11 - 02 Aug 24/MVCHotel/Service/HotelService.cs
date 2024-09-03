using Microsoft.EntityFrameworkCore;
using MVCHotel.Models;
using MVCHotel.Repository;

namespace MVCHotel.Service
{
    public class HotelService : IHotel
    {
        private readonly HotelDbContext _context;

        public HotelService(HotelDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.Hotels.Include(r => r.Rooms).ToList();
        }

        //Hotel IHotel.GetHotel(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
