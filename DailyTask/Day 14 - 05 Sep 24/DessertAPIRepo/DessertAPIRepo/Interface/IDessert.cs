using DessertAPIRepo.Models;

namespace DessertAPIRepo.Interface
{
    public interface IDessert
    {
        Task<IEnumerable<Dessert>> GetAllDesserts();
        Task<Dessert> GetDessertById(int id);
        Task AddDessert(Dessert dessert);
        Task UpdateDessert(int id, Dessert dessert);
        Task DeleteDessert(int id);
    }
}
