using EmployeeAPI.Models;

namespace EmployeeAPI.Interface
{
    public interface IUserValid
    {
        Task<IEnumerable<ValidUser>> GetAllUsers();
        Task AddUser(ValidUser user);
    }
}
