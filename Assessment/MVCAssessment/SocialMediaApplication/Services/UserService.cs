using Microsoft.EntityFrameworkCore;
using SocialMediaApplication.Models;
using SocialMediaApplication.Repository;

namespace SocialMediaApplication.Services
{
    public class UserService : IUser
    {
        private readonly SocialMediaContext _context;

        public UserService(SocialMediaContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {

            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(p => p.posts).ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(p => p.posts).FirstOrDefault(u => u.UserId == id) ?? new User();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
