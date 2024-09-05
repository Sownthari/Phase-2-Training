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
    public class DessertsController : ControllerBase
    {
        private readonly DessertDbContext _context;

        public DessertsController(DessertDbContext context)
        {
            _context = context;
        }

        // GET: api/Desserts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dessert>>> Getdesserts()
        {
            return await _context.desserts.Include(f => f.DessertFlavours!)
                .ThenInclude(f => f.flavour)
                .ToListAsync();
        }

        // GET: api/Desserts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dessert>> GetDessert(int id)
        {
            var dessert = await _context.desserts.FindAsync(id);

            if (dessert == null)
            {
                return NotFound();
            }

            return dessert;
        }

        // PUT: api/Desserts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDessert(int id, Dessert dessert)
        {
            if (id != dessert.DessertID)
            {
                return BadRequest();
            }

            _context.Entry(dessert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertExists(id))
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

        // POST: api/Desserts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dessert>> PostDessert(Dessert dessert)
        {
            _context.desserts.Add(dessert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDessert", new { id = dessert.DessertID }, dessert);
        }

        // DELETE: api/Desserts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessert(int id)
        {
            var dessert = await _context.desserts.FindAsync(id);
            if (dessert == null)
            {
                return NotFound();
            }

            _context.desserts.Remove(dessert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DessertExists(int id)
        {
            return _context.desserts.Any(e => e.DessertID == id);
        }
    }
}
