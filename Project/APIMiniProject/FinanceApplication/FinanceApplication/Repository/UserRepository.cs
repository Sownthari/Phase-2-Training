using FinanceApplication.Interface;
using FinanceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApplication.Repository
{
    public class UserRepository : IUser
    {
        private readonly FinanceDbContext _context;

        public UserRepository(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync() ?? throw new KeyNotFoundException($"No users found");
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id) ?? throw new KeyNotFoundException($"No users found with id {id}");
        }

        public async Task UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }
}
