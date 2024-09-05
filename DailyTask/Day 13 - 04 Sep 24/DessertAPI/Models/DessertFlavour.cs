using System.ComponentModel.DataAnnotations.Schema;

namespace DessertAPI.Models
{
    public class DessertFlavour
    {
        public int DessertId { get; set; }
        [ForeignKey("DessertId")]
        public Dessert? dessert { get; set; }
        public int FlavourId { get; set; }
        [ForeignKey("FlavourId")]
        public Flavour? flavour { get; set; }

    }
}
