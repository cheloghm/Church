using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Church.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _context.Roles.InsertOneAsync(role);
            return role;
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _context.Roles.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.Find(_ => true).ToListAsync();
        }

        public async Task<bool> RoleExists(string roleName)
        {
            var role = await _context.Roles.Find(r => r.Name == roleName).FirstOrDefaultAsync();
            return role != null;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.Find(r => r.Name == name).FirstOrDefaultAsync();
        }
    }
}
