using EmployeeAPI.Models;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController (OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        // GET: api/<OrganizationController>
        [HttpGet]
        [Authorize(Roles = "Employee, Manager, Admin")]
        public async Task<IEnumerable<Organization>> Get()
        {
            return await _organizationService.GetAllOrganizations();
        }

        // GET api/<OrganizationController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee, Manager, Admin")]
        public async Task<Organization> Get(int id)
        {
            return await _organizationService.GetOrganizationById(id);
        }

        // POST api/<OrganizationController>
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public async Task Post([FromBody] Organization organization)
        {
            await _organizationService.AddOrganization(organization); 
        }

        // PUT api/<OrganizationController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrganizationController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public void Delete(int id)
        {
        }
    }
}
