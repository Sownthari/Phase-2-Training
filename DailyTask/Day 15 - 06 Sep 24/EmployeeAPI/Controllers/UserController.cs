using EmployeeAPI.Models;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
             _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<ValidUser>> Get()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task Add(ValidUser user)
        {
            await _userService.AddUser(user);
        }
    }
}
