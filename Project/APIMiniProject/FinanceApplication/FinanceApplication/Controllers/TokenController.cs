using FinanceApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly FinanceDbContext _context;
        public TokenController(IConfiguration configuration, FinanceDbContext con)
        {
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(configuration["Key"]!));
            _context = con;
        }
        [HttpPost]
        public string GenerateToken(User user)
        {
            string token = string.Empty;
            if (ValidateAdminUser(user.Email!, user.Password!))
            {
                var claims = new List<Claim>
                  {
                      new Claim(JwtRegisteredClaimNames.NameId,Convert.ToString(user.UserId)!),
                      new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                      new Claim(ClaimTypes.Role, "Admin")

                  };
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    SigningCredentials = cred,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(20)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var createToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(createToken);
                return token;
            }
            else if(ValidateUser(user.Email!, user.Password!))
            {
                var claims = new List<Claim>
                  {
                      new Claim(JwtRegisteredClaimNames.NameId,Convert.ToString(user.UserId)!),
                      new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                      new Claim(ClaimTypes.Role, "User")

                  };
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    SigningCredentials = cred,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(20)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var createToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(createToken);
                return token;
            }
            else
            {
                return string.Empty;
            }
        }
        private bool ValidateAdminUser(string email, string password)
        {
            var admins = _context.Managements.ToList();
            var admin = admins.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (admin != null)
            {
                return true;
            }
            return false;
        }
        private bool ValidateUser(string email, string password)
        {
            var users = _context.Users.ToList();
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
