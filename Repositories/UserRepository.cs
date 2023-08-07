using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Bson;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleId(string roleId)
        {
            return await _context.Users.Find(u => u.RoleId == roleId).ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersByName(string name)
        {
            var filter = Builders<User>.Filter.Regex(u => u.FirstName, new BsonRegularExpression(name, "i"))
                        | Builders<User>.Filter.Regex(u => u.MiddleName, new BsonRegularExpression(name, "i"))
                        | Builders<User>.Filter.Regex(u => u.LastName, new BsonRegularExpression(name, "i"));

            return await _context.Users.Find(filter).ToListAsync();
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
