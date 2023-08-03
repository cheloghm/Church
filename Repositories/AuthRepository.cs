using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Church.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            await _context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task UpdateUser(User user)
        {
            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}
