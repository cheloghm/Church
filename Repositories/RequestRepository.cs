// RequestRepository.cs
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Church.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IMongoCollection<Request> _requests;

        public RequestRepository(IMongoDatabase database)
        {
            _requests = database.GetCollection<Request>("Requests");
        }

        public async Task<Request> AddRequest(Request request)
        {
            await _requests.InsertOneAsync(request);
            return request;
        }

        public async Task<Request> GetRequest(string id)
        {
            return await _requests.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _requests.Find(r => true).ToListAsync();
        }

        public async Task<Request> UpdateRequest(Request request)
        {
            await _requests.ReplaceOneAsync(r => r.Id == request.Id, request);
            return request;
        }

        public async Task DeleteRequest(string id)
        {
            await _requests.DeleteOneAsync(r => r.Id == id);
        }
    }
}
