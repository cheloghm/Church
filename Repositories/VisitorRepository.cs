using Church.Models;
using MongoDB.Driver;
using Church.RepositoryInterfaces;
using Church.Data;

namespace Church.Repositories
{
    // Visitor Repository
    public class VisitorRepository : IVisitorRepository
    {
        private readonly DataContext _context;

        public VisitorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            await _context.Visitors.InsertOneAsync(visitor);
            return visitor; // Return the visitor after adding it
        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            await _context.Visitors.ReplaceOneAsync(v => v.Id == visitor.Id, visitor);
            return visitor; // Return the visitor after updating it
        }

        public async Task<Visitor> GetVisitor(string id)
        {
            return await _context.Visitors.Find(visitor => visitor.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            return await _context.Visitors.Find(visitor => true).ToListAsync();
        }

        public async Task DeleteVisitor(string id)
        {
            await _context.Visitors.DeleteOneAsync(visitor => visitor.Id == id);
        }

        public IEnumerable<Visitor> GetVisitorsByDate(DateTime date)
        {
            var filter = Builders<Visitor>.Filter.Eq(v => v.DateEntered.Date, date.Date);
            return _context.Visitors.Find(filter).ToList();
        }

    }
}
