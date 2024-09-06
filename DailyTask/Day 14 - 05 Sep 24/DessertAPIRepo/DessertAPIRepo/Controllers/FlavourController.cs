using DessertAPIRepo.Models;
using DessertAPIRepo.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DessertAPIRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavourController : ControllerBase
    {
        private readonly FlavourService _favourService;

        public FlavourController(FlavourService favourService)
        {
            _favourService = favourService;
        }


        // GET: api/<FlavourController>
        [HttpGet]
        public async Task<IEnumerable<Flavour>> Get()
        {
            return await _favourService.GetAllFlavours();
        }

        // GET api/<FlavourController>/5
        [HttpGet("{id}")]
        public async Task<Flavour> Get(int id)
        {
            return await _favourService.GetFlavourById(id);
        }

        // POST api/<FlavourController>
        [HttpPost]
        public async Task Post([FromBody] Flavour flavour)
        {
            await _favourService.AddFlavour(flavour);
        }

        // PUT api/<FlavourController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Flavour flavour)
        {
            await _favourService.UpdateFlavour(id, flavour);
        }

        // DELETE api/<FlavourController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _favourService.DeleteFlavour(id);
        }
    }
}
