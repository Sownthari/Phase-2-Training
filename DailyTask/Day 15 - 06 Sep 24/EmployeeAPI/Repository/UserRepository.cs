using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class UserRepository : IUserValid
    {
        private readonly EmployeeContext _context;
        public UserRepository(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ValidUser>> GetAllUsers()
        {
            return await _context.ValidUsers.ToListAsync();
        }

        public async Task AddUser(ValidUser user)
        {
            await _context.ValidUsers.AddAsync(user);
        }
    }
}
