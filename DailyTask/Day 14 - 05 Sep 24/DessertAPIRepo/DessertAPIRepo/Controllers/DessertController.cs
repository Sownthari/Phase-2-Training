using DessertAPIRepo.Models;
using DessertAPIRepo.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DessertAPIRepo.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DessertController : ControllerBase
    {
        private readonly DessertService _dessertService;

        public DessertController(DessertService dessertService)
        {
            _dessertService = dessertService;
        }

        // GET: api/<DessertController>
        [HttpGet]
        public async Task<IEnumerable<Dessert>> Get()
        {
            return await _dessertService.GetAllDesserts();

        }

        // GET api/<DessertController>/5
        [HttpGet("{id}")]
        public async Task<Dessert> Get(int id)
        {
            return await _dessertService.GetDessertById(id);
        }

        // POST api/<DessertController>
        [HttpPost]
        public async Task Post([FromBody] Dessert dessert)
        {
            await _dessertService.AddDessert(dessert);
        }

        // PUT api/<DessertController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Dessert dessert)
        {
            await _dessertService.UpdateDessert(id, dessert);
        }

        // DELETE api/<DessertController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _dessertService.DeleteDessert(id);
        }
    }
}
