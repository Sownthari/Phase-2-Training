using FinanceApplication.Interface;
using FinanceApplication.Models;

namespace FinanceApplication.Service
{
    public class UserService
    {
        private readonly IUser _userRepo;

        public UserService(IUser userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task AddUser(User user)
        {
            await _userRepo.AddUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepo.DeleteUser(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task UpdateUser(int id, User user)
        {
            await _userRepo.UpdateUser(id, user);
        }
    }
}
