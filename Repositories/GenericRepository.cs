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

        public async Task<IEnumerable<T>> SearchAsync(string query)
        {
            if (typeof(T) == typeof(Visitor))
            {
                var builder = Builders<Visitor>.Filter;
                var filter = builder.Regex(v => v.FullName, new BsonRegularExpression(query, "i")) |
                             builder.Regex(v => v.GuestOf, new BsonRegularExpression(query, "i")) |
                             builder.Regex(v => v.OtherRemarks, new BsonRegularExpression(query, "i"));

                var collection = _collection as IMongoCollection<Visitor>;
                return (IEnumerable<T>)await collection.Find(filter).ToListAsync();
            }
            else if (typeof(T) == typeof(Announcement))
            {
                var builder = Builders<Announcement>.Filter;
                var filter = builder.Regex(a => a.Title, new BsonRegularExpression(query, "i")) |
                             builder.Regex(a => a.Message, new BsonRegularExpression(query, "i"));

                var collection = _collection as IMongoCollection<Announcement>;
                return (IEnumerable<T>)await collection.Find(filter).ToListAsync();
            }
            else
            {
                throw new Exception($"Entity type {typeof(T).Name} is not supported for search.");
            }
        }

    }
}
