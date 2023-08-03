using Church.Models;
using System.Threading.Tasks;

namespace Church.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExists(string email);
        Task UpdateUser(User user);
    }
}
