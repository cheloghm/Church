// RequestService.cs
using Church.Models;
using Church.ServiceInterfaces;
using Church.RepositoryInterfaces;
using System.Threading.Tasks;

namespace Church.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Request> AddRequest(Request request)
        {
            return await _requestRepository.AddRequest(request);
        }

        public async Task<Request> GetRequest(string id)
        {
            return await _requestRepository.GetRequest(id);
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _requestRepository.GetAllRequests();
        }

        public async Task<Request> UpdateRequest(Request request)
        {
            return await _requestRepository.UpdateRequest(request);
        }

        public async Task DeleteRequest(string id)
        {
            await _requestRepository.DeleteRequest(id);
        }
    }
}
