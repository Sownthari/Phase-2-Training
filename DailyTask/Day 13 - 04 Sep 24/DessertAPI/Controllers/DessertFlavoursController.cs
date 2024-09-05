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
    public class DessertFlavoursController : ControllerBase
    {
        private readonly DessertDbContext _context;

        public DessertFlavoursController(DessertDbContext context)
        {
            _context = context;
        }

        // GET: api/DessertFlavours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DessertFlavour>>> GetdessertFlavours()
        {
            return await _context.dessertFlavours.ToListAsync();
        }

        // GET: api/DessertFlavours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DessertFlavour>> GetDessertFlavour(int id)
        {
            var dessertFlavour = await _context.dessertFlavours.FindAsync(id);

            if (dessertFlavour == null)
            {
                return NotFound();
            }

            return dessertFlavour;
        }

        // PUT: api/DessertFlavours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDessertFlavour(int id, DessertFlavour dessertFlavour)
        {
            if (id != dessertFlavour.DessertId)
            {
                return BadRequest();
            }

            _context.Entry(dessertFlavour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DessertFlavourExists(id))
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

        // POST: api/DessertFlavours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DessertFlavour>> PostDessertFlavour(DessertFlavour dessertFlavour)
        {
            _context.dessertFlavours.Add(dessertFlavour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DessertFlavourExists(dessertFlavour.DessertId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDessertFlavour", new { id = dessertFlavour.DessertId }, dessertFlavour);
        }

        // DELETE: api/DessertFlavours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDessertFlavour(int id)
        {
            var dessertFlavour = await _context.dessertFlavours.FindAsync(id);
            if (dessertFlavour == null)
            {
                return NotFound();
            }

            _context.dessertFlavours.Remove(dessertFlavour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DessertFlavourExists(int id)
        {
            return _context.dessertFlavours.Any(e => e.DessertId == id);
        }
    }
}
