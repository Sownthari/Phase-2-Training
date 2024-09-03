using Microsoft.AspNetCore.Mvc;
using MVCHotel.Repository;

namespace MVCHotel.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotel _hotelService;

        public HotelController(IHotel hotelService)
        {
            _hotelService = hotelService;
        }
        public IActionResult Index()
        {
            return View(_hotelService.GetAllHotels());
        }
    }
}
