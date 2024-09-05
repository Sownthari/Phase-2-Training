using SocialMediaApplication.Models;

namespace SocialMediaApplication.Repository
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

    }
}
