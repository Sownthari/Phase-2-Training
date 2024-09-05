using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DessertAPI.Models;

namespace DessertAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavoursController : ControllerBase
    {
        private readonly DessertDbContext _context;

        public FlavoursController(DessertDbContext context)
        {
            _context = context;
        }

        // GET: api/Flavours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flavour>>> Getflavours()
        {
            return await _context.flavours.Include(f => f.DessertFlavours!)
                .ThenInclude(d => d.dessert)
                .ToListAsync();
        }

        // GET: api/Flavours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flavour>> GetFlavour(int id)
        {
            var flavour = await _context.flavours.FindAsync(id);

            if (flavour == null)
            {
                return NotFound();
            }

            return flavour;
        }

        // PUT: api/Flavours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlavour(int id, Flavour flavour)
        {
            if (id != flavour.FlavourId)
            {
                return BadRequest();
            }

            _context.Entry(flavour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlavourExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flavours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flavour>> PostFlavour(Flavour flavour)
        {
            _context.flavours.Add(flavour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlavour", new { id = flavour.FlavourId }, flavour);
        }

        // DELETE: api/Flavours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlavour(int id)
        {
            var flavour = await _context.flavours.FindAsync(id);
            if (flavour == null)
            {
                return NotFound();
            }

            _context.flavours.Remove(flavour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlavourExists(int id)
        {
            return _context.flavours.Any(e => e.FlavourId == id);
        }
    }
}
