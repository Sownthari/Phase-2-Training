using DessertAPIRepo.Interface;
using DessertAPIRepo.Models;

namespace DessertAPIRepo.Service
{
    public class FlavourService
    {
        private readonly IFlavour _flavourRepo;

        public FlavourService(IFlavour flavourRepo)
        {
            _flavourRepo = flavourRepo;
        }

        public async Task<IEnumerable<Flavour>> GetAllFlavours()
        {
            return await _flavourRepo.GetAllFlavours();
        }

        public async Task<Flavour> GetFlavourById(int id)
        {
            return await _flavourRepo.GetFlavourById(id);
        }

        public async Task AddFlavour(Flavour flavour)
        {
            await _flavourRepo.AddFlavour(flavour);
        }

        public async Task UpdateFlavour(int id, Flavour flavour)
        {
            await _flavourRepo.UpdateFlavour(id, flavour);
        }

        public async Task DeleteFlavour(int id)
        {
            await _flavourRepo.DeleteFlavour(id);
        }
    }
}
