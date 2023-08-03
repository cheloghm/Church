using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;

namespace Church.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _context.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }

        // other methods...

        public async Task<User> UpdateUser(User user)
        {
            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
            return user;
        }
    }
}
