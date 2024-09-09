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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        [Authorize(Roles = "Employee, Manager, Admin")]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employeeService.GetAllEmployees();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee, Manager, Admin")]
        public async Task<Employee> Get(int id)
        {
            return await _employeeService.GetEmployeeById(id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public async Task Post([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public void Delete(int id)
        {
        }
    }
}
