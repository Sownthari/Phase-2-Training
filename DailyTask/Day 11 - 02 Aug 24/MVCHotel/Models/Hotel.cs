using System.ComponentModel.DataAnnotations;

namespace MVCHotel.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 5, ErrorMessage = "Hotel name should have minimum length of 5")]
        public string HotelName { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
