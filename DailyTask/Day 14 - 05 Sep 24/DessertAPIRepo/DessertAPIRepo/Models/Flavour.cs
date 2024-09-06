namespace DessertAPIRepo.Models
{
    public class Flavour
    {
        public int FlavourId { get; set; }
        public string? FlavourName { get; set; }
        public ICollection<DessertFlavour>? DessertFlavours { get; set; }
    }
}
