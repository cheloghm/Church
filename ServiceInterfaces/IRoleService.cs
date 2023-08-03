using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRole(string roleName);
        Task<Role> GetRoleById(string id);
        Task<List<Role>> GetAllRoles();
        Task<bool> RoleExists(string roleName);
        Task<Role> GetRoleByName(string name);
    }
}
