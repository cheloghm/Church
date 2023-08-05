// IRequestService.cs
using Church.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.ServiceInterfaces
{
    public interface IRequestService
    {
        Task<Request> AddRequest(Request request);
        Task<Request> GetRequest(string id);
        Task<IEnumerable<Request>> GetAllRequests();
        Task<Request> UpdateRequest(Request request);
        Task DeleteRequest(string id);
        Task<IEnumerable<Request>> GetRequestsByDate(DateTime date);
    }
}
