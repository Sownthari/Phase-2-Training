using DessertAPIRepo.Models;
using DessertAPIRepo.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DessertAPIRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DessertFlavourController : ControllerBase
    {
        private readonly DessertService _dessertService;
        private readonly FlavourService _flavourService;

        public DessertFlavourController(DessertService dessertService, FlavourService flavourService)
        {
            _dessertService = dessertService;
            _flavourService = flavourService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFlavourToDessert(DessertFlavour dessertFlavour)
        {
            Dessert dessert = await _dessertService.GetDessertById(dessertFlavour.DessertId);
            if (dessert == null)
            {
                throw new KeyNotFoundException($"Dessert with id {dessertFlavour.DessertId} not found");
            }
            Flavour flavour = await _flavourService.GetFlavourById(dessertFlavour.FlavourId);
            if (flavour == null)
            {
                throw new KeyNotFoundException($"Flavour with id {dessertFlavour.FlavourId} not found");
            }
            DessertFlavour? addedFlavour = dessert.DessertFlavours?.FirstOrDefault(f => f.FlavourId == dessertFlavour.FlavourId);
            if( addedFlavour!= null)
            {
                return Conflict("Dessert already has this flavour");
                
            }
            dessert.DessertFlavours?.Add(dessertFlavour);
            flavour.DessertFlavours?.Add(dessertFlavour);

            await _dessertService.UpdateDessert(dessert.DessertID, dessert);
            await _flavourService.UpdateFlavour(flavour.FlavourId, flavour);
            return Ok("Flavour added to dessert successfully");

        }
    }
}
