using Church.Models;

namespace Church.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId);
        Task<User> UpdateUser(User user);
    }
}
