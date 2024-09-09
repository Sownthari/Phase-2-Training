using FinanceApplication.Models;

namespace FinanceApplication.Interface
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(int id, User user);
        Task DeleteUser(int id);
    }
}
