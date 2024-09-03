using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCHotel.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string RoomNo { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        [Range(1000,100000, ErrorMessage = "Prize should be between 1000 and 1000000")]
        public decimal Price { get; set; }

        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
    }
}
