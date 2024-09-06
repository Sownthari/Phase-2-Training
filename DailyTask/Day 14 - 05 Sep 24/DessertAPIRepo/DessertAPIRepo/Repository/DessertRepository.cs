using DessertAPIRepo.Interface;
using DessertAPIRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace DessertAPIRepo.Repository
{
    public class DessertRepository : IDessert
    {
        private readonly DessertContext _context;

        public DessertRepository(DessertContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dessert>> GetAllDesserts()
        {
            return await _context.Desserts.Include(df => df.DessertFlavours!).ThenInclude(f => f.flavour).ToListAsync(); 
        }

        public async Task<Dessert> GetDessertById(int id)
        {
        
            return await _context.Desserts
                .Include(df => df.DessertFlavours!)
                .ThenInclude(f => f.flavour)
                .FirstOrDefaultAsync(d => d.DessertID == id) ?? throw new KeyNotFoundException($"Dessert with ID {id} was not found.");

        }

        public async Task AddDessert(Dessert dessert)
        {
            await _context.Desserts.AddAsync(dessert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDessert(int id, Dessert dessert)
        {
            var existingDessert = _context.Desserts.Find(id);
            if (existingDessert == null)
            {
                throw new KeyNotFoundException("Dessert not found.");
            }

            _context.Entry(existingDessert).CurrentValues.SetValues(dessert);
            //_context.Desserts.Update(dessert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDessert(int id)
        {
            var existingDessert = await _context.Desserts.FirstOrDefaultAsync(d => d.DessertID == id);
            if (existingDessert == null)
            {
                throw new KeyNotFoundException("Dessert not found.");
            }

            _context.Desserts.Remove(existingDessert);
            await _context.SaveChangesAsync();
        }
    }
}
