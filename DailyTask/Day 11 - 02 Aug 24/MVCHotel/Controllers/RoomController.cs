using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCHotel.Models;
using MVCHotel.Repository;

namespace MVCHotel.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoom _roomService;
        private readonly IHotel _hotelService;

        public RoomController(IRoom roomService, IHotel hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        public IActionResult Index()
        {

            return View(_roomService.GetAllRooms());
        }

        public IActionResult Details(int id)
        {
            return View(_roomService.GetRoom(id));
        }

        
        public IActionResult Create()
        {
            ViewBag.HotelId = new SelectList(_hotelService.GetAllHotels(), "HotelId", "HotelName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            _roomService.AddRoom(room);
            return RedirectToAction(nameof(Index));
        }


    }
}
