using FinanceApplication.Interface;
using FinanceApplication.Models;
using FinanceApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;


namespace FinanceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private int _userId;

        public UserController(UserService userService)
        {
            {
                _userService = userService;
            }
        }
        
        private void StoreClaims()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                _userId = Convert.ToInt32(claimsIdentity.FindFirst(JwtRegisteredClaimNames.NameId)?.Value);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<User>> Get()
        {
            StoreClaims();
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            StoreClaims();
            if(id == _userId)
            {
                return await _userService.GetUserById(id);
            }
            throw new Exception($"Unauthorized access");  
        }

        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            StoreClaims();
            await _userService.AddUser(user);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User user)
        {
            if (id == _userId)
            {
                await _userService.UpdateUser(id, user);
            }
            throw new Exception($"Unauthorized access");
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id == _userId)
            {
                await _userService.DeleteUser(id);
            }
            throw new Exception($"Unauthorized access");
        }
    }
}
