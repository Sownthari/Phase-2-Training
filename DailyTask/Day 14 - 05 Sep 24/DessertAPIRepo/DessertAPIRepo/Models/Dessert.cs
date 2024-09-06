namespace DessertAPIRepo.Models
{
    public class Dessert
    {
        public int DessertID { get; set; }
        public string? DessertName { get; set; }
        public decimal Price { get; set; }
        public ICollection<DessertFlavour>? DessertFlavours { get; set; }
    }
}
