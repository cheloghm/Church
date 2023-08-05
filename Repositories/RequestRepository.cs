// RequestRepository.cs
using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Church.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DataContext _context;

        public RequestRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Request> AddRequest(Request request)
        {
            await _context.Requests.InsertOneAsync(request);
            return request;
        }

        public async Task<Request> GetRequest(string id)
        {
            return await _context.Requests.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _context.Requests.Find(r => true).ToListAsync();
        }

        public async Task<Request> UpdateRequest(Request request)
        {
            await _context.Requests.ReplaceOneAsync(r => r.Id == request.Id, request);
            return request;
        }

        public async Task DeleteRequest(string id)
        {
            await _context.Requests.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Request>> GetRequestsByDate(DateTime date)
        {
            return await _context.Requests
                .Find(r => r.DateEntered.Date == date.Date)
                .ToListAsync();
        }

    }
}
