using Church.Models;
using System.Threading.Tasks;

namespace Church.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(Role role);
        Task<Role> GetRoleById(string id);
        Task<List<Role>> GetAllRoles();
        Task<bool> RoleExists(string roleName);
        Task<Role> GetRoleByName(string name);
    }
}
