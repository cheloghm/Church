using Church.Models;
using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;

namespace Church.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> CreateRole(string roleName)
        {
            var role = new Role { Name = roleName };
            return await _roleRepository.CreateRole(role);
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleRepository.RoleExists(roleName);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _roleRepository.GetRoleByName(name);
        }
    }
}
