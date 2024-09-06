using DessertAPIRepo.Models;

namespace DessertAPIRepo.Interface
{
    public interface IFlavour
    {
        Task<IEnumerable<Flavour>> GetAllFlavours();
        Task<Flavour> GetFlavourById(int id);
        Task AddFlavour (Flavour flavour);
        Task UpdateFlavour (int id, Flavour flavour);
        Task DeleteFlavour (int id);
    }
}
