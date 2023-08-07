using Church.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> GetUsersByRoleId(string roleId);
        Task<IEnumerable<User>> SearchUsersByName(string name);
        Task<User> UpdateUser(User user);
    }
}
