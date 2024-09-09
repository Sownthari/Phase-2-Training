
using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Service
{
    public class UserService
    {
        private readonly IUserValid _userRepo;

        public UserService(IUserValid userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<ValidUser>> GetAllUsers()
        {
            return await _userRepo.GetAllUsers();
        }

        public async Task AddUser(ValidUser user)
        {
            await _userRepo.AddUser(user);
        }
    }
}
