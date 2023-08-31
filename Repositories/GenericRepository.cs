using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _collection = GetCollectionForType();
        }

        private IMongoCollection<T> GetCollectionForType()
        {
            if (typeof(T) == typeof(Visitor))
            {
                return _context.Visitors as IMongoCollection<T>;
            }
            else if (typeof(T) == typeof(Announcement))
            {
                return _context.Announcements as IMongoCollection<T>;
            }
            else
            {
                throw new Exception($"Entity type {typeof(T).Name} is not supported.");
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public IEnumerable<T> GetByDate(DateTime date)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var filter = Builders<T>.Filter.Eq("DateCreatedString", dateString);
            return _collection.Find(filter).ToList();
        }

        public async Task<T> UpdateAsync(T entity, string id)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
            return entity;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}
