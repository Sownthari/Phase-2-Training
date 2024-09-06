using DessertAPIRepo.Interface;
using DessertAPIRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace DessertAPIRepo.Repository
{
    public class FlavourRepository : IFlavour
    {
        private readonly DessertContext _context;

        public FlavourRepository( DessertContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Flavour>> GetAllFlavours()
        {
            return await _context.Flavours.Include(df => df.DessertFlavours!).ThenInclude(d => d.dessert).ToListAsync() ?? throw new NullReferenceException();
        }

        public async Task<Flavour> GetFlavourById(int id)
        {
            return await _context.Flavours.Include(df => df.DessertFlavours!).ThenInclude(d => d.dessert).FirstOrDefaultAsync(f => f.FlavourId == id) ?? throw new KeyNotFoundException($"Flavour with ID {id} was not found.");
        }

        public async Task AddFlavour(Flavour flavour)
        {
            await _context.Flavours.AddAsync(flavour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlavour(int id,Flavour flavour)
        {
            var existingFlavour = await _context.Flavours.FindAsync(id);
            if(existingFlavour == null)
            {
                throw new KeyNotFoundException($"Flavour with Id {id} not found");
            }

            _context.Entry(existingFlavour).CurrentValues.SetValues(flavour);
            //_context.Flavours.Update(flavour);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteFlavour(int id)
        {
            var existingFlavour = await _context.Flavours.FindAsync(id);
            if (existingFlavour == null)
            {
                throw new KeyNotFoundException($"Flavour with Id {id} not found");
            }

            _context.Flavours.Remove(existingFlavour);
            await _context.SaveChangesAsync();
        }
    }
}
