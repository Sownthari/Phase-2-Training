using MVCHotel.Models;

namespace MVCHotel.Repository
{
    public interface IHotel
    {
        IEnumerable<Hotel> GetAllHotels();
        //Hotel GetHotel(int id);
        //void AddHotel(Hotel hotel);

        //void UpdateHotel(int id, Hotel hotel);
        //void DeleteHotel(int id, Hotel hotel);
    }
}
