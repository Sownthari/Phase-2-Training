using DessertAPIRepo.Interface;
using DessertAPIRepo.Models;

namespace DessertAPIRepo.Service
{
    public class DessertService
    {
        private readonly IDessert _dessertRepo;

        public DessertService(IDessert dessertRepo)
        {
            _dessertRepo = dessertRepo;
        }

        public async Task<IEnumerable<Dessert>> GetAllDesserts()
        {
            return await _dessertRepo.GetAllDesserts();
        }

        public async Task<Dessert> GetDessertById(int id)
        {
            return await _dessertRepo.GetDessertById(id);
        }

        public async Task AddDessert(Dessert dessert)
        {
            await _dessertRepo.AddDessert(dessert);
        }

        public async Task UpdateDessert(int id, Dessert dessert)
        {
            await _dessertRepo.UpdateDessert(id, dessert);
        }

        public async Task DeleteDessert(int id)
        {
            await _dessertRepo.DeleteDessert(id);
        }
    }
}
